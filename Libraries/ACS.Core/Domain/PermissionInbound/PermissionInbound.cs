using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.PermissionInbound
{
    public class PermissionInbound : BaseEntity
    {
        public int ProductId { get; set; }
        public string TypeFor { get; set; }
        public int? AuthorContractId { get; set; }
        public int? ProductLicenseId { get; set; }
        public string AssetsType { get; set; }
        public string Code { get; set; }
        #region Navigation
        public virtual ICollection<PermissionInboundImageVideoBank> PermissionInboundImageVideoBank { get; set; }
        public virtual ICollection<PermissionInboundOthers> PermissionInboundOthers { get; set; }
        //public virtual ICollection<PermissionInboundCopyRightHolderMaster> PermissionInboundCopyRightHolderMaster { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ProductLicense ProductLicense { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

        #endregion
    }

    public class PermissionInboundImageVideoBank : BaseEntity
    {
        public int PermissionInboundId { get; set; }
        public int ImageBankId { get; set; }
        public int? ImageBankDataId { get;set; }


        #region Navigation
        public virtual ICollection<PermissionInboundImageVideoBankData> PermissionInboundImageVideoBankData { get; set; }
        public virtual OtherContractImageBank OtherContractImageBank { get; set; }
        public virtual PermissionInbound PermissionInbound { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        #endregion

    }
    public class PermissionInboundImageVideoBankData : BaseEntity
    {
        public int IVBId { get; set; }
        public string ContractTypes { get; set; }
        public string imagevideobankid { get; set; }
        public string Description { get; set; }
        public string invoiceno { get; set; }
        public double invoicevalue { get; set; }
        public DateTime? invoicedate { get; set; }
        public int? printquantity { get; set; }
        public DateTime? permissionexpirydate { get; set; }
        public string weblink { get; set; }
        public string creditlines { get; set; }
        public string Remarks { get; set; }
        public string EditorialonlyType { get; set; }

        public string usage { get; set; }

        public int? ImageBankPartyId { get; set; }

        public int? CurrencyId { get; set; }

        public virtual PermissionInboundImageVideoBank PermissionInboundImageVideoBank { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }

    }
    public class PermissionInboundOthers : BaseEntity
    {

       public int PermissionInboundId { get; set; }

        public int? AssetSubTypeId { get; set; }
        public string AssetDescription { get; set; }
        public int statusId { get; set; }
        public string Restriction { get; set; }
        public string SubLicensing { get; set; }
        public double Fee { get; set; }
        public int? CurrencyId { get; set; }
        public string Acknowledgementline { get; set; }
        public string InboundRemarks { get; set; }
        public string Extent { get; set; }
        public string Gratiscopytobesent { get; set; }
        public int? Noofcopy { get; set; }
        public string OriginalSource { get; set; }
        public string InvoiceNumber { get; set; }
        public double? Invoicevalue { get; set; }
        public DateTime? PermissionExpirydate { get; set; }

        public int? TerritoryRightsId { get; set; }

        //public string Remarks { get; set; }

        #region Navigation
        public virtual PermissionInbound PermissionInbound { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
        public virtual AssetSubType AssetSubType { get; set; }
        public virtual StatusMaster StatusMaster { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual ICollection<OtherContractDateRequest> OtherContractDateRequest { get; set; }
        public virtual ICollection<PermissionInboundOthersRightsLink> PermissionInboundOthersRightsLink { get; set; }

        public virtual ICollection<PermissionInboundCopyRightHolderMaster> PermissionInboundCopyRightHolderMaster { get; set; }

        public virtual TerritoryRightsMaster TerritoryRightsMaster { get; set; }
        #endregion
    }

    public class PermissionInboundOthersRightsLink : BaseEntity
    {
        public int PIOID { get; set; }
        public int RightsId { get; set; }
        public string status { get; set; }
        public string RunGranted { get; set; }
        public Int32? Number { get; set; }
        #region Navigation
        public virtual PermissionInboundOthers PermissionInboundOthers { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual OtherRightsMaster OtherRightsMaster { get; set; }
        #endregion

    }
    public class OtherContractDateRequest : BaseEntity
    {
        public string dateOf { get; set; }
        public DateTime? dateValue { get; set; }
        public int PIOID { get; set; }
        #region navigation
        public virtual PermissionInboundOthers PermissionInboundOthers { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        #endregion

    }


    public class OtherRightsMaster : BaseEntity
    {
        public string RightsName { get; set; }

    }
    public partial class PermissionInboundCopyRightHolderMaster : BaseEntity, ILocalizedEntity
    {
        public string CopyRightHolderCode { get; set; }
        public string CopyRightHolderName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
       // public int PermissionInboundId { get; set; }

        public int InboundOthersId { get; set; }
        public string OtherCountry { get; set; }
        public int Stateid { get; set; }
        public string OtherState { get; set; }
        public int Cityid { get; set; }
        public string OtherCity { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string BankAddress { get; set; }
        public string IFSCCode { get; set; }
        public string PANNo { get; set; }
        public string VendorCOde { get; set; }
        public virtual PermissionInboundOthers PermissionInboundOthers { get; set; }

    }
    public partial class OtherContractImageBankInbound : BaseEntity, ILocalizedEntity
    {
        public int? Printrunquantity { get; set; }
        public string PrintRights { get; set; }
        public string electronicrights { get; set; }
        public string ebookrights { get; set; }
        public string restriction { get; set; }

        public virtual ICollection<VideoImageBankInbound> VideoImageBankInbound { get; set; }

    }

    public partial class VideoImageBankInbound : BaseEntity, ILocalizedEntity
    {
        public int ImageBankId { get; set; }
        public string Type { get; set; }
        public string ShortName { get; set; }
        public string Fullname { get; set; }
        public int CurrencyId { get; set; }
        public double Cost { get; set; }
        public virtual OtherContractImageBankInbound OtherContractImageBankInbound { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }

    }
    public partial class PermissionInboundSearchHistory : BaseEntity, ILocalizedEntity
    {
        public string SessionId { get; set; }
        public string Assetstype { get; set; }
        public string AuthorContractCode { get; set; }
        public string LicenseCode { get; set; }
        public string ProductCode { get; set; }
        public string InboundPermissionCode { get; set; }
        public int? CopyrightHoplderId { get; set; }
        public int? partyid { get; set; }
        public int? Status { get; set; }
        public string Restriction { get; set; }
        public string Ebookrights { get; set; }
        public string ElectronicRights { get; set; }
        public string PrintRights { get; set; }
        public int? AssetSubTypeId {get;set;}
        public string Gratiscopytobesent { get; set; }
        public string RestrictionOther { get; set; }
        public string SubLicensing { get; set; }
        public double? Fee { get; set; }
        public int? CurrencyId { get; set; }
        public string Extent { get; set; }
        public string OriginalSource { get; set; }
        public string InvoiceNumber { get; set; }
        public double? Invoicevalue { get; set; }
        public string DateRequest { get; set; }
        public DateTime? PermissionExpirydate { get; set; }

        public string Flag { get; set; }

        public string AuthorName { get; set; }
        public string ISBN { get; set; }
        public string ProductName { get; set; }
        public string SubProductName { get; set; }

        public string For { get; set; }
    }




    public partial class PermissionInboundUpdate : BaseEntity, ILocalizedEntity
    {
        public string Contractstatus { get; set; }
        public DateTime? SignedContractSentDate { get; set; }
        public DateTime? SignedContractReceived_Date { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public int PermissionInboundId { get; set; }
        public string Remarks { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? Contractperiodinmonth { get; set; }
        public DateTime? Expirydate { get; set; }
        public virtual PermissionInbound PermissionInbound { get; set; }
        public virtual ICollection<PermissionInboundDocuments> PermissionInboundDocuments { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }

    public partial class PermissionInboundDocuments : BaseEntity, ILocalizedEntity
    {

        public string Documentname { get; set; }
        public string DocumentFile { get; set; }
        public int PermissionsInboundUpdateId { get; set; }

        public virtual PermissionInboundUpdate PermissionInboundUpdate { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }

}
