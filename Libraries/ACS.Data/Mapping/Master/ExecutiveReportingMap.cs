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
    public partial class ExecutiveReportingMap : EntityTypeConfiguration<ExecutiveReporting>
    {
        public ExecutiveReportingMap()
        {
            this.ToTable("ExecutiveReporting");
            this.HasKey(a => a.Id);


            //this.HasRequired<ExecutiveMaster>(a => a.ExecutiveM).WithMany().HasForeignKey(a => a.executiveid);

           // this.HasRequired<ExecutiveMaster>(a => a.ReportingTo).WithMany().HasForeignKey(a => a.reportingidto);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.ExecutiveM)
             .WithMany(a=>a.ExecutiveReportings)
             .HasForeignKey(a => a.executiveid)
             .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ReportingTo)
             .WithMany()
             .HasForeignKey(a => a.reportingidto)
             .WillCascadeOnDelete(false);
            
        }
    }
}
