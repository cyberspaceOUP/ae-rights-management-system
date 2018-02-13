//Create By Ankush 08/09/2016
using ACS.Core.Domain.RightsSelling;
using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.RightsSelling
{
    public partial class RightsSellingPaymentTaggingMap : EntityTypeConfiguration<RightsSellingPaymentTagging>
    {
       public RightsSellingPaymentTaggingMap()
        {
            this.ToTable("RightsSellingPaymentTagging");
            this.HasKey(a => a.Id);
            this.Property(a => a.ContractId).IsOptional();
            this.Property(a => a.ProductLicenseId).IsOptional();
            this.Property(a => a.subproducttypeid).IsOptional();
            this.Property(a => a.Percentage).IsOptional();
            this.Property(a => a.PaymentMode).IsOptional();
            this.Property(a => a.ChequeNumber).IsOptional();
            this.Property(a => a.ChequeDate).IsOptional();
            this.Property(a => a.BankName).IsOptional();
            this.Property(a => a.Amount).IsOptional();
            this.Property(a => a.AuthorAmount).IsOptional();
            this.Property(a => a.OupAmount).IsOptional();
           
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.Property(a => a.AuthorId).IsRequired();
            this.Property(a => a.RightsSellingMasterId).IsRequired();
            this.Property(a => a.PublishingCompanyId).IsRequired();

            this.Property(a => a.WithHoldingTax).IsOptional();
            this.Property(a => a.ConverisonRate).IsOptional();

            this.HasOptional(a => a.AuthorContract)
                         .WithMany()
                         .HasForeignKey(a => a.ContractId)
                         .WillCascadeOnDelete(false);


            this.HasOptional(a => a.ProductLicense)
                      .WithMany()
                      .HasForeignKey(a => a.ProductLicenseId)
                      .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.EnteredByForeignKey)
                      .WithMany()
                      .HasForeignKey(usl => usl.EnteredBy)
                        .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.DeactivateByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.DeactivateBy)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.ModifiedByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.ModifiedBy)
                          .WillCascadeOnDelete(false);

            //this.HasRequired(usl => usl.AuthorMaster)
            //           .WithMany()
            //           .HasForeignKey(usl => usl.AuthorId)
            //             .WillCascadeOnDelete(false);

            //this.HasOptional(usl => usl.RightsSellingMaster)
            //            .WithMany()
            //            .HasForeignKey(usl => usl.RightsSellingMasterId)
            //              .WillCascadeOnDelete(false);

        }

    }
}
