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
   public partial class RightsSellingMap : EntityTypeConfiguration<RightsSellingMaster>
    {
       public RightsSellingMap()
        {
            this.ToTable("RightsSellingMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.RightsSellingCode).IsOptional().HasMaxLength(400);
            this.Property(a => a.LicenseeID).IsOptional();
            this.Property(a => a.Licenseecode).IsOptional().HasMaxLength(400);
            this.Property(a => a.OrganizationName).IsOptional().HasMaxLength(800);
            this.Property(a => a.ContactPerson).IsOptional().HasMaxLength(800);
            this.Property(a => a.Address).IsOptional().HasMaxLength(2000);
            this.Property(a => a.CountryId).IsOptional();
            this.Property(a => a.OtherCountry).IsOptional().HasMaxLength(800);
            this.Property(a => a.Stateid).IsOptional();
            this.Property(a => a.OtherState).IsOptional().HasMaxLength(800);
            this.Property(a => a.Cityid).IsOptional();
            this.Property(a => a.OtherCity).IsOptional().HasMaxLength(400);
            this.Property(a => a.Pincode).IsOptional().HasMaxLength(100);
            this.Property(a => a.Mobile).IsOptional().HasMaxLength(100);
            this.Property(a => a.Email).IsOptional().HasMaxLength(100);
            this.Property(a => a.URL).IsOptional().HasMaxLength(100);
            this.Property(a => a.RequestDate).IsOptional();
            this.Property(a => a.DateContract).IsOptional();
            this.Property(a => a.ContractPeriod).IsOptional();
            this.Property(a => a.First_Impression_within_date).IsOptional();
            this.Property(a => a.DateExpiry).IsOptional();
            this.Property(a => a.Contract_Effective_Date).IsOptional();
            this.Property(a => a.ProductCategory).IsOptional();
            this.Property(a => a.Will_be_material_be_translated).IsOptional().HasMaxLength(400);
            this.Property(a => a.Language).IsOptional();
            this.Property(a => a.Print_Run_Quantity_Allowed).IsOptional().HasMaxLength(400);
            this.Property(a => a.Number_of_Impression_Allowed).IsOptional();
            this.Property(a => a.Advance_Payment).IsOptional().HasMaxLength(400);
            this.Property(a => a.Currency).IsOptional();
            this.Property(a => a.Payment_Term).IsOptional().HasMaxLength(400);
            this.Property(a => a.Payment_Amount).IsOptional().HasMaxLength(400);
            this.Property(a => a.Territory_Rights).IsOptional();
            this.Property(a => a.Advance_Royalty_Amount).IsOptional().HasMaxLength(400);
            this.Property(a => a.Royalty_Recurring).IsOptional().HasMaxLength(400);
            this.Property(a => a.Recurring_From_Period).IsOptional();
            this.Property(a => a.Recurring_To_Period).IsOptional();
            this.Property(a => a.ContractId).IsOptional();
            this.Property(a => a.ProductLicenseId).IsOptional();
            this.Property(a => a.Status).IsOptional().HasMaxLength(100);
            this.Property(a => a.Remarks).IsOptional().HasMaxLength(800);

            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.Property(a => a.Print_Run_Quantity_Type).IsOptional().HasMaxLength(100);
            this.Property(a => a.FirstPublicationDate).IsOptional();


            this.HasOptional(a => a.Country)
                 .WithMany()
                 .HasForeignKey(a => a.CountryId)
                 .WillCascadeOnDelete(false);

            this.HasOptional(a => a.State)
                .WithMany()
                .HasForeignKey(a => a.Stateid)
                .WillCascadeOnDelete(false);

            this.HasOptional(a => a.City)
                .WithMany()
                .HasForeignKey(a => a.Cityid)
                .WillCascadeOnDelete(false);

            this.HasOptional(a => a.Licensee)
                 .WithMany()
                 .HasForeignKey(a => a.LicenseeID)
                 .WillCascadeOnDelete(false);


            this.HasOptional(a => a.ProductCategoryRightMaster)
               .WithMany()
               .HasForeignKey(a => a.ProductCategory)
               .WillCascadeOnDelete(false);



            this.HasOptional(a => a.CurrencyMaster)
               .WithMany()
               .HasForeignKey(a => a.Currency)
               .WillCascadeOnDelete(false);


            this.HasOptional(a => a.TerritoryRightsMaster)
              .WithMany()
              .HasForeignKey(a => a.Territory_Rights)
              .WillCascadeOnDelete(false);


            this.HasOptional(a => a.AuthorContract)
             .WithMany()
             .HasForeignKey(a => a.ContractId)
             .WillCascadeOnDelete(false);


            this.HasOptional(a => a.ProductLicense)
          .WithMany()
          .HasForeignKey(a => a.ProductLicenseId)
          .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductMaster)
           .WithMany()
           .HasForeignKey(a => a.ProuductId)
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
