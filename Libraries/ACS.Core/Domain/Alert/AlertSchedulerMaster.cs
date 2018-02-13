///Create by Saddam on 18/10/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.Alert
{
    public partial class AlertSchedulerMaster : BaseEntity, ILocalizedEntity
    {
        public string SchedulerName { get; set; }
        public DateTime? SchedulerDate { get; set; }

    }
}
