///Create by Saddam on 08/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.PermissionsOutbound
{
  
    public partial class PermissionsOutboundSearchHistory : BaseEntity
    {
        public string SessionId { get; set; }
        public string LicenseeName { get; set; }

        public DateTime? RequestFromDate { get; set; }
        public DateTime? RequestToDate { get; set; }

        public string LicenseePublicationTitle { get; set; }
        public DateTime? PermissionFromDate { get; set; }
        public DateTime? PermissionToDate { get; set; }

        public DateTime? DateExpiry { get; set; }
        public string RequestMaterial { get; set; }
     
        public int? LanguageId { get; set; }
        public string Extent { get; set; }
        public int? TerritoryId { get; set; }
        public DateTime? InvoiceFromdate { get; set; }
        public DateTime? InvoiceTodate { get; set; }


        public string InvoiceApplicable { get; set; }
        public int? InvoiceNo { get; set; }
        public int? InvoiceCurrency { get; set; }
        public string InvoiceValue { get; set; }
        public string InvoiceDescription { get; set; }
        public string Copies_To_Be_Received { get; set; }
        public int? NumberOfCopies { get; set; }
        public string Remarks { get; set; }
        public string TypeOfrightsId { get; set; }
        public string ProductCode { get; set; }
        public string ContractCode { get; set; }
        public string PermissionsOutboundCode { get; set; }
        public string ContractStatus { get; set; }
        public string TypeFor { get; set; }


        public string ISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string AuthorName { get; set; }
        public string AuthorContractCode { get; set; }
        public string AuthorCode { get; set; }
        public string ProductLicenseCode { get; set; }
        public string PublishingCompanyCode { get; set; }

        public string AuthorSAPCode { get; set; }

        
    }
}
