using System;
using System.Collections.Generic;
using iso20022_generator;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using iso20022_generator.validation;
using Xunit;
using static generator_test.TestData;

namespace generator_test
{
    public class ValidationTest
    {
        [Theory]
        [InlineData(SenderIban, true)]
        [InlineData(QrIban, true)]
        [InlineData(GermanIban, true)]
        [InlineData("CH93 0076 2011 6238 5295 8", false)]
        [InlineData("CH5656565656565656565", false)]
        public void IbanCheck(string iban, bool valid) => Assert.Equal(valid, Checks.IsValidIban(iban));

        [Theory]
        [InlineData(QrIban, true)]
        [InlineData(DomesticIban, false)]
        [InlineData(GermanIban, false)]
        public void QrIbanCheck(string iban, bool isQrIban) => Assert.Equal(isQrIban, Checks.IsQrIban(iban));

        [Theory]
        [InlineData(QrReference, true)]
        [InlineData("21 00000 00003 13947 14300 09018", false)]
        [InlineData("12345", false)]
        public void QrReferenceCheck(string reference, bool valid) => Assert.Equal(valid, Checks.IsValidQrReference(reference));

        [Theory]
        [InlineData(CreditorReference, true)]
        [InlineData("RF19 5390 0754 7034", false)]
        [InlineData("AB18 5390 0754 7034", false)]
        public void CreditorReferenceCheck(string reference, bool valid) => Assert.Equal(valid, Checks.IsValidCreditorReference(reference));

        [Theory]
        [InlineData("MSG-1", true)]
        [InlineData("Ref (1) + 2, a.b/c:d?'", true)]
        [InlineData("/start", false)]
        [InlineData("end/", false)]
        [InlineData("a//b", false)]
        [InlineData(" leading", false)]
        [InlineData("under_score", false)]
        [InlineData("0123456789012345678901234567890123456", false)]
        public void ReferenceCharacterSet(string value, bool valid) => Assert.Equal(valid, Checks.IsValidReference(value));

        [Theory]
        [InlineData("Zürich Ærø Šibenik Ţară €", true)]
        [InlineData("Emoji 😀", false)]
        [InlineData("Кириллица", false)]
        public void SpsCharacterSet(string value, bool valid) => Assert.Equal(valid, Checks.IsValidSpsText(value));

        [Fact]
        public void QrIban_WithoutQrReference_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, SwissReceiver(),
                new TransactionIBANandQRR { CurrencyCode = "CHF", Amount = 1, ReceiverIban = QrIban, UnstructuredRemittanceInformation = "x" }));
        }

        [Fact]
        public void QrReference_WithNormalIban_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, SwissReceiver(),
                new TransactionIBANandQRR { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban, QRReferenceNumber = QrReference }));
        }

        [Fact]
        public void InvalidReferences_AreRejectedBySetters()
        {
            Assert.Throws<ArgumentException>(() => new TransactionIBANandQRR { QRReferenceNumber = "12345" });
            Assert.Throws<ArgumentException>(() => new TransactionIBANandSCOR { StructuredCustomerReference = "RF00 1234" });
            Assert.Throws<ArgumentException>(() => new TransactionIBAN { ReceiverIban = "CH00 0000 0000 0000 0000 0" });
            Assert.Throws<ArgumentException>(() => new Sender { SenderIban = QrIban });
        }

        [Fact]
        public void Address_WithoutTownOrCountry_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            var receiver = SwissReceiver();
            receiver.City = null;

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, receiver,
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void Address_WithMoreThanTwoLines_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            var receiver = SwissReceiver();
            receiver.AddressLines = new List<string> { "a", "b", "c" };

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, receiver,
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void ReceiverName_IsMandatory()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, new Receiver(),
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void InstructionForCreditorAgent_IsRejectedForDomestic()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, SwissReceiver(), new TransactionIBAN
            {
                CurrencyCode = "CHF",
                Amount = 1,
                ReceiverIban = DomesticIban,
                InstructionsForCreditorAgent = new List<InstructionForCreditorAgent> { new InstructionForCreditorAgent { Information = "x" } }
            }));
        }

        [Fact]
        public void ChargeBearer_OnBothLevels_IsRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            p.ChargeBearer = ChargeBearer.SHAR;

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, SwissReceiver(),
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban, ChargeBearer = ChargeBearer.DEBT }));
        }

        [Fact]
        public void InvalidCharacters_AreRejected()
        {
            var generator = Generator();
            var p = generator.AddPaymentInfo(ExecutionDate);
            var receiver = SwissReceiver();
            receiver.Name = "Max �";

            Assert.Throws<ArgumentException>(() => generator.AddTransaction(p, receiver,
                new TransactionIBAN { CurrencyCode = "CHF", Amount = 1, ReceiverIban = DomesticIban }));
        }

        [Fact]
        public void UniqueDocumentId_MustUseReferenceCharacterSet()
        {
            Assert.Throws<ArgumentException>(() => new Pain001Generator(new Initialization { UniqueDocumentId = "a_b" }, Sender()));
            Assert.Throws<ArgumentException>(() => new Pain001Generator(new Initialization(), Sender()));
        }
    }
}
