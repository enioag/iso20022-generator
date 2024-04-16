using System;
using System.Collections.Generic;
using iso20022_generator;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using iso20022_generator.schema;
using Xunit;

namespace generator_test
{
    public class GeneratorTest
    {
        [Fact]
        public void InitGenerator()
        {
            var generator = new Pain001Generator(new Initialization()
            {
                UniqueDocumentId = Guid.NewGuid().ToString().Substring(0, 34),  // Must be unique for the bank within 90 days
                ContactDetailsName = "enio AG",
                ContactDetailsOther = "3.0.4",
                AutoCalculateControlSum = true
                // SenderBic = "ZKBKCHZZ80A" //ZKB
            },
                new Sender()
                {
                    SenderPartyName = "enio AG",
                    SenderIban = "CH90 8136 1000 0338 6282 8",
                    SenderBic = "CRESCHZZ80A", //CS
                });

            var p1 = generator.AddPaymentInfo(DateTime.Now.AddDays(10), "TRA", batchBookingSpezified:true);

            generator.AddTransaction(p1, new Receiver
            {
                Name = "Max Kälin",
                StreetName = "Dorfstrasse",
                StreetNumber = "9b",
                Zip = "8808",
                City = "Pfäffikon",
                CountryCode = "CH"
            },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH26 0840 1016 9700 6070 6",
                    ReferenceIdentification = "Reference Id for Receiver",
                    InstructionForDebtorAgent = "Instruction",
                    UnstructuredRemittanceInformation = "Invoice Number 4399",
                    InstructionsForCreditorAgent = new List<InstructionForCreditorAgent1> {
                        new InstructionForCreditorAgent1()
                        {
                            InstrInf = "Line 1 of InstructionForCreditorAgent"
                        },
                        new InstructionForCreditorAgent1()
                        {
                            Cd = Instruction3Code.CHQB,
                            CdSpecified = true,
                            InstrInf = "Line 2 of InstructionForCreditorAgent"
                        }
                    }
                    //ReceiverBIC = "MIGRCHZZXXX"
                });

            generator.AddTransaction(p1, new Receiver
            {
                Name = "Alp Alpweid",
                StreetName = "Alpstrasse",
                StreetNumber = "99",
                Zip = "8887",
                City = "Mels",
                CountryCode = "CH",
            },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH02 3080 8007 2045 5121 8",
                    ReferenceIdentification = "QRR Test",
                    QRReferenceNumber = "36 63580 00000 00000 30060 03574",
                    InstructionForDebtorAgent = "Instruction",
                    AdditionalRemittanceInformation = new string[] { "Auftrag vom 25.10.2022" }
                });

            var p2 = generator.AddPaymentInfo(DateTime.Now.AddDays(5), "TRF");
            generator.AddTransaction(p2, new Receiver
            {
                Name = "Marco Birchler",
                StreetName = "Hauptstrasse",
                StreetNumber = "13",
                Zip = "8840",
                City = "Einsiedelnäöü",
                CountryCode = "CH"
            },
            new TransactionIBANandQRR
            {
                CurrencyCode = "CHF",
                Amount = 100,
                ReceiverIban = "CH23 0024 5245 1002 3901 K",
                ReferenceIdentification = "Reference Id for Receiver",
                InstructionForDebtorAgent = "Instruction"
            });

            string ret = generator.GetPain001String();

            var generator2 = new Pain001Generator(new Initialization()
            {
                UniqueDocumentId = Guid.NewGuid().ToString().Substring(0, 34),  // Must be unique for the bank within 90 days
                ContactDetailsName = "enio AG",
                ContactDetailsOther = "3.0.4",
                AutoCalculateControlSum = true
                // SenderBic = "ZKBKCHZZ80A" //ZKB
            },
                new Sender()
                {
                    SenderPartyName = "enio AG",
                    SenderIban = "CH90 8136 1000 0338 6282 8",
                    SenderBic = "CRESCHZZ80A", //CS
                });

            var p3 = generator2.AddPaymentInfo(DateTime.Now.AddDays(10), "TRA");

            generator2.AddTransaction(p3, new Receiver
            {
                Name = "Test Name",
                StreetName = "",
                StreetNumber = "",
                Zip = "70372",
                City = "Stuttgart",
                CountryCode = "DE"
            },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "JPY",
                    Amount = 1117,
                    ReceiverIban = "CH23 0024 5245 1002 3901 K",
                    ReferenceIdentification = "69-10",
                    QRReferenceNumber = "36 63580 00000 00000 30060 03574",
                    InstructionForDebtorAgent = null
                });

            string ret2 = generator2.GetPain001String();


            var generatorph = new Pain001Generator(new Initialization()
                {
                    UniqueDocumentId = Guid.NewGuid().ToString().Substring(0, 34),  // Must be unique for the bank within 90 days
                    ContactDetailsName = "enio AG",
                    ContactDetailsOther = "3.0.4",
                    AutoCalculateControlSum = true
                },
                new Sender()
                {
                    SenderPartyName = "Muster AG",
                    SenderIban = "InitialWertInitialWert",
                    SenderBic = "HHHHCH22", //HBL
                });

            generatorph.Sender.SenderIban = "CH5656565656565656565";
            var ph = generatorph.AddPaymentInfo(DateTime.Now.AddDays(10), "TRA");

            generatorph.AddTransaction(ph, new Receiver
                {
                    Name = "Hypo AG",
                    StreetName = "",
                    StreetNumber = "",
                    Zip = "70372",
                    City = "Stuttgart",
                    CountryCode = "DE"
                },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH0200202020202020202",
                    ReferenceIdentification = "69-10",
                    QRReferenceNumber = "36 63580 00000 00000 30060 03574",
                    InstructionForDebtorAgent = "/Cost center/1/VAT/1/Project number/1/"
                });

            generatorph.Sender.SenderIban = "CH2929292929292929292";
            var ph2 = generatorph.AddPaymentInfo(DateTime.Now.AddDays(10), "TRA");

            generatorph.AddTransaction(ph2, new Receiver
                {
                    Name = "Hypo AG",
                    StreetName = "",
                    StreetNumber = "",
                    Zip = "70372",
                    City = "Stuttgart",
                    CountryCode = "DE"
                },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH0200202020202020202",
                    ReferenceIdentification = "69-10",
                    QRReferenceNumber = "36 63580 00000 00000 30060 03574",
                    InstructionForDebtorAgent = "/Cost center/1/VAT/1/Project number/1/"
                });

            generatorph.Sender.SenderIban = "CH0200202020202020202";
            var ph3 = generatorph.AddPaymentInfo(DateTime.Now.AddDays(10), "TRA");

            generatorph.AddTransaction(ph3, new Receiver
                {
                    Name = "Hypo AG",
                    StreetName = "",
                    StreetNumber = "",
                    Zip = "70372",
                    City = "Stuttgart",
                    CountryCode = "DE"
                },
                new TransactionIBANandQRR
                {
                    CurrencyCode = "CHF",
                    Amount = 200,
                    ReceiverIban = "CH7777777777777777777",
                    ReferenceIdentification = "69-10",
                    QRReferenceNumber = "36 63580 00000 00000 30060 03574",
                    InstructionForDebtorAgent = ""
                });

            string retph = generatorph.GetPain001String();


            Assert.True(true);
        }
    }
}