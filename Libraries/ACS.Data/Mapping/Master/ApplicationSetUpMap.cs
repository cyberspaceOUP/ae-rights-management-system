//Create by Saddam on 30/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Data.Mapping.Master
{
   

    public partial class ApplicationSetUpMap : EntityTypeConfiguration<ApplicationSetUp>
    {
        public ApplicationSetUpMap()
        {
            this.ToTable("ApplicationSetUp");
            this.HasKey(a => a.Id);
            this.Property(a => a.key).IsRequired().HasMaxLength(100);
            this.Property(a => a.keyValue).IsOptional();
            this.Property(a => a.keyStatus).IsOptional().HasMaxLength(100);
            this.Property(a => a.keyDescription).IsOptional().HasMaxLength(200);
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
