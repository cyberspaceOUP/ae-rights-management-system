//Create by Saddam on 02/05/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{
    public partial class PubCenterMaster: BaseEntity, ILocalizedEntity
    {
        public string PubcenterCode { get; set; }
        public int? PublishingCompanyid { get; set; }
        public virtual PublishingCompanyMaster PublishingCompanyM { get; set; }
        public string CenterName { get; set; }
        public string ContactPerson { get; set; }
        public string PublishingCompanyDivision { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string OtherCountry { get; set; }
        public int Stateid { get; set; }
        public string OtherState { get; set; }
        public int Cityid { get; set; }
        public string OtherCity { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
