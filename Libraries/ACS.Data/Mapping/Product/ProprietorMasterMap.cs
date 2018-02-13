using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProprietorMasterMap : EntityTypeConfiguration<ProprietorMaster>
    {
        public ProprietorMasterMap()
        {

            this.ToTable("ProprietorMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.ProprietorISBN).IsRequired().HasMaxLength(20);
            this.Property(a => a.ProprietorProduct).IsRequired().HasMaxLength(200);
            this.Property(a => a.ProprietorEdition).IsRequired().HasMaxLength(200);
            this.Property(a => a.ProprietorCopyrightYear).IsRequired().HasMaxLength(10);
            this.Property(a => a.PublishingCompanyId).IsRequired();
            this.Property(a => a.ProprietorPubCenterId).IsRequired();
            this.Property(a => a.ProprietorImPrintId).IsRequired();
            this.Property(a => a.Main).IsRequired();
            this.Property(a => a.ProprietorAuthorName).IsOptional().HasMaxLength(400);

            this.HasRequired(a => a.ProprietorPublishingCompany)
              .WithMany()
              .HasForeignKey(a => a.PublishingCompanyId)
              .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProprietorPubCenter)
              .WithMany()
              .HasForeignKey(a => a.ProprietorPubCenterId)
              .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProprietorImprint)
              .WithMany()
              .HasForeignKey(a => a.ProprietorImPrintId)
              .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.ProprietorProductMaster)
               .WithMany(s => s.ProductProprietorMaster)
               .HasForeignKey(usl => usl.ProductId)
                 .WillCascadeOnDelete(false);
            
           
        }

    }
}
