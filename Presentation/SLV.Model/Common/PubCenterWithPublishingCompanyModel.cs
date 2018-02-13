using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public class PubCenterWithPublishingCompanyModel
    {
        public int Id { get; set; }

        public int PublishingCompanyId { get; set; }

        public string PublishingCompanyName { get; set; }

        public string CenterName { get; set; }

        public string PublishingCompanyDivision { get; set; }

        public string ContactPerson { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        //Added by Ankush on 18/07/2016 For Display in List
        public string Mobile { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string CountryName { get; set; }

        public string StateName { get; set; }

        public string CityName { get; set; }
        //End By Ankush

        //Added by Suranjana on 19/07/2016
        public string Flag { get; set; }
        //Ended by Suranjana

        //Added by Suranjana on 26/07/2016
        public string IsEditable { get; set; }
        //Ended by Suranjana
    }
}
