//Create by Saddam on 29/04/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Master
{


    public partial class PublishingCompanyMaster : BaseEntity, ILocalizedEntity
    {
        public string PublishingCompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
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
        public string Email { get; set; }
        public string Website { get; set; }

        #region Navigation Properties
        public virtual GeographicalMaster PublishingCompanyCountry { get; set; }
        public virtual GeographicalMaster PublishingCompanyState { get; set; }
        public virtual GeographicalMaster PublishingCompanyCity { get; set; }

        #endregion
    }
}
