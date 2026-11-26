using System;
using System.IO;
using iso20022_generator;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;

var outDir = args[0];
Directory.CreateDirectory(outDir);
var date = DateTime.Today.AddDays(3);
var stamp = DateTime.Now.ToString("yyyyMMddHHmmss");

Pain001Generator New(string id) => new Pain001Generator(
    new Initialization { UniqueDocumentId = $"MSG-{id}-{stamp}" },
    new Sender { SenderPartyName = "enio AG", SenderIban = "CH93 0076 2011 6238 5295 7", SenderBic = "ZKBKCHZZ80A",
                 StreetName = "Churerstrasse", StreetNumber = "35", Zip = "8808", City = "Pfäffikon SZ", CountryCode = "CH" });

Receiver Ch() => new Receiver { Name = "Max Kälin", StreetName = "Dorfstrasse", StreetNumber = "9b", Zip = "8808", City = "Pfäffikon", CountryCode = "CH" };
Receiver De() => new Receiver { Name = "Müller GmbH", StreetName = "Königstrasse", StreetNumber = "1", Zip = "70173", City = "Stuttgart", CountryCode = "DE" };

void Save(string name, Pain001Generator g)
{
    g.SavePain001ToDirectory(Path.Combine(outDir, name));
    Console.WriteLine($"OK  {name}");
}

// D: QRR, SCOR, IBAN Freitext, EUR inland
var d = New("D");
var pd = d.AddPaymentInfo(date, batchBookingSpezified: true);
d.AddTransaction(pd, Ch(), new TransactionIBANandQRR { CurrencyCode = "CHF", Amount = 100.50m, ReceiverIban = "CH44 3199 9123 0008 8901 2", QRReferenceNumber = "21 00000 00003 13947 14300 09017", ReferenceIdentification = "E2E-QRR-1" });
d.AddTransaction(pd, Ch(), new TransactionIBANandSCOR { CurrencyCode = "CHF", Amount = 250m, ReceiverIban = "CH56 0483 5012 3456 7800 9", StructuredCustomerReference = "RF18 5390 0754 7034", ReferenceIdentification = "E2E-SCOR-1" });
d.AddTransaction(pd, Ch(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 75.20m, ReceiverIban = "CH56 0483 5012 3456 7800 9", UnstructuredRemittanceInformation = "Rechnung 4711", ReferenceIdentification = "E2E-IBAN-1" });
var pdEur = d.AddPaymentInfo(date);
d.AddTransaction(pdEur, Ch(), new TransactionIBAN { CurrencyCode = "EUR", Amount = 120m, ReceiverIban = "CH56 0483 5012 3456 7800 9", UnstructuredRemittanceInformation = "EUR Inland" });
Save("01_D_inland.xml", d);

// D V2: Instant und Instant mit Fallback
var i = New("INST");
var pi = i.AddInstantPaymentInfo(DateTime.Today);
i.AddTransaction(pi, Ch(), new TransactionIBAN { CurrencyCode = "CHF", Amount = 50m, ReceiverIban = "CH56 0483 5012 3456 7800 9", UnstructuredRemittanceInformation = "Instant" });
var pf = i.AddInstantPaymentInfo(DateTime.Today, InstantPaymentMode.InstantWithFallback);
i.AddTransaction(pf, Ch(), new TransactionIBANandQRR { CurrencyCode = "CHF", Amount = 60m, ReceiverIban = "CH44 3199 9123 0008 8901 2", QRReferenceNumber = "21 00000 00003 13947 14300 09017" });
Save("02_DV2_instant.xml", i);

// S: SEPA
var s = New("SEPA");
var ps = s.AddSepaPaymentInfo(date);
s.AddTransaction(ps, De(), new TransactionIBANandSCOR { CurrencyCode = "EUR", Amount = 1250m, ReceiverIban = "DE89 3704 0044 0532 0130 00", StructuredCustomerReference = "RF18 5390 0754 7034" });
s.AddTransaction(ps, De(), new TransactionIBAN { CurrencyCode = "EUR", Amount = 99.90m, ReceiverIban = "DE89 3704 0044 0532 0130 00", UnstructuredRemittanceInformation = "Rechnung 2026-001" });
Save("03_S_sepa.xml", s);

// X: Ausland / Fremdwährung
var x = New("X");
var px = x.AddPaymentInfo(date);
x.AddTransaction(px, new Receiver { Name = "John Smith", StreetName = "Main Street", StreetNumber = "100", Zip = "10001", City = "New York", CountryCode = "US" },
    new TransactionForeign { CurrencyCode = "USD", Amount = 1000m, ReceiverAccountNumber = "123456789", ReceiverBankClearingSystemCode = "USABA", ReceiverBankClearingMemberId = "026009593",
        ReceiverBankName = "Bank of America", ReceiverBankAddress = new PostalAddress { TownName = "New York", CountryCode = "US" }, ChargeBearer = ChargeBearer.DEBT, UnstructuredRemittanceInformation = "Invoice 123" });
x.AddTransaction(px, De(), new TransactionIBAN { CurrencyCode = "USD", Amount = 300m, ReceiverIban = "DE89 3704 0044 0532 0130 00", ReceiverBIC = "COBADEFFXXX", ChargeBearer = ChargeBearer.SHAR });
x.AddTransaction(px, Ch(), new TransactionIBAN { CurrencyCode = "GBP", Amount = 80m, ReceiverIban = "CH56 0483 5012 3456 7800 9" });
Save("04_X_ausland.xml", x);

// Alles in einer Datei
var all = New("ALL");
var a1 = all.AddPaymentInfo(date, batchBookingSpezified: true);
all.AddTransaction(a1, Ch(), new TransactionIBANandQRR { CurrencyCode = "CHF", Amount = 100.50m, ReceiverIban = "CH44 3199 9123 0008 8901 2", QRReferenceNumber = "21 00000 00003 13947 14300 09017" });
var a2 = all.AddSepaPaymentInfo(date);
all.AddTransaction(a2, De(), new TransactionIBAN { CurrencyCode = "EUR", Amount = 10m, ReceiverIban = "DE89 3704 0044 0532 0130 00", UnstructuredRemittanceInformation = "Test" });
var a3 = all.AddPaymentInfo(date);
all.AddTransaction(a3, De(), new TransactionIBAN { CurrencyCode = "USD", Amount = 20m, ReceiverIban = "DE89 3704 0044 0532 0130 00", ReceiverBIC = "COBADEFFXXX" });
Save("05_gemischt.xml", all);
