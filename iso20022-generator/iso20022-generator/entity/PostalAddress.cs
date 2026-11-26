using System.Collections.Generic;

namespace iso20022_generator.entity
{
    /// <summary>
    /// Structured or hybrid postal address (IG v2.3, chapter 3.11).
    /// Town name and country are mandatory, street name, building number and post code are recommended.
    /// A maximum of two address lines (70 characters each) may be added for data which cannot be
    /// provided in a structured element (hybrid address).
    /// </summary>
    public class PostalAddress
    {
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public string PostCode { get; set; }
        public string TownName { get; set; }

        /// <summary>ISO 3166 alpha-2 country code</summary>
        public string CountryCode { get; set; }

        /// <summary>Max. 2 lines with 70 characters each (hybrid address)</summary>
        public List<string> AddressLines { get; set; } = new List<string>();

        internal bool IsEmpty =>
            string.IsNullOrWhiteSpace(StreetName) && string.IsNullOrWhiteSpace(BuildingNumber) &&
            string.IsNullOrWhiteSpace(PostCode) && string.IsNullOrWhiteSpace(TownName) &&
            string.IsNullOrWhiteSpace(CountryCode) && (AddressLines == null || AddressLines.Count == 0);
    }
}
