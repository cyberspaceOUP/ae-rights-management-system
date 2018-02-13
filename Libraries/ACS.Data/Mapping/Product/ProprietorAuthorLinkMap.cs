using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProprietorAuthorLinkMap : EntityTypeConfiguration<ProprietorAuthorLink>
    {
        public ProprietorAuthorLinkMap()
        {

            this.ToTable("ProprietorAuthorLink");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProprietorId).IsRequired();
            this.Property(a => a.AuthorId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(a => a.ProprietorAuthorLinkAuthor)
               .WithMany()
               .HasForeignKey(a => a.AuthorId)
               .WillCascadeOnDelete(false);


            //this.HasRequired(usl => usl.ProprietorMaster  )
            //    .WithMany(s => s.ProprietorAuthorLink)
            //    .HasForeignKey(usl => usl.ProprietorId);

            this.HasRequired(usl => usl.ProprietorMaster)
                .WithMany(s => s.ProprietorAuthorLink)
                .HasForeignKey(usl => usl.ProprietorId)
                  .WillCascadeOnDelete(false);
        }
    }
}
