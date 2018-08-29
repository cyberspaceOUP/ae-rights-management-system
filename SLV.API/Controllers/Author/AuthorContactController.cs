using ACS.Core.Domain.Master;
using ACS.Services.Master;
using ACS.Services.Product;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SLV.Model.AuthorContract;
using ACS.Services.AuthorContract;
using ACS.Core.Domain.AuthorContract;
using ACS.Services.User;
using ACS.Core.Data;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Core.Domain.AuthorContract;



using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Services.User;
using System.Text;
using ACS.Services.Alert;
using ACS.Core.Domain.Alert;
using Logger;
using ACS.Core;

namespace SLV.API.Controllers.Author
{
    public class AuthorContactController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IExecutive _Iexecutive;
        private readonly IDepartmentService _Department;
        private readonly ICommonListService _CommonListService;
        private readonly IAuthorContractService _IAuthorContractService;
        private readonly IApplicationSetUpService _ApplicationSetUpService;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IRepository<AuthorContractAgreement> _AuthorAgreement;

        private readonly IRepository<ContractMaster> _ContractType;
        private readonly IDbContext _dbContext;
        private readonly ILocalizationService _localizationService;

        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _ProductMaster;
        private readonly IRepository<SeriesMaster> _SeriesMaster;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryMaster;
        private readonly IRepository<AuthorContractAddendumDetails> _AuthorContractAddendumDetails;

        private readonly IProductType _IProductType;
        private readonly ISeriesProductEntryService _SeriesProductEntryService;

        private readonly IRepository<AuthorContractOriginal> _AuthorContract;
        private readonly IServiceApplicationEmailSetup _IServiceApplicationEmailSetup;
        private readonly IRepository<ExecutiveMaster> _ExecutiveMaster;

        private readonly IRepository<ACS.Core.Domain.Product.ProductPreviousProductLink> _ProductPreviousProductLink;
        private readonly IRepository<AuthorContractAddendumRoyality> _AuthorContractAddendumRoyality;
        private readonly IRepository<ProductTypeMaster> _ProductTypeMaster;
        private readonly IRepository<ProductAuthorLink> _ProductAuthorLink;

        private readonly IRepository<AddendumFileUpload> _AddendumFileUpload;

        public AuthorContactController(IExecutive Iexecutive
            , IDepartmentService Department
            , ICommonListService CommonListService
            , IAuthorContractService IAuthorContractService
            , IRepository<ApplicationSetUp> ApplicationSetUp
            , IApplicationSetUpService ApplicationSetUpService
            , IRepository<ContractMaster> ContractType
            , IRepository<AuthorContractAgreement> AuthorAgreement
             , IDbContext dbContext
            , ILocalizationService localizationService

            , IRepository<ACS.Core.Domain.Product.ProductMaster> ProductMaster
            , IRepository<SeriesMaster> SeriesMaster
            , IRepository<ProductCategoryMaster> ProductCategoryMaster
            , IRepository<AuthorContractAddendumDetails> AuthorContractAddendumDetails
            ,IProductType IProductType
            , ISeriesProductEntryService SeriesProductEntryService
            ,IRepository<AuthorContractOriginal> AuthorContract
            , IServiceApplicationEmailSetup IServiceApplicationEmailSetup
            , IRepository<ExecutiveMaster> ExecutiveMaster

            , IRepository<ProductPreviousProductLink> ProductPreviousProductLink
            , IRepository<AuthorContractAddendumRoyality> AuthorContractAddendumRoyality
            , IRepository<ProductTypeMaster> ProductTypeMaster
            , IRepository<ProductAuthorLink> ProductAuthorLink
            , IRepository<AddendumFileUpload> AddendumFileUpload
            )
        {
            _Iexecutive = Iexecutive;
            _Department = Department;
            _CommonListService = CommonListService;
            _IAuthorContractService = IAuthorContractService;
            _ApplicationSetUp = ApplicationSetUp;
            _ApplicationSetUpService = ApplicationSetUpService;
            _ContractType = ContractType;
            _AuthorAgreement = AuthorAgreement;
            this._dbContext = dbContext;
            this._localizationService = localizationService;

            _ProductMaster = ProductMaster;
            _SeriesMaster = SeriesMaster;
            _ProductCategoryMaster = ProductCategoryMaster;

            _AuthorContractAddendumDetails = AuthorContractAddendumDetails;

            _IProductType = IProductType;
            _SeriesProductEntryService = SeriesProductEntryService;
            this._AuthorContract = AuthorContract;
            this._IServiceApplicationEmailSetup = IServiceApplicationEmailSetup;
            this._ExecutiveMaster = ExecutiveMaster;

            _ProductPreviousProductLink = ProductPreviousProductLink;
            _AuthorContractAddendumRoyality = AuthorContractAddendumRoyality;
            _ProductTypeMaster = ProductTypeMaster;
            _ProductAuthorLink = ProductAuthorLink;
            this._AddendumFileUpload = AddendumFileUpload;
        }

        /********************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :   06th june 2016
         Create for :   getting the list of handled by in rights dep case editorolia list other wise single editorial person
         
         *********************************************************************************************************************/

