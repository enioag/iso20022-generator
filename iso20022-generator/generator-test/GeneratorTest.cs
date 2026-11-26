using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using iso20022_generator;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using iso20022_generator.xml;
using Xunit;
using static generator_test.TestData;

namespace generator_test
{
    public class GeneratorTest
    {
        private static readonly XNamespace Ns = SchemaValidator.Namespace;

        [Fact]
        public void DomesticPayments_ProduceSchemaValidDocument()
        {
            var generator = Generator();
            var p1 = generator.AddPaymentInfo(ExecutionDate, batchBookingSpezified: true);

            generator.AddTransaction(p1, SwissReceiver(), new TransactionIBANandQRR
            {
                CurrencyCode = "CHF",
                Amount = 100.50m,
                ReceiverIban = QrIban,
                ReferenceIdentification = "QRR Test",
                QRReferenceNumber = QrReference,
                AdditionalRemittanceInformation = new[] { "Auftrag vom 25.10.2026" }
            });
            generator.AddTransaction(p1, SwissReceiver(), new TransactionIBANandSCOR
            {
                CurrencyCode = "CHF",
                Amount = 20,
                ReceiverIban = DomesticIban,
                ReferenceIdentification = "SCOR Test",
                StructuredCustomerReference = CreditorReference
            });
            generator.AddTransaction(p1, SwissReceiver(), new TransactionIBAN
            {
                CurrencyCode = "EUR",
                Amount = 30,
                ReceiverIban = DomesticIban,
                ReferenceIdentification = "IBAN Test",
                UnstructuredRemittanceInformation = "Rechnung 4399",
                InstructionForDebtorAgent = "Kostenstelle 1"
            });

            var doc = generator.GetXDocument();

            Assert.Empty(SchemaValidator.Validate(doc));
            Assert.Equal("3", doc.Descendants(Ns + "GrpHdr").Single().Element(Ns + "NbOfTxs").Value);
            Assert.Equal("150.5", doc.Descendants(Ns + "GrpHdr").Single().Element(Ns + "CtrlSum").Value);
            Assert.Equal(ExecutionDate.ToString("yyyy-MM-dd"), doc.Descendants(Ns + "ReqdExctnDt").Single().Element(Ns + "Dt").Value);
            Assert.Equal("ZKBKCHZZ80A", doc.Descendants(Ns + "DbtrAgt").Single().Descendants(Ns + "BICFI").Single().Value);
            Assert.Equal("QRR", doc.Descendants(Ns + "Prtry").First().Value);
            Assert.Equal("SCOR", doc.Descendants(Ns + "CdOrPrtry").Last().Element(Ns + "Cd").Value);
            Assert.All(doc.Descendants(Ns + "UETR"), u => Assert.True(Guid.TryParse(u.Value, out _)));
            Assert.Equal(3, doc.Descendants(Ns + "UETR").Count());
            Assert.Equal(new[] { "1-1", "1-2", "1-3" }, doc.Descendants(Ns + "InstrId").Select(e => e.Value));
            Assert.Equal(PaymentType.Domestic, generator.GetPaymentType(p1, new TransactionIBAN { CurrencyCode = "CHF", ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void SoftwareInformation_IsWrittenAsContactDetails()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });

            var others = generator.GetXDocument().Descendants(Ns + "CtctDtls").Single().Elements(Ns + "Othr")
                .ToDictionary(o => o.Element(Ns + "ChanlTp").Value, o => o.Element(Ns + "Id").Value);

            Assert.Equal("iso20022-generator", others["NAME"]);
            Assert.Equal("enio AG", others["PRVD"]);
            Assert.Equal("4.0.0", others["VRSN"]);
            Assert.Equal("0203", others["SPSV"]);
        }

        [Fact]
        public void DebtorAgent_UsesIidFromIban_WhenNoBic()
        {
            var sender = Sender();
            sender.SenderBic = "";
            var generator = new Pain001Generator(Init(), sender);
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });

            var agent = generator.GetXDocument().Descendants(Ns + "DbtrAgt").Single();

