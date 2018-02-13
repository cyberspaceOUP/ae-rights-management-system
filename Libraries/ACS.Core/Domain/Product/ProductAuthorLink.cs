using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductAuthorLink : BaseEntity, ILocalizedEntity
    {
       public int ProductId { get; set;}
       public int AuthorId { get; set; }

       #region Navigation Properties
       public virtual AuthorMaster ProductAuthorLinkAuthor { get; set; }

       public virtual ProductMaster ProductAuthorLinkProduct { get; set; }
       
       #endregion
    }
}
