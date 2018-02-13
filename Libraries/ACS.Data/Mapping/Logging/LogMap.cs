using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Logging;

namespace ACS.Data.Mapping.Logging
{
    public partial class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(l => l.Id);
            this.Property(l => l.ShortMessage).IsRequired();
            this.Property(l => l.IpAddress).HasMaxLength(200);

            this.Ignore(l => l.LogLevel);

            
            

        }
    }
}