using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class AddendumRoyaltySlabMap : EntityTypeConfiguration<AddendumRoyaltySlab>
    {
        public AddendumRoyaltySlabMap()
        {

            this.ToTable("AddendumRoyaltySlab");
            this.HasKey(a => a.Id);
            this.Property(a => a.AddendumId).IsRequired();
            this.Property(a => a.ProductSubTypeId).IsRequired();
            this.HasRequired<ProductTypeMaster>(a => a.AddendumRoyalityProductSubProduct).WithMany().HasForeignKey(a => a.ProductSubTypeId);
            this.Property(a => a.copiesfrom).IsRequired();
            this.Property(a => a.copiesto).IsRequired();
            this.Property(a => a.percentage).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.AddendumRoyalityAddendumDetails)
              .WithMany(s => s.AddendumDetailsRoyalty)
              .HasForeignKey(usl => usl.AddendumId)
                .WillCascadeOnDelete(false);

        }
    }
}
