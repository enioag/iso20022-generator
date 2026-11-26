using System;
using System.Collections.Generic;
using iso20022_generator.validation;

namespace iso20022_generator.entity
{
    /// <summary>
    /// Debtor (owner of the debit account). The address is optional because Swiss banks use their
    /// master data, but if supplied, City and CountryCode are mandatory.
    /// </summary>
    public class Sender
    {
        private string _senderIban;
        private string _senderBic = "";
        private string _countryCode;

        /// <summary>Name of the debtor, also used as initiating party.</summary>
        public string SenderPartyName { get; set; }

        /// <summary>IBAN of the debit account. Must not be a QR-IBAN.</summary>
        public string SenderIban
        {
            get => _senderIban;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Sender IBAN cannot be null or whitespace.", nameof(value));
                var iban = Checks.Compact(value);
                if (!Checks.IsValidIban(iban))
                    throw new ArgumentException($"Sender IBAN '{value}' is not valid", nameof(value));
                if (Checks.IsQrIban(iban))
                    throw new ArgumentException("Sender IBAN must not be a QR-IBAN", nameof(value));
                _senderIban = iban;
            }
        }

        /// <summary>Optional BIC of the debtor's bank. If empty, the institution identification (IID)
        /// is derived from the IBAN.</summary>
        public string SenderBic
        {
            get => _senderBic;
            set
            {
                var bic = Checks.Compact(value) ?? "";
                if (bic.Length > 0 && !Checks.IsValidBic(bic))
                    throw new ArgumentException($"Sender BIC '{value}' is not valid", nameof(value));
                _senderBic = bic;
            }
        }

        /// <summary>Optional Legal Entity Identifier of the debtor.</summary>
        public string Lei { get; set; }

        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        /// <summary>ISO 3166 alpha-2 country code, e.g. "CH"</summary>
        public string CountryCode
        {
            get => _countryCode;
            set
            {
                if (value != null && value.Trim().Length != 2)
                    throw new ArgumentException("CountryCode is not valid - must be 2 characters", nameof(value));
                _countryCode = value?.Trim().ToUpperInvariant();
            }
        }

        /// <summary>Hybrid address: max. 2 lines with 70 characters each.</summary>
        public List<string> AddressLines { get; set; } = new List<string>();

        internal PostalAddress ToPostalAddress() => new PostalAddress
        {
            StreetName = StreetName,
            BuildingNumber = StreetNumber,
            PostCode = Zip,
            TownName = City,
            CountryCode = CountryCode,
            AddressLines = AddressLines
        };
    }
}
