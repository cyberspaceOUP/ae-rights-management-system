using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.Product
{
    public partial class ProductLicenseUpdate
    {

        public class ClsRoyaltySlab
        {
            public string typeName { get; set; }
            public int copiesfrom { get; set; }
            public int copiesto { get; set; }
            public decimal percentage { get; set; }

        }

        public class ClsSubsidiaryRights
        {
            public string SubsidiaryRights { get; set; }
            public decimal publisherpercentage { get; set; }
            public decimal ouppercentage { get; set; }

        }

        public class ClsFileDetails
        {
            public int Id { get; set; }
            public string FileName { get; set; }
            public string UploadFileName { get; set; }

        }


        public int Id { get; set; }
        public int productid { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string Requestdate { get; set; }
        public string ContractDate { get; set; }
        public string effectivedate { get; set; }
        public int contractperiodinmonth { get; set; }
        //public string Expirydate { get; set; }
        public string TerritoryRights { get; set; }
        public string Impressionwithindate { get; set; }
        public int noofimpressions { get; set; }
        public string printquantitytype { get; set; }
        public int printquantity { get; set; }
        public string RoyalityTerms { get; set; }
        public string PaymentAmount { get; set; }
        public string AdvancedAmount { get; set; }
        public int copiesforlicensor { get; set; }
        public string pricetype { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }
        public decimal price { get; set; }
        public string thirdpartypermission { get; set; }
        public string Remarks { get; set; }

        public string LicensorCopiesSentDate { get; set; }
        public decimal EFilesCost { get; set; }
        public string EFilesRequestDate { get; set; }
        public string EFilesReceivedDate { get; set; }
        public string Mode { get; set; }
        public string AgreementDate { get; set; }
        public string Effectivedate { get; set; }
        public string Contractperiodinmonth { get; set; }
        public string Expirydate { get; set; }

        public string ProductLicensecode { get; set; }
        //public List<ClsRoyaltySlab> RoyaltySlabDetails { get; set; }
        //public List<ClsSubsidiaryRights> SubsidiaryRightsDetails { get; set; }
    }
}
