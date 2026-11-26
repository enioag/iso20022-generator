using System.Linq;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using static iso20022_generator.validation.Checks;

namespace iso20022_generator.validation
{
    /// <summary>
    /// Business rules of the Swiss Implementation Guidelines Credit Transfer (SPS 2026, v2.3) that cannot be
    /// expressed by the XML schema.
    /// </summary>
    internal static class PaymentRules
    {
        /// <summary>Determines the SPS payment type of a transaction (IG chapter 3.15, figure 10).</summary>
        public static PaymentType Classify(PaymentInformation pmtInf, TransactionBase tx, Initialization init)
        {
            if (pmtInf.IsSepa)
                return PaymentType.Sepa;

            var domesticAccount = tx.CreditorIban != null
                ? IsChOrLiIban(tx.CreditorIban)
                : tx.CreditorAgentClearing?.ClearingSystem == "CHBCC" ||
                  BicCountry(tx.CreditorBic) == "CH" || BicCountry(tx.CreditorBic) == "LI";

            var domesticCurrency = tx.CurrencyCode == "CHF" || (tx.CurrencyCode == "EUR" && !init.TreatDomesticEurAsForeign);

            if (domesticAccount && domesticCurrency && !(tx is TransactionForeign))
                return pmtInf.IsInstant ? PaymentType.DomesticInstant : PaymentType.Domestic;

            return PaymentType.Foreign;
        }

