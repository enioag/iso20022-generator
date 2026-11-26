using System;
using System.Collections.Generic;

namespace iso20022_generator.entity
{
    /// <summary>
    /// Creditor of a transaction. The name is mandatory. If an address is supplied, City (TwnNm) and
    /// CountryCode (Ctry) are mandatory (SPS 2026: unstructured addresses are no longer allowed).
    /// </summary>
    public class Receiver
    {
        private string _countryCode;

        /// <summary>Name of the creditor. Max. 140 characters, for SEPA max. 70 characters.</summary>
        public string Name { get; set; }

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

        /// <summary>Hybrid address: max. 2 lines (70 characters each) with information which cannot be
        /// provided in a structured element. Must not repeat structured data.</summary>
        public List<string> AddressLines { get; set; } = new List<string>();

        /// <summary>Optional Legal Entity Identifier of the creditor.</summary>
        public string Lei { get; set; }

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
