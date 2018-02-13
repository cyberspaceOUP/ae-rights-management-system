using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractmaterialdetailsMap : EntityTypeConfiguration<AuthorContractmaterialdetails>
    {
        public AuthorContractmaterialdetailsMap()
        {
            this.ToTable("AuthorContractmaterialdetails");
            this.HasKey(a => a.Id);
            this.Property(a => a.AuthorContractId).IsRequired();
            this.Property(a => a.MaterialId).IsRequired();
            
            this.HasRequired(usl=>usl.AuthorContractOriginal)
                .WithMany(s=>s.AuthorContractmaterialdetails)
                .HasForeignKey(usl=>usl.AuthorContractId)
                .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.SupplyMaterialMaster)
                         .WithMany()
                         .HasForeignKey(usl => usl.MaterialId)
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
