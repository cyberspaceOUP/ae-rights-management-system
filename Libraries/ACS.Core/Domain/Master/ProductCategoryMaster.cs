//Create by Saddam 29/04/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{


    public partial class ProductCategoryMaster : BaseEntity, ILocalizedEntity
    {
        public string ProductCategory { get; set; }

        public string ProductCategoryCode { get; set; }

    }

}
