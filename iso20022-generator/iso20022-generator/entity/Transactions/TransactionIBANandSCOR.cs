using System;
using iso20022_generator.validation;

namespace iso20022_generator.entity.Transactions
{
    /// <summary>
    /// Payment with an ISO 11649 creditor reference (SCOR, "RF..."). Not allowed with a QR-IBAN.
    /// If no reference is set, the transaction behaves like <see cref="TransactionIBAN"/> (3.x compatibility).
    /// </summary>
    public class TransactionIBANandSCOR : TransactionIBAN
    {
        private string _scorNumber;

        /// <summary>ISO 11649 creditor reference, "RF" + check digits, max. 25 characters. Spaces are removed.</summary>
        public string StructuredCustomerReference
        {
            get => _scorNumber;
            set
            {
                var reference = Checks.Compact(value);
                if (!string.IsNullOrEmpty(reference) && !Checks.IsValidCreditorReference(reference))
                    throw new ArgumentException($"Creditor reference '{value}' is not a valid ISO 11649 reference", nameof(value));
                _scorNumber = string.IsNullOrEmpty(reference) ? null : reference;
            }
        }

        /// <summary>Additional remittance information (AddtlRmtInf), only together with a creditor reference.
        /// Domestic: max. 1 entry, SEPA: not allowed.</summary>
        public string[] AdditionalRemittanceInformation { get; set; }

        internal override (string Reference, string Type)? CreditorReference =>
            StructuredCustomerReference == null ? ((string, string)?)null : (StructuredCustomerReference, "SCOR");

        internal override string[] AdditionalRemittanceInfo => AdditionalRemittanceInformation;
    }
}
