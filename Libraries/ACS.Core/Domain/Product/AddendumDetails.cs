using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class AddendumDetails : BaseEntity, ILocalizedEntity
    {
        public int LicenseId { get; set; }
        public int ProductId { get; set; }
        public string AddendumCode { get; set; }
        public DateTime AddendumDate  { get; set; }
        public string AddendumType { get; set; }
        public int? Periodofagreement { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? FirstImpressionWithinDate { get; set; }
        public int? NoOfImpressions { get; set; }
        public string BalanceQuantityCarryForward { get; set; }
        public int? AddendumQuantity { get; set; }
        public int? BalanceQuantity { get; set; }
        public string RoyaltyTerms { get; set; }
        public string Remarks { get; set; }

        #region Navigation Properties
        public virtual ICollection<AddendumRoyaltySlab> AddendumDetailsRoyalty { get; set; }
        public virtual ICollection<AddendumFileDetails> IAddendumFileDetails { get; set; }
        #endregion
    }
}
