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
    public partial class ProductLicenseRoyalityMap : EntityTypeConfiguration<ProductLicenseRoyality>
    {
        public ProductLicenseRoyalityMap()
        {

            this.ToTable("ProductLicenseRoyality");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductLicenseid).IsRequired();
            this.Property(a => a.ProductSubTypeId).IsRequired();
            this.HasRequired<ProductTypeMaster>(a => a.RoyalityProductSubProduct).WithMany().HasForeignKey(a => a.ProductSubTypeId);
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

            this.HasRequired(usl => usl.RoyalityProductLicense)
              .WithMany(s => s.PProductLicenseRoyality)
              .HasForeignKey(usl => usl.ProductLicenseid)
                .WillCascadeOnDelete(false);

        }
    }
}
