//Create By Saddam on 26/09/2016
using ACS.Core.Domain.Alert;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Alert
{
    public partial class ApplicationEmailSetupMap : EntityTypeConfiguration<ApplicationEmailSetup>
    {
        public ApplicationEmailSetupMap()
        {
            this.ToTable("ApplicationEmailSetup");
            this.HasKey(a => a.Id);
            this.Property(a => a.EmailType).IsOptional().HasMaxLength(400);
            this.Property(a => a.subject).IsOptional().HasMaxLength(400);
            this.Property(a => a.EmailTo).IsOptional().HasMaxLength(400);
            this.Property(a => a.EmailCCTo).IsOptional().HasMaxLength(400);
            this.Property(a => a.EmailBCCTo).IsOptional().HasMaxLength(400);
        }
    }
}
