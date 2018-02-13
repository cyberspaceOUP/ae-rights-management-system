//Create by Saddam on 13/06/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SLV.Model.Common
{
    public class OtherContractDetailsModel
    {
      public int? Id { get; set; }
      
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


        public string Expirydate { get; set; }

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


        public int? EnteredBy { get; set; }

  
        public string RequestFromDate { get; set; }

        public string RequestToDate  { get; set; }
      
      public string ContractFromDate  { get; set; }

        public string ContractToDate  { get; set; }

        public string contractdate { get; set; }

        public string contractname { get; set; }



        public string OtherContractCountry { get; set; }
        public string OtherContractState { get; set; }
        public string OtherContractCity { get; set; }

        public string requestdate { get; set; }
        public string territoryrights { get; set; }

        public string paymenttype { get; set; }
        public string divisionname { get; set; }
        public string executivename { get; set; }

        public string currencyname { get; set; }

        public string Service { get; set; }
        public string SubService { get; set; }

        public string Flag { get; set; }

        public string SessionId { get; set; }
        public string ReturnList { get; set; }

        public string Contractstatus { get; set; }
        public string Cancellation_Reason { get; set; }
        public string Status { get; set; }
        public string Pending_Remarks { get; set; }
        public string AgreementDate { get; set; }
        public string Effectivedate { get; set; }
        public string Contractperiodinmonth { get; set; }
        public string SignedContractSentDate { get; set; }
        public string SignedContractReceived_Date { get; set; }
        public string CancellationDate { get; set; }


        public DateTime? contractdateForSort { get; set; }
        public DateTime? ExpirydateForSort { get; set; }

    }
}
