//Create By Saddam on 13/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;

namespace ACS.Core.Domain.RightsSelling
{
    public partial class RightsSellingMaster : BaseEntity, ILocalizedEntity
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
        public int? Language { get; set; }
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
        public int? ProuductId { get; set; }

        public string Print_Run_Quantity_Type { get; set; }
        public DateTime? FirstPublicationDate { get; set; }

        public virtual  GeographicalMaster Country { get; set; }
        public virtual GeographicalMaster State { get; set; }
        public virtual GeographicalMaster City { get; set; }

        public virtual LicenseeMaster Licensee { get; set; }

        public virtual ProductMaster ProductMaster { get; set; }

        public virtual ProductCategoryRightMaster ProductCategoryRightMaster { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
        public virtual TerritoryRightsMaster TerritoryRightsMaster { get; set; }

        public virtual AuthorContractOriginal AuthorContract { get; set; }

        public virtual ProductLicense ProductLicense { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }


        public virtual ICollection<RightsSellingLanguageMaster> RightsSellingLanguageMaster { get; set; }

        public virtual ICollection<RightsSellingRoyalty> RightsSellingRoyalty { get; set; }

    }
}
