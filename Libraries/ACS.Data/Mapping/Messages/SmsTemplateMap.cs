using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Messages;

namespace ACS.Data.Mapping.Messages
{
    public partial class SmsTemplateMap : EntityTypeConfiguration<SmsTemplate>
    {
        public SmsTemplateMap()
        {
            this.ToTable("SmsTemplate");
            this.HasKey(st => st.Id);

            this.Property(st => st.Name).IsRequired().HasMaxLength(35);
            this.Property(st => st.Body).IsRequired().HasMaxLength(300);
            this.Property(st => st.DeactTag).IsRequired();

            this.HasRequired(st => st.SMSAccount)
                .WithMany()
                .HasForeignKey(st => st.SmsAccountId)
                .WillCascadeOnDelete(false);
        }
    }
}
