using System.Data.Entity.ModelConfiguration;

namespace ACS.Data.Mapping.Configuration
{
    public partial class UserProfileMap : EntityTypeConfiguration<ACS.Core.Domain.Configuration.UserProfile>
    {
        public UserProfileMap()
        {
            this.ToTable("UserProfile");

            this.HasKey(up => up.Id);

            this.Property(up => up.Name).IsRequired().HasMaxLength(35);
            this.Property(up => up.Code).IsRequired().HasMaxLength(10);

            this.HasMany(up => up.ApplicationActivities)
                .WithMany(aa => aa.UserProfiles)
                .Map(up =>
                {
                    up.ToTable("ActivityProfileLink");
                    up.MapLeftKey("ProfileId");
                    up.MapRightKey("ActivityId");
                });
        }
    }
}
