using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;
using iso20022_generator.validation;

namespace iso20022_generator.xml
{
    /// <summary>Builds the pain.001.001.09 (SPS 2026) XML document. Element order follows the XSD.</summary>
    internal class Pain001DocumentBuilder
    {
        private static readonly XNamespace Ns = SchemaValidator.Namespace;

        private readonly Initialization _init;
        private readonly Sender _sender;
        private readonly DateTime _creationDateTime;

        public Pain001DocumentBuilder(Initialization init, Sender sender, DateTime creationDateTime)
        {
            _init = init;
            _sender = sender;
            _creationDateTime = creationDateTime;
        }

        public XDocument Build(IReadOnlyList<PaymentInformation> paymentInformations)
        {
            var allTransactions = paymentInformations.SelectMany(p => p.Transactions).ToList();
            var controlSum = _init.AutoCalculateControlSum
                ? allTransactions.Sum(t => t.Transaction.Amount)
                : _init.ControlSum;

            var groupHeader = new XElement(Ns + "GrpHdr",
                new XElement(Ns + "MsgId", _init.UniqueDocumentId),
                new XElement(Ns + "CreDtTm", _creationDateTime.ToString("yyyy-MM-dd'T'HH:mm:ss", CultureInfo.InvariantCulture)),
                new XElement(Ns + "NbOfTxs", allTransactions.Count.ToString(CultureInfo.InvariantCulture)),
                controlSum > 0 ? new XElement(Ns + "CtrlSum", FormatAmount(controlSum)) : null,
                new XElement(Ns + "InitgPty",
                    new XElement(Ns + "Nm", _sender.SenderPartyName),
                    SoftwareInformation()));

            var initiation = new XElement(Ns + "CstmrCdtTrfInitn", groupHeader,
                paymentInformations.Select((p, i) => PaymentInformationElement(p, i + 1)));

            return new XDocument(new XDeclaration("1.0", "UTF-8", null), new XElement(Ns + "Document", initiation));
        }

        private XElement SoftwareInformation()
        {
            var entries = new[]
                {
                    ("NAME", _init.SoftwareName),
                    ("PRVD", _init.SoftwareProvider),
                    ("VRSN", _init.SoftwareVersion),
                    ("SPSV", Initialization.SpsIgVersion)
                }
                .Where(e => !string.IsNullOrWhiteSpace(e.Item2))
                .Select(e => new XElement(Ns + "Othr",
                    new XElement(Ns + "ChanlTp", e.Item1),
                    new XElement(Ns + "Id", Truncate(e.Item2, 128))))
                .ToList();

            return entries.Count == 0 ? null : new XElement(Ns + "CtctDtls", entries);
        }

        private XElement PaymentInformationElement(PaymentInformation pmtInf, int pmtInfIndex)
        {
            return new XElement(Ns + "PmtInf",
                new XElement(Ns + "PmtInfId", pmtInf.Id),
                new XElement(Ns + "PmtMtd", "TRF"),
                pmtInf.BatchBooking ? new XElement(Ns + "BtchBookg", "true") : null,
                new XElement(Ns + "NbOfTxs", pmtInf.NumberOfTransactions.ToString(CultureInfo.InvariantCulture)),
                new XElement(Ns + "CtrlSum", FormatAmount(pmtInf.Transactions.Sum(t => t.Transaction.Amount))),
                PaymentTypeInformation(pmtInf),
                new XElement(Ns + "ReqdExctnDt",
                    new XElement(Ns + "Dt", pmtInf.RequestedExecutionDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))),
                new XElement(Ns + "Dbtr",
                    new XElement(Ns + "Nm", _sender.SenderPartyName),
                    Address(_sender.ToPostalAddress()),
                    LeiIdentification(_sender.Lei)),
                new XElement(Ns + "DbtrAcct",
                    new XElement(Ns + "Id", new XElement(Ns + "IBAN", _sender.SenderIban)),
                    string.IsNullOrEmpty(pmtInf.DebitAdvice)
                        ? null
                        : new XElement(Ns + "Tp", new XElement(Ns + "Prtry", pmtInf.DebitAdvice))),
                new XElement(Ns + "DbtrAgt", DebtorAgentIdentification()),
                Optional("InstrForDbtrAgt", pmtInf.InstructionForDebtorAgent),
                pmtInf.ChargeBearer.HasValue ? new XElement(Ns + "ChrgBr", pmtInf.ChargeBearer.Value.ToString()) : null,
                pmtInf.Transactions.Select((t, i) =>
                    TransactionElement(pmtInf, t.Receiver, t.Transaction, $"{pmtInfIndex}-{i + 1}")));
        }

