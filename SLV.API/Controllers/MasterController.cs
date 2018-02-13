using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using System.Web.Http.Description;
using System.Transactions;
using ACS.Services.Security;
using Logger;
using ACS.Data;

namespace SLV.API.Controllers
{
    public class MasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IDepartmentService _DepartmentService;
        private readonly IDivisionService _DivisionService;
        private readonly ILocalizationService _localizationService;
        private readonly IExecutive _ExecutiveService;
        private readonly IWorkContext _workContext;
        private readonly IProductType _ProductTypeService;
        private readonly IEncryptionService _encryptionService;
        private readonly ICommonListService _commonList;
        private readonly IDbContext _dbContext;
        //Added by Saddam on 17/05/2016
        private readonly IAuthorService _AuthorTypeService;
        //ended by saddam
        private readonly IPublishingCompanyService _publishingCompanyService;
        //Added by Suranjana on 11/07/2016
        private readonly ITypeOfRightsService _typeOfRightsService;
        //ended by Suranjana
                
        public MasterController(IDepartmentService DepartmentService, IDivisionService DivisionService,
            ILocalizationService localizationService, IExecutive ExecutiveService, IWorkContext workContext,
            IProductType ProductTypeService
            //added by Saddam on 17/05/2016
             , IAuthorService AuthorService,
            //Added by sanjeet
            IPublishingCompanyService publishingCompanyService,
            IEncryptionService encryptionService,
            ICommonListService commonList,
            IDbContext dbContext,
            //Added By Ankush Kumar on 11/07/2016
            ISubsidiaryRightsService subsidiaryRightsService,
            //Added by Suranjana on 11/07/2016
            ITypeOfRightsService typeOfRightsService
            )
        {
            _DepartmentService = DepartmentService;
            _DivisionService = DivisionService;
            _localizationService = localizationService;
            _ExecutiveService = ExecutiveService;
            _workContext = workContext;
            _ProductTypeService = ProductTypeService;
            //Added by saddam on 17/05/2016
            _AuthorTypeService = AuthorService;
            //ended by saddam
            _publishingCompanyService = publishingCompanyService;
            _encryptionService = encryptionService;
            _commonList = commonList;
            this._dbContext = dbContext;
            //Added by Suranjana on 11/07/2016
            _typeOfRightsService = typeOfRightsService;
        }


        public IHttpActionResult Department(DepartmentMaster Department)
        {
            DepartmentMaster _Department = _DepartmentService.GetDepartmentById(Department);
            return Json(_Department);
        }

