using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Messages;

namespace ACS.Data.Mapping.Messages
{
    public partial class SmsUrlTemplateMap : EntityTypeConfiguration<SmsUrlTemplate>
    {
        public SmsUrlTemplateMap()
        {
            this.ToTable("SmsUrlTemplate");
            this.HasKey(sut => sut.Id);

            this.Property(sut => sut.XmlName).IsRequired().HasMaxLength(35);
            this.Property(sut => sut.XmlURL).IsRequired().HasMaxLength(500);
            this.Property(sut => sut.DeactTag).IsRequired();
        }
    }
}
