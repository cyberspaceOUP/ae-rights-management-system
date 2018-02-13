//Create by Ankush on 18/07/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public partial class PublishingCompanyModel
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    }

    public partial class CopyRightHolderModel
    {
        public string CopyRightHolderCode { get; set; }
        public string CopyRightHolderName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string OtherCountry { get; set; }
        public int Stateid { get; set; }
        public string StateName { get; set; }
        public string OtherState { get; set; }
        public int Cityid { get; set; }
        public string CityName { get; set; }
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

    }

    public class LicenseeModel
    {
        public string Licenseecode { get; set; }
        public string OrganizationName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string OtherCountry { get; set; }
        public int Stateid { get; set; }
        public string StateName { get; set; }
        public string OtherState { get; set; }
        public int Cityid { get; set; }
        public string CityName { get; set; }
        public string OtherCity { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string URL { get; set; }
    }
}
