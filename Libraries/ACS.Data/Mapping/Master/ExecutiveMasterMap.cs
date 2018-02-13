//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
  public partial  class ExecutiveMasterMap : EntityTypeConfiguration<ExecutiveMaster>
    {
      public ExecutiveMasterMap()
      {
          this.ToTable("ExecutiveMaster");
          this.HasKey(a => a.Id);
          this.Property(a => a.executiveName).IsRequired().HasMaxLength(200);
          this.Property(a => a.executivecode).IsRequired().HasMaxLength(25);
          this.Property(a => a.Emailid).IsRequired().HasMaxLength(1000);
          this.Property(a => a.Password).IsRequired().HasMaxLength(200);
          this.Property(a => a.Mobile).HasMaxLength(25);
          this.Property(a => a.Phoneno).IsRequired().HasMaxLength(25);
          //added by sanjeet singh
          this.Property(a => a.block).IsRequired().HasMaxLength(5);
          this.Property(a => a.PwdChanged).IsRequired().HasMaxLength(5);
          //
          this.Property(a => a.DepartmentId).IsOptional();
          this.Property(a => a.ProcessTransferTo).IsOptional();
          //this.HasOptional<DepartmentMaster>(a => a.DepartmentM).WithMany().HasForeignKey(a => a.Departmentid);
          //this.HasOptional<ExecutiveMaster>(a => a.ExecutiveM).WithMany().HasForeignKey(a => a.ProcessTransferTo);
          this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
          this.Property(a => a.EnteredBy).IsOptional();
          this.Property(a => a.EntryDate).IsRequired();
          this.Property(a => a.ModifiedBy).IsOptional();
          this.Property(a => a.ModifiedDate).IsOptional();
          this.Property(a => a.DeactivateBy).IsOptional();
          this.Property(a => a.DeactivateDate).IsOptional();

          this.Property(a => a.Active).IsOptional();
      }
    }
}
