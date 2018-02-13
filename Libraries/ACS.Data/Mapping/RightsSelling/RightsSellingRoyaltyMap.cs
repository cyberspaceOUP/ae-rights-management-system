//Create By Ankush 08/09/2016
using ACS.Core.Domain.RightsSelling;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.RightsSelling
{
    public partial class RightsSellingRoyaltyMap : EntityTypeConfiguration<RightsSellingRoyalty>
    {
        public RightsSellingRoyaltyMap()
        {
            this.ToTable("RightsSellingRoyalty");
            this.HasKey(a => a.Id);

            this.Property(a => a.ContractId).IsOptional();

            this.Property(a => a.ProductLicenseId).IsOptional();

            this.Property(a => a.subproducttypeid).IsOptional();

            this.Property(a => a.CopiesFrom).IsOptional();

            this.Property(a => a.CopiesTo).IsOptional();
            this.Property(a => a.Percentage).IsOptional();
            

            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.Property(a => a.RightsSellingID).IsOptional();



            this.HasOptional(a => a.RightsSellingMaster)
                .WithMany(a => a.RightsSellingRoyalty)
                .HasForeignKey(a => a.RightsSellingID)
                .WillCascadeOnDelete(false);





            this.HasOptional(a => a.ProductTypeMaster)
              .WithMany()
              .HasForeignKey(a => a.subproducttypeid)
              .WillCascadeOnDelete(false);


            this.HasOptional(a => a.AuthorContract)
             .WithMany()
             .HasForeignKey(a => a.ContractId)
             .WillCascadeOnDelete(false);


            this.HasOptional(a => a.Product)
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


        }
    }
}
