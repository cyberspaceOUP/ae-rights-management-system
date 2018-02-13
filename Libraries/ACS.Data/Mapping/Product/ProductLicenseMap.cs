using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductLicenseMap : EntityTypeConfiguration<ProductLicense>
    {
        public ProductLicenseMap() {
            this.ToTable("ProductLicense");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductLicensecode).IsRequired().HasMaxLength(20);
            this.Property(a => a.productid).IsOptional();
            this.Property(a => a.publishingcompanyid).IsOptional();
            this.Property(a => a.ContactPerson).IsOptional().HasMaxLength(200);
            this.Property(a => a.Address).IsOptional().HasMaxLength(2000);
            this.Property(a => a.CountryId).IsOptional();
            this.Property(a => a.OtherCountry).IsOptional().HasMaxLength(100);
            this.Property(a => a.Stateid).IsOptional();
            this.Property(a => a.OtherState).IsOptional().HasMaxLength(100);
            this.Property(a => a.Cityid).IsOptional();
            this.Property(a => a.OtherCity).IsOptional().HasMaxLength(100);
            this.Property(a => a.Pincode).IsOptional().HasMaxLength(10);
            this.Property(a => a.Mobile).IsOptional().HasMaxLength(25);
            this.Property(a => a.Email).IsOptional().HasMaxLength(100);
            this.Property(a => a.Requestdate).IsRequired();
            this.Property(a => a.ContractDate).IsOptional();
            this.Property(a => a.effectivedate).IsOptional();
            this.Property(a => a.contractperiodinmonth).IsOptional();
            this.Property(a => a.Expirydate).IsOptional();
            this.Property(a => a.Territoryrightsid).IsRequired();
            this.Property(a => a.Impressionwithindate).IsOptional();
            this.Property(a => a.noofimpressions).IsOptional();
            this.Property(a => a.printquantitytype).IsRequired().HasMaxLength(20);
            this.Property(a => a.printquantity).IsOptional();
            this.Property(a => a.RoyalityTerms).IsRequired().HasMaxLength(25);
            this.Property(a => a.PaymentAmount).IsOptional().HasMaxLength(25);
            this.Property(a => a.AdvancedAmount).IsOptional().HasMaxLength(25);
            this.Property(a => a.copiesforlicensor).IsOptional();
            this.Property(a => a.pricetype).IsOptional().HasMaxLength(1);
            this.Property(a => a.Currencyid).IsOptional();
            this.Property(a => a.price).IsOptional();
            this.Property(a => a.thirdpartypermission).IsOptional().HasMaxLength(1);
            this.Property(a => a.Remarks).IsOptional().HasMaxLength(5000);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.DeactivateRemarks).IsOptional();
            this.Property(a => a.LicenseStatus).IsRequired().HasMaxLength(1);
            this.Property(a => a.balanceqtycf).IsRequired().HasMaxLength(1);
            this.Property(a => a.balanceqty).IsOptional();

            


            this.HasRequired(a => a.ProductLicenseProduct)
               .WithMany()
               .HasForeignKey(a => a.productid)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductLicenseCountry)
               .WithMany()
               .HasForeignKey(a => a.CountryId)
               .WillCascadeOnDelete(false);


            this.HasOptional(a => a.ProductLicenseState)
               .WithMany()
               .HasForeignKey(a => a.Stateid)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductLicenseCity)
               .WithMany()
               .HasForeignKey(a => a.Cityid)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.ProductLicenseTerritoryRights)
               .WithMany()
               .HasForeignKey(a => a.Territoryrightsid)
               .WillCascadeOnDelete(false);

            this.HasOptional(a => a.ProductLicenseCurrency)
               .WithMany()
               .HasForeignKey(a => a.Currencyid)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.LicensePublishing)
              .WithMany()
              .HasForeignKey(a => a.publishingcompanyid)
              .WillCascadeOnDelete(false);


        
        }

    }
}
