using iso20022_generator.schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using iso20022_generator.entity;
using iso20022_generator.entity.Transactions;

namespace iso20022_generator
{
    public class Pain001Generator
    {
        private Document doc = new Document();
        private GroupHeader32CH grpHdr = new GroupHeader32CH(); // Index 1.0
        private PartyIdentification32CH_NameAndId initPty = new PartyIdentification32CH_NameAndId(); // Index 1.8
        private ContactDetails2CH ctctDtls = new ContactDetails2CH(); // Index 1.8
        private PartyIdentification32CH dbtr = new PartyIdentification32CH(); // Index 2.19
        private CashAccount16CH_IdTpCcy dbtrAcct = new CashAccount16CH_IdTpCcy(); // Index 2.20
        private AccountIdentification4ChoiceCH dbtrAcctId = new AccountIdentification4ChoiceCH(); // Index 2.20 / Id
        private BranchAndFinancialInstitutionIdentification4CH_BicOrClrId dbtrAgt = new BranchAndFinancialInstitutionIdentification4CH_BicOrClrId(); // Index 2.21
        private FinancialInstitutionIdentification7CH_BicOrClrId finInstnIdDbtr = new FinancialInstitutionIdentification7CH_BicOrClrId(); // Index 2.21 / Financial Institution Identification

        private List<PaymentInstructionInformation3CH> pmtInfList  = new List<PaymentInstructionInformation3CH>(); // Index 2.0

        private CustomerCreditTransferInitiationV03CH cstmrCdtTrfInitn = new CustomerCreditTransferInitiationV03CH();
        private Initialization initialization;

        /// <summary>
        /// Initializes a new generator which allows creating ISO20022-pain.001 files.
        /// </summary>
        /// <param name="init">Object with all the required information for setting up a new transaction document.</param>
        public Pain001Generator(Initialization init)
        {
            initialization = init;
            doc.CstmrCdtTrfInitn = cstmrCdtTrfInitn;

            // Level A
            cstmrCdtTrfInitn.GrpHdr = grpHdr; // Index 1.0
            grpHdr.MsgId = init.UniqueDocumentId; // Index 1.1 / Required for duplication check
            grpHdr.CreDtTm = DateTime.Now; // Index 1.2
            grpHdr.NbOfTxs = "0"; // Index 1.6
            grpHdr.CtrlSum = 0; // Index 1.7

            grpHdr.InitgPty = initPty; // Index 1.8

            initPty.Nm = init.SenderPartyName; // Index 1.8 - Name
            initPty.CtctDtls = ctctDtls; // Index 1.8 - Contact Details
            ctctDtls.Nm = "iso20022-Generator"; // Index 1.8 - Contact Details.Name
            ctctDtls.Othr = "1.3.0"; // Index 1.8 - Contact Details.Other
        }

        public PaymentInstructionInformation3CH AddPaymentInfo(DateTime requiredExecutionDate)
        {
            if (requiredExecutionDate.Date < DateTime.Now.Date)
                throw new ArgumentException("ExecutionDate cannot be in the past");

            // Level B
            PaymentInstructionInformation3CH pmtInf = new PaymentInstructionInformation3CH();

            pmtInf.PmtInfId = $"PmtInfId-{pmtInfList.Count + 1}"; // Index 2.1
            pmtInf.PmtMtd = PaymentMethod3Code.TRA; // Index 2.2
            pmtInf.BtchBookg = true; // Index 2.3

            pmtInf.ReqdExctnDt = requiredExecutionDate; // Index 2.17
            pmtInf.Dbtr = dbtr;

            dbtr.Nm = initialization.SenderPartyName;

            pmtInf.DbtrAcct = dbtrAcct;
            dbtrAcct.Id = dbtrAcctId;
            dbtrAcctId.Item = initialization.SenderIban; // Index 2.20 / Id / IBAN  Bezugs-Konto

            pmtInf.DbtrAgt = dbtrAgt;

            // Add BIC only if is set to guarantee the compatibility to the old version
            if (!string.IsNullOrEmpty(initialization.SenderBic)) // Index 2.21
                finInstnIdDbtr.BIC = initialization.SenderBic;

            dbtrAgt.FinInstnId = finInstnIdDbtr;


            // Level C
            pmtInf.CdtTrfTxInf = new CreditTransferTransactionInformation10CH[0]; // Index 2.27

            pmtInfList.Add(pmtInf);

            return pmtInf;
        }

