//Created by sanjeet singh
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class ExecutiveLoginHistoryMap : EntityTypeConfiguration<ExecutiveLoginHistory>
    {
      public ExecutiveLoginHistoryMap()
      {
          this.ToTable("ExecutiveLoginHistory");
          this.HasKey(a => a.Id);
          this.Property(a => a.ExecutiveUserName).IsRequired().HasMaxLength(200);
          this.Property(a => a.LoginTime).IsOptional();
          this.Property(a => a.LogoutTime).IsOptional();
      }
    }
}
