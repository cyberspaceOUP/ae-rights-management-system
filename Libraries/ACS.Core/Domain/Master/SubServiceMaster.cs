using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class SubServiceMaster : BaseEntity, ILocalizedEntity
    {
        public string ServiceName { get; set; }
        public int ServiceMasterId { get; set; }

        public virtual ServiceMaster ServiceMaster { get; set; }
    }
}
