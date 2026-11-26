using System;
using System.Collections.Generic;
using iso20022_generator.entity.Transactions;

namespace iso20022_generator.entity
{
    /// <summary>
    /// B level of a pain.001 message (PmtInf): a group of transactions with the same debit account,
    /// execution date and payment type information. Created by the AddXxxPaymentInfo methods of
    /// <see cref="Pain001Generator"/>.
    /// </summary>
    public class PaymentInformation
    {
        internal PaymentInformation(string id, DateTime requestedExecutionDate)
        {
            Id = id;
            RequestedExecutionDate = requestedExecutionDate.Date;
        }

        /// <summary>PmtInfId, unique within the message.</summary>
        public string Id { get; }

        public DateTime RequestedExecutionDate { get; }

        /// <summary>Emits BtchBookg=true. Batch booking per payment information.</summary>
        public bool BatchBooking { get; set; }

        /// <summary>Service level code, "SEPA" for SEPA payments.</summary>
        public string ServiceLevel { get; internal set; }

        /// <summary>Local instrument code, "INST" or "ITP" for instant payments.</summary>
        public string LocalInstrument { get; internal set; }

        /// <summary>Optional category purpose code (e.g. SALA, PENS, SUPP).</summary>
        public string CategoryPurpose { get; set; }

        /// <summary>Optional charge bearer for all transactions. Must not be set on transaction level at the same time.</summary>
        public ChargeBearer? ChargeBearer { get; set; }

        /// <summary>Optional instruction for the debtor agent on B level (max. 140 characters).
        /// Not allowed for SEPA and instant payments.</summary>
        public string InstructionForDebtorAgent { get; set; }

        /// <summary>Optional debit advice control (DbtrAcct/Tp/Prtry): NOA, SIA, CND or CWD.</summary>
        public string DebitAdvice { get; set; }

        public bool IsSepa => ServiceLevel == "SEPA";

        public bool IsInstant => LocalInstrument == "INST" || LocalInstrument == "ITP";

        internal List<(Receiver Receiver, TransactionBase Transaction)> Transactions { get; } =
            new List<(Receiver, TransactionBase)>();

        public int NumberOfTransactions => Transactions.Count;
    }
}
