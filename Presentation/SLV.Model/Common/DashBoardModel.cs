//Create by Saddam on 04/07/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SLV.Model.Common
{
    public class DashBoardModel
    { 
        public int Id { get; set; }
        public string ProductLicensecode { get; set; }
        public int ProductLicenseId { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public string WorkingProduct { get; set; }
        public string WorkingSubProduct { get; set; }
        public string Expirydate { get; set; }
        public string AddendumDate { get; set; }
        public string ISBN { get; set; }
        public int? balanceqty { get; set; }
        public string AddendumCode { get; set; }
        public string AuthorName { get; set; }
        public string ProjectedPublishingDate { get; set; }
        public string Contract_Code { get; set; }

        public int ContractId { get; set; }

        public string Territoryrights { get; set; }
        public int RightsSellingID { get; set; }

        public string OrganizationName { get; set; }
        public string ContactPerson { get; set; }

        public int PermissionsOutboundId { get; set; }
        public string PermissionsOutboundCode { get; set; }
        
        //Added by Suranjana on 21/07/2016
        public string ContractDate { get; set; }
        //Ended by Suranjana


        //Added By Saddam

        public string Flag { get; set; }
        public string EntryDate { get; set; }
        public string ExecutiveName { get; set; }
        public string typename { get; set; }
        public string Status { get; set; }

        public string othercontractcode { get; set; }
        public string partyname { get; set; }
        public string Expiredate { get; set; }
        public string contractname { get; set; }
        public string Contractstatus { get; set; }
        public string LicenseeCode { get; set; }

        public string Code { get; set; }
        public string AssetsType { get; set; }
        public string DateOfInvoice { get; set; }
        public int? BalancePercentage { get; set; }
        public int? BalanceCounts { get; set; }

        public string Amount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string Frequency { get; set; }
        public string PublishingCompany { get; set; }
        public string FinalProductName { get; set; }

        //added by prakash on 12 april, 2017
        public string RightsSellingCode { get; set; }
        public string divisionName { get; set; }
        public string AgreementDate { get; set; }
        public int ExecutiveId { get; set; }
       
      
    }
    
}
