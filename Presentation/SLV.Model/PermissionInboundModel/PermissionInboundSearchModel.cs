using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.PermissionInboundModel
{
    public class PermissionInboundSearchModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public string TypeFor { get; set; }
        public string ProductCode { get; set; }
        public string AssetsType { get; set; }
        public string InvoiceNumber { get; set; }
        public string CopyRightHolderName { get; set; }
        public string TypeCode { get; set; }

        //public string ProductId { get; set; }
        public string TypeProdyctId { get; set; }
        public int? ContractId { get; set; }

        public string Flag { get; set; }

       public string SessionId { get; set; }

        //private string SessionId { get; set; }

        public string ReturnList { get; set; }

        public string WorkingProduct { get; set; }
        public string ISBN { get; set; }

        public string AuthorName { get; set; }

        public string WorkingSubProduct { get; set; }

        public int productId { get; set; }
        public string TypeForValue { get; set; }


        public string ImageVideo { get; set; }
        public string CopyRight { get; set; }


        public string productcategory { get; set; }

        public string InBoundType { get; set; }
        public string LicenseCode { get; set; }

        public string ContractTypes { get; set; }
        public string imagevideobankid { get; set; }
        public string Description { get; set; }
        public string invoiceno { get; set; }
        public string invoicevalue { get; set; }
        public string invoicedate { get; set; }
        public string printquantity { get; set; }
        public string permissionexpirydate { get; set; }
        public string weblink { get; set; }
        public string creditlines { get; set; }
        public string usage { get; set; }
        public string Remarks { get; set; }
        public string partyname { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public string CopyRightHolder { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string StatusName { get; set; }
        public string AssetSubType { get; set; }
        public string AssestDescription { get; set; }
        public string Restriction { get; set; }
        public string RunGrantedQty { get; set; }
        public string SubLicensing { get; set; }
        public string Fee { get; set; }

        public string Territoryrights { get; set; }
        public string Extent { get; set; }
        public string Gratiscopytobesent { get; set; }
        public string Noofcopy { get; set; }
        public string OriginalSource { get; set; }



        public string Acknowledgementline { get; set; }
        public string InboundRemarks { get; set; }
        public string DateRequstDetails { get; set; }

        public string EditorialonlyType { get; set; }


        public string CurrencyNameCopyRights { get; set; }

        public string ProductTypeName { get; set; }
        public string ProductSubTypeName { get; set; }
        public DateTime? permissionexpirydateForSort { get; set; }

        public string QunatityPrinted { get; set; }
        public string quantity { get; set; }
        public string BalanceCounts { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }

        public int? InboundOthersId { get; set; }
        public int? CopyRightHolderId { get; set; }
       
    }


    public class PermissionInBoundImageVideoBankDetails {
        public string ContractTypes { get; set; }
        public string imagevideobankid { get; set; }
        public string Description { get; set; }

        public string invoiceno { get; set; }
        public string invoicevalue { get; set; }
        public string invoicedate { get; set; }
        public string printquantity { get; set; }
        public string permissionexpirydate { get; set; }
        public string weblink { get; set; }
        public string creditlines { get; set; }
        public string Remarks { get; set; }
        public int? ImageVideoBankLinkId { get; set; }
        public string partyname { get; set; }
        public int? PartyNameId { get; set; }

        public int? ImageVideoBankDataId { get; set; }


        public int? Id { get; set; }
        public int? CopyRightHolderId { get; set; }
        public int? InboundOthersId { get; set; }
        public string AssetSubType { get; set; }
        public string AssestDescription { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string CopyRightHolder { get; set; }
        public string StatusName { get; set; }
        public string TotalQty { get; set; }
        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }
        public string Editorial_Only_Type { get; set; }

        public int? LinkId { get; set; }
        public int? DataId { get; set; }
        public int? OthersId { get; set; }
        public string Type { get; set; }
        public int DeactivateBy { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
    }



}