        public static void Validate(PaymentInformation pmtInf, Receiver receiver, TransactionBase tx, Initialization init)
        {
            var type = Classify(pmtInf, tx, init);
            var label = $"Transaction '{tx.ReferenceIdentification}' (payment type {type})";

            // Amount / currency
            Require(tx.Amount > 0, $"{label}: amount must be greater than 0");
            Require(!string.IsNullOrEmpty(tx.CurrencyCode), $"{label}: currency is mandatory");
            if (type == PaymentType.Sepa)
            {
                Require(tx.CurrencyCode == "EUR", $"{label}: SEPA payments must be in EUR");
                Require(tx.Amount <= 999999999.99m, $"{label}: SEPA amount must not exceed 999,999,999.99");
            }
            if (pmtInf.IsInstant && !pmtInf.IsSepa)
                Require(type == PaymentType.DomesticInstant,
                    $"{label}: domestic instant payments are only possible in CHF to a CH/LI IBAN");
            if (type == PaymentType.DomesticInstant)
                Require(tx.CurrencyCode == "CHF", $"{label}: domestic instant payments must be in CHF");

            // References
            Require(string.IsNullOrEmpty(tx.ReferenceIdentification) || IsValidReference(tx.ReferenceIdentification),
                $"{label}: ReferenceIdentification (EndToEndId) must have max. 35 characters of A-Z a-z 0-9 space ' ( ) + , - . / : ? " +
                "and must not start with a space or '/', end with '/' or contain '//'");

            // Creditor
            Require(!string.IsNullOrWhiteSpace(receiver?.Name), $"{label}: receiver name is mandatory");
            Require(receiver.Name.Length <= (type == PaymentType.Sepa ? 70 : 140),
                $"{label}: receiver name too long (max. {(type == PaymentType.Sepa ? 70 : 140)} characters)");
            ValidateAddress(receiver.ToPostalAddress(), $"{label}: receiver address");
            Require(string.IsNullOrEmpty(receiver.Lei) || IsValidLei(receiver.Lei), $"{label}: receiver LEI is not valid");

            // Creditor account
            var iban = tx.CreditorIban;
            Require(iban != null || !string.IsNullOrWhiteSpace(tx.CreditorAccountNumber), $"{label}: creditor IBAN or account number is mandatory");
            Require(iban == null || string.IsNullOrWhiteSpace(tx.CreditorAccountNumber), $"{label}: use either IBAN or account number, not both");
            if (type == PaymentType.Sepa || type == PaymentType.DomesticInstant)
                Require(iban != null, $"{label}: an IBAN is mandatory");
            if (type == PaymentType.Domestic && iban == null)
                Require(tx.CreditorBic != null || tx.CreditorAgentClearing != null,
                    $"{label}: domestic payments to an account number need the BIC or IID of the creditor agent");
            Require(tx.CreditorAccountNumber == null || tx.CreditorAccountNumber.Length <= 34, $"{label}: account number max. 34 characters");

            // Creditor agent
            if (type == PaymentType.Foreign && iban == null)
                Require(tx.CreditorBic != null || tx.CreditorAgentClearing != null || !string.IsNullOrWhiteSpace(tx.CreditorAgentName),
                    $"{label}: creditor agent (BIC, clearing code or bank name and address) is mandatory for account numbers");
            Require(tx.CreditorBic == null || tx.CreditorAgentClearing == null, $"{label}: BIC and clearing member id must not be used together");
            Require(tx.CreditorBic == null || string.IsNullOrWhiteSpace(tx.CreditorAgentName), $"{label}: bank name must not be used together with a BIC");
            if (tx.CreditorAgentClearing is var (system, _))
            {
                Require(!string.IsNullOrWhiteSpace(system), $"{label}: clearing system code is mandatory with a clearing member id");
                if (type == PaymentType.Foreign && system != "CHBCC")
                    Require(!string.IsNullOrWhiteSpace(tx.CreditorAgentName) && tx.CreditorAgentAddress != null,
                        $"{label}: bank name and address are mandatory with a foreign clearing code");
            }
            if (!string.IsNullOrWhiteSpace(tx.CreditorAgentName))
            {
                Require(tx.CreditorAgentAddress != null && !tx.CreditorAgentAddress.IsEmpty, $"{label}: bank address is mandatory with a bank name");
                ValidateAddress(tx.CreditorAgentAddress, $"{label}: bank address");
            }

            // QR-IBAN / remittance information
            var isQrIban = IsQrIban(iban);
            var reference = tx.CreditorReference;
            if (isQrIban)
            {
                Require(type == PaymentType.Domestic || type == PaymentType.DomesticInstant, $"{label}: QR-IBAN only for domestic payments");
                Require(reference?.Type == "QRR", $"{label}: a QR-IBAN requires a QR reference");
                Require(string.IsNullOrEmpty(tx.UnstructuredRemittanceInformation), $"{label}: unstructured remittance information is not allowed with a QR-IBAN");
            }
            if (reference?.Type == "QRR")
                Require(isQrIban, $"{label}: a QR reference requires a QR-IBAN");
            if (reference?.Type == "SCOR")
                Require(IsValidCreditorReference(reference.Value.Reference), $"{label}: creditor reference is not a valid ISO 11649 reference");
            Require(reference == null || string.IsNullOrEmpty(tx.UnstructuredRemittanceInformation),
                $"{label}: use either a structured reference or unstructured remittance information, not both");
            Require(tx.UnstructuredRemittanceInformation == null || tx.UnstructuredRemittanceInformation.Length <= 140,
                $"{label}: unstructured remittance information max. 140 characters");

            var additional = tx.AdditionalRemittanceInfo?.Where(a => !string.IsNullOrWhiteSpace(a)).ToArray() ?? new string[0];
            if (additional.Length > 0)
            {
                Require(reference != null, $"{label}: additional remittance information only together with a structured reference");
                Require(type != PaymentType.Sepa, $"{label}: additional remittance information is not allowed for SEPA");
                Require(additional.Length <= (type == PaymentType.Foreign ? 3 : 1),
                    $"{label}: too many additional remittance information entries");
                Require(additional.All(a => a.Length <= 140), $"{label}: additional remittance information max. 140 characters");
            }
            // Instructions
            var cdtrInstructions = tx.InstructionsForCreditorAgent?.Count ?? 0;
            if (cdtrInstructions > 0)
            {
                Require(type == PaymentType.Foreign, $"{label}: instructions for the creditor agent are only allowed for payment type X");
                Require(cdtrInstructions <= 2, $"{label}: max. 2 instructions for the creditor agent");
                Require(tx.InstructionsForCreditorAgent.All(i => i.Code == null || new[] { "CHQB", "HOLD", "PHOB", "TELB" }.Contains(i.Code)),
                    $"{label}: instruction code must be CHQB, HOLD, PHOB or TELB");
            }
            if (!string.IsNullOrWhiteSpace(tx.InstructionForDebtorAgent))
            {
                Require(type != PaymentType.Sepa && type != PaymentType.DomesticInstant,
                    $"{label}: instruction for the debtor agent is not allowed for SEPA and instant payments");
                Require(tx.InstructionForDebtorAgent.Length <= 140, $"{label}: instruction for the debtor agent max. 140 characters");
            }

            // Charge bearer
            Require(!(pmtInf.ChargeBearer.HasValue && tx.ChargeBearer.HasValue),
                $"{label}: charge bearer must be set either on payment information or on transaction level");
            var chargeBearer = tx.ChargeBearer ?? pmtInf.ChargeBearer;
            if (type == PaymentType.Sepa && chargeBearer.HasValue)
                Require(chargeBearer == ChargeBearer.SLEV || chargeBearer == ChargeBearer.SHAR, $"{label}: SEPA only allows charge bearer SLEV or SHAR");

            // Character set of free texts
            var texts = new[] { receiver.Name, receiver.StreetName, receiver.City, tx.UnstructuredRemittanceInformation, tx.InstructionForDebtorAgent }
                .Concat(receiver.AddressLines ?? Enumerable.Empty<string>())
                .Concat(additional);
            Require(texts.All(IsValidSpsText), $"{label}: contains characters outside the SPS character set (Latin-1, Latin Extended-A, €)");
        }

