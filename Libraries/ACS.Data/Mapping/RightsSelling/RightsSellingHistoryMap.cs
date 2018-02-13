//Create By Ankush 08/09/2016
using ACS.Core.Domain.RightsSelling;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.RightsSelling
{
    public partial class RightsSellingHistoryMap : EntityTypeConfiguration<RightsSellingHistory>
    {
        public RightsSellingHistoryMap()
        {
            this.ToTable("RightsSellingHistory");
            this.HasKey(a => a.Id);
            this.Property(a => a.SessionId).IsRequired();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            

        }
    }
}