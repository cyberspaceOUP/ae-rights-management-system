using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ImpressionDetails : BaseEntity, ILocalizedEntity
    {
        public int ProductId { get; set; }
        public int? LicenseId { get; set; }
        public int? AddendumId { get; set; }
        public int? ContractId { get; set; }
        public int ImpressionNo { get; set; }
        public DateTime ImpressionDate { get; set; }
        public int QunatityPrinted { get; set; }
        public int? BalanceQty { get; set; }
        public string ThroughAddendum { get; set; }
        public int? PrevLicenseId { get; set; }
        public int? PrevAddendumId { get; set; }
        public int? PrevContactQty { get; set; }

        public int? KitISBNId { get; set; }

        #region Navigation Properties
        public virtual ProductLicense tblPreviousLicense { get; set; }
        public virtual AddendumDetails tblPreviousAddendum { get; set; }
        public virtual AuthorContractOriginal tblPreviousAuthorContract { get; set; }
        #endregion
    }
}
