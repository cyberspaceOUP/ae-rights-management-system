using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseUpdateDetails : BaseEntity, ILocalizedEntity
    {
        public int LicenseId { get; set; }
        public DateTime? LicensorCopiesSentDate { get; set; }
        public decimal? EFilesCost { get; set; }
        public DateTime? EFilesRequestDate { get; set; }
        public DateTime? EFilesReceivedDate { get; set; }
        public string Mode { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? Contractperiodinmonth { get; set; }
        public DateTime? Expirydate { get; set; }

        #region Navigation Properties
        public virtual ProductLicense UpdateProductLicense { get; set; }
        public virtual ICollection<ProductLicenseFileDetails> ILicenseUpdateFileDetails { get; set; }
        #endregion
        
    }
}
