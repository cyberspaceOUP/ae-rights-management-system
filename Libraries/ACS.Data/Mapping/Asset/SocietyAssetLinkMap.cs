using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Asset
{
    public partial class SocietyAssetLinkMap: EntityTypeConfiguration<SocietyAssetLink>
    {
        public SocietyAssetLinkMap()
        {
            this.ToTable("SocietyAssetLink");
            this.HasKey(sa => sa.Id);

            this.HasRequired(g => g.Society)
                .WithMany(g=>g.SocietyAssetLinks)
                .HasForeignKey(g => g.SocietyId)
                .WillCascadeOnDelete(false);

            this.HasOptional(g => g.Block)
                .WithMany()
                .HasForeignKey(g => g.BlockId)
                .WillCascadeOnDelete(false);

            this.HasOptional(g => g.Tower)
                .WithMany()
                .HasForeignKey(g => g.TowerId)
                .WillCascadeOnDelete(false);

            this.HasRequired(g => g.GlobalAssetMaster)
                .WithMany()
                .HasForeignKey(g => g.AssetId)
                .WillCascadeOnDelete(false);
        }
    }
}
