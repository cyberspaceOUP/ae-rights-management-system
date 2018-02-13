using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    public partial class ProductLicenseSubsidiaryRightsMap : EntityTypeConfiguration<ProductLicenseSubsidiaryRights>
    {
        public ProductLicenseSubsidiaryRightsMap()
        {

            this.ToTable("ProductLicenseSubsidiaryRights");
            this.HasKey(a => a.Id);
            this.Property(a => a.ProductLicenseid).IsRequired();
            this.Property(a => a.publisherpercentage).IsRequired();
            this.Property(a => a.ouppercentage).IsRequired();
            this.Property(a => a.subsidiaryrightsid).IsRequired();
            this.HasRequired<SubsidiaryRightsMaster>(a => a.SubsidiaryRightsSubsidiaryRightse).WithMany().HasForeignKey(a => a.subsidiaryrightsid);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();

            this.HasRequired(usl => usl.SubsidiaryRightsProductLicense)
             .WithMany(s => s.PProductLicenseSubsidiaryRights)
             .HasForeignKey(usl => usl.ProductLicenseid)
               .WillCascadeOnDelete(false);

        }
    }
}