            Assert.Equal("CHBCC", agent.Descendants(Ns + "Cd").Single().Value);
            Assert.Equal("762", agent.Descendants(Ns + "MmbId").Single().Value);
        }

        [Fact]
        public void InstantPayment_SetsLocalInstrument()
        {
            var generator = Generator();
            var p = generator.AddInstantPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN
            {
                CurrencyCode = "CHF",
                Amount = 99.95m,
                ReceiverIban = DomesticIban,
                UnstructuredRemittanceInformation = "Sofort"
            });

            var doc = generator.GetXDocument();

            Assert.Equal("INST", doc.Descendants(Ns + "LclInstrm").Single().Element(Ns + "Cd").Value);
            Assert.Equal(PaymentType.DomesticInstant, generator.GetPaymentType(p, new TransactionIBAN { CurrencyCode = "CHF", ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void InstantPayment_InEur_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddInstantPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, SwissReceiver(),
                new TransactionIBAN { CurrencyCode = "EUR", Amount = 1, ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void SepaPayment_ProducesSchemaValidDocument_WithoutUetr()
        {
            var generator = Generator();
            var p = generator.AddSepaPaymentInfo(ExecutionDate);
            p.ChargeBearer = ChargeBearer.SLEV;
            generator.AddTransaction(p, GermanReceiver(), new TransactionIBANandSCOR
            {
                CurrencyCode = "EUR",
                Amount = 1250,
                ReceiverIban = GermanIban,
                ReferenceIdentification = "SEPA-1",
                StructuredCustomerReference = CreditorReference
            });

            var doc = generator.GetXDocument();

            Assert.Equal("SEPA", doc.Descendants(Ns + "SvcLvl").Single().Element(Ns + "Cd").Value);
            Assert.Equal("SLEV", doc.Descendants(Ns + "ChrgBr").Single().Value);
            Assert.Empty(doc.Descendants(Ns + "UETR"));
        }

        [Fact]
        public void SepaPayment_RejectsWrongCurrencyChargeBearerAndLongName()
        {
            var generator = Generator();
            var p = generator.AddSepaPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, GermanReceiver(),
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = GermanIban }));
            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, GermanReceiver(),
                new TransactionIBAN { CurrencyCode = "EUR", Amount = 1, ReceiverIban = GermanIban, ChargeBearer = ChargeBearer.DEBT }));

