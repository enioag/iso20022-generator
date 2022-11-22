using System;

namespace iso20022_generator.entity
{
    public class Initialization
    {
        private string _senderIban;
        private string _senderBic = "";
        private decimal _controlSum;
        private string _contactDetailsName = "iso20022-Generator / enio AG";
        private string _contactDetailsOther = "2.1.0";

        public string SenderPartyName { get; set; }

        public string SenderIban
        {
            get => _senderIban;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Sender IBAN cannot be null or whitespace.", nameof(value));
                if (value.Length < 20)
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

        public decimal ControlSum
        {
            get => _controlSum;
            set
            {
                _controlSum = value;
            }
        }

        public bool AutoCalculateControlSum { get; set; }

        public string ContactDetailsName
        {
            get => _contactDetailsName;
            set
            {
                _contactDetailsName = value;
            }
        }

        public string ContactDetailsOther
        {
            get => _contactDetailsOther;
            set
            {
                _contactDetailsOther = value;
            }
        }

        /// <summary>
        /// This must be a unique string identifier over all sent pain.001 files over the last 90 days.
        /// Your financial institute will use that id for duplicate checking. The id must not be longer than 34 characters
        /// </summary>
        public string UniqueDocumentId { get; set; }
    }
}