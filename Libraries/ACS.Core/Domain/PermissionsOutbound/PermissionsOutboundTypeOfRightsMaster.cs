//Create By Saddam on 01/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;


namespace ACS.Core.Domain.PermissionsOutbound
{
   public partial class PermissionsOutboundTypeOfRightsMaster :BaseEntity, ILocalizedEntity
    {
       public int? PermissionsOutboundId { get; set; }
       public int? TypeofRightsId { get; set; }
       public string Quantity { get; set; }
        
    

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

        public virtual TypeOfRightsMaster TypeOfRights { get; set; }

        public virtual PermissionsOutboundMaster PermissionsOutboundMaster { get; set; }
       
    }
}
