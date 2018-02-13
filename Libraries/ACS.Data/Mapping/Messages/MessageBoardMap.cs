using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Messages;

namespace ACS.Data.Mapping.Messages
{
    public partial class MessageBoardMap : EntityTypeConfiguration<MessageBoard>
    {
        public MessageBoardMap()
        {
            this.ToTable("MessageBoard");
            this.HasKey(mb => mb.Id);
            this.Property(mb => mb.Heading).IsRequired().HasMaxLength(100);
            this.Property(mb => mb.Body).IsRequired().HasMaxLength(500);
            this.Property(mb => mb.Sequence).IsRequired();
            this.Property(mb => mb.FromDate).IsRequired();
            this.Property(mb => mb.ToDate);
            this.Property(mb => mb.LinkUrl).IsOptional().HasMaxLength(250);
            this.Property(mb => mb.ImageUrl).IsOptional().HasMaxLength(150);
            this.Property(mb => mb.EnteredBy).IsRequired();
            this.Property(mb => mb.DeactTag).IsRequired();

            ////RecordedBy Id
            //this.HasRequired(mb => mb.MessageEnteredBy)
            //    .WithMany()
            //    .HasForeignKey(mb => mb.EnteredBy)
            //    .WillCascadeOnDelete(false);
        }
    }
}
