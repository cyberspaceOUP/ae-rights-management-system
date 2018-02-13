using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class AddendumRoyaltySlab : BaseEntity, ILocalizedEntity
    {
        public int AddendumId { get; set; }
        public int ProductSubTypeId { get; set; }
        public int copiesfrom { get; set; }
        public int copiesto { get; set; }
        public decimal percentage { get; set; }

        #region Navigation Properties
        public virtual AddendumDetails AddendumRoyalityAddendumDetails { get; set; }
        public virtual ProductTypeMaster AddendumRoyalityProductSubProduct { get; set; }
        #endregion
    }
}
