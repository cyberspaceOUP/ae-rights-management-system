using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Product
{
    public class ProductLinkModel
    {
        public int? ProductMasterId { get; set; }
        public int? AuthorContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public int? RightsSellingId { get; set; }
        public int? PermissionsOutboundId { get; set; }
        
        public string ProductCode { get; set; }
        public string AuthorContractCode { get; set; }
        public string ProductLicenseCode { get; set; }
        public string RightsSellingCode { get; set; }
        public string PermissionsOutboundCode { get; set; }

        public int? PermissionsInboundId { get; set; }
        public string PermissionsInboundCode { get; set; }
        public string TypeFor { get; set; }
    }


    public class SAPAgreement
    {
        public int? id { get; set; }
        public string AuthorName { get; set; }
        public string ProductCategory { get; set; }
    }



    public class ProductISBN
    {
        public int? Id { get; set; }
        public string ISBN { get; set; }
    }

    public class KitISBNModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public int[] ProductIds { get; set; }
        public int EnteredBy { get; set; }

        public int? Division { get; set; }
        public int? SubDivision { get; set; }
        public int? ProductCategory { get; set; }
        public string WorkingProduct { get; set; }
        public string SubWorkingProduct { get; set; }
        public Decimal? ProjectedPrice { get; set; }
        public int? ProjectedCurrency { get; set; }
        public string Type { get; set; }
        public string EntryDate { get; set; }
        public DateTime EntryDateforsort { get; set; }
        public string DivisionName { get; set; }
        public string SubDivisionName { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductTypeId { get; set; }
        public int? SubProductTypeId { get; set; }

        public int KitISBNId { get; set; }
        public int ProductId { get; set; }
        public string ProductISBN { get; set; }
        public string ProductISBNId { get; set; }
        public int KitProductLinkId { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string ProductTypeName { get; set; }
    }

  

    public class SAPAggrementList
    {
        public string OUPISBN { get; set; }
        public string SAPagreementNo { get; set; }
        public string AuthorCode { get; set; }
        public string AuthorName { get; set; }
        public string ProductCategory { get; set; }
    }

    public class SAPAggrementUpdate
    {

        public string OUPISBN { get; set; }
        public string SAPagreementNo { get; set; }
        public string AuthorCode { get; set; }
        public int? AuthorId { get; set; }
        public int Id { get;set;}
    }

    public class SAPAggrementModel
    {
        public int flag { get; set; }
    }


    public class KitISBNListModel
    {
        public string ProductCategory { get; set; }
        public string ISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string SubWorkingProduct { get; set; }
        public string Division { get; set; }
        public string SubDivision { get; set; }
        public int? Id { get; set; }
        public string Flag { get; set; }
        public string productcode { get; set; }
        public string ProductType { get; set; }
        public string AuthorName { get; set; }
        public string Type { get; set; }
        public string printquantity { get; set; }
        public string balanceqty { get; set; }
        public string noofimpressions { get; set; }
        public int ProductLicenseId { get; set; }
        public int AuthorContractId { get; set; }
        public string Check_NoOfImpressions { get; set; }
        public int ProductId { get; set; }
        public string ImpressionDateView { get; set; }
        public string QunatityPrintedView { get; set; }

        public int? ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public int LinkedProductId { get; set; }
        public string LinkedProductCode { get; set; }
        public string LinkedProductISBN { get; set; }

        public IList<ProductKitIsbn> ProductKitIsbn = new List<ProductKitIsbn>();
    }

    public class ProductKitIsbn
    {
        public int ProductLicenseId { get; set; }
        public int ProductId { get; set; }
        public int Balanceqty { get; set; }
        public int? NoOfImpressions { get; set; }
        public int ImpressionNo { get; set; }
        public DateTime ImpressionDate { get; set; }
        public int QunatityPrinted { get; set; }
        public int? ContractId { get; set; }
        public int KitISBNId { get; set; }
        public string Check_NoOfImpressions { get; set; }
        public int EnteredBy { get; set; }
       public string Unrestricted_Check { get; set; }
        
    }


}
