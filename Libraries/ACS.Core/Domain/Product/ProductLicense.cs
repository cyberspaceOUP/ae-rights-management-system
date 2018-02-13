using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicense : BaseEntity, ILocalizedEntity
    {
        public string ProductLicensecode { get; set; }
        public int productid { get; set; }
        public int publishingcompanyid { get; set; }
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
        public DateTime Requestdate { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? effectivedate { get; set; }
        public int? contractperiodinmonth { get; set; }
        public DateTime? Expirydate { get; set; }
        public int Territoryrightsid { get; set; }
        public string Impressionwithindate { get; set; }
        public int? noofimpressions { get; set; }
        public string printquantitytype { get; set; }
        public int? printquantity { get; set; }
        public string RoyalityTerms { get; set; }
        //public decimal? PaymentAmount { get; set; }
        //public decimal? AdvancedAmount { get; set; }
        public string PaymentAmount { get; set; }
        public string AdvancedAmount { get; set; }
        public int? copiesforlicensor { get; set; }
        public string pricetype { get; set; }
        public int? Currencyid { get; set; }
        public decimal? price { get; set; }
        public string thirdpartypermission { get; set; }
        public string Remarks { get; set; }
        public string DeactivateRemarks { get; set; }
        public string LicenseStatus { get; set; }
        public string balanceqtycf { get; set; }
        public int? balanceqty { get; set; }
       



        #region Navigation Properties
        public virtual ProductMaster ProductLicenseProduct { get; set; }
        public virtual GeographicalMaster ProductLicenseCountry { get; set; }
        public virtual GeographicalMaster ProductLicenseState { get; set; }
        public virtual GeographicalMaster ProductLicenseCity { get; set; }
        public virtual TerritoryRightsMaster ProductLicenseTerritoryRights { get; set; }
        public virtual CurrencyMaster ProductLicenseCurrency { get; set; }
        public virtual PublishingCompanyMaster LicensePublishing { get; set; }

        public virtual ICollection<ProductLicenseUpdateDetails> IProductLicenseUpdateDetails { get; set; }
        public virtual ICollection<ProductLicenseAddendumLink> ProductLicenseAddendumLink  { get; set; }
        public virtual ICollection<ProductLicenseRoyality> PProductLicenseRoyality {get; set;}
        public virtual ICollection<ProductLicenseSubsidiaryRights> PProductLicenseSubsidiaryRights {get; set;}
        #endregion
    }
}