        public IHttpActionResult InsertDepartment(DepartmentMaster Department)
        {
            string status = "";
            try
            {
                status = _DepartmentService.DuplicityCheck(Department);
                if (status == "Y")
                {
                    if (Department.Id == 0)
                    {
                        _DepartmentService.InsertDepartment(Department);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        DepartmentMaster _Department = _DepartmentService.GetDepartmentById(Department);
                        _Department.DepartmentName = Department.DepartmentName;
                        _Department.ModifiedBy = Department.EnteredBy;
                        _Department.ModifiedDate = DateTime.Now;
                        _DepartmentService.UpdateDepartment(_Department);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult UpdateDepartment(DepartmentMaster Department)
        {
            string status = string.Empty;
            try
            {

                //if (!_geographyService.GetCityListByStateId(stateId).Where(i => i.Name == city.Name && i.Code == city.Code).Any())
                //{
                DepartmentMaster _Department = _DepartmentService.GetDepartmentById(Department);
                _Department.Deactivate = Department.DepartmentName;
                _Department.ModifiedBy = Department.EnteredBy;
                _Department.ModifiedDate = DateTime.Now;
                _DepartmentService.UpdateDepartment(_Department);

                //status = _localizationService.GetResource("Common.API.Success.Message");
                //}
                //else
                //    status = _localizationService.GetResource("City.Duplicate.CodeAndName.Message");


            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult DepartmentDelete(DepartmentMaster Department)
        {

            string status = string.Empty;
            try
            {
                DepartmentMaster _Department = _DepartmentService.GetDepartmentById(Department);
                _Department.Deactivate = "Y";
                _Department.DeactivateBy = Department.EnteredBy;
                _Department.DeactivateDate = DateTime.Now;
                _DepartmentService.UpdateDepartment(_Department);

                status = "OK";
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        //Added by Dheeraj/
        public IHttpActionResult InsertDivision(DivisionMaster Division)
        {

            string status = "";
            try
            {
                if (Division.parentdivisionid == null)
                {
                    Division.divisionlevel = 1;
                }
                else
                {
                    Division.divisionlevel = 2;
                }
                status = _DivisionService.DuplicityCheck(Division);
                if (status == "Y")
                {
                    if (Division.Id == 0)
                    {
                        _DivisionService.InsertDivision(Division);

                    }
                    else
                    {
                        DivisionMaster mobj_division = _DivisionService.GetDivisionById(Division);
                        mobj_division.divisionName = Division.divisionName;
                        mobj_division.divisionlevel = Division.divisionlevel;
                        mobj_division.parentdivisionid = Division.parentdivisionid;
                        mobj_division.ModifiedBy = Division.EnteredBy;
                        mobj_division.ModifiedDate = System.DateTime.Now;
                        _DivisionService.UpdateDivision(mobj_division);

                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");

                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }
        
        //Update the Sub division
        public IHttpActionResult DivisionDelete(DivisionMaster _division)
        {

            string status = string.Empty;
            try
            {

                DivisionMaster _Division = _DivisionService.GetDivisionById(_division);
                _Division.Deactivate = "Y";
                _Division.ModifiedBy = _division.EnteredBy;
                _Division.ModifiedDate = DateTime.Now;
                _DivisionService.UpdateDivision(_Division);
                status = "OK";


            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult SubDivision(DivisionMaster _division)
        {
            DivisionMaster _Division = _DivisionService.GetDivisionById(_division);
            return Json(_Division);
        }
        
        //Added by Saddam/
        public IHttpActionResult insertExecutive(ExecutiveModel Executive)
        {

            string status = "";
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ExecutiveMaster _Executive = new ExecutiveMaster();
                    _Executive.executiveName = Executive.executiveName;
                    _Executive.executivecode = Executive.executivecode;
                    _Executive.Emailid = Executive.Emailid;
                    _Executive.DepartmentId = Executive.DepartmentId;
                    _Executive.Mobile = Executive.Mobile;
                    _Executive.Phoneno = Executive.Phoneno;
                    _Executive.DepartmentId = Executive.DepartmentId;
                    _Executive.Password = _encryptionService.EncryptText(Executive.Password, _ExecutiveService.KeyValue("encriptionkey"));
                    _Executive.Id = Executive.Id;
                    _Executive.EnteredBy = Executive.EnteredBy;
                    //  status = _ExecutiveService.DuplicityCheck(_Executive);
                    status = _ExecutiveService.DuplicityExecutiveCodeCheck(_Executive);

                    if (status == "Y")
                    {
                        if (Executive.Id == 0)
                        {
                            int ExecutiveIdId = _ExecutiveService.InsertExecutive(_Executive);
                            if (ExecutiveIdId != 0)
                            {
                                ExecutiveDivisionLink Link = new ExecutiveDivisionLink();
                                foreach (var item in Executive.Division)
                                {
                                    Link.executiveid = ExecutiveIdId;
                                    Link.divisionid = item;
                                    Link.EnteredBy = Executive.EnteredBy;
                                    _ExecutiveService.InsertExecutiveDivisionLinking(Link);
                                }
                            }

                            if (Executive.ReportingId != 0 && ExecutiveIdId != 0 && Executive.RoleName == "executive")
                            {
                                ExecutiveReporting Reporting = new ExecutiveReporting();
                                Reporting.executiveid = ExecutiveIdId;
                                Reporting.reportingidto = Executive.ReportingId;
                                Reporting.EnteredBy = Executive.EnteredBy;
                                _ExecutiveService.InsertExecutiveReporting(Reporting);
                            }

                        }

                        else
                        {
                            ExecutiveMaster mobj_Excutive = _ExecutiveService.GetExecutiveById(_Executive.Id); //_ExecutiveService.GetDivisionById(Executive);
                            mobj_Excutive.executiveName = Executive.executiveName;
                            mobj_Excutive.executivecode = Executive.executivecode;
                            mobj_Excutive.Emailid = Executive.Emailid;
                            mobj_Excutive.Password = _encryptionService.EncryptText(Executive.Password, _ExecutiveService.KeyValue("encriptionkey"));
                            mobj_Excutive.Mobile = Executive.Mobile;
                            mobj_Excutive.Phoneno = Executive.Phoneno;
                            mobj_Excutive.DepartmentId = Executive.DepartmentId;
                            mobj_Excutive.ModifiedBy = Executive.EnteredBy;
                            mobj_Excutive.ModifiedDate = System.DateTime.Now;

                            _ExecutiveService.UpdateExecutive(mobj_Excutive);

                            ExecutiveDivisionLink Link = new ExecutiveDivisionLink();
                            _ExecutiveService.DeactivateExecutiveDivisionLinking(Executive.Id, Executive.EnteredBy);

                            foreach (var item in Executive.Division)
                            {
                                Link.executiveid = Executive.Id;
                                Link.divisionid = item;
                                Link.EnteredBy = Executive.EnteredBy;
                                _ExecutiveService.InsertExecutiveDivisionLinking(Link);
                            }

                            if (Executive.ReportingId != 0)
                            {
                                _ExecutiveService.DeavtivateExecutiveReporting(Executive.Id, Executive.EnteredBy);
                                ExecutiveReporting Reporting = new ExecutiveReporting();
                                Reporting.executiveid = Executive.Id;
                                if (Executive.RoleName == "executive")
                                {
                                    Reporting.reportingidto = Executive.ReportingId;
                                    Reporting.EnteredBy = Executive.EnteredBy;
                                    _ExecutiveService.InsertExecutiveReporting(Reporting);
                                }
                                else
                                {
                                    Reporting.EnteredBy = Executive.EnteredBy;
                                    _ExecutiveService.UpdateExecutiveReporting(Reporting);
                                }
                            }
                        }


                        //--set executive repoting to or not
                        if (Executive.ReportingId != 0)
                        {
                            ExecutiveMaster mobj_Excutive = _ExecutiveService.GetExecutiveById(Executive.ReportingId);
                            mobj_Excutive.ProcessTransferTo = Executive.ProcessTransferTo;
                            _ExecutiveService.UpdateExecutive(mobj_Excutive);
                        }
                        //-------------------------

                        status = _localizationService.GetResource("Master.API.Success.Message");
                        scope.Complete();
                    }
                    else
                    {
                        status = "Duplicate";
                    }
                    
                }
                catch (ACSException ex)
                {
                    status = ex.InnerException.Message;
                }
                catch (Exception ex)
                {
                    status = ex.InnerException.Message;
                }
            }
            return Json(status);
        }
        
        //Added by Saddam/
        public IHttpActionResult ExecutiveDelete(ExecutiveMaster _executive)
        {
            string status = string.Empty;
            try
            {
                //worked temporary modifty after further process has done
                ExecutiveMaster _Executive = new ExecutiveMaster();
                if (_executive.ProcessTransferTo != 0)
                {
                    //_Executive = _ExecutiveService.GetExecutiveById(_executive.ProcessTransferTo);
                    //_Executive.ModifiedBy = _executive.EnteredBy;
                    //_Executive.ModifiedDate = DateTime.Now;
                    //_Executive.ProcessTransferTo = _executive.Id;
                    //_ExecutiveService.UpdateExecutive(_Executive);
                    
                    IList<ExecutiveReporting> _repotingList = _ExecutiveService.GetRepotingListById(_executive.Id);
                    if (_repotingList.Count > 0)
                    {
                        foreach (var items in _repotingList)
                        {
                            //--delete repoting to who executive is deleted
                            ExecutiveReporting mobj_repoting = _ExecutiveService.getExecutiveReporting(items.executiveid);
                            _ExecutiveService.UpdateExecutiveReporting(mobj_repoting);

                            //--insert repoting to for which executive is deleted
                            ExecutiveReporting Reporting = new ExecutiveReporting();
                            Reporting.executiveid = items.executiveid;
                            Reporting.reportingidto = _executive.ProcessTransferTo;
                            Reporting.EnteredBy = _executive.EnteredBy;
                            _ExecutiveService.InsertExecutiveReporting(Reporting);
                        }
                    }


                    //--set executive repoting to or not
                    ExecutiveMaster mobj_Excutive = _ExecutiveService.GetExecutiveById(_executive.ProcessTransferTo);
                    mobj_Excutive.ProcessTransferTo = 1;
                    _ExecutiveService.UpdateExecutive(mobj_Excutive);
                    //-------------------------
                }

                _Executive = _ExecutiveService.GetExecutiveById(_executive.Id);
                //_Executive.Deactivate = "Y";
                _Executive.block = "Y"; //for only restrict login
                _Executive.ModifiedBy = _executive.EnteredBy;
                _Executive.ModifiedDate = DateTime.Now;
                _Executive.ProcessTransferTo = 0;
                _ExecutiveService.UpdateExecutive(_Executive);
                       
                status = "OK";
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }
        
        [HttpPost]
        public IHttpActionResult WebGetExecutiveById(ExecutiveMaster Executive)
        {
            ACS.Core.Domain.Master.ExecutiveMaster _Executive = _ExecutiveService.GetExecutiveById(Executive.Id);
            var reporting = _Executive.ExecutiveReportings.Where(i => i.Deactivate == "N").ToList().FirstOrDefault();
            var divisionLinks = _Executive.ExecutiveDivisionLinks.Where(a => a.Deactivate == "N").ToList();
            var query = new
            {
                Password = _encryptionService.DecryptText(_Executive.Password, _ExecutiveService.KeyValue("encriptionkey")),
                Executivename = _Executive.executiveName,
                Email = _Executive.Emailid,
                Phone = _Executive.Phoneno,
                Mobile = _Executive.Mobile,
                DepartmentId = _Executive.DepartmentId,
                Id = _Executive.Id,
                Code = _Executive.executivecode

            };


            return Json(SerializeObj.SerializeObject(new { query, reporting, divisionLinks }));
        }
        
        //Added by sanjeet on 16th may 2016
        public IHttpActionResult InsertProductType(ProductTypeMaster ProductType)
        {

            string status = "";
            try
            {

                if (ProductType.parenttypeid == null)
                {
                    ProductType.typelevel = 1;
                }
                else
                {
                    ProductType.typelevel = 2;
                }
                status = _ProductTypeService.DuplicityCheck(ProductType);
                if (status == "Y")
                {
                    if (ProductType.Id == 0)
                    {
                        _ProductTypeService.InsertProductType(ProductType);

                    }
                    else
                    {
                        ProductTypeMaster mobj_producttype = _ProductTypeService.GetProductTypeById(ProductType);
                        mobj_producttype.typeName = ProductType.typeName;
                        mobj_producttype.typelevel = ProductType.typelevel;
                        mobj_producttype.parenttypeid = ProductType.parenttypeid;
                        mobj_producttype.ModifiedBy = ProductType.EnteredBy;
                        mobj_producttype.ModifiedDate = System.DateTime.Now;
                        _ProductTypeService.UpdateProductType(mobj_producttype);

                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");

                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult ProductTypeDelete(ProductTypeMaster _productType)
        {
            string status = string.Empty;
            try
            {

                ProductTypeMaster productType = _ProductTypeService.GetProductTypeById(_productType);
                productType.Deactivate = "Y";
                productType.ModifiedBy = _productType.EnteredBy;
                productType.ModifiedDate = DateTime.Now;
                _ProductTypeService.UpdateProductType(productType);
                status = "OK";
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);

        }

        public IHttpActionResult ProductType(ProductTypeMaster _productType)
        {
            ProductTypeMaster productType = _ProductTypeService.GetProductTypeById(_productType);
            return Json(productType);
        }

        public IHttpActionResult InsertPublishingCompany(PublishingCompanyMaster _publishingCompany)
        {
            string status = string.Empty;
            try
            {
                status = _publishingCompanyService.DuplicityCheck(_publishingCompany);
                if (status == "Y")
                {
                    if (_publishingCompany.Id == 0)
                    {
                        _publishingCompanyService.InsertPublishingCompany(_publishingCompany);
                    }
                    else
                    {
                        PublishingCompanyMaster objPublishingCompany = _publishingCompanyService.GetPublishingCompanyById(_publishingCompany);
                        objPublishingCompany.CompanyName = _publishingCompany.CompanyName;
                        objPublishingCompany.ContactPerson = _publishingCompany.ContactPerson;
                        objPublishingCompany.Address = _publishingCompany.Address;
                        objPublishingCompany.Email = _publishingCompany.Email;
                        objPublishingCompany.Phone = _publishingCompany.Phone;
                        objPublishingCompany.Mobile = _publishingCompany.Mobile;
                        objPublishingCompany.Website = _publishingCompany.Website;
                        objPublishingCompany.CountryId = _publishingCompany.CountryId;
                        objPublishingCompany.OtherCountry = _publishingCompany.OtherCountry;
                        objPublishingCompany.Stateid = _publishingCompany.Stateid;
                        objPublishingCompany.OtherState = _publishingCompany.OtherState;
                        objPublishingCompany.Cityid = _publishingCompany.Cityid;
                        objPublishingCompany.OtherCity = _publishingCompany.OtherCity;

                        objPublishingCompany.ModifiedBy = _publishingCompany.EnteredBy;
                        objPublishingCompany.ModifiedDate = System.DateTime.Now;

                        _publishingCompanyService.UpdatePublishingCompany(objPublishingCompany);

                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                else
                {
                    status = "Duplicate";
                }
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);
        }
        
        [HttpGet]
        public IHttpActionResult getRole()
        {
            var query = _ExecutiveService.getExecutiveRole().Select(i => new
            {
                Id = i.Id,
                Role = i.Role
            }).ToList();

            return Json(query);
        }

        //Added by dheeraj kumar sharma to get manager list based on department
        [HttpPost]
        public IHttpActionResult getManagerList(DepartmentMaster department)
        {
            return Json(_ExecutiveService.getManagerList(department.Id).Select(i => new
            {
                Id = i.Id,
                Manager = i.executiveName,
                Emailid = i.Emailid
            }).OrderBy(i => i.Manager).ToList());
        }

        [HttpPost]
        public IHttpActionResult TransferToExecutiveList(ExecutiveMaster Executive)
        {
            var query = _commonList.GetAllExecutive().Where(i => i.Id != Executive.Id && i.DepartmentId == Executive.DepartmentId).Select(i => new
                        {
                            Id = i.Id,
                            Name = i.executiveName,
                            Emailid = i.Emailid
                        }).ToList();
            return Json(query);
        }

        /// <summary>
        /// Api method to insert Ticker
        /// </summary>
        /// <param name="TickerMaster">accepts Ticker object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertTicker(Ticker _Ticker)
        {
            string status = "";
            try
            {
                status = _commonList.TicketDuplicityCheck(_Ticker);
                if (status == "Y")
                {
                if (_Ticker.Id == 0)
                    {
                        _commonList.InsertTicker(_Ticker);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        Ticker _Tickerdetail = _commonList.GetTickerById(_Ticker.Id);
                        _Tickerdetail.Title = _Ticker.Title;
                        _Tickerdetail.FromDate = _Ticker.FromDate;
                        _Tickerdetail.ToDate = _Ticker.ToDate;
                        _Tickerdetail.Order = _Ticker.Order;
                        _Tickerdetail.ModifiedBy = _Ticker.EnteredBy;
                        _Tickerdetail.ModifiedDate = DateTime.Now;
                        _commonList.UpdateTicker(_Tickerdetail);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertTicker", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertTicker", ex);
                status = ex.InnerException.Message;
            }
            return Json(status);
        }

        [HttpGet]
        public IHttpActionResult GetTickerList()
        {
            var tickerList = _commonList.GetTickerList().ToList();

            var List = (from list in tickerList
                        select new
                        {
                            Id = list.Id,
                            Title = list.Title,
                            FromDate = string.Format("{0:dd/MM/yyyy}", list.FromDate),
                            ToDate = string.Format("{0:dd/MM/yyyy}", list.ToDate),
                            Order = list.Order == null ? 0 : list.Order,
                        }).Distinct().ToList();

            return Json(List);
        }

        [HttpGet]
        public IHttpActionResult getTickerById(int Id)
        {
            Ticker _Ticker = _commonList.GetTickerById(Id);
            var _tickerDetails = new
            {
                Id = _Ticker.Id,
                Title = _Ticker.Title,
                FromDate = string.Format("{0:dd/MM/yyyy}", _Ticker.FromDate),
                ToDate = string.Format("{0:dd/MM/yyyy}", _Ticker.ToDate),
                Order = _Ticker.Order,
            };

            return Json(_tickerDetails);
        }

        [HttpPost]
        public IHttpActionResult DeleteTickerSet(Ticker _Ticker)
        {

            string status = string.Empty;
            try
            {
                Ticker _Tickerdetail = _commonList.GetTickerById(_Ticker.Id);
                _Tickerdetail.Deactivate = "Y";
                _Tickerdetail.DeactivateBy = _Ticker.EnteredBy;
                _Tickerdetail.DeactivateDate = DateTime.Now;
                _commonList.UpdateTicker(_Tickerdetail);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertTicker", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertTicker", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }
        
        //Upload Document for all Common
        [HttpPost]
        public IHttpActionResult UploadDocumentCommon(UploadDocumentModel _objModel)
        {
            string status = string.Empty;
            try
            {
                foreach (UploadFileDetails _FilesData in _objModel.FileDetails)
                {
                    UploadDocument _UploadDocument = new UploadDocument();
                    _UploadDocument.MasterName = _objModel.MasterName;
                    _UploadDocument.MasterId = Convert.ToInt32(_objModel.MasterId);
                    _UploadDocument.FileName = _FilesData.FileName;
                    _UploadDocument.UploadFileName = _FilesData.UploadFileName;
                    _UploadDocument.Deactivate = "N";
                    _UploadDocument.EnteredBy = _objModel.EnteredBy;
                    _UploadDocument.EntryDate = DateTime.Now;
                    _UploadDocument.ModifiedBy = null;
                    _UploadDocument.ModifiedDate = null;
                    _UploadDocument.DeactivateBy = null;
                    _UploadDocument.DeactivateDate = null;
                    _commonList.InsertUploadDocumentDetails(_UploadDocument);
                }
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult GetCommonUploadDocumentList(string MasterName, int MasterId)
        {
            try
            {
                var _documentList = _commonList.getCommonUploadDocumentByMaster(MasterName, MasterId).ToList();
                return Json(_documentList);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
            }
            return null;
        }
        
        [HttpPost]
        public IHttpActionResult DeleteCommonUploadDocumentDetails(UploadDocumentModel _objModel)
        {
            string status = string.Empty;
            try
            {
                UploadDocument _UploadDocument = _commonList.getCommonUploadDocumentById(Convert.ToInt32(_objModel.Id));
                _UploadDocument.Deactivate = "Y";
                _UploadDocument.DeactivateBy = _objModel.EnteredBy;
                _UploadDocument.DeactivateDate = DateTime.Now;
                _commonList.DeleteUploadDocumentDetails(_UploadDocument);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "UploadDocumentCommon", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        [HttpGet]
        public IHttpActionResult getEscalationMatrixList()
        {
            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<ExecutiveModel>("Proc_EscalationMatrixList_get").ToList();

            return Json(_GetList);
        }
        
        /// <summary>
        /// Api method to insert Currency
        /// </summary>
        /// <param name="CurrencyMaster">accepts Currency object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertCurrencyMaster(CurrencyMaster _CurrencyMaster)
        {
            string status = "";
            try
            {
                status = _commonList.CurrencyDuplicityCheck(_CurrencyMaster);
                if (status == "Y")
                {
                    if (_CurrencyMaster.Id == 0)
                    {
                        _commonList.InsertCurrencyMaster(_CurrencyMaster);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        CurrencyMaster obj_CurrencyMasterDetails = _commonList.GetCurrencyMasterById(_CurrencyMaster.Id);
                        obj_CurrencyMasterDetails.CurrencyName = _CurrencyMaster.CurrencyName;
                        obj_CurrencyMasterDetails.Symbol = _CurrencyMaster.Symbol;
                        obj_CurrencyMasterDetails.ModifiedBy = _CurrencyMaster.EnteredBy;
                        obj_CurrencyMasterDetails.ModifiedDate = DateTime.Now;
                        _commonList.UpdateCurrencyMaster(obj_CurrencyMasterDetails);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertCurrencyMaster", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "InsertCurrencyMaster", ex);
                status = ex.InnerException.Message;
            }
            return Json(status);
        }

        [HttpGet]
        public IHttpActionResult getCurrencyMaster()
        {
            var _currencyList = _dbContext.ExecuteStoredProcedureListNewData<CurrencyMastetModel>("Proc_fetchCurrencyMasterList_get").ToList();
            //var _currencyList = _commonList.GetCurrencyMasterList().ToList();

            var _list = (from list in _currencyList
                         select new
                         {
                             Id = list.Id,
                             CurrencyName = list.CurrencyName,
                             SymbolName = list.SymbolName,
                             Symbol = list.Symbol,
                             flag = list.flag
                         }).OrderBy(x => x.CurrencyName).ToList();

            return Json(_list);
        }

        [HttpGet]
        public IHttpActionResult getCurrencyMasterDetailsById(int Id)
        {
            CurrencyMaster obj_CurrencyMaster = _commonList.GetCurrencyMasterById(Id);
            var obj_Details = new
            {
                Id = obj_CurrencyMaster.Id,
                CurrencyName = obj_CurrencyMaster.CurrencyName,
                SymbolName = obj_CurrencyMaster.Symbol,
            };

            return Json(obj_Details);
        }

        [HttpPost]
        public IHttpActionResult DeleteCurrencyMaster(CurrencyMaster _CurrencyMaster)
        {
            string status = string.Empty;
            try
            {
                CurrencyMaster obj_CurrencyMasterDetails = _commonList.GetCurrencyMasterById(_CurrencyMaster.Id);
                obj_CurrencyMasterDetails.Deactivate = "Y";
                obj_CurrencyMasterDetails.DeactivateBy = _CurrencyMaster.EnteredBy;
                obj_CurrencyMasterDetails.DeactivateDate = DateTime.Now;
                _commonList.UpdateCurrencyMaster(obj_CurrencyMasterDetails);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "DeleteCurrencyMaster", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "MasterController.cs", "DeleteCurrencyMaster", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }


    }
}
