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
    public partial class GeographicalMasterMap : EntityTypeConfiguration<GeographicalMaster >
    {

        public GeographicalMasterMap()
        {
            this.ToTable("GeographicalMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.geogcode ).IsRequired().HasMaxLength(20);
            this.Property(a => a.geogName).IsRequired().HasMaxLength(100);
            this.Property(a => a.geogtype).IsRequired().HasMaxLength(20);
            //this.HasOptional<GeographicalMaster>(a => a.GeographicalM).WithMany().HasForeignKey(a => a.parentid);
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
