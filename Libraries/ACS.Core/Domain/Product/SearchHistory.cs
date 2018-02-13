using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class SearchHistory : BaseEntity
    {
        public string SessionId { get; set; }
        public int? DivisionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SubDivisionId { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? SubProductTypeId { get; set; }
        public string ProjectCode { get; set; }
        public string ProductCode { get; set; }
        public string SapAuthorCode { get; set; }
        public string SapAgreementNo { get; set; }
        public string OupIsbn { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string OupEdition { get; set; }
        public string Volume { get; set; }
        public string CopyrightYear { get; set; }
        public string Imprint { get; set; }
        public int? LanguageId { get; set; }
        public string Series { get; set; }
        public string AuthorCategory { get; set; }
        public string AuthorName { get; set; }
        public string Derivatives { get; set; }
        public DateTime? ProjectedDate { get; set; }
        public string ProjectedPrice { get; set; }
        public int? ProjectedCurrencyId { get; set; }
        public string FinalProductEntered { get; set; }
        public string FinalProduct { get; set; }
        public DateTime? FinalPublishingDate { get; set; }
        public string ProprietorAuthorType { set; get; }
        public string ProprietorAuthorName { set; get; }
        public string ProprietorPubCenterId { get; set; }
        public int? ProprietorPublishingCompanyId { get; set; }
        public string ProprietorProduct { get; set; }
        public int? ProprietorImprintId { get; set; }
        public string ProprietorIsbn { get; set; }
        public string ProprietorEdition { get; set; }
        public string ProprietorCopyright { get; set; }
        public string ProprietorAuthorCategoryId { get; set; }
        public string TypeFor { get; set; }
        public string Status { get; set; }
        public string License { get; set; }
        public string ThirdPartyPermission { get; set; }
        public string KitISBN { get; set; }
        public string KitWorkingProduct { get; set; }
        public string KitWorkingSubProduct { get; set; }
    }
}