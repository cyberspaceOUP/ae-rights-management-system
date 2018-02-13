using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    class ServiceMasterMap :EntityTypeConfiguration<ServiceMaster>
    {
        public ServiceMasterMap()
        {
            this.ToTable("ServiceMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.ServiceName).IsRequired().HasMaxLength(200);
        }
    }
}
