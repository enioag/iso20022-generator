using System;

namespace iso20022_generator.entity
{
    public class Initialization
    {
        private string _senderIban;
        private DateTime _executionDate;
        private string _senderBic = "";

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

        public DateTime ExecutionDate
        {
            get => _executionDate;
            set
            {
                if (value.Date < DateTime.Now.Date)
                    throw new ArgumentException("ExecutionDate cannot be in the past");
                _executionDate = value;
            }
        }

        /// <summary>
        /// This must be a unique string identifier over all sent pain.001 files over the last 90 days.
        /// Your financial institute will use that id for duplicate checking. The id must not be longer than 34 characters
        /// </summary>
        public string UniqueDocumentId { get; set; }
    }
}