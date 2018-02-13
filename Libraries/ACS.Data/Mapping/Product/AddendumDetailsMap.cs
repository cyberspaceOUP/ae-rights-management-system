using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class AddendumDetailsMap : EntityTypeConfiguration<AddendumDetails>
    {
        public AddendumDetailsMap(){
            this.ToTable("AddendumDetails");
            this.HasKey(a => a.Id);
            this.Property(a => a.LicenseId).IsRequired();
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.AddendumCode).IsRequired().HasMaxLength(20);
            this.Property(a => a.AddendumDate).IsRequired();
            this.Property(a => a.AddendumType).IsRequired().HasMaxLength(10);
            this.Property(a => a.Periodofagreement).IsOptional();
            this.Property(a => a.ExpiryDate).IsOptional();
            this.Property(a => a.FirstImpressionWithinDate).IsOptional();
            this.Property(a => a.NoOfImpressions).IsOptional();
            this.Property(a => a.BalanceQuantityCarryForward).IsRequired().HasMaxLength(1);
            this.Property(a => a.AddendumQuantity).IsOptional();
            this.Property(a => a.BalanceQuantity).IsOptional();
            this.Property(a => a.RoyaltyTerms).IsOptional();
            this.Property(a => a.Remarks).IsOptional();
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
