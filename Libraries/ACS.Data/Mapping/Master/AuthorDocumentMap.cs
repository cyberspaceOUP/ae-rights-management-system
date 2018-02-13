//Create by Saddam on 06/06/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Data.Mapping.Master
{
    

    public partial class AuthorDocumentMap : EntityTypeConfiguration<AuhtorDocument>
    {
        public AuthorDocumentMap()
        {
            this.ToTable("AuthorDocument");
            this.HasKey(a => a.Id);
           this.Property(a => a.AuhtorId).IsRequired();
            this.Property(a => a.DocumentName).IsOptional().HasMaxLength(500);
            this.Property(a => a.UploadFile).IsOptional().HasMaxLength(500);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

          //  this.HasRequired(a => a.AuthorMaster)
          //.WithMany()
          //.HasForeignKey(a => a.AuhtorId)
          //.WillCascadeOnDelete(false);

            this.HasRequired(usl => usl.AuthorMaster)
                .WithMany(s => s.AuhtorDocument )
                .HasForeignKey(usl => usl.AuhtorId)
                  .WillCascadeOnDelete(false);
        }
    }
}
