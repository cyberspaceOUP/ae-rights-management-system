using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Asset
{    
    public partial class GlobalAssetMasterMap : EntityTypeConfiguration<GlobalAssetMaster>
    {
        public GlobalAssetMasterMap()
        {
            this.ToTable("GlobalAssetMaster");
            this.HasKey(mt => mt.Id);

            this.Property(gt => gt.Code).HasMaxLength(10);
            this.Property(gt => gt.Name).IsRequired().HasMaxLength(100);
            
        }
    }
}
