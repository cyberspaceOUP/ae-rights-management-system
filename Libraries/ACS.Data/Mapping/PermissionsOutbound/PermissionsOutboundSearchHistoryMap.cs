//Create by Saddam on 08/08/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.PermissionsOutbound;

namespace ACS.Data.Mapping.PermissionsOutbound
{
   

    class PermissionsOutboundSearchHistoryMap : EntityTypeConfiguration<PermissionsOutboundSearchHistory>
    {
        public PermissionsOutboundSearchHistoryMap()
        {
            this.ToTable("PermissionsOutboundSearchHistory");
            this.HasKey(a => a.Id);
            this.Property(a => a.SessionId).IsRequired();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
        }
    }
}
