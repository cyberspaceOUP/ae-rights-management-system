//Create by Saddam 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SLV.Model.Common
{
  public class PermissionsOutboundModel
    {
      public int id { get; set; }
      public int productid { get; set; }
      public int ContactId { get; set; }
      public string UserProfile { get; set; }

      public string PermissionsOutboundCode { get; set; }
      public int LicenseeID { get; set; }
      public string Licenseecode { get; set; }
      public string OrganizationName { get; set; }
      public string ContactPerson { get; set; }
      public string Address { get; set; }
      public int? CountryId { get; set; }
      public string OtherCountry { get; set; }
      public int? Stateid { get; set; }
      public string OtherState { get; set; }
      public int? Cityid { get; set; }
      public string OtherCity { get; set; }
      public string Pincode { get; set; }
      public string Mobile { get; set; }
      public string Email { get; set; }
      public string URL { get; set; }
      public DateTime? RequestDate { get; set; }
      public string LicenseePublicationTitle { get; set; }
      public DateTime? DateOfPermission { get; set; }
      public int? PermissionPeriod { get; set; }
      public DateTime? DateExpiry { get; set; }
      public string RequestMaterial { get; set; }
      public string Will_be_material_be_translated { get; set; }
      public string Will_be_material_be_adepted { get; set; }
      public int? LanguageId { get; set; }
      public string Extent { get; set; }
      public int? TerritoryId { get; set; }
      public DateTime? DateOfInvoice { get; set; }
      public string InvoiceApplicable { get; set; }
      public string InvoiceNo { get; set; }
      public int? InvoiceCurrency { get; set; }
      public string InvoiceValue { get; set; }
      public string InvoiceDescription { get; set; }

      public string Copies_To_Be_Received { get; set; }
      public int? NumberOfCopies { get; set; }
      public string PaymentReceived { get; set; }

      public string Remarks { get; set; }
     public string ProductCode { get; set; }

      public int EnteredBy { get; set; }
      public string ContractStatus { get; set; }

      public string DocumentFile { get; set; }
      public string[] Documentname { get; set; }
     // public string PaymentReceived { get; set; }

      public int? PaymentAmount { get; set; }


      public int? CurrencyId { get; set; }


      public DateTime? Date_of_agreement { get; set; }
      public DateTime? Signed_Contract_sent_date { get; set; }
      public DateTime? Signed_Contract_receiveddate { get; set; }
      public DateTime? CancellationDate { get; set; }
      public string Cancellation_Reason { get; set; }
      public string Contributor_Agreement { get; set; }
      public int? PermissionsOutboundID { get; set; }

      public string Type { get; set; }
      public string PendingRemarks { get; set; }


      public int[] Language { get; set; }

      public DateTime? Effectivedate { get; set; }
      public int? Contractperiodinmonth { get; set; }

      public IList<SupplyTypeOfRights> SupplyTypeOfRights = new List<SupplyTypeOfRights>();

    }
     public class SupplyTypeOfRights
    {
        public int Id { get; set; }
        public int? TypeofRightsId { get; set; }
          public string Quantity { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
    }

     public class PermissionsoutboundDetials
     {
         public int Id { get; set; }
         public int PermissionsoutboundId { get; set; }
         public string permissionsoutboundcode { get; set; }
         public int licenseeid { get; set; }
         public string licenseecode { get; set;}
         public string organizationname { get; set; }
        public string contactperson { get; set; }
        public string address { get; set; }
        public int? countryid { get; set;}
        public string Country { get; set; }
        public string State { get; set; }
         public string City { get; set; }
         public string othercountry { get; set; }
         public int? stateid { get; set; }
         public string otherstate { get; set; }
         public int? cityid { get; set; }
         public string othercity { get; set; }
         public string pincode { get; set; }
         public string mobile { get; set; }
         public string email { get; set; }
         public string url { get; set; }
         public DateTime? requestdate { get; set; }
         public string RequestDateView { get; set; }
         public string licenseepublicationtitle { get; set; }
         public DateTime? dateofpermission { get; set; }
         public string DateOfPermissionView { get; set; }
         public int? permissionperiod { get; set; }
         public DateTime? dateexpiry { get; set; }
         public string DateExpiryView { get; set; }
         public string requestmaterial { get; set; }
         public string will_be_material_be_translated { get; set; }
         public string will_be_material_be_adepted { get; set; }
         public int? languageid { get; set; }
         public string languagename { get; set; }
         public string extent { get; set; }
         public int? territoryid { get; set; }


         public string territoryrights { get; set; }
         public DateTime? dateofinvoice { get; set; }
         public string DateOfInvoiceView { get; set; }
         public string invoiceapplicable { get; set; }
         public string invoiceno { get; set; }
         public int? InvoiceCurrency { get; set; }
         public string InvoiceCurrencyName { get; set; }
         public string InvoiceCurrencySymbol { get; set; }
         public string invoicevalue { get; set; }
         public string invoicedescription { get; set; }



         public string copies_to_be_received { get; set; }
         public int? numberofcopies { get; set; }
         public string paymentreceived { get; set; }
         public string remarks { get; set; }


         public int[] DocumentIds { get; set; }
         public string[] Documentname { get; set; }

         public string DocumentFile { get; set; }
         public int EnteredBy { get; set; }

         public string contractstatus { get; set; }
         public int? paymentamount { get; set; }
         public string currencyname { get; set; }

         public string Date_of_agreement { get; set; }
         public string Signed_Contract_sent_date { get; set; }
         public string Signed_Contract_receiveddate { get; set; }
         public string CancellationDate { get; set; }
         public string Cancellation_Reason { get; set; }
         public string Contributor_Agreement { get; set; }
         //public int? PermissionsOutboundID { get; set; }

         public string PendingRemarks { get; set; }

         public string Effectivedate { get; set; }
         public int? Contractperiodinmonth { get; set; }
         public string Expirydate { get; set; }

         public string WorkingProduct { get; set; }
         public string WorkingSubProduct { get; set; }
         public string AuthorName { get; set; }

         //Add ISBN for Invoice
         public string ISBN { get; set; }
     }

    public class PermissionOutBoundSearch
    {
        public int Id { get; set; }
        public DateTime? RequestFromDate { get; set; }
        public DateTime? RequestToDate { get; set; }
        public DateTime? PermissionFromDate { get; set; }
        public DateTime? PermissionToDate { get; set; }
        public DateTime? DateExpiry { get; set; }
        public int? LanguageId { get; set; }
        //public string Extent { get; set; }
        public int? TerritoryId { get; set; }
        public DateTime? InvoiceFromdate { get; set; }
        public DateTime? InvoiceTodate { get; set; }
        public int? InvoiceNo { get; set; }
        public int? NumberOfCopies { get; set; }
        public string TypeOfrightsId { get; set; }
        public string ProductCode { get; set; }
        public string ContractCode { get; set; }
        
        public string PermissionsOutboundCode { get; set; }
       
        public string ProductId { get; set; }
        public int? ContractId { get; set; }
        public int PermissionsoutboundId { get; set; }
        public string flag { get; set; }

        public string ReturnList { get; set; }
        public string SessionId { get; set; }


        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string AuthorName { get; set; }

        public string ISBN { get; set; }


        public string LicenseeCode { get; set; }
        public string LicenseeName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string RequestDateView { get; set; }
        public string LicenseePublicationTitle { get; set; }
        public string RequestMaterial { get; set; }
        public string TypeOfRights { get; set; }
        public string Will_be_material_be_translated { get; set; }
        public string Will_be_material_be_adepted { get; set; }
        public string Language { get; set; }
        public string Extent { get; set; }
        public string TerritoryRights { get; set; }
        public string DateOfInvoice { get; set; }
        public string InvoiceApplicable { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceCurrency { get; set; }
        public string InvoiceValue { get; set; }
        public string InvoiceDescription { get; set; }
        public string Copies_To_Be_Received { get; set; }
        public string NoOfCopies { get; set; }
        public string Remarks { get; set; }
        public string ContractStatus { get; set; }
        public string PaymentReceived { get; set; }
        public string PaymentCurrency { get; set; }
        public string PaymentAmount { get; set; }
        public string Date_of_agreement { get; set; }
        public string Signed_Contract_sent_date { get; set; }
        public string Signed_Contract_receiveddate { get; set; }
        public string CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public string Contributor_Agreement { get; set; }
        public string PendingRemarks { get; set; }
        public string Effectivedate { get; set; }
        public string Contractperiodinmonth { get; set; }
        public string ExpiryDateView { get; set; }

        public string LicenseCode { get; set; }
        public int? ProductMasterId { get; set; }
        public int? LicenseId { get; set; }

        public DateTime? Date_of_agreementForSort { get; set; }
        public DateTime? ExpiryDateViewForSort { get; set; }

    }
    public class PaymentTagging
    {
        public int AuthorId { get; set; }
        public string AuthorContractCode { get; set; }
        public string AuthorName { get; set; }
        public int authorcontractid { get; set; }
        public string subsidiaryrights { get; set; }
        public int id { get; set; }

        public int PublishingCompanyId { get; set; }
        public string PublishingCompanyName { get; set; }
        public int ProductLicenseId { get; set; }
        public string ProductLicensecode { get; set; }

        public string InvoiceNo { get; set; }
        public string InvoiceValue { get; set; }
        public string InvoiceCurrencySymbol { get; set; }

        public string ISBN { get; set; }
        public string AuthorCode { get; set; }
        public string SAPagreementNo { get; set; }

        public string AuthorSAPCode { get; set; }
        public string AuthorPercentage { get; set; }

      //  public int authorid { get; set; }

    }
}
