using System;
using System.Reflection;

namespace iso20022_generator.entity
{
    public class Initialization
    {
        /// <summary>SPS IG version implemented by this library (nnnn format, 0203 = IG v2.3 / SPS 2026).</summary>
        public const string SpsIgVersion = "0203";

        /// <summary>
        /// Control sum (GrpHdr/CtrlSum). Ignored if <see cref="AutoCalculateControlSum"/> is true.
        /// </summary>
        public decimal ControlSum { get; set; }

        /// <summary>Calculates the control sum from all transactions. Recommended.</summary>
        public bool AutoCalculateControlSum { get; set; } = true;

        /// <summary>
        /// This must be a unique string identifier over all sent pain.001 files over the last 90 days.
        /// Your financial institute will use that id for duplicate checking. Max. 35 characters of the
        /// restricted reference character set (A-Z a-z 0-9 space ' ( ) + , - . / : ?).
        /// </summary>
        public string UniqueDocumentId { get; set; }

        /// <summary>Software name (GrpHdr/InitgPty/CtctDtls/Othr NAME).</summary>
        public string SoftwareName { get; set; } = "iso20022-generator";

        /// <summary>Software provider (GrpHdr/InitgPty/CtctDtls/Othr PRVD).</summary>
        public string SoftwareProvider { get; set; } = "enio AG";

        /// <summary>Software version (GrpHdr/InitgPty/CtctDtls/Othr VRSN).</summary>
        public string SoftwareVersion { get; set; } =
            typeof(Initialization).GetTypeInfo().Assembly.GetName().Version?.ToString(3);

        [Obsolete("pain.001.001.09 has no contact name any more. Use SoftwareName.")]
        public string ContactDetailsName
        {
            get => SoftwareName;
            set => SoftwareName = value;
        }

        [Obsolete("pain.001.001.09 has no free contact field any more. Use SoftwareVersion.")]
        public string ContactDetailsOther
        {
            get => SoftwareVersion;
            set => SoftwareVersion = value;
        }

        /// <summary>
        /// Generates a UETR (unique end-to-end transaction reference, UUIDv4) for every transaction
        /// where none is set. Not generated for SEPA, because it cannot be forwarded there.
        /// </summary>
        public bool GenerateUetr { get; set; } = true;

        /// <summary>Validates the generated document against the official SIX XML schema
        /// (pain.001.001.09.ch.03.xsd). Strongly recommended.</summary>
        public bool ValidateAgainstSchema { get; set; } = true;

        /// <summary>
        /// euroSIC will be discontinued in November 2027. From then on EUR payments within Switzerland
        /// are payment type X instead of D. Set to true once your bank requires it.
        /// </summary>
        public bool TreatDomesticEurAsForeign { get; set; }
    }
}
