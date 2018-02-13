//Create by Saddam on 29/04/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
   
    public partial class PublishingCompanyMasterMap : EntityTypeConfiguration<PublishingCompanyMaster>
    {
        public PublishingCompanyMasterMap()
        {
            this.ToTable("PublishingCompanyMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.PublishingCompanyCode).HasMaxLength(50);
            this.Property(a => a.CompanyName).IsRequired().HasMaxLength(200);
            this.Property(a => a.ContactPerson).IsRequired().HasMaxLength(200);
            this.Property(a => a.Address).IsRequired().HasMaxLength(2000);
            this.Property(a => a.CountryId);
            this.Property(a => a.OtherCountry).HasMaxLength(100);
            this.Property(a => a.Stateid);
            this.Property(a => a.OtherState).HasMaxLength(100);
            this.Property(a => a.Cityid);
            this.Property(a => a.OtherCity).HasMaxLength(100);
            this.Property(a => a.Pincode).HasMaxLength(10);
            this.Property(a => a.Phone).IsRequired().HasMaxLength(25);
            this.Property(a => a.Mobile).HasMaxLength(25);
            this.Property(a => a.Email).IsRequired().HasMaxLength(100);
            this.Property(a => a.Website).HasMaxLength(100);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.PublishingCompanyCountry)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.PublishingCompanyState)
                .WithMany()
                .HasForeignKey(a => a.Stateid)
                .WillCascadeOnDelete(false);

            this.HasRequired(a => a.PublishingCompanyCity)
                .WithMany()
                .HasForeignKey(a => a.Cityid)
                .WillCascadeOnDelete(false);

        }
    
    }
}
