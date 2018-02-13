using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class KitISBN : BaseEntity, ILocalizedEntity
    {
        public string ISBN { get; set; }

        public int? Division { get; set; }
        public int? SubDivision { get; set; }
        public int? ProductCategory { get; set; }

        public string WorkingProduct { get; set; }
        public string SubWorkingProduct { get; set; }
        public Decimal? ProjectedPrice { get; set; }
        public int? ProjectedCurrency { get; set; }
        public int? ProductTypeId { get; set; }
        public int? SubProductTypeId { get; set; }

        public virtual ICollection<KitProductLink> KitProductLink { get; set; }

    }

    public partial class KitProductLink : BaseEntity, ILocalizedEntity
    {
        public int KitId { get; set; }
        public int ProductId { get; set; }

        public virtual KitISBN KitISBN { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }

}
