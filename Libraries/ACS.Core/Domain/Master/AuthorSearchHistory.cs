//Create by Saddam on 19/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
  public partial  class AuthorSearchHistory  : BaseEntity
    {
      public string SessionId { get; set; }
        public string AuthorCode { get; set; }
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ResidencyStatus { get; set; }
        public int? CountryId { get; set; }
        public string OtherCountry { get; set; }
        public int? StateId { get; set; }
        public string OtherState { get; set; }
        public int? CityId { get; set; }
        public string OtherCity { get; set; }
        public string PinCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PANNo { get; set; }
        public string AdharCardNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DeathDate { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSECode { get; set; }
        public string InstituteCompanyName { get; set; }
        public string AffiliationDesignation { get; set; }
        public string AffiliationDepartment { get; set; }
        public string AffiliationAddress { get; set; }
        public int? AffiliationCountryId { get; set; }
        public string AffiliationOtherCountry { get; set; }
        public int? AffiliationStateId { get; set; }
        public string AffiliationOtherState { get; set; }
        public int? AffiliationCityId { get; set; }
        public string AffiliationOtherCity { get; set; }
        public string AffiliationPinCode { get; set; }
        public string AffiliationPhone { get; set; }
        public string AffiliationEmail { get; set; }
        public string AffiliationWebSite { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryRelation { get; set; }
        public string BeneficiaryAddress { get; set; }
        public int? BeneficiaryCountryId { get; set; }
        public string BeneficiaryOtherCountry { get; set; }
        public int? BeneficiaryStateId { get; set; }
        public string BeneficiaryOtherState { get; set; }
        public int? BeneficiaryCityId { get; set; }
        public string BeneficiaryOtherCity { get; set; }
        public string BeneficiaryPinCode { get; set; }
        public string BeneficiaryEmail { get; set; }
        public string BeneficiaryPhone { get; set; }
        public string BeneficiaryMobile { get; set; }
        public string BeneficiaryFax { get; set; }
        public string BeneficiaryPanNo { get; set; }
        public string BeneficiaryAccountNo { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string BeneficiaryBranchName { get; set; }
        public string BeneficiaryIFSECode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeAddress { get; set; }
        public int? NomineeCountryId { get; set; }
        public string NomineeOtherCountry { get; set; }
        public int? NomineeStateId { get; set; }
        public string NomineeOtherState { get; set; }
        public int? NomineeCityId { get; set; }
        public string NomineeOtherCity { get; set; }
        public string NomineePinCode { get; set; }
        public string NomineeEmail { get; set; }
        public string NomineePhone { get; set; }
        public string NomineeMobile { get; set; }
        public string NomineeFax { get; set; }
        public string NomineePanNo { get; set; }
        public string Remark { get; set; }

        public string AuthorSAPCode { get; set; }
    }
}
