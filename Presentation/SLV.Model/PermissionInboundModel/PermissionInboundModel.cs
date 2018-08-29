using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.PermissionInboundModel
{
    public class PermissionInboundModel 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public char TypeFor { get; set; }
        public int ImageBankId { get; set; }
        public int? LicenseId { get; set; }
        public string AssetsType { get; set; }
        public int? AuthorContractId { get; set; }
        public int EnteredBy { get; set; }
        public string DocFileName { get; set; }

       
        public PermissionsInboundDataModel PermissionsInboundDataModel { get; set; }
        public IList<PermissionRightsObject> PermissionRightsObject { get; set; }
        public IList<DateRequestObject> DateRequestObject { get; set; }
    }
    public class PermissionsInboundDataModel
    {
        public string AssetsType { get; set; }
        public int CopyRightHolder { get; set; }
        public int PartyName { get; set; }
        public int AssetSubType { get; set; }
        public string AssetDescription { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int InboundId { get; set; }
        public string Restriction { get; set; }
        public string SubLicensing { get; set; }
        public double Fee { get; set; }
        public int? CurrencyValue { get; set; }
        public string Acknowledgementline { get; set; }
        public string InboundRemarks { get; set; }
        public string Extent { get; set; }
        public string Gratiscopytobesent { get; set; }
        public int? Noofcopy { get; set; }
        public string OriginalSource { get; set; }
        public string InvoiceNumber { get; set; }
        public double InvoiceValue { get; set; }
        public DateTime? PermissionExpirydate { get; set; }

        public int? TerritoryRights { get; set; }
    }
   
    public class PermissionInboundImageVideoBankModel
    {
        public int Id { get; set; }
        public int PermissionInboundId { get; set; }
        public int PartyName { get; set; }
        public IList<PermissionInboundImageVideoBankDataModel> PermissionInboundImageVideoBankDataModel { get; set; }
    }


    public class PermissionInboundImageVideoBankDataModel 
    {
        public int IVBId { get; set; }
        public string ContractTypes { get; set; }
        public int imagevideobankid { get; set; }
        public string Description { get; set; }
        public string invoiceno { get; set; }
        public double invoicevalue { get; set; }
        public DateTime invoicedate { get; set; }
        public int printquantity { get; set; }
        public DateTime permissionexpirydate { get; set; }
        public string weblink { get; set; }
        public string creditlines { get; set; }
        public string status { get; set; }
      }



   


    public class PendingRequestPermissionsInbound
    {
        public int Id { get; set; }
        public string Contractstatus { get; set; }
        public DateTime? SignedContractSentDate { get; set; }
        public DateTime? SignedContractReceived_Date { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public string PendingRemarks { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? Contractperiodinmonth { get; set; }
        public DateTime? Expirydate { get; set; }

        public string DocumentFile { get; set; }

        public string[] Documentname {get; set;}

        public string UpdateRight { get; set; }

        public int EnteredBy { get; set; }
        public int[] DocumentIds { get; set; }



       
        public string ContractTypes { get; set; }
        public string imagevideobankid { get; set; }
        public string Description { get; set; }
        public string invoiceno { get; set; }
        public double invoicevalue { get; set; }
        public DateTime? invoicedate { get; set; }
        public int printquantity { get; set; }
        public DateTime? permissionexpirydate { get; set; }
        public string weblink { get; set; }
        public string creditlines { get; set; }
        public string Remarks { get; set; }
        public string EditorialonlyType { get; set; }

        public int? ImageVedioId { get; set; }

     //   public int ImageBankId { get; set; }


        public string CopyRightHolderName { get; set; }
        public string ContactPerson { get; set; }
        public string CopyRightHolderCode { get; set; }
        public string Mobile { get; set; }
        public string CopyRightHolderAddress { get; set; }
        public string CopyRightHolderEmail { get; set; }
        public string CopyRightHolderURL { get; set; }
        public string CopyRightHolderAccountNo { get; set; }
        public string CopyRightHolderBankName { get; set; }
        public string CopyRightHolderBankAddress { get; set; }
        public string CopyRightHolderIFSCCode { get; set; }
        public string CopyRightHolderPANNo { get; set; }
        public string Pincode { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public int? Status { get; set; }
        public int? AssetSubType { get; set; }
        public string AssetDescription { get; set; }
        public string Extent { get; set; }
        public string Gratiscopytobesent { get; set; }
        public int? Noofcopy { get; set; }
        public string OriginalSource { get;set;}
        public string Restriction { get; set; }
        public string[] PermissionsInboundRightsModel { get; set; }
        public string[] PrintRunGrantedForPrint { get; set; }
        public string[] NumberPrint { get; set; }
        public string SubLicensing { get; set;}
        public double? Fee { get; set; }
        public int? CurrencyValue { get; set; }
        public string InvoiceNumber { get; set; }
        public double? InvoiceValue { get; set; }
        public DateTime? PermissionExpirydate { get; set; }
        public DateTime[] DateRequest { get; set; }
        public string Acknowledgementline { get; set; }
        public string InboundRemarks { get; set; }
        public int? InboundOthersId { get; set; }




        public int CopyRightHolderId { get; set; }
      
        public int ProductId { get; set; }
        public char TypeFor { get; set; }
        public int ImageBankId { get; set; }
        public int? LicenseId { get; set; }
        public string AssetsType { get; set; }
        public int? AuthorContractId { get; set; }
      
        public string DocFileName { get; set; }


        public int hid_ImageVideoBankId { get; set; }

        public int hid_CopyrightholderId { get; set; }
        public string Usage { get; set; }

        public string PartyName { get; set; }

        public int? ImageVideoCurrency { get; set; }

        public int? TerritoryRights { get;set;}

        public PermissionsInboundDataModel PermissionsInboundDataModel { get; set; }
        public IList<PermissionRightsObject> PermissionRightsObject { get; set; }
        public IList<DateRequestObject> DateRequestObject { get; set; }


        public string Code { get; set; }
    }

    public class DateRequestObject
    {
        public int Id { get; set; }
        public string DateOf { get; set; }
        public DateTime? DateValue { get; set; }

    }
    public class PermissionRightsObject
    {
        public int Id { get; set; }
        public int? RightsId { get; set; }
        public string Status { get; set; }
        public string RunGranted { get; set; }
        public int Number { get; set; }

    }

    ////--------------Manage (Copy Existing Permissions Of Other Product)
    public class CopyExistingPermissionInboundModel
    {
        public string InboundCode { get; set; }
        public int ProductId { get; set; }
        public string TypeFor { get; set; }
        public string AssetsType { get; set; }
        public int EnteredBy { get; set; }

        public IList<IVcheckedListModel> IVcheckedList { get; set; }
        public IList<OthercheckedListModel> OthercheckedList { get; set; }
    }

    public class IVcheckedListModel
    {
        public int? InboundId { get; set; }
        public int? LinkId { get; set; }
        public int? DataId { get; set; }
    }

    public class OthercheckedListModel
    {
        public int? InboundId { get; set; }
        public int? InboundOthersId { get; set; }
        public int? CopyRightHolderId { get; set; }
    }
    ////--------------end Manage (Copy Existing Permissions Of Other Product)

}
