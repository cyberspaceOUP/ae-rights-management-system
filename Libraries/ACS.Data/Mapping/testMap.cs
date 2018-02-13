using ACS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping
{
  public partial  class testMap:EntityTypeConfiguration<test>
    {
      public testMap()
        {
            this.ToTable("VRV");
            this.HasKey(c => c.Id);
            this.Property(c => c.COlumn1).IsRequired();
       }
    }
}
