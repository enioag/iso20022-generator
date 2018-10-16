using System;
using System.Collections.Generic;
using System.Text;

namespace iso20022_generator.entity
{
    public class Receiver
    {
        private string _countryCode;
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

        public string CountryCode
        {
            get => _countryCode;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 2)
                    throw new ArgumentException("CountryCode is not valid - must be 2 characters", nameof(value));
                _countryCode = value;
            }
        }
    }
}