        private XElement PaymentTypeInformation(PaymentInformation pmtInf)
        {
            if (string.IsNullOrEmpty(pmtInf.ServiceLevel) && string.IsNullOrEmpty(pmtInf.LocalInstrument) &&
                string.IsNullOrEmpty(pmtInf.CategoryPurpose))
                return null;

            return new XElement(Ns + "PmtTpInf",
                string.IsNullOrEmpty(pmtInf.ServiceLevel) ? null : new XElement(Ns + "SvcLvl", new XElement(Ns + "Cd", pmtInf.ServiceLevel)),
                string.IsNullOrEmpty(pmtInf.LocalInstrument) ? null : new XElement(Ns + "LclInstrm", new XElement(Ns + "Cd", pmtInf.LocalInstrument)),
                string.IsNullOrEmpty(pmtInf.CategoryPurpose) ? null : new XElement(Ns + "CtgyPurp", new XElement(Ns + "Cd", pmtInf.CategoryPurpose)));
        }

        private XElement DebtorAgentIdentification()
        {
            if (!string.IsNullOrEmpty(_sender.SenderBic))
                return new XElement(Ns + "FinInstnId", new XElement(Ns + "BICFI", _sender.SenderBic));

            var iid = Checks.IidFromIban(_sender.SenderIban);
            if (iid != null)
                return new XElement(Ns + "FinInstnId", ClearingMember("CHBCC", iid));

            throw new ArgumentException("Sender BIC is mandatory for a debit account outside of CH/LI");
        }

        private XElement TransactionElement(PaymentInformation pmtInf, Receiver receiver, TransactionBase tx, string instructionId)
        {
            var type = PaymentRules.Classify(pmtInf, tx, _init);
            var uetr = tx.Uetr ?? (_init.GenerateUetr && type != PaymentType.Sepa ? Guid.NewGuid() : (Guid?)null);
            var reference = tx.CreditorReference;

            return new XElement(Ns + "CdtTrfTxInf",
                new XElement(Ns + "PmtId",
                    new XElement(Ns + "InstrId", instructionId),
                    new XElement(Ns + "EndToEndId", string.IsNullOrWhiteSpace(tx.ReferenceIdentification) ? "NOTPROVIDED" : tx.ReferenceIdentification),
                    uetr.HasValue && type != PaymentType.Sepa ? new XElement(Ns + "UETR", uetr.Value.ToString("D").ToLowerInvariant()) : null),
                new XElement(Ns + "Amt",
                    new XElement(Ns + "InstdAmt", new XAttribute("Ccy", tx.CurrencyCode), FormatAmount(tx.Amount))),
                tx.ChargeBearer.HasValue ? new XElement(Ns + "ChrgBr", tx.ChargeBearer.Value.ToString()) : null,
                CreditorAgent(tx),
                new XElement(Ns + "Cdtr",
                    new XElement(Ns + "Nm", receiver.Name),
                    Address(receiver.ToPostalAddress()),
                    LeiIdentification(receiver.Lei)),
                new XElement(Ns + "CdtrAcct",
                    new XElement(Ns + "Id", tx.CreditorIban != null
                        ? new XElement(Ns + "IBAN", tx.CreditorIban)
                        : new XElement(Ns + "Othr", new XElement(Ns + "Id", tx.CreditorAccountNumber)))),
                tx.InstructionsForCreditorAgent?.Select(i => new XElement(Ns + "InstrForCdtrAgt",
                    Optional("Cd", i.Code),
                    Optional("InstrInf", i.Information))),
                Optional("InstrForDbtrAgt", tx.InstructionForDebtorAgent),
                string.IsNullOrWhiteSpace(tx.PurposeCode) ? null : new XElement(Ns + "Purp", new XElement(Ns + "Cd", tx.PurposeCode)),
                RemittanceInformation(tx, reference));
        }

