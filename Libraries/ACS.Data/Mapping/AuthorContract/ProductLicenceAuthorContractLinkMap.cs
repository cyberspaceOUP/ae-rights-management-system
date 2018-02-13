using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    class ProductLicenceAuthorContractLinkMap : EntityTypeConfiguration<ProductLicenceAuthorContractLink>
    {
        public ProductLicenceAuthorContractLinkMap()
        {
            this.ToTable(" ProductLicenceAuthorContractLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.AuthorContractId).IsRequired();
            this.Property(a => a.licenseId).IsRequired();
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.EnteredByForeignKey)
                     .WithMany()
                     .HasForeignKey(usl => usl.EnteredBy)
                       .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.AuthorContractOriginal)
                       .WithMany()
                       .HasForeignKey(usl => usl.AuthorContractId)
                         .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.ProductMaster)
                     .WithMany()
                     .HasForeignKey(usl => usl.ProductId)
                       .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.ProductLicense)
                    .WithMany()
                    .HasForeignKey(usl => usl.licenseId)
                      .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.DeactivateByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.DeactivateBy)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.ModifiedByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.ModifiedBy)
                          .WillCascadeOnDelete(false);
        }
        
    }
}
