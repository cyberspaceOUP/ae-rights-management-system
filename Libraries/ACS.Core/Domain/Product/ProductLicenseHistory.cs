//create  by saddam on 19/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Core.Domain.Product
{
 public partial   class ProductLicenseHistory : BaseEntity
    {
         public string SessionId { get; set; }
        public string SAPAgreementNo { get; set; }
        public string ProjectCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string SubProductName { get; set; }
        public string ISBN { get; set; }
        public int? DivisionId { get; set; }
        public int? SubDivisionId { get; set; }
        public string ProductType { get; set; }
        public string SubProductType { get; set; }
        public int? ImprintId { get; set; }
        public int? LanguageId { get; set; }
        public int? AuthorId { get; set; }
        public int? SeriesId { get; set; }
        public string ProjectedPriceCond { get; set; }
        public decimal? ProjectedPrice { get; set; }
        public int? ProjectCurrencyId { get; set; }
        public int? ProjectHandleBy { get; set; }
        public string ProductLicenseCode { get; set; }
        public DateTime? ProductFromDate { get; set; }
        public DateTime? ProductToDate { get; set; }
        public DateTime? ExpiryFromDate { get; set; }
        public DateTime? ExpiryToDate { get; set; }
        public string ISBNAssigned { get; set; }
        public string SAPAgreementUploaded { get; set; }
        public string RoyaltyTerms { get; set; }
        public string AdvanceAmount { get; set; }
        public string PriceType { get; set; }
        public string PriceTypeCond { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public string ThridPartyPermission { get; set; }
        public string LicenseStatus { get; set; }
        public string Remarks { get; set; }
        public string For_ { get; set; }
    }
}
