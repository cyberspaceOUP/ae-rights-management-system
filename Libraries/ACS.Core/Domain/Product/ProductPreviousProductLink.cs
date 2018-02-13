using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductPreviousProductLink : BaseEntity , ILocalizedEntity
    {

       public int ProductId {get;set;}
       public int PreviousProductId { get; set; }
       public int? AuthorContractId { get; set; }

       #region Navigation Properties
       public virtual ProductMaster PreviousProduct { get; set; }
       public virtual ProductMaster ProductMaster { get; set; }
       public virtual AuthorContractOriginal AuthorContract { get; set; }
       #endregion

    }
}
