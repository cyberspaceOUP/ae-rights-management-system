using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Asset
{
    public partial class GlobalAssetAttributeMasterMap: EntityTypeConfiguration<GlobalAssetAttributeMaster>
    {
        public GlobalAssetAttributeMasterMap()
        {
            this.ToTable("GlobalAssetAttributeMaster");
            this.HasKey(g => g.Id);

            this.Property(g => g.Code).HasMaxLength(10);
            this.Property(g => g.Name).IsRequired().HasMaxLength(100);
            this.Property(g => g.Type).IsRequired().HasMaxLength(100);
            this.Property(g => g.UsedFor).IsOptional().HasMaxLength(2);


            this.HasRequired(g => g.GlobalAssetMaster)
                .WithMany()
                .HasForeignKey(g => g.AssetId)
                .WillCascadeOnDelete(false);
        }
    }
}
