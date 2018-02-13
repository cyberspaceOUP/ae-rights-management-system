using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProprietorAuthorLink : BaseEntity, ILocalizedEntity
    {
        public int ProprietorId { get; set; }
       public int AuthorId { get; set; }

       #region Navigation Properties
       public virtual AuthorMaster ProprietorAuthorLinkAuthor { get; set; }
      
       public virtual ProprietorMaster ProprietorMaster { get; set; }


       #endregion

      
    }
}
