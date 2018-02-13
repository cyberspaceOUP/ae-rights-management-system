using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    public partial class PageAccessMasterMap : EntityTypeConfiguration<PageAccessMaster>
    {
        public PageAccessMasterMap()
        {
            this.ToTable("PageAccessMaster");
            this.HasKey(a => a.Id);
            this.Property(a => a.UserName).IsRequired().HasMaxLength(200);
            this.Property(a => a.PageUrl).IsRequired().HasMaxLength(1000);
        }
    }
}
