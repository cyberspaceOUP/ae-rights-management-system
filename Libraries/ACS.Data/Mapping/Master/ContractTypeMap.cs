using ACS.Core.Domain.Master;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Master
{
    class ContractTypeMap : EntityTypeConfiguration<ContractType>
    {
        public ContractTypeMap()
        {
            this.ToTable("ContractType");
            this.HasKey(a => a.Id);
            this.Property(a => a.ContractName).IsRequired().HasMaxLength(200);
        }
    }
}
