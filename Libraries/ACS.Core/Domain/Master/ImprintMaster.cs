//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    
    public partial class ImprintMaster : BaseEntity, ILocalizedEntity
    {
        public string ImprintName { get; set; }
        public int? PublishingCompanyId { get; set; }

        public virtual PublishingCompanyMaster PublishingCompanyMaster { get; set; }

    }

}
