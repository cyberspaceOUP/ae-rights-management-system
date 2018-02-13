//Create By Saddam on 04/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.PermissionsOutbound;

namespace ACS.Core.Domain.PermissionsOutbound
{
   
    public partial class PermissionsOutboundDocument : BaseEntity, ILocalizedEntity
    {
        public string Documentname { get; set; }
        public string DocumentFile { get; set; }
        public int PermissionsOutboundUpdateId { get; set; }

        public virtual PermissionsOutboundUpdate PermissionsOutboundUpdate { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
