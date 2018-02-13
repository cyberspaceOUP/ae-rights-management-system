//Create by Saddam on 29/04/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{


    public partial class ExecutiveRoleMaster : BaseEntity, ILocalizedEntity
    {
        public string Role { get; set; }
    }

}
