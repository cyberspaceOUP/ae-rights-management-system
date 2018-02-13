using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractAddendumDetailsMap : EntityTypeConfiguration<AuthorContractAddendumDetails>
    {
        public AuthorContractAddendumDetailsMap()
        {
            this.ToTable("AuthorContractAddendumDetails");
            this.HasKey(a => a.Id);
            this.Property(a => a.AddendumDate).IsRequired();
            this.Property(a => a.AddendumType).IsRequired().HasMaxLength(1);
            this.Property(a => a.Periodofagreement).IsOptional();
            this.Property(a => a.SameAsEntery).IsOptional();
            this.Property(a => a.SeriesCode).IsOptional();
            this.Property(a => a.AuthorContractId).IsOptional();
            this.Property(a => a.ExpiryDate).IsOptional();
            this.Property(a => a.Remarks).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.AddendumCode).IsRequired();


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
