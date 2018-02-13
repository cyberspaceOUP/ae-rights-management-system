using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Messages;

namespace ACS.Data.Mapping.Messages
{
    public partial class QueuedSmsMap : EntityTypeConfiguration<QueuedSms>
    {
        public QueuedSmsMap()
        {
            this.ToTable("QueuedSms");
            this.HasKey(qs => qs.Id);

            this.Property(qs => qs.Priority).IsRequired();
            this.Property(qs => qs.MobileNumber).IsRequired().HasMaxLength(10);
            this.Property(qs => qs.Body).IsRequired().HasMaxLength(300);
            this.Property(qs => qs.SentTries).IsRequired();
            this.Property(qs => qs.SentOn).IsOptional();
            this.Property(qs => qs.Guid).IsOptional().HasMaxLength(70);
            this.Property(qs => qs.StatusCode).IsOptional().HasMaxLength(10);
            this.Property(qs => qs.ReasonCode).IsOptional().HasMaxLength(10);

            this.HasRequired(qs => qs.SMSAccount)
                .WithMany()
                .HasForeignKey(qs => qs.SmsAccountId)
                .WillCascadeOnDelete(false);

            this.HasRequired(qs => qs.SMSTemplate)
                .WithMany()
                .HasForeignKey(qs => qs.SmsTemplateId)
                .WillCascadeOnDelete(false);
        }
    }
}
