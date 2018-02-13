using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
     public class ProductLicenceAuthorContractLink : BaseEntity
    {
         public int ProductId { get; set; }
         public int AuthorContractId { get; set; }
         public int licenseId { get; set; }
         public virtual ProductMaster ProductMaster { get; set; }
         public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
         public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
         public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
         public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
         public virtual ProductLicense ProductLicense { get; set; }
    }
}
