using System;
using iso20022_generator.validation;

namespace iso20022_generator.entity.Transactions
{
    /// <summary>
    /// QR-bill payment with QR reference (QRR). The QR reference requires a QR-IBAN and vice versa.
    /// If no QR reference is set, the transaction behaves like <see cref="TransactionIBAN"/> (3.x compatibility).
    /// </summary>
    public class TransactionIBANandQRR : TransactionIBAN
    {
        private string _qrReferenceNumber;

        /// <summary>27 digit QR reference with modulo 10 check digit. Spaces are removed.</summary>
        public string QRReferenceNumber
        {
            get => _qrReferenceNumber;
            set
            {
                var reference = Checks.Compact(value);
                if (!string.IsNullOrEmpty(reference) && !Checks.IsValidQrReference(reference))
                    throw new ArgumentException($"QR reference '{value}' is not valid (27 digits, modulo 10 check digit)", nameof(value));
                _qrReferenceNumber = string.IsNullOrEmpty(reference) ? null : reference;
            }
        }

        /// <summary>Additional remittance information (AddtlRmtInf), only together with a QR reference.
        /// Domestic: max. 1 entry with 140 characters.</summary>
        public string[] AdditionalRemittanceInformation { get; set; }

        internal override (string Reference, string Type)? CreditorReference =>
            QRReferenceNumber == null ? ((string, string)?)null : (QRReferenceNumber, "QRR");

        internal override string[] AdditionalRemittanceInfo => AdditionalRemittanceInformation;
    }
}
