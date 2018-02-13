using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Product;

namespace ACS.Data.Mapping.Product
{
    public partial class KITISBNMap : EntityTypeConfiguration<KitISBN>
    {
        public KITISBNMap()
        {
            this.ToTable("KitISBN");
            this.Property(a => a.ISBN).IsRequired().HasMaxLength(20);

            this.Property(a => a.Division).IsOptional();
            this.Property(a => a.SubDivision).IsOptional();
            this.Property(a => a.ProductCategory).IsOptional();

            this.Property(a => a.WorkingProduct).IsOptional();
            this.Property(a => a.SubWorkingProduct).IsOptional();
            this.Property(a => a.ProjectedPrice).IsOptional();
            this.Property(a => a.ProjectedCurrency).IsOptional();
            this.Property(a => a.ProductTypeId).IsOptional();
            this.Property(a => a.SubProductTypeId).IsOptional();
        }
    }


    public partial class KitProductLinkMap : EntityTypeConfiguration<KitProductLink>
    {
        public KitProductLinkMap()
        {
            this.ToTable("KitProductLink");
            this.Property(a => a.KitId).IsRequired();
            this.Property(a => a.ProductId).IsRequired();

            this.HasRequired(a => a.KitISBN)
              .WithMany()
              .HasForeignKey(a => a.KitId)
              .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductMaster).WithMany().HasForeignKey(a => a.ProductId).WillCascadeOnDelete(false);
        }
    }

}
