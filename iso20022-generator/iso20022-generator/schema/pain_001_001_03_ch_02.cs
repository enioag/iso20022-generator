namespace iso20022_generator.schema {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IsNullable=false)]
    public partial class Document {
        
        private CustomerCreditTransferInitiationV03CH cstmrCdtTrfInitnField;
        
        /// <remarks/>
        public CustomerCreditTransferInitiationV03CH CstmrCdtTrfInitn {
            get {
                return this.cstmrCdtTrfInitnField;
            }
            set {
                this.cstmrCdtTrfInitnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CustomerCreditTransferInitiationV03-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CustomerCreditTransferInitiationV03CH {
        
        private GroupHeader32CH grpHdrField;
        
        private PaymentInstructionInformation3CH[] pmtInfField;
        
        /// <remarks/>
        public GroupHeader32CH GrpHdr {
            get {
                return this.grpHdrField;
            }
            set {
                this.grpHdrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PmtInf")]
        public PaymentInstructionInformation3CH[] PmtInf {
            get {
                return this.pmtInfField;
            }
            set {
                this.pmtInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="GroupHeader32-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GroupHeader32CH {
        
        private string msgIdField;
        
        private System.DateTime creDtTmField;
        
        private string nbOfTxsField;
        
        private decimal ctrlSumField;
        
        private bool ctrlSumFieldSpecified;
        
        private PartyIdentification32CH_NameAndId initgPtyField;
        
        private BranchAndFinancialInstitutionIdentification4 fwdgAgtField;
        
        /// <remarks/>
        public string MsgId {
            get {
                return this.msgIdField;
            }
            set {
                this.msgIdField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime CreDtTm {
            get {
                return this.creDtTmField;
            }
            set {
                this.creDtTmField = value;
            }
        }
        
        /// <remarks/>
        public string NbOfTxs {
            get {
                return this.nbOfTxsField;
            }
            set {
                this.nbOfTxsField = value;
            }
        }
        
        /// <remarks/>
        public decimal CtrlSum {
            get {
                return this.ctrlSumField;
            }
            set {
                this.ctrlSumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CtrlSumSpecified {
            get {
                return this.ctrlSumFieldSpecified;
            }
            set {
                this.ctrlSumFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH_NameAndId InitgPty {
            get {
                return this.initgPtyField;
            }
            set {
                this.initgPtyField = value;
            }
        }
        
        /// <remarks/>
        public BranchAndFinancialInstitutionIdentification4 FwdgAgt {
            get {
                return this.fwdgAgtField;
            }
            set {
                this.fwdgAgtField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PartyIdentification32-CH_NameAndId", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PartyIdentification32CH_NameAndId {
        
        private string nmField;
        
        private Party6ChoiceCH idField;
        
        private ContactDetails2CH ctctDtlsField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public Party6ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public ContactDetails2CH CtctDtls {
            get {
                return this.ctctDtlsField;
            }
            set {
                this.ctctDtlsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Party6Choice-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class Party6ChoiceCH {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OrgId", typeof(OrganisationIdentification4CH))]
        [System.Xml.Serialization.XmlElementAttribute("PrvtId", typeof(PersonIdentification5CH))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="OrganisationIdentification4-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class OrganisationIdentification4CH {
        
        private string bICOrBEIField;
        
        private GenericOrganisationIdentification1 othrField;
        
        /// <remarks/>
        public string BICOrBEI {
            get {
                return this.bICOrBEIField;
            }
            set {
                this.bICOrBEIField = value;
            }
        }
        
        /// <remarks/>
        public GenericOrganisationIdentification1 Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GenericOrganisationIdentification1 {
        
        private string idField;
        
        private OrganisationIdentificationSchemeName1Choice schmeNmField;
        
        private string issrField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public OrganisationIdentificationSchemeName1Choice SchmeNm {
            get {
                return this.schmeNmField;
            }
            set {
                this.schmeNmField = value;
            }
        }
        
        /// <remarks/>
        public string Issr {
            get {
                return this.issrField;
            }
            set {
                this.issrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class OrganisationIdentificationSchemeName1Choice {
        
        private string itemField;
        
        private ItemChoiceType itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ContactDetails2 {
        
        private NamePrefix1Code nmPrfxField;
        
        private bool nmPrfxFieldSpecified;
        
        private string nmField;
        
        private string phneNbField;
        
        private string mobNbField;
        
        private string faxNbField;
        
        private string emailAdrField;
        
        private string othrField;
        
        /// <remarks/>
        public NamePrefix1Code NmPrfx {
            get {
                return this.nmPrfxField;
            }
            set {
                this.nmPrfxField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NmPrfxSpecified {
            get {
                return this.nmPrfxFieldSpecified;
            }
            set {
                this.nmPrfxFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public string PhneNb {
            get {
                return this.phneNbField;
            }
            set {
                this.phneNbField = value;
            }
        }
        
        /// <remarks/>
        public string MobNb {
            get {
                return this.mobNbField;
            }
            set {
                this.mobNbField = value;
            }
        }
        
        /// <remarks/>
        public string FaxNb {
            get {
                return this.faxNbField;
            }
            set {
                this.faxNbField = value;
            }
        }
        
        /// <remarks/>
        public string EmailAdr {
            get {
                return this.emailAdrField;
            }
            set {
                this.emailAdrField = value;
            }
        }
        
        /// <remarks/>
        public string Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum NamePrefix1Code {
        
        /// <remarks/>
        DOCT,
        
        /// <remarks/>
        MIST,
        
        /// <remarks/>
        MISS,
        
        /// <remarks/>
        MADM,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PersonIdentification5 {
        
        private DateAndPlaceOfBirth dtAndPlcOfBirthField;
        
        private GenericPersonIdentification1[] othrField;
        
        /// <remarks/>
        public DateAndPlaceOfBirth DtAndPlcOfBirth {
            get {
                return this.dtAndPlcOfBirthField;
            }
            set {
                this.dtAndPlcOfBirthField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Othr")]
        public GenericPersonIdentification1[] Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class DateAndPlaceOfBirth {
        
        private System.DateTime birthDtField;
        
        private string prvcOfBirthField;
        
        private string cityOfBirthField;
        
        private string ctryOfBirthField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime BirthDt {
            get {
                return this.birthDtField;
            }
            set {
                this.birthDtField = value;
            }
        }
        
        /// <remarks/>
        public string PrvcOfBirth {
            get {
                return this.prvcOfBirthField;
            }
            set {
                this.prvcOfBirthField = value;
            }
        }
        
        /// <remarks/>
        public string CityOfBirth {
            get {
                return this.cityOfBirthField;
            }
            set {
                this.cityOfBirthField = value;
            }
        }
        
        /// <remarks/>
        public string CtryOfBirth {
            get {
                return this.ctryOfBirthField;
            }
            set {
                this.ctryOfBirthField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GenericPersonIdentification1 {
        
        private string idField;
        
        private PersonIdentificationSchemeName1Choice schmeNmField;
        
        private string issrField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public PersonIdentificationSchemeName1Choice SchmeNm {
            get {
                return this.schmeNmField;
            }
            set {
                this.schmeNmField = value;
            }
        }
        
        /// <remarks/>
        public string Issr {
            get {
                return this.issrField;
            }
            set {
                this.issrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PersonIdentificationSchemeName1Choice {
        
        private string itemField;
        
        private ItemChoiceType1 itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType1 {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class OrganisationIdentification4 {
        
        private string bICOrBEIField;
        
        private GenericOrganisationIdentification1[] othrField;
        
        /// <remarks/>
        public string BICOrBEI {
            get {
                return this.bICOrBEIField;
            }
            set {
                this.bICOrBEIField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Othr")]
        public GenericOrganisationIdentification1[] Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class Party6Choice {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OrgId", typeof(OrganisationIdentification4))]
        [System.Xml.Serialization.XmlElementAttribute("PrvtId", typeof(PersonIdentification5))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PartyIdentification32 {
        
        private string nmField;
        
        private PostalAddress6 pstlAdrField;
        
        private Party6Choice idField;
        
        private string ctryOfResField;
        
        private ContactDetails2 ctctDtlsField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6 PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
        
        /// <remarks/>
        public Party6Choice Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string CtryOfRes {
            get {
                return this.ctryOfResField;
            }
            set {
                this.ctryOfResField = value;
            }
        }
        
        /// <remarks/>
        public ContactDetails2 CtctDtls {
            get {
                return this.ctctDtlsField;
            }
            set {
                this.ctctDtlsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PostalAddress6 {
        
        private AddressType2Code adrTpField;
        
        private bool adrTpFieldSpecified;
        
        private string deptField;
        
        private string subDeptField;
        
        private string strtNmField;
        
        private string bldgNbField;
        
        private string pstCdField;
        
        private string twnNmField;
        
        private string ctrySubDvsnField;
        
        private string ctryField;
        
        private string[] adrLineField;
        
        /// <remarks/>
        public AddressType2Code AdrTp {
            get {
                return this.adrTpField;
            }
            set {
                this.adrTpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AdrTpSpecified {
            get {
                return this.adrTpFieldSpecified;
            }
            set {
                this.adrTpFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Dept {
            get {
                return this.deptField;
            }
            set {
                this.deptField = value;
            }
        }
        
        /// <remarks/>
        public string SubDept {
            get {
                return this.subDeptField;
            }
            set {
                this.subDeptField = value;
            }
        }
        
        /// <remarks/>
        public string StrtNm {
            get {
                return this.strtNmField;
            }
            set {
                this.strtNmField = value;
            }
        }
        
        /// <remarks/>
        public string BldgNb {
            get {
                return this.bldgNbField;
            }
            set {
                this.bldgNbField = value;
            }
        }
        
        /// <remarks/>
        public string PstCd {
            get {
                return this.pstCdField;
            }
            set {
                this.pstCdField = value;
            }
        }
        
        /// <remarks/>
        public string TwnNm {
            get {
                return this.twnNmField;
            }
            set {
                this.twnNmField = value;
            }
        }
        
        /// <remarks/>
        public string CtrySubDvsn {
            get {
                return this.ctrySubDvsnField;
            }
            set {
                this.ctrySubDvsnField = value;
            }
        }
        
        /// <remarks/>
        public string Ctry {
            get {
                return this.ctryField;
            }
            set {
                this.ctryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdrLine")]
        public string[] AdrLine {
            get {
                return this.adrLineField;
            }
            set {
                this.adrLineField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum AddressType2Code {
        
        /// <remarks/>
        ADDR,
        
        /// <remarks/>
        PBOX,
        
        /// <remarks/>
        HOME,
        
        /// <remarks/>
        BIZZ,
        
        /// <remarks/>
        MLTO,
        
        /// <remarks/>
        DLVY,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CreditorReferenceType1Choice {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(DocumentType3Code))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum DocumentType3Code {
        
        /// <remarks/>
        RADM,
        
        /// <remarks/>
        RPIN,
        
        /// <remarks/>
        FXDR,
        
        /// <remarks/>
        DISP,
        
        /// <remarks/>
        PUOR,
        
        /// <remarks/>
        SCOR,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CreditorReferenceType2 {
        
        private CreditorReferenceType1Choice cdOrPrtryField;
        
        private string issrField;
        
        /// <remarks/>
        public CreditorReferenceType1Choice CdOrPrtry {
            get {
                return this.cdOrPrtryField;
            }
            set {
                this.cdOrPrtryField = value;
            }
        }
        
        /// <remarks/>
        public string Issr {
            get {
                return this.issrField;
            }
            set {
                this.issrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CreditorReferenceInformation2 {
        
        private CreditorReferenceType2 tpField;
        
        private string refField;
        
        /// <remarks/>
        public CreditorReferenceType2 Tp {
            get {
                return this.tpField;
            }
            set {
                this.tpField = value;
            }
        }
        
        /// <remarks/>
        public string Ref {
            get {
                return this.refField;
            }
            set {
                this.refField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class DocumentAdjustment1 {
        
        private ActiveOrHistoricCurrencyAndAmount amtField;
        
        private CreditDebitCode cdtDbtIndField;
        
        private bool cdtDbtIndFieldSpecified;
        
        private string rsnField;
        
        private string addtlInfField;
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount Amt {
            get {
                return this.amtField;
            }
            set {
                this.amtField = value;
            }
        }
        
        /// <remarks/>
        public CreditDebitCode CdtDbtInd {
            get {
                return this.cdtDbtIndField;
            }
            set {
                this.cdtDbtIndField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CdtDbtIndSpecified {
            get {
                return this.cdtDbtIndFieldSpecified;
            }
            set {
                this.cdtDbtIndFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Rsn {
            get {
                return this.rsnField;
            }
            set {
                this.rsnField = value;
            }
        }
        
        /// <remarks/>
        public string AddtlInf {
            get {
                return this.addtlInfField;
            }
            set {
                this.addtlInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ActiveOrHistoricCurrencyAndAmount {
        
        private string ccyField;
        
        private decimal valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Ccy {
            get {
                return this.ccyField;
            }
            set {
                this.ccyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum CreditDebitCode {
        
        /// <remarks/>
        CRDT,
        
        /// <remarks/>
        DBIT,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class RemittanceAmount1 {
        
        private ActiveOrHistoricCurrencyAndAmount duePyblAmtField;
        
        private ActiveOrHistoricCurrencyAndAmount dscntApldAmtField;
        
        private ActiveOrHistoricCurrencyAndAmount cdtNoteAmtField;
        
        private ActiveOrHistoricCurrencyAndAmount taxAmtField;
        
        private DocumentAdjustment1[] adjstmntAmtAndRsnField;
        
        private ActiveOrHistoricCurrencyAndAmount rmtdAmtField;
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount DuePyblAmt {
            get {
                return this.duePyblAmtField;
            }
            set {
                this.duePyblAmtField = value;
            }
        }
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount DscntApldAmt {
            get {
                return this.dscntApldAmtField;
            }
            set {
                this.dscntApldAmtField = value;
            }
        }
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount CdtNoteAmt {
            get {
                return this.cdtNoteAmtField;
            }
            set {
                this.cdtNoteAmtField = value;
            }
        }
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount TaxAmt {
            get {
                return this.taxAmtField;
            }
            set {
                this.taxAmtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdjstmntAmtAndRsn")]
        public DocumentAdjustment1[] AdjstmntAmtAndRsn {
            get {
                return this.adjstmntAmtAndRsnField;
            }
            set {
                this.adjstmntAmtAndRsnField = value;
            }
        }
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount RmtdAmt {
            get {
                return this.rmtdAmtField;
            }
            set {
                this.rmtdAmtField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ReferredDocumentType1Choice {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(DocumentType5Code))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum DocumentType5Code {
        
        /// <remarks/>
        MSIN,
        
        /// <remarks/>
        CNFA,
        
        /// <remarks/>
        DNFA,
        
        /// <remarks/>
        CINV,
        
        /// <remarks/>
        CREN,
        
        /// <remarks/>
        DEBN,
        
        /// <remarks/>
        HIRI,
        
        /// <remarks/>
        SBIN,
        
        /// <remarks/>
        CMCN,
        
        /// <remarks/>
        SOAC,
        
        /// <remarks/>
        DISP,
        
        /// <remarks/>
        BOLD,
        
        /// <remarks/>
        VCHR,
        
        /// <remarks/>
        AROI,
        
        /// <remarks/>
        TSUT,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ReferredDocumentType2 {
        
        private ReferredDocumentType1Choice cdOrPrtryField;
        
        private string issrField;
        
        /// <remarks/>
        public ReferredDocumentType1Choice CdOrPrtry {
            get {
                return this.cdOrPrtryField;
            }
            set {
                this.cdOrPrtryField = value;
            }
        }
        
        /// <remarks/>
        public string Issr {
            get {
                return this.issrField;
            }
            set {
                this.issrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ReferredDocumentInformation3 {
        
        private ReferredDocumentType2 tpField;
        
        private string nbField;
        
        private System.DateTime rltdDtField;
        
        private bool rltdDtFieldSpecified;
        
        /// <remarks/>
        public ReferredDocumentType2 Tp {
            get {
                return this.tpField;
            }
            set {
                this.tpField = value;
            }
        }
        
        /// <remarks/>
        public string Nb {
            get {
                return this.nbField;
            }
            set {
                this.nbField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime RltdDt {
            get {
                return this.rltdDtField;
            }
            set {
                this.rltdDtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RltdDtSpecified {
            get {
                return this.rltdDtFieldSpecified;
            }
            set {
                this.rltdDtFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class StructuredRemittanceInformation7 {
        
        private ReferredDocumentInformation3[] rfrdDocInfField;
        
        private RemittanceAmount1 rfrdDocAmtField;
        
        private CreditorReferenceInformation2 cdtrRefInfField;
        
        private PartyIdentification32 invcrField;
        
        private PartyIdentification32 invceeField;
        
        private string[] addtlRmtInfField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RfrdDocInf")]
        public ReferredDocumentInformation3[] RfrdDocInf {
            get {
                return this.rfrdDocInfField;
            }
            set {
                this.rfrdDocInfField = value;
            }
        }
        
        /// <remarks/>
        public RemittanceAmount1 RfrdDocAmt {
            get {
                return this.rfrdDocAmtField;
            }
            set {
                this.rfrdDocAmtField = value;
            }
        }
        
        /// <remarks/>
        public CreditorReferenceInformation2 CdtrRefInf {
            get {
                return this.cdtrRefInfField;
            }
            set {
                this.cdtrRefInfField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32 Invcr {
            get {
                return this.invcrField;
            }
            set {
                this.invcrField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32 Invcee {
            get {
                return this.invceeField;
            }
            set {
                this.invceeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AddtlRmtInf")]
        public string[] AddtlRmtInf {
            get {
                return this.addtlRmtInfField;
            }
            set {
                this.addtlRmtInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="RemittanceInformation5-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class RemittanceInformation5CH {
        
        private string ustrdField;
        
        private StructuredRemittanceInformation7 strdField;
        
        /// <remarks/>
        public string Ustrd {
            get {
                return this.ustrdField;
            }
            set {
                this.ustrdField = value;
            }
        }
        
        /// <remarks/>
        public StructuredRemittanceInformation7 Strd {
            get {
                return this.strdField;
            }
            set {
                this.strdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class StructuredRegulatoryReporting3 {
        
        private string tpField;
        
        private System.DateTime dtField;
        
        private bool dtFieldSpecified;
        
        private string ctryField;
        
        private string cdField;
        
        private ActiveOrHistoricCurrencyAndAmount amtField;
        
        private string[] infField;
        
        /// <remarks/>
        public string Tp {
            get {
                return this.tpField;
            }
            set {
                this.tpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime Dt {
            get {
                return this.dtField;
            }
            set {
                this.dtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DtSpecified {
            get {
                return this.dtFieldSpecified;
            }
            set {
                this.dtFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Ctry {
            get {
                return this.ctryField;
            }
            set {
                this.ctryField = value;
            }
        }
        
        /// <remarks/>
        public string Cd {
            get {
                return this.cdField;
            }
            set {
                this.cdField = value;
            }
        }
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount Amt {
            get {
                return this.amtField;
            }
            set {
                this.amtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Inf")]
        public string[] Inf {
            get {
                return this.infField;
            }
            set {
                this.infField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class RegulatoryAuthority2 {
        
        private string nmField;
        
        private string ctryField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public string Ctry {
            get {
                return this.ctryField;
            }
            set {
                this.ctryField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class RegulatoryReporting3 {
        
        private RegulatoryReportingType1Code dbtCdtRptgIndField;
        
        private bool dbtCdtRptgIndFieldSpecified;
        
        private RegulatoryAuthority2 authrtyField;
        
        private StructuredRegulatoryReporting3[] dtlsField;
        
        /// <remarks/>
        public RegulatoryReportingType1Code DbtCdtRptgInd {
            get {
                return this.dbtCdtRptgIndField;
            }
            set {
                this.dbtCdtRptgIndField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DbtCdtRptgIndSpecified {
            get {
                return this.dbtCdtRptgIndFieldSpecified;
            }
            set {
                this.dbtCdtRptgIndFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public RegulatoryAuthority2 Authrty {
            get {
                return this.authrtyField;
            }
            set {
                this.authrtyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Dtls")]
        public StructuredRegulatoryReporting3[] Dtls {
            get {
                return this.dtlsField;
            }
            set {
                this.dtlsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum RegulatoryReportingType1Code {
        
        /// <remarks/>
        CRED,
        
        /// <remarks/>
        DEBT,
        
        /// <remarks/>
        BOTH,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Purpose2-CH_Code", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class Purpose2CH_Code {
        
        private string cdField;
        
        /// <remarks/>
        public string Cd {
            get {
                return this.cdField;
            }
            set {
                this.cdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class InstructionForCreditorAgent1 {
        
        private Instruction3Code cdField;
        
        private bool cdFieldSpecified;
        
        private string instrInfField;
        
        /// <remarks/>
        public Instruction3Code Cd {
            get {
                return this.cdField;
            }
            set {
                this.cdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CdSpecified {
            get {
                return this.cdFieldSpecified;
            }
            set {
                this.cdFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string InstrInf {
            get {
                return this.instrInfField;
            }
            set {
                this.instrInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum Instruction3Code {
        
        /// <remarks/>
        CHQB,
        
        /// <remarks/>
        HOLD,
        
        /// <remarks/>
        PHOB,
        
        /// <remarks/>
        TELB,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CashAccount16-CH_Id", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CashAccount16CH_Id {
        
        private AccountIdentification4ChoiceCH idField;
        
        /// <remarks/>
        public AccountIdentification4ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="AccountIdentification4Choice-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class AccountIdentification4ChoiceCH {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IBAN", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Othr", typeof(GenericAccountIdentification1CH))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="GenericAccountIdentification1-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GenericAccountIdentification1CH {
        
        private string idField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PartyIdentification32-CH_Name", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PartyIdentification32CH_Name {
        
        private string nmField;
        
        private PostalAddress6CH pstlAdrField;
        
        private Party6ChoiceCH idField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6CH PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
        
        /// <remarks/>
        public Party6ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PostalAddress6-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PostalAddress6CH {
        
        private AddressType2Code adrTpField;
        
        private bool adrTpFieldSpecified;
        
        private string deptField;
        
        private string subDeptField;
        
        private string strtNmField;
        
        private string bldgNbField;
        
        private string pstCdField;
        
        private string twnNmField;
        
        private string ctrySubDvsnField;
        
        private string ctryField;
        
        private string[] adrLineField;
        
        /// <remarks/>
        public AddressType2Code AdrTp {
            get {
                return this.adrTpField;
            }
            set {
                this.adrTpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AdrTpSpecified {
            get {
                return this.adrTpFieldSpecified;
            }
            set {
                this.adrTpFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string Dept {
            get {
                return this.deptField;
            }
            set {
                this.deptField = value;
            }
        }
        
        /// <remarks/>
        public string SubDept {
            get {
                return this.subDeptField;
            }
            set {
                this.subDeptField = value;
            }
        }
        
        /// <remarks/>
        public string StrtNm {
            get {
                return this.strtNmField;
            }
            set {
                this.strtNmField = value;
            }
        }
        
        /// <remarks/>
        public string BldgNb {
            get {
                return this.bldgNbField;
            }
            set {
                this.bldgNbField = value;
            }
        }
        
        /// <remarks/>
        public string PstCd {
            get {
                return this.pstCdField;
            }
            set {
                this.pstCdField = value;
            }
        }
        
        /// <remarks/>
        public string TwnNm {
            get {
                return this.twnNmField;
            }
            set {
                this.twnNmField = value;
            }
        }
        
        /// <remarks/>
        public string CtrySubDvsn {
            get {
                return this.ctrySubDvsnField;
            }
            set {
                this.ctrySubDvsnField = value;
            }
        }
        
        /// <remarks/>
        public string Ctry {
            get {
                return this.ctryField;
            }
            set {
                this.ctryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AdrLine")]
        public string[] AdrLine {
            get {
                return this.adrLineField;
            }
            set {
                this.adrLineField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="GenericFinancialIdentification1-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GenericFinancialIdentification1CH {
        
        private string idField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="FinancialInstitutionIdentification7-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class FinancialInstitutionIdentification7CH {
        
        private string bICField;
        
        private ClearingSystemMemberIdentification2 clrSysMmbIdField;
        
        private string nmField;
        
        private PostalAddress6CH pstlAdrField;
        
        private GenericFinancialIdentification1CH othrField;
        
        /// <remarks/>
        public string BIC {
            get {
                return this.bICField;
            }
            set {
                this.bICField = value;
            }
        }
        
        /// <remarks/>
        public ClearingSystemMemberIdentification2 ClrSysMmbId {
            get {
                return this.clrSysMmbIdField;
            }
            set {
                this.clrSysMmbIdField = value;
            }
        }
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6CH PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
        
        /// <remarks/>
        public GenericFinancialIdentification1CH Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ClearingSystemMemberIdentification2 {
        
        private ClearingSystemIdentification2Choice clrSysIdField;
        
        private string mmbIdField;
        
        /// <remarks/>
        public ClearingSystemIdentification2Choice ClrSysId {
            get {
                return this.clrSysIdField;
            }
            set {
                this.clrSysIdField = value;
            }
        }
        
        /// <remarks/>
        public string MmbId {
            get {
                return this.mmbIdField;
            }
            set {
                this.mmbIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ClearingSystemIdentification2Choice {
        
        private string itemField;
        
        private ItemChoiceType2 itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType2 ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType2 {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="BranchAndFinancialInstitutionIdentification4-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class BranchAndFinancialInstitutionIdentification4CH {
        
        private FinancialInstitutionIdentification7CH finInstnIdField;
        
        /// <remarks/>
        public FinancialInstitutionIdentification7CH FinInstnId {
            get {
                return this.finInstnIdField;
            }
            set {
                this.finInstnIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ChequeDeliveryMethod1Choice {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(ChequeDelivery1Code))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum ChequeDelivery1Code {
        
        /// <remarks/>
        MLDB,
        
        /// <remarks/>
        MLCD,
        
        /// <remarks/>
        MLFA,
        
        /// <remarks/>
        CRDB,
        
        /// <remarks/>
        CRCD,
        
        /// <remarks/>
        CRFA,
        
        /// <remarks/>
        PUDB,
        
        /// <remarks/>
        PUCD,
        
        /// <remarks/>
        PUFA,
        
        /// <remarks/>
        RGDB,
        
        /// <remarks/>
        RGCD,
        
        /// <remarks/>
        RGFA,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="Cheque6-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class Cheque6CH {
        
        private ChequeType2Code chqTpField;
        
        private bool chqTpFieldSpecified;
        
        private ChequeDeliveryMethod1Choice dlvryMtdField;
        
        /// <remarks/>
        public ChequeType2Code ChqTp {
            get {
                return this.chqTpField;
            }
            set {
                this.chqTpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChqTpSpecified {
            get {
                return this.chqTpFieldSpecified;
            }
            set {
                this.chqTpFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public ChequeDeliveryMethod1Choice DlvryMtd {
            get {
                return this.dlvryMtdField;
            }
            set {
                this.dlvryMtdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum ChequeType2Code {
        
        /// <remarks/>
        CCHQ,
        
        /// <remarks/>
        CCCH,
        
        /// <remarks/>
        BCHQ,
        
        /// <remarks/>
        DRFT,
        
        /// <remarks/>
        ELDR,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ExchangeRateInformation1 {
        
        private decimal xchgRateField;
        
        private bool xchgRateFieldSpecified;
        
        private ExchangeRateType1Code rateTpField;
        
        private bool rateTpFieldSpecified;
        
        private string ctrctIdField;
        
        /// <remarks/>
        public decimal XchgRate {
            get {
                return this.xchgRateField;
            }
            set {
                this.xchgRateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool XchgRateSpecified {
            get {
                return this.xchgRateFieldSpecified;
            }
            set {
                this.xchgRateFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public ExchangeRateType1Code RateTp {
            get {
                return this.rateTpField;
            }
            set {
                this.rateTpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RateTpSpecified {
            get {
                return this.rateTpFieldSpecified;
            }
            set {
                this.rateTpFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string CtrctId {
            get {
                return this.ctrctIdField;
            }
            set {
                this.ctrctIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum ExchangeRateType1Code {
        
        /// <remarks/>
        SPOT,
        
        /// <remarks/>
        SALE,
        
        /// <remarks/>
        AGRD,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class EquivalentAmount2 {
        
        private ActiveOrHistoricCurrencyAndAmount amtField;
        
        private string ccyOfTrfField;
        
        /// <remarks/>
        public ActiveOrHistoricCurrencyAndAmount Amt {
            get {
                return this.amtField;
            }
            set {
                this.amtField = value;
            }
        }
        
        /// <remarks/>
        public string CcyOfTrf {
            get {
                return this.ccyOfTrfField;
            }
            set {
                this.ccyOfTrfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class AmountType3Choice {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EqvtAmt", typeof(EquivalentAmount2))]
        [System.Xml.Serialization.XmlElementAttribute("InstdAmt", typeof(ActiveOrHistoricCurrencyAndAmount))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PaymentIdentification1 {
        
        private string instrIdField;
        
        private string endToEndIdField;
        
        /// <remarks/>
        public string InstrId {
            get {
                return this.instrIdField;
            }
            set {
                this.instrIdField = value;
            }
        }
        
        /// <remarks/>
        public string EndToEndId {
            get {
                return this.endToEndIdField;
            }
            set {
                this.endToEndIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CreditTransferTransactionInformation10-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CreditTransferTransactionInformation10CH {
        
        private PaymentIdentification1 pmtIdField;
        
        private PaymentTypeInformation19CH pmtTpInfField;
        
        private AmountType3Choice amtField;
        
        private ExchangeRateInformation1 xchgRateInfField;
        
        private ChargeBearerType1Code chrgBrField;
        
        private bool chrgBrFieldSpecified;
        
        private Cheque6CH chqInstrField;
        
        private PartyIdentification32CH ultmtDbtrField;
        
        private BranchAndFinancialInstitutionIdentification4CH intrmyAgt1Field;
        
        private BranchAndFinancialInstitutionIdentification4CH cdtrAgtField;
        
        private PartyIdentification32CH_Name cdtrField;
        
        private CashAccount16CH_Id cdtrAcctField;
        
        private PartyIdentification32CH_Name ultmtCdtrField;
        
        private InstructionForCreditorAgent1[] instrForCdtrAgtField;
        
        private string instrForDbtrAgtField;
        
        private Purpose2CH_Code purpField;
        
        private RegulatoryReporting3[] rgltryRptgField;
        
        private RemittanceInformation5CH rmtInfField;
        
        /// <remarks/>
        public PaymentIdentification1 PmtId {
            get {
                return this.pmtIdField;
            }
            set {
                this.pmtIdField = value;
            }
        }
        
        /// <remarks/>
        public PaymentTypeInformation19CH PmtTpInf {
            get {
                return this.pmtTpInfField;
            }
            set {
                this.pmtTpInfField = value;
            }
        }
        
        /// <remarks/>
        public AmountType3Choice Amt {
            get {
                return this.amtField;
            }
            set {
                this.amtField = value;
            }
        }
        
        /// <remarks/>
        public ExchangeRateInformation1 XchgRateInf {
            get {
                return this.xchgRateInfField;
            }
            set {
                this.xchgRateInfField = value;
            }
        }
        
        /// <remarks/>
        public ChargeBearerType1Code ChrgBr {
            get {
                return this.chrgBrField;
            }
            set {
                this.chrgBrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChrgBrSpecified {
            get {
                return this.chrgBrFieldSpecified;
            }
            set {
                this.chrgBrFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public Cheque6CH ChqInstr {
            get {
                return this.chqInstrField;
            }
            set {
                this.chqInstrField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH UltmtDbtr {
            get {
                return this.ultmtDbtrField;
            }
            set {
                this.ultmtDbtrField = value;
            }
        }
        
        /// <remarks/>
        public BranchAndFinancialInstitutionIdentification4CH IntrmyAgt1 {
            get {
                return this.intrmyAgt1Field;
            }
            set {
                this.intrmyAgt1Field = value;
            }
        }
        
        /// <remarks/>
        public BranchAndFinancialInstitutionIdentification4CH CdtrAgt {
            get {
                return this.cdtrAgtField;
            }
            set {
                this.cdtrAgtField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH_Name Cdtr {
            get {
                return this.cdtrField;
            }
            set {
                this.cdtrField = value;
            }
        }
        
        /// <remarks/>
        public CashAccount16CH_Id CdtrAcct {
            get {
                return this.cdtrAcctField;
            }
            set {
                this.cdtrAcctField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH_Name UltmtCdtr {
            get {
                return this.ultmtCdtrField;
            }
            set {
                this.ultmtCdtrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InstrForCdtrAgt")]
        public InstructionForCreditorAgent1[] InstrForCdtrAgt {
            get {
                return this.instrForCdtrAgtField;
            }
            set {
                this.instrForCdtrAgtField = value;
            }
        }
        
        /// <remarks/>
        public string InstrForDbtrAgt {
            get {
                return this.instrForDbtrAgtField;
            }
            set {
                this.instrForDbtrAgtField = value;
            }
        }
        
        /// <remarks/>
        public Purpose2CH_Code Purp {
            get {
                return this.purpField;
            }
            set {
                this.purpField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RgltryRptg")]
        public RegulatoryReporting3[] RgltryRptg {
            get {
                return this.rgltryRptgField;
            }
            set {
                this.rgltryRptgField = value;
            }
        }
        
        /// <remarks/>
        public RemittanceInformation5CH RmtInf {
            get {
                return this.rmtInfField;
            }
            set {
                this.rmtInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PaymentTypeInformation19-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PaymentTypeInformation19CH {
        
        private Priority2Code instrPrtyField;
        
        private bool instrPrtyFieldSpecified;
        
        private ServiceLevel8Choice svcLvlField;
        
        private LocalInstrument2Choice lclInstrmField;
        
        private CategoryPurpose1CH_Code ctgyPurpField;
        
        /// <remarks/>
        public Priority2Code InstrPrty {
            get {
                return this.instrPrtyField;
            }
            set {
                this.instrPrtyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InstrPrtySpecified {
            get {
                return this.instrPrtyFieldSpecified;
            }
            set {
                this.instrPrtyFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public ServiceLevel8Choice SvcLvl {
            get {
                return this.svcLvlField;
            }
            set {
                this.svcLvlField = value;
            }
        }
        
        /// <remarks/>
        public LocalInstrument2Choice LclInstrm {
            get {
                return this.lclInstrmField;
            }
            set {
                this.lclInstrmField = value;
            }
        }
        
        /// <remarks/>
        public CategoryPurpose1CH_Code CtgyPurp {
            get {
                return this.ctgyPurpField;
            }
            set {
                this.ctgyPurpField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum Priority2Code {
        
        /// <remarks/>
        HIGH,
        
        /// <remarks/>
        NORM,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ServiceLevel8Choice {
        
        private string itemField;
        
        private ItemChoiceType4 itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType4 ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType4 {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class LocalInstrument2Choice {
        
        private string itemField;
        
        private ItemChoiceType5 itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType5 ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType5 {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CategoryPurpose1-CH_Code", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CategoryPurpose1CH_Code {
        
        private string cdField;
        
        /// <remarks/>
        public string Cd {
            get {
                return this.cdField;
            }
            set {
                this.cdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum ChargeBearerType1Code {
        
        /// <remarks/>
        DEBT,
        
        /// <remarks/>
        CRED,
        
        /// <remarks/>
        SHAR,
        
        /// <remarks/>
        SLEV,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PartyIdentification32-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PartyIdentification32CH {
        
        private string nmField;
        
        private PostalAddress6CH pstlAdrField;
        
        private Party6ChoiceCH idField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6CH PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
        
        /// <remarks/>
        public Party6ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CashAccount16-CH_IdAndCurrency", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CashAccount16CH_IdAndCurrency {
        
        private AccountIdentification4ChoiceCH idField;
        
        private string ccyField;
        
        /// <remarks/>
        public AccountIdentification4ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Ccy {
            get {
                return this.ccyField;
            }
            set {
                this.ccyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="FinancialInstitutionIdentification7-CH_BicOrClrId", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class FinancialInstitutionIdentification7CH_BicOrClrId {
        
        private string bICField;
        
        private ClearingSystemMemberIdentification2 clrSysMmbIdField;
        
        /// <remarks/>
        public string BIC {
            get {
                return this.bICField;
            }
            set {
                this.bICField = value;
            }
        }
        
        /// <remarks/>
        public ClearingSystemMemberIdentification2 ClrSysMmbId {
            get {
                return this.clrSysMmbIdField;
            }
            set {
                this.clrSysMmbIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="BranchAndFinancialInstitutionIdentification4-CH_BicOrClrId", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class BranchAndFinancialInstitutionIdentification4CH_BicOrClrId {
        
        private FinancialInstitutionIdentification7CH_BicOrClrId finInstnIdField;
        
        /// <remarks/>
        public FinancialInstitutionIdentification7CH_BicOrClrId FinInstnId {
            get {
                return this.finInstnIdField;
            }
            set {
                this.finInstnIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CashAccountType2 {
        
        private object itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(CashAccountType4Code))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum CashAccountType4Code {
        
        /// <remarks/>
        CASH,
        
        /// <remarks/>
        CHAR,
        
        /// <remarks/>
        COMM,
        
        /// <remarks/>
        TAXE,
        
        /// <remarks/>
        CISH,
        
        /// <remarks/>
        TRAS,
        
        /// <remarks/>
        SACC,
        
        /// <remarks/>
        CACC,
        
        /// <remarks/>
        SVGS,
        
        /// <remarks/>
        ONDP,
        
        /// <remarks/>
        MGLD,
        
        /// <remarks/>
        NREX,
        
        /// <remarks/>
        MOMA,
        
        /// <remarks/>
        LOAN,
        
        /// <remarks/>
        SLRY,
        
        /// <remarks/>
        ODFT,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="CashAccount16-CH_IdTpCcy", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class CashAccount16CH_IdTpCcy {
        
        private AccountIdentification4ChoiceCH idField;
        
        private CashAccountType2 tpField;
        
        private string ccyField;
        
        /// <remarks/>
        public AccountIdentification4ChoiceCH Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public CashAccountType2 Tp {
            get {
                return this.tpField;
            }
            set {
                this.tpField = value;
            }
        }
        
        /// <remarks/>
        public string Ccy {
            get {
                return this.ccyField;
            }
            set {
                this.ccyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PaymentInstructionInformation3-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PaymentInstructionInformation3CH {
        
        private string pmtInfIdField;
        
        private PaymentMethod3Code pmtMtdField;
        
        private bool btchBookgField;
        
        private bool btchBookgFieldSpecified;
        
        private string nbOfTxsField;
        
        private decimal ctrlSumField;
        
        private bool ctrlSumFieldSpecified;
        
        private PaymentTypeInformation19CH pmtTpInfField;
        
        private System.DateTime reqdExctnDtField;
        
        private PartyIdentification32CH dbtrField;
        
        private CashAccount16CH_IdTpCcy dbtrAcctField;
        
        private BranchAndFinancialInstitutionIdentification4CH_BicOrClrId dbtrAgtField;
        
        private PartyIdentification32CH ultmtDbtrField;
        
        private ChargeBearerType1Code chrgBrField;
        
        private bool chrgBrFieldSpecified;
        
        private CashAccount16CH_IdAndCurrency chrgsAcctField;
        
        private CreditTransferTransactionInformation10CH[] cdtTrfTxInfField;
        
        /// <remarks/>
        public string PmtInfId {
            get {
                return this.pmtInfIdField;
            }
            set {
                this.pmtInfIdField = value;
            }
        }
        
        /// <remarks/>
        public PaymentMethod3Code PmtMtd {
            get {
                return this.pmtMtdField;
            }
            set {
                this.pmtMtdField = value;
            }
        }
        
        /// <remarks/>
        public bool BtchBookg {
            get {
                return this.btchBookgField;
            }
            set {
                this.btchBookgField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BtchBookgSpecified {
            get {
                return this.btchBookgFieldSpecified;
            }
            set {
                this.btchBookgFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string NbOfTxs {
            get {
                return this.nbOfTxsField;
            }
            set {
                this.nbOfTxsField = value;
            }
        }
        
        /// <remarks/>
        public decimal CtrlSum {
            get {
                return this.ctrlSumField;
            }
            set {
                this.ctrlSumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CtrlSumSpecified {
            get {
                return this.ctrlSumFieldSpecified;
            }
            set {
                this.ctrlSumFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public PaymentTypeInformation19CH PmtTpInf {
            get {
                return this.pmtTpInfField;
            }
            set {
                this.pmtTpInfField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="date")]
        public System.DateTime ReqdExctnDt {
            get {
                return this.reqdExctnDtField;
            }
            set {
                this.reqdExctnDtField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH Dbtr {
            get {
                return this.dbtrField;
            }
            set {
                this.dbtrField = value;
            }
        }
        
        /// <remarks/>
        public CashAccount16CH_IdTpCcy DbtrAcct {
            get {
                return this.dbtrAcctField;
            }
            set {
                this.dbtrAcctField = value;
            }
        }
        
        /// <remarks/>
        public BranchAndFinancialInstitutionIdentification4CH_BicOrClrId DbtrAgt {
            get {
                return this.dbtrAgtField;
            }
            set {
                this.dbtrAgtField = value;
            }
        }
        
        /// <remarks/>
        public PartyIdentification32CH UltmtDbtr {
            get {
                return this.ultmtDbtrField;
            }
            set {
                this.ultmtDbtrField = value;
            }
        }
        
        /// <remarks/>
        public ChargeBearerType1Code ChrgBr {
            get {
                return this.chrgBrField;
            }
            set {
                this.chrgBrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ChrgBrSpecified {
            get {
                return this.chrgBrFieldSpecified;
            }
            set {
                this.chrgBrFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public CashAccount16CH_IdAndCurrency ChrgsAcct {
            get {
                return this.chrgsAcctField;
            }
            set {
                this.chrgsAcctField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CdtTrfTxInf")]
        public CreditTransferTransactionInformation10CH[] CdtTrfTxInf {
            get {
                return this.cdtTrfTxInfField;
            }
            set {
                this.cdtTrfTxInfField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public enum PaymentMethod3Code {
        
        /// <remarks/>
        CHK,
        
        /// <remarks/>
        TRF,
        
        /// <remarks/>
        TRA,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class BranchData2 {
        
        private string idField;
        
        private string nmField;
        
        private PostalAddress6 pstlAdrField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6 PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class FinancialIdentificationSchemeName1Choice {
        
        private string itemField;
        
        private ItemChoiceType3 itemElementNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cd", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Prtry", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType3 ItemElementName {
            get {
                return this.itemElementNameField;
            }
            set {
                this.itemElementNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd", IncludeInSchema=false)]
    public enum ItemChoiceType3 {
        
        /// <remarks/>
        Cd,
        
        /// <remarks/>
        Prtry,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class GenericFinancialIdentification1 {
        
        private string idField;
        
        private FinancialIdentificationSchemeName1Choice schmeNmField;
        
        private string issrField;
        
        /// <remarks/>
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public FinancialIdentificationSchemeName1Choice SchmeNm {
            get {
                return this.schmeNmField;
            }
            set {
                this.schmeNmField = value;
            }
        }
        
        /// <remarks/>
        public string Issr {
            get {
                return this.issrField;
            }
            set {
                this.issrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class FinancialInstitutionIdentification7 {
        
        private string bICField;
        
        private ClearingSystemMemberIdentification2 clrSysMmbIdField;
        
        private string nmField;
        
        private PostalAddress6 pstlAdrField;
        
        private GenericFinancialIdentification1 othrField;
        
        /// <remarks/>
        public string BIC {
            get {
                return this.bICField;
            }
            set {
                this.bICField = value;
            }
        }
        
        /// <remarks/>
        public ClearingSystemMemberIdentification2 ClrSysMmbId {
            get {
                return this.clrSysMmbIdField;
            }
            set {
                this.clrSysMmbIdField = value;
            }
        }
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public PostalAddress6 PstlAdr {
            get {
                return this.pstlAdrField;
            }
            set {
                this.pstlAdrField = value;
            }
        }
        
        /// <remarks/>
        public GenericFinancialIdentification1 Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class BranchAndFinancialInstitutionIdentification4 {
        
        private FinancialInstitutionIdentification7 finInstnIdField;
        
        private BranchData2 brnchIdField;
        
        /// <remarks/>
        public FinancialInstitutionIdentification7 FinInstnId {
            get {
                return this.finInstnIdField;
            }
            set {
                this.finInstnIdField = value;
            }
        }
        
        /// <remarks/>
        public BranchData2 BrnchId {
            get {
                return this.brnchIdField;
            }
            set {
                this.brnchIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="ContactDetails2-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class ContactDetails2CH {
        
        private string nmField;
        
        private string othrField;
        
        /// <remarks/>
        public string Nm {
            get {
                return this.nmField;
            }
            set {
                this.nmField = value;
            }
        }
        
        /// <remarks/>
        public string Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="PersonIdentification5-CH", Namespace="http://www.six-interbank-clearing.com/de/pain.001.001.03.ch.02.xsd")]
    public partial class PersonIdentification5CH {
        
        private DateAndPlaceOfBirth dtAndPlcOfBirthField;
        
        private GenericPersonIdentification1 othrField;
        
        /// <remarks/>
        public DateAndPlaceOfBirth DtAndPlcOfBirth {
            get {
                return this.dtAndPlcOfBirthField;
            }
            set {
                this.dtAndPlcOfBirthField = value;
            }
        }
        
        /// <remarks/>
        public GenericPersonIdentification1 Othr {
            get {
                return this.othrField;
            }
            set {
                this.othrField = value;
            }
        }
    }
}
