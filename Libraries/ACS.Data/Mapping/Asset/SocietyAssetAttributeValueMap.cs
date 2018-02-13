using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Asset
{
    public partial class SocietyAssetAttributeValueMap: EntityTypeConfiguration<SocietyAssetAttributeValue>
    {
        public SocietyAssetAttributeValueMap()
        {
            this.ToTable("SocietyAssetAttributeValue");
            this.HasKey(g => g.Id);

            this.Property(g => g.Value).IsRequired().HasMaxLength(100);


            this.HasRequired(g => g.Society)
                .WithMany()
                .HasForeignKey(g => g.SocietyId)
                .WillCascadeOnDelete(false);

            this.HasRequired(g => g.GlobalAssetMaster)
                .WithMany()
                .HasForeignKey(g => g.AssetId)
                .WillCascadeOnDelete(false);

            this.HasRequired(g => g.GlobalAssetAttributeMaster)
                .WithMany()
                .HasForeignKey(g => g.GAAMId)
                .WillCascadeOnDelete(false);
        }
    }
}
