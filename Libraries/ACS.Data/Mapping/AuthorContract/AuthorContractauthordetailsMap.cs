using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    class AuthorContractauthordetailsMap : EntityTypeConfiguration<AuthorContractauthordetails>
    {
        public AuthorContractauthordetailsMap()
        {
            this.ToTable(" AuthorContractauthordetails");
            this.HasKey(a => a.Id);
            this.Property(a => a.AuthorContractid).IsRequired();
            this.Property(a => a.AuthorId).IsRequired();
            this.Property(a => a.paymentperiodid).IsOptional();
            this.Property(a => a.AuthorCopies).IsOptional();
            this.Property(a => a.Seedmoney).IsOptional();
            this.Property(a => a.onetimepayment).IsOptional();
            this.Property(a => a.advanceroyality).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            //this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            //this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasOptional(usl => usl.PaymentPeriod)
                        .WithMany()
                        .HasForeignKey(usl => usl.paymentperiodid)
                          .WillCascadeOnDelete(false);
            
            this.HasRequired(usl => usl.AuthorContractOriginal)
                         .WithMany(s => s.AuthorContractauthordetails)
                         .HasForeignKey(usl => usl.AuthorContractid)
                           .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.AuthorMaster)
                        .WithMany()
                        .HasForeignKey(usl => usl.AuthorId)
                          .WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.ContractMaster)
                     .WithMany()
                     .HasForeignKey(usl => usl.ContractTypeId)
                       .WillCascadeOnDelete(false);
            

            this.HasRequired(usl => usl.EnteredByForeignKey)
                       .WithMany()
                       .HasForeignKey(usl => usl.EnteredBy)
                         .WillCascadeOnDelete(false);
            this.HasRequired(usl => usl.AuthorTypeMaster)
                       .WithMany()
                       .HasForeignKey(usl => usl.Authortype)
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