        [HttpPost]
        public IHttpActionResult getHandledByContract(ExecutiveMaster executive)
        {
            try
            {
                var query = _Iexecutive.getExecutiveListBasedonDepartment(executive).Select(i => new
                {
                    ExecutiveName = i.executiveName,
                    Id = i.Id
                }).ToList();
                DepartmentMaster dept = new DepartmentMaster();
                dept.Id = executive.DepartmentId;
                string code = _Department.GetDepartmentById(dept).DepartmentCode.ToString();
                return Json(new { query, code });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getHandledByContract", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getHandledByContract", ex);
                return Json(ex.InnerException);
            }
        }

        /********************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :   06th june 2016
         Create for :   getting the list of contract type
         
         *********************************************************************************************************************/
        public IHttpActionResult getAuthorContract()
        {
            try
            {
                var query = _CommonListService.GetContract().Select(i => new
                {
                    Id = i.Id,
                    Contact = i.ContractName,
                });
                return Json(new { query });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAuthorContract", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAuthorContract", ex);
                return Json(ex.InnerException);
            }
        }

        [HttpGet]
        public IHttpActionResult getAuthorList(string ProductId)
        {
            var query = _CommonListService.GetAllAuthor(ProductId).ToList().Select(i => new
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName
            });
            return Json(query);
        }

        /********************************************************************************************************************
       Created By :   Dheeraj Kumar Sharma
       Created On :   06th june 2016
       Create for :   getting the list of copyrights
         
       *********************************************************************************************************************/
        public IHttpActionResult GetCopyrightsType()
        {
            var query = _CommonListService.GetCopyRightsTerms().Select(i => new
            {
                Id = i.Id,
                Copyrights = i.TypeOfRights,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :   06th june 2016
         Create for :  Paymeode mode
         
     *********************************************************************************************************************/
        public IHttpActionResult GetPaymentType()
        {
            var query = _CommonListService.GetAllPamentPeriod().Select(i => new
            {
                Id = i.PeriodValueId,
                PatmentType = i.PaymentType,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
           Created By :   Dheeraj Kumar Sharma
           Created On :   06th june 2016
           Create for :  Books sale area territory right
         
        *********************************************************************************************************************/
        public IHttpActionResult getTerriteryRights()
        {
            var query = _CommonListService.TerriteryRights().Select(i => new
            {
                Id = i.Id,
                Territoryrights = i.Territoryrights,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
          Created By :   Dheeraj Kumar Sharma
          Created On :   06th june 2016
          Create for :  subsidiary List
         
       *********************************************************************************************************************/
        public IHttpActionResult getSubsidiaryList()
        {
            var query = _CommonListService.SubsidiaryList().Select(i => new
            {
                Id = i.Id,
                SubsidiaryRights = i.SubsidiaryRights,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
          Created By :   Dheeraj Kumar Sharma
          Created On :  06/07/2016
          Create for :  Material list List
         
       *********************************************************************************************************************/
        public IHttpActionResult getSupplyMaterialList()
        {
            var query = _CommonListService.MaterialSuppliedByAuthor().Select(i => new
            {
                Id = i.Id,
                SupplyMaterial = i.SupplyMaterial,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :  06/07/2016
         Create for :  Material list List
         
      *********************************************************************************************************************/
        public IHttpActionResult getCurrencyList()
        {
            var query = _CommonListService.GetCurrencyList().Select(i => new
            {
                Id = i.Id,
                CurrencyName = i.CurrencyName,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :  07/06/2016
         Create for :  Material list List
         
      *********************************************************************************************************************/
        public IHttpActionResult getMenuScriptDeliveryFormat()
        {
            var query = _CommonListService.ManuscriptDeliveryFormatList().Select(i => new
            {
                Id = i.Id,
                DeliveryFormat = i.ManuscriptDeliveryFormat,
            });
            return Json(new { query });
        }
        /********************************************************************************************************************
     Created By :   Dheeraj Kumar Sharma
     Created On :  15/06/2016
     Create for :  for Inserting author contract
         
  *********************************************************************************************************************/
        [HttpPost]
        public IHttpActionResult InsertAuthorContractDetails(AuthorContractModel AuthorContractModel)
        {
            //This section will define weather data will update or inserted 
            string status = string.Empty;
            string _ThirdPartyValue = string.Empty;
             string[] values;
            if (AuthorContractModel.Id == 0 && AuthorContractModel.SeriesCode == "")
            {
                string[] ProductIds = AuthorContractModel.SeriesIds == "" ? new string[0] : AuthorContractModel.SeriesIds.Split(',');
                if (ProductIds.Length == 0)
                {
                    status = InsertAuthorContract(AuthorContractModel);
                }
                else
                {
                    string SeriesCode = GenerateSeriesCode("SeriesContract", "SR");
                    //foreach (var productId in ProductIds)
                    //{
                    //    //Added by Saddam on 17/08/2017
                    //    string s = AuthorContractModel.ThirdPartyPermission;
                    //    string[] values = s.Split(',');



                    //    status = InsertAuthorContract(AuthorContractModel, Convert.ToInt16(productId), AuthorContractModel.SeriesId, SeriesCode);
                    //    status = "OK" + "," + SeriesCode;

                    //}

                    //_ThirdPartyValue = AuthorContractModel.ThirdPartyPermission;
                    for (int i = 0; i < ProductIds.Length; i++)
                    {
                        //Added by Saddam on 17/08/2017

                        //values = _ThirdPartyValue.Split(',');

                        //AuthorContractModel.ThirdPartyPermission = values[i];

                        status = InsertAuthorContract(AuthorContractModel, Convert.ToInt16(ProductIds[i]), AuthorContractModel.SeriesId, SeriesCode);
                        status = "OK" + "," + SeriesCode;
                    }

                }


            }
            else
            {
                if (AuthorContractModel.SeriesCode == "")
                {

                    //_ThirdPartyValue = AuthorContractModel.ThirdPartyPermission;

                    //if (_ThirdPartyValue.Contains(","))
                    //{
                    //    values = _ThirdPartyValue.Split(',');
                    //    AuthorContractModel.ThirdPartyPermission = values[0];
                    //}
                    //else
                    //{
                    //    AuthorContractModel.ThirdPartyPermission = _ThirdPartyValue;
                    //}

                    status = UpdateAuthorContract(AuthorContractModel);
                }
                else
                {
                    IList<AuthorContractOriginal> _Contract = new List<AuthorContractOriginal>();
                    _Contract = _IAuthorContractService.GetAuthorContractBySeriesId(AuthorContractModel.SeriesCode).ToList();
                    using (var scope = new System.Transactions.TransactionScope())
                    {

                        //_ThirdPartyValue = AuthorContractModel.ThirdPartyPermission;
                        //values = _ThirdPartyValue.Split(',');

                        int i = 0;
                        foreach (var _ContractTable in _Contract)
                        {
                            //AuthorContractModel.ThirdPartyPermission = values[i];

                            status = UpdateAuthorContract(AuthorContractModel, _ContractTable);
                            i++;
                        }

                        //foreach (var _ContractTable in _Contract)
                        //{
                        //    status = UpdateAuthorContract(AuthorContractModel, _ContractTable);
                        //}


                  

                        scope.Complete();
                    }


                }

            }
            return Json(status);
        }
        /*******************************************************************************************************
         * Create By    :   Dheeraj Sharma
         * Created on   :   7th july
         * Created For  :   Insert the agreement datils whne user update contract form in update mode 
         * then save extra info about the contract
         ******************************************************************************************************/
        [HttpPost]
        public IHttpActionResult ContractAgreement(ContractAgreement _Agreement)
        {
            var status = "";

            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {

                    AuthorContractAgreement Agreement = new AuthorContractAgreement();
                    if (_Agreement.Id != 0 && _Agreement.ContractId != 0 && _Agreement.SeriesCode == null)
                    {
                        Agreement = _IAuthorContractService.getAgreementByContractId(_Agreement.ContractId.GetValueOrDefault());
                    }
                    else if (_Agreement.SeriesCode != "" && _Agreement.Id != 0)
                    {
                        Agreement = _IAuthorContractService.AuthorContractAgreementbySeriesCode(_Agreement.SeriesCode).FirstOrDefault();
                    }
                    Agreement.contractstatus = _Agreement.ContractStatus;
                    Agreement.dateofagreement = _Agreement.AgreementDate;
                    Agreement.ContractId = _Agreement.ContractId == null || _Agreement.ContractId ==0? null : _Agreement.ContractId;
                    Agreement.SeriesCode = _Agreement.SeriesCode == "" || _Agreement.SeriesCode==null ? null : _Agreement.SeriesCode;
                    Agreement.SignedContractreceived = _Agreement.contractRecieved;
                    Agreement.SignedContractsentdate = _Agreement.SignedcontracDate;
                    Agreement.Authorcopiessentdate = _Agreement.AuthorCopiesSend;
                    Agreement.periodofagreement = _Agreement.PeriodOfAgreement;
                    Agreement.Expirydate = _Agreement.ExpiryDate;
                    Agreement.effectiveDate = _Agreement.EffectiveDate;
                    Agreement.Contributorcopiessentdate = _Agreement.CotributorCopiessend;
                    Agreement.Cancellationdate = _Agreement.CancelDate;
                    Agreement.Cancellationreason = _Agreement.Cancellationremarks;
                    Agreement.Remarks = _Agreement.AgreementRemarks == "" ? _Agreement.Cancellationremarks : _Agreement.AgreementRemarks;
                    Agreement.EnteredBy = _Agreement.EnteredBy;

                    string[] filename = _Agreement.AgreementFileName.Length >= 1 && _Agreement.AgreementFileName != "" ? _Agreement.AgreementFileName.Substring(0, _Agreement.AgreementFileName.Length - 1).ToString().Split(',') : _Agreement.AgreementFileName.ToString().Split('&');
                    string[] ContributorFile = _Agreement.ContributorFileName.Length >= 1 && _Agreement.ContributorFileName != "" ? _Agreement.ContributorFileName.Substring(0, _Agreement.ContributorFileName.Length - 1).ToString().Split(',') : _Agreement.ContributorFileName.Split('$');
                    string[] doc = _Agreement.Doc != null ? _Agreement.Doc.Length >= 1 ? _Agreement.Doc.Substring(0, _Agreement.Doc.Length - 1).ToString().Split(',') : _Agreement.Doc.Split('&') : null;
                    int index = 0;
                    IList<AuthorContractDocument> _DocumentList = new List<AuthorContractDocument>();
                    if (_Agreement.Id != 0)
                    {

                        Agreement.ModifiedBy = _Agreement.EnteredBy;
                        Agreement.ModifiedDate = DateTime.Now;
                        _AuthorAgreement.Update(Agreement);
                        if (_Agreement.SeriesCode == "" || _Agreement.SeriesCode == null)
                        { }
                        else
                        {
                            IList<AuthorContractOriginal> _contract = new List<AuthorContractOriginal>();
                            _contract = _IAuthorContractService.GetAuthorContractBySeriesId(_Agreement.SeriesCode);
                            foreach (var lst in _contract)
                            {
                                lst.Status = _Agreement.ContractStatus;
                                lst.ModifiedBy = _Agreement.EnteredBy;
                                lst.ModifiedDate = DateTime.Now;
                                _IAuthorContractService.UpdateAuthorContract(lst);
                            }
                        }

                        if (_Agreement.ContractId != 0 && (_Agreement.SeriesCode == "" || _Agreement.SeriesCode == null))
                        {
                            AuthorContractOriginal _contract = new AuthorContractOriginal();
                            _contract = _IAuthorContractService.GetAuthorContractById(_Agreement.ContractId.GetValueOrDefault());
                            _contract.Status = _Agreement.ContractStatus;
                            _contract.ModifiedBy = _Agreement.EnteredBy;
                            _contract.ModifiedDate = DateTime.Now;
                            _IAuthorContractService.UpdateAuthorContract(_contract);
                        }

                        /*Insert the data into the link table of document */
                        if (filename.Length >= 1 && filename[0] != "")
                        {
                            foreach (string word in filename)
                            {
                                AuthorContractDocument document = new AuthorContractDocument();
                                document.AgreementId = _Agreement.Id;
                                document.FileNameEntered = word;
                                document.FileName = doc[index];
                                document.DocumentTypeId = 1;
                                document.EnteredBy = _Agreement.EnteredBy;
                                _IAuthorContractService.InsertAuthorAgreementDocument(document);
                                index++;
                            }
                        }
                        if (ContributorFile.Length >= 1 && ContributorFile[0] != "")
                        {
                            foreach (string word in ContributorFile)
                            {
                                AuthorContractDocument document = new AuthorContractDocument();
                                document.AgreementId = _Agreement.Id;
                                document.FileNameEntered = word;
                                document.FileName = doc[index];
                                document.DocumentTypeId = 2;
                                document.EnteredBy = _Agreement.EnteredBy;
                                _IAuthorContractService.InsertAuthorAgreementDocument(document);

                            }
                        }
                    }
                    else
                    {

                        if (filename.Length >= 1 && filename[0] != "")
                        {
                            foreach (string word in filename)
                            {
                                AuthorContractDocument document = new AuthorContractDocument();
                                document.FileNameEntered = word;
                                document.FileName = doc[index];
                                document.DocumentTypeId = 1;
                                document.Deactivate = "N";
                                document.EnteredBy = _Agreement.EnteredBy;
                                document.EntryDate = DateTime.Now;
                                _DocumentList.Add(document);
                                index++;
                            }
                        }
                        if (ContributorFile.Length >= 1 && ContributorFile[0] != "")
                        {
                            foreach (string word in ContributorFile)
                            {
                                AuthorContractDocument document = new AuthorContractDocument();
                                document.FileNameEntered = word;
                                document.FileName = doc[index];
                                document.DocumentTypeId = 2;
                                document.Deactivate = "N";
                                document.EnteredBy = _Agreement.EnteredBy;
                                document.EntryDate = DateTime.Now;
                                _DocumentList.Add(document);
                                index++;
                            }
                        }
                        Agreement.AuthorContractDocument = _DocumentList;
                        _IAuthorContractService.InsertAuthorContractAgreement(Agreement);


                        if (_Agreement.SeriesCode != "" && _Agreement.SeriesCode != null && (_Agreement.ContractId == 0 || _Agreement.ContractId == null))
                        {
                            IList<AuthorContractOriginal> _contract = new List<AuthorContractOriginal>();
                            _contract = _IAuthorContractService.GetAuthorContractBySeriesId(_Agreement.SeriesCode);
                            foreach (var lst in _contract)
                            {
                                lst.Status = _Agreement.ContractStatus;
                                lst.ModifiedBy = _Agreement.EnteredBy;
                                lst.ModifiedDate = DateTime.Now;
                                _IAuthorContractService.UpdateAuthorContract(lst);
                            }
                        }
                        else if (_Agreement.ContractId != 0 ) // && _Agreement.SeriesCode == "" &&  _Agreement.SeriesCode==null)
                        {
                            AuthorContractOriginal _contract = new AuthorContractOriginal();
                            _contract = _IAuthorContractService.GetAuthorContractById(_Agreement.ContractId.GetValueOrDefault());
                            _contract.Status = _Agreement.ContractStatus;
                            _contract.ModifiedBy = _Agreement.EnteredBy;
                            _contract.ModifiedDate = DateTime.Now;
                            _IAuthorContractService.UpdateAuthorContract(_contract);
                        }


                    }

                    /*====================================================================================================================
                      * This section will assign AuthorContractContributor object insert parame
                    ======================================================================================================================*/
                    //int mint_flag = 0;
                    ////update existing contributer details
                    //IList<AuthorContractContributor> mobj_ContributorList = _IAuthorContractService.GetAuthorContractContributorListByContractId(Convert.ToInt32(_Agreement.ContractId));
                    //foreach (AuthorContractContributor _contrbuter in mobj_ContributorList)
                    //{
                    //    AuthorContractContributor mobj_ContributorDetailsUpdate = _IAuthorContractService.GetAuthorContractContributorDetailById(_contrbuter.Id);
                    //    mobj_ContributorDetailsUpdate.ContributorName = _Agreement.ContributorName[mint_flag].Contributor.ToString();
                    //    mobj_ContributorDetailsUpdate.ModifiedBy = _Agreement.EnteredBy;
                    //    mobj_ContributorDetailsUpdate.ModifiedDate = DateTime.Now;

                    //    _IAuthorContractService.UpdateAuthorContractContributorDetails(mobj_ContributorDetailsUpdate);
                    //    mint_flag++;
                    //}

                    ////insert new contributer details
                    //foreach (var items in _Agreement.ContributorName)
                    //{
                    //    if (_Agreement.ContributorName.Count > mint_flag)
                    //    {
                    //        AuthorContractContributor mobj_ContributorDetailsInsert = new ACS.Core.Domain.AuthorContract.AuthorContractContributor();
                    //        mobj_ContributorDetailsInsert.AuthorContractId = Convert.ToInt32(_Agreement.ContractId);
                    //        mobj_ContributorDetailsInsert.ContributorName = _Agreement.ContributorName[mint_flag].Contributor.ToString(); ;
                    //        mobj_ContributorDetailsInsert.Deactivate = "N";
                    //        mobj_ContributorDetailsInsert.EnteredBy = _Agreement.EnteredBy;
                    //        mobj_ContributorDetailsInsert.EntryDate = DateTime.Now;
                    //        mobj_ContributorDetailsInsert.ModifiedBy = null;
                    //        mobj_ContributorDetailsInsert.ModifiedDate = null;
                    //        mobj_ContributorDetailsInsert.DeactivateBy = null;
                    //        mobj_ContributorDetailsInsert.DeactivateDate = null;

                    //        _IAuthorContractService.InsertAuthorContractContributorDetails(mobj_ContributorDetailsInsert);
                    //        mint_flag++;
                    //    }
                    //}
                    //  /*====================================================================================================================
                    //  * This section will assign AuthorContractmaterialdetails object insert parame
                    //======================================================================================================================*/

                   scope.Complete();
                    status = "OK";
                }
                catch (Exception ex)
                {
                    status = ex.ToString();
                }
           }

           // getListAuthorContractStatusMail(_Agreement.ContractId.GetValueOrDefault(), _Agreement.EnteredBy);
            return Json(status);
        }
        /** Create By    :   Dheeraj Kumar Sharma
            Create On    :   23rd june 2016 
            Created For   :   Fetching the details of author contract
         */
        [HttpGet]
        public IHttpActionResult GetAuthorContractDetails(Int64 Id)
        {
            IList<AuthorContractModel> _AuthorModel = new List<AuthorContractModel>();
            AuthorContractOriginal _Contract = new AuthorContractOriginal();
            string status = "";
            try
            {

                _Contract = _IAuthorContractService.GetAuthorContractById(Id);

                var _AuhtorContract = new
                {
                    HandleById = _Contract.ExecutiveCode,
                    HandledByName = _Contract.EnteredByForeignKey.executiveName,
                    ProductId = _Contract.ProductId,
                    EntryDate = _Contract.EntryDate.toDDMMYYYY(),
                    ContractDate = _Contract.EntryDate.toDDMMYYYY(),
                    //ContractTypeId = _Contract.ContractTypeId,
                    //ContractType = _Contract.ContractMaster.ContractName,
                    NoOfAuthors = _Contract.NoOfAuthors,
                    TemsOfCopyRight = _Contract.TermsOfCopyright,
                    PeriodInMonth = _Contract.TermsOfCopyright == 1 ? _Contract.contractperiodinmonth : 0,
                    ContractExpiry = _Contract.TermsOfCopyright == 1 ? Convert.ToDateTime(_Contract.ContractExpiryDate).toDDMMYYYY() : null,
                    BuyBack = _Contract.BuyBack,
                    NatureOfWork = _Contract.NatureOfWork,
                    CopyRightOwner = _Contract.CopyrightOwner,
                    TeriterryId = _Contract.Territoryrightsid,
                    Teriterry = _Contract.TerritoryRightsMaster.Territoryrights,
                    ThirdPartyPermission = _Contract.thirdpartypermission,
                    Amendment = _Contract.Amendment,
                    AmendmentRemarks = _Contract.Amendment == true ? _Contract.AmendmentRemarks : null,
                    Restriction = _Contract.Restriction,
                    SubjectMatterandTreatment = _Contract.subjectMatterAndTreatment,
                    MinWords = _Contract.MinNoOfwords,
                    MaxWords = _Contract.MaxNoOfwords,
                    MinPages = _Contract.MinNoOfPages,
                    MaxPages = _Contract.MaxNoOfPages,
                    PriceType = _Contract.PriceType,
                    CurrencyId = _Contract.CurrencyId,
                    Currency = _Contract.CurrencyId != null ? _Contract.CurrencyMaster.CurrencyName : null,
                    CurrencySymbol = _Contract.CurrencyId != null ? _Contract.CurrencyMaster.Symbol : null,
                    Price = _Contract.Price,
                    MediumofDelivery = _Contract.MediumOfdelivery,
                    Deliveryschedule = _Contract.Deliveryschedule,
                    ProductRemarks = _Contract.ProductRemarks,
                    MenuScriptDeliveryId = _Contract.ManuscriptId,
                    MenuScriptDelivery = _Contract.ManuscriptId == null ? "" : _Contract.ManuscriptDeliveryFormatMaster.ManuscriptDeliveryFormat,
                    AuthorContractCode = _Contract.AuthorContractCode.ToUpper(),
                    LicenceId = _Contract.LicenseId,
                    SeriesCode = _Contract.SeriesCode                  

                };

                var _contributor = _Contract.AuthorContactContibutor.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Name = i.ContributorName
                });


                var _AuthorList = _Contract.AuthorContractauthordetails.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Type = i.AuthorTypeMaster.AuthorTypeName,
                    TypeId = i.Authortype,
                    Id = i.AuthorMaster.Id,
                    Name = i.AuthorMaster.FirstName + " " + i.AuthorMaster.LastName,
                    PaymentPeriodId = i.paymentperiodid != null ? i.paymentperiodid : (int?)null,
                    PaymentPeriod = i.paymentperiodid != null ? i.PaymentPeriod.PaymentType : "",
                    AuthorCopies = i.AuthorCopies != null ? i.AuthorCopies : (int?)null,
                    SeedMoney = i.Seedmoney != null ? i.Seedmoney : (int?)null,
                    OneTimePayment = i.onetimepayment != null ? i.onetimepayment : (int?)i.onetimepayment,
                    AdvanceRoyalty = i.advanceroyality != null ? i.advanceroyality : (int?)i.advanceroyality,
                    RecId = i.Id,
                    ContractId = i.ContractTypeId,
                    ContractName = i.ContractMaster.ContractName
                }).ToList();

                IList<RoyaltySlab> _royalty = new List<RoyaltySlab>();
                foreach (var lst in _Contract.AuthorContractauthordetails)
                {

                    var list = lst.AuthorContractRoyality.Where(i => i.Deactivate == "N").ToList();
                    foreach (var item in list)
                    {
                        RoyaltySlab Royalty = new RoyaltySlab();
                        Royalty.Id = lst.Id;
                        Royalty.AuthorId = lst.AuthorId;
                        Royalty.CopiesFrom = item.CopiesFrom;
                        Royalty.CopiesTo = item.CopiesTo;
                        Royalty.SubProductType = item.ProductTypeMaster.typeName;
                        Royalty.subproductTypeId = item.subproducttypeid;
                        Royalty.Percentage = item.Percentage;
                        _royalty.Add(Royalty);
                    }
                }

                var _MaterialDate = _Contract.AuthorContractmaterialdetails.Where(i => i.Deactivate == "N").ToList()
                                                                    .Select(i => new
                                                                    {
                                                                        SuppliedDate = i.materialdate.toDDMMYYYY(),
                                                                        Material = i.SupplyMaterialMaster.SupplyMaterial,
                                                                        MaterialId = i.MaterialId
                                                                    }).ToList();

               
               
                
                IList<SusidiaryRights> _susidiaryRightsList = new List<SusidiaryRights>();
                var _susidiaryRights = _Contract.AuthorContractSubsidiaryRights.Where(i => i.Deactivate == "N").ToList();
                foreach (var lst in _susidiaryRights)
                {
                    SusidiaryRights _Subsidiary = new SusidiaryRights();
                    _Subsidiary.authorId = lst.AuthorId;
                    _Subsidiary.subsidiaryid = lst.subsidiaryrightsid;
                    _Subsidiary.AuthorName = lst.AuthorMaster.FirstName + " " + lst.AuthorMaster.LastName;
                    _Subsidiary.Subsidiary = lst.SubsidiaryRightsMaster.SubsidiaryRights;
                    _Subsidiary.OupPercentage = lst.ouppercentage;
                    _Subsidiary.Percentage = lst.AuthorPercentage;
                    _susidiaryRightsList.Add(_Subsidiary);
                }

                var _AgreementTable = _IAuthorContractService.getAgreementByContractId(_Contract.Id);
                //var _AgreementTable = _IAuthorContractService.getAgreementByContractId(_Contract.Id, _Contract.SeriesCode);

                var _ContractAgreement = _AgreementTable != null ? (new
                {
                    AgreementId = _AgreementTable.Id,
                    contractstatus = _AgreementTable.contractstatus,
                    AgreementDate = _AgreementTable.dateofagreement != null ? Convert.ToDateTime(_AgreementTable.dateofagreement).toDDMMYYYY() : null,
                    signedcontractsentdate = _AgreementTable.SignedContractsentdate != null ? Convert.ToDateTime(_AgreementTable.SignedContractsentdate).toDDMMYYYY() : null,
                    SignedContractreceived = _AgreementTable.SignedContractreceived != null ? Convert.ToDateTime(_AgreementTable.SignedContractreceived).toDDMMYYYY() : null,
                    Authorcopiessentdate = _AgreementTable.Authorcopiessentdate != null ? Convert.ToDateTime(_AgreementTable.Authorcopiessentdate).toDDMMYYYY() : null,
                    Contributorcopiessentdate = _AgreementTable.Contributorcopiessentdate != null ? Convert.ToDateTime(_AgreementTable.Contributorcopiessentdate).toDDMMYYYY() : null,
                    cancellationdate = _AgreementTable.Cancellationdate != null ? Convert.ToDateTime(_AgreementTable.Cancellationdate).toDDMMYYYY() : null,
                    Cancellationreason = _AgreementTable.Cancellationreason,
                    remarks = _AgreementTable.Remarks,
                    EffectiveDate = _AgreementTable.effectiveDate != null ? Convert.ToDateTime(_AgreementTable.effectiveDate).toDDMMYYYY() : null,
                    PeriodinMonth = _AgreementTable.periodofagreement,
                    ExpiryDate = _AgreementTable.Expirydate != null ? Convert.ToDateTime(_AgreementTable.Expirydate).toDDMMYYYY() : null
                }) : null;

                var _agreementDoc = _ContractAgreement != null ? _AgreementTable.AuthorContractDocument.Count() > 0 ?
                                    _AgreementTable.AuthorContractDocument.ToList().Where(i => i.Deactivate == "N").Select(i => new
                                    {
                                        Id = i.Id,
                                        FileName = i.FileName,
                                        FileNameEntered = i.FileNameEntered,
                                        DocumentTypeId = i.DocumentTypeId,
                                        type = i.DocumentTypeMaster.DocumentTypeName,
                                        agreementId = i.AgreementId

                                    }).ToList() : null : null;

                var _ttlSusidiary = _Contract.AuthorContractSubsidiaryRights.GroupBy(i => i.subsidiaryrightsid).Count();
                var TblList = _Contract.AuthorContractauthordetails.Select(i => new { Id = i.AuthorId, AuthorName = i.AuthorMaster.FirstName + " " + i.AuthorMaster.LastName }).ToList();

                /****************************************************************************************************
                 This section will be used to show license details if author contract is created by product license
                 ******************************************************************************************************/
                //var LicenseId = _Contract.

                /*******************************************************
                 * End Here
                 * **************************************************/
                var _ManuscriptDeliveryList = _Contract.AuthorContractMenuscriptDeliveryLink.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Id = i.ManuscriptId,
                    ManuscriptDeliveryFormat = i.ManuscriptDeliveryFormatMaster.ManuscriptDeliveryFormat
                }).ToList();

                var SeriesCode = _Contract.SeriesCode;

                return Json(new 
                            { 
                                _AuhtorContract, 
                                _royalty, 
                                _AuthorList,
                                _contributor, 
                                _MaterialDate, 
                                _susidiaryRightsList, 
                                TblList, 
                                _ttlSusidiary, 
                                status, 
                                _ContractAgreement,
                                _agreementDoc, 
                                _ManuscriptDeliveryList, 
                                SeriesCode 
                            });
            }
            catch (Exception Ex)
            {
                status = Ex.ToString();
                return Json(new { status });
            }

        }

        /***********************************************************************************************************
             * Create By    :   Dheeraj Kumar Sharma
               Create On    :   1st Aug 2016
               Created For   :  Fetching the details of author contract by seriesId
        *************************************************************************************************************/
        public IHttpActionResult GetAuthorContractDetailsbySeriesId(string SeriesCode)
        {
            IList<AuthorContractModel> _AuthorModel = new List<AuthorContractModel>();
            AuthorContractOriginal _Contract = new AuthorContractOriginal();
            IList<AuthorContractOriginal> ContractIList = new List<AuthorContractOriginal>();
            string status = "";
            try
            {
                ContractIList = _IAuthorContractService.GetAuthorContractBySeriesId(SeriesCode).ToList();

                //Added by saddam on 18/08/2017


                var _ThirdPartyContractList = ContractIList.Select(i => new {
                    ThirdPartyPermission = i.thirdpartypermission,
                }).ToList();


                //ended by Saddam

                var query = ContractIList.ToList().Select(i => new { i.ProductId });
                string ProductId = string.Empty;
                foreach (var r in query)
                {
                    ProductId = ProductId + r.ProductId + ",";
                }
                ProductId = ProductId.Substring(0, ProductId.Length - 1);
                _Contract = ContractIList.FirstOrDefault();
                var _AuhtorContract = new
                {
                    HandleById = _Contract.ExecutiveCode,
                    HandledByName = _Contract.EnteredByForeignKey.executiveName,
                    ProductId = ProductId,
                    Id = _Contract.Id,
                    EntryDate = _Contract.EntryDate.toDDMMYYYY(),
                    ContractDate = _Contract.EntryDate.toDDMMYYYY(),
                    SeriesCode = _Contract.SeriesCode,
                    SeriesId = _Contract.SeriesId,
                    SeriesName = _Contract.SeriesMaster.Seriesname,
                    //ContractTypeId = _Contract.ContractTypeId,
                    //ContractType = _Contract.ContractMaster.ContractName,
                    NoOfAuthors = _Contract.NoOfAuthors,
                    TemsOfCopyRight = _Contract.TermsOfCopyright,
                    PeriodInMonth = _Contract.TermsOfCopyright == 1 ? _Contract.contractperiodinmonth : 0,
                    ContractExpiry = _Contract.TermsOfCopyright == 1 ? Convert.ToDateTime(_Contract.ContractExpiryDate).toDDMMYYYY() : null,
                    BuyBack = _Contract.BuyBack,
                    NatureOfWork = _Contract.NatureOfWork,
                    CopyRightOwner = _Contract.CopyrightOwner,
                    TeriterryId = _Contract.Territoryrightsid,
                    Teriterry = _Contract.TerritoryRightsMaster.Territoryrights,
                    ThirdPartyPermission = _Contract.thirdpartypermission,
                    Amendment = _Contract.Amendment,
                    AmendmentRemarks = _Contract.Amendment == true ? _Contract.AmendmentRemarks : null,
                    Restriction = _Contract.Restriction,
                    SubjectMatterandTreatment = _Contract.subjectMatterAndTreatment,
                    MinWords = _Contract.MinNoOfwords,
                    MaxWords = _Contract.MaxNoOfwords,
                    MinPages = _Contract.MinNoOfPages,
                    MaxPages = _Contract.MaxNoOfPages,
                    PriceType = _Contract.PriceType,
                    CurrencyId = _Contract.CurrencyId,
                    Currency = _Contract.CurrencyId != null ? _Contract.CurrencyMaster.CurrencyName : null,
                    Price = _Contract.Price,
                    MediumofDelivery = _Contract.MediumOfdelivery,
                    Deliveryschedule = _Contract.Deliveryschedule,
                    ProductRemarks = _Contract.ProductRemarks,
                    MenuScriptDeliveryId = _Contract.ManuscriptId,
                    MenuScriptDelivery = _Contract.ManuscriptId == null ? "" : _Contract.ManuscriptDeliveryFormatMaster.ManuscriptDeliveryFormat,
                    AuthorContractCode = _Contract.AuthorContractCode.ToUpper()

                };

                var _contributor = _Contract.AuthorContactContibutor.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Name = i.ContributorName
                });
                var _AuthorList = _Contract.AuthorContractauthordetails.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Type = i.AuthorTypeMaster.AuthorTypeName,
                    TypeId = i.Authortype,
                    Id = i.AuthorMaster.Id,
                    Name = i.AuthorMaster.FirstName + " " + i.AuthorMaster.LastName,
                    PaymentPeriodId = i.paymentperiodid != null ? i.paymentperiodid : (int?)null,
                    PaymentPeriod = i.paymentperiodid != null ? i.PaymentPeriod.PaymentType : "",
                    AuthorCopies = i.AuthorCopies != null ? i.AuthorCopies : (int?)null,
                    SeedMoney = i.Seedmoney != null ? i.Seedmoney : (int?)null,
                    OneTimePayment = i.onetimepayment != null ? i.onetimepayment : (int?)i.onetimepayment,
                    AdvanceRoyalty = i.advanceroyality != null ? i.advanceroyality : (int?)i.advanceroyality,
                    RecId = i.Id,
                    ContractId = i.ContractTypeId,
                    ContractName = i.ContractMaster.ContractName
                }).ToList();

                IList<RoyaltySlab> _royalty = new List<RoyaltySlab>();
                foreach (var lst in _Contract.AuthorContractauthordetails)
                {

                    var list = lst.AuthorContractRoyality.Where(i => i.Deactivate == "N").ToList();
                    foreach (var item in list)
                    {
                        RoyaltySlab Royalty = new RoyaltySlab();
                        Royalty.Id = lst.Id;
                        Royalty.AuthorId = lst.AuthorId;
                        Royalty.CopiesFrom = item.CopiesFrom;
                        Royalty.CopiesTo = item.CopiesTo;
                        Royalty.SubProductType = item.ProductTypeMaster.typeName;
                        Royalty.subproductTypeId = item.subproducttypeid;
                        Royalty.Percentage = item.Percentage;
                        _royalty.Add(Royalty);
                    }
                }

                var _MaterialDate = _Contract.AuthorContractmaterialdetails.Where(i => i.Deactivate == "N").ToList()
                                                                    .Select(i => new
                                                                    {
                                                                        SuppliedDate = i.materialdate.toDDMMYYYY(),
                                                                        Material = i.SupplyMaterialMaster.SupplyMaterial,
                                                                        MaterialId = i.MaterialId
                                                                    }).ToList();
                IList<SusidiaryRights> _susidiaryRightsList = new List<SusidiaryRights>();
                var _susidiaryRights = _Contract.AuthorContractSubsidiaryRights.ToList();
                foreach (var lst in _susidiaryRights)
                {
                    SusidiaryRights _Subsidiary = new SusidiaryRights();
                    _Subsidiary.authorId = lst.AuthorId;
                    _Subsidiary.subsidiaryid = lst.subsidiaryrightsid;
                    _Subsidiary.AuthorName = lst.AuthorMaster.FirstName + " " + lst.AuthorMaster.LastName;
                    _Subsidiary.Subsidiary = lst.SubsidiaryRightsMaster.SubsidiaryRights;
                    _Subsidiary.OupPercentage = lst.ouppercentage;
                    _Subsidiary.Percentage = lst.AuthorPercentage;
                    _susidiaryRightsList.Add(_Subsidiary);
                }

                var _AgreementTable = _IAuthorContractService.AuthorContractAgreementbySeriesCode(SeriesCode).FirstOrDefault();

                var _ContractAgreement = _AgreementTable != null ? (new
                {
                    AgreementId = _AgreementTable.Id,
                    contractstatus = _AgreementTable.contractstatus,
                    AgreementDate = _AgreementTable.dateofagreement != null ? Convert.ToDateTime(_AgreementTable.dateofagreement).toDDMMYYYY() : null,
                    signedcontractsentdate = _AgreementTable.SignedContractsentdate != null ? Convert.ToDateTime(_AgreementTable.SignedContractsentdate).toDDMMYYYY() : null,
                    SignedContractreceived = _AgreementTable.SignedContractreceived != null ? Convert.ToDateTime(_AgreementTable.SignedContractreceived).toDDMMYYYY() : null,
                    Authorcopiessentdate = _AgreementTable.Authorcopiessentdate != null ? Convert.ToDateTime(_AgreementTable.Authorcopiessentdate).toDDMMYYYY() : null,
                    Contributorcopiessentdate = _AgreementTable.Contributorcopiessentdate != null ? Convert.ToDateTime(_AgreementTable.Contributorcopiessentdate).toDDMMYYYY() : null,
                    cancellationdate = _AgreementTable.Cancellationdate != null ? Convert.ToDateTime(_AgreementTable.Cancellationdate).toDDMMYYYY() : null,
                    Cancellationreason = _AgreementTable.Cancellationreason,
                    remarks = _AgreementTable.Remarks,
                    EffectiveDate = _AgreementTable.effectiveDate!=null? Convert.ToDateTime(_AgreementTable.effectiveDate).toDDMMYYYY():null,
                    PeriodinMonth = _AgreementTable.periodofagreement,
                    ExpiryDate = _AgreementTable.Expirydate!=null?Convert.ToDateTime(_AgreementTable.Expirydate).toDDMMYYYY():null
                }) : null;

                var _agreementDoc = _ContractAgreement != null ? _AgreementTable.AuthorContractDocument.Count() > 0 ?
                                    _AgreementTable.AuthorContractDocument.ToList().Where(i => i.Deactivate == "N").Select(i => new
                                    {
                                        Id = i.Id,
                                        FileName = i.FileName,
                                        FileNameEntered = i.FileNameEntered,
                                        DocumentTypeId = i.DocumentTypeId,
                                        type = i.DocumentTypeMaster.DocumentTypeName,
                                        agreementId = i.AgreementId

                                    }).ToList() : null : null;
                var _ManuscriptDeliveryList = _Contract.AuthorContractMenuscriptDeliveryLink.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Id = i.ManuscriptId,
                    ManuscriptDeliveryFormat = i.ManuscriptDeliveryFormatMaster.ManuscriptDeliveryFormat
                }).ToList();
                var _ttlSusidiary = _Contract.AuthorContractSubsidiaryRights.GroupBy(i => i.subsidiaryrightsid).Count();
                var TblList = _Contract.AuthorContractauthordetails.Select(i => new { Id = i.AuthorId, AuthorName = i.AuthorMaster.FirstName + " " + i.AuthorMaster.LastName }).ToList();
                return Json(new { _AuhtorContract, _royalty, _AuthorList, _contributor, _MaterialDate, _susidiaryRightsList, TblList, _ttlSusidiary, status, _ContractAgreement, _agreementDoc, _ManuscriptDeliveryList, _ThirdPartyContractList });
            }
            catch (Exception Ex)
            {
                status = Ex.ToString();
                return Json(new { status });
            }

        }

        public IHttpActionResult RemoveAuhtorDocumentLink(AuthorContractDocument Docoument)
        {
            string status = string.Empty;
            try
            {

                _IAuthorContractService.DeavtivateauthorContractDocumentsLinkById(Docoument.Id);
                status = "Deleted";

            }

            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }
        /** Create By    :   Saddam
           Create On    :   28rd june 2016 
           Created For   :   Fetching the details of Author Contract Search
        */
        public IHttpActionResult AuthorContractSearch(AuthorContractHistory SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _IAuthorContractService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAuthorContractListing(String SessionId)
        {
            try
            {
                if (SessionId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SessionId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_AuthorContractDetails_get", parameters).ToList();

                    if (_GetAuthorReport1.Count == 0)
                    {
                        parameters = new SqlParameter[1];
                        parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                        parameters[0].Value = "'" + SessionId + "'";

                        _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_AuthorContractDetailsByChild_get", parameters).ToList();                        
                    }

                    var royalty = _IAuthorContractService.AuthorContractRoyality();
                    var ProductTypeMaster = _IProductType.GetAllProductTypeList();

                    var _GetAuthorReport = (from data in _GetAuthorReport1
                                            select new
                                              {
                                                  AuthorContractId = data.AuthorContractId,
                                                  AuthorContractCode = data.AuthorContractCode,
                                                  ProductCode = data.ProductCode,
                                                  ProjectCode = data.ProjectCode,
                                                  WorkingTitle = data.WorkingTitle,
                                                  WorkingSubProduct = data.WorkingSubProduct,
                                                  oupisbn = data.oupisbn,
                                                  AuthorName = data.AuthorName,
                                                  ContractEntryDate = data.ContractEntryDate,
                                                  ContractExpiryDate = data.ContractExpiryDate,
                                                  DateOfAgreement = data.DateOfAgreement,
                                                  ProductId = data.ProductId,
                                                  Flag = data.Flag,
                                                  UpdateFlag = data.UpdateFlag,
                                                  ContractEntryDate_EntryDate = data.ContractEntryDate_EntryDate,
                                                  ExecutiveName = data.ExecutiveName,
                                                  divisionName = data.divisionName,
                                                  Remarks = data.Remarks,
                                                  ThirdPartyPermission = data.ThirdPartyPermission,

                                                  DateOfAgreementForSort = data.DateOfAgreementForSort,
                                                  ContractExpiryDateForSort = data.ContractExpiryDateForSort,
                                                  ContractEntryDate_EntryDateForSort = data.ContractEntryDate_EntryDateForSort,
                                                  AddendumId = data.AddendumId,

                                                  Royalty = from rol in royalty.Where(a => a.AuthorContractid == data.AuthorContractId).OrderBy(a => a.subproducttypeid)
                                                            join type in ProductTypeMaster
                                                            on rol.subproducttypeid equals type.Id
                                                            select new {
                                                                SubProductType  = type.typeName,
                                                                CopiesFrom = rol.CopiesFrom,
                                                                CopiesTo = rol.CopiesTo,
                                                                Percentage = rol.Percentage,
                                                            }


                                              });

                    return Json(JsonSerializer.SerializeObj.SerializeObject(_GetAuthorReport.ToList()));
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "GetAuthorContractListing", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "GetAuthorContractListing", ex);
                return Json(ex.InnerException);
            }

        }

        public IHttpActionResult getAlltheDocument(int agreementid)
        {
            var _agreementDoc = _IAuthorContractService.getAuthorDocument(agreementid).ToList().Select(i => new
            {
                Id = i.Id,
                FileName = i.FileName,
                FileNameEntered = i.FileNameEntered,
                DocumentTypeId = i.DocumentTypeId,
                type = i.DocumentTypeMaster.DocumentTypeName,
                agreementId = i.AgreementId
            }).ToList();

            return Json(new { _agreementDoc });
        }

        string InsertAuthorContract(AuthorContractModel AuthorContractModel, int? productid = 0, int? SeriesId = 0, string seriesCode = "")
        {
            AuthorContractOriginal _Contract = new AuthorContractOriginal();
            string status = ""; string Code = "";
            int Id = 0;
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {


                    /*====================================================================================================================
                       * This section will assign Author contract object insert parame
                     ======================================================================================================================*/
                    _Contract.AuthorContractCode = GenerateSeriesCode("AuthorContract", "AC");
                    _Contract.AuthorContractCode = _Contract.AuthorContractCode.ToString().ToUpper();
                    _Contract.ExecutiveCode = AuthorContractModel.ExecutiveCode;
                    _Contract.ProductId = productid.GetValueOrDefault() == 0 ? AuthorContractModel.ProductId : productid.GetValueOrDefault();
                    //_Contract.ContractTypeId = AuthorContractModel.ContractTypeId;
                    _Contract.SeriesId = SeriesId != 0 ? SeriesId : null;
                    _Contract.SeriesCode = SeriesId != 0 && seriesCode != "" ? seriesCode : null;
                    _Contract.Status = "Pending";
                    _Contract.NoOfAuthors = AuthorContractModel.NoofAuthors;
                    _Contract.ContractEntryDate = AuthorContractModel.ContractEntryDate;
                    _Contract.ContractDate = AuthorContractModel.ContractDate;
                    _Contract.TermsOfCopyright = AuthorContractModel.termsofcopyright;
                    _Contract.ContractExpiryDate = AuthorContractModel.termsofcopyright == 1 ? AuthorContractModel.ContractExpirydate : null;
                    _Contract.contractperiodinmonth = AuthorContractModel.periodOfAgreement;
                    _Contract.BuyBack = AuthorContractModel.BuyBack;
                    _Contract.NatureOfWork = AuthorContractModel.NatureofWork;
                    _Contract.CopyrightOwner = AuthorContractModel.CopyRightOwner;
                    _Contract.Territoryrightsid = AuthorContractModel.TerritoryId;
                    //_Contract.thirdpartypermission = AuthorContractModel.ThirdPartyPermission;
                    _Contract.Amendment = AuthorContractModel.Amendment;
                    _Contract.AmendmentRemarks = AuthorContractModel.AmendmentRemarks;
                    _Contract.Restriction = AuthorContractModel.Restriction;
                    _Contract.subjectMatterAndTreatment = AuthorContractModel.subjectMatterAndTreatment;
                    _Contract.MinNoOfwords = AuthorContractModel.MinNoOfWords;
                    _Contract.MaxNoOfwords = AuthorContractModel.MaxNoOfWords;
                    _Contract.MinNoOfPages = AuthorContractModel.MinNoOfPages;
                    _Contract.MaxNoOfPages = AuthorContractModel.MaxNoOfPages;
                    _Contract.PriceType = AuthorContractModel.PriceType;
                    _Contract.Price = AuthorContractModel.Price;
                    _Contract.CurrencyId = AuthorContractModel.CurrencyId == 0 ? (int?)null : AuthorContractModel.CurrencyId;
                    _Contract.MediumOfdelivery = AuthorContractModel.mediumOfDelivery;
                    _Contract.ManuscriptId = AuthorContractModel.MenuScriptDeliveryFormatId == 0 ? null : AuthorContractModel.MenuScriptDeliveryFormatId;
                    _Contract.Deliveryschedule = AuthorContractModel.deliverySchedule;
                    _Contract.EnteredBy = AuthorContractModel.EnteredBy;
                    //_Contract.NoofAuthors = AuthorContractModel.NoofAuthors;
                    _Contract.ProductRemarks = AuthorContractModel.ProductRemarks;
                    _Contract.LicenseId = AuthorContractModel.licenseId;

                    /*====================================================================================================================
                      * This section will assign AuthorContractContributor object insert parame
                    ======================================================================================================================*/
                    IList<AuthorContractContributor> obj_abc = new List<AuthorContractContributor>();
                    foreach (var item in AuthorContractModel.ContributorName)
                    {
                        AuthorContractContributor obj_contributor = new AuthorContractContributor();
                        obj_contributor.ContributorName = item.Contributor;
                        obj_contributor.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_contributor.Deactivate = "N";
                        obj_contributor.EntryDate = DateTime.Now;
                        obj_abc.Add(obj_contributor);
                    }

                    _Contract.AuthorContactContibutor = obj_abc;

                    /*====================================================================================================================
                    * This section will assign AuthorContractmaterialdetails object insert parame
                  ======================================================================================================================*/

                    IList<AuthorContractmaterialdetails> obj_authorMaterial = new List<AuthorContractmaterialdetails>();


                    foreach (var item in AuthorContractModel.SupplyMaterialbyAuthor)
                    {
                        AuthorContractmaterialdetails obj_author = new AuthorContractmaterialdetails();
                        obj_author.MaterialId = item.MaterialId;
                        obj_author.materialdate = item.materialDate;
                        obj_author.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_author.Deactivate = "N";
                        obj_author.EntryDate = DateTime.Now;
                        obj_authorMaterial.Add(obj_author);
                    }
                    _Contract.AuthorContractmaterialdetails = obj_authorMaterial;

                    /*====================================================================================================================
                        * This section will assign AuthorContractSubsidiaryRights object insert parame
                     ======================================================================================================================*/
                    IList<AuthorContractSubsidiaryRights> obj_SubsidiaryRights = new List<AuthorContractSubsidiaryRights>();

                    foreach (var item in AuthorContractModel.AuthorSubsidiaryRights)
                    {
                        var list = item.SusidiaryRights.ToList();
                        foreach (var lst in list)
                        {
                            AuthorContractSubsidiaryRights obj_Subsidiary = new AuthorContractSubsidiaryRights();
                            obj_Subsidiary.AuthorId = lst.authorId;
                            obj_Subsidiary.subsidiaryrightsid = lst.subsidiaryid;
                            obj_Subsidiary.AuthorPercentage = lst.Percentage;
                            obj_Subsidiary.ouppercentage = lst.OupPercentage;
                            obj_Subsidiary.EnteredBy = AuthorContractModel.EnteredBy;
                            obj_Subsidiary.EntryDate = DateTime.Now;
                            obj_Subsidiary.Deactivate = "N";
                            obj_SubsidiaryRights.Add(obj_Subsidiary);
                            
                        }
                    }
                    _Contract.AuthorContractSubsidiaryRights = obj_SubsidiaryRights;
                    /*====================================================================================================================
                       * This section will assign AuthorContractauthordetails and  AuthorContractRoyality object insert parame
                    ======================================================================================================================*/
                    IList<AuthorContractauthordetails> obj_AuthorContract = new List<AuthorContractauthordetails>();


                    foreach (var item in AuthorContractModel.AuthorContactDetails)
                    {
                        IList<AuthorContractRoyality> obj_Royality = new List<AuthorContractRoyality>();
                        AuthorContractauthordetails obj_authordetails = new AuthorContractauthordetails();
                        var list = item.RoyaltySlab.ToList();
                        foreach (var lst in list)
                        {
                            AuthorContractRoyality Royalty = new AuthorContractRoyality();
                            Royalty.subproducttypeid = lst.subproductTypeId;
                            Royalty.CopiesFrom = lst.CopiesFrom;
                            Royalty.CopiesTo = lst.CopiesTo;
                            Royalty.Percentage = lst.Percentage;
                            Royalty.EnteredBy = AuthorContractModel.EnteredBy;
                            Royalty.EntryDate = DateTime.Now;
                            Royalty.Deactivate = "N";
                            obj_Royality.Add(Royalty);
                        }
                        obj_authordetails.AuthorContractRoyality = obj_Royality;
                        obj_authordetails.Authortype = item.AuthorTypeId;
                        obj_authordetails.ContractTypeId = item.ContractTypeId;
                        obj_authordetails.AuthorId = item.AuthorId;
                        obj_authordetails.paymentperiodid = item.PaymentperiodId != 0 ? item.PaymentperiodId : (int?)null;
                        obj_authordetails.AuthorCopies = (int?)item.AuthorCopies != 0 ? item.AuthorCopies : (int?)null;
                        obj_authordetails.Seedmoney = (int?)item.SendMoney != 0 ? item.SendMoney : (double?)null;
                        obj_authordetails.onetimepayment = (int?)item.OneTimePayment != 0 ? item.OneTimePayment : (double?)null;
                        obj_authordetails.advanceroyality = (int?)item.AdvanceRoyalty != 0 ? item.AdvanceRoyalty : (double?)null;
                        obj_authordetails.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_authordetails.EntryDate = DateTime.Now;
                        obj_authordetails.Deactivate = "N";
                        obj_AuthorContract.Add(obj_authordetails);
                    }
                    _Contract.AuthorContractauthordetails = obj_AuthorContract;

                    /****************************************************************************************************************
                   * section for updating Menuscript dilivery format
                   *******************************************************************************************************************/

                    IList<AuthorContractMenuscriptDeliveryLink> MenuscriptDeliveryLink = new List<AuthorContractMenuscriptDeliveryLink>();
                    if (AuthorContractModel.ManuScriptFormatList != null)
                    {
                        foreach (var _obj in AuthorContractModel.ManuScriptFormatList)
                        {
                            AuthorContractMenuscriptDeliveryLink AuthorContractMenuscriptDeliveryLink = new AuthorContractMenuscriptDeliveryLink();
                            AuthorContractMenuscriptDeliveryLink.EnteredBy = _Contract.EnteredBy;
                            AuthorContractMenuscriptDeliveryLink.EntryDate = DateTime.Now;
                            AuthorContractMenuscriptDeliveryLink.ManuscriptId = _obj.MenuScriptId;
                            AuthorContractMenuscriptDeliveryLink.Deactivate = "N";
                            MenuscriptDeliveryLink.Add(AuthorContractMenuscriptDeliveryLink);
                        }
                        _Contract.AuthorContractMenuscriptDeliveryLink = MenuscriptDeliveryLink;
                    }
                    /*====================================================================================================================
                       * Final Insertion of the code
                    ======================================================================================================================*/
                    Id = _IAuthorContractService.InsertAuthorContract(_Contract);
                    /*====================================================================================================================
                    * End section
                 ======================================================================================================================*/
                    //if (AuthorContractModel.licenseId != null)
                    //{
                    //    ProductLicenceAuthorContractLink _link = new ProductLicenceAuthorContractLink();
                    //    _link.licenseId = AuthorContractModel.licenseId.GetValueOrDefault(); ;
                    //    _link.ProductId = AuthorContractModel.ProductId;
                    //    _link.AuthorContractId = Id;
                    //    _link.EnteredBy = AuthorContractModel.EnteredBy;
                    //    _IAuthorContractService.ProductLicenceAuthorContractLink(_link);
                    //}



                    ////----save Amendment Document
                    string[] docurl1 = AuthorContractModel.UploadFile.Split(',');
                    int i = 0;
                    foreach (string doc in AuthorContractModel.DocumentName)
                    {
                        AuthorAmendmentDocument Link = new AuthorAmendmentDocument();
                        Link.AuthorContractId = Id;
                        Link.Documentname = doc;
                        Link.documentfile = docurl1[i];
                        Link.EnteredBy = AuthorContractModel.EnteredBy;
                        _IAuthorContractService.InsertAuthorAmendmentDocumentLinking(Link);
                        i++;
                    }



                    IList<ProductPreviousProductLink> ProductPreviousProductLinkList = _IAuthorContractService.ProductPreviousProductLinkList(productid.GetValueOrDefault() == 0 ? AuthorContractModel.ProductId : productid.GetValueOrDefault());
                    if (ProductPreviousProductLinkList != null)
                    {
                        foreach (ProductPreviousProductLink PreviousProductLink in ProductPreviousProductLinkList)
                        {
                            ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                            _ProductPreviousProductLink.ProductId = PreviousProductLink.ProductId;
                            _ProductPreviousProductLink.PreviousProductId = PreviousProductLink.PreviousProductId;
                            _ProductPreviousProductLink.AuthorContractId = Id;
                            _ProductPreviousProductLink.Deactivate = "N";
                            _ProductPreviousProductLink.EnteredBy = AuthorContractModel.EnteredBy;
                            _ProductPreviousProductLink.EntryDate = DateTime.Now;
                            _ProductPreviousProductLink.ModifiedBy = null;
                            _ProductPreviousProductLink.ModifiedDate = null;
                            _ProductPreviousProductLink.DeactivateBy = null;
                            _ProductPreviousProductLink.DeactivateDate = null;
                            _SeriesProductEntryService.InsertProductPreviousProductLink(_ProductPreviousProductLink);
                        }
                    }

                    status = "OK";
                    Code = _Contract.AuthorContractCode;
                    scope.Complete();
                    return status + "," + Code + "," + Id;
                }
                catch (Exception ex)
                {
                    status = ex.ToString();
                    return status;
                }
            }
        }
        string UpdateAuthorContract(AuthorContractModel AuthorContractModel)
        {
            AuthorContractOriginal _Contract = new AuthorContractOriginal();
            string status = "";
            using (var scope = new System.Transactions.TransactionScope())
            {
                try
                {    //Get the data from the database and assign the new value to it.
                    _Contract = _IAuthorContractService.GetAuthorContractById(AuthorContractModel.Id);
                    _Contract.ExecutiveCode = _Contract.ExecutiveCode;
                    _Contract.ProductId = AuthorContractModel.ProductId;
                    //_Contract.ContractTypeId = AuthorContractModel.ContractTypeId;
                    _Contract.NoOfAuthors = AuthorContractModel.NoofAuthors;
                    
                    //_Contract.ContractEntryDate = AuthorContractModel.ContractEntryDate;
                    // _Contract.ContractDate = AuthorContractModel.ContractDate;


                    _Contract.TermsOfCopyright = AuthorContractModel.termsofcopyright;
                    _Contract.ContractExpiryDate = AuthorContractModel.termsofcopyright == 1 ? AuthorContractModel.ContractExpirydate : null;
                    _Contract.contractperiodinmonth = AuthorContractModel.periodOfAgreement;
                    _Contract.BuyBack = AuthorContractModel.BuyBack;
                    _Contract.NatureOfWork = AuthorContractModel.NatureofWork;
                    _Contract.CopyrightOwner = AuthorContractModel.CopyRightOwner;
                    _Contract.Territoryrightsid = AuthorContractModel.TerritoryId;
                    //_Contract.thirdpartypermission = AuthorContractModel.ThirdPartyPermission;
                    _Contract.Amendment = AuthorContractModel.Amendment;
                    _Contract.AmendmentRemarks = AuthorContractModel.AmendmentRemarks;
                    _Contract.Restriction = AuthorContractModel.Restriction;
                    _Contract.subjectMatterAndTreatment = AuthorContractModel.subjectMatterAndTreatment;
                    _Contract.MinNoOfwords = AuthorContractModel.MinNoOfWords;
                    _Contract.MaxNoOfwords = AuthorContractModel.MaxNoOfWords;
                    _Contract.MinNoOfPages = AuthorContractModel.MinNoOfPages;
                    _Contract.MaxNoOfPages = AuthorContractModel.MaxNoOfPages;
                    _Contract.PriceType = AuthorContractModel.PriceType;
                    _Contract.Price = AuthorContractModel.Price;
                    _Contract.CurrencyId = AuthorContractModel.CurrencyId == 0 ? (int?)null : AuthorContractModel.CurrencyId;
                    _Contract.MediumOfdelivery = AuthorContractModel.mediumOfDelivery;
                    _Contract.ManuscriptId = AuthorContractModel.MenuScriptDeliveryFormatId == 0 ? null : AuthorContractModel.MenuScriptDeliveryFormatId;
                    _Contract.Deliveryschedule = AuthorContractModel.deliverySchedule;
                    _Contract.EnteredBy = AuthorContractModel.EnteredBy;
                    _Contract.ProductRemarks = AuthorContractModel.ProductRemarks;
                    _Contract.ModifiedDate = DateTime.Now;
                    _Contract.ModifiedBy = AuthorContractModel.EnteredBy;
                    // _Contract.Status = AuthorContractModel.ContractStatus;

                    List<AuthorContractContributor> _List = _Contract.AuthorContactContibutor.ToList();

                    _IAuthorContractService.DeactivateAuthorContributor(_Contract.AuthorContactContibutor.ToList());
                    _IAuthorContractService.DeactivateAuthorMaterial(_Contract.AuthorContractmaterialdetails.ToList());
                    _IAuthorContractService.DeativateAuthorContractSubsidiaryRights(_Contract.AuthorContractSubsidiaryRights.ToList());
                    _IAuthorContractService.DeativateAuthorandRoyaltySlab(_Contract.AuthorContractauthordetails.ToList());



                    /*====================================================================================================================
                      * This section will assign AuthorContractContributor object insert parame
                    ======================================================================================================================*/


                    IList<AuthorContractContributor> _Contributor = new List<AuthorContractContributor>();
                    foreach (var item in AuthorContractModel.ContributorName)
                    {
                        AuthorContractContributor obj_contributor = new AuthorContractContributor();
                        obj_contributor.AuthorContractId = AuthorContractModel.Id;
                        obj_contributor.ContributorName = item.Contributor;
                        obj_contributor.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_contributor.Deactivate = "N";
                        obj_contributor.EntryDate = DateTime.Now;
                        _Contributor.Add(obj_contributor);
                    }

                    _Contract.AuthorContactContibutor = _Contributor;

                    //  /*====================================================================================================================
                    //  * This section will assign AuthorContractmaterialdetails object insert parame
                    //======================================================================================================================*/

                    // // IList<AuthorContractmaterialdetails> obj_authorMaterial = new List<AuthorContractmaterialdetails>();
                    IList<AuthorContractmaterialdetails> _materialList = new List<AuthorContractmaterialdetails>();


                    //_materialList.RemoveAll(i => i.Id > 0);

                    foreach (var item in AuthorContractModel.SupplyMaterialbyAuthor)
                    {
                        AuthorContractmaterialdetails obj_author = new AuthorContractmaterialdetails();
                        obj_author.AuthorContractId = AuthorContractModel.Id;
                        obj_author.MaterialId = item.MaterialId;
                        obj_author.materialdate = item.materialDate;
                        obj_author.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_author.Deactivate = "N";
                        obj_author.EntryDate = DateTime.Now;
                        _materialList.Add(obj_author);
                    }
                    _Contract.AuthorContractmaterialdetails = _materialList;

                    /*====================================================================================================================
                        * This section will assign AuthorContractSubsidiaryRights object insert parame
                     ======================================================================================================================*/
                    IList<AuthorContractSubsidiaryRights> obj_SubsidiaryRights = new List<AuthorContractSubsidiaryRights>();

                    foreach (var item in AuthorContractModel.AuthorSubsidiaryRights)
                    {
                        var list = item.SusidiaryRights.ToList();
                        foreach (var lst in list)
                        {
                            AuthorContractSubsidiaryRights obj_Subsidiary = new AuthorContractSubsidiaryRights();
                            obj_Subsidiary.AuthorContractid = AuthorContractModel.Id;
                            obj_Subsidiary.AuthorId = lst.authorId;
                            obj_Subsidiary.subsidiaryrightsid = lst.subsidiaryid;
                            obj_Subsidiary.AuthorPercentage = lst.Percentage;
                            obj_Subsidiary.ouppercentage = lst.OupPercentage;
                            obj_Subsidiary.EnteredBy = AuthorContractModel.EnteredBy;
                            obj_Subsidiary.EntryDate = DateTime.Now;
                            obj_Subsidiary.Deactivate = "N";
                            obj_SubsidiaryRights.Add(obj_Subsidiary);

                            //if (obj_Subsidiary.AuthorPercentage != 0 &&  obj_Subsidiary.ouppercentage == 100)
                            //{
                            //    obj_SubsidiaryRights.Add(obj_Subsidiary);
                            //}
                            //else
                            //{
                            //    continue;
                            //}


                        }
                    }
                    _Contract.AuthorContractSubsidiaryRights = obj_SubsidiaryRights;
                    /*====================================================================================================================
                       * This section will assign AuthorContractauthordetails and  AuthorContractRoyality object insert parame
                    ======================================================================================================================*/

                    IList<AuthorContractauthordetails> obj_AuthorContract = new List<AuthorContractauthordetails>();

                    foreach (var item in AuthorContractModel.AuthorContactDetails)
                    {
                        IList<AuthorContractRoyality> obj_Royality = new List<AuthorContractRoyality>();
                        AuthorContractauthordetails obj_authordetails = new AuthorContractauthordetails();
                        var list = item.RoyaltySlab.ToList();
                        foreach (var lst in list)
                        {
                            AuthorContractRoyality Royalty = new AuthorContractRoyality();
                            Royalty.AuthorContractid = AuthorContractModel.Id;
                            Royalty.subproducttypeid = lst.subproductTypeId;
                            Royalty.CopiesFrom = lst.CopiesFrom;
                            Royalty.CopiesTo = lst.CopiesTo;
                            Royalty.Percentage = lst.Percentage;
                            Royalty.EnteredBy = AuthorContractModel.EnteredBy;
                            Royalty.EntryDate = DateTime.Now;
                            Royalty.Deactivate = "N";
                            obj_Royality.Add(Royalty);
                        }
                        obj_authordetails.AuthorContractid = AuthorContractModel.Id;
                        obj_authordetails.AuthorContractRoyality = obj_Royality;
                        obj_authordetails.ContractTypeId = item.ContractTypeId;
                        obj_authordetails.Authortype = item.AuthorTypeId;
                        obj_authordetails.AuthorId = item.AuthorId;
                        obj_authordetails.paymentperiodid = item.PaymentperiodId;
                        obj_authordetails.AuthorCopies = item.AuthorCopies;
                        obj_authordetails.Seedmoney = item.SendMoney;
                        obj_authordetails.onetimepayment = item.OneTimePayment;
                        obj_authordetails.advanceroyality = item.AdvanceRoyalty;
                        obj_authordetails.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_authordetails.EntryDate = DateTime.Now;
                        obj_authordetails.Deactivate = "N";
                        obj_AuthorContract.Add(obj_authordetails);
                    }

                    _Contract.AuthorContractauthordetails = obj_AuthorContract;
                    /****************************************************************************************************************
                    * section for updating Menuscript dilivery format
                    *******************************************************************************************************************/
                    _IAuthorContractService.DeativateMenuscriptDeliveryLink(_Contract.AuthorContractMenuscriptDeliveryLink.ToList());
                    IList<AuthorContractMenuscriptDeliveryLink> MenuscriptDeliveryLink = new List<AuthorContractMenuscriptDeliveryLink>();
                    if (AuthorContractModel.ManuScriptFormatList != null)
                    {
                        foreach (var _obj in AuthorContractModel.ManuScriptFormatList)
                        {
                            AuthorContractMenuscriptDeliveryLink AuthorContractMenuscriptDeliveryLink = new AuthorContractMenuscriptDeliveryLink();
                            AuthorContractMenuscriptDeliveryLink.EnteredBy = _Contract.EnteredBy;
                            AuthorContractMenuscriptDeliveryLink.EntryDate = DateTime.Now;
                            AuthorContractMenuscriptDeliveryLink.ManuscriptId = _obj.MenuScriptId;
                            AuthorContractMenuscriptDeliveryLink.Deactivate = "N";
                            MenuscriptDeliveryLink.Add(AuthorContractMenuscriptDeliveryLink);
                        }
                        _Contract.AuthorContractMenuscriptDeliveryLink = MenuscriptDeliveryLink;
                    }
                    /*====================================================================================================================
                       * Final updattion of the code
                    ======================================================================================================================*/
                    _IAuthorContractService.UpdateAuthorContract(_Contract);


                    ////----save Amendment Document
                    if (AuthorContractModel.Id != null && AuthorContractModel.ContractStatus != "Draft" && AuthorContractModel.ContractStatus != "Cancelled")
                    {
                        string[] docurl1 = AuthorContractModel.UploadFile.Split(',');
                        int i = 0;
                        foreach (string doc in AuthorContractModel.DocumentName)
                        {
                            AuthorAmendmentDocument Link = new AuthorAmendmentDocument();
                            Link.AuthorContractId = AuthorContractModel.Id;
                            Link.Documentname = doc;
                            Link.documentfile = docurl1[i];
                            Link.EnteredBy = AuthorContractModel.EnteredBy;
                            _IAuthorContractService.InsertAuthorAmendmentDocumentLinking(Link);
                            i++;
                        }

                    }


                  
                  /*====================================================================================================================
                    * End section
                 ======================================================================================================================*/
                    scope.Complete();
                    status = "OK";
                  //  getListAuthorContractStatusMail(AuthorContractModel.Id, AuthorContractModel.EnteredBy);
                   
                    return status + "," + "Update";
                  

                }
                catch (Exception ex)
                {
                    status = ex.ToString();
                    return status;
                }
           }
        }
        /** Create By    :   Saddam
           Create On    :   14th july 2016 
           Created For   :   Fetching the details of author contract
        */
        public IHttpActionResult AuthorContractDetails(AuthorcontractDetail AuthorContract)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (AuthorContract != null)
                {
                    parameters[0] = new SqlParameter("AuthorContractId", SqlDbType.VarChar, 50);
                    if (AuthorContract.AuthorContractId == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = AuthorContract.AuthorContractId;
                    }


                }


            }
            catch (Exception ex)
            {
            }
            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorcontractDetail>("Proc_AuthorContract_Detail_get", parameters).ToList();


            return Json(_GetAuthorReport);
        }
        /* Create By  : Saddam
         * Create on  : 26st July 2016
         * Create for : Fetaching Details for Series Contract Using Product Master, Series Master and Product Category Master Tables
         */
        /* public IHttpActionResult GetProductSeriesContract(int ProductId)
         {
             var Mobj_ProductSeriesList = (from Pm in _ProductMaster.Table.Where(a => a.Deactivate == "N")
                                           join SM in _SeriesMaster.Table.Where(a => a.Deactivate == "N")
                                           on Pm.SeriesId equals SM.Id into firtTbl
                                           from d in firtTbl.DefaultIfEmpty()
                                           join PCM in _ProductCategoryMaster.Table.Where(a => a.Deactivate == "N")
                                           on Pm.ProductCategoryId equals PCM.Id into output
                                           from g in output.DefaultIfEmpty()

                                           select new
                                           {
                                               Id = Pm.Id,
                                               ProductCode = Pm.ProductCode.ToUpper(),
                                               WorkingProduct = Pm.WorkingProduct,
                                               ProductCategory = g.ProductCategory,
                                               Deative = Pm.Deactivate,
                                               Contract_Type = "Author",
                                               SeriesId = Pm.SeriesId
                                           }

                 ).Distinct().Where(a => a.Deative == "N" && a.SeriesId == ProductId).OrderBy(a => a.ProductCode);

             return Json(Mobj_ProductSeriesList);
         }*/


        /* Create By  : Ankush Kumar
        * Create on  : 02nd Aug 2016
         * 
         * Modify By  : Prakash
        * Modify on  :  07 june, 2017
        */
        public IHttpActionResult GetProductSeriesContract(int SeriesId,string For="")
        {
            //---added by prakash on 07 june, 2017
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("SeriesId", SqlDbType.VarChar, 100);
            parameters[0].Value = "'" + SeriesId + "'";
            parameters[1] = new SqlParameter("For", SqlDbType.VarChar, 100);
            parameters[1].Value = "'" + For + "'";

            var _ProductSeriesContractList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.ProductSeriesContract>("Proc_GetProductSeriesContractList_Get", parameters).ToList();

            return Json(_ProductSeriesContractList);

            ////---commented by prakash on 07 june, 2017
            //var AuthorContractList = _IAuthorContractService.GetAuthorContractBySeriesId1(SeriesId).Where(a => a.SeriesCode != null);

            //var SeriesCodeUnique = AuthorContractList.Select(a => a.SeriesCode).Distinct().ToList();

            //if (For == "Addendum")
            //{
            //   var data = (from seriesCodeUnique in SeriesCodeUnique
            //                select new
            //                {
            //                    SeriesCode = seriesCodeUnique,
            //                    AuthorContractCodeList = (from ACL in AuthorContractList.Where(a => a.Deactivate == "N" && a.SeriesCode == seriesCodeUnique)
            //                                              join agreement in _AuthorAgreement.Table.Where(a => a.Deactivate == "N")
            //                                              on ACL.SeriesCode equals agreement.SeriesCode
            //                                              into firtTbl
            //                                              from agreement in firtTbl.DefaultIfEmpty()
            //                                              join Add in _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == seriesCodeUnique)
            //                                              on ACL.SeriesCode equals Add.SeriesCode 
            //                                              into firtTb2
            //                                              from Add in firtTb2.DefaultIfEmpty()

            //                                              join pppl in _ProductPreviousProductLink.Table.Where(x => x.Deactivate == "N")
            //                                              on ACL.ProductId equals pppl.PreviousProductId
            //                                              into ppplTbl
            //                                              from pppl in ppplTbl.DefaultIfEmpty()

            //                                            select  new
            //                                            {
            //                                                AuthorContractCode = ACL.AuthorContractCode,
            //                                                ContractEntryDate = ACL.ContractEntryDate.toDDMMYYYY(),
            //                                                ContractExpiryDate = (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                                ProductName = _ProductMaster.GetById(ACL.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(ACL.ProductId).WorkingProduct,
            //                                                //Flag = ACL.Status == "Issued" ? Add.AuthorContractId == null ? "0" : Add.AuthorContractId == ACL.Id ? "1" : "2" : "0"
            //                                                Flag = (ACL.Status == "Issued" ? (Add == null ? "2" : (Add.SeriesCode == ACL.SeriesCode ? "1" : "2")) : "0"),
            //                                                //Flag =(ACL.Status == "Issued" ? (Add.AuthorContractId.ToString) : "1")

            //                                                ChildAuthorContractCode = (pppl == null) ? "---" : ACL.AuthorContractCode,
            //                                                ChildContractEntryDate = (pppl == null) ? "---" : ACL.ContractEntryDate.toDDMMYYYY(),
            //                                                ChildContractExpiryDate = (pppl == null) ? "---" : (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                                ChildProductName = (pppl == null) ? "---" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct,

            //                                            }).Distinct().ToList()
            //                }).ToList();

            //    return Json(data);

            //}
            //else
            //{
            //    var data = (from seriesCodeUnique in SeriesCodeUnique

            //                select new
            //                {
            //                    SeriesCode = seriesCodeUnique,
            //                    AuthorContractCodeList = (from b in AuthorContractList.Where(b => b.SeriesCode == seriesCodeUnique)
            //                                              join agreement in _AuthorAgreement.Table.Where(a => a.Deactivate == "N")
            //                                              on b.SeriesCode equals agreement.SeriesCode
            //                                              into firtTbl
            //                                              from agreement in firtTbl.DefaultIfEmpty()

            //                                              join pppl in _ProductPreviousProductLink.Table.Where(x => x.Deactivate == "N")
            //                                              on b.ProductId equals pppl.PreviousProductId
            //                                              into ppplTbl
            //                                              from pppl in ppplTbl.DefaultIfEmpty()
                                                          
            //                                              select new
            //                                              {
            //                                                  AuthorContractCode = b.AuthorContractCode,
            //                                                  ContractEntryDate = b.ContractEntryDate.toDDMMYYYY(),
            //                                                  ContractExpiryDate = (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                                  ProductName = _ProductMaster.GetById(b.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(b.ProductId).WorkingProduct,
            //                                                  //Flag = b.Status == "Issued" ? _AuthorContractAddendumDetails.Table.Where(c => c.AuthorContractId == b.Id).FirstOrDefault().AuthorContractId == b.Id ? "1" : "2" : "0"
            //                                                  Flag = b.Status == "Issued" ? "1" : "0",

            //                                                  ChildAuthorContractCode = (pppl == null) ? "---" : b.AuthorContractCode,
            //                                                  ChildContractEntryDate = (pppl == null) ? "---" : b.ContractEntryDate.toDDMMYYYY(),
            //                                                  ChildContractExpiryDate = (pppl == null) ? "---" : (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                                  ChildProductName = (pppl == null) ? "---" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct,

            //                                              }).Distinct().ToList(),
                                
            //                }).ToList();


            //    return Json(data);

            //}

        }

        /* Create By  : Prakash
           Create on  : 19 May, 2017
        */
         [HttpGet]
        public IHttpActionResult GetPendingProductSeriesContract(string param)
        {
            //---added on 07 june, 2017
            var mstr_SeriesCode = "";

            var AuthorContractList = _IAuthorContractService.GetAuthorContractSeries().Where(a => a.SeriesCode != null).ToList();
            var AuthorContractAgreementList = _AuthorAgreement.Table.Where(a => a.Deactivate == "N" && a.SeriesCode != null).Distinct().ToList();

            if (param == "pendingseries")
            {
                var AuthorContractSeriesCodeUnique = AuthorContractList.Select(a => a.SeriesCode).Distinct().ToList();
                var AuthorContractAgreementSeriesCodeUnique = AuthorContractAgreementList.Select(a => a.SeriesCode).Distinct().ToList();

                var SeriesCodeUnique = AuthorContractSeriesCodeUnique.Where(m => !AuthorContractAgreementSeriesCodeUnique.Contains(m)).Distinct().ToList();
                foreach (var items in SeriesCodeUnique)
                {
                    mstr_SeriesCode += items + ",";
                }
            }
            else if (param == "issueddraftseries")
            {
                var AuthorContractAgreementSeriesCodeUnique = AuthorContractAgreementList.Where(x => x.contractstatus == "Pending" || x.contractstatus == "Draft").Select(a => a.SeriesCode).Distinct().ToList();

                var SeriesCodeUnique = AuthorContractAgreementSeriesCodeUnique.OrderBy(a => a).ToList(); //.Take(3)
                foreach (var items in SeriesCodeUnique)
                {
                    mstr_SeriesCode += items + ",";
                }
            }


            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("SeriesCode", SqlDbType.VarChar, 100);
            parameters[0].Value = "'" + mstr_SeriesCode + "'";

            var _ProductSeriesContractList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.ProductSeriesContract>("Proc_GetPendingProductSeriesContractList_Get", parameters).ToList();

            return Json(_ProductSeriesContractList);

            ////---commented on 07 june, 2017
            //var data = (from seriesCodeUnique in SeriesCodeUnique
            //            select new
            //            {
            //                SeriesCode = seriesCodeUnique,
            //                AuthorContractCodeList = (from b in AuthorContractList.Where(b => b.SeriesCode == seriesCodeUnique)
            //                                          join agreement in _AuthorAgreement.Table.Where(a => a.Deactivate == "N")
            //                                          on b.SeriesCode equals agreement.SeriesCode
            //                                          into firtTbl
            //                                          from agreement in firtTbl.DefaultIfEmpty()

            //                                          join pppl in _ProductPreviousProductLink.Table.Where(x => x.Deactivate == "N")
            //                                          on b.ProductId equals pppl.PreviousProductId
            //                                          into ppplTbl
            //                                          from pppl in ppplTbl.DefaultIfEmpty()

            //                                          select new
            //                                          {
            //                                              AuthorContractCode = b.AuthorContractCode,
            //                                              ContractEntryDate = b.ContractEntryDate.toDDMMYYYY(),
            //                                              ContractExpiryDate = (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                              ProductName = _ProductMaster.GetById(b.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(b.ProductId).WorkingProduct,
            //                                              Flag = b.Status == "Issued" ? "1" : "0",

            //                                              ChildAuthorContractCode = (pppl == null) ? "---" : b.AuthorContractCode,
            //                                              ChildContractEntryDate = (pppl == null) ? "---" : b.ContractEntryDate.toDDMMYYYY(),
            //                                              ChildContractExpiryDate = (pppl == null) ? "---" : (agreement == null) ? "--" : agreement.Expirydate == null ? "--" : Convert.ToDateTime(agreement.Expirydate).toDDMMYYYY(),
            //                                              ChildProductName = (pppl == null) ? "---" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct == null ? "--" : _ProductMaster.GetById(pppl.ProductId).WorkingProduct,

            //                                          }).Distinct().ToList()
            //            }).ToList();


            //return Json(data);
        }

        /*******************************************************************************************************************
         Created By :   Dheeraj Kumar Sharma
         Created On :   1st aug 2016
         Created For :  Generating SeriesCode 
         ********************************************************************************************************************/

        protected string GenerateSeriesCode(string Key, string Prefix)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            //string numbers = "1234567890";
            //string characters = numbers;
            ApplicationSetUp _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == Key && x.Deactivate == "N").ToList().FirstOrDefault();
            ApplicationSetUp Mobj_ApplicationSetUp = new ApplicationSetUp();
            Mobj_ApplicationSetUp.Id = _ApplicationSetUpList.Id;
            ApplicationSetUp _ApplicationSetUpUpdate = _ApplicationSetUpService.GetApplicationSetUpById(Mobj_ApplicationSetUp);
            _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;
            int Value = Int32.Parse(_ApplicationSetUpList.keyValue) + 1;
            _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');
            _ApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);
            string otp = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, alphabets.Length);
                    character = alphabets.ToCharArray()[index].ToString();
                }
                while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return Prefix + otp.ToUpper() + _ApplicationSetUpList.keyValue.ToString();
        }


        /****************************************************************************************************************
         Created By  :  Dheeraj Kumar sharma
         Created For :  Update series Contract based on seriesCode
         Created on  :  1 st august
         ****************************************************************************************************************/

        string UpdateAuthorContract(AuthorContractModel AuthorContractModel, AuthorContractOriginal _Contract)
        {
            // AuthorContractOriginal _Contract = new AuthorContractOriginal();
            string status = "";
            AuthorContractModel.Id = AuthorContractModel.Id == 0 ? _Contract.Id : AuthorContractModel.Id;
            try
            {    //Get the data from the database and assign the new value to it.

                //Added by Saddam 
                //_Contract = _IAuthorContractService.GetAuthorContractById(AuthorContractModel.Id);
                //

                _Contract.ExecutiveCode = AuthorContractModel.ExecutiveCode;
                _Contract.NoOfAuthors = AuthorContractModel.NoofAuthors;

                //_Contract.ContractEntryDate = AuthorContractModel.ContractEntryDate;

                //_Contract.ContractDate = AuthorContractModel.ContractDate;


                _Contract.TermsOfCopyright = AuthorContractModel.termsofcopyright;
                _Contract.ContractExpiryDate = AuthorContractModel.termsofcopyright == 1 ? AuthorContractModel.ContractExpirydate : null;
                _Contract.contractperiodinmonth = AuthorContractModel.periodOfAgreement;
                _Contract.BuyBack = AuthorContractModel.BuyBack;
                _Contract.NatureOfWork = AuthorContractModel.NatureofWork;
                _Contract.CopyrightOwner = AuthorContractModel.CopyRightOwner;
                _Contract.Territoryrightsid = AuthorContractModel.TerritoryId;
                //_Contract.thirdpartypermission = AuthorContractModel.ThirdPartyPermission;
                _Contract.Amendment = AuthorContractModel.Amendment;
                _Contract.AmendmentRemarks = AuthorContractModel.AmendmentRemarks;
                _Contract.Restriction = AuthorContractModel.Restriction;
                _Contract.subjectMatterAndTreatment = AuthorContractModel.subjectMatterAndTreatment;
                _Contract.MinNoOfwords = AuthorContractModel.MinNoOfWords;
                _Contract.MaxNoOfwords = AuthorContractModel.MaxNoOfWords;
                _Contract.MinNoOfPages = AuthorContractModel.MinNoOfPages;
                _Contract.MaxNoOfPages = AuthorContractModel.MaxNoOfPages;
                _Contract.PriceType = AuthorContractModel.PriceType;
                _Contract.Price = AuthorContractModel.Price;
                _Contract.CurrencyId = AuthorContractModel.CurrencyId == 0 ? (int?)null : AuthorContractModel.CurrencyId;
                _Contract.MediumOfdelivery = AuthorContractModel.mediumOfDelivery;
                _Contract.ManuscriptId = AuthorContractModel.MenuScriptDeliveryFormatId == 0 ? null : AuthorContractModel.MenuScriptDeliveryFormatId;
                _Contract.Deliveryschedule = AuthorContractModel.deliverySchedule;
                _Contract.EnteredBy = AuthorContractModel.EnteredBy;
                _Contract.ProductRemarks = AuthorContractModel.ProductRemarks;
                _Contract.ModifiedDate = DateTime.Now;
               /// 
               
                _Contract.ModifiedBy = AuthorContractModel.EnteredBy;
                // _Contract.Status = AuthorContractModel.ContractStatus;

                List<AuthorContractContributor> _List = _Contract.AuthorContactContibutor.ToList();

                _IAuthorContractService.DeactivateAuthorContributor(_Contract.AuthorContactContibutor.ToList());
                _IAuthorContractService.DeactivateAuthorMaterial(_Contract.AuthorContractmaterialdetails.ToList());
                _IAuthorContractService.DeativateAuthorContractSubsidiaryRights(_Contract.AuthorContractSubsidiaryRights.ToList());
                _IAuthorContractService.DeativateAuthorandRoyaltySlab(_Contract.AuthorContractauthordetails.ToList());
                _IAuthorContractService.DeativateMenuscriptDeliveryLink(_Contract.AuthorContractMenuscriptDeliveryLink.ToList());



                /*====================================================================================================================
                  * This section will assign AuthorContractContributor object insert parame
                ======================================================================================================================*/


                IList<AuthorContractContributor> _Contributor = new List<AuthorContractContributor>();
                foreach (var item in AuthorContractModel.ContributorName)
                {
                    AuthorContractContributor obj_contributor = new AuthorContractContributor();
                    obj_contributor.AuthorContractId = AuthorContractModel.Id;
                    obj_contributor.ContributorName = item.Contributor;
                    obj_contributor.EnteredBy = AuthorContractModel.EnteredBy;
                    obj_contributor.Deactivate = "N";
                    obj_contributor.EntryDate = DateTime.Now;
                    _Contributor.Add(obj_contributor);
                }

                _Contract.AuthorContactContibutor = _Contributor;

                //  /*====================================================================================================================
                //  * This section will assign AuthorContractmaterialdetails object insert parame
                //======================================================================================================================*/

                // // IList<AuthorContractmaterialdetails> obj_authorMaterial = new List<AuthorContractmaterialdetails>();
                IList<AuthorContractmaterialdetails> _materialList = new List<AuthorContractmaterialdetails>();


                //_materialList.RemoveAll(i => i.Id > 0);

                foreach (var item in AuthorContractModel.SupplyMaterialbyAuthor)
                {
                    AuthorContractmaterialdetails obj_author = new AuthorContractmaterialdetails();
                    obj_author.AuthorContractId = AuthorContractModel.Id;
                    obj_author.MaterialId = item.MaterialId;
                    obj_author.materialdate = item.materialDate;
                    obj_author.EnteredBy = AuthorContractModel.EnteredBy;
                    obj_author.Deactivate = "N";
                    obj_author.EntryDate = DateTime.Now;
                    _materialList.Add(obj_author);
                }
                _Contract.AuthorContractmaterialdetails = _materialList;

                /*====================================================================================================================
                    * This section will assign AuthorContractSubsidiaryRights object insert parame
                 ======================================================================================================================*/
                IList<AuthorContractSubsidiaryRights> obj_SubsidiaryRights = new List<AuthorContractSubsidiaryRights>();

                foreach (var item in AuthorContractModel.AuthorSubsidiaryRights)
                {
                    var list = item.SusidiaryRights.ToList();
                    foreach (var lst in list)
                    {
                        AuthorContractSubsidiaryRights obj_Subsidiary = new AuthorContractSubsidiaryRights();
                        obj_Subsidiary.AuthorContractid = AuthorContractModel.Id;
                        obj_Subsidiary.AuthorId = lst.authorId;
                        obj_Subsidiary.subsidiaryrightsid = lst.subsidiaryid;
                        obj_Subsidiary.AuthorPercentage = lst.Percentage;
                        obj_Subsidiary.ouppercentage = lst.OupPercentage;
                        obj_Subsidiary.EnteredBy = AuthorContractModel.EnteredBy;
                        obj_Subsidiary.EntryDate = DateTime.Now;
                        obj_Subsidiary.Deactivate = "N";
                        obj_SubsidiaryRights.Add(obj_Subsidiary);

                        //if (obj_Subsidiary.AuthorPercentage != 0 && obj_Subsidiary.ouppercentage != 100)
                        //{
                        //    obj_SubsidiaryRights.Add(obj_Subsidiary);
                        //}
                        //else
                        //{
                        //    continue;
                        //}


                    }
                }
                _Contract.AuthorContractSubsidiaryRights = obj_SubsidiaryRights;
                /*====================================================================================================================
                   * This section will assign AuthorContractauthordetails and  AuthorContractRoyality object insert parame
                ======================================================================================================================*/

                IList<AuthorContractauthordetails> obj_AuthorContract = new List<AuthorContractauthordetails>();

                foreach (var item in AuthorContractModel.AuthorContactDetails)
                {
                    IList<AuthorContractRoyality> obj_Royality = new List<AuthorContractRoyality>();
                    AuthorContractauthordetails obj_authordetails = new AuthorContractauthordetails();
                    var list = item.RoyaltySlab.ToList();
                    foreach (var lst in list)
                    {
                        AuthorContractRoyality Royalty = new AuthorContractRoyality();
                        Royalty.AuthorContractid = AuthorContractModel.Id;
                        Royalty.subproducttypeid = lst.subproductTypeId;
                        Royalty.CopiesFrom = lst.CopiesFrom;
                        Royalty.CopiesTo = lst.CopiesTo;
                        Royalty.Percentage = lst.Percentage;
                        Royalty.EnteredBy = AuthorContractModel.EnteredBy;
                        Royalty.EntryDate = DateTime.Now;
                        Royalty.Deactivate = "N";
                        obj_Royality.Add(Royalty);
                    }
                    obj_authordetails.AuthorContractid = AuthorContractModel.Id;
                    obj_authordetails.AuthorContractRoyality = obj_Royality;
                    obj_authordetails.ContractTypeId = item.ContractTypeId;
                    obj_authordetails.Authortype = item.AuthorTypeId;
                    obj_authordetails.AuthorId = item.AuthorId;
                    obj_authordetails.paymentperiodid = item.PaymentperiodId;
                    obj_authordetails.AuthorCopies = item.AuthorCopies;
                    obj_authordetails.Seedmoney = item.SendMoney;
                    obj_authordetails.onetimepayment = item.OneTimePayment;
                    obj_authordetails.advanceroyality = item.AdvanceRoyalty;
                    obj_authordetails.EnteredBy = AuthorContractModel.EnteredBy;
                    obj_authordetails.EntryDate = DateTime.Now;
                    obj_authordetails.Deactivate = "N";
                    obj_AuthorContract.Add(obj_authordetails);
                }


                _Contract.AuthorContractauthordetails = obj_AuthorContract;

                /****************************************************************************************************************
                 * section for updating Menuscript dilivery format
                 *******************************************************************************************************************/

                _IAuthorContractService.DeativateMenuscriptDeliveryLink(_Contract.AuthorContractMenuscriptDeliveryLink.ToList());
                IList<AuthorContractMenuscriptDeliveryLink> MenuscriptDeliveryLink = new List<AuthorContractMenuscriptDeliveryLink>();
                if (AuthorContractModel.ManuScriptFormatList != null)
                {
                    foreach (var _obj in AuthorContractModel.ManuScriptFormatList)
                    {
                        AuthorContractMenuscriptDeliveryLink AuthorContractMenuscriptDeliveryLink = new AuthorContractMenuscriptDeliveryLink();
                        AuthorContractMenuscriptDeliveryLink.EnteredBy = _Contract.EnteredBy;
                        AuthorContractMenuscriptDeliveryLink.EntryDate = DateTime.Now;
                        AuthorContractMenuscriptDeliveryLink.ManuscriptId = _obj.MenuScriptId;
                        AuthorContractMenuscriptDeliveryLink.Deactivate = "N";
                        MenuscriptDeliveryLink.Add(AuthorContractMenuscriptDeliveryLink);
                    }
                    _Contract.AuthorContractMenuscriptDeliveryLink = MenuscriptDeliveryLink;
                }
                /*====================================================================================================================
                   * Final updattion of the code
                ======================================================================================================================*/
                _IAuthorContractService.UpdateAuthorContract(_Contract);
                //scope.Complete();
                /*====================================================================================================================
                * End section
             ======================================================================================================================*/
                status = "OK";
                return status + "," + "Update";
            }
            catch (Exception ex)
            {
                status = ex.ToString();
                return status;
            }
        }
        //}
        public IHttpActionResult InsertAddendumUpload(AddendumUpload Addendum)
        {
            string status = "";
            try
            {
                int Id = 0;
                AuthorContractAddendumDetails AddendumDetails;

                if (Addendum.Id != 0)
                {
                    //Update data
                    if (Addendum.SeriesCode == null)
                    {
                        Addendum.SeriesCode = null;
                        AddendumDetails = new AuthorContractAddendumDetails();
                        AddendumDetails = _IAuthorContractService.getAddendumDetails((int)Addendum.AuthorContrctId);
                    }
                    else
                    {
                        Addendum.AuthorContrctId = null;
                        AddendumDetails = new AuthorContractAddendumDetails();
                        AddendumDetails = _IAuthorContractService.getAddendumDetailsBySeries(Addendum.SeriesCode);
                    }

                    if (Addendum.AddendumType == "T")
                        Addendum.SameAsEntery = "Y";

                    if (AddendumDetails != null)
                    {
                        AddendumDetails.AuthorContractId = (int)Addendum.AuthorContrctId;
                        AddendumDetails.AddendumDate = Addendum.AddendumDate;
                        AddendumDetails.AddendumType = Addendum.AddendumType;
                        AddendumDetails.Periodofagreement = Addendum.Periodofagreement;
                        AddendumDetails.ExpiryDate = Addendum.ExpiryDate;
                        AddendumDetails.Remarks = Addendum.Remarks;
                        AddendumDetails.SameAsEntery = Addendum.SameAsEntery;
                        AddendumDetails.ModifiedBy = Addendum.EnteredBy;
                        AddendumDetails.ModifiedDate = DateTime.Now;

                        Id = _IAuthorContractService.UpdateAuthorContractAddendumDetails(AddendumDetails);
                    }
                }
                else
                {
                    //Insert new data
                    AddendumDetails = new AuthorContractAddendumDetails();
                    AddendumDetails.AddendumDate = Addendum.AddendumDate;
                    AddendumDetails.AddendumType = Addendum.AddendumType;
                    AddendumDetails.Periodofagreement = Addendum.Periodofagreement;
                    AddendumDetails.ExpiryDate = Addendum.ExpiryDate;
                    AddendumDetails.Remarks = Addendum.Remarks;
                    AddendumDetails.SameAsEntery = Addendum.SameAsEntery;
                    AddendumDetails.EnteredBy = Addendum.EnteredBy;
                    AddendumDetails.EntryDate = DateTime.Now;
                    AddendumDetails.Deactivate = "N";
                    AddendumDetails.AuthorContractId = Addendum.AuthorContrctId;
                    AddendumDetails.SeriesCode = Addendum.SeriesCode;

                    Id = _IAuthorContractService.InsertAuthorContractAddendumDetails(AddendumDetails);
                }

                if (Id != 0)
                {
                    if (Addendum.AddendumType == "R" && Addendum.SameAsEntery == "N")
                    {
                        if (Addendum.Id != 0)
                        {
                            //Update data
                            foreach (var data in Addendum.AuthorContractRoyality)
                            {
                                AuthorContractAddendumRoyality Royality = _IAuthorContractService.getAuthorContractAddendumRoyalityById(Convert.ToInt32(data.Id));

                                Royality.copiesfrom = data.copiesfrom;
                                Royality.copiesto = data.copiesto;
                                Royality.ProductSubTypeId = data.ProductSubTypeId;
                                Royality.percentage = data.percentage;

                                Royality.ModifiedBy = Addendum.EnteredBy;
                                Royality.ModifiedDate = DateTime.Now;

                                _IAuthorContractService.UpdateAuthorContractAddendumRoyality(Royality);
                            }
                        }
                        else
                        {
                            //Insert New data
                            foreach (var data in Addendum.AuthorContractRoyality)
                            {
                                AuthorContractAddendumRoyality Royality = new AuthorContractAddendumRoyality();

                                Royality.copiesfrom = data.copiesfrom;
                                Royality.copiesto = data.copiesto;
                                Royality.ProductSubTypeId = data.ProductSubTypeId;
                                Royality.percentage = data.percentage;
                                Royality.AddendumDetailsId = Id;
                                Royality.AuthorContractId = data.AuthorContractId;
                                Royality.AuthorId = data.AuthorId;

                                Royality.EnteredBy = Addendum.EnteredBy;
                                Royality.EntryDate = DateTime.Now;
                                Royality.Deactivate = "N";

                                _IAuthorContractService.InsertAuthorContractAddendumRoyality(Royality);
                            }
                        }

                    }

                    AddendumUpload _Addendum = new AddendumUpload();
                    string[] docurl = Addendum.UploadFile.Split(',');
                    int i = 0;
                    foreach (string doc in Addendum.DocumentName)
                    {
                        ACS.Core.Domain.AuthorContract.AddendumFileUpload Link = new ACS.Core.Domain.AuthorContract.AddendumFileUpload();
                        Link.AddendumDetailsId = Id;
                        Link.Documentname = doc;
                        Link.documentfile = docurl[i];
                        Link.EnteredBy = Addendum.EnteredBy;
                        _IAuthorContractService.InsertAddendumFileUpload(Link);
                        i++;
                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "InsertAddendumUpload", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "InsertAddendumUpload", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        //Added By Ankush Kumar
        public IHttpActionResult getAddendumDetails(int ContractId, int addendumId = 0)
        {
            try
            {
                AuthorContractAddendumDetails _addendumFile = new AuthorContractAddendumDetails();
                if (addendumId == 0)
                {
                     _addendumFile = _IAuthorContractService.getAddendumDetails(ContractId);
                }
                else
                {
                    _addendumFile = _IAuthorContractService.getAddendumDetailsView(ContractId, addendumId);
                }

                AuthorContractAddendumDetails author = new AuthorContractAddendumDetails();

                if (_addendumFile.AddendumDate.ToShortDateString() != null)
                    author.AddendumDate = Convert.ToDateTime(_addendumFile.AddendumDate.ToShortDateString());

                author.AddendumType = _addendumFile.AddendumType;
                author.Periodofagreement = _addendumFile.Periodofagreement;

                if (_addendumFile.ExpiryDate != null)
                    author.ExpiryDate = Convert.ToDateTime(_addendumFile.ExpiryDate.ToString());

                author.Remarks = _addendumFile.Remarks;
                return Json(author);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendumDetails", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendumDetails", ex);
                return Json(ex.InnerException);
            }
        }

        public IHttpActionResult getAddendum_BasicDetails(int ContractId, int addendumId = 0)
        {
            AuthorContractOriginal _Contract = new AuthorContractOriginal();
            string status = "";
            try
            {
                _Contract = _IAuthorContractService.GetAuthorContractById(ContractId);
                
                var _AuthorList = _Contract.AuthorContractauthordetails.Where(i => i.Deactivate == "N").ToList().Select(i => new
                {
                    Type = i.AuthorTypeMaster.AuthorTypeName,
                    TypeId = i.Authortype,
                    Id = i.AuthorMaster.Id,
                    Name = i.AuthorMaster.FirstName + " " + i.AuthorMaster.LastName,
                    ContractId = i.ContractTypeId,
                    ContractName = i.ContractMaster.ContractName
                }).ToList();

                IList<RoyaltySlab> _royalty = new List<RoyaltySlab>();
                AuthorContractAddendumDetails _addendumDetails = new AuthorContractAddendumDetails();

                if (addendumId == 0)
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == ContractId && a.Deactivate == "N").OrderByDescending(a => a.EntryDate).FirstOrDefault();
                }
                else
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == ContractId && a.Deactivate == "N" && a.Id == addendumId).FirstOrDefault();
                }

                if (_addendumDetails != null)
                {
                    var list = (from acar in _AuthorContractAddendumRoyality.Table.Where(a => a.Deactivate == "N" && a.AuthorContractId == ContractId && a.AddendumDetailsId == _addendumDetails.Id)
                                join ptm in _ProductTypeMaster.Table.Where(a => a.Deactivate == "N")
                                on acar.ProductSubTypeId equals ptm.Id
                                select new
                                {
                                    Id = acar.Id,
                                    copiesfrom = acar.copiesfrom,
                                    copiesto = acar.copiesto,
                                    percentage = acar.percentage,
                                    AuthorId = acar.AuthorId,
                                    subproductTypeId = acar.ProductSubTypeId,

                                    SubProductType = ptm.typeName,
                                }).ToList();

                    foreach (var item in list)
                    {
                        RoyaltySlab Royalty = new RoyaltySlab();
                        Royalty.Id = item.Id;
                        Royalty.CopiesFrom = item.copiesfrom;
                        Royalty.CopiesTo = item.copiesto;
                        Royalty.Percentage = Convert.ToDouble(item.percentage);
                        Royalty.AuthorId = item.AuthorId;
                        Royalty.SubProductType = item.SubProductType;
                        Royalty.subproductTypeId = item.subproductTypeId;
                        _royalty.Add(Royalty);
                    }
                }
                
                return Json(new
                {
                    _royalty,
                    _AuthorList,
                });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendum_BasicDetails", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendum_BasicDetails", ex);
                return Json(ex.InnerException);
            }
        }

        public IHttpActionResult getAlltheAddendumDocument(int ContractId, int addendumId = 0)
        {
            try
            {
                AuthorContractAddendumDetails _addendumDetails = new AuthorContractAddendumDetails();

                if (addendumId == 0)
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == ContractId && a.Deactivate == "N").OrderByDescending(a => a.EntryDate).FirstOrDefault();
                }
                else
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == ContractId && a.Deactivate == "N" && a.Id == addendumId).FirstOrDefault();
                }

                var _addendumFileList = _IAuthorContractService.getAddendumFileUpload(ContractId).Where(a => a.AddendumDetailsId == _addendumDetails.Id).ToList();
                var _addendumFile = _addendumFileList.Select(i => new
                {
                    Id = i.Id,
                    name = i.Documentname,
                    url = i.documentfile

                }).ToList();

                return Json(new { _addendumFile });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAlltheAddendumDocument", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAlltheAddendumDocument", ex);
                return Json(ex.InnerException);
            }
        }

        public IHttpActionResult RemoveAddendumFileUpload(ACS.Core.Domain.AuthorContract.AddendumFileUpload Docoument)
        {
            string status = string.Empty;
            try
            {
                _IAuthorContractService.DeavtivateauthorAddendumFileUploadById(Docoument.Id);
                status = "Deleted";

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "RemoveAddendumFileUpload", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "RemoveAddendumFileUpload", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult getAddendumDetailsBySeries(string SeriesCode, int addendumId = 0)
        {
            try
            {
                //var _addendumFile = _IAuthorContractService.getAddendumDetailsBySeries(SeriesCode);
                AuthorContractAddendumDetails _addendumFile = new AuthorContractAddendumDetails();
                if (addendumId == 0)
                {
                    _addendumFile = _IAuthorContractService.getAddendumDetailsBySeries(SeriesCode);
                }
                else
                {
                    _addendumFile = _IAuthorContractService.getAddendumDetailsBySeriesView(SeriesCode, addendumId);
                }

                AuthorContractAddendumDetails author = new AuthorContractAddendumDetails();

                if (_addendumFile.AddendumDate != null)
                    author.AddendumDate = Convert.ToDateTime(_addendumFile.AddendumDate.ToShortDateString());
                author.AddendumType = _addendumFile.AddendumType;
                author.Periodofagreement = _addendumFile.Periodofagreement;
                if (_addendumFile.ExpiryDate != null)
                    author.ExpiryDate = Convert.ToDateTime(_addendumFile.ExpiryDate.ToString());
                author.Remarks = _addendumFile.Remarks;
                author.SeriesCode = _addendumFile.SeriesCode;
                return Json(author);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendumDetailsBySeries", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAddendumDetailsBySeries", ex);
                return Json(ex.InnerException);
            }
        }

        public IHttpActionResult getAlltheAddendumDocumentBySeries(string SeriesCode, int addendumId = 0)
        {
            try
            {
                AuthorContractAddendumDetails _addendumDetails = new AuthorContractAddendumDetails();

                if (addendumId == 0)
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.SeriesCode == SeriesCode && a.Deactivate == "N").OrderByDescending(a => a.EntryDate).FirstOrDefault();
                }
                else
                {
                    _addendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.SeriesCode == SeriesCode && a.Deactivate == "N" && a.Id == addendumId).FirstOrDefault();
                }

                var _addendumFileList = _IAuthorContractService.getAddendumFileUploadBySeries(SeriesCode).Where(a => a.AddendumDetailsId == _addendumDetails.Id).ToList();
                var _addendumFile = _addendumFileList.Select(i => new
                {
                    Id = i.Id,
                    name = i.Documentname,
                    url = i.documentfile

                }).ToList();

                return Json(new { _addendumFile });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAlltheAddendumDocumentBySeries", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getAlltheAddendumDocumentBySeries", ex);
                return Json(ex.InnerException);
            }
        }

        public IHttpActionResult TopSearch(String Code)
        {
            try
            {
                AuthorContractOriginal AuthorContract = _AuthorContract.Table.Where(a => a.AuthorContractCode == Code && a.Deactivate == "N").FirstOrDefault();

                if (AuthorContract != null)
                {
                    var _AuthorContractValue = new
                    {
                        Id = AuthorContract.Id
                    };

                    return Json(new { _AuthorContractValue });
                }
                else
                {
                    string _AuthorContractValue = string.Empty;
                    return Json(new { _AuthorContractValue });
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "TopSearch", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "TopSearch", ex);
                return Json(ex.InnerException);
            }
          
        }

        public IHttpActionResult GetViewAmendmentDocumentList(int Id)
        {


            if (Id != null)
            {
                IList<AuthorAmendmentDocument> _AuthorAmendmentDocumentDocuments = _IAuthorContractService.getAuthorAmendmentDocumentDocumentsById(Id);

                AuthorContractModel AuthorAmendmentDocumentDetails = new AuthorContractModel();

                AuthorAmendmentDocumentDetails.DocumentIds = _AuthorAmendmentDocumentDocuments.Select(i => i.Id).ToArray();

                AuthorAmendmentDocumentDetails.DocumentName = _AuthorAmendmentDocumentDocuments.Select(i => i.Documentname).ToArray();

                foreach (var docs in _AuthorAmendmentDocumentDocuments)
                    AuthorAmendmentDocumentDetails.UploadFile = AuthorAmendmentDocumentDetails.UploadFile + docs.documentfile + ",";

                return Json(new { AuthorAmendmentDocumentDetails });
            }
            else
            {
                return null;
            }
            
        }

        public IHttpActionResult RemoveAmendmentDocument(AuthorAmendmentDocument Dcoument)
        {


            var documet = _IAuthorContractService.getAuthorAmendmentDocumentsById(Dcoument.Id);
            string status = string.Empty;
            try
            {

                _IAuthorContractService.DeavtivateAuthorAmendmentDocumentsDocumentById(Dcoument.Id, Dcoument.EnteredBy);


                status = _localizationService.GetResource("Master.API.Success.Message");

            }
            catch 
            {
                return null;
            }
           

            return Json(status);
        }

        public IHttpActionResult RemoveAuthorContractDocument(AuthorAmendmentDocument Dcoument)
        {


            var documet = _IAuthorContractService.AuthorContractDocumentsById(Dcoument.Id);
            string status = string.Empty;
            try
            {

                _IAuthorContractService.DeavtivateAuthorContractDocumentsDocumentById(Dcoument.Id, Dcoument.EnteredBy);


                status = _localizationService.GetResource("Master.API.Success.Message");

            }
            catch
            {
                return null;
            }


            return Json(status);
        }

        public IHttpActionResult getListAuthorContractStatusMail(int Id , int EnterBy)
        {
            try
            {

                SendMailAuthorContractStatus(Id, EnterBy);
                return null;
            }
            catch
            {
                return null;

            }

        }

        public IList<SLV.Model.AuthorContract.AuthorContractSearchmodel> AuthorContractStatus(int AuthorContractId)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("AuthorContractId", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + AuthorContractId + "'";
            var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_getAuthorContractDetailsForMail_get", parameters).ToList();

            return _GetAuthorReport1;
        }

        public void SendMailAuthorContractStatus(int Id, int EnterBy)
        {
            try
            {
                string mstr_body = string.Empty;
                string EmailTO = string.Empty;

                List<SLV.Model.AuthorContract.AuthorContractSearchmodel> _mobjReportList = new List<SLV.Model.AuthorContract.AuthorContractSearchmodel>();


                _mobjReportList = AuthorContractStatus(Id).ToList();

          

                if (_mobjReportList.Count > 0)
                {


                    EmailTO = _ExecutiveMaster.Table.Where(a => a.Deactivate == "N" && a.Id == EnterBy).FirstOrDefault().Emailid.ToString().Trim();

                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        //if (_mobjReportList.Count > 0)
                        //{
                        //    mstr_searchparameter.Append("<table width='100%'>");
                        //    mstr_searchparameter.Append("<tr>");
                        //    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                        //    mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                        //    mstr_searchparameter.Append("<tr>");
                        //    mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Assignment Contract Status</b> " + "</td>");
                        //    mstr_searchparameter.Append("</tr>");



                        //    mstr_searchparameter.Append("<tr>");
                        //    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                        //    mstr_searchparameter.Append("</tr>");

                        //    mstr_searchparameter.Append("</table>");
                        //    mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                        //    mstr_searchparameter.Append("<tr>");
                        //    mstr_searchparameter.Append("<td colspan='2'>");
                        //    mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                        //    mstr_searchparameter.Append("<tr>");
                        //    mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Code</b></td>");


                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");

                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Sub-Product</b></td>");

                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");

                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");


                        //    mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Status</b></td>");



                        //    mstr_searchparameter.Append("</tr>");
                        //    mstr_searchparameter.Append("</td>");
                        //    int mint_Counter = 1;
                        //    foreach (SLV.Model.AuthorContract.AuthorContractSearchmodel data in _mobjReportList)
                        //    {
                        //        mstr_searchparameter.Append("<tr>");
                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                        //        mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.AuthorContractCode + "</td>");
                        //        //  mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingTitle + "</td>");

                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingSubProduct + "</td>");

                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");
                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.oupisbn + "</td>");

                        //        mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Contractstatus + "</td>");

                        //        mstr_searchparameter.Append("</tr>");
                        //        mint_Counter++;
                        //    }
                        //    mstr_searchparameter.Append("</table></td></tr></table>");

                        //}



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/AuthorContractStatus.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                    //    mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        mstr_body = mstr_body.Replace("#Date", now.ToString("D"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("AuthorContractStatus"));



                        mstr_body = mstr_body.Replace("#RequstPersonName", _mobjReportList.FirstOrDefault().RequestBy.ToString());
                        mstr_body = mstr_body.Replace("#UpdateDate", now.toDDMMYYYY());


                        mstr_body = mstr_body.Replace("#Code", _mobjReportList.FirstOrDefault().AuthorContractCode.ToString());

                        mstr_body = mstr_body.Replace("#Contractstatus", _mobjReportList.FirstOrDefault().Contractstatus.ToString());

                        mstr_body = mstr_body.Replace("#ProductCode", _mobjReportList.FirstOrDefault().ProductCode.ToString());

                        mstr_body = mstr_body.Replace("#Title", _mobjReportList.FirstOrDefault().WorkingTitle.ToString());

                        mstr_body = mstr_body.Replace("#SubTitle", _mobjReportList.FirstOrDefault().WorkingSubProduct.ToString());

                        mstr_body = mstr_body.Replace("#ISBN", _mobjReportList.FirstOrDefault().oupisbn.ToString());

                        mstr_body = mstr_body.Replace("#AuthorName", _mobjReportList.FirstOrDefault().AuthorName.ToString());


                        mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = EmailTO;
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("AuthorContractStatus");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("AuthorContractStatus");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("AuthorContractStatus");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (Exception ex)
            {



            }
        }

        //added by Prakash on 17 Aug, 2017
        [HttpGet]
        public IHttpActionResult InsertRestProductSeriesDetails(string ProductId, string SeriesCode)
        {
            AuthorContractOriginal _AuthorContractModel = _AuthorContract.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode).ToList().FirstOrDefault();

            string status = ""; string Code = "";
            int Id = 0;

            if (ProductId != "")
            {
                var productid_List = ProductId.Split(',');
                foreach (var productid in productid_List)
                {
                    //var _checkDuplicate = _AuthorContract.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode && a.ProductId == Convert.ToInt32(productid)).ToList().FirstOrDefault();
                    //if(_checkDuplicate != null)
                    //{
                    //    status = "Already Exist: " + productid; 
                    //    return Json(status);
                    //}

                    AuthorContractOriginal _Contract = new AuthorContractOriginal();
                    using (var scope = new System.Transactions.TransactionScope())
                    {
                        try
                        {

                            /*====================================================================================================================
                               * This section will assign Author contract object insert parame
                             ======================================================================================================================*/
                            _Contract.AuthorContractCode = GenerateSeriesCode("AuthorContract", "AC");
                            _Contract.AuthorContractCode = _Contract.AuthorContractCode.ToString().ToUpper();
                            _Contract.ExecutiveCode = _AuthorContractModel.ExecutiveCode;
                            _Contract.ProductId = Convert.ToInt32(productid);
                            //_Contract.ContractTypeId = AuthorContractModel.ContractTypeId;
                            _Contract.SeriesId = _AuthorContractModel.SeriesId;
                            _Contract.SeriesCode = _AuthorContractModel.SeriesCode;
                            _Contract.Status = _AuthorContractModel.Status;
                            _Contract.NoOfAuthors = _AuthorContractModel.NoOfAuthors;
                            _Contract.ContractEntryDate = _AuthorContractModel.ContractEntryDate;
                            _Contract.ContractDate = _AuthorContractModel.ContractDate;
                            _Contract.TermsOfCopyright = _AuthorContractModel.TermsOfCopyright;
                            _Contract.ContractExpiryDate = _AuthorContractModel.ContractExpiryDate;
                            _Contract.contractperiodinmonth = _AuthorContractModel.contractperiodinmonth;
                            _Contract.BuyBack = _AuthorContractModel.BuyBack;
                            _Contract.NatureOfWork = _AuthorContractModel.NatureOfWork;
                            _Contract.CopyrightOwner = _AuthorContractModel.CopyrightOwner;
                            _Contract.Territoryrightsid = _AuthorContractModel.Territoryrightsid;
                            //_Contract.thirdpartypermission = _AuthorContractModel.thirdpartypermission;
                            _Contract.Amendment = _AuthorContractModel.Amendment;
                            _Contract.AmendmentRemarks = _AuthorContractModel.AmendmentRemarks;
                            _Contract.Restriction = _AuthorContractModel.Restriction;
                            _Contract.subjectMatterAndTreatment = _AuthorContractModel.subjectMatterAndTreatment;
                            _Contract.MinNoOfwords = _AuthorContractModel.MinNoOfwords;
                            _Contract.MaxNoOfwords = _AuthorContractModel.MaxNoOfwords;
                            _Contract.MinNoOfPages = _AuthorContractModel.MinNoOfPages;
                            _Contract.MaxNoOfPages = _AuthorContractModel.MaxNoOfPages;
                            _Contract.PriceType = _AuthorContractModel.PriceType;
                            _Contract.Price = _AuthorContractModel.Price;
                            _Contract.CurrencyId = _AuthorContractModel.CurrencyId;
                            _Contract.MediumOfdelivery = _AuthorContractModel.MediumOfdelivery;
                            _Contract.ManuscriptId = _AuthorContractModel.ManuscriptId;
                            _Contract.Deliveryschedule = _AuthorContractModel.Deliveryschedule;
                            _Contract.EnteredBy = _AuthorContractModel.EnteredBy;
                            //_Contract.NoofAuthors = AuthorContractModel.NoofAuthors;
                            _Contract.ProductRemarks = _AuthorContractModel.ProductRemarks;
                            _Contract.LicenseId = _AuthorContractModel.LicenseId;

                            /*====================================================================================================================
                              * This section will assign AuthorContractContributor object insert parame
                            ======================================================================================================================*/
                            //IList<AuthorContractContributor> obj_abc = new List<AuthorContractContributor>();
                            //foreach (var item in AuthorContractModel.ContributorName)
                            //{
                            //    AuthorContractContributor obj_contributor = new AuthorContractContributor();
                            //    obj_contributor.ContributorName = item.Contributor;
                            //    obj_contributor.EnteredBy = AuthorContractModel.EnteredBy;
                            //    obj_contributor.Deactivate = "N";
                            //    obj_contributor.EntryDate = DateTime.Now;
                            //    obj_abc.Add(obj_contributor);
                            //}

                            //_Contract.AuthorContactContibutor = obj_abc;

                            /*====================================================================================================================
                            * This section will assign AuthorContractmaterialdetails object insert parame
                          ======================================================================================================================*/

                            //IList<AuthorContractmaterialdetails> obj_authorMaterial = new List<AuthorContractmaterialdetails>();


                            //foreach (var item in AuthorContractModel.SupplyMaterialbyAuthor)
                            //{
                            //    AuthorContractmaterialdetails obj_author = new AuthorContractmaterialdetails();
                            //    obj_author.MaterialId = item.MaterialId;
                            //    obj_author.materialdate = item.materialDate;
                            //    obj_author.EnteredBy = AuthorContractModel.EnteredBy;
                            //    obj_author.Deactivate = "N";
                            //    obj_author.EntryDate = DateTime.Now;
                            //    obj_authorMaterial.Add(obj_author);
                            //}
                            //_Contract.AuthorContractmaterialdetails = obj_authorMaterial;

                            /*====================================================================================================================
                                * This section will assign AuthorContractSubsidiaryRights object insert parame
                             ======================================================================================================================*/
                            //IList<AuthorContractSubsidiaryRights> obj_SubsidiaryRights = new List<AuthorContractSubsidiaryRights>();

                            //foreach (var item in AuthorContractModel.AuthorSubsidiaryRights)
                            //{
                            //    var list = item.SusidiaryRights.ToList();
                            //    foreach (var lst in list)
                            //    {
                            //        AuthorContractSubsidiaryRights obj_Subsidiary = new AuthorContractSubsidiaryRights();
                            //        obj_Subsidiary.AuthorId = lst.authorId;
                            //        obj_Subsidiary.subsidiaryrightsid = lst.subsidiaryid;
                            //        obj_Subsidiary.AuthorPercentage = lst.Percentage;
                            //        obj_Subsidiary.ouppercentage = lst.OupPercentage;
                            //        obj_Subsidiary.EnteredBy = AuthorContractModel.EnteredBy;
                            //        obj_Subsidiary.EntryDate = DateTime.Now;
                            //        obj_Subsidiary.Deactivate = "N";
                            //        obj_SubsidiaryRights.Add(obj_Subsidiary);

                            //    }
                            //}
                            //_Contract.AuthorContractSubsidiaryRights = obj_SubsidiaryRights;
                            /*====================================================================================================================
                               * This section will assign AuthorContractauthordetails and  AuthorContractRoyality object insert parame
                            ======================================================================================================================*/
                            //IList<AuthorContractauthordetails> obj_AuthorContract = new List<AuthorContractauthordetails>();


                            //foreach (var item in AuthorContractModel.AuthorContactDetails)
                            //{
                            //    IList<AuthorContractRoyality> obj_Royality = new List<AuthorContractRoyality>();
                            //    AuthorContractauthordetails obj_authordetails = new AuthorContractauthordetails();
                            //    var list = item.RoyaltySlab.ToList();
                            //    foreach (var lst in list)
                            //    {
                            //        AuthorContractRoyality Royalty = new AuthorContractRoyality();
                            //        Royalty.subproducttypeid = lst.subproductTypeId;
                            //        Royalty.CopiesFrom = lst.CopiesFrom;
                            //        Royalty.CopiesTo = lst.CopiesTo;
                            //        Royalty.Percentage = lst.Percentage;
                            //        Royalty.EnteredBy = AuthorContractModel.EnteredBy;
                            //        Royalty.EntryDate = DateTime.Now;
                            //        Royalty.Deactivate = "N";
                            //        obj_Royality.Add(Royalty);
                            //    }
                            //    obj_authordetails.AuthorContractRoyality = obj_Royality;
                            //    obj_authordetails.Authortype = item.AuthorTypeId;
                            //    obj_authordetails.ContractTypeId = item.ContractTypeId;
                            //    obj_authordetails.AuthorId = item.AuthorId;
                            //    obj_authordetails.paymentperiodid = item.PaymentperiodId != 0 ? item.PaymentperiodId : (int?)null;
                            //    obj_authordetails.AuthorCopies = (int?)item.AuthorCopies != 0 ? item.AuthorCopies : (int?)null;
                            //    obj_authordetails.Seedmoney = (int?)item.SendMoney != 0 ? item.SendMoney : (double?)null;
                            //    obj_authordetails.onetimepayment = (int?)item.OneTimePayment != 0 ? item.OneTimePayment : (double?)null;
                            //    obj_authordetails.advanceroyality = (int?)item.AdvanceRoyalty != 0 ? item.AdvanceRoyalty : (double?)null;
                            //    obj_authordetails.EnteredBy = AuthorContractModel.EnteredBy;
                            //    obj_authordetails.EntryDate = DateTime.Now;
                            //    obj_authordetails.Deactivate = "N";
                            //    obj_AuthorContract.Add(obj_authordetails);
                            //}
                            //_Contract.AuthorContractauthordetails = obj_AuthorContract;

                            /****************************************************************************************************************
                           * section for updating Menuscript dilivery format
                           *******************************************************************************************************************/

                            //IList<AuthorContractMenuscriptDeliveryLink> MenuscriptDeliveryLink = new List<AuthorContractMenuscriptDeliveryLink>();
                            //if (AuthorContractModel.ManuScriptFormatList != null)
                            //{
                            //    foreach (var _obj in AuthorContractModel.ManuScriptFormatList)
                            //    {
                            //        AuthorContractMenuscriptDeliveryLink AuthorContractMenuscriptDeliveryLink = new AuthorContractMenuscriptDeliveryLink();
                            //        AuthorContractMenuscriptDeliveryLink.EnteredBy = _Contract.EnteredBy;
                            //        AuthorContractMenuscriptDeliveryLink.EntryDate = DateTime.Now;
                            //        AuthorContractMenuscriptDeliveryLink.ManuscriptId = _obj.MenuScriptId;
                            //        AuthorContractMenuscriptDeliveryLink.Deactivate = "N";
                            //        MenuscriptDeliveryLink.Add(AuthorContractMenuscriptDeliveryLink);
                            //    }
                            //    _Contract.AuthorContractMenuscriptDeliveryLink = MenuscriptDeliveryLink;
                            //}
                            /*====================================================================================================================
                               * Final Insertion of the code
                            ======================================================================================================================*/
                            Id = _IAuthorContractService.InsertAuthorContract(_Contract);
                            /*====================================================================================================================
                            * End section
                         ======================================================================================================================*/

                            //IList<ProductPreviousProductLink> ProductPreviousProductLinkList = _IAuthorContractService.ProductPreviousProductLinkList(productid.GetValueOrDefault() == 0 ? AuthorContractModel.ProductId : productid.GetValueOrDefault());
                            //if (ProductPreviousProductLinkList != null)
                            //{
                            //    foreach (ProductPreviousProductLink PreviousProductLink in ProductPreviousProductLinkList)
                            //    {
                            //        ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                            //        _ProductPreviousProductLink.ProductId = PreviousProductLink.ProductId;
                            //        _ProductPreviousProductLink.PreviousProductId = PreviousProductLink.PreviousProductId;
                            //        _ProductPreviousProductLink.AuthorContractId = Id;
                            //        _ProductPreviousProductLink.Deactivate = "N";
                            //        _ProductPreviousProductLink.EnteredBy = AuthorContractModel.EnteredBy;
                            //        _ProductPreviousProductLink.EntryDate = DateTime.Now;
                            //        _ProductPreviousProductLink.ModifiedBy = null;
                            //        _ProductPreviousProductLink.ModifiedDate = null;
                            //        _ProductPreviousProductLink.DeactivateBy = null;
                            //        _ProductPreviousProductLink.DeactivateDate = null;
                            //        _SeriesProductEntryService.InsertProductPreviousProductLink(_ProductPreviousProductLink);
                            //    }
                            //}

                            status = "OK";
                            Code = _Contract.AuthorContractCode;
                            scope.Complete();
                            //return status + "," + Code + "," + Id;
                        }
                        catch (Exception ex)
                        {
                            status = ex.ToString();
                            //return status;
                        }
                    }
                }
            }

            //status = "OK";
            return Json(status);
        }

        /* Create By  : Prakash
       * Create on  : 20 Sep, 2017
       * Create for : Delete Contract
       */
        [HttpPost]
        public IHttpActionResult DeleteContractSet(AuthorContractModel mobj_AuthorContract)
        {
            SqlParameter[] parameters = new SqlParameter[4];
            string status = string.Empty;

            try
            {
                if (mobj_AuthorContract.AuthorContractId != 0 || mobj_AuthorContract.SeriesCode != "")
                {
                    parameters[0] = new SqlParameter("ContractId", SqlDbType.VarChar, 100);
                    parameters[0].Value = "'" + mobj_AuthorContract.AuthorContractId + "'";

                    parameters[1] = new SqlParameter("Role", SqlDbType.VarChar, 100);
                    parameters[1].Value = "'" + mobj_AuthorContract.Role + "'";

                    parameters[2] = new SqlParameter("DeactivateBy", SqlDbType.VarChar, 100);
                    parameters[2].Value = "'" + mobj_AuthorContract.DeactivateBy + "'";

                    parameters[3] = new SqlParameter("SeriesCode", SqlDbType.VarChar, 100);
                    parameters[3].Value = "'" + mobj_AuthorContract.SeriesCode + "'";

                    var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<AuthorContractModel>("Proc_Contractdelete_set", parameters).ToList();

                    if (_GetResult[0].flag == 1)
                        status = "OK";
                }

                return Json(status);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "DeleteContractSet", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "DeleteContractSet", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);

        }

        /* Create By  : Prakash
       * Create on  : 27 Sep, 2017
       * Create for : Get contract Addendum List
       */
        public IHttpActionResult getContractAddendumList(int ContractId = 0, string SeriesCode = "")
        {
            string status = "";

            try
            {
                if (SeriesCode != "" && SeriesCode != null)
                {
                    var list = (from acad in _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode)
                                select new
                                {
                                    Id = acad.Id,
                                    AuthorContractId = acad.AuthorContractId,
                                    SeriesCode = acad.SeriesCode,
                                    AddendumCode = acad.AddendumCode,
                                    AddendumDate = acad.AddendumDate,
                                    ExpiryDate = acad.ExpiryDate,
                                    AddendumType = acad.AddendumType == "T" ? "Term Addendum" : (acad.AddendumType == "R" ? "Royalty Change Addendum" : "Other Changes"),
                                    //AddenDumDocumnetName = _AddendumFileUpload.Table.Where(a => a.Deactivate == "N" && a.AddendumDetailsId == acad.Id).ToList(),
                                    //AddenDumDocumnetFile = _AddendumFileUpload.Table.Where(a => a.Deactivate == "N" && a.AddendumDetailsId == acad.Id).ToList(),

                                }).ToList();

                    return Json(list);

                }
                else
                {
                    var list = (from acad in _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.AuthorContractId == ContractId)
                                select new
                                {
                                    Id = acad.Id,
                                    AuthorContractId = acad.AuthorContractId,
                                    SeriesCode = acad.SeriesCode,
                                    AddendumCode = acad.AddendumCode,
                                    AddendumDate = acad.AddendumDate,
                                    ExpiryDate = acad.ExpiryDate,
                                    AddendumType = acad.AddendumType == "T" ? "Term Addendum" : (acad.AddendumType == "R" ? "Royalty Change Addendum" : "Other Changes"),
                                    //AddenDumDocumnetName = _AddendumFileUpload.Table.Where(a => a.Deactivate == "N" && a.AddendumDetailsId == acad.Id).ToList(),
                                    //AddenDumDocumnetFile = _AddendumFileUpload.Table.Where(a => a.Deactivate == "N" && a.AddendumDetailsId == acad.Id).ToList(),

                                }).ToList();

                    return Json(list);
                }                
               
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getContractAddendumList", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorContactController.cs", "getContractAddendumList", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }



    }

}
