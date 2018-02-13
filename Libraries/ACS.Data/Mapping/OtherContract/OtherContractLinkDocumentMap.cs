//Create by Saddam on 14/06/2016
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
   

    public partial class OtherContractLinkDocumentMap : EntityTypeConfiguration<OtherContractLinkDocument>
    {

        public OtherContractLinkDocumentMap()
        {
            this.ToTable("OtherContractLinkDocument");
            this.HasKey(a => a.Id);

            this.Property(a => a.othercontractLinkid).IsOptional();
            this.Property(a => a.DocumentnameLink).IsOptional().HasMaxLength(500);
            this.Property(a => a.documentfileLink).IsOptional().HasMaxLength(500);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(usl => usl.OtherContractLink)
                  .WithMany(s => s.OtherContractLinkDocument)
                  .HasForeignKey(usl => usl.othercontractLinkid)
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
