using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseSubsidiaryRights : BaseEntity ,ILocalizedEntity
    {
        public int ProductLicenseid { get; set; }
        public decimal publisherpercentage { get; set; }
        public decimal ouppercentage { get; set; }
        public int subsidiaryrightsid { get; set; }

        #region Navigation Properties
        public virtual ProductLicense SubsidiaryRightsProductLicense { get; set; }
        public virtual SubsidiaryRightsMaster SubsidiaryRightsSubsidiaryRightse { get; set; }
        #endregion
    }
}
