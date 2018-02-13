using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Tasks;

namespace ACS.Data.Mapping.Tasks
{
    public partial class ScheduleTaskMap : EntityTypeConfiguration<ScheduleTask>
    {
        public ScheduleTaskMap()
        {
            this.ToTable("ScheduleTask");
            this.HasKey(st => st.Id);
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Type).IsRequired();
        }
    }
}
