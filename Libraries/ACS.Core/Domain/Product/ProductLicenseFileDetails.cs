using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseFileDetails : BaseEntity, ILocalizedEntity
    {
        public int LicenseUpdateId { get; set; }
        public int LicenseId { get; set; }
        public string FileName { get; set; }
        public string UploadFileName { get; set; }

        #region Navigation Properties
        public virtual ProductLicenseUpdateDetails UpdateProductLicenseDetails { get; set; }
        #endregion
    }
}
