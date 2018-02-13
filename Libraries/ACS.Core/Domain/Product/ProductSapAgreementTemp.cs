using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductSapAgreementTemp : BaseEntity, ILocalizedEntity
    {
        public int ProductCategoryId { get; set; }
        public string ProjectCode { get; set; }
        public string ProductCode { get; set; }
        public string OUPISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string Derivatives { get; set; }
        public string OrgISBN { get; set; }
        public DateTime? ProjectedPublishingDate { get; set; }
        public string ProjectedPrice { get; set; }
        public int? ProjectedCurrencyId { get; set; }
        public int? PubCenterId { get; set; }
        public string SAPagreementNo { get; set; }
        public string POUPISBN { get; set; }
        public string ProjectDate { get; set; }
        public string Author{ get; set; }
        public string ContractCode { get; set; }
    }
}
