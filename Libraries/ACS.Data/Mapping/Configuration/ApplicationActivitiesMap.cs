using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ACS.Core.Domain.Configuration;

namespace ACS.Data.Mapping.Configuration
{
    public partial class ApplicationActivitiesMap : EntityTypeConfiguration<ApplicationActivities>
    {
        public ApplicationActivitiesMap()
        {
            this.ToTable("ApplicationActivities");
            this.HasKey(mm => mm.Id);
            this.Property(mm => mm.ActivityDesc).IsRequired().HasMaxLength(100);
            this.Property(mm => mm.ParentId);
            this.Property(mm => mm.Area).IsOptional().HasMaxLength(50); //ADDED BY AMAN KUMAR ON DATE 04/03/2016
            this.Property(mm => mm.Controller).IsOptional().HasMaxLength(50); //ADDED BY AMAN KUMAR ON DATE 04/03/2016
            this.Property(mm => mm.Action).IsOptional().HasMaxLength(75); //ADDED BY AMAN KUMAR ON DATE 04/03/2016
            this.Property(mm => mm.SequenceNo).IsRequired();
            this.Property(mm => mm.IsNew).IsOptional(); //ADDED BY AMAN KUMAR ON DATE 04/03/2016
            this.Property(mm => mm.IconClass).IsOptional().HasMaxLength(100); //ADDED BY AMAN KUMAR ON DATE 04/03/2016
            this.Property(mm => mm.DeactTag).IsRequired();
            this.Property(mm => mm.DeactDate).IsOptional();
            this.Property(mm => mm.QueryString).IsOptional();


            this.HasOptional(mm => mm.ParentActivity)
                .WithMany(ch => ch.ChildActivities)
                .HasForeignKey(mm => mm.ParentId);


            //this.HasMany(mm => mm.ContactRoles)
            //    .WithMany(par => par.ApplicationActivities)
            //    .Map(m => m.ToTable("ApplicationActivitiesRoleLink")
            //            .MapLeftKey("ActivityId")
            //            .MapRightKey("RoleId")
            //    );

        }
    }
}
