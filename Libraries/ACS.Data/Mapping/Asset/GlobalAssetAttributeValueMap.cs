using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;
namespace ACS.Data.Mapping.Asset
{
    public partial class GlobalAssetAttributeValueMap : EntityTypeConfiguration<GlobalAssetAttributeValue>
    {
        public GlobalAssetAttributeValueMap()
        {
            this.ToTable("GlobalAssetAttributeValue");
            this.HasKey(mt => mt.Id);

            this.Property(g => g.Value).IsRequired().HasMaxLength(100);

            
            this.HasRequired(g => g.GlobalAssetAttributeMaster)
                .WithMany()
                .HasForeignKey(g => g.GAAMId)
                .WillCascadeOnDelete(false);
        }
    }
}
