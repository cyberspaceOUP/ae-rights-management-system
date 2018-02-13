using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class ISBNBagMap : EntityTypeConfiguration<ISBNBag>
    {
        public ISBNBagMap()
        {
            this.ToTable("ISBNBag");
            this.HasKey(a => a.Id);
            this.Property(a => a.ISBN).IsRequired().HasMaxLength(13);
            this.HasRequired<ProductTypeMaster>(a => a.ProductTypeM).WithMany().HasForeignKey(a => a.ProductTypeid);
            this.Property(a => a.Used).IsRequired().HasMaxLength(1);
            this.Property(a => a.ProductId).IsOptional();
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
            this.Property(a => a.Used).IsOptional().HasMaxLength(1);

            this.Property(a => a.KitISBNId).IsOptional();

        }
    }
}
