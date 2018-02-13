//Create By Saddam on 13/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.RightsSelling;

namespace ACS.Core.Domain.RightsSelling
{
   public partial class RightsSellingRoyalty : BaseEntity, ILocalizedEntity
    {
       public int? ContractId { get; set; }
       public int? ProductLicenseId { get; set; }
       public int? subproducttypeid { get; set; }
       public Decimal? CopiesFrom { get; set; }
       public Decimal? CopiesTo { get; set; }
       public Decimal? Percentage { get; set; }
       public int? RightsSellingID { get; set; }


       public virtual AuthorContractOriginal AuthorContract { get; set; }
       public virtual ProductLicense Product { get; set; }
       public virtual ProductTypeMaster ProductTypeMaster { get; set; }

       public virtual RightsSellingMaster RightsSellingMaster { get; set; }


       public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
       public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
       public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
       

    }
}
