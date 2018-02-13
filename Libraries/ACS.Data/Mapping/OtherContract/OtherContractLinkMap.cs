//Create by Saddam on 16/06/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.OtherContract;

namespace ACS.Data.Mapping.OtherContract
{
    public partial class OtherContractLinkMap : EntityTypeConfiguration<OtherContractLink>
    {
        public OtherContractLinkMap()
          {
              this.ToTable("OtherContractLink");
              this.HasKey(a => a.Id);

              this.Property(a => a.Contractstatus).IsOptional().HasMaxLength(20);
              this.Property(a => a.SignedContractSentDate).IsOptional();
              this.Property(a => a.SignedContractReceived_Date).IsOptional();

              this.Property(a => a.CancellationDate).IsOptional();

              this.Property(a => a.Cancellation_Reason).IsOptional().HasMaxLength(1600);

              this.Property(a => a.othercontractid).IsOptional();

              this.Property(a => a.Status).IsOptional().HasMaxLength(40);

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


              this.HasRequired(a => a.OtherContractMaster)
                  .WithMany()
                  .HasForeignKey(a => a.othercontractid)
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
