//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ProductTypeMaster : BaseEntity, ILocalizedEntity 
    {
        public string typeName { get; set; }
        public int typelevel { get; set; }
        public int? parenttypeid { get; set; }
        public virtual ProductTypeMaster ProductTypeM { get; set; }

    }
}
