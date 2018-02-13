//Create By Saddam on 01/08/2016
using ACS.Core.Domain.PermissionsOutbound;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.PermissionsOutbound
{
    public partial class PermissionsOutboundMap : EntityTypeConfiguration<PermissionsOutboundMaster>
    {
        public PermissionsOutboundMap()
        {
            this.ToTable("PermissionsOutboundMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.PermissionsOutboundCode).IsOptional().HasMaxLength(400);
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
            this.Property(a => a.LicenseePublicationTitle).IsOptional().HasMaxLength(800); 
            this.Property(a => a.DateOfPermission).IsOptional();
            this.Property(a => a.PermissionPeriod).IsOptional();
            this.Property(a => a.DateExpiry).IsOptional();
            this.Property(a => a.RequestMaterial).IsOptional().HasMaxLength(800);
            this.Property(a => a.Will_be_material_be_translated).IsOptional().HasMaxLength(100);
            this.Property(a => a.Will_be_material_be_adepted).IsOptional().HasMaxLength(100);
            this.Property(a => a.LanguageId).IsOptional();
            this.Property(a => a.Extent).IsOptional().HasMaxLength(800);
            this.Property(a => a.TerritoryId).IsOptional();
            this.Property(a => a.DateOfInvoice).IsOptional();
            this.Property(a => a.InvoiceApplicable).IsOptional().HasMaxLength(100);
            this.Property(a => a.InvoiceNo).IsOptional().HasMaxLength(400);
            this.Property(a => a.InvoiceCurrency).IsOptional();
            this.Property(a => a.InvoiceValue).IsOptional().HasMaxLength(400);
            this.Property(a => a.InvoiceDescription).IsOptional().HasMaxLength(800);
            this.Property(a => a.Copies_To_Be_Received).IsOptional().HasMaxLength(100);
            this.Property(a => a.NumberOfCopies).IsOptional();
            this.Property(a => a.PaymentReceived).IsOptional().HasMaxLength(100);
        
            this.Property(a => a.Remarks).IsOptional().HasMaxLength(800);

            this.Property(a => a.Type).IsOptional().HasMaxLength(40);
            this.Property(a => a.ContactId);

            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(a => a.ProductMaster)
                .WithMany()
                .HasForeignKey(a => a.productid)
                .WillCascadeOnDelete(false);

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


       



           


            this.HasOptional(a => a.TerritoryRightsMaster)
              .WithMany()
              .HasForeignKey(a => a.TerritoryId)
              .WillCascadeOnDelete(false);


           


            this.HasOptional(a => a.Language)
          .WithMany()
          .HasForeignKey(a => a.LanguageId)
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
