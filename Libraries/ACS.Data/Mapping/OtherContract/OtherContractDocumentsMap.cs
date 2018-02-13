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
    public partial class OtherContractDocumentsMap : EntityTypeConfiguration<OtherContractDocuments>
    {

        public OtherContractDocumentsMap()
        {
            this.ToTable("OtherContractDocuments");
            this.HasKey(a => a.Id);

            this.Property(a => a.othercontractid).IsOptional();
            this.Property(a => a.Documentname).IsOptional().HasMaxLength(500);
            this.Property(a => a.documentfile).IsOptional().HasMaxLength(500);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

          
            this.HasRequired(usl => usl.OtherContractMaster)
                .WithMany(s => s.OtherContractDocuments)
                .HasForeignKey(usl => usl.othercontractid)
                  .WillCascadeOnDelete(false);
           }
    }
}
