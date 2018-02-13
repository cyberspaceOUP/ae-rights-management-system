using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.AuthorContract
{
    public class AuthorContractModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ExecutiveCode { get; set; }
        public DateTime ContractEntryDate { get; set; }
        public DateTime ContractDate { get; set; }
        public int termsofcopyright { get; set; }
        public int periodOfAgreement { get; set; }
        public DateTime? ContractExpirydate { get; set; }
        public string BuyBack { get; set; }
        public string NatureofWork { get; set; }
        public string CopyRightOwner { get; set; }
        public int TerritoryId { get; set; }
        public string ThirdPartyPermission { get; set; }
        public bool Amendment { get; set; }
        public string AmendmentRemarks { get; set; }
        public string Restriction { get; set; }
        public string subjectMatterAndTreatment { get; set; }
        public string MinNoOfWords { get; set; }
        public string MaxNoOfWords { get; set; }
        public string MinNoOfPages { get; set; }
        public string MaxNoOfPages { get; set; }
        public string PriceType { get; set; }
        public string SeriesCode { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        public int? SeriesId { get; set; }
        public string mediumOfDelivery { get; set; }
        public int? MenuScriptDeliveryFormatId { get; set; }
        public string deliverySchedule { get; set; }
        public string ProductRemarks { get; set; }
        public int NoofAuthors { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public int? licenseId { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public string ContractStatus { get; set; }
        public string SeriesIds { get; set; }



        public int AuthorContractId { get; set; }

        public string Role { get; set; }
        public int flag { get; set; }

        public string[] DocumentName { get; set; }
        public string UploadFile { get; set; }

        public string DocumentId { get; set; }


        public int[] DocumentIds { get; set; }



        public IList<ContributorName> ContributorName = new List<ContributorName>();
        public IList<SupplyMaterialbyAuthor> SupplyMaterialbyAuthor = new List<SupplyMaterialbyAuthor>();
        public IList<AuthorContactDetails> AuthorContactDetails = new List<AuthorContactDetails>();
        public IList<AuthorSubsidiaryRights> AuthorSubsidiaryRights = new List<AuthorSubsidiaryRights>();
        public IList<ManuScriptFormatLink> ManuScriptFormatList = new List<ManuScriptFormatLink>();
    }
    public class ManuScriptFormatLink
    {
        public int MenuScriptId { get; set; }
    }
    public class ContributorName
    {
        public int Id { get; set; }
        public string Contributor { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
    }
    public class SupplyMaterialbyAuthor
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public DateTime materialDate { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
    }
    public class RoyaltySlab
    {
        public int Id { get; set; }
        public int subproductTypeId { get; set; }
        public int CopiesFrom { get; set; }
        public int CopiesTo { get; set; }
        public double Percentage { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public string SubProductType { get; set; }
        public int AuthorId { get; set; }
       
    }
    public class AuthorContactDetails
    {
        public int Id { get; set; }
        public int ContractTypeId { get; set; }
        public int AuthorId { get; set; }
        public int AuthorTypeId { get; set; }
        public int? PaymentperiodId { get; set; }
        public int? AuthorCopies { get; set; }
        public double? SendMoney { get; set; }
        public double? OneTimePayment { get; set; }
        public double? AdvanceRoyalty { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public IList<RoyaltySlab> RoyaltySlab = new List<RoyaltySlab>();
    }
    public class AuthorSubsidiaryRights
    {
        public int Id { get; set; }
        public int AuthorContractId { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public IList<SusidiaryRights> SusidiaryRights = new List<SusidiaryRights>();
        //public IList<SusidiaryRights> SusidiaryRights = new List<SusidiaryRights>();
    }
    public class SusidiaryRights
    {
        public int subsidiaryid { get; set; }
        public string Subsidiary { get; set; }
        public string AuthorName { get; set; }
        public int authorId { get; set; }
        public double Percentage { get; set; }
        public double OupPercentage { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
    }

    public class ContractAgreement
    {
        public int? ContractId { get; set; }
        public string SeriesCode { get; set; }
        public string productIds { get; set; }
        public string ContractStatus { get; set; }
        public string AgreementRemarks { get; set; }
        public string Cancellationremarks { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? PeriodOfAgreement { get; set; }
        public DateTime? SignedcontracDate { get; set; }
        public DateTime? contractRecieved { get; set; }
        public DateTime? AuthorCopiesSend { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? CotributorCopiessend { get; set; }
        public string Doc { get; set; }
        public string ContributorFileName { get; set; }
        public string AgreementFileName { get; set; }
        public string Deactivate { get; set; }
        public int EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? DeactivateBy { get; set; }
        public DateTime? DeactivateDate { get; set; }
        public int Id { get; set; }

        public IList<ContributorName> ContributorName = new List<ContributorName>();
    }

    public class AddendumUpload
    {
        public DateTime AddendumDate { get; set; }

        public string AddendumType { get; set; }

        public int? Periodofagreement { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string Remarks { get; set; }

        public int EnteredBy { get; set; }

        public string[] DocumentName { get; set; }

        public int? AuthorContrctId { get; set; }
      
        public string UploadFile { get; set; }

        public string SameAsEntery { get; set; }

        public string SeriesCode { get; set; }

        public int? Id { get; set; }
        public string AddendumCode { get; set; }

        public IList<AuthorContractRoyalityModel> AuthorContractRoyality = new List<AuthorContractRoyalityModel>();
   
    }

    public class AuthorContractRoyalityModel
    {
        public int? Id { get; set; }
        public int ProductSubTypeId { get; set; }
        public int copiesfrom { get; set; }
        public int copiesto { get; set; }
        public decimal percentage { get; set; }
        public int AuthorContractId { get; set; }
        public int AddendumDetailsId { get; set; }
        public int AuthorId { get; set; }
    }

    public class AuthorcontractDetail
    {
        public string authorcontractcode { get; set; }
        public string ContractDate { get; set; }
        public string territoryrights { get; set; }
        public string ContractEntryDate { get; set; }
        public string ContractExpiryDate { get; set; }
        public string productremarks { get; set; }
        public int AuthorContractId { get; set; }
        public string LanguageName { get; set; }
        public string executiveName { get; set; }
    }
    
   
}
