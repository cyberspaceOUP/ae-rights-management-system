//Create by Saddam on 2904/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class profileMasterMAp : EntityTypeConfiguration<ProfileMaster>
    {
        public profileMasterMAp()
        {
            this.ToTable("ProfileMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.Profilecode).IsRequired().HasMaxLength(50);
            this.Property(a => a.ProfileName).IsRequired().HasMaxLength(50);
            this.Property(a => a.Departmentid);
            this.Property(a => a.Deactivate).IsRequired().HasMaxLength(1);
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
            this.Property(a => a.ModifiedBy).IsOptional();
            this.Property(a => a.ModifiedDate).IsOptional();
            this.Property(a => a.DeactivateBy).IsOptional();
            this.Property(a => a.DeactivateDate).IsOptional();
        }
    }
}
