//Create by Saddam on 13/06/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SLV.Model.Common
{
    public class Contract_Signed_By_Executive
    {
        public int Id { get; set; }
        public string executiveName { get; set; }
        public string othercontractcode { get; set; }
        public string partyname { get; set; }
        //public string ContactPerson { get; set; }
        public int natureofserviceid { get; set; }
        public int? natureofsubserviceid { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public string OtherCountry { get; set; }
        public int Stateid { get; set; }
        public string OtherState { get; set; }
        public int Cityid { get; set; }
        public string OtherCity { get; set; }
        public string Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PANNo { get; set; }

        public DateTime? Requestdate { get; set; }

        public string RequestdateValue { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectISBN { get; set; }

        public int Contracttypeid { get; set; }

        public string ContractDate { get; set; }

        public int? Periodofagreement { get; set; }


        public DateTime? Expirydate { get; set; }
        public string Expiredate { get; set; }

        public int Territoryrightsid { get; set; }

        public string Payment { get; set; }

        public int? paymentperiodid { get; set; }

        public string NatureOfWork { get; set; }

        public int divisionid { get; set; }

        public int? ContractSignedByExecutiveid { get; set; }

        public string Remarks { get; set; }

        public string[] Documentname { get; set; }
        public string documentfile { get; set; }
        public int? othercontractid { get; set; }

        public int? Printrunquantity { get; set; }
        public string PrintRights { get; set; }
        public string electronicrights { get; set; }
        public string ebookrights { get; set; }
        public string cost { get; set; }
        public int? currencyid { get; set; }
        public string restriction { get; set; }
        //public int othercontractid { get; set; }

        public int EnteredBy { get; set; }

        public int OtherContractIdId { get; set; }

        public string ForImageBank { get; set; }


        public string RequestFromDate { get; set; }

        public string RequestToDate { get; set; }

        public string ContractFromDate { get; set; }

        public string ContractToDate { get; set; }

        public int[] DocumentIds { get; set; }


        public string Service { get; set; }

        public string SubService { get; set; }

        public string OtherContractCountry { get; set; }

        public string OtherContractState { get; set; }




        public string OtherContractCity { get; set; }


        public string contractname { get; set; }

        public string divisionname { get; set; }
        public string territoryrights { get; set; }
        public string paymenttype { get; set; }
        public string currencyname { get; set; }

        public string PendingRequest { get; set; }

        public string UpdateRight { get; set; }

        public string Contractstatus { get; set; }
        public DateTime? SignedContractSentDate { get; set; }
        public DateTime? SignedContractReceived_Date { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public int OtherContractLinkId { get; set; }


        public string[] DocumentnameLink { get; set; }
        public string documentfileLink { get; set; }
        public int othercontractLinkid { get; set; }

        public int[] DocumentlinkIds { get; set; }

        public string Status { get; set; }

        public string PendingRemarks { get; set; }

        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? Contractperiodinmonth { get; set; }

        public string AgreementDateValue { get; set; }
        public string EffectivedateValue { get; set; }
        public string ExpirydateValue { get; set; }
        public string SignedContractSentDateValue { get; set; }
        public string SignedContractReceived_DateValue { get; set; }
        public string CancellationDateValue { get; set; }

        public string PaymentAmount { get; set; }
        public int? CurrencyMasterId { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }

        public IList<VideoImageBankModel> VideoImageBank { get; set; }


        public int[] Division { get; set; }

    }
    public class VideoImageBankModel
    {
        public int ImageBankId { get; set; }
        public string Type { get; set; }
        public string ShortName { get; set; }
        public string Fullname { get; set; }
        public int CurrencyId { get; set; }
        public double Cost { get; set; }
    }
}
