using System;

namespace iso20022_generator.entity
{
    public class Initialization
    {
        
        private decimal _controlSum;
        private string _contactDetailsName = "iso20022-Generator / enio AG";
        private string _contactDetailsOther = "3.0.0";

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