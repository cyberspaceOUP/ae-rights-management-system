using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductLicenseAddendumLinkMap : EntityTypeConfiguration<ProductLicenseAddendumLink>
    {
        public ProductLicenseAddendumLinkMap(){
            this.ToTable("ProductLicenseAddendumLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.LicenseId).IsRequired();
            this.Property(a => a.AddendumId).IsOptional();
            this.Property(a => a.Active).IsRequired().HasMaxLength(1);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.AddendumLinkProductLicense)
             .WithMany(s => s.ProductLicenseAddendumLink)
             .HasForeignKey(usl => usl.LicenseId)
               .WillCascadeOnDelete(false);
        
        }
        
        
    }
}
