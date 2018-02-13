//Create by Saddam on 30/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Data.Mapping.Master
{

    public partial class Upload_ISBN_BackMap : EntityTypeConfiguration<Upload_ISBN_Back>
    { 
      public Upload_ISBN_BackMap()
        {
            this.ToTable("Upload_ISBN_Back");
            this.HasKey(a => a.Id);
             this.Property(a => a.UploadFile).IsOptional().HasMaxLength(200);
             this.Property(a => a.DocumentName).IsOptional().HasMaxLength(200);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();


            this.HasRequired(usl => usl.ISBNBagMaster)
                 .WithMany(s => s.Upload_ISBN_Back)
                 .HasForeignKey(usl => usl.ISBNBagId)
                   .WillCascadeOnDelete(false);
        }
    }
 
}
