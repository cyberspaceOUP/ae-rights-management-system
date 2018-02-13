using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class PaymentPeriodMap : EntityTypeConfiguration<PaymentPeriod>
    {
        public PaymentPeriodMap()
        {
            this.ToTable("PaymentPeriod");
            this.HasKey(a => a.Id);
            this.Property(a => a.PeriodValueId);
            this.Property(a => a.PaymentType).IsRequired().HasMaxLength(200);
        }
    }
}
