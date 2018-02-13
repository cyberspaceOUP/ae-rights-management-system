using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductAuthorLinkMap : EntityTypeConfiguration<ProductAuthorLink>
    {
        public ProductAuthorLinkMap(){

            this.ToTable("ProductAuthorLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.AuthorId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.ProductAuthorLinkAuthor)
               .WithMany()
               .HasForeignKey(a => a.AuthorId)
               .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.ProductAuthorLinkProduct)
              .WithMany(s => s.ProductProductAuthorLink)
              .HasForeignKey(usl => usl.ProductId)
                .WillCascadeOnDelete(false);

        }
    }
}