        private XElement CreditorAgent(TransactionBase tx)
        {
            XElement finInstnId;
            if (tx.CreditorBic != null)
            {
                finInstnId = new XElement(Ns + "FinInstnId", new XElement(Ns + "BICFI", tx.CreditorBic));
            }
            else if (tx.CreditorAgentClearing != null || !string.IsNullOrWhiteSpace(tx.CreditorAgentName))
            {
                var clearing = tx.CreditorAgentClearing;
                finInstnId = new XElement(Ns + "FinInstnId",
                    clearing.HasValue ? ClearingMember(clearing.Value.ClearingSystem, clearing.Value.MemberId) : null,
                    Optional("Nm", tx.CreditorAgentName),
                    string.IsNullOrWhiteSpace(tx.CreditorAgentName) ? null : Address(tx.CreditorAgentAddress));
            }
            else
            {
                return null;
            }

            return new XElement(Ns + "CdtrAgt", finInstnId);
        }

        private XElement RemittanceInformation(TransactionBase tx, (string Reference, string Type)? reference)
        {
            if (reference.HasValue)
            {
                var type = reference.Value.Type == "SCOR"
                    ? new XElement(Ns + "Cd", "SCOR")
                    : new XElement(Ns + "Prtry", reference.Value.Type);

                return new XElement(Ns + "RmtInf",
                    new XElement(Ns + "Strd",
                        new XElement(Ns + "CdtrRefInf",
                            new XElement(Ns + "Tp", new XElement(Ns + "CdOrPrtry", type)),
                            new XElement(Ns + "Ref", reference.Value.Reference)),
                        tx.AdditionalRemittanceInfo?
                            .Where(a => !string.IsNullOrWhiteSpace(a))
                            .Select(a => new XElement(Ns + "AddtlRmtInf", a))));
            }

            return string.IsNullOrWhiteSpace(tx.UnstructuredRemittanceInformation)
                ? null
                : new XElement(Ns + "RmtInf", new XElement(Ns + "Ustrd", tx.UnstructuredRemittanceInformation));
        }

        private static XElement ClearingMember(string clearingSystem, string memberId) =>
            new XElement(Ns + "ClrSysMmbId",
                new XElement(Ns + "ClrSysId", new XElement(Ns + "Cd", clearingSystem)),
                new XElement(Ns + "MmbId", memberId));

        private static XElement Address(PostalAddress address)
        {
            if (address == null || address.IsEmpty)
                return null;

            return new XElement(Ns + "PstlAdr",
                Optional("StrtNm", address.StreetName),
                Optional("BldgNb", address.BuildingNumber),
                Optional("PstCd", address.PostCode),
                Optional("TwnNm", address.TownName),
                Optional("Ctry", address.CountryCode?.Trim().ToUpperInvariant()),
                address.AddressLines?
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => new XElement(Ns + "AdrLine", l.Trim())));
        }

        private static XElement LeiIdentification(string lei) =>
            string.IsNullOrWhiteSpace(lei)
                ? null
                : new XElement(Ns + "Id", new XElement(Ns + "OrgId", new XElement(Ns + "LEI", lei.Trim())));

        /// <summary>Elements must not be empty or contain only spaces (IG chapter 3.4).</summary>
        private static XElement Optional(string name, string value) =>
            string.IsNullOrWhiteSpace(value) ? null : new XElement(Ns + name, value.Trim());

        private static string FormatAmount(decimal amount) => amount.ToString("0.#####", CultureInfo.InvariantCulture);

        private static string Truncate(string value, int length) => value.Length <= length ? value : value.Substring(0, length);
    }
}
