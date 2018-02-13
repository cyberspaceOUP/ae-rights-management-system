using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class ProductSearchDetails
    {
     
        public int? Id { get; set; }

        public int  DivisionId { get; set; }
        public string Division { get; set; }
        public string SubDivision { get; set; }
        public string ProductCategory { get; set; }
        public string ProductType { get; set; }
        public string SubProductType { get; set; }
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
        public string Language { get; set; }
        public string Series { get; set; }
        public string OUPPubCenter { get; set; } 
        public string AuthorCategory { get; set; }
        public string AuthorName { get; set;}
        public string Derivatives { get; set; }
        public string ProjectedDate { get; set; }
        public string ProjectedPrice { get; set; }
        public string ProjectedCurrency { get; set; }
        public string FinalProductEntered { get; set; }
        public string FinalProduct { get; set; }
        public string FinalPublishingDate { get; set; }
        public string ProprietorAuthorType { set; get; }
        public string ProprietorAuthorName { set; get; }
        public string ProprietorPubCenter { get; set; }
        public string ProprietorPublishingCompany{ get; set; }
        public string ProprietorProduct { get; set;}
        public string ProprietorImprint { get; set; }
        public string ProprietorIsbn { get; set; }
        public string ProprietorEdition { get; set; }
        public string ProprietorCopyright { get; set; }
        public string ProprietorAuthorCategory { get; set; }
        public int ProductTypeId { get; set; }
        public int DepartmentId { get; set; }
        public string TypeFor { get; set; }
        public int ContractStatus { get; set; }
        public int LicenceStatus { get; set; }


        public string divisionname { get; set; }
        public string SubdivName { get; set; }
        public string typename { get; set; }
        public string SubtypeName { get; set; }
        public string imprintname { get; set; }
        public string languagename { get; set; }
        public string seriesname { get; set; }
        public string orgisbn { get; set; }
        public string ProjPubDate { get; set; }
        public string currencyname { get; set; }
        public string centername { get; set; }
        public string finalproductname { get; set; }
        public string PubDate { get; set; }
        public string AuthName { get; set; }
        public int ProductId { get; set; }

        public DateTime? ProjectedPublishingDate { get; set; }

        public string Status { get; set; }

        public int? ProjectedCurrencyId { get; set; }

        public int? LicenseId { get; set; }

        public string SessionId { get; set; }
        public string ReturnList { get; set; }
        public string PreviousProductISBN { get; set; }
        public string LinkedProduct { get; set; }
       // public string Division { get; set; }

        //public string MultipleId { get; set; }

        public int? ParentId { get; set; }

        public string FinalPublishedTitle { get; set; }


        public int flag { get; set; }

        //adde by Prakash
        public int? ChildProductId { get; set; }
        public int? ChildProjectedCurrencyId { get; set; }
        public int? ChildParentId { get; set; }
        public DateTime? Childprojectedpublishingdate { get; set; }
        public string Childproductcode { get; set; }
        public string Childprojectcode { get; set; }
        public string Childproductcategory { get; set; }
        public string Childworkingproduct { get; set; }
        public string ChildOupIsbn { get; set; }
        public string ChildProjectedPrice { get; set; }
        public string ChildAuthorName { get; set; }
        public string ChildWorkingSubProduct { get; set; }
        public string ChildProductType { get; set; }
        public string ChildSubProductType { get; set; }
        public string AuthorContractCode { get; set; }
        public string ProductLicensecode { get; set; }
        public string Role { get; set; }
        public int DeactivatedBy { get; set; }
        public string ProjectedCurrencySymbol { get; set; }



        //Added by Saddam on 17/08/2017
        public string thirdpartypermission { get; set; }
        public string ThirdpartypermissionProdunct { get; set; }

        public string ProductLicCode { get; set; }
        public string AuthorContract { get; set;}
        //Ended by Saddam 17/08/2017
    }


    public class ProductLicenseDetail
    {
        public string productlicensecode { get; set; }
        public string ContractDate { get; set; }
        public string territoryrights { get; set; }
        public string Requestdate { get; set; }
        public string Expirydate { get; set; }
        public string remarks { get; set; }
        public int ProductLicenseId { get; set; }
        public string LanguageName { get; set; }
        public int productid { get; set; }
        public string executiveName { get; set; }

        public int? ProjectedCurrencyId { get; set; }
        public string ProjectedPrice { get; set; }
    }
}
