using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.RightsSelling;
using ACS.Core.Domain.Product;

namespace ACS.Services.Master
{
    public partial interface ICommonListService
    {
        /// <summary>
        /// Gets all Department
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Department collection</returns>
        IList<DepartmentMaster> GetAllDepartments();

        /// <summary>
        /// Gets all Division
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Division collection</returns>
        IList<DivisionMaster> GetAllDivisions(int Id);

        /// <summary>
        /// Gets all SubDivision
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Division collection</returns>
        IList<DivisionMaster> GetAllSubDivisions();

         /// <summary>
        /// Gets all SubDivision By DivisionId
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>SubDivision collection</returns>
        IList<DivisionMaster> GetAllSubDivisionsbyDivisonId(DivisionMaster Division);

        /// <summary>
        /// Gets all Executive
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Executive collection</returns>
        IList<ExecutiveMaster> GetAllExecutive();

        /// <summary>
        /// Gets all Author
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Author collection</returns>
        IList<AuthorMaster> GetAllAuthor(string ProductId="");

        /// <summary>
        /// Gets all Product Category
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product Category collection</returns>
        IList<ProductCategoryMaster> GetAllProductCategory();

        /// <summary>
        /// Gets all Product Type List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns> Product Type collection</returns>
        IList<ProductTypeMaster> GetAllProductType();

        /// <summary>
        /// Gets all Sub Product Type List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Sub Product Type collection</returns>
        IList<ProductTypeMaster> GetAllSubProductType(ProductTypeMaster ProductType);

        IList<ProductTypeMaster> GetSubProductType();

        /// <summary>
        /// Gets all Author Suggesation
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Author Suggesation</returns>
        IList<AuthorMaster> GetAuthorSuggesationList(AuthorMaster AuthorMaster);

        /// <summary>
        /// Gets Imprint List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Imprint List</returns>
        IList<ImprintMaster> GetImprintList();

        /// <summary>
        /// Gets Language List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Language List</returns>
        IList<LanguageMaster> GetLanguageList();

        /// <summary>
        /// Gets Currency List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Currency List</returns>
        IList<CurrencyMaster> GetCurrencyList();

        /// <summary>
        /// Get Publishing Company List
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Publishing Company List</returns>
        IList<PublishingCompanyMaster> GetPublishingCompanyList();

        /// <summary>
        /// Get Pub Center List Publishing CompanyId
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Pub Center List</returns>
        IList<PubCenterMaster> GetPubCenterByCompanyIdList(PublishingCompanyMaster PublishingCompany);

        /// <summary>
        /// Get Series List By Division and Sub Division Id
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Series List</returns>
        IList<SeriesMaster> GetSeriesList(SeriesMaster SeriesMaster);
        
        /// <summary>
        /// Get the contact type list
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Series List</returns>
        IList<ContractMaster> GetContract();
        
        /// <summary>
        /// Get the terms of copyrights 
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Series List</returns>
        IList<PaymentPeriod> GetAllPamentPeriod();//Without Payment as per Schedule
        
        IList<PaymentPeriod> GetAllPamentPeriodList();

        IList<ContractType> GetAllContractType();

        IList<TypeOfRightsMaster> GetCopyRightsTerms();

        /// <summary>
        /// Get the terms of TerriteryRights created by dheeraj kumar sharma 
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Series List</returns>
        IList<TerritoryRightsMaster> TerriteryRights();

        IList<ServiceMaster> GetAllServiceList();

        IList<SubServiceMaster> GetAllSubServiceList();
        
        /// <summary>
        /// Get the terms of subsidiary List created by dheeraj kumar sharma 
        /// </summary>
        /// <param name="showHidden">Subsidiary list</param>
        /// <returns>Series List</returns>
        IList<SubsidiaryRightsMaster> SubsidiaryList();

        IList<AuthorType> GetAllAuthorTypeList();
        
        /// <summary>
        /// Get the terms of material supplied by the author Created by dheeraj kumar sharma
        /// </summary>
        /// <param name="showHidden">Subsidiary list</param>
        /// <returns> material supplied</returns>
        IList<SupplyMaterialMaster> MaterialSuppliedByAuthor();

        /// <summary>
        /// get the currency list Created by dheeraj kumar sharma
        /// </summary>
        /// <param name="showHidden">Currency list</param>
        /// <returns> List of Currency</returns>
        IList<CurrencyMaster> CurrencyList();

        /// <summary>
        /// get the menuscript format by dheeraj kumar sharma
        /// </summary>
        /// <param name="showHidden">Currency list</param>
        /// <returns> List of ManuscriptDeliveryFormat</returns>
        IList<ManuscriptDeliveryFormatMaster> ManuscriptDeliveryFormatList();

        IList<SubServiceMaster> GetAllSubServiceListByServiceId(int ServiceId);

        IList<ISBNBag> GetAllISBNByProductTypeId(int ProductTypeId);

        IList<ISBNBag> GetAllISBNBagList();

        ISBNBag GetTopOneISBNByProductTypeId(int ProductTypeId);

        /// <summary>
        /// create by Ankush on 10/08/2016
        /// Gets All Frequency List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Frequency List</returns>
        IList<FrequencyMaster> GetFrequencyList();

        IList<ProductMaster> GetProductISBNList();

        void InsertTicker(Ticker _Ticker);

        string TicketDuplicityCheck(Ticker _Ticker);

        Ticker GetTickerById(int Id);

        List<Ticker> GetTickerList();

        void UpdateTicker(Ticker _Ticker);

        void InsertUploadDocumentDetails(UploadDocument _UploadDocument);

        List<UploadDocument> getCommonUploadDocumentByMaster(string MasterName, int MasterId);

        UploadDocument getCommonUploadDocumentById(int Id);

        void DeleteUploadDocumentDetails(UploadDocument _UploadDocument);

        //added on 23 nov, 2017
        void InsertAuthorType(AuthorType _obj);

        void UpdateAuthorType(AuthorType _obj);

        //added on 04 Jan, 2018
        string CurrencyDuplicityCheck(CurrencyMaster _CurrencyMaster);

        List<CurrencyMaster> GetCurrencyMasterList();

        void InsertCurrencyMaster(CurrencyMaster _CurrencyMaster);

        CurrencyMaster GetCurrencyMasterById(int Id);

        void UpdateCurrencyMaster(CurrencyMaster _CurrencyMaster);


    }
}
