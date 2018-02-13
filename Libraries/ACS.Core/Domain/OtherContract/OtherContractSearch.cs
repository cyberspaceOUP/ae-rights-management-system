///Create by Saddam on 19/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
namespace ACS.Core.Domain.OtherContract
{
  public partial  class OtherContractSearch : BaseEntity
    {
      public string SessionId { get; set; }
        public string Othercontractcode { get; set; }
        public string partyname { get; set; }

        public int? natureofserviceid { get; set; }
        public int? natureofsubserviceid { get; set; }
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
        public string PANNo { get; set; }


        public string ProjectTitle { get; set; }
        public string ProjectISBN { get; set; }

        public int? Contracttypeid { get; set; }



        public int? Periodofagreement { get; set; }


        public DateTime? Expirydate { get; set; }

        public int? Territoryrightsid { get; set; }

        public string Payment { get; set; }

        public int? paymentperiodid { get; set; }

        public string NatureOfWork { get; set; }

        public int? divisionid { get; set; }

        public int? ContractSignedByExecutiveid { get; set; }

        public string Remarks { get; set; }

        public int? Printrunquantity { get; set; }
        public string PrintRights { get; set; }
        public string electronicrights { get; set; }
        public string ebookrights { get; set; }
        public string cost { get; set; }
        public int? currencyid { get; set; }
        public string restriction { get; set; }


        public DateTime? RequestFromDate { get; set; }

        public DateTime? RequestToDate { get; set; }

        public DateTime? ContractFromDate { get; set; }

        public DateTime? ContractToDate { get; set; }

        public string ContractStatus { get; set; }

    }
}
