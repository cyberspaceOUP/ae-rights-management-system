//Create by Saddam on 01/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
   public partial class AssetSubType : BaseEntity , ILocalizedEntity
    {
       public string AssetName { get; set; }

    }
}
