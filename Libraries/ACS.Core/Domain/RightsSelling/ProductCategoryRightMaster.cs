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
  public partial  class ProductCategoryRightMaster : BaseEntity, ILocalizedEntity
    {
        public string ProductCategory { get; set; }

        public string ProductCategoryCode { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
