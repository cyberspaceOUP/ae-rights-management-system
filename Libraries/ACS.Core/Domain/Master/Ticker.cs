using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.Master
{
    public partial class Ticker : BaseEntity, ILocalizedEntity
    {
        public string Title { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Order { get; set; }
    }
}
