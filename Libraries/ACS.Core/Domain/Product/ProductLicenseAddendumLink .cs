using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseAddendumLink : BaseEntity, ILocalizedEntity
    {
        public int ProductId {get;set;}
        public int LicenseId {get;set;}
        public int? AddendumId {get;set;}
        public string Active {get;set;}

        #region Navigation Properties
        public virtual ProductLicense AddendumLinkProductLicense { get; set; }
        #endregion
    }
}
