//Create by Saddam on 09/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class AuthorMasterMap : EntityTypeConfiguration<AuthorMaster>
    {
        public AuthorMasterMap()
         {
             this.ToTable("AuthorMaster");
             this.HasKey(a => a.Id);
             this.Property(a => a.AuthorCode).HasMaxLength(50);
             this.Property(a => a.Type).IsRequired().HasMaxLength(50);
             this.Property(a => a.FirstName).HasMaxLength(100);
             this.Property(a => a.LastName).IsOptional().HasMaxLength(100);
             this.Property(a => a.Address).IsRequired().HasMaxLength(2000);
             this.Property(a => a.ResidencyStatus).HasMaxLength(50);
             this.Property(a => a.CountryId).IsRequired();
             this.Property(a => a.OtherCountry).HasMaxLength(100);
             this.Property(a => a.StateId).IsRequired();
             this.Property(a => a.OtherState).HasMaxLength(100);
             this.Property(a => a.CityId).IsRequired();
             this.Property(a => a.OtherCity).HasMaxLength(100);
             this.Property(a => a.PinCode).IsRequired().HasMaxLength(50);
             this.Property(a => a.Email).IsRequired().HasMaxLength(100);
             this.Property(a => a.Phone).HasMaxLength(20);
             this.Property(a => a.Mobile).IsOptional().HasMaxLength(20);
             this.Property(a => a.Fax).HasMaxLength(100);
             this.Property(a => a.PANNo).IsOptional().HasMaxLength(40);
             this.Property(a => a.AdharCardNo).HasMaxLength(20);
             this.Property(a => a.DateOfBirth).IsOptional();
             this.Property(a => a.DeathDate).IsOptional();
             this.Property(a => a.AccountNo).HasMaxLength(50);
             this.Property(a => a.BankName).HasMaxLength(100);
             this.Property(a => a.BranchName).HasMaxLength(100);
             this.Property(a => a.IFSECode).HasMaxLength(20);
             this.Property(a => a.InstituteCompanyName).HasMaxLength(100);
             this.Property(a => a.AffiliationDesignation).HasMaxLength(100);
             this.Property(a => a.AffiliationDepartment).IsOptional().HasMaxLength(400);;
             this.Property(a => a.AffiliationAddress).HasMaxLength(2000);
             this.Property(a => a.AffiliationCountryId).IsOptional();
             this.Property(a => a.AffiliationOtherCountry).HasMaxLength(100);
             this.Property(a => a.AffiliationStateId).IsOptional();
             this.Property(a => a.AffiliationOtherState).HasMaxLength(100);
             this.Property(a => a.AffiliationCityId).IsOptional();
             this.Property(a => a.AffiliationOtherCity).HasMaxLength(100);
             this.Property(a => a.AffiliationPinCode).HasMaxLength(50);
             this.Property(a => a.AffiliationPhone).HasMaxLength(20);
             this.Property(a => a.AffiliationEmail).HasMaxLength(100);
             this.Property(a => a.AffiliationWebSite).HasMaxLength(100);
             this.Property(a => a.BeneficiaryName).IsRequired().HasMaxLength(200);
             this.Property(a => a.BeneficiaryRelation).IsOptional().HasMaxLength(100);
             this.Property(a => a.BeneficiaryAddress).IsRequired().HasMaxLength(2000);
             this.Property(a => a.BeneficiaryCountryId).IsRequired();
             this.Property(a => a.BeneficiaryOtherCountry).HasMaxLength(100);
             this.Property(a => a.BeneficiaryStateId).IsRequired();
             this.Property(a => a.BeneficiaryOtherState);
             this.Property(a => a.BeneficiaryCityId).IsRequired();
             this.Property(a => a.BeneficiaryOtherCity);
             this.Property(a => a.BeneficiaryPinCode).HasMaxLength(50);
             this.Property(a => a.BeneficiaryEmail).IsRequired().HasMaxLength(100);
             this.Property(a => a.BeneficiaryPhone).HasMaxLength(20);
             this.Property(a => a.BeneficiaryMobile).IsOptional().HasMaxLength(20);
             this.Property(a => a.BeneficiaryFax).HasMaxLength(100);
             this.Property(a => a.BeneficiaryPanNo).HasMaxLength(40);
             this.Property(a => a.BeneficiaryAccountNo).HasMaxLength(20);
             this.Property(a => a.BeneficiaryBankName).HasMaxLength(100);
             this.Property(a => a.BeneficiaryBranchName).HasMaxLength(100);
             this.Property(a => a.BeneficiaryIFSECode).HasMaxLength(20);

             this.Property(a => a.NomineeName).IsOptional().HasMaxLength(200);
             this.Property(a => a.NomineeRelation).IsOptional().HasMaxLength(100);
             this.Property(a => a.NomineeAddress).IsOptional().HasMaxLength(2000);
             this.Property(a => a.NomineeCountryId).IsOptional();
             this.Property(a => a.NomineeOtherCountry).HasMaxLength(100);
             this.Property(a => a.NomineeStateId).IsOptional();
             this.Property(a => a.NomineeOtherState);
             this.Property(a => a.NomineeCityId).IsOptional();
             this.Property(a => a.NomineeOtherCity);
             this.Property(a => a.NomineePinCode).HasMaxLength(50);
             this.Property(a => a.NomineeEmail).IsOptional().HasMaxLength(100);
             this.Property(a => a.NomineePhone).HasMaxLength(20);
             this.Property(a => a.NomineeMobile).IsOptional().HasMaxLength(20);
             this.Property(a => a.NomineeFax).HasMaxLength(100);
             this.Property(a => a.NomineePanNo).HasMaxLength(40);


             this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
             this.Property(a => a.EnteredBy).IsRequired();
             this.Property(a => a.EntryDate).IsRequired();
             this.Property(a => a.ModifiedBy).IsOptional();
             this.Property(a => a.ModifiedDate).IsOptional();
             this.Property(a => a.DeactivateBy).IsOptional();
             this.Property(a => a.DeactivateDate).IsOptional();
             this.Property(a => a.Remark).IsOptional().HasMaxLength(2000);

             this.Property(a => a.NomineeAccountNo).IsOptional().HasMaxLength(100);
             this.Property(a => a.NomineeBranchName).IsOptional().HasMaxLength(300);
             this.Property(a => a.NomineeBankName).IsOptional().HasMaxLength(300);
             this.Property(a => a.NomineeIFSECode).IsOptional().HasMaxLength(100);

             this.Property(a => a.AuthorSAPCode).HasMaxLength(20);

             this.HasRequired(a => a.AuthorDetCompanyCountry)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .WillCascadeOnDelete(false);

             this.HasRequired(a => a.AuthorDetCompanyState)
                .WithMany()
                .HasForeignKey(a => a.StateId)
                .WillCascadeOnDelete(false);

             this.HasRequired(a => a.AuthorDetCompanyCity)
                .WithMany()
                .HasForeignKey(a => a.CityId)
                .WillCascadeOnDelete(false);



             this.HasOptional(a => a.AuthorAffCompanyCountry)
                .WithMany()
                .HasForeignKey(a => a.AffiliationCountryId)
                .WillCascadeOnDelete(false);

             this.HasOptional(a => a.AuthorAffCompanyState)
                .WithMany()
                .HasForeignKey(a => a.AffiliationStateId)
                .WillCascadeOnDelete(false);

             this.HasOptional(a => a.AuthorAffCompanyCity)
                .WithMany()
                .HasForeignKey(a => a.AffiliationCityId)
                .WillCascadeOnDelete(false);


             this.HasRequired(a => a.AuthorBenfiCompanyCountry)
             .WithMany()
             .HasForeignKey(a => a.BeneficiaryCountryId)
             .WillCascadeOnDelete(false);

             this.HasRequired(a => a.AuthorBenfiCompanyState)
                .WithMany()
                .HasForeignKey(a => a.BeneficiaryStateId)
                .WillCascadeOnDelete(false);

             this.HasRequired(a => a.AuthorBenfiCompanyCity)
                .WithMany()
                .HasForeignKey(a => a.BeneficiaryCityId)
                .WillCascadeOnDelete(false);


             this.HasOptional(a => a.AuthorCompanyCountry)
            .WithMany()
            .HasForeignKey(a => a.NomineeCountryId)
            .WillCascadeOnDelete(false);

             this.HasOptional(a => a.AuthorCompanyState)
                .WithMany()
                .HasForeignKey(a => a.NomineeStateId)
                .WillCascadeOnDelete(false);

             this.HasOptional(a => a.AuthorCompanyCity)
                .WithMany()
                .HasForeignKey(a => a.NomineeCityId)
                .WillCascadeOnDelete(false);

             //this.HasOptional(a => a.AuthorDepartment)
             // .WithMany()
             // .HasForeignKey(a => a.AffiliationDepartment)
             // .WillCascadeOnDelete(false);


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
