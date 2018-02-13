using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductLicenseUpdateDetailsMap : EntityTypeConfiguration<ProductLicenseUpdateDetails>
    {
        public ProductLicenseUpdateDetailsMap() {
            this.ToTable("ProductLicenseUpdateDetails");
            this.HasKey(a => a.Id);
            //this.HasOptional<ProductLicense>(a => a.UpdateProductLicense).WithMany().HasForeignKey(a => a.LicenseId);
            this.Property(a => a.LicenseId).IsRequired();
            this.Property(a => a.LicensorCopiesSentDate).IsOptional();
            this.Property(a => a.EFilesCost).IsOptional();
            this.Property(a => a.EFilesRequestDate).IsOptional();
            this.Property(a => a.EFilesReceivedDate).IsOptional();
            this.Property(a => a.Mode).IsOptional();
            this.Property(a => a.LicenseId).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.AgreementDate).IsOptional();
            this.Property(a => a.Effectivedate).IsOptional();
            this.Property(a => a.Contractperiodinmonth).IsOptional();
            this.Property(a => a.Expirydate).IsOptional();

            this.HasRequired(usl => usl.UpdateProductLicense)
                .WithMany(a => a.IProductLicenseUpdateDetails)
                .HasForeignKey(a => a.LicenseId)
              .WillCascadeOnDelete(false);        
        
        }
    }
}
