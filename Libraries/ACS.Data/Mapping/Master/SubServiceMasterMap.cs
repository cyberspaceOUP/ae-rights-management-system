using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    class SubServiceMasterMap : EntityTypeConfiguration<SubServiceMaster>
    {
        public SubServiceMasterMap()
        {
            this.ToTable("SubServiceMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.ServiceName).IsRequired().HasMaxLength(200);

            this.HasRequired(a => a.ServiceMaster)
            .WithMany(a => a.SubServiceMasters)
            .HasForeignKey(a => a.ServiceMasterId)
            .WillCascadeOnDelete(false);

        }
    }
}
