# Migration 3.x → 4.0

Version 4.0 generates `pain.001.001.09.ch.03` (Swiss Payment Standards 2026) instead of
`pain.001.001.03.ch.02`. The parallel phase for the old format ended in November 2026. If your bank
still requires the old format, keep using 3.x (branch `release/3.x`).

## Code that usually keeps working

- `new Pain001Generator(init, sender)`, `AddPaymentInfo(...)`, `AddTransaction(...)`,
  `GetPain001String()`, `SavePain001ToDirectory(...)`
- `TransactionIBANandQRR`, `TransactionIBANandSCOR`, `Receiver`, `Sender`
- `var p = generator.AddPaymentInfo(...)` compiles unchanged. The return type is now `PaymentInformation`
  instead of the generated schema class `PaymentInstructionInformation3CH`.

## Breaking changes

| 3.x | 4.0 | Action |
|:---|:---|:---|
| `getDocumentObject()` returning generated schema classes | `GetXDocument()` returning `XDocument` | Adjust custom modifications to LINQ to XML |
| `iso20022_generator.schema.*` (generated classes) | removed | Use the classes in `iso20022_generator.entity` |
| `InstructionsForCreditorAgent` as `List<InstructionForCreditorAgent1>` | `List<InstructionForCreditorAgent>` (`Code`, `Information`) | Rename properties. Only allowed for payment type X (SPS 2026) |
| Payment method `"TRA"` (default) | `"TRF"` (default). `TRA` is mapped to `TRF`, `CHK` is rejected | none |
| `ContactDetailsName` / `ContactDetailsOther` | `SoftwareName` / `SoftwareVersion` (+ `SoftwareProvider`) | Old properties are `[Obsolete]` and mapped |
| Receiver address optional / partial | If an address is given, `City` and `CountryCode` are mandatory | Complete addresses |
| Receiver without name allowed | `Receiver.Name` is mandatory | Always set a name |
| QR-IBAN with free text, or QR reference with normal IBAN | rejected | QR-IBAN ⇔ QR reference |
| Free text *and* structured reference at the same time | rejected (Ustrd was silently ignored) | Use one of the two |
| `AutoCalculateControlSum` default `false` | default `true` | Set `false` and `ControlSum` if you need a fixed value |
| `InstructionForDebtorAgent` with SEPA / instant | rejected | Remove |
| `TransactionES`, `TransactionESR` | removed | Were already obsolete |
| Invalid IBAN / BIC / references | Exception in the setter (check digits) | Validate input data |
| Test project .NET 6 | .NET 10 | — |

## New features

- `AddInstantPaymentInfo(date, InstantPaymentMode.Instant | InstantPaymentMode.InstantWithFallback)`: domestic instant payment
- `AddSepaPaymentInfo(date, instant: false)`: SEPA payment
- `TransactionForeign`: foreign currency / cross-border with IBAN or account number, BIC or clearing code
- `TransactionIBAN`: IBAN with free text (formerly done with `TransactionIBANandQRR` without a reference)
- `PaymentInformation`: `ChargeBearer`, `CategoryPurpose`, `InstructionForDebtorAgent`, `DebitAdvice`
- `TransactionBase`: `ChargeBearer`, `PurposeCode`, `Uetr`
- `Sender`: address and `Lei`. `Receiver`: `AddressLines` (hybrid address) and `Lei`
- `Initialization`: `GenerateUetr`, `ValidateAgainstSchema`, `TreatDomesticEurAsForeign` (euroSIC end Nov 2027)
- `GetPaymentType(pmtInf, transaction)`: shows which SPS payment type a transaction is assigned to
- `Pain001ValidationException` with all schema errors
