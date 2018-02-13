//Create By Ankush 16/08/2016
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
    public partial class RightsSellingPaymentTagging : BaseEntity, ILocalizedEntity
    {
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public int? subproducttypeid { get; set; }
        public double? Percentage { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string BankName { get; set; }
        public double? Amount { get; set; }
        public double? AuthorAmount { get; set; }
        public double? OupAmount { get; set; }

        public int AuthorId { get; set; }
        public int RightsSellingMasterId { get; set; }
        public int PublishingCompanyId { get; set; }

        public double? WithHoldingTax { get; set; }
        public double? ConverisonRate { get; set; }
        
        public virtual AuthorContractOriginal AuthorContract { get; set; }

        public virtual ProductLicense ProductLicense { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

        //public virtual AuthorMaster AuthorMaster { get; set; }
        //public virtual RightsSellingMaster RightsSellingMaster { get; set; }
      

    }
}
