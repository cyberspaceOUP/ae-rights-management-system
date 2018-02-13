using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class LoginHistoryMap : EntityTypeConfiguration<LoginHistory>
    {
      public LoginHistoryMap()
      {
          this.ToTable("LoginHistory");
          this.HasKey(a => a.Id);
          this.Property(a => a.UserName).IsRequired().HasMaxLength(200);
          this.Property(a => a.UserPassword).IsRequired().HasMaxLength(300);
          //this.Property(a => a.Date).IsRequired().HasMaxLength(50);
          this.Property(a => a.Attempt).IsRequired();
        }
    }
}
