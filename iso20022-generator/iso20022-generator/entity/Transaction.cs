using System;
using System.Collections.Generic;
using System.Text;

namespace iso20022_generator.entity
{
    public class Transaction
    {
        private string _currencyCode;
        private string _receiverIban;

        public string ReceiverIban
        {
            get => _receiverIban;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("IBAN cannot be null or whitespace.", nameof(value));
                if (value.Length < 20)
                    throw new ArgumentException("IBAN is to short", nameof(value));
                _receiverIban = value.Replace(" ", "");
            }
        }

        public string ReferenceIdentification { get; set; }

        public string CurrencyCode
        {
            get => _currencyCode;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
                    throw new ArgumentException("CurrencyCode is not valid - must be 3 characters", nameof(value));
                _currencyCode = value;
            }
        }

        public decimal Amount { get; set; }

    }
}
