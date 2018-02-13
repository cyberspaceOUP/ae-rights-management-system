using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Core.Domain.Master
{
    public partial class ServiceMaster : BaseEntity, ILocalizedEntity
    {
        public ICollection<SubServiceMaster> SubServiceMaster { get; set; }
        public string ServiceName { get; set; }

      
        public virtual ICollection<SubServiceMaster> SubServiceMasters
        {
            get { return SubServiceMaster ?? (SubServiceMaster = new List<SubServiceMaster>()); }
            set { SubServiceMaster = value; }

        }

    }
}
