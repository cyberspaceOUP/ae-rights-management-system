//Create By Saddam on 01/08/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;



namespace ACS.Core.Domain.PermissionsOutbound
{
    public partial class PermissionsOutboundMaster : BaseEntity, ILocalizedEntity
    {
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

        public int productid { get; set; }
        public int ContactId { get; set; }
        public string Type { get; set; }

        public virtual GeographicalMaster Country { get; set; }
        public virtual GeographicalMaster State { get; set; }
        public virtual GeographicalMaster City { get; set; }

        public virtual LicenseeMaster Licensee { get; set; }

        public virtual LanguageMaster Language { get; set; }
        public virtual TerritoryRightsMaster TerritoryRightsMaster { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }


        public virtual ProductMaster ProductMaster { get; set; }

        public virtual ICollection<PermissionsOutboundTypeOfRightsMaster> PermissionsOutboundTypeOfRightsMaster { get; set; }

        public virtual ICollection<PermissionsOutboundLanguageMaster> PermissionsOutboundLanguageMaster { get; set; }

    }
}
