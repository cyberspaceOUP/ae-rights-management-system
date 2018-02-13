using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractOriginalMap :EntityTypeConfiguration<AuthorContractOriginal>
    {
        public AuthorContractOriginalMap()
        {
            this.ToTable("AuthorContract");
            this.HasKey(a => a.Id);
            this.Property(a => a.AuthorContractCode).IsRequired().HasMaxLength(50);
            this.Property(a => a.ExecutiveCode).IsRequired();
            this.Property(a => a.ContractEntryDate).IsRequired();
            this.Property(a => a.ContractDate).IsRequired();
            this.Property(a => a.NoOfAuthors).IsRequired();
            this.Property(a => a.contractperiodinmonth).IsRequired();
            this.Property(a => a.ContractExpiryDate).IsOptional();
            this.Property(a => a.BuyBack).IsOptional().HasMaxLength(3);
            this.Property(a => a.NatureOfWork).IsOptional();
            this.Property(a => a.CopyrightOwner).IsRequired();
            this.Property(a => a.thirdpartypermission).IsOptional().HasMaxLength(3);
            this.Property(a => a.Amendment).IsRequired();
            this.Property(a => a.AmendmentRemarks).IsOptional();
            this.Property(a => a.Restriction).IsOptional().HasMaxLength(100);
            this.Property(a => a.subjectMatterAndTreatment).IsOptional().HasMaxLength(5000);
            this.Property(a => a.MinNoOfwords).IsOptional();
            this.Property(a => a.MaxNoOfwords).IsOptional();
            this.Property(a => a.MinNoOfPages).IsOptional();
            this.Property(a => a.MaxNoOfPages).IsOptional();
            this.Property(a => a.PriceType).IsOptional().HasMaxLength(15);
            this.Property(a => a.Price).IsOptional();
            this.Property(a => a.MediumOfdelivery).IsOptional();
            this.Property(a => a.ManuscriptId).IsOptional();
            this.Property(a => a.Deliveryschedule).IsOptional();
            this.Property(a => a.ProductRemarks).IsOptional();
            this.Property(a => a.TermsOfCopyright).IsRequired();
            this.Property(a => a.SeriesCode).IsOptional();
            this.Property(a => a.Status).IsRequired();
            

            this.HasRequired(usl => usl.ProductMaster)
                         .WithMany()
                         .HasForeignKey(usl => usl.ProductId)
                           .WillCascadeOnDelete(false);
            
            this.HasRequired(usl => usl.TerritoryRightsMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.Territoryrightsid)
                          .WillCascadeOnDelete(false);

           this.HasOptional(usl => usl.SeriesMaster)
                       .WithMany()
                       .HasForeignKey(usl => usl.SeriesId)
                         .WillCascadeOnDelete(false);
            
            this.HasOptional(usl => usl.ManuscriptDeliveryFormatMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.ManuscriptId)
                        .WillCascadeOnDelete(false);
            
            this.HasOptional(usl => usl.CurrencyMaster)
                      .WithMany()
                      .HasForeignKey(usl => usl.CurrencyId)
                        .WillCascadeOnDelete(false);
           
           
            this.HasRequired(usl => usl.EnteredByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.EnteredBy)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.DeactivateByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.DeactivateBy)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.ProductLicense)
                        .WithMany()
                        .HasForeignKey(usl => usl.LicenseId)
                          .WillCascadeOnDelete(false);

            this.HasOptional(usl => usl.ModifiedByForeignKey)
                        .WithMany()
                        .HasForeignKey(usl => usl.ModifiedBy)
                          .WillCascadeOnDelete(false);
        }
    }
    class AuthorContractMenuscriptDeliveryLinkMap : EntityTypeConfiguration<AuthorContractMenuscriptDeliveryLink>
    {
        public AuthorContractMenuscriptDeliveryLinkMap()
        {
            this.ToTable("AuthorContractMenuscriptDeliveryLink");
            this.HasKey(a => a.Id);
            
            this.HasRequired(usl => usl.AuthorContractOriginal)
                           .WithMany(s => s.AuthorContractMenuscriptDeliveryLink)
                           .HasForeignKey(usl => usl.AuthorContractId)
                             .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.ManuscriptDeliveryFormatMaster)
                           .WithMany()
                           .HasForeignKey(usl => usl.ManuscriptId)
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
