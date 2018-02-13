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
    public partial class OtherContractImageBankMap : EntityTypeConfiguration<OtherContractImageBank>
    {
        public OtherContractImageBankMap()
          {
              this.ToTable("OtherContractImageBank");
              this.HasKey(a => a.Id);

              this.Property(a => a.Printrunquantity).IsOptional();
              this.Property(a => a.PrintRights).IsRequired().HasMaxLength(4);
              this.Property(a => a.electronicrights).IsOptional().HasMaxLength(4);

              this.Property(a => a.ebookrights).IsOptional().HasMaxLength(4);

              this.Property(a => a.cost).IsOptional().HasMaxLength(4);

              this.Property(a => a.currencyid).IsOptional();
              this.Property(a => a.restriction).IsOptional().HasMaxLength(500);

              this.Property(a => a.othercontractid).IsOptional();

              this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
              this.Property(a => a.EnteredBy).IsRequired();
              this.Property(a => a.EntryDate).IsRequired();
              this.Property(a => a.ModifiedBy).IsOptional();
              this.Property(a => a.ModifiedDate).IsOptional();
              this.Property(a => a.DeactivateBy).IsOptional();
              this.Property(a => a.DeactivateDate).IsOptional();
            
              this.HasRequired(a => a.OtherContractMaster)
                 .WithMany()
                 .HasForeignKey(a => a.othercontractid)
                 .WillCascadeOnDelete(false);

          }
    }
    public partial class VideoImageBankMap : EntityTypeConfiguration<VideoImageBank>
    {
        public VideoImageBankMap()
        {
            this.ToTable("VideoImageBankLink");
            this.HasKey(a => a.Id);

            this.Property(a => a.Fullname).IsRequired().HasMaxLength(100);
            this.Property(a => a.Type).IsRequired().HasMaxLength(1); ;
            this.Property(a => a.ShortName).IsRequired().HasMaxLength(2);
            this.Property(a => a.ShortName).IsRequired().HasMaxLength(2);
            this.Property(a => a.CurrencyId).IsRequired();
            this.Property(a => a.Cost).IsRequired();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.OtherContractImageBank)
               .WithMany()
               .HasForeignKey(a => a.ImageBankId)
               .WillCascadeOnDelete(false);

            this.HasRequired(a => a.CurrencyMaster)
               .WithMany()
               .HasForeignKey(a => a.CurrencyId)
               .WillCascadeOnDelete(false);

        }
    }
}