        /// <summary>
        /// Adds a new transaction to the document
        /// </summary>
        /// <param name="receiver">Object with all the required information about the receiver of the new transaction</param>
        /// <param name="transaction">Object with all the required information about the transaction itself</param>
        public void AddTransaction(PaymentInstructionInformation3CH pmtInf, Receiver receiver, TransactionBase transaction)
        {
            CreditTransferTransactionInformation10CH cdtTrfTxInf = new CreditTransferTransactionInformation10CH(); // Index 2.27

            PaymentIdentification1 pmtId = new PaymentIdentification1(); // Index 2.28
            cdtTrfTxInf.PmtId = pmtId;
            pmtId.InstrId = "1-" + pmtInf.CdtTrfTxInf.Length; // Index 2.29
            pmtId.EndToEndId = transaction.ReferenceIdentification; // Index 2.30

            AmountType3Choice amt = new AmountType3Choice(); // Index 2.42
            cdtTrfTxInf.Amt = amt;

            ActiveOrHistoricCurrencyAndAmount currencyAndAmount = new ActiveOrHistoricCurrencyAndAmount(); // Index 2.43
            amt.Item = currencyAndAmount;
            currencyAndAmount.Ccy = transaction.CurrencyCode;
            currencyAndAmount.Value = transaction.Amount;

            PartyIdentification32CH_Name cdtr = new PartyIdentification32CH_Name(); // Index 2.79
            cdtTrfTxInf.Cdtr = cdtr;

            cdtr.Nm = receiver.Name; // Index 2.79 / Name
            PostalAddress6CH pstlAdr = new PostalAddress6CH(); // Index 2.79 / Postal Address
            cdtr.PstlAdr = pstlAdr;

            
            pstlAdr.StrtNm = receiver.StreetName; // Index 2.79 / Street Name

            if (!string.IsNullOrWhiteSpace(receiver.StreetNumber))
            {
                pstlAdr.StrtNm = receiver.StreetName + " " + receiver.StreetNumber; // Index 2.79 / Building Number
            }

            pstlAdr.PstCd = receiver.Zip; // Index 2.79 / Post Code
            pstlAdr.TwnNm = receiver.City; // Index 2.79 / Town Name
            pstlAdr.Ctry = receiver.CountryCode; // Index 2.79 / Country

            CashAccount16CH_Id cdtrAcct = new CashAccount16CH_Id(); // Index 2.80
            cdtrAcct.Id = new AccountIdentification4ChoiceCH(); // Index 2.80 / Identification
            cdtTrfTxInf.CdtrAcct = cdtrAcct;

            if (transaction.GetType() == typeof(TransactionIBANandQRR))
            {
                TransactionIBANandQRR transactionIbaNandQrr = ((TransactionIBANandQRR) transaction);

                cdtrAcct.Id.Item = transactionIbaNandQrr.ReceiverIban; // Index 2.80 / Id / IBAN  Ziel-Konto

                if (!string.IsNullOrWhiteSpace(transactionIbaNandQrr.ReceiverBIC))
                {
                    BranchAndFinancialInstitutionIdentification4CH cdtrAgt = new BranchAndFinancialInstitutionIdentification4CH(); // Index 2.77
                    cdtTrfTxInf.CdtrAgt = cdtrAgt;

                    FinancialInstitutionIdentification7CH finInstnIdCdtr = new FinancialInstitutionIdentification7CH(); // Index 2.77 / Financial Institution Identification
                    cdtrAgt.FinInstnId = finInstnIdCdtr;
                    finInstnIdCdtr.BIC = transactionIbaNandQrr.ReceiverBIC; // Index 2.21
                }

                // QRR
                if (!string.IsNullOrWhiteSpace(transactionIbaNandQrr.QRReferenceNumber))
                {
                    var rmtInf = new RemittanceInformation5CH(); // Index 2.126
                    cdtTrfTxInf.RmtInf = rmtInf;
                    rmtInf.Strd = new StructuredRemittanceInformation7
                    {
                        CdtrRefInf = new CreditorReferenceInformation2
                        {
                            Ref = transactionIbaNandQrr.QRReferenceNumber,
                            Tp = new CreditorReferenceType2
                            {
                                CdOrPrtry = new CreditorReferenceType1Choice
                                {
                                    Item = "QRR"
                                }
                            }
                        }
                    };
                }
            }
            else if (transaction.GetType() == typeof(TransactionESR))
            {
                TransactionESR transactionESR = ((TransactionESR) transaction);

                PaymentTypeInformation19CH pmtTpInf = new PaymentTypeInformation19CH(); // Index 2.31
                cdtTrfTxInf.PmtTpInf = pmtTpInf;
                cdtTrfTxInf.PmtTpInf = new PaymentTypeInformation19CH // Index 2.31
                {
                    LclInstrm = new LocalInstrument2Choice // Index 2.36
                    {
                        ItemElementName = ItemChoiceType5.Prtry, // Index 2.38
                        Item = transactionESR.PaymentType
                    }
                }; 

                cdtrAcct.Id.Item = new GenericAccountIdentification1CH() // Index 2.80
                {
                    Id = transactionESR.ReceiverAccount
                };

                var rmtInf = new RemittanceInformation5CH(); // Index 2.98
                cdtTrfTxInf.RmtInf = rmtInf;
                rmtInf.Strd = new StructuredRemittanceInformation7
                {
                    CdtrRefInf = new CreditorReferenceInformation2
                    {
                        Ref = transactionESR.ESRReferenceNumber
                    }
                };
            }
            else if (transaction.GetType() == typeof(TransactionES))
            {
                TransactionES transactionES = ((TransactionES)transaction);

                PaymentTypeInformation19CH pmtTpInf = new PaymentTypeInformation19CH(); // Index 2.31
                cdtTrfTxInf.PmtTpInf = pmtTpInf;
                cdtTrfTxInf.PmtTpInf = new PaymentTypeInformation19CH // Index 2.31
                {
                    LclInstrm = new LocalInstrument2Choice // Index 2.36
                    {
                        ItemElementName = ItemChoiceType5.Prtry, // Index 2.38
                        Item = transactionES.PaymentType
                    }
                };

                cdtrAcct.Id.Item = new GenericAccountIdentification1CH() // Index 2.80
                {
                    Id = transactionES.ReceiverAccount
                };
            }

            AddNewCreditTransferTransactionInformation(pmtInf, cdtTrfTxInf);
        }

