using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Product
{
    public class SeriesProductEntryModel
    {
        //public int DivisionId { get; set; }
        //public int SubdivisionId { get; set; }
        //public int ProductCategoryId { get; set; }
        //public int ProductTypeId { get; set; }
        //public int SubProductTypeId { get; set; }
        //public string OUPISBN { get; set; }
        //public string WorkingProduct { get; set; }
        //public string WorkingSubProduct { get; set; }
        //public string OUPEdition { get; set; }
        //public string CopyrightYear { get; set; }
        //public int ImprintId { get; set; }
        //public int LanguageId { get; set; }
        //public int? SeriesId { get; set; }
        //public string Derivatives { get; set; }
        //public string OrgISBN { get; set; }
        //public string Deactivate { get; set; }
        //public int EnteredBy { get; set; }
        //public DateTime EntryDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public int? DeactivateBy { get; set; }
        //public DateTime? DeactivateDate { get; set; }

        //public object[]  testArr { get; set; }

        //public string ProductCode { get; set; }

        public IList<testArr> testArr = new List<testArr>();
        ///public IList<SeriesProductEntryModel> Object = new List<SeriesProductEntryModel>();
        //public IList<AuthorLink> AuthorLink = new List<AuthorLink>();
    }

    public class testArr
    {
        public int DivisionId { get; set; }
        public int SubdivisionId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductTypeId { get; set; }
        public int SubProductTypeId { get; set; }
        public string OUPISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string OUPEdition { get; set; }
        public DateTime? ProjectPublishingDate { get; set; }
        public string CopyrightYear { get; set; }
        public int ImprintId { get; set; }
        public int LanguageId { get; set; }
        public int? SeriesId { get; set; }
        public string Derivatives { get; set; }
        public string OrgISBN { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public IList<AuthorIds> AuthorObject { get; set; }
        public IList<ISBN> ISBNObject { get; set; }
        //public object[] testArr { get; set; }

        public string ProductCode { get; set; }

        public string ProjectedPrice { get; set; }
        public int? ProjectedCurrencyId { get; set; }

        public int ThirdPartyPermission { get; set; }
    }

    public class ISBN
    {
        public string OUPISBNnos { get; set; }
    }

    public class AuthorIds
    {
        public int AuthorId { get; set; }
        //public string OUPISBNnos { get; set; }
    }


    //public class AuthorLink
    //{
    //    public int AuthorId { get; set; }
    //    public int EnteredBy { get; set; }
    //    public DateTime EntryDate { get; set; }
    //    public int? ModifiedBy { get; set; }
    //    public DateTime? ModifiedDate { get; set; }
    //    public int? DeactivateBy { get; set; }
    //    public DateTime? DeactivateDate { get; set; }
    //    public string Authorname { get; set; }
    //    public int ProductId { get; set; }
    //}
}