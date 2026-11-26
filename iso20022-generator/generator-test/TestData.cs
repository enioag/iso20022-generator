using System;
using iso20022_generator;
using iso20022_generator.entity;

namespace generator_test
{
    internal static class TestData
    {
        public const string SenderIban = "CH93 0076 2011 6238 5295 7";
        public const string DomesticIban = "CH56 0483 5012 3456 7800 9";
        public const string QrIban = "CH44 3199 9123 0008 8901 2";
        public const string QrReference = "21 00000 00003 13947 14300 09017";
        public const string CreditorReference = "RF18 5390 0754 7034";
        public const string GermanIban = "DE89 3704 0044 0532 0130 00";

        public static DateTime ExecutionDate => DateTime.Today.AddDays(5);

        public static Initialization Init() => new Initialization
        {
            UniqueDocumentId = "MSG-" + Guid.NewGuid().ToString("N").Substring(0, 20)
        };

        public static Sender Sender() => new Sender
        {
            SenderPartyName = "enio AG",
            SenderIban = SenderIban,
            SenderBic = "ZKBKCHZZ80A",
            StreetName = "Churerstrasse",
            StreetNumber = "35",
            Zip = "8808",
            City = "Pfäffikon SZ",
            CountryCode = "CH"
        };

        public static Pain001Generator Generator() => new Pain001Generator(Init(), Sender());

        public static Receiver SwissReceiver() => new Receiver
        {
            Name = "Max Kälin",
            StreetName = "Dorfstrasse",
            StreetNumber = "9b",
            Zip = "8808",
            City = "Pfäffikon",
            CountryCode = "CH"
        };

        public static Receiver GermanReceiver() => new Receiver
        {
            Name = "Müller GmbH",
            StreetName = "Königstrasse",
            StreetNumber = "1",
            Zip = "70173",
            City = "Stuttgart",
            CountryCode = "DE"
        };
    }
}
