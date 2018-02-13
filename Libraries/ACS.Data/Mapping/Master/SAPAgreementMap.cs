using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class SAPAgreementMap : EntityTypeConfiguration<ProductSAPAgreementMaster>
    {
       public SAPAgreementMap()
       {
           this.ToTable("ProductSAPAgreementMaster");
           this.HasKey(A => A.Id);
           this.Property(a => a.OUPISBN).IsRequired().HasMaxLength(20);
           this.Property(a => a.SAPagreementNo).IsOptional().HasMaxLength(20);
           this.Property(a => a.AuthorCode).IsOptional().HasMaxLength(20);
           this.Property(a => a.AuthorId).IsOptional();
           this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
           this.Property(a => a.EnteredBy).IsOptional();
           this.Property(a => a.EntryDate).IsOptional();
           this.Property(a => a.EnteredBy).IsRequired();
           this.Property(a => a.ModifiedBy).IsOptional();
           this.Property(a => a.ModifiedDate).IsOptional();
           this.Property(a => a.DeactivateBy).IsOptional();
           this.Property(a => a.DeactivateDate).IsOptional();
           this.Property(a => a.ProprietorAuthorId).IsOptional();
           this.Property(a => a.ProductCategory).IsOptional();
       }
    }
}
