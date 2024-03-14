using System;

namespace iso20022_generator.entity
{
    public class Sender
    {
        private string _senderIban;
        private string _senderBic = "";
        public string SenderPartyName { get; set; }

        public string SenderIban
        {
            get => _senderIban;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Sender IBAN cannot be null or whitespace.", nameof(value));
                if (value.Length < 15)
                    throw new ArgumentException("Sender IBAN is to short", nameof(value));
                _senderIban = value.Replace(" ", "");
            }
        }

        public string SenderBic
        {
            get => _senderBic;
            set
            {
                if (value.Length > 11)
                    throw new ArgumentException("Sender BIC is to long", nameof(value));
                _senderBic = value;
            }
        }
    }
}
