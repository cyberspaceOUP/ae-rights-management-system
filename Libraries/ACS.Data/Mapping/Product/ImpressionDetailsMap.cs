using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ImpressionDetailsMap : EntityTypeConfiguration<ImpressionDetails>
    {
        public ImpressionDetailsMap()
        {
            this.ToTable("ImpressionDetails");
            this.HasKey(a => a.Id);
            this.Property(a => a.LicenseId).IsOptional();
            this.Property(a => a.ProductId).IsOptional();
            this.Property(a => a.ContractId).IsOptional();
            this.Property(a => a.AddendumId).IsOptional();
            this.Property(a => a.ImpressionNo).IsRequired();
            this.Property(a => a.ImpressionDate).IsRequired();
            this.Property(a => a.QunatityPrinted).IsRequired();
            this.Property(a => a.BalanceQty).IsOptional();
            this.Property(a => a.ThroughAddendum).IsOptional().HasMaxLength(1);
            this.Property(a => a.PrevLicenseId).IsOptional();
            this.Property(a => a.PrevAddendumId).IsOptional();
            this.Property(a => a.PrevContactQty).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.Property(a => a.KitISBNId).IsOptional();

            this.HasOptional(a => a.tblPreviousLicense)
               .WithMany()
               .HasForeignKey(a => a.PrevLicenseId)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.tblPreviousAddendum)
               .WithMany()
               .HasForeignKey(a => a.PrevAddendumId)
               .WillCascadeOnDelete(false);


            this.HasOptional(a => a.tblPreviousAuthorContract)
               .WithMany()
               .HasForeignKey(a => a.ContractId)
               .WillCascadeOnDelete(false);


        }
    }
}
