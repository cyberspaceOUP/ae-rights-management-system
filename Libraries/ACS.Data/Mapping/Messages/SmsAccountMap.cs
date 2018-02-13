using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Messages;

namespace ACS.Data.Mapping.Messages
{
    public partial class SmsAccountMap : EntityTypeConfiguration<SmsAccount>
    {
        public SmsAccountMap()
        {
            this.ToTable("SmsAccount");
            this.HasKey(ea => ea.Id);

            this.Property(sa => sa.Vendor).IsRequired().HasMaxLength(25);
            this.Property(sa => sa.Username).IsRequired().HasMaxLength(35);
            this.Property(sa => sa.Password).IsRequired().HasMaxLength(80);
            this.Property(sa => sa.FromName).IsRequired().HasMaxLength(10);
            this.Property(sa => sa.DeactTag).IsRequired();
        }
    }
}
