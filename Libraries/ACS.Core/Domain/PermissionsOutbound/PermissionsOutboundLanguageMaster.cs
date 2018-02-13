//Create By Saddam on 23/08/2016
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
  public partial  class PermissionsOutboundLanguageMaster: BaseEntity, ILocalizedEntity
    {
      public int? PermissionsOutboundId { get; set; }
        public int? languageId { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

        public virtual LanguageMaster LanguageMaster { get; set; }

        public virtual PermissionsOutboundMaster PermissionsOutboundMaster { get; set; }


    }
}