            var longName = GermanReceiver();
            longName.Name = new string('A', 71);
            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, longName,
                new TransactionIBAN { CurrencyCode = "EUR", Amount = 1, ReceiverIban = GermanIban }));
        }

        [Fact]
        public void ForeignPayment_WithIbanAndBic()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            var tx = new TransactionForeign
            {
                CurrencyCode = "USD",
                Amount = 500,
                ReceiverIban = GermanIban,
                ReceiverBIC = "COBADEFFXXX",
                ChargeBearer = ChargeBearer.SHAR,
                UnstructuredRemittanceInformation = "Invoice 77",
                InstructionsForCreditorAgent = new List<InstructionForCreditorAgent>
                {
                    new InstructionForCreditorAgent { Code = "PHOB", Information = "+49 711 000000" }
                }
            };
            generator.AddTransaction(p, GermanReceiver(), tx);

            var doc = generator.GetXDocument();

            Assert.Equal(PaymentType.Foreign, generator.GetPaymentType(p, tx));
            Assert.Equal("COBADEFFXXX", doc.Descendants(Ns + "CdtrAgt").Single().Descendants(Ns + "BICFI").Single().Value);
            Assert.Equal("PHOB", doc.Descendants(Ns + "InstrForCdtrAgt").Single().Element(Ns + "Cd").Value);
        }

        [Fact]
        public void ForeignPayment_WithAccountNumberAndClearingCode()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, new Receiver
            {
                Name = "John Smith",
                StreetName = "Main Street",
                StreetNumber = "100",
                Zip = "10001",
                City = "New York",
                CountryCode = "US",
                AddressLines = new List<string> { "Suite 5" }
            }, new TransactionForeign
            {
                CurrencyCode = "USD",
                Amount = 1000,
                ReceiverAccountNumber = "123456789",
                ReceiverBankClearingSystemCode = "USABA",
                ReceiverBankClearingMemberId = "026009593",
                ReceiverBankName = "Bank of America",
                ReceiverBankAddress = new PostalAddress { TownName = "New York", CountryCode = "US" },
                ChargeBearer = ChargeBearer.DEBT
            });

            var doc = generator.GetXDocument();

            Assert.Equal("123456789", doc.Descendants(Ns + "CdtrAcct").Single().Descendants(Ns + "Id").Last().Value);
            Assert.Equal("Suite 5", doc.Descendants(Ns + "AdrLine").Single().Value);
            Assert.Equal("USABA", doc.Descendants(Ns + "CdtrAgt").Single().Descendants(Ns + "Cd").Single().Value);
        }

        [Fact]
        public void ForeignPayment_AccountNumberWithoutAgent_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, GermanReceiver(),
                new TransactionForeign { CurrencyCode = "USD", Amount = 1, ReceiverAccountNumber = "4711" }));
        }

        [Fact]
        public void EurToGermany_WithoutSepaServiceLevel_IsForeign()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Equal(PaymentType.Foreign, generator.GetPaymentType(p, new TransactionIBAN { CurrencyCode = "EUR", ReceiverIban = GermanIban }));
        }

        [Fact]
        public void DomesticEur_CanBeTreatedAsForeign_AfterEuroSicDiscontinuation()
        {
            var init = Init();
            init.TreatDomesticEurAsForeign = true;
            var generator = new Pain001Generator(init, Sender());
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Equal(PaymentType.Foreign, generator.GetPaymentType(p, new TransactionIBAN { CurrencyCode = "EUR", ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void SavePain001ToDirectory_WritesUtf8WithoutBom()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });
            var file = Path.Combine(Path.GetTempPath(), $"pain001-{Guid.NewGuid():N}.xml");

            try
            {
                generator.SavePain001ToDirectory(file);
                var bytes = File.ReadAllBytes(file);

                Assert.NotEqual(0xEF, bytes[0]);
                Assert.Empty(SchemaValidator.Validate(XDocument.Load(file)));
                Assert.Contains("Max Kälin", File.ReadAllText(file));
            }
            finally
            {
                File.Delete(file);
            }
        }

        [Fact]
        public void GetPain001String_StartsWithUtf8Declaration()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });

            var xml = generator.GetPain001String();

            Assert.StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\"?>", xml, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("urn:iso:std:iso:20022:tech:xsd:pain.001.001.09", xml);
        }

        [Fact]
        public void LegacyPaymentMethodTra_IsMappedToTrf()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate, "TRA");
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });

            Assert.Equal("TRF", generator.GetXDocument().Descendants(Ns + "PmtMtd").Single().Value);
            Assert.Throws<ArgumentException>(() => generator.AddPaymentInfo(ExecutionDate, "CHK"));
        }

        [Fact]
        public void SchemaValidator_DetectsInvalidDocument()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban });
            var invalidCharacters = generator.GetXDocument();
            invalidCharacters.Descendants(Ns + "Cdtr").Single().Element(Ns + "Nm").Value = "Emoji 😀";
            var missingElement = generator.GetXDocument();
            missingElement.Descendants(Ns + "ReqdExctnDt").Single().Remove();

            Assert.Contains("Pattern constraint", Assert.Single(SchemaValidator.Validate(invalidCharacters)));
            Assert.Contains("ReqdExctnDt", Assert.Single(SchemaValidator.Validate(missingElement)));
        }

        [Fact]
        public void EmptyDocument_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => Generator().GetPain001String());
        }

        [Fact]
        public void ExecutionDateInPast_Throws()
        {
            Assert.Throws<ArgumentException>(() => Generator().AddPaymentInfo(DateTime.Today.AddDays(-1)));
        }
    }
}
