using ACS.Core.Caching;
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.RightsSelling;

namespace ACS.Services.Master
{
    public partial class CommonListService : ICommonListService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string COUNTRIES_ALL_KEY = "ACS.Department.all-{0}";
        #endregion

        #region Fields

        private readonly IRepository<DepartmentMaster> _DepartmentRepository;
        private readonly IRepository<DivisionMaster> _DivisionRepository;
        private readonly IRepository<ExecutiveMaster> _ExecutiveRepository;
        private readonly IRepository<AuthorMaster> _AuthorRepository;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryRepository;
        private readonly IRepository<ProductTypeMaster> _ProductTypeRepository;
        private readonly IRepository<ImprintMaster> _ImprintRepository;
        private readonly IRepository<LanguageMaster> _LanguageRepository;
        private readonly IRepository<CurrencyMaster> _CurrencyRepository;
        private readonly IRepository<PublishingCompanyMaster> _PublishingCompanyRepository;
        private readonly IRepository<PubCenterMaster> _PubCenterRepository;
        private readonly IRepository<SeriesMaster> _SeriesRepository;
        private readonly IRepository<ContractMaster> _contractMaster;
        private readonly IRepository<TypeOfRightsMaster> _TypeOfRightsMaster;
        private readonly IRepository<PaymentPeriod> _PaymentPeriod;
        private readonly IRepository<ContractType> _ContractType;
        private readonly IRepository<TerritoryRightsMaster> _TerritoryRightsMaster;
        private readonly IRepository<ServiceMaster> _ServiceMaster;
        private readonly IRepository<SubsidiaryRightsMaster> _SubsidiaryRightsMaster;
        private readonly IRepository<SupplyMaterialMaster> _SupplyMaterialMaster;
        private readonly IRepository<CurrencyMaster> _CurrencyMaster;
        private readonly IRepository<ManuscriptDeliveryFormatMaster> _ManuscriptDeliveryFormatMaster;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<AuthorType> _AuthorType;
        private readonly IRepository<SubServiceMaster> _SubServiceMaster;
        private readonly IRepository<ExecutiveDivisionLink> _ExecutiveDivisionLink;
        private readonly IRepository<ProductAuthorLink> _ProductAuthorLink;
        private readonly IRepository<ISBNBag> _ISBNBag;
        private readonly IRepository<FrequencyMaster> _FrequencyRepository;
        private readonly IRepository<ProductCategoryRightMaster> _ProductCategoryRightMaster;
        private readonly IRepository<ProductMaster> _ProductRepository;
        private readonly IRepository<Ticker> _TickerRepository;
        private readonly IRepository<UploadDocument> _UploadDocumentRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="countryRepository">Country repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        public CommonListService
            (
                ICacheManager cacheManager,
                IRepository<DepartmentMaster> DepartmentRepository,
                IRepository<DivisionMaster> DivisionRepository,
                IRepository<ExecutiveMaster> ExecutiveRepository,
                IRepository<AuthorMaster> AuthorRepository,
                IRepository<ProductCategoryMaster> ProductCategoryRepository,
                IRepository<ProductTypeMaster> ProductTypeRepository,
                IRepository<ImprintMaster> ImprintRepository,
                IRepository<LanguageMaster> LanguageRepository,
                IRepository<CurrencyMaster> CurrencyRepository,
                IRepository<PublishingCompanyMaster> PublishingCompanyRepository,
                IRepository<PubCenterMaster> PubCenterRepository,
                IRepository<SeriesMaster> SeriesRepository,
                IRepository<ContractMaster> contractMaster,
                IRepository<TypeOfRightsMaster> TypeOfRightsMaster,
                IRepository<PaymentPeriod> PaymentPeriod,
                IRepository<ContractType> ContractType,
                IRepository<TerritoryRightsMaster> TerritoryRightsMaster,
                IRepository<ServiceMaster> ServiceMaster,
                IRepository<SubsidiaryRightsMaster> SubsidiaryRightsMaster,
                IRepository<SupplyMaterialMaster> SupplyMaterialMaster,
                IRepository<AuthorType> AuthorType,
                IRepository<CurrencyMaster> CurrencyMaster,
                IRepository<ManuscriptDeliveryFormatMaster> ManuscriptDeliveryFormatMaster,
                IRepository<SubServiceMaster> SubServiceMaster,
                IRepository<ExecutiveDivisionLink> ExecutiveDivisionLink,
                IRepository<ISBNBag> ISBNBag,
                IRepository<ProductAuthorLink> ProductAuthorLink,

                IRepository<FrequencyMaster> FrequencyRepository

                , IRepository<ProductCategoryRightMaster> ProductCategoryRightMaster

                 , IRepository<ProductMaster> ProductRepository
             , IRepository<Ticker> TickerRepository
             , IRepository<UploadDocument> UploadDocument

            )
        {
            this._cacheManager = cacheManager;
            this._DepartmentRepository = DepartmentRepository;
            this._DivisionRepository = DivisionRepository;
            this._ExecutiveRepository = ExecutiveRepository;
            this._AuthorRepository = AuthorRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._ProductTypeRepository = ProductTypeRepository;
            this._ImprintRepository = ImprintRepository;
            this._LanguageRepository = LanguageRepository;
            this._CurrencyRepository = CurrencyRepository;
            this._PublishingCompanyRepository = PublishingCompanyRepository;
            this._PubCenterRepository = PubCenterRepository;
            this._SeriesRepository = SeriesRepository;
            this._contractMaster = contractMaster;
            this._TypeOfRightsMaster = TypeOfRightsMaster;
            this._PaymentPeriod = PaymentPeriod;
            this._ContractType = ContractType;
            this._TerritoryRightsMaster = TerritoryRightsMaster;
            this._ServiceMaster = ServiceMaster;
            this._SubsidiaryRightsMaster = SubsidiaryRightsMaster;
            this._SupplyMaterialMaster = SupplyMaterialMaster;
            this._AuthorType = AuthorType;
            this._CurrencyMaster = CurrencyMaster;
            this._ManuscriptDeliveryFormatMaster = ManuscriptDeliveryFormatMaster;
            this._SubServiceMaster = SubServiceMaster;
            this._ExecutiveDivisionLink = ExecutiveDivisionLink;
            this._ISBNBag = ISBNBag;
            this._ProductAuthorLink = ProductAuthorLink;

            _FrequencyRepository = FrequencyRepository;
            this._ProductCategoryRightMaster = ProductCategoryRightMaster;

            this._ProductRepository = ProductRepository;
            this._TickerRepository = TickerRepository;
            this._UploadDocumentRepository = UploadDocument;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets all countries
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<DepartmentMaster> GetAllDepartments()
        {
            //string key = string.Format(COUNTRIES_ALL_KEY);
            //return _cacheManager.Get(key, () =>
            //{
            var query = _DepartmentRepository.Table;
            var Departments = query.Where(d => d.DepartmentName != null && d.Deactivate == "N").OrderBy(c => c.DepartmentName)
                .ToList();

            return Departments;
            //});

        }

        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<DivisionMaster> GetAllDivisions(int Id)
        {   
            var _divisionLink = _ExecutiveDivisionLink.Table.Where(i => i.executiveid == Id && i.Deactivate == "N").ToList();
            var _Division = _DivisionRepository.Table.Where(d => d.parentdivisionid == null && d.Deactivate == "N").OrderBy(c => c.divisionName).ToList();

            var query = _Division.Join(_divisionLink, s => s.Id, v => v.divisionid, (s, v) => new DivisionMaster
            {

                Id = s.Id,

                divisionName = s.divisionName

            }).ToList();
            return query.ToList();




        }

        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<DivisionMaster> GetAllSubDivisions()
        {
            var _Division = _DivisionRepository.Table.Where(d => d.parentdivisionid != null && d.Deactivate == "N").OrderBy(c => c.divisionName).ToList();
            return _Division;

        }


        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Sub Division collection</returns>
        public virtual IList<DivisionMaster> GetAllSubDivisionsbyDivisonId(DivisionMaster Division)
        {
            return _DivisionRepository.Table.Where(d => d.parentdivisionid == Division.Id && d.Deactivate == "N").OrderBy(c => c.divisionName).ToList();

        }

        /// <summary>
        /// create by Saddam on 05/05/2016
        /// Gets all Executive
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<ExecutiveMaster> GetAllExecutive()
        {
            //var query = _ExecutiveRepository.Table;
            //var Executive = query.OrderBy(c => c.executiveName)
            //    .ToList();
            //return Executive;

            var Executive = _ExecutiveRepository.Table.Where(d => d.executiveName != null && d.Deactivate == "N" && d.block == "N").OrderBy(c => c.executiveName).ToList();
            return Executive;

        }


        /// <summary>
        /// create by Saddam on 09/05/2016
        /// Gets all Executive
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<AuthorMaster> GetAllAuthor(string ProductId = "")
        {
            if (ProductId == "")
            {
                return _AuthorRepository.Table.Where(a => a.LastName != null && a.LastName != ""
                                                       && a.Deactivate == "N").OrderBy(c => c.LastName).ToList();
            }
            else
            {
                IEnumerable<string> ProductIdList = ProductId.Split(',').Select(str => str);

                var list = _ProductAuthorLink.Table.Where(p => ProductIdList.Contains(p.ProductId.ToString()) && p.Deactivate == "N").Select(a => new
                {
                    Id = a.AuthorId
                }).ToList();

                string joined = "";
                foreach (var lst in list)
                {
                    joined = joined + lst.Id.ToString() + ",";
                }
                joined = joined.Substring(0, joined.Length - 1);

                IEnumerable<string> AuthorIdList = joined.Split(',').Select(str => str);

                var againList = _AuthorRepository.Table.Where(a => a.Deactivate == "N"
                                                          && AuthorIdList.Contains(a.Id.ToString())
                                                          ).OrderBy(c => c.LastName).ToList();

                return againList;
            }

        }



        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets all ProductCategory
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>ProductCategory collection</returns>
        public virtual IList<ProductCategoryMaster> GetAllProductCategory()
        {
            var ProductCategory = _ProductCategoryRepository.Table.Where(c => c.Deactivate == "N").OrderBy(c => c.ProductCategory).ToList();
            return ProductCategory;

        }



        


        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets all ProductType
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>ProductType collection</returns>
        public virtual IList<ProductTypeMaster> GetAllProductType()
        {
            return _ProductTypeRepository.Table.Where(d => d.typelevel == 1 && d.Deactivate == "N").OrderBy(c => c.typeName).ToList();

        }

        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets all Sub ProductType by ProductType Id WISE
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>ProductType collection</returns>
        public virtual IList<ProductTypeMaster> GetAllSubProductType(ProductTypeMaster ProductType)
        {
            return _ProductTypeRepository.Table.Where(d => d.parenttypeid == ProductType.Id && d.Deactivate == "N").OrderBy(c => c.typeName).ToList();

        }

        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets all Sub ProductType by ProductType Id WISE
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Author suggesation</returns>
        public virtual IList<AuthorMaster> GetAuthorSuggesationList(AuthorMaster AuthorMaster)
        {
            return _AuthorRepository.Table.Where(a => a.Deactivate == "N"
                                                && a.Type == AuthorMaster.Type &&
                                                ((a.FirstName.Contains(AuthorMaster.FirstName)
                                                || a.LastName.Contains(AuthorMaster.FirstName) ||
                                                 (a.FirstName + " " + a.LastName).Contains(AuthorMaster.FirstName)))).OrderBy(c => c.LastName).ToList();

        }

        /// <summary>
        /// create by Sanjeet Singh on 17/05/2016
        /// Gets all Sub ProductType by ProductType Id WISE
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Author suggesation</returns>
        public virtual IList<ProductTypeMaster> GetSubProductType()
        {
            return _ProductTypeRepository.Table.Where(d => d.parenttypeid != null && d.Deactivate == "N").OrderBy(c => c.typeName).ToList();

        }


        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets all Imprint List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Imprint List</returns>
        public virtual IList<ImprintMaster> GetImprintList()
        {
            return _ImprintRepository.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.ImprintName).ToList();

        }

        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets All Language List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Langauge List</returns>
        public virtual IList<LanguageMaster> GetLanguageList()
        {
            return _LanguageRepository.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.LanguageName).ToList();

        }


        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets All Currency List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Currency List</returns>
        public virtual IList<CurrencyMaster> GetCurrencyList()
        {
            return _CurrencyRepository.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.CurrencyName).ToList();

        }

        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets All PublishingCompany List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>PublishingCompany List</returns>
        public virtual IList<PublishingCompanyMaster> GetPublishingCompanyList()
        {
            //return _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.CompanyName).ToList();

            //Modified by Suranjana on 26/07/2016 for Proprietor Details Publishing Company dropdown list bind
            return _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N" && a.PublishingCompanyCode != "PCM0002").OrderBy(c => c.CompanyName).ToList();

        }

        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Gets AllPub Center List By Publishing CompanyId 
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>PubCenter List</returns>
        public virtual IList<PubCenterMaster> GetPubCenterByCompanyIdList(PublishingCompanyMaster PublishingCompany)
        {
            return _PubCenterRepository.Table.Where(a => a.Deactivate == "N" && a.PublishingCompanyid == PublishingCompany.Id).OrderBy(c => c.CenterName).ToList();
        }


        /// <summary>
        /// create by Vishal Verma on 12/05/2016
        /// Get Series List By DivisionId and SubDivisonId
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Series List</returns>
        public virtual IList<SeriesMaster> GetSeriesList(SeriesMaster SeriesMaster)
        {
            return _SeriesRepository.Table.Where(a => a.Deactivate == "N" && (a.divisionid == SeriesMaster.divisionid && SeriesMaster.Subdivisionid == null) || (a.divisionid == SeriesMaster.divisionid && a.Subdivisionid == SeriesMaster.Subdivisionid)).OrderBy(c => c.Seriesname).ToList();

        }

        /// <summary>
        /// Created by dheeraj kumar sharma 
        /// to get the list of contract type
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>Series List</returns>
        public virtual IList<ContractMaster> GetContract()
        {
            return _contractMaster.Table.Where(i => i.Deactivate == "N" && i.ContractName != null).OrderBy(i => i.ContractName).ToList();

        }

        /// <summary>
        /// Created by dheeraj kumar sharma 
        ///to get the type of copyright
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>Series List</returns>
        public virtual IList<TypeOfRightsMaster> GetCopyRightsTerms()
        {
            return _TypeOfRightsMaster.Table.Where(i => i.Deactivate == "N" && i.TypeOfRights != null).OrderBy(i => i.TypeOfRights).ToList();

        }


        /// <summary>
        /// Created by vinay 
        ///to get the type of copyright
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>Series List</returns>
        public virtual IList<PaymentPeriod> GetAllPamentPeriod()//Without Payment as per Schedule
        {
            return _PaymentPeriod.Table.Where(i => i.Deactivate == "N" && i.PaymentType != null && i.PeriodValueId != -1).OrderBy(i => i.PaymentType).ToList();

        }

        public virtual IList<PaymentPeriod> GetAllPamentPeriodList()
        {
            return _PaymentPeriod.Table.Where(i => i.Deactivate == "N" && i.PaymentType != null).OrderBy(i => i.PaymentType).ToList();

        }

        /// <summary>
        /// Created by vinay 
        ///to get the type of copyright
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>Series List</returns>
        public virtual IList<ContractType> GetAllContractType()
        {
            return _ContractType.Table.Where(i => i.Deactivate == "N" && i.ContractName != null).OrderBy(i => i.ContractName).ToList();

        }

        /// <summary>
        /// Created by Dheeraj sharma 
        ///to get the territery rights
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>territery list List</returns>
        public virtual IList<TerritoryRightsMaster> TerriteryRights()
        {
            return _TerritoryRightsMaster.Table.Where(i => i.Deactivate == "N" && i.Territoryrights != null).OrderBy(i => i.Territoryrights).ToList();

        }

        /// <summary>
        /// Created by vinay 
        ///to get the type of copyright
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>Series List</returns>
        public virtual IList<ServiceMaster> GetAllServiceList()
        {
            return _ServiceMaster.Table.Where(i => i.Deactivate == "N" && i.ServiceName != null).OrderBy(i => i.ServiceName).ToList();

        }


        public virtual IList<SubServiceMaster> GetAllSubServiceList()
        {
            return _SubServiceMaster.Table.Where(i => i.Deactivate == "N" && i.ServiceName != null).OrderBy(i => i.ServiceName).ToList();

        }
        /// <summary>
        /// Created by Dheeraj sharma 
        ///to get the subsidiary List
        /// </summary>
        /// <param name="showDeactivated">nothing</param>
        /// <returns>territery list List</returns>
        public virtual IList<SubsidiaryRightsMaster> SubsidiaryList()
        {
            return _SubsidiaryRightsMaster.Table.Where(i => i.Deactivate == "N" && i.SubsidiaryRights != null).OrderBy(i => i.SubsidiaryRights).ToList();

        }
        /// <summary>
        /// Get the terms of subsidiary List created by dheeraj kumar sharma 
        /// </summary>
        /// <param name="showHidden">Subsidiary list</param>
        /// <returns>Series List</returns>
        /// 
        public virtual IList<AuthorType> GetAllAuthorTypeList()
        {
            return _AuthorType.Table.Where(i => i.Deactivate == "N" && i.AuthorTypeName != null).OrderBy(i => i.AuthorTypeName).ToList();

        }

        /// <summary>
        /// Get the list of supply material
        /// </summary>
        /// <param name="showHidden">supply material list</param>
        /// <returns>Series List</returns>
        /// 
        public virtual IList<SupplyMaterialMaster> MaterialSuppliedByAuthor()
        {
            return _SupplyMaterialMaster.Table.Where(i => i.Deactivate == "N" && i.SupplyMaterial != null).OrderBy(i => i.SupplyMaterial).ToList();

        }

        /// <summary>
        /// Get the currency List
        /// </summary>
        /// <param name="showHidden">currency List</param>
        /// <returns>currency List</returns>
        /// 
        public virtual IList<CurrencyMaster> CurrencyList()
        {
            return _CurrencyMaster.Table.Where(i => i.Deactivate == "N" && i.CurrencyName != null).OrderBy(i => i.CurrencyName).ToList();
        }

        /// <summary>
        /// Get the Menu script format list 
        /// </summary>
        /// <param name="showHidden">Menu script format list </param>
        /// <returns>Menu script format list </returns>
        /// 
        public virtual IList<ManuscriptDeliveryFormatMaster> ManuscriptDeliveryFormatList()
        {
            return _ManuscriptDeliveryFormatMaster.Table.Where(i => i.Deactivate == "N" && i.ManuscriptDeliveryFormat != null).OrderBy(i => i.ManuscriptDeliveryFormat).ToList();
        }


        public virtual IList<SubServiceMaster> GetAllSubServiceListByServiceId(int ServiceId)
        {
            return _SubServiceMaster.Table.Where(i => i.Deactivate == "N" && i.ServiceName != null && i.ServiceMasterId == ServiceId).OrderBy(i => i.ServiceName).ToList();

        }

        public virtual IList<ISBNBag> GetAllISBNByProductTypeId(int ProductTypeId)
        {
            return _ISBNBag.Table.Where(i => i.Deactivate == "N" && i.Used == "N" && i.ProductTypeid == ProductTypeId).OrderBy(i => i.ISBN).ToList();

        }

        public virtual IList<ISBNBag> GetAllISBNBagList()
        {
            return _ISBNBag.Table.Where(i => i.Deactivate == "N" && i.Used == "N").OrderBy(i => i.ISBN).ToList();
        }

        public virtual ISBNBag GetTopOneISBNByProductTypeId(int ProductTypeId)
        {
            return _ISBNBag.Table.Where(i => i.Deactivate == "N" && i.Used == "N" && i.ProductTypeid == ProductTypeId).OrderBy(i => i.ISBN).FirstOrDefault();

        }

        
        /// <summary>
        /// create by Ankush on 10/08/2016
        /// Gets All Frequency List
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Frequency List</returns>
        public virtual IList<FrequencyMaster> GetFrequencyList()
        {
            return _FrequencyRepository.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.Frequency).ToList();

        }

        #endregion

        public virtual IList<ProductMaster> GetProductISBNList()
        {
            return _ProductRepository.Table.Where(i => i.Deactivate == "N" && i.OUPISBN != null).ToList();

        }

        public void InsertTicker(Ticker _Ticker)
        {
            _Ticker.Deactivate = "N";
            _Ticker.EntryDate = DateTime.Now;
            _Ticker.ModifiedBy = null;
            _Ticker.ModifiedDate = null;
            _Ticker.DeactivateBy = null;
            _Ticker.DeactivateDate = null;
            _TickerRepository.Insert(_Ticker);
        }

        public string TicketDuplicityCheck(Ticker _Ticker)
        {
            var dupes = _TickerRepository.Table.Where(x => x.Title.Trim().ToLower() == _Ticker.Title.Trim().ToLower()
                                                            && x.Deactivate == "N"
                                                            && (_Ticker.Id != 0 ? x.Id : 0) != (_Ticker.Id != 0 ? _Ticker.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }

        public Ticker GetTickerById(int Id)
        {
            return _TickerRepository.Table.Where(i => i.Id == Id && i.Deactivate == "N").FirstOrDefault();
        }

        public List<Ticker> GetTickerList()
        {
            return _TickerRepository.Table.Where(i => i.Deactivate == "N").ToList();
        }

        public void UpdateTicker(Ticker _Ticker)
        {
            _TickerRepository.Update(_Ticker);
        }

        public void InsertUploadDocumentDetails(UploadDocument _UploadDocument)
        {
            _UploadDocumentRepository.Insert(_UploadDocument);
        }

        public List<UploadDocument> getCommonUploadDocumentByMaster(string MasterName, int MasterId)
        {
            return _UploadDocumentRepository.Table.Where(d => d.Deactivate == "N" && d.MasterName.ToLower() == MasterName.ToLower() && d.MasterId == MasterId).ToList();
        }

        public UploadDocument getCommonUploadDocumentById(int Id)
        {
            return _UploadDocumentRepository.Table.Where(d => d.Deactivate == "N" && d.Id == Id).FirstOrDefault();
        }

        public void DeleteUploadDocumentDetails(UploadDocument _UploadDocument)
        {
            _UploadDocumentRepository.Update(_UploadDocument);
        }


        public void InsertAuthorType(AuthorType _obj)
        {
            _obj.Deactivate = "N";
            _obj.EntryDate = DateTime.Now;
            _obj.ModifiedBy = null;
            _obj.ModifiedDate = null;
            _obj.DeactivateBy = null;
            _obj.DeactivateDate = null;
            _AuthorType.Insert(_obj);
        }

        public void UpdateAuthorType(AuthorType _obj)
        {
            _AuthorType.Update(_obj);
        }

        //added on 04 Jan, 2018
        public string CurrencyDuplicityCheck(CurrencyMaster _CurrencyMaster)
        {
            var dupes = _CurrencyRepository.Table.Where(x => x.CurrencyName.Trim().ToLower() == _CurrencyMaster.CurrencyName.Trim().ToLower()
                                                            && x.Symbol.Trim().ToLower() == _CurrencyMaster.Symbol.Trim().ToLower()
                                                            && x.Deactivate == "N"
                                                            && (_CurrencyMaster.Id != 0 ? x.Id : 0) != (_CurrencyMaster.Id != 0 ? _CurrencyMaster.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }

        public  List<CurrencyMaster> GetCurrencyMasterList()
        {
            return _CurrencyRepository.Table.Where(i => i.Deactivate == "N").ToList();
        }

        public void InsertCurrencyMaster(CurrencyMaster _CurrencyMaster)
        {
            _CurrencyMaster.Deactivate = "N";
            _CurrencyMaster.EntryDate = DateTime.Now;
            _CurrencyMaster.ModifiedBy = null;
            _CurrencyMaster.ModifiedDate = null;
            _CurrencyMaster.DeactivateBy = null;
            _CurrencyMaster.DeactivateDate = null;
            _CurrencyRepository.Insert(_CurrencyMaster);
        }

        public CurrencyMaster GetCurrencyMasterById(int Id)
        {
            return _CurrencyRepository.Table.Where(i => i.Deactivate == "N" && i.Id == Id).SingleOrDefault();
        }

        public void UpdateCurrencyMaster(CurrencyMaster _CurrencyMaster)
        {
            _CurrencyRepository.Update(_CurrencyMaster);
        }



    }
}
