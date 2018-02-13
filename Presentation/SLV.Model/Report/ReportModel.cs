using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Report
{
    public class AuthorContractExpiryReportModel
    {
        public string Authors { get; set; }
        public string ExpiryFromDate { get; set; }
        public string ExpiryToDate { get; set; }


        public int AuthorContractId { get; set; }
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
        public string RequestFromDate { get; set; }
        public string RequestToDate { get; set; }
        public int? ContractType { get; set; }
        public string DateofagreementFromdate { get; set; }
        public string DateofagreementTodate { get; set; }
        public string ExpiryDate { get; set; }
        public int? Territory { get; set; }
        public string ThirdPartyPermission { get; set; }
        public string Royalty { get; set; }
        public string MaterialSupplied { get; set; }
        public string MediumofDelivery { get; set; }
        public string ManuscriptDeliveryFormat { get; set; }
        public string Contractstatus { get; set; }
        public string Remarks { get; set; }
        public string Amendment { get; set; }
        public string WorkingTitle { get; set; }
        public string oupisbn { get; set; }
        public string ContractEntryDate { get; set; }
        public string ContractExpiryDate { get; set; }
        public int? ProductId { get; set; }
    }

    public class LicenseListModel
    {
        public string Flag { get; set; }

        public int Id { get; set; }
        public string ProductLicensecode { get; set; }
        public string ProductCode { get; set; }
        public string WorkingProduct { get; set; }
        public string Expirydate { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }

    }

    public class AddendumListModel
    {
        public string Flag { get; set; }

        public int Id { get; set; }
        public string ProductLicensecode { get; set; }
        public string ProductCode { get; set; }
        public string WorkingProduct { get; set; }
        public string Expirydate { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }

    }
}
