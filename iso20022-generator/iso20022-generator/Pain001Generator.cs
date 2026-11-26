using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using iso20022_generator.validation;
using iso20022_generator.xml;

namespace iso20022_generator
{
    /// <summary>
    /// Generates pain.001.001.09.ch.03 credit transfer files according to the Swiss Payment Standards 2026
    /// (Implementation Guidelines Credit Transfer v2.3).
    /// </summary>
    public class Pain001Generator
    {
        private readonly List<PaymentInformation> _paymentInformations = new List<PaymentInformation>();
        private readonly Initialization _initialization;

        public Sender Sender { get; set; }

        /// <summary>
        /// Initializes a new generator which allows creating ISO20022-pain.001 files.
        /// </summary>
        /// <param name="init">Object with all the required information for setting up a new transaction document.</param>
        /// <param name="sender">Debtor (owner of the debit account).</param>
        public Pain001Generator(Initialization init, Sender sender)
        {
            _initialization = init ?? throw new ArgumentNullException(nameof(init));
            Checks.Require(Checks.IsValidReference(init.UniqueDocumentId),
                "UniqueDocumentId (MsgId) is mandatory, max. 35 characters of A-Z a-z 0-9 space ' ( ) + , - . / : ?");
            PaymentRules.ValidateSender(sender);
            Sender = sender;
        }

        /// <summary>
        /// Adds a payment information (B level) for domestic (D) or foreign currency / cross-border (X) payments.
        /// </summary>
        /// <param name="requiredExecutionDate">Requested execution date, must not be in the past.</param>
        /// <param name="paymentMethod">Only "TRF" is supported ("TRA" is accepted for 3.x compatibility and mapped to "TRF").</param>
        /// <param name="batchBookingSpezified">Emits BtchBookg=true (one batch booking per payment information).</param>
        public PaymentInformation AddPaymentInfo(DateTime requiredExecutionDate, string paymentMethod = "TRF", bool batchBookingSpezified = false)
        {
            var method = paymentMethod?.Trim().ToUpperInvariant();
            if (method != "TRF" && method != "TRA")
                throw new ArgumentException($"Invalid payment method '{paymentMethod}'. Only TRF is supported (cheques are not supported).");

            return CreatePaymentInformation(requiredExecutionDate, batchBookingSpezified);
        }

        /// <summary>
        /// Adds a payment information for domestic instant payments (D V2: CHF to a CH/LI IBAN).
        /// The bank must offer instant payments via pain.001.
        /// </summary>
        public PaymentInformation AddInstantPaymentInfo(DateTime requiredExecutionDate,
            InstantPaymentMode mode = InstantPaymentMode.Instant, bool batchBookingSpezified = false)
        {
            var pmtInf = CreatePaymentInformation(requiredExecutionDate, batchBookingSpezified);
            pmtInf.LocalInstrument = mode == InstantPaymentMode.Instant ? "INST" : "ITP";
            return pmtInf;
        }

        /// <summary>
        /// Adds a payment information for SEPA payments (S: EUR to an IBAN in the SEPA area).
        /// </summary>
        /// <param name="requiredExecutionDate">Requested execution date, must not be in the past.</param>
        /// <param name="instant">SEPA Instant (LclInstrm INST), only in consultation with the bank.</param>
        /// <param name="batchBookingSpezified">Emits BtchBookg=true.</param>
        public PaymentInformation AddSepaPaymentInfo(DateTime requiredExecutionDate, bool instant = false, bool batchBookingSpezified = false)
        {
            var pmtInf = CreatePaymentInformation(requiredExecutionDate, batchBookingSpezified);
            pmtInf.ServiceLevel = "SEPA";
            if (instant)
                pmtInf.LocalInstrument = "INST";
            return pmtInf;
        }

        /// <summary>
        /// Adds a new transaction to the given payment information. The transaction is validated against the
        /// rules of its payment type (D, D instant, S or X); an <see cref="ArgumentException"/> is thrown on violations.
        /// </summary>
        /// <param name="pmtInf">Payment information created by one of the AddXxxPaymentInfo methods.</param>
        /// <param name="receiver">Object with all the required information about the receiver of the new transaction</param>
        /// <param name="transaction">Object with all the required information about the transaction itself</param>
        public void AddTransaction(PaymentInformation pmtInf, Receiver receiver, TransactionBase transaction)
        {
            if (pmtInf == null || !_paymentInformations.Contains(pmtInf))
                throw new ArgumentException("Payment information must be created by this generator", nameof(pmtInf));
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            PaymentRules.Validate(pmtInf, receiver, transaction, _initialization);
            pmtInf.Transactions.Add((receiver, transaction));
        }

        /// <summary>Returns the SPS payment type a transaction is assigned to.</summary>
        public PaymentType GetPaymentType(PaymentInformation pmtInf, TransactionBase transaction) =>
            PaymentRules.Classify(pmtInf, transaction, _initialization);

        /// <summary>
        /// Builds the document, validates it and returns it as XDocument. Allows adding elements your
        /// financial institution may require; serialize it yourself afterwards.
        /// </summary>
        public XDocument GetXDocument()
        {
            if (_paymentInformations.Count == 0)
                throw new InvalidOperationException("At least one payment information must be added. Use AddPaymentInfo method.");

            foreach (var pmtInf in _paymentInformations)
            {
                PaymentRules.ValidatePaymentInformation(pmtInf, Sender);
                foreach (var (receiver, transaction) in pmtInf.Transactions)
                    PaymentRules.Validate(pmtInf, receiver, transaction, _initialization);
            }

            var document = new Pain001DocumentBuilder(_initialization, Sender, DateTime.Now).Build(_paymentInformations);

            if (_initialization.ValidateAgainstSchema)
            {
                var errors = SchemaValidator.Validate(document);
                if (errors.Count > 0)
                    throw new Pain001ValidationException(errors);
            }

            return document;
        }

        /// <summary>
        /// Returns the xml serialized document (UTF-8).
        /// </summary>
        public string GetPain001String()
        {
            var document = GetXDocument();
            using (var stream = new MemoryStream())
            {
                Write(document, stream);
                return new UTF8Encoding(false).GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Serializes the document to the given file (UTF-8 without BOM).
        /// </summary>
        /// <param name="fileName">Full path to the desired xml file</param>
        public void SavePain001ToDirectory(string fileName)
        {
            var document = GetXDocument();
            using (var stream = File.Create(fileName))
                Write(document, stream);
        }

        private PaymentInformation CreatePaymentInformation(DateTime requiredExecutionDate, bool batchBooking)
        {
            if (requiredExecutionDate.Date < DateTime.Now.Date)
                throw new ArgumentException("ExecutionDate cannot be in the past");

            var pmtInf = new PaymentInformation($"PmtInfId-{_paymentInformations.Count + 1}", requiredExecutionDate)
            {
                BatchBooking = batchBooking
            };
            _paymentInformations.Add(pmtInf);
            return pmtInf;
        }

        private static void Write(XDocument document, Stream stream)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true
            };
            using (var writer = XmlWriter.Create(stream, settings))
                document.Save(writer);
        }
    }
}
