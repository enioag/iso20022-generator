using System;
using iso20022_generator.validation;

namespace iso20022_generator.entity.Transactions
{
    /// <summary>
    /// Foreign currency (domestic) or cross-border payment, payment type X.
    /// The creditor account is either an IBAN or a proprietary account number. The creditor agent is
    /// identified by BIC (recommended), or by clearing code plus bank name and address, or by bank name
    /// and address only. For a proprietary account number a creditor agent is mandatory.
    /// </summary>
    public class TransactionForeign : TransactionBase
    {
        private string _receiverIban;
        private string _receiverBic;

        /// <summary>IBAN of the creditor. Either this or <see cref="ReceiverAccountNumber"/>.</summary>
        public string ReceiverIban
        {
            get => _receiverIban;
            set
            {
                var iban = Checks.Compact(value);
                if (!string.IsNullOrEmpty(iban) && !Checks.IsValidIban(iban))
                    throw new ArgumentException($"IBAN '{value}' is not valid", nameof(value));
                _receiverIban = string.IsNullOrEmpty(iban) ? null : iban;
            }
        }

        /// <summary>Proprietary account number (max. 34 characters) if the creditor has no IBAN.</summary>
        public string ReceiverAccountNumber { get; set; }

        /// <summary>BIC of the creditor's bank (recommended).</summary>
        public string ReceiverBIC
        {
            get => _receiverBic;
            set
            {
                var bic = Checks.Compact(value);
                if (!string.IsNullOrEmpty(bic) && !Checks.IsValidBic(bic))
                    throw new ArgumentException($"BIC '{value}' is not valid", nameof(value));
                _receiverBic = string.IsNullOrEmpty(bic) ? null : bic;
            }
        }

        /// <summary>Clearing system code of the creditor's bank, e.g. "USABA", "GBDSC", "CHBCC".
        /// Requires <see cref="ReceiverBankClearingMemberId"/>, bank name and address. Not together with a BIC.</summary>
        public string ReceiverBankClearingSystemCode { get; set; }

        /// <summary>Clearing member id (bank code) of the creditor's bank.</summary>
        public string ReceiverBankClearingMemberId { get; set; }

        /// <summary>Name of the creditor's bank. Not together with a BIC.</summary>
        public string ReceiverBankName { get; set; }

        /// <summary>Address of the creditor's bank, mandatory if a bank name is given.</summary>
        public PostalAddress ReceiverBankAddress { get; set; }

        /// <summary>Optional ISO 11649 creditor reference (SCOR, "RF..."), max. 25 characters.</summary>
        public string StructuredCustomerReference { get; set; }

        internal override string CreditorIban => ReceiverIban;
        internal override string CreditorAccountNumber => ReceiverAccountNumber;
        internal override string CreditorBic => ReceiverBIC;

        internal override (string Reference, string Type)? CreditorReference
        {
            get
            {
                var reference = Checks.Compact(StructuredCustomerReference);
                return string.IsNullOrEmpty(reference) ? ((string, string)?)null : (reference, "SCOR");
            }
        }

        internal override (string ClearingSystem, string MemberId)? CreditorAgentClearing =>
            string.IsNullOrWhiteSpace(ReceiverBankClearingMemberId)
                ? ((string, string)?)null
                : (ReceiverBankClearingSystemCode, ReceiverBankClearingMemberId);

        internal override string CreditorAgentName => ReceiverBankName;
        internal override PostalAddress CreditorAgentAddress => ReceiverBankAddress;
    }
}
