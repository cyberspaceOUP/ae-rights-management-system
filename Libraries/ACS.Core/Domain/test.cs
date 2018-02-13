using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain
{
  public partial  class test : BaseEntity, ILocalizedEntity
    {
     
      public int COlumn1 { get; set; }
    }
}
