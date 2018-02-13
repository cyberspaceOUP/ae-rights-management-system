using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class TickerMap : EntityTypeConfiguration<Ticker>
    {
        public TickerMap()
        {
            this.ToTable("Ticker");
            this.HasKey(a => a.Id);
            this.Property(a => a.Title).IsRequired().HasMaxLength(500);
            this.Property(a => a.FromDate).IsOptional();
            this.Property(a => a.ToDate).IsOptional();
            this.Property(a => a.Order).IsOptional();
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
