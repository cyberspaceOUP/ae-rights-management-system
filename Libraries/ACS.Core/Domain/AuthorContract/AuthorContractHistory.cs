//Create by Saddam on 19/07/2016
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Core.Domain.AuthorContract
{
 public partial class AuthorContractHistory : BaseEntity
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
        public string ProjectedPrice { get; set; }
        public string AuthorContractCurrencyId { get; set; }
        public int? ProjectHandleBy { get; set; }
        public string PriceTypeCond { get; set; }
        public int? CurrencyId { get; set; }
        public string AuthorContractCode { get; set; }
        public string AuthorName { get; set; }
        public DateTime? RequestFromDate { get; set; }
        public DateTime? RequestToDate { get; set; }
        public int? ContractType { get; set; }
        public DateTime? DateofagreementFromdate { get; set; }
        public DateTime? DateofagreementTodate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? Territory { get; set; }
        public string ThirdPartyPermission { get; set; }
        public string Royalty { get; set; }
        public string MaterialSupplied { get; set; }
        public string MediumofDelivery { get; set; }
        public string ManuscriptDeliveryFormat { get; set; }
        public string Contractstatus { get; set; }
        public string Remarks { get; set; }
        public string Amendment { get; set; }
        public string For_ { get; set; }
        
    }
}
