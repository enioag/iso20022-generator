using System;
using iso20022_generator;
using iso20022_generator.entity;
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
                SenderIban = "CH90 8136 1000 0338 6282 8"
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
                new Transaction
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH26 0840 1016 9700 6070 6",
                    ReferenceIdentification = "Reference Id for Receiver"
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
                new Transaction
                {
                    CurrencyCode = "CHF",
                    Amount = 100,
                    ReceiverIban = "CH23 0024 5245 1002 3901 K",
                    ReferenceIdentification = "Reference Id for Receiver"
                });

            generator.GetPain001String();
            Assert.True(true);
        }
    }
}