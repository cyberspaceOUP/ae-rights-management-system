//Create By Saddam on 26/09/2016
using ACS.Core.Domain.Alert;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Alert
{
    public partial class AlertSchedulerMasterMap : EntityTypeConfiguration<AlertSchedulerMaster>
    {
        public AlertSchedulerMasterMap()
        {
            this.ToTable("AlertSchedulerMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.SchedulerName).IsOptional().HasMaxLength(400);
            this.Property(a => a.SchedulerDate).IsOptional();
           
        }
    }
}
