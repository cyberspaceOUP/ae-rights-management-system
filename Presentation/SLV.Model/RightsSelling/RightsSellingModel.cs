using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.RightsSelling
{

    public class RightsSellingModel
    {
        public string RightsSellingCode { get; set; }
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
        public DateTime? DateContract { get; set; }
        public int? ContractPeriod { get; set; }
        public DateTime? First_Impression_within_date { get; set; }
        public DateTime? DateExpiry { get; set; }
        public DateTime? Contract_Effective_Date { get; set; }
        public int? ProductCategory { get; set; }
        public string Will_be_material_be_translated { get; set; }
       /// public int? Language { get; set; }
      
        public int[] Language { get; set; }

        public string Print_Run_Quantity_Allowed { get; set; }
        public int? Number_of_Impression_Allowed { get; set; }
        public string Advance_Payment { get; set; }
        public int? Currency { get; set; }
        public string Payment_Term { get; set; }
        public string Payment_Amount { get; set; }
        public int? Territory_Rights { get; set; }
        public string Advance_Royalty_Amount { get; set; }
        public string RoyaltyType { get; set; }
        public string Royalty_Recurring { get; set; }
        public DateTime? Recurring_From_Period { get; set; }
        public DateTime? Recurring_To_Period { get; set; }
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int? Frequency { get; set; }
        public int EnteredBy { get; set; }
        public string ProuductCode { get; set; }
        public int? ProuductId { get; set; }

        public string Print_Run_Quantity_Type { get; set; }
        public DateTime? FirstPublicationDate { get; set; }

        public int? ID { get; set; }
        public IList<RightsSellingRoyaltyModel> RightsSellingRoyalty = new List<RightsSellingRoyaltyModel>();

    }

    public class RightsSellingRoyaltyModel
    {
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public int? subproducttypeid { get; set; }
        public Decimal? CopiesFrom { get; set; }
        public Decimal? CopiesTo { get; set; }
        public Decimal? Percentage { get; set; }
    }

    public class RightsSellingRoyalty_WITH_SubProductType
    {
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public string SubProductType { get; set; }
        public int? subproducttypeid { get; set; }
        public Decimal? CopiesFrom { get; set; }
        public Decimal? CopiesTo { get; set; }
        public Decimal? Percentage { get; set; }
    }

    public partial class RightsSellingUpdateModel
    {
        public string ContractStatus { get; set; }
        public DateTime? Date_of_agreement { get; set; }
        public DateTime? Signed_Contract_sent_date { get; set; }
        public DateTime? Signed_Contract_receiveddate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public string Contributor_Agreement { get; set; }
        public int? RightsSellingID { get; set; }
        public string RemarksUpdate { get; set; }
        public int EnteredBy { get; set; }
        public string[] DocumentName { get; set; }
        public string UploadFile { get; set; }

        //added by Saddam on 22/08/2016 for Update Rights Selling for admin
        public string RightsSellingCode { get; set; }
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
        public DateTime? DateContract { get; set; }
        public int? ContractPeriod { get; set; }
        public DateTime? First_Impression_within_date { get; set; }
        public DateTime? DateExpiry { get; set; }
        public DateTime? Contract_Effective_Date { get; set; }
        public int? ProductCategory { get; set; }
        public string Will_be_material_be_translated { get; set; }
       // public int? Language { get; set; }
        public string Print_Run_Quantity_Allowed { get; set; }
        public int? Number_of_Impression_Allowed { get; set; }
        public string Advance_Payment { get; set; }
        public int? Currency { get; set; }
        public string Payment_Term { get; set; }
        public string Payment_Amount { get; set; }
        public int? Territory_Rights { get; set; }
        public string Advance_Royalty_Amount { get; set; }
        public string RoyaltyType { get; set; }
        public string Royalty_Recurring { get; set; }
        public DateTime? Recurring_From_Period { get; set; }
        public DateTime? Recurring_To_Period { get; set; }
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int? Frequency { get; set; }
        
        public string ProuductCode { get; set; }
        public int? ProuductId { get; set; }
        public string UserType { get; set; }
        public IList<RightsSellingRoyaltyModel> RightsSellingRoyalty = new List<RightsSellingRoyaltyModel>();
        public string Type { get; set; }
        public int Id { get; set; }


        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? ContractperiodUpload { get; set; }
        public DateTime? Expirydate { get; set; }
        public int[] Language { get; set; }

        //ended by saddam

        //added by Prakash on 10 July, 2017
        public string Print_Run_Quantity_Type { get; set; }
        public DateTime? FirstPublicationDate { get; set; }

    }

    public partial class RightsSellingSearchModel
    {
        public int? Id { get; set; }
        public string RightsSellingCode { get; set; }
        public int? ProuductId { get; set; }
        public string ProductCode { get; set; }
        public int? ContractId { get; set; }
        public string AuthorContractCode { get; set; }
        public int? ProductLicenseId { get; set; }
        public string ProductLicensecode { get; set; }
        public string RequestDate { get; set; }
        public string DateContract { get; set; }
        public string DateExpiry { get; set; }
        public string OrganizationName { get; set; }
        public string Flag { get; set; }
        public string SessionId { get; set; }
        public string ReturnList { get; set; }

        public string ISBN { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }

        public string AuthorName { get; set; }
        public int? AuthorId { get; set; }
        public int? PublishingCompanyId { get; set; }
        public string PublishingCompanyCode { get; set; }

        public string Licenseecode { get; set; }
         
         public string ContactPerson { get; set; }
         public string Address { get; set; }
         public string Country { get; set; }
         public string State { get; set; }
         public string City { get; set; }
         public string Pincode { get; set; }
         public string LicenseeMobile { get; set; }
         public string Email { get; set; }
         public string URL { get; set; }
        
         public string Will_be_material_be_translated { get; set; }
         public string Language { get; set; }
         public string Print_Run_Quantity_Allowed { get; set; }
         public string Number_of_Impression_Allowed { get; set; }
         public string Advance_Payment { get; set; }
         public string CurrencyName { get; set; }
         public string Payment_Term { get; set; }
         public string Payment_Amount { get; set; }
         public string TerritoryRights { get; set; }
         public string Advance_Royalty_Amount { get; set; }
         public string Royalty_Recurring { get; set; }
         public string Recurring_From_Period { get; set; }
         public string Recurring_To_Period { get; set; }
         public string Status { get; set; }
         public string Remarks { get; set; }
         public string RoyaltyType { get; set; }
         public string Frequency { get; set; }
         public string ContractStatus { get; set; }
         public string Date_of_agreement { get; set; }
         public string Signed_Contract_sent_date { get; set; }
         public string Signed_Contract_receiveddate { get; set; }
         public string CancellationDate { get; set; }
         public string Cancellation_Reason { get; set; }
         public string Contributor_Agreement { get; set;}
         public string Pending_Remarks { get; set; }
         public string Effectivedate { get; set; }
         public string Contractperiodinmonth { get; set; }

         public string typeName { get; set; }
         public string CopiesFrom { get; set; }
         public string CopiesTo { get; set; }
         public string Percentage { get; set; }
         public string RoyaltySlab { get; set; }

         //public IList<ProductTypeMaster> TYPENAME { get; set; }

         public DateTime? DateContractForSort { get; set; }
         public DateTime? DateExpiryForSort { get; set; }

        
    }

    public class ProductTypeMaster
    {
        public string typeName { get; set; }

    }

    public class RightsSellingViewModel
    {
        public string RightsSellingCode { get; set; }
        public int LicenseeID { get; set; }
        public string Licenseecode { get; set; }
        public string OrganizationName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string OtherCountry { get; set; }
        public string State { get; set; }
        public string OtherState { get; set; }
        public string City { get; set; }
        public string OtherCity { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string RequestDate { get; set; }
        public string DateContract { get; set; }
        public int? ContractPeriod { get; set; }
        public string First_Impression_within_date { get; set; }
        public string DateExpiry { get; set; }
        public string Contract_Effective_Date { get; set; }
        public string ProductCategory { get; set; }
        public string Will_be_material_be_translated { get; set; }
        public string Language { get; set; }
        public string Print_Run_Quantity_Allowed { get; set; }
        public int? Number_of_Impression_Allowed { get; set; }
        public string Advance_Payment { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public string Payment_Term { get; set; }
        public string Payment_Amount { get; set; }
        public string Territory_Rights { get; set; }
        public string Advance_Royalty_Amount { get; set; }
        public string RoyaltyType { get; set; }
        public string Royalty_Recurring { get; set; }
        public string Recurring_From_Period { get; set; }
        public string Recurring_To_Period { get; set; }
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Frequency { get; set; }
        public int EnteredBy { get; set; }
        public string ProuductCode { get; set; }
        public int? ProuductId { get; set; }

        public int? CountryId { get; set; }
        public int? Stateid { get; set; }
        public int? Cityid { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? CurrencyId { get; set; }
        public int? LanguageId { get; set; }
        public int? TerritoryRightsId { get; set; }
        public int? FrequencyID { get; set; }

        public string RemarksInsert { get; set; }

        //added by Prakash on 10 July, 2017
        public string Print_Run_Quantity_Type { get; set; }
        public string FirstPublicationDate { get; set; }
        public string LicenseeName { get; set; }
    }

    public class RightsSellingPaymentTaggingModelData
    {
        public IList<RightsSellingPaymentTaggingModel> RightsSellingPaymentTagging = new List<RightsSellingPaymentTaggingModel>();
    }


    public class RightsSellingPaymentTaggingModel
    {
        public int PaymentTaggingId { get; set; }
        public int? ContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public int? subproducttypeid { get; set; }
        public double? Percentage { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string BankName { get; set; }
        public double? Amount { get; set; }
        public double? AuthorAmount { get; set; }
        public double? OupAmount { get; set; }
        public int EnteredBy { get; set; }

        public int AuthorId { get; set; }
        public int RightsSellingId { get; set; }
        public int PermissionsOutboundId { get; set; }
        public int PublishingCompanyId { get; set; }

        public double? WithHoldingTax { get; set; }
        public double? ConverisonRate { get; set; }

        public string RightsSellingCode { get; set; }
        public string PermissionsOutboundCode { get; set; }

        public string AuthorContractCode { get; set; }
        public string ProductLicensecode { get; set; }
        public string AuthorName { get; set; }
        public string CompanyName { get; set; }
        public string EntryDate { get; set; }
        public DateTime EntryDateForSort { get; set; }
        public string Type { get; set; }
        public int? DeactivateBy { get; set; }
    }

}
