///Create by Saddam on 14/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;


namespace ACS.Core.Domain.OtherContract
{
 

    public partial class OtherContractMaster : BaseEntity, ILocalizedEntity
    {
        public string othercontractcode { get; set; }
        public string partyname { get; set; }
        //public string ContactPerson { get; set; }
        public int natureofserviceid   { get; set; }
        public int? natureofsubserviceid { get; set; }
        public string Address  { get; set; }
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
        public string ProjectTitle { get; set; }
        public string ProjectISBN  { get; set; }

        public int Contracttypeid   { get; set; }

        public string ContractDate { get; set; }

        public int Periodofagreement  { get; set; }


        public string Expirydate { get; set; }

        public int Territoryrightsid { get; set; }

        public string Payment { get; set; }

        public int? paymentperiodid { get; set; }

        public string NatureOfWork { get; set; }

       // public int divisionid { get; set; }

        public int? ContractSignedByExecutiveid  { get; set; }

        public string Remarks { get; set; }

        public string PaymentAmount { get; set; }

        public int? CurrencyMasterId { get; set; }
        
        #region Navigation Properties
        public virtual GeographicalMaster OtherContractCompanyCountry { get; set; }
        public virtual GeographicalMaster OtherContractCompanyState { get; set; }
        public virtual GeographicalMaster OtherContractCompanyCity { get; set; }

        public virtual ContractType OtherContractContractType { get; set; }

        public virtual TerritoryRightsMaster OtherContractTerritoryRightsMaster { get; set; }

        public virtual PaymentPeriod OtherContractPaymentPeriod { get; set; }


        public virtual DivisionMaster OtherContractDivisionMaster { get; set; }

        public virtual ExecutiveMaster OtherContractExecutiveMaster { get; set; }

        public virtual ICollection<OtherContractDocuments> OtherContractDocuments { get; set; }

        public virtual ICollection<OtherContractDivisionLink> OtherContractDivisionLink { get; set; }


        #endregion
    }
}
