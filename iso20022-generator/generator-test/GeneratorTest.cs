using System;
using iso20022_generator;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
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
                ExecutionDate = DateTime.Now.AddDays(3),
                SenderPartyName = "enio AG",
                SenderIban = "CH90 8136 1000 0338 6282 8",
                SenderBic = "CRESCHZZ80A" //CS
                // SenderBic = "ZKBKCHZZ80A" //ZKB
            });

            generator.AddTransaction(new Receiver
                {
                    Name = "Max Kälin",
                    StreetName = "Dorfstrasse",
                    StreetNumber = "9b",
                    Zip = "8808",
                    City = "Pfäffikon",
                    CountryCode = "CH"
                },
                new TransactionIBAN
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH26 0840 1016 9700 6070 6",
                    ReferenceIdentification = "Reference Id for Receiver",
                    ReceiverBIC = "MIGRCHZZXXX"
                });

            generator.AddTransaction(new Receiver
            {
                Name = "Marco Birchler",
                StreetName = "Hauptstrasse",
                StreetNumber = "13",
                Zip = "8840",
                City = "Einsiedeln",
                CountryCode = "CH"
            },
            new TransactionIBAN
            {
                CurrencyCode = "CHF",
                Amount = 100,
                ReceiverIban = "CH23 0024 5245 1002 3901 K",
                ReferenceIdentification = "Reference Id for Receiver"
            });

            generator.AddTransaction(new Receiver
                {
                    Name = "Strassenverkehrs- und Schifffahrtsamt",
                    StreetName = "Strasse",
                    StreetNumber = "",
                    Zip = "9001",
                    City = "St. Gallen",
                    CountryCode = "CH"
                },
                new TransactionESR()
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverAccount = "01-72765-4",
                    ReferenceIdentification = "Reference Id for Receiver",
                    ESRReferenceNumber = "80 00102 32416 20202 00126 57394"
                });

            generator.AddTransaction(new Receiver
                {
                    Name = "Alpkorporation Kohlschlag",
                    StreetName = "Kohlschlagerstrasse 2",
                    StreetNumber = "",
                    Zip = "8887",
                    City = "Mels",
                    CountryCode = "CH"
                },
                new TransactionES()
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverAccount = "70-4906-9",
                    ReferenceIdentification = "Reference Id for Receiver",
                });

            string ret = generator.GetPain001String();
            Assert.True(true);
        }
    }
}