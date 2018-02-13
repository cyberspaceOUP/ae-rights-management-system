//Create by saddam on 19/07/2016
using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.AuthorContract
{
    public partial class AuthorContractHistoryMap : EntityTypeConfiguration<AuthorContractHistory>
    {
        public AuthorContractHistoryMap()
        {
            this.ToTable("AuthorContractHistory");
            this.HasKey(a => a.Id);
            this.Property(a => a.SessionId).IsRequired();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();

        }
    }
}
