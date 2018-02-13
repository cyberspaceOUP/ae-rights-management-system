using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    class AuthorTypeMap : EntityTypeConfiguration<AuthorType>
    {
        public AuthorTypeMap()
         {
             this.ToTable("AuthorType");
             this.HasKey(a => a.Id);
             this.Property(a => a.AuthorTypeName).HasMaxLength(100);
         }
    }
}
