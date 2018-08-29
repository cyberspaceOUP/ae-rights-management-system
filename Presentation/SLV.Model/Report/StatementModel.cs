using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Report
{

    public class StatementModel
    {
        //Start by Prakash
        public string AuthorContractCode { get; set; }
        public string AuthorCode { get; set; }
        public string AuthorName { get; set; }

        public string ProductLicenseCode { get; set; }
        public string PublishingCompanyCode { get; set; }
        public string PublishingCompanyName { get; set; }

        public string Year { get; set; }
        public string Month { get; set; }

        public string EntryFromYear { get; set; }
        public string EntryFromMonth { get; set; }
        public string EntryToYear { get; set; }
        public string EntryToMonth { get; set; }

        public string FromYear { get; set; }
        public string FromMonth { get; set; }
        public string ToYear { get; set; }
        public string ToMonth { get; set; }

        public string Type { get; set; }

        public int? AuthorId { get; set; }
        public int? AuthorContractId { get; set; }
        public int? PublishingCompanyId { get; set; }
        public int? ProductLicenseId { get; set; }

        public Decimal? TotalAmount { get; set; }

        //---More Field For Detail
        public int RightsSellingPaymentTaggingId { get; set; }
        public Decimal? Amount { get; set; }
        public Decimal? AuthorAmount { get; set; }
        public Decimal? OupAmount { get; set; }
        public Decimal? Percentage { get; set; }
        public string PaymentDate { get; set; }
        public string BankName { get; set; }
        public string ChequeNumber { get; set; }
        public string PaymentMode { get; set; }
        public string ContractId { get; set; }
        public string LicenseId { get; set; }

        //--Added on 30/11/2016
        public string InvoiceCurrency { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string OrganizationName { get; set; }
        public string TypeOfRights { get; set; }
        public string LanguageName { get; set; }

        public string RightsSellingCode { get; set; }
        public string PermissionsOutboundCode { get; set; }

        //added on 12 July, 2017
        public string SAPagreementNo { get; set; }
        public string LicenseeName { get; set; }
        public string CountryName { get; set; }
        public string LanguageNameOriginal { get; set; }
        public string AuthorSAPCode { get; set; }
        public string For { get; set; }
        public Decimal? WithHoldingTax { get; set; }
        public Decimal? ConverisonRate { get; set; }
        //End by Prakash

        //Add by Ankush on Dated 30/09/2016
        public string SubsidiaryRights { get; set; }

        public string ReturnList { get; set; }

        public string InvoiceNo { get; set; }
        public string InvoiceValue { get; set; }

        //Added by Saddam on 17/11/2016
        public string ISBN { get; set; }
        public string DivisionName { get; set; }
        //ended by saddam

    }

}
