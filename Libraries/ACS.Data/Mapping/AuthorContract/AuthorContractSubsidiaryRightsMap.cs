using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.AuthorContract;
using System.Data.Entity.ModelConfiguration;
namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractSubsidiaryRightsMap : EntityTypeConfiguration<AuthorContractSubsidiaryRights>
    {
        public AuthorContractSubsidiaryRightsMap()
        {
            this.ToTable(" AuthorContractSubsidiaryRights");
            this.HasKey(a => a.Id);
            this.Property(a => a.subsidiaryrightsid).IsRequired();
            this.Property(a => a.ouppercentage).IsRequired();
            this.Property(a => a.AuthorPercentage).IsRequired();
            this.Property(a => a.AuthorId).IsRequired();
            this.Property(a => a.AuthorContractid).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            //this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            //this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

        

            this.HasRequired(usl => usl.AuthorContractOriginal)
                         .WithMany(s=>s.AuthorContractSubsidiaryRights)
                         .HasForeignKey(usl => usl.AuthorContractid)
                           .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.AuthorMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.AuthorId)
                          .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.SubsidiaryRightsMaster)
                       .WithMany()
                       .HasForeignKey(usl => usl.subsidiaryrightsid)
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
