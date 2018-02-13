using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.AuthorContract;

namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractContributorMap : EntityTypeConfiguration<AuthorContractContributor>
    {
        public AuthorContractContributorMap()
        {
            this.ToTable("AuthorContractContributor");
            this.HasKey(a => a.Id);
            this.Property(a => a.AuthorContractId).IsRequired();
            this.Property(a => a.ContributorName).IsRequired().HasMaxLength(500);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            //this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            //this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            
            this.HasRequired(usl => usl.AuthorContractOriginal)
                           .WithMany(s => s.AuthorContactContibutor)
                           .HasForeignKey(usl => usl.AuthorContractId)
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
