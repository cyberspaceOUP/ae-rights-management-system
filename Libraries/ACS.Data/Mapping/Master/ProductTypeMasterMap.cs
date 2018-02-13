//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class ProductTypeMasterMap : EntityTypeConfiguration<ProductTypeMaster>
    {
        public ProductTypeMasterMap()
        {
            this.ToTable("ProductTypeMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.typeName).IsRequired().HasMaxLength(100);
            this.Property(a => a.typelevel).IsRequired();
            
            this.HasOptional<ProductTypeMaster>(a => a.ProductTypeM).WithMany().HasForeignKey(a => a.parenttypeid);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
        }
    }
}
