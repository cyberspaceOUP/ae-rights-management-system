using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class DepartmentMasterMap : EntityTypeConfiguration<DepartmentMaster>
    {
        public DepartmentMasterMap()
        {
            this.ToTable("DepartmentMaster");
            this.HasKey(a => a.Id);
            this.Property(a =>a.DepartmentName).IsRequired().HasMaxLength(100);
            this.Property(a => a.Deactivate).IsOptional().HasMaxLength(1);
            this.Property(a => a.DepartmentCode).IsOptional().HasMaxLength(3);
            this.Property(a => a.EnteredBy).IsOptional();
            this.Property(a => a.EntryDate).IsOptional();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

        }
    }
}
