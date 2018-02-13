using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractOriginal : BaseEntity, ILocalizedEntity
    {

        /*
         ==============================================================
         * Contract Information
         * ===============================================================
         */
        public string AuthorContractCode { get; set; }
        public string ExecutiveCode { get; set; }
        public int ProductId { get; set; }
        public int? SeriesId { get; set; }
        public int TermsOfCopyright { get; set; }
        
        public int NoOfAuthors { get; set; }
        public DateTime ContractEntryDate { get; set; }
        public DateTime ContractDate { get; set; }
        public int contractperiodinmonth{ get; set; }
        public DateTime? ContractExpiryDate { get; set; }
        public string BuyBack { get; set; }
        public string NatureOfWork { get; set; }
        public string CopyrightOwner { get; set; }
        public int Territoryrightsid{ get; set; }
        public string thirdpartypermission{ get; set; }
        public bool Amendment { get; set; }
        public string AmendmentRemarks { get; set; }
        public string Restriction { get; set; }
        public string SeriesCode { get; set; }

        /*
         ==============================================================
         * Product Information
         ===============================================================
         */
        public string subjectMatterAndTreatment { get; set; }
        public string MinNoOfwords { get; set; }
        public string MaxNoOfwords { get; set; }
        public string MinNoOfPages { get; set; }
        public string MaxNoOfPages { get; set; }
        //public bool? CopyrightOwner { get; set; }
        public string PriceType { get; set; }
        public Double? Price { get; set; }
        public int? CurrencyId { get; set; }
        public string MediumOfdelivery { get; set; }
        public int? ManuscriptId { get; set; }
        public string Deliveryschedule { get; set; }
        public string ProductRemarks { get; set; }
        public string Status { get; set; }
        public int? LicenseId { get; set; } 
        public int? AuthorContractAgreement_Id{get;set;}

        public virtual ICollection<AuthorContractContributor> AuthorContactContibutor { get; set; }
        public virtual ICollection<AuthorContractmaterialdetails> AuthorContractmaterialdetails { get; set; }
        public virtual ICollection<AuthorContractauthordetails> AuthorContractauthordetails { get; set; }
        public virtual ICollection<AuthorContractSubsidiaryRights> AuthorContractSubsidiaryRights { get; set; }
        public virtual ICollection<AuthorContractMenuscriptDeliveryLink> AuthorContractMenuscriptDeliveryLink { get; set; }
        public virtual ProductLicense ProductLicense { get; set; }
        //public virtual AuthorContractAgreement AuthorContractAgreement { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual SeriesMaster SeriesMaster { get; set; }
        public virtual TerritoryRightsMaster TerritoryRightsMaster { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
        public virtual ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormatMaster { get; set; }
      }

    public class AuthorContractMenuscriptDeliveryLink : BaseEntity, ILocalizedEntity
    {
        public int AuthorContractId { get; set; }
        public int ManuscriptId { get; set; }
        public virtual ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormatMaster { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
