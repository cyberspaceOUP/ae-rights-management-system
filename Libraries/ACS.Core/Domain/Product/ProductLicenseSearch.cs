using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseSearch
    {

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
        public string ProductFromDate { get; set; }
        public string ProductToDate { get; set; }
        public string ExpiryFromDate { get; set; }
        public string ExpiryToDate { get; set; }
        public string ISBNAssigned { get; set; }
        public string SAPAgreementUploaded { get; set; }
        public string RoyaltyTerms { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public string PriceType { get; set; }
        public string PriceTypeCond { get; set; }
        public decimal? Price { get; set; }
        public int? CurrencyId { get; set; }
        public string ThridPartyPermission { get; set; }
        public string LicenseStatus { get; set; }
        public string Remarks { get; set; }
        public string flag { get; set; }

        public class ClsSearchReport
        {
            public int LicenseId { get; set; }
            public int ProductId { get; set; }
            public string productlicensecode { get; set; }
            public string productcode { get; set; }
            public string projectcode { get; set; }
            public string WorkingTitle { get; set; }
            public string OUPISBN { get; set; }
            public string ContractDate { get; set; }
            public int AddendumId { get; set; }
            public string AddendumCode { get; set; }

            public string ReturnList { get; set; }
            public string SessionId { get; set; }
            public string flag { get; set; }

            public string AuthorName { get; set; }
            public string WorkingSubProduct { get; set; }



            public string ProprietorCompany { get; set; }
            public string ContactPerson { get; set; }
            public string Address { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Pincode { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Requestdate { get; set; }
            public string TerritoryRights { get; set; }
            public string Impressionwithindate { get; set; }
            public string noofimpressions { get; set; }
            public string printquantitytype { get; set; }
            public string printquantity { get; set; }
            public string RoyalityTerms { get; set; }
            public string PaymentAmount { get; set; }
            public string AdvancedAmount { get; set; }
            public string copiesforlicensor { get; set; }
            public string pricetype { get; set; }
            public string Currencyid { get; set; }
            public string price { get; set; }
            public string thirdpartypermission { get; set; }
            public string Remarks { get; set; }
            public string LicensorCopiesSentDate { get; set; }
            public string EFilesCost { get; set; }
            public string EFilesRequestDate { get; set; }
            public string EFilesReceivedDate { get; set; }
            public string Mode { get; set; }
            public string effectivedate { get; set; }
            public string contractperiodinmonth { get; set; }
            public string Expirydate { get; set; }
            public string AgreementDate { get; set; }

            public string AddendumDate { get; set; }
            public string AddendumType { get; set; }
            public string AddendumPeriodofagreement { get; set; }
            public string AddendumExpiryDate { get; set; }
            public string AddendumFirstImpressionWithinDate { get; set; }
            public string AddendumNoOfImpressions { get; set; }
            public string AddendumBalanceQuantityCarryForward { get; set; }
            public string AddendumQuantity { get; set; }
            public string AddendumRoyaltyTerms { get; set; }
            public string AddendumRemarks { get; set; }
            public string RoyaltySlab { get; set; }
            public string SubsidiaryRights { get; set; }

            public DateTime? AgreementDateForSort { get; set; }
            public DateTime? ExpirydateForSort { get; set; }
        }
    }
}
