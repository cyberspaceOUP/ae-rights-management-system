//Create by Saddam on 19/07/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.OtherContract;

namespace ACS.Data.Mapping.OtherContract
{
    class OtherContractSearchMap : EntityTypeConfiguration<OtherContractSearch>
    {
        public OtherContractSearchMap()
        {
            this.ToTable("OtherContractSearch");
            this.HasKey(a => a.Id);
            this.Property(a => a.SessionId).IsRequired();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
        }
    }
}
