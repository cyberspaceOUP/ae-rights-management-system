using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseRoyality : BaseEntity, ILocalizedEntity
    {
        public int ProductLicenseid { get; set; }
        public int ProductSubTypeId { get; set; }
        public int copiesfrom { get; set; }
        public int copiesto { get; set; }
        public decimal percentage { get; set; }

        #region Navigation Properties
        public virtual ProductLicense RoyalityProductLicense { get; set; }
        public virtual ProductTypeMaster RoyalityProductSubProduct { get; set; }
        #endregion
    }
}
