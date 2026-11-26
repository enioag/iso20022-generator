namespace iso20022_generator.entity
{
    /// <summary>Charge bearer (ChrgBr). For SEPA only SLEV or SHAR are allowed.</summary>
    public enum ChargeBearer
    {
        /// <summary>Borne by debtor (ex OUR)</summary>
        DEBT,
        /// <summary>Borne by creditor (ex BEN)</summary>
        CRED,
        /// <summary>Shared (ex SHA)</summary>
        SHAR,
        /// <summary>Following service level</summary>
        SLEV
    }

    /// <summary>SPS payment types (IG v2.3, chapter 3.15). Cheques (type C) are not supported.</summary>
    public enum PaymentType
    {
        /// <summary>D V1: domestic payment in CHF or EUR to an account in CH/LI</summary>
        Domestic,
        /// <summary>D V2: domestic instant payment (CHF only, LclInstrm INST/ITP)</summary>
        DomesticInstant,
        /// <summary>S: SEPA payment (EUR, service level SEPA)</summary>
        Sepa,
        /// <summary>X: foreign currency domestic (V1) or cross-border (V2)</summary>
        Foreign
    }

    /// <summary>Local instrument codes for instant payments.</summary>
    public enum InstantPaymentMode
    {
        /// <summary>INST: instant payment, rejected if it cannot be executed instantly</summary>
        Instant,
        /// <summary>ITP: instant payment with fallback to a normal payment (only by agreement with the bank)</summary>
        InstantWithFallback
    }
}
