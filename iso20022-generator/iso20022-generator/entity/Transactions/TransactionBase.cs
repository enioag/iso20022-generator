using System;
using System.Collections.Generic;

namespace iso20022_generator.entity.Transactions
{
    public abstract class TransactionBase
    {
        private string _currencyCode;

        /// <summary>ISO 4217 currency code. D: CHF/EUR, D instant: CHF, S: EUR.</summary>
        public string CurrencyCode
        {
            get => _currencyCode;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length != 3)
                    throw new ArgumentException("CurrencyCode is not valid - must be 3 characters", nameof(value));
                _currencyCode = value.Trim().ToUpperInvariant();
            }
        }

        /// <summary>
        /// End-to-end identification, forwarded unchanged to the creditor. Max. 35 characters of the
        /// restricted reference character set. "NOTPROVIDED" is used if empty.
        /// </summary>
        public string ReferenceIdentification { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// Optional UETR (UUIDv4). If empty and <see cref="Initialization.GenerateUetr"/> is true, one is generated
        /// (except for SEPA).
        /// </summary>
        public Guid? Uetr { get; set; }

        /// <summary>Optional instruction for the debtor agent on C level (max. 140 characters).
        /// Not allowed for SEPA and instant payments.</summary>
        public virtual string InstructionForDebtorAgent { get; set; }

        /// <summary>Max. 2 instructions for the creditor agent. Only allowed for payment type X.</summary>
        public virtual List<InstructionForCreditorAgent> InstructionsForCreditorAgent { get; set; }

        /// <summary>Optional charge bearer. Must not be set on payment information level at the same time.</summary>
        public ChargeBearer? ChargeBearer { get; set; }

        /// <summary>Optional purpose code (Purp/Cd) according to the ISO external code list.</summary>
        public string PurposeCode { get; set; }

        /// <summary>Unstructured remittance information, max. 140 characters.
        /// Not allowed together with a QR-IBAN.</summary>
        public string UnstructuredRemittanceInformation { get; set; }

        // --- internal model used by the generator ------------------------------------------------

        /// <summary>IBAN of the creditor account (null if a proprietary account number is used).</summary>
        internal abstract string CreditorIban { get; }

        /// <summary>Proprietary account number of the creditor (only payment type X).</summary>
        internal virtual string CreditorAccountNumber => null;

        internal virtual string CreditorBic => null;

        /// <summary>Structured creditor reference and its type ("QRR" proprietary or "SCOR" code).</summary>
        internal virtual (string Reference, string Type)? CreditorReference => null;

        internal virtual string[] AdditionalRemittanceInfo => null;

        internal virtual (string ClearingSystem, string MemberId)? CreditorAgentClearing => null;
        internal virtual string CreditorAgentName => null;
        internal virtual PostalAddress CreditorAgentAddress => null;
    }
}
