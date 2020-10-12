using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace iso20022_generator.entity.Transactions
{
    public class TransactionES : TransactionBase
    {
        private string _receiverAccount;

        //(Postkontonummer oder Bankkontonummer des jeweiligen Finanzinstituts, im
        //Falle von ESR die ESR-Teilnehmernummer
        public string ReceiverAccount
        {
            get => _receiverAccount;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Account cannot be null or whitespace.", nameof(value));

                _receiverAccount = value;
            }
        }

        public override string PaymentType => "CH02";
    }
}