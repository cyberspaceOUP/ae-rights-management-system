using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Data.Mapping.Product
{
    class SearchHistoryMapcs : EntityTypeConfiguration<SearchHistory>
    {
        public SearchHistoryMapcs()
        {
            this.ToTable("SearchHistory");
            this.HasKey(a => a.Id);
            this.Property(a => a.SessionId).IsRequired();
            this.Property(a => a.EnteredBy).IsRequired();
            this.Property(a => a.EntryDate).IsRequired();
        }
    }
}