        private void AddNewCreditTransferTransactionInformation(PaymentInstructionInformation3CH pmtInf, CreditTransferTransactionInformation10CH cdtTrfTxInf)
        {
            var pmtInfCdtTrfTxInf = pmtInf.CdtTrfTxInf;
            Array.Resize(ref pmtInfCdtTrfTxInf, pmtInfCdtTrfTxInf.Length + 1);
            pmtInfCdtTrfTxInf[pmtInfCdtTrfTxInf.Length - 1] = cdtTrfTxInf;
            pmtInf.CdtTrfTxInf = pmtInfCdtTrfTxInf;
        }

        private void UpdateLevelA()
        {
            int total = 0;
            cstmrCdtTrfInitn.PmtInf = pmtInfList.ToArray();
            pmtInfList.ForEach(p =>
            {
                total = total + p.CdtTrfTxInf.Length;
            });

            grpHdr.NbOfTxs = total.ToString(); // Index 1.6
        }

        /// <summary>
        /// Serializes the generated document object to a given file destination
        /// </summary>
        /// <param name="fileName">Full path to the desired xml file</param>
        public void SavePain001ToDirectory(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                new XmlSerializer(typeof(Document)).Serialize(sw, doc);
            }
        }

        /// <summary>
        /// Returns the xml serialized document object
        /// </summary>
        /// <returns></returns>
        public string GetPain001String()
        {
            if (pmtInfList.Count == 0)
            {
                throw new Exception("At least one Paymentinformation must be added. Use AddPaymentInfo method.");
            }

            UpdateLevelA();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Document));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);

            xmlSerializer.Serialize(xmlSink, doc);
            byte[] utf8EncodedData = memStrm.ToArray();
            return utf8e.GetString(utf8EncodedData).Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"");
        }

        /// <summary>
        /// Allows direct access to the generated document object. This method is not thought to
        /// be used under normal conditions however your financial institude maybe requires
        /// setting some addittional properties.
        /// </summary>
        /// <returns>Document object which will be serialized to xml</returns>
        public Document getDocumentObject()
        {
            return doc;
        }
    }
}
