using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorAgreementMap : EntityTypeConfiguration<AuthorContractAgreement>
    {
        public AuthorAgreementMap()
        {
            this.ToTable("AuthorContractAgreement");
            this.HasKey(a => a.Id);
            this.Property(a => a.ContractId).IsOptional();
            this.Property(a => a.dateofagreement).IsOptional();
            this.Property(a => a.SignedContractsentdate).IsOptional();
            this.Property(a => a.SignedContractreceived).IsOptional();
            this.Property(a => a.effectiveDate).IsOptional();
            this.Property(a => a.periodofagreement).IsOptional();
            this.Property(a => a.Expirydate).IsOptional();
            this.Property(a => a.Authorcopiessentdate).IsOptional();
            this.Property(a => a.Contributorcopiessentdate).IsOptional();
            this.Property(a => a.Cancellationdate).IsOptional();
            this.Property(a => a.ContributorRemarks).IsOptional();
            this.Property(a => a.Cancellationreason).IsOptional();
            this.Property(a => a.Remarks).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.HasOptional(usl => usl.AuthorContractOriginal)
                      .WithMany()
                      .HasForeignKey(usl => usl.ContractId)
                        .WillCascadeOnDelete(false);
            
            //this.HasOptional(usl => usl.AuthorContractOriginal.SeriesCode)
            //         .WithMany()
            //         .HasForeignKey(usl => usl.SeriesCode)
            //           .WillCascadeOnDelete(false);
           // this.HasOptional<AuthorContractOriginal>(a => a.SeriesCode).WithMany().HasForeignKey(a => a.SeriesCode);
            
            
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
    class AuthorContractDocumentMap : EntityTypeConfiguration<AuthorContractDocument>
    {
        public AuthorContractDocumentMap()
        {
            this.ToTable("AuthorContractDocument");
            this.HasKey(a => a.Id);
            this.Property(a => a.FileNameEntered).IsOptional();
            this.Property(a => a.AgreementId).IsRequired();
            this.Property(a => a.DocumentTypeId).IsRequired();
            this.Property(a => a.FileName).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.HasRequired(usl => usl.AuthorContractAgreement)
                      .WithMany(usl => usl.AuthorContractDocument)
                      .HasForeignKey(usl => usl.AgreementId)
                        .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.DocumentTypeMaster)
                     .WithMany()
                     .HasForeignKey(usl => usl.DocumentTypeId)
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
    class DocumentTypeMasterMap : EntityTypeConfiguration<DocumentTypeMaster>
    {
        public DocumentTypeMasterMap()
        {
            this.ToTable("DocumentTypeMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.DocumentTypeName).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
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
