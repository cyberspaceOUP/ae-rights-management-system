using ACS.Core.Domain.Asset;
using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Asset
{
    public partial class SocietyAssetImageMap: EntityTypeConfiguration<SocietyAssetImage>
    {
        public SocietyAssetImageMap()
        {
            this.ToTable("SocietyAssetImage");
            this.HasKey(sa => sa.Id);

            this.Property(sa => sa.ImageURL).IsRequired().HasMaxLength(200);
            this.Property(sa => sa.ImageDescription).IsOptional().HasMaxLength(100);
            this.Property(sa => sa.ImageType).IsRequired().HasMaxLength(10);

            this.HasRequired(g => g.SocietyAssetLink)
                .WithMany()
                .HasForeignKey(g => g.SocietyAssetId)
                .WillCascadeOnDelete(false);
        }
    }
}
