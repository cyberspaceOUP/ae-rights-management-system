//create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
  public partial  class ExecutiveReporting : BaseEntity, ILocalizedEntity
    {
      public int executiveid { get; set; }
      public int reportingidto { get; set; }
    
      //public virtual ICollection<ExecutiveMaster> ExecutiveM { get; set; }
      public virtual ExecutiveMaster ExecutiveM { get; set; }

      public virtual ExecutiveMaster ReportingTo { get; set; }

      
      
    }
}
