//Create By Saddam on 17/10/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class FrequencyAlertMaster : BaseEntity, ILocalizedEntity 
    {
        public DateTime? AlertDate { get; set; }
    }
}
