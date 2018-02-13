using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.PaymentTaggingMaster
{

    public class AuthorCodeModel
    {
        public string AuthorContractCode { get; set; }
        public string AuthorCode { get; set; }
        public string ProductLicenseCode { get; set; }
        public string PublishingCompanyCode { get; set; }
        public string AuthorId { get; set; }
        public string AuthorContractId { get; set; }
        public int? PublishingCompanyId { get; set; }
        public int? ProductLicenseId { get; set; }
    }


    public partial class PaymentTaggingModel
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public int? ProuductId { get; set; }
        public string ProductCode { get; set; }
        public int? ContractId { get; set; }
        public string AuthorContractCode { get; set; }
        public int? ProductLicenseId { get; set; }
        public string ProductLicensecode { get; set; }
        public string OrganizationName { get; set; }
        public string SessionId { get; set; }
        public string ReturnList { get; set; }

        public string ISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }

        public string AuthorName { get; set; }
        public int? AuthorId { get; set; }
        public int? PublishingCompanyId { get; set; }
        public string PublishingCompanyCode { get; set; }
        public string Type { get; set; }

        //added by Prakash on 21 July, 2017
        public string Licenseecode { get; set; }
        public string Date_of_agreement { get; set; }
        public string Expirydate { get; set; }

        public DateTime? ExpirydateSort { get; set; }
        public DateTime? Date_of_agreementSort { get; set; }
    }


    public partial class PaymentTaggingList
    {
        public string Code { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceValue { get; set; }
        public string CurrencyName { get; set; }
        public string FirstPublicationDate { get; set; }
        public string ProductCategory { get; set; }
        public string EntryDate { get; set; }
        public int Id { get; set; }
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public int? ProuductId { get; set; }
        public int? RightsSellingId { get; set; }
        public int? PermissionsOutboundId { get; set; }
        public string Type { get; set; }
    }

}
