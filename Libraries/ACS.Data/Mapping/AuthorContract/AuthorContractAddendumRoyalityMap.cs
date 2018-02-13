using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractAddendumRoyalityMap : EntityTypeConfiguration<AuthorContractAddendumRoyality>
    {
        public AuthorContractAddendumRoyalityMap()
        {
            this.ToTable("AuthorContractAddendumRoyality");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductSubTypeId).IsRequired();
            this.Property(a => a.copiesfrom).IsRequired();
            this.Property(a => a.copiesto).IsRequired();
            this.Property(a => a.percentage).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.AuthorId).IsOptional();

            this.HasRequired(usl => usl.AuthorContractOriginal)
                      .WithMany()
                      .HasForeignKey(usl => usl.AuthorContractId)
                        .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.AuthorContractAddendumDetails)
                      .WithMany()
                      .HasForeignKey(usl => usl.AddendumDetailsId)
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
