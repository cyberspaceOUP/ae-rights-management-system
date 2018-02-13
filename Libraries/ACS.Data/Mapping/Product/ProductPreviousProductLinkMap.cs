using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductPreviousProductLinkMap : EntityTypeConfiguration<ProductPreviousProductLink>
    {
        public ProductPreviousProductLinkMap()
        {

            this.ToTable("ProductPreviousProductLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.PreviousProductId).IsOptional();
            this.Property(a => a.AuthorContractId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.PreviousProduct)
               .WithMany()
               .HasForeignKey(a => a.PreviousProductId)
               .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.ProductMaster)
               .WithMany(s => s.ProductPreviousProductLink)
               .HasForeignKey(usl => usl.ProductId)
                 .WillCascadeOnDelete(false);

        }
    }
}
