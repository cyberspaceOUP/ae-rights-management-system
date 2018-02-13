using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductMasterMap : EntityTypeConfiguration<ProductMaster>
    {
        public ProductMasterMap()
        {
            this.ToTable("ProductMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.DivisionId).IsRequired();
            this.Property(a => a.SubdivisionId).IsOptional();
            this.Property(a => a.ProductCategoryId).IsRequired();
            this.Property(a => a.ProductTypeId).IsRequired();
            this.Property(a => a.SubProductTypeId).IsRequired();
            this.Property(a => a.SubProductTypeId).IsOptional();
            this.Property(a => a.ProjectCode).IsOptional().HasMaxLength(50);
            this.Property(a => a.ProductCode).IsRequired().HasMaxLength(50);
            this.Property(a => a.OUPISBN).IsOptional().HasMaxLength(20);
            this.Property(a => a.WorkingProduct).IsRequired().HasMaxLength(200);
            this.Property(a => a.WorkingSubProduct).IsOptional().HasMaxLength(200);
            this.Property(a => a.OUPEdition).IsRequired().HasMaxLength(100);
            this.Property(a => a.Volume).IsOptional().HasMaxLength(100);
            this.Property(a => a.CopyrightYear).IsRequired().HasMaxLength(100);
            this.Property(a => a.ImprintId).IsRequired();
            this.Property(a => a.LanguageId).IsRequired();
            this.Property(a => a.SeriesId).IsOptional();
            this.Property(a => a.Derivatives).IsRequired();
            this.Property(a => a.OrgISBN).IsOptional().HasMaxLength(20);
            this.Property(a => a.ProjectedPublishingDate).IsOptional();
            this.Property(a => a.ProjectedPrice).IsOptional();
            this.Property(a => a.ProjectedCurrencyId).IsOptional();
            this.Property(a => a.PubCenterId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.FinalProductName).IsOptional();
            this.Property(a => a.PublishingDate).IsOptional();
            this.Property(a => a.ThirdPartyPermission).IsRequired();

            this.HasRequired(a => a.ProductDivision)
               .WithMany()
               .HasForeignKey(a => a.DivisionId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductSubDivision)
               .WithMany()
               .HasForeignKey(a => a.SubdivisionId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductProductCategory)
               .WithMany()
               .HasForeignKey(a => a.ProductCategoryId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductProductType)
               .WithMany()
               .HasForeignKey(a => a.ProductTypeId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductSubProductType)
               .WithMany()
               .HasForeignKey(a => a.SubProductTypeId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductImprint)
               .WithMany()
               .HasForeignKey(a => a.ImprintId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductLanguage)
               .WithMany()
               .HasForeignKey(a => a.LanguageId)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductSeries)
               .WithMany()
               .HasForeignKey(a => a.SeriesId)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductProjectedCurrecy)
              .WithMany()
              .HasForeignKey(a => a.ProjectedCurrencyId)
              .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductPubCenter)
              .WithMany()
              .HasForeignKey(a => a.PubCenterId)
              .WillCascadeOnDelete(false);

        }
    }
}
