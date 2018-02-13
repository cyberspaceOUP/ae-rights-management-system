using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.Master
{
   public partial class ISBNBag : BaseEntity, ILocalizedEntity
    {
       public string ISBN { get; set; }
       public int? ProductTypeid { get; set; }
       public virtual ProductTypeMaster ProductTypeM { get; set; }
       public string Used { get; set; }
       public int? ProductId { get; set; }
       public string Blocked { get; set; }
       
       public int? KitISBNId { get; set; }

       public virtual ICollection<Upload_ISBN_Back> Upload_ISBN_Back { get; set; }
    }
}