        public static void ValidatePaymentInformation(PaymentInformation pmtInf, Sender sender)
        {
            var label = $"Payment information '{pmtInf.Id}'";
            Require(pmtInf.NumberOfTransactions > 0, $"{label}: contains no transactions");
            if (pmtInf.IsSepa || pmtInf.IsInstant)
                Require(string.IsNullOrWhiteSpace(pmtInf.InstructionForDebtorAgent),
                    $"{label}: instruction for the debtor agent is not allowed for SEPA and instant payments");
            Require(pmtInf.InstructionForDebtorAgent == null || pmtInf.InstructionForDebtorAgent.Length <= 140,
                $"{label}: instruction for the debtor agent max. 140 characters");
            Require(pmtInf.DebitAdvice == null || new[] { "NOA", "SIA", "CND", "CWD" }.Contains(pmtInf.DebitAdvice),
                $"{label}: debit advice must be NOA, SIA, CND or CWD");
            if (pmtInf.IsSepa)
                Require(string.IsNullOrEmpty(sender.SenderPartyName) || sender.SenderPartyName.Length <= 70,
                    $"{label}: debtor name max. 70 characters for SEPA");
        }

        public static void ValidateSender(Sender sender)
        {
            Require(sender != null, "Sender is mandatory");
            Require(!string.IsNullOrWhiteSpace(sender.SenderIban), "Sender IBAN is mandatory");
            Require(!string.IsNullOrWhiteSpace(sender.SenderPartyName), "Sender name is mandatory");
            Require(sender.SenderPartyName.Length <= 140, "Sender name max. 140 characters");
            Require(string.IsNullOrEmpty(sender.Lei) || IsValidLei(sender.Lei), "Sender LEI is not valid");
            ValidateAddress(sender.ToPostalAddress(), "Sender address");
            Require(IsValidSpsText(sender.SenderPartyName), "Sender name contains characters outside the SPS character set");
        }

        public static void ValidateAddress(PostalAddress address, string label)
        {
            if (address == null || address.IsEmpty)
                return;

            Require(!string.IsNullOrWhiteSpace(address.TownName), $"{label}: town name (City) is mandatory");
            Require(!string.IsNullOrWhiteSpace(address.CountryCode) && address.CountryCode.Trim().Length == 2,
                $"{label}: country code (ISO alpha-2) is mandatory");
            var lines = address.AddressLines?.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            Require(lines == null || lines.Count <= 2, $"{label}: max. 2 address lines (hybrid address)");
            Require(lines == null || lines.All(l => l.Length <= 70), $"{label}: address lines max. 70 characters");
            Require((address.StreetName?.Length ?? 0) <= 70, $"{label}: street name max. 70 characters");
            Require((address.BuildingNumber?.Length ?? 0) <= 16, $"{label}: building number max. 16 characters");
            Require((address.PostCode?.Length ?? 0) <= 16, $"{label}: post code max. 16 characters");
            Require(address.TownName.Length <= 35, $"{label}: town name max. 35 characters");
        }
    }
}
