# iso20022-generator

The iso20022-generator is a C# library which generates Swiss ISO 20022 credit transfer files (pain.001) with the minimum amount of information needed to set up valid documents.

**Version 4 generates `pain.001.001.09.ch.03` according to the Swiss Payment Standards 2026 (Implementation Guidelines Credit Transfer v2.3, valid from 14 November 2026).** The old format `pain.001.001.03.ch.02` is no longer supported; use version 3.x if your bank still requires it. Upgrading from 3.x: see [MIGRATION.md](https://github.com/enioag/iso20022-generator/blob/master/MIGRATION.md).

## Features

- Domestic payments, payment type D: QR-bill with QR reference (QRR), ISO creditor reference (SCOR), IBAN with free text
- Domestic instant payments, payment type D V2 (`LclInstrm` INST / ITP)
- SEPA payments, payment type S (optionally SEPA Instant)
- Foreign currency and cross-border payments, payment type X (IBAN or account number, BIC or clearing code)
- Structured and hybrid addresses (unstructured addresses are no longer allowed since November 2026)
- Several payment information blocks (B level) with their own execution date, each with several transactions
- UETR is generated automatically (not for SEPA)
- Software information in `InitgPty/CtctDtls` (NAME, PRVD, VRSN, SPSV)
- Validation:
  - business rules of the Implementation Guidelines per payment type, such as QR-IBAN ⇔ QR reference, address rules, currency, instructions and charge bearer
  - check digits of IBAN, QR reference and ISO 11649 reference
  - SPS character set
- Every generated file is validated against the official SIX schema `pain.001.001.09.ch.03.xsd`

Not supported: cheques (payment type C), regulatory reporting, ultimate debtor/creditor.

## Usage

```csharp
var generator = new Pain001Generator(
    new Initialization
    {
        UniqueDocumentId = "MSG-2026-11-26-001"    // unique for 90 days, max. 35 characters
    },
    new Sender
    {
        SenderPartyName = "Muster AG",
        SenderIban = "CH93 0076 2011 6238 5295 7",
        SenderBic = "ZKBKCHZZ80A",                 // optional, otherwise the IID is taken from the IBAN
        City = "Zürich",                           // optional address: City and CountryCode are mandatory if used
        CountryCode = "CH"
    });

// Domestic (D)
var domestic = generator.AddPaymentInfo(DateTime.Today.AddDays(1), batchBookingSpezified: true);
generator.AddTransaction(domestic,
    new Receiver { Name = "Max Kälin", StreetName = "Dorfstrasse", StreetNumber = "9b", Zip = "8808", City = "Pfäffikon", CountryCode = "CH" },
    new TransactionIBANandQRR
    {
        CurrencyCode = "CHF",
        Amount = 100.50m,
        ReceiverIban = "CH44 3199 9123 0008 8901 2",   // QR-IBAN
        QRReferenceNumber = "21 00000 00003 13947 14300 09017",
        ReferenceIdentification = "E2E-4711"
    });

// Domestic instant (D V2)
var instant = generator.AddInstantPaymentInfo(DateTime.Today);
generator.AddTransaction(instant,
    new Receiver { Name = "Anna Muster", City = "Bern", CountryCode = "CH" },
    new TransactionIBAN { CurrencyCode = "CHF", Amount = 50, ReceiverIban = "CH56 0483 5012 3456 7800 9" });

// SEPA (S)
var sepa = generator.AddSepaPaymentInfo(DateTime.Today.AddDays(1));
generator.AddTransaction(sepa,
    new Receiver { Name = "Müller GmbH", City = "Stuttgart", CountryCode = "DE" },
    new TransactionIBANandSCOR { CurrencyCode = "EUR", Amount = 1250, ReceiverIban = "DE89 3704 0044 0532 0130 00", StructuredCustomerReference = "RF18 5390 0754 7034" });

// Foreign currency / cross-border (X)
var foreign = generator.AddPaymentInfo(DateTime.Today.AddDays(2));
generator.AddTransaction(foreign,
    new Receiver { Name = "John Smith", StreetName = "Main Street", StreetNumber = "100", Zip = "10001", City = "New York", CountryCode = "US" },
    new TransactionForeign
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

string xml = generator.GetPain001String();           // or SavePain001ToDirectory(path) / GetXDocument()
```

Invalid input throws an `ArgumentException` with a description of the violated rule. If the generated document does not conform to the XML schema, a `Pain001ValidationException` is thrown. Its `Errors` property lists the schema errors.

More examples: [test files](https://github.com/enioag/iso20022-generator/tree/master/iso20022-generator/generator-test).

## Payment types

The payment type is determined per transaction as described in IG chapter 3.15:

| Type | Description | How | Currency |
|:---|:---|:---|:---|
| D | Domestic | `AddPaymentInfo` + CH/LI IBAN (or IID/BIC CH) | CHF, EUR (EUR until Nov 2027) |
| D V2 | Domestic instant | `AddInstantPaymentInfo` + CH/LI IBAN | CHF |
| S | SEPA | `AddSepaPaymentInfo` | EUR |
| X | Foreign currency / cross-border | `AddPaymentInfo` + all other cases or `TransactionForeign` | all |

euroSIC will be discontinued in November 2027. From then on, EUR payments within Switzerland are type X. Set `Initialization.TreatDomesticEurAsForeign = true` as soon as your bank requires it.

| Transaction class | Usage |
|:---|:---|
| `TransactionIBAN` | IBAN with optional unstructured remittance information (max. 140 characters) |
| `TransactionIBANandQRR` | QR-IBAN with QR reference (QR-bill) |
| `TransactionIBANandSCOR` | IBAN with ISO 11649 creditor reference (RF...) |
| `TransactionForeign` | IBAN or account number, creditor agent by BIC, clearing code or name and address |

## Limitations

The library was built for Swiss banks. Version 3.x was tested against the test environments of Raiffeisen, Credit Suisse, UBS and Zürcher Kantonalbank. Version 4 is validated against the SIX schema, but it has not yet been tested in any bank test environment. Instant payments and some details (e.g. `InstrForCdtrAgt`, `ITP`) depend on your bank's offer.

# Standards

- Swiss Payment Standards 2026, Implementation Guidelines Credit Transfer v2.3:
  https://www.six-group.com/dam/download/banking-services/standardization/sps/ig-credit-transfer-sps-2026-en.pdf
- XML schema pain.001.001.09.ch.03:
  https://www.six-group.com/dam/download/banking-services/standardization/sps/ig-credit-transfer-xml-schema-sps-2026-en.zip
- Delta guide SPS 2025 → 2026:
  https://www.six-group.com/dam/download/banking-services/standardization/sps/ig-delta-guide-sps2026-en.pdf
- Overview: https://www.six-group.com/en/products-services/banking-services/payment-standardization/swiss-payments.html

# Version Information

## 4.0.0-beta.1 (2026-11-26) - Breaking Changes, Beta
- New message format `pain.001.001.09.ch.03` (SPS 2026, IG v2.3). `pain.001.001.03.ch.02` was removed.
- XML is generated with LINQ to XML and validated against the embedded SIX schema. The generated XmlSerializer classes were removed, and `getDocumentObject()` was replaced by `GetXDocument()`.
- New: payment types S (`AddSepaPaymentInfo`), X (`TransactionForeign`) and D V2 instant (`AddInstantPaymentInfo`)
- New: `TransactionIBAN`, `PostalAddress`, hybrid addresses (`AddressLines`), sender address, LEI, UETR, charge bearer, purpose code, category purpose, debit advice
- New: business rule validation and check digit validation (IBAN, QR reference, RF reference)
- Software information is written to `InitgPty/CtctDtls/Othr` (NAME/PRVD/VRSN/SPSV)
- `AutoCalculateControlSum` is now `true` by default
- Fixed: `InstrId` is unique per message, and `SavePain001ToDirectory` now also updates the group header
- Removed obsolete `TransactionES` / `TransactionESR`
- Test project moved to .NET 10

## 3.1.0 (2024-04-16)
- added XML tag InstrForCdtrAgt (Index 2.81). The element may only be used in consultation with the commissioned financial institution. This element may have different characteristics and instructions depending on the financial institution.

## 3.0.5 (2024-03-14)
- changed minimum length of IBAN to 15 (Norway)
- updated several NuGet packages

## 3.0.4 (2023-07-18)
- added UnstructuredRemittanceInformation and AdditionalRemittanceInformation to TransactionIBANandSCOR

## 3.0.3 (2023-05-08)
- added boolean value batchBookingSpezified to enable export of pmtInf.BtchBookg flag properly

## 3.0.2 (2023-04-13)
- Split Street and StreetNumber into two properties
- Don't export postal adress properties if empty
- Don't export InstructionForDebtorAgent if empty

## 3.0.1 (2023-04-03)
- Corrected issue with unintentionally overriding of sender information

## 3.0.0 (2023-03-30) - Breaking Changes
- Separated Sender information from Initialzation
- Changed Sender information to public property
- Added README.md for nuget package and moved help documentation to this file

## 2.1.1 (2023-03-24)
Corrected null pointer exception on missing additional remittance information

## 2.1.0 (2022-11-22)
- Added possibility to change contact detail and contact detail other values (Index 1.8)
- Added configuration for payment method (TFA, TRA, CHK, ....) (Index 2.2)
- Removed field PaymentType -> obsolete
- Added additional remittance information field for QRR transaction
- Added unstructured remittance information for IBAN
- Removed ESR and ES transactions -> obsolete
- Added configuration for control sum field (Index 1.7)
- Added AutoCalculateControlSum field

## 2.0.0 (2022-10-12)
- Added InstructionForDebtorAgent  Index 2.85
- Updated help

## 1.6.1 (2022-10-15)
- Added IBAN and SCOR transaction. Allow empty address and receiver name
- Bump NuGet.CommandLine from 4.9.5 to 4.9.6

## 1.6.0 (2022-10-15)
- Added QR with Reference

## 1.4.0 (2022-10-14)
- Extended generator to add more than one payment info record including the feature to use a different required execution date per payment info

## 1.4.0 (2022-10-12)
- Implementation of ESR and ES 1, introduced transactions for each implementation
- FIX: Implemenetation of required BIC Code for PostFinance support

## 1.3.0 (2021-12-02)
- Fixed issue with amount in payment file

## 1.1.0 (2021-11-02)
- Fixed issue with setting property Street-Number (StrtNm, 2.79)

## 1.0.0 (2021-10-15)
- Initial release

# Links

- [License](LICENSE.md)
- [Homepage enio AG](https://www.enio.ch)
- [Releases](https://github.com/enioag/iso20022-generator/releases)
