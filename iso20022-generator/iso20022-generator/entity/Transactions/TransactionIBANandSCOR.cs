using System;

namespace iso20022_generator.entity.Transactions
{
    public class TransactionIBANandSCOR : TransactionBase
    {
        private string _receiverIban;
        private string _scorNumber;

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

        public string ReceiverBIC { get; set; }

        public override string PaymentType => "";

        public string StructuredCustomerReference
        {
            get => _scorNumber;
            set => _scorNumber = value.Replace(" ", "");
        }
    }
}
