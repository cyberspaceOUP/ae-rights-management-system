using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    class FrequencyAlertMasterMap : EntityTypeConfiguration<FrequencyAlertMaster>
    {
        public FrequencyAlertMasterMap()
        {
            this.ToTable("FrequencyAlertMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.AlertDate).IsOptional();
        }
    }
}
