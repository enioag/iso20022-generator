namespace iso20022_generator.entity
{
    /// <summary>
    /// Instruction for the creditor agent (InstrForCdtrAgt). Only for payment type X and only after
    /// consultation with the financial institution. Not allowed for domestic (D) and SEPA (S) payments.
    /// </summary>
    public class InstructionForCreditorAgent
    {
        /// <summary>Optional code: CHQB, HOLD, PHOB or TELB</summary>
        public string Code { get; set; }

        /// <summary>Optional free text, max. 140 characters</summary>
        public string Information { get; set; }
    }
}
