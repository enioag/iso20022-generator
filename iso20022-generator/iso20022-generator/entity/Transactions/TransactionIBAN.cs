using System;
using iso20022_generator.validation;

namespace iso20022_generator.entity.Transactions
{
    /// <summary>
    /// Payment to an IBAN with optional unstructured remittance information.
    /// Usable for domestic (D), instant (D V2), SEPA (S) and foreign (X) payments.
    /// </summary>
    public class TransactionIBAN : TransactionBase
    {
        private string _receiverIban;
        private string _receiverBic;

        public string ReceiverIban
        {
            get => _receiverIban;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("IBAN cannot be null or whitespace.", nameof(value));
                var iban = Checks.Compact(value);
                if (!Checks.IsValidIban(iban))
                    throw new ArgumentException($"IBAN '{value}' is not valid", nameof(value));
                _receiverIban = iban;
            }
        }

        /// <summary>Optional BIC of the creditor's bank. Not needed for CH/LI IBANs and SEPA.</summary>
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

        internal override string CreditorIban => ReceiverIban;
        internal override string CreditorBic => ReceiverBIC;
    }
}
