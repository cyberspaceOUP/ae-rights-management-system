///Create by Saddam on 14/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;

namespace ACS.Core.Domain.OtherContract
{
  public partial  class OtherContractDivisionLink : BaseEntity, ILocalizedEntity
    {
      public int? othercontractid { get; set; }
      public int? divisionid { get; set; }
      public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
      public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
      public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
      public virtual DivisionMaster Division { get; set; }
      public virtual OtherContractMaster OtherContractMaster { get; set; }
    
    }
}
