//Create by Saddam on 09/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class AuthorMasterDetail  
    {
        public int Id { get; set; }

        public string AuthorCode { get; set; }
        public string Type { get; set; }
        public string AuthorName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }



       
        public string Address { get; set; }
        public string ResidencyStatus { get; set;}
       
        public string AuthorCountry { get; set; }

        public string AuthorState { get; set; }

        public string AuthorCity { get; set; }
        public int CountryId { get; set; }
        public string OtherCountry { get; set; }
        public int StateId { get; set; }
        public string OtherState { get; set; }
        public int CityId { get; set; }
        public string OtherCity { get; set; }



        public string PinCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string PANNo { get; set; }
        public string   AdharCardNo { get; set; }
        public string DateOfBirth { get; set; }
        public string DeathDate { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSECode { get; set; }
        public string InstituteCompanyName { get; set; }
        public string AffiliationDesignation { get; set; }
        public string AffiliationDepartment { get; set; }
        public string AffiliationAddress { get; set; }
    
        public string AffiliationCountry { get; set; }
       
        public string AffiliationState { get; set; }
    
        public string AffiliationCity { get; set; }
        public int AffiliationCountryId { get; set; }
        public string AffiliationOtherCountry { get; set; }
        public int AffiliationStateId { get; set; }
        public string AffiliationOtherState { get; set; }
        public int AffiliationCityId { get; set; }
        public string AffiliationOtherCity { get; set; }



        public string AffiliationPinCode { get; set; }
        public string AffiliationPhone { get; set; }
        public string AffiliationEmail { get; set; }
        public string AffiliationWebSite { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryRelation { get; set; }
        public string BeneficiaryAddress { get; set; }
       
        public string BeneficiaryCountry { get; set; }
      
        public string BeneficiaryState { get; set; }
      
        public string BeneficiaryCity { get; set; }
        public int BeneficiaryCountryId { get; set; }
        public string BeneficiaryOtherCountry { get; set; }
        public int BeneficiaryStateId { get; set; }
        public string BeneficiaryOtherState { get; set; }
        public int BeneficiaryCityId { get; set; }
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
     
        public string NomineeCountry { get; set; }
     
        public string NomineeState { get; set; }
      
        public string NomineeCity { get; set;}

        public int NomineeCountryId { get; set; }
        public string NomineeOtherCountry { get; set; }
        public int NomineeStateId { get; set; }
        public string NomineeOtherState { get; set; }
        public int NomineeCityId { get; set; }
        public string NomineeOtherCity { get; set; }


        public string NomineePinCode { get; set; }
        public string NomineeEmail { get; set; }
        public string NomineePhone { get; set; }
        public string NomineeMobile { get; set; }
        public string NomineeFax { get; set; }
        public string NomineePanNo { get; set; }

        public string Remark { get; set; }

        public string Flag { get; set; }

        public string C_Auth { get; set; }
        public string S_Auth { get; set; }
        public string City_Auth { get; set; }


        public string C_Aff { get; set; }
        public string S_Aff { get; set; }
        public string City_Aff { get; set; }

        public string C_Benff { get; set; }
        public string S_Benff { get; set; }
        public string City_Benff { get; set; }


        public string C_Nomi { get; set; }
        public string S_Nomi { get; set; }
        public string City_Nomi { get; set; }

        public string DepartmentName { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }


        public string AffiliationCountryName { get; set; }
        public string AffiliationStateName { get; set; }
        public string AffiliationCityName { get; set; }


        public string NomineeAccountNo { get; set; }
        public string NomineeBranchName { get; set; }
        public string NomineeBankName { get; set; }
        public string NomineeIFSECode { get; set; }

        public string AuthorSAPCode { get; set; }
    }
}
