using ACS.Core.Domain.PermissionInbound;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.PermissionInboundMappings
{
    public partial class OtherRightsMasterMap : EntityTypeConfiguration<OtherRightsMaster>
    {
        public OtherRightsMasterMap()
        {
            this.ToTable("OthersRightsMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.RightsName).IsRequired().HasMaxLength(100);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
        }
    }
    public class PermissionInboundMap : EntityTypeConfiguration<PermissionInbound>
    {
        public PermissionInboundMap()
        {
            this.ToTable("PermissionInbound");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductId).IsRequired();
            this.Property(a => a.TypeFor).IsRequired().HasMaxLength(1);
            this.Property(a => a.Code).IsRequired().HasMaxLength(10);
            this.Property(a => a.ProductLicenseId).IsOptional();
            this.Property(a => a.AuthorContractId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(usl => usl.ProductMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.ProductId)
                          .WillCascadeOnDelete(false);
            this.HasOptional(usl => usl.AuthorContractOriginal)
                       .WithMany()
                       .HasForeignKey(usl => usl.AuthorContractId)
                         .WillCascadeOnDelete(false);
            this.HasOptional(usl => usl.ProductLicense)
                      .WithMany()
                      .HasForeignKey(usl => usl.ProductLicenseId)
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
    public class PermissionInboundImageVideoBankMap : EntityTypeConfiguration<PermissionInboundImageVideoBank>
    {
        public PermissionInboundImageVideoBankMap()
        {
            this.ToTable("PermissionInboundImageVideoBankLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.PermissionInboundId).IsRequired();
            this.Property(a => a.ImageBankId).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.Property(a => a.ImageBankDataId).IsOptional();


            this.HasRequired(usl => usl.PermissionInbound)
                        .WithMany()
                        .HasForeignKey(usl => usl.PermissionInboundId)
                          .WillCascadeOnDelete(false);


            this.HasRequired(usl => usl.OtherContractImageBank)
                       .WithMany()
                       .HasForeignKey(usl => usl.ImageBankId)
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
    public class PermissionInboundImageVideoBankDataMap : EntityTypeConfiguration<PermissionInboundImageVideoBankData>
    {
        public PermissionInboundImageVideoBankDataMap()
        {
            this.ToTable("PermissionInboundImageVideoBankData");
            this.HasKey(a => a.Id);
            this.Property(a => a.IVBId).IsRequired();
            this.Property(a => a.ContractTypes).IsOptional();
            this.Property(a => a.imagevideobankid).IsOptional();
            this.Property(a => a.Description).IsOptional();
            this.Property(a => a.invoiceno).IsOptional();
            this.Property(a => a.invoicevalue).IsOptional();
            this.Property(a => a.invoicedate).IsOptional();
            this.Property(a => a.printquantity).IsOptional();
            this.Property(a => a.permissionexpirydate).IsOptional();
            this.Property(a => a.weblink).IsOptional();
            this.Property(a => a.creditlines).IsOptional();
            this.Property(a => a.EditorialonlyType).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.CurrencyId).IsOptional();

            this.Property(a => a.usage).IsOptional().HasMaxLength(4000);

            this.Property(a => a.ImageBankPartyId).IsOptional();

            this.HasRequired(usl => usl.PermissionInboundImageVideoBank)
                        .WithMany()
                        .HasForeignKey(usl => usl.IVBId)
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
    public class PermissionInboundOthersMap : EntityTypeConfiguration<PermissionInboundOthers>
    {
        public PermissionInboundOthersMap()
        {
            this.ToTable("PermissionInboundOthers");
            this.HasKey(a => a.Id);
            this.Property(a => a.PermissionInboundId).IsRequired();
            this.Property(a => a.AssetSubTypeId).IsOptional();
            this.Property(a => a.AssetDescription).IsOptional();
            this.Property(a => a.Noofcopy).IsOptional();
            this.Property(a => a.statusId).IsRequired();
            this.Property(a => a.Restriction).IsOptional();
            this.Property(a => a.SubLicensing).IsOptional();
            this.Property(a => a.Fee).IsOptional();
            this.Property(a => a.CurrencyId).IsOptional();
            this.Property(a => a.Extent).IsOptional();
            this.Property(a => a.OriginalSource).IsOptional();
            this.Property(a => a.InvoiceNumber).IsOptional();
            this.Property(a => a.Invoicevalue).IsOptional();
            this.Property(a => a.PermissionExpirydate).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.Acknowledgementline).IsOptional();

            this.Property(a => a.InboundRemarks).IsOptional();

            this.HasRequired(usl => usl.PermissionInbound)
                        .WithMany()
                        .HasForeignKey(usl => usl.PermissionInboundId)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.CurrencyMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.CurrencyId)
                          .WillCascadeOnDelete(false);
            this.HasOptional(usl => usl.AssetSubType)
                       .WithMany()
                       .HasForeignKey(usl => usl.AssetSubTypeId)
                         .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.StatusMaster)
                      .WithMany()
                      .HasForeignKey(usl => usl.statusId)
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
    public class PermissionInboundOthersRightsLinkMap : EntityTypeConfiguration<PermissionInboundOthersRightsLink>
    {
        public PermissionInboundOthersRightsLinkMap()
        {
            this.ToTable("PermissionInboundOthersRightsLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.PIOID).IsRequired();
            this.Property(a => a.RightsId).IsOptional();
            this.Property(a => a.status).IsOptional();
            this.Property(a => a.RunGranted).IsOptional();
            this.Property(a => a.Number).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.PermissionInboundOthers)
                        .WithMany()
                        .HasForeignKey(usl => usl.PIOID)
                          .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.OtherRightsMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.RightsId)
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
    public class OtherContractDateRequestMap : EntityTypeConfiguration<OtherContractDateRequest>
    {
        public OtherContractDateRequestMap()
        {
            this.ToTable("OtherContractDateRequest");
            this.HasKey(a => a.Id);
            this.Property(a => a.PIOID).IsRequired();
            this.Property(a => a.dateOf).IsOptional();
            this.Property(a => a.dateValue).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.PermissionInboundOthers)
                        .WithMany()
                        .HasForeignKey(usl => usl.PIOID)
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
    public partial class PermissionInboundCopyRightHolderMasterMap : EntityTypeConfiguration<PermissionInboundCopyRightHolderMaster>
    {
        public PermissionInboundCopyRightHolderMasterMap()
        {
            this.ToTable("PermissionInboundCopyRightHolderMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.CopyRightHolderCode).HasMaxLength(50);
            this.Property(a => a.CopyRightHolderName).IsRequired().HasMaxLength(200);
            this.Property(a => a.ContactPerson).IsRequired().HasMaxLength(200);
            this.Property(a => a.Address).IsRequired().HasMaxLength(2000);
            this.Property(a => a.CountryId);
            this.Property(a => a.OtherCountry).HasMaxLength(100);
            this.Property(a => a.Stateid);
            this.Property(a => a.OtherState).HasMaxLength(100);
            this.Property(a => a.Cityid);
            this.Property(a => a.OtherState).HasMaxLength(100);
            this.Property(a => a.Pincode).HasMaxLength(10);
            this.Property(a => a.Mobile).HasMaxLength(25);
            this.Property(a => a.Email).IsRequired().HasMaxLength(100);
            this.Property(a => a.URL).HasMaxLength(100);
            this.Property(a => a.BankName).HasMaxLength(200);
            this.Property(a => a.AccountNo).HasMaxLength(100);
            this.Property(a => a.BankAddress).HasMaxLength(500);
            this.Property(a => a.IFSCCode).HasMaxLength(100);
            this.Property(a => a.PANNo).HasMaxLength(20);
            this.Property(a => a.VendorCOde).HasMaxLength(50);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.HasRequired(a => a.PermissionInboundOthers)
              .WithMany()
              .HasForeignKey(a => a.InboundOthersId)
              .WillCascadeOnDelete(false);
        }

    }
    public partial class OtherContractImageBankInboundMap : EntityTypeConfiguration<OtherContractImageBankInbound>
    {
        public OtherContractImageBankInboundMap()
        {
            this.ToTable("OtherContractImageBankInbound");
            this.HasKey(a => a.Id);

            this.Property(a => a.Printrunquantity).IsOptional();
            this.Property(a => a.PrintRights).IsRequired().HasMaxLength(4);
            this.Property(a => a.electronicrights).IsOptional().HasMaxLength(4);
            this.Property(a => a.ebookrights).IsOptional().HasMaxLength(4);
            this.Property(a => a.restriction).IsOptional().HasMaxLength(500);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();



        }
    }
    public partial class VideoImageBankInboundMap : EntityTypeConfiguration<VideoImageBankInbound>
    {
        public VideoImageBankInboundMap()
        {
            this.ToTable("VideoImageBankInbound");
            this.HasKey(a => a.Id);

            this.Property(a => a.Fullname).IsRequired().HasMaxLength(100);
            this.Property(a => a.Type).IsRequired().HasMaxLength(1); ;
            this.Property(a => a.ShortName).IsRequired().HasMaxLength(2);
            this.Property(a => a.ShortName).IsRequired().HasMaxLength(2);
            this.Property(a => a.CurrencyId).IsRequired();
            this.Property(a => a.Cost).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.OtherContractImageBankInbound)
               .WithMany()
               .HasForeignKey(a => a.ImageBankId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.CurrencyMaster)
               .WithMany()
               .HasForeignKey(a => a.CurrencyId)
               .WillCascadeOnDelete(false);

        }
    }
    
    public partial class PermissionInboundSearchHistoryMap : EntityTypeConfiguration<PermissionInboundSearchHistory>
    {
        public PermissionInboundSearchHistoryMap()
        {
            this.ToTable("PermissionInboundSearchHistory");
            this.HasKey(a => a.Id);
        }
    }



    public partial class PermissionInboundUpdateMap : EntityTypeConfiguration<PermissionInboundUpdate>
    {
        public PermissionInboundUpdateMap()
        {
            this.ToTable("PermissionInboundUpdate");
            this.HasKey(a => a.Id);
            this.Property(a => a.Contractstatus).IsOptional().HasMaxLength(20);
            this.Property(a => a.SignedContractSentDate).IsOptional();
            this.Property(a => a.SignedContractReceived_Date).IsOptional();

            this.Property(a => a.CancellationDate).IsOptional();

            this.Property(a => a.Cancellation_Reason).IsOptional().HasMaxLength(1600);

            this.Property(a => a.PermissionInboundId).IsOptional();

       

            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.Remarks).IsOptional();


            this.Property(a => a.AgreementDate).IsOptional();
            this.Property(a => a.Effectivedate).IsOptional();
            this.Property(a => a.Contractperiodinmonth).IsOptional();
            this.Property(a => a.Expirydate).IsOptional();


            this.HasRequired(a => a.PermissionInbound)
                .WithMany()
                .HasForeignKey(a => a.PermissionInboundId)
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


    public partial class PermissionInboundDocumentsMap : EntityTypeConfiguration<PermissionInboundDocuments>
    {
        public PermissionInboundDocumentsMap()
        {
            this.ToTable("PermissionInboundDocuments");
            this.HasKey(a => a.Id);
            this.Property(a => a.Documentname).IsOptional().HasMaxLength(400);
            this.Property(a => a.DocumentFile).IsOptional().HasMaxLength(400);
            this.Property(a => a.PermissionsInboundUpdateId).IsOptional();


            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(usl => usl.PermissionInboundUpdate)
               .WithMany(s => s.PermissionInboundDocuments)
               .HasForeignKey(usl => usl.PermissionsInboundUpdateId)
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
