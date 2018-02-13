//Create By Ankush 08/09/2016
using ACS.Core.Domain.RightsSelling;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.RightsSelling
{
    public partial class RightsSellingUpdateMap : EntityTypeConfiguration<RightsSellingUpdate>
    {
        public RightsSellingUpdateMap()
        {
            this.ToTable("RightsSellingUpdate");
            this.HasKey(a => a.Id);
            this.Property(a => a.ContractStatus).IsOptional().HasMaxLength(100);
            this.Property(a => a.Date_of_agreement).IsOptional();
            this.Property(a => a.Signed_Contract_sent_date).IsOptional();
            this.Property(a => a.Signed_Contract_receiveddate).IsOptional();
            this.Property(a => a.CancellationDate).IsOptional();
            this.Property(a => a.Cancellation_Reason).IsOptional().HasMaxLength(800);
            this.Property(a => a.Contributor_Agreement).IsOptional();
            this.Property(a => a.RightsSellingID).IsOptional();



           // this.Property(a => a.AgreementDate).IsOptional();
            this.Property(a => a.Effectivedate).IsOptional();
            this.Property(a => a.Contractperiodinmonth).IsOptional();
            this.Property(a => a.Expirydate).IsOptional();



            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasOptional(a => a.RightsSellingMaster)
               .WithMany()
               .HasForeignKey(a => a.RightsSellingID)
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
