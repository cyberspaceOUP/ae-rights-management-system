//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class CopyRightHolderMaster : BaseEntity, ILocalizedEntity
    {
        public string CopyRightHolderCode { get; set; }
        public string CopyRightHolderName { get; set; }
        public string ContactPerson { get; set;}
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string OtherCountry { get; set;}
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

        }
}
