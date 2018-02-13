//Create by Saddam on 29/04/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    
    public partial class ExecutiveRoleMasterMap : EntityTypeConfiguration<ExecutiveRoleMaster>
    {
        public ExecutiveRoleMasterMap()
        {
            this.ToTable("ExecutiveRoleMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.Role).IsRequired().HasMaxLength(100);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
        }
    }
}
