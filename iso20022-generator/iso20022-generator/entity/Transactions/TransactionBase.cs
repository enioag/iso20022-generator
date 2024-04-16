using System;
using System.Collections.Generic;
using iso20022_generator.schema;

namespace iso20022_generator.entity.Transactions
{
    public class TransactionBase
    {
        private string _currencyCode;

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

        public string ReferenceIdentification { get; set; }

        public decimal Amount { get; set; }

        [Obsolete("Not used any more", true)]
        public virtual string PaymentType => "";

        public virtual string InstructionForDebtorAgent { get; set; }

        public virtual List<InstructionForCreditorAgent1> InstructionsForCreditorAgent { get; set; }
    }
}
