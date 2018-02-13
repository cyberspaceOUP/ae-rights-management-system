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
    public partial class SeriesMasterMap : EntityTypeConfiguration<SeriesMaster>
    {
        public SeriesMasterMap()
        {
            this.ToTable("SeriesMaster");
            this.HasKey(a => a.Id);
            this.HasRequired<DivisionMaster>(a => a.DivisionM).WithMany().HasForeignKey(a => a.divisionid);
            this.HasRequired<DivisionMaster>(a => a.DivisionM).WithMany().HasForeignKey(a => a.Subdivisionid);
            this.Property(a => a.Seriesname).IsRequired().HasMaxLength(200);
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
