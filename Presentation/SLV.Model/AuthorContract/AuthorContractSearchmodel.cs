using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.AuthorContract
{
    public class AuthorContractSearchmodel
    {
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
        public string Royalty { get; set; }
        public string MaterialSupplied { get; set; }
        //public string MediumofDelivery { get; set; }
        public string WorkingTitle { get; set; }
        public string WorkingSubProduct { get; set; }
        public string oupisbn { get; set; }

        public int? ProductId { get; set; }

        public string Flag { get; set; }
        public string UpdateFlag { get; set; }
        public string ReturnList { get; set; }
        public string SessionId { get; set; }


        public string ContractEntryDate { get; set; }
        public string NoOfAuthors { get; set; }
        public string PriceType { get; set; }
        public string TermsOfCopyright { get; set; }
        public string BuyBack { get; set; }
        public string NatureOfWork { get; set; }
        public string CopyrightOwner { get; set; }
        public string TerritoryRights { get; set; }
        public string ThirdPartyPermission { get; set; }
        public string Contributors { get; set; }
        public string Amendment { get; set; }
        public string AmendmentRemarks { get; set; }
        public string Restriction { get; set; }
        public string subjectMatterAndTreatment { get; set; }
        public string MinNoOfwords { get; set; }
        public string MaxNoOfwords { get; set; }
        public string MinNoOfPages { get; set; }
        public string MaxNoOfPages { get; set; }
        public string MaterialtobeSupplied { get; set; }
        public string CurrencyName { get; set; }
        public string Price { get; set; }
        public string MediumOfdelivery { get; set; }
        public string ManuscriptDeliveryFormat { get; set; }
        public string Deliveryschedule { get; set; }
        public string ProductRemarks { get; set; }
        public string SeriesName { get; set; }
        public string ContractDate { get; set; }
        public string Contractstatus { get; set; }
        public string DateOfAgreement { get; set; }
        public string SignedContractsentdate { get; set; }
        public string SignedContractreceived { get; set; }
        public string Authorcopiessentdate { get; set; }
        public string Contributorcopiessentdate { get; set; }
        public string Cancellationdate { get; set; }
        public string Cancellationreason { get; set; }
        public string Remarks { get; set; }
        public string effectiveDate { get; set; }
        public string periodofagreement { get; set; }
        public string ContractExpiryDate { get; set; }

        public string RequestBy { get; set; }
        public int RequestById { get; set; }

        public string AuthorData { get; set; }

        public string ContractEntryDate_EntryDate { get; set; }
        public string ExecutiveName { get; set; }
        public string divisionName { get; set; }

        public DateTime? DateOfAgreementForSort { get; set; }
        public DateTime? ContractExpiryDateForSort { get; set; }
        public DateTime? ContractEntryDate_EntryDateForSort { get; set; }

        public int AddendumId { get; set; }
        public string SeriesCode { get; set; }
    }

    public class ProductSeriesContract
    {
        public int AuthorContractId { get; set; }
        public string SeriesCode { get; set; }
        public string AuthorContractCode { get; set; }
        public string ContractEntryDate { get; set; }
        public string ContractExpiryDate { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string OUPISBN { get; set; }
        public string Flag { get; set; }

        public string ChildAuthorContractCode { get; set; }
        public string ChildContractEntryDate { get; set; }
        public string ChildContractExpiryDate { get; set; }
        public string ChildProductName { get; set; }
        public string ChildProductCode { get; set; }
        public string ChildOUPISBN { get; set; }
    }

  
}
