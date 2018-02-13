//Create By Ankush 09/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;

namespace ACS.Core.Domain.RightsSelling
{
    public partial class RightsSellingHistory : BaseEntity
    {
        public string SessionId { get; set; }
        public DateTime? RequestFromDate { get; set; }
        public DateTime? RequestToDate { get; set; }
        public string Licensee { get; set; }
        public DateTime? ContractFromDate { get; set; }
        public DateTime? ContractToDate { get; set; }
        public int? Dateofexpiry { get; set; }
        public int? ProductCategory { get; set; }
        public int? LanguageId { get; set; }
        public string Payment_Term { get; set; }
        public string Payment_Amount { get; set; }
        public string AuthorName { get; set; }
        public int? Territory { get; set; }
        public string Remarks { get; set; }
        public string ContractStatus { get; set; }
        public string Dateofexpiry_opr { get; set; }

        public string ISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string SubWorkingProduct { get; set; }
        public string ProductCode { get; set; }
        public string AuthorContractCode { get; set; }
        public string AuthorCode { get; set; }
        public string ProductLicenseCode { get; set; }
        public string PublishingCompanyCode { get; set; }
        public string RightsSalesCode { get; set; }

        public string AuthorSAPCode { get; set; }
    }
}
