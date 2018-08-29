using ACS.Core;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using ACS.Data;
using ACS.Services.Configuration;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Services.Product;
using SLV.API.Controllers.JsonSerializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Services.User;
using ACS.Core.Data;
using Logger;
using SLV.Model;
using System.Data.SqlClient;
using System.Data;

namespace SLV.API.Controllers.ProductMaster
{
    public class AddendumController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IProductLicenseService _ProductLicenseService;
        private readonly IProductMasterService _ProductMasterService;
        private readonly IExecutive _ExecutiveService;
        private readonly ISettingService _ISettingService;
        private readonly IAddendumServices _AddendumServices;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;

        private readonly IRepository<ProductLicenseUpdateDetails> _ProductLicenseUpdateDetails;
        private readonly IRepository<AddendumDetails> _AddendumDetails;
        private readonly IRepository<ImpressionDetails> _ImpressionDetails;
        private readonly IRepository<ProductLicense> _ProductLicense;

        public AddendumController(
                IProductLicenseService ProductLicenseService,
                IProductMasterService ProductMasterService,
                ISettingService ISettingService,
                IExecutive ExecutiveService,
                IAddendumServices AddendumServices
              , ILocalizationService localizationService
              , IDbContext dbContext

            , IRepository<ProductLicenseUpdateDetails> ProductLicenseUpdateDetails
            , IRepository<AddendumDetails> AddendumDetails
            , IRepository<ImpressionDetails> ImpressionDetails
            , IRepository<ProductLicense> ProductLicense
            )
        {

            _ProductLicenseService = ProductLicenseService;
            _AddendumServices = AddendumServices;
            _ExecutiveService = ExecutiveService;
            _ISettingService = ISettingService;
            _ProductMasterService = ProductMasterService;
            _localizationService = localizationService;
            this._dbContext = dbContext;

            this._ProductLicenseUpdateDetails = ProductLicenseUpdateDetails;
            this._AddendumDetails = AddendumDetails;
            this._ImpressionDetails = ImpressionDetails;
            this._ProductLicense = ProductLicense;
        }

        [HttpPost]
        public IHttpActionResult getLicenseDetailsByLicenseId(ACS.Core.Domain.Product.ProductLicense ProductLicense)
        {
            ACS.Core.Domain.Product.ProductLicense _ProductLicense = _ProductLicenseService.GetProductLicenseById(ProductLicense);

            DateTime? ProductLicenseExpirydate = null;
            DateTime? ProductLicenseContractDate = null;
           // DateTime? ProductLicenseContractDate = _ProductLicense.ContractDate;
           


            var mstr_requestDate = _ProductLicenseUpdateDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ProductLicense.Id).OrderByDescending(i =>i.EntryDate).FirstOrDefault();

            if (mstr_requestDate != null)
            {
                ProductLicenseContractDate = mstr_requestDate.AgreementDate;
                ProductLicenseExpirydate = mstr_requestDate.Expirydate;
            }



           // DateTime? ProductLicenseExpirydate = _ProductLicense.Expirydate;
            string format = "dd/MM/yyyy";
            string ContractDate = ProductLicenseContractDate == null ? "" : Convert.ToDateTime(ProductLicenseContractDate).ToString(format);
            string ExpiryDate = ProductLicenseExpirydate == null ? "" : Convert.ToDateTime(ProductLicenseExpirydate).ToString(format);
            var LicenseData = new
            {
                Id = _ProductLicense.Id,
                ProductId = _ProductLicense.productid,
                AddendumId = _ProductLicense.ProductLicenseAddendumLink.Where(a=>a.Active =="Y").Select(a =>a.AddendumId).FirstOrDefault(),
                ProductLicensecode = _ProductLicense.ProductLicensecode,
                Company = _ProductLicense.LicensePublishing.CompanyName,
                ContactPerson = _ProductLicense.ContactPerson,
                RequestDate = _ProductLicense.Requestdate.Date.ToString("dd/MM/yyyy"),
                //ContractDate = _ProductLicense.ContractDate.Date.ToString("dd/MM/yyyy"),
                //ExpiryDate = _ProductLicense.Expirydate.Date.ToString("dd/MM/yyyy")

                Impression = _ProductLicense.printquantity == null 
                            ? (_AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).OrderByDescending(a => a.EntryDate).Select(a => a.AddendumQuantity).FirstOrDefault()) 
                            : _ProductLicense.printquantity,
                ImpressionBalance = _ProductLicense.balanceqty == null 
                                    ? ( (_AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQuantity).FirstOrDefault()) == null
                                            ? (_ImpressionDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQty).FirstOrDefault())
                                            : (_AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQuantity).FirstOrDefault()) )
                                    : _ProductLicense.balanceqty,

                //------------------------------------------------
                Licenseprintquantity = _ProductLicense.printquantity,
                LicenseAddendumQuantity = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).Select(a => a.AddendumQuantity).ToList(),
                ImpressionQuantityPrinted = _ImpressionDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).Select(a => a.QunatityPrinted).ToList(),

                BalanceQuantityCarryForward = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQuantityCarryForward).FirstOrDefault(),
                LicenseAddendumQuantity1 = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == _ProductLicense.Id && a.BalanceQuantityCarryForward == "N").OrderByDescending(a => a.EntryDate).Select(a => a.AddendumQuantity).FirstOrDefault(),
                ImpressionQuantityPrinted1 = _ImpressionDetails.Table.Where(a => a.Deactivate == "N" 
                                            && a.LicenseId == _ProductLicense.Id 
                                            && a.AddendumId == (_AddendumDetails.Table.Where(b => b.Deactivate == "N" && b.LicenseId == _ProductLicense.Id && b.BalanceQuantityCarryForward == "N").OrderByDescending(b => b.EntryDate).Select(b => b.Id).FirstOrDefault()))
                                            .Select(a => a.QunatityPrinted).ToList(),
                //------------------------------------------------

                ContractDate = ContractDate,
                ExpiryDate = ExpiryDate,
                printquantitytype = _ProductLicense.printquantitytype //Unrestricted or Number
            };
            return Json(LicenseData);

        }

        [HttpPost]
        public IHttpActionResult InsertAddendumDetails(ACS.Core.Domain.Product.AddendumDetails AddendumDetails)
        {
            int AddendumId = 0;
            string status = string.Empty;
            IList<AddendumRoyaltySlab> _IAddendumRoyaltySlab = new List<AddendumRoyaltySlab>();

            ProductLicense License = _ProductLicenseService.GetProductLicenseById(AddendumDetails.LicenseId);

            AddendumDetails.BalanceQuantity = AddendumDetails.AddendumQuantity;

            if (License.printquantitytype == "Number")
            {
                if (AddendumDetails.BalanceQuantityCarryForward == "Y")
                {
                    AddendumDetails.BalanceQuantity = AddendumDetails.BalanceQuantity + Convert.ToInt32(License.balanceqty);
                }
            }

            foreach (AddendumRoyaltySlab RoyaltySlab in AddendumDetails.AddendumDetailsRoyalty)
            {
                // Author Details Set
                AddendumRoyaltySlab _AddendumRoyaltySlab = new AddendumRoyaltySlab();
                _AddendumRoyaltySlab.ProductSubTypeId = RoyaltySlab.ProductSubTypeId;
                _AddendumRoyaltySlab.copiesfrom = RoyaltySlab.copiesfrom;
                _AddendumRoyaltySlab.copiesto = RoyaltySlab.copiesto;
                _AddendumRoyaltySlab.percentage = RoyaltySlab.percentage;
                _AddendumRoyaltySlab.Deactivate = "N";
                _AddendumRoyaltySlab.EnteredBy = AddendumDetails.EnteredBy;
                _AddendumRoyaltySlab.EntryDate = DateTime.Now;
                _AddendumRoyaltySlab.ModifiedBy = null;
                _AddendumRoyaltySlab.ModifiedDate = null;
                _AddendumRoyaltySlab.DeactivateBy = null;
                _AddendumRoyaltySlab.DeactivateDate = null;
                _IAddendumRoyaltySlab.Add(_AddendumRoyaltySlab);
            }


            ACS.Core.Domain.Product.AddendumDetails _AddendumDetails = new ACS.Core.Domain.Product.AddendumDetails();
            _AddendumDetails.LicenseId = AddendumDetails.LicenseId;
            _AddendumDetails.ProductId = AddendumDetails.ProductId;
            _AddendumDetails.AddendumDate = AddendumDetails.AddendumDate;
            _AddendumDetails.AddendumType = AddendumDetails.AddendumType;
            //_AddendumDetails.Periodofagreement = AddendumDetails.Periodofagreement;
            _AddendumDetails.ExpiryDate = AddendumDetails.ExpiryDate;
            _AddendumDetails.FirstImpressionWithinDate = AddendumDetails.FirstImpressionWithinDate;
            _AddendumDetails.NoOfImpressions = AddendumDetails.NoOfImpressions;
            _AddendumDetails.BalanceQuantityCarryForward = AddendumDetails.BalanceQuantityCarryForward;
            _AddendumDetails.AddendumQuantity = AddendumDetails.AddendumQuantity;
            _AddendumDetails.BalanceQuantity = AddendumDetails.BalanceQuantity;
            _AddendumDetails.RoyaltyTerms = AddendumDetails.RoyaltyTerms;
            _AddendumDetails.Remarks = AddendumDetails.Remarks;
            _AddendumDetails.AddendumDetailsRoyalty = _IAddendumRoyaltySlab;
            _AddendumDetails.Deactivate = "N";
            _AddendumDetails.EnteredBy = AddendumDetails.EnteredBy;
            _AddendumDetails.EntryDate = DateTime.Now;
            _AddendumDetails.ModifiedBy = null;
            _AddendumDetails.ModifiedDate = null;
            _AddendumDetails.DeactivateBy = null;
            _AddendumDetails.DeactivateDate = null;
            AddendumId = _AddendumServices.InsertAddendumDetails(_AddendumDetails);

            if (AddendumId > 0 && License.printquantitytype == "Number")
            {
                License.balanceqty = _AddendumDetails.BalanceQuantity;
                License.ModifiedBy = AddendumDetails.EnteredBy;
                License.ModifiedDate = DateTime.Now;
                _ProductLicenseService.UpdateProductLicenseMaster(License);
            }

            // Addendum Details Set
            ACS.Core.Domain.Product.ProductLicenseAddendumLink _ProductLicenseAddendumLink = new ACS.Core.Domain.Product.ProductLicenseAddendumLink();
            _ProductLicenseAddendumLink.AddendumId = AddendumId;
            _ProductLicenseAddendumLink.ProductId = AddendumDetails.ProductId;
            _ProductLicenseAddendumLink.LicenseId = AddendumDetails.LicenseId;
            _ProductLicenseAddendumLink.Active = "Y";
            _ProductLicenseAddendumLink.Deactivate = "N";
            _ProductLicenseAddendumLink.EnteredBy = AddendumDetails.EnteredBy;
            _ProductLicenseAddendumLink.EntryDate = DateTime.Now;
            _ProductLicenseAddendumLink.ModifiedBy = null;
            _ProductLicenseAddendumLink.ModifiedDate = null;
            _ProductLicenseAddendumLink.DeactivateBy = null;
            _ProductLicenseAddendumLink.DeactivateDate = null;
            _ProductLicenseService.InsertMultipleProductLink(_ProductLicenseAddendumLink);



            IList<AddendumFileDetails> _IAddendumFileDetails = new List<AddendumFileDetails>();



            foreach (AddendumFileDetails FileDetails in AddendumDetails.IAddendumFileDetails)
            {

                // Author Details Set
                AddendumFileDetails _AddendumFileDetails = new AddendumFileDetails();
                _AddendumFileDetails.AddendumId = AddendumId;
                _AddendumFileDetails.LicenseId = FileDetails.LicenseId;
                _AddendumFileDetails.FileName = FileDetails.FileName;
                _AddendumFileDetails.UploadFileName = FileDetails.UploadFileName;
                _AddendumFileDetails.Deactivate = "N";
                _AddendumFileDetails.EnteredBy = AddendumDetails.EnteredBy;
                _AddendumFileDetails.EntryDate = DateTime.Now;
                _AddendumFileDetails.ModifiedBy = null;
                _AddendumFileDetails.ModifiedDate = null;
                _AddendumFileDetails.DeactivateBy = null;
                _AddendumFileDetails.DeactivateDate = null;
                //_IAddendumFileDetails.Add(_AddendumFileDetails);

                _AddendumServices.InsertAddendumFileDetails(_AddendumFileDetails);
            }
            status = "OK";
            return Json(SerializeObj.SerializeObject(new { status, AddendumId }));

            //return Json("OK");
        }

        [HttpPost]
        public IHttpActionResult getAddendumDetailsByAddendumId(ACS.Core.Domain.Product.AddendumDetails AddendumDetails)
        {
            ACS.Core.Domain.Product.AddendumDetails AddendumDetails1 = _AddendumServices.GetAddendumDetailsById(AddendumDetails);

            return Json(SerializeObj.SerializeObject(new { AddendumDetails1 }));


        }

        [HttpPost]
        public IHttpActionResult getAddendumDetailsByLicenseId(int LicenseId)
        {
            IList<AddendumDetails> AddendumDetails1 = _AddendumServices.GetAddendumDetailsByLicenseId(LicenseId);

            var AddendumDetails = (from Addendum in AddendumDetails1
                                   select new
                                   {
                                       Id = Addendum.Id,
                                       LicenseId = Addendum.LicenseId,
                                       AddendumCode = Addendum.AddendumCode,
                                       AddendumDate = Addendum.AddendumDate.toDDMMYYYY(),
                                       AddendumQuantity = Addendum.AddendumQuantity,
                                       ExpiryDate = Addendum.ExpiryDate == null? null : Convert.ToDateTime(Addendum.ExpiryDate).toDDMMYYYY(),
                                       Addendum_Type = Addendum.AddendumType,
                                       //AddendumFileName = Addendum.IAddendumFileDetails.FirstOrDefault().FileName,
                                       //AddendumUploadFileName = Addendum.IAddendumFileDetails.FirstOrDefault().UploadFileName,

                                   }).ToList();

            return Json(SerializeObj.SerializeObject(new { AddendumDetails }));
        }

        [HttpPost]
        public IHttpActionResult UpdateAddendumDetails(ACS.Core.Domain.Product.AddendumDetails AddendumDetails)
        {
            string status = string.Empty;
            if (AddendumDetails.Id > 0)
            {
                ACS.Core.Domain.Product.AddendumDetails mobj_AddendumDetails = _AddendumServices.GetAddendumDetailsById(AddendumDetails);
                List<AddendumRoyaltySlab> _IOldAddendumDetailsRoyalty = mobj_AddendumDetails.AddendumDetailsRoyalty.ToList();
                List<AddendumFileDetails> _IOldAddendumFileDetails = mobj_AddendumDetails.IAddendumFileDetails.ToList();

                foreach (AddendumRoyaltySlab Royality in _IOldAddendumDetailsRoyalty)
                {
                    _AddendumServices.DeleteAddendumRoyaltySlab(Royality);
                }

                foreach (AddendumFileDetails FileDetails in _IOldAddendumFileDetails)
                {
                    _AddendumServices.DeleteAddendumFileDetails(FileDetails);
                }

                IList<AddendumRoyaltySlab> _IAddendumRoyaltySlab = new List<AddendumRoyaltySlab>();


                foreach (AddendumRoyaltySlab RoyaltySlab in AddendumDetails.AddendumDetailsRoyalty)
                {
                    // Author Details Set
                    AddendumRoyaltySlab _AddendumRoyaltySlab = new AddendumRoyaltySlab();
                    _AddendumRoyaltySlab.ProductSubTypeId = RoyaltySlab.ProductSubTypeId;
                    _AddendumRoyaltySlab.copiesfrom = RoyaltySlab.copiesfrom;
                    _AddendumRoyaltySlab.copiesto = RoyaltySlab.copiesto;
                    _AddendumRoyaltySlab.percentage = RoyaltySlab.percentage;
                    _AddendumRoyaltySlab.Deactivate = "N";
                    _AddendumRoyaltySlab.EnteredBy = AddendumDetails.EnteredBy;
                    _AddendumRoyaltySlab.EntryDate = DateTime.Now;
                    _AddendumRoyaltySlab.ModifiedBy = null;
                    _AddendumRoyaltySlab.ModifiedDate = null;
                    _AddendumRoyaltySlab.DeactivateBy = null;
                    _AddendumRoyaltySlab.DeactivateDate = null;
                    _IAddendumRoyaltySlab.Add(_AddendumRoyaltySlab);
                }

                IList<AddendumFileDetails> _IAddendumFileDetails = new List<AddendumFileDetails>();



                foreach (AddendumFileDetails FileDetails in AddendumDetails.IAddendumFileDetails)
                {

                    // Author Details Set
                    AddendumFileDetails _AddendumFileDetails = new AddendumFileDetails();
                    _AddendumFileDetails.AddendumId = FileDetails.AddendumId;
                    _AddendumFileDetails.LicenseId = FileDetails.LicenseId;
                    _AddendumFileDetails.FileName = FileDetails.FileName;
                    _AddendumFileDetails.UploadFileName = FileDetails.UploadFileName;
                    _AddendumFileDetails.Deactivate = "N";
                    _AddendumFileDetails.EnteredBy = AddendumDetails.EnteredBy;
                    _AddendumFileDetails.EntryDate = DateTime.Now;
                    _AddendumFileDetails.ModifiedBy = null;
                    _AddendumFileDetails.ModifiedDate = null;
                    _AddendumFileDetails.DeactivateBy = null;
                    _AddendumFileDetails.DeactivateDate = null;
                    _IAddendumFileDetails.Add(_AddendumFileDetails);
                }


                ACS.Core.Domain.Product.AddendumDetails _AddendumDetails = new ACS.Core.Domain.Product.AddendumDetails();

                mobj_AddendumDetails.AddendumDate = AddendumDetails.AddendumDate;
                mobj_AddendumDetails.AddendumType = AddendumDetails.AddendumType;
                //mobj_AddendumDetails.Periodofagreement = AddendumDetails.Periodofagreement;
                mobj_AddendumDetails.ExpiryDate = AddendumDetails.ExpiryDate;
                mobj_AddendumDetails.FirstImpressionWithinDate = AddendumDetails.FirstImpressionWithinDate;
                mobj_AddendumDetails.NoOfImpressions = AddendumDetails.NoOfImpressions;
                mobj_AddendumDetails.BalanceQuantityCarryForward = AddendumDetails.BalanceQuantityCarryForward;
                mobj_AddendumDetails.AddendumQuantity = AddendumDetails.AddendumQuantity;
                mobj_AddendumDetails.RoyaltyTerms = AddendumDetails.RoyaltyTerms;
                mobj_AddendumDetails.Remarks = AddendumDetails.Remarks;
                mobj_AddendumDetails.AddendumDetailsRoyalty = _IAddendumRoyaltySlab;
                mobj_AddendumDetails.IAddendumFileDetails = _IAddendumFileDetails;
                mobj_AddendumDetails.ModifiedBy = AddendumDetails.EnteredBy;
                mobj_AddendumDetails.ModifiedDate = DateTime.Now;
                _AddendumServices.UpdateAddendumDetails(_AddendumDetails);

            }


            status = "OK";
            return Json(SerializeObj.SerializeObject(new { status }));
            //return Json("OK");
        }

        [HttpPost]
        public IHttpActionResult DeleteFile(ACS.Core.Domain.Product.AddendumFileDetails FileDetails)
        {
            string status = "";
            try
            {
                AddendumFileDetails mobj_FileDetails = _AddendumServices.GetFileDetailsById(FileDetails);
                if (FileDetails.Id > 0)
                {
                    _AddendumServices.DeleteAddendumFileDetails(mobj_FileDetails);
                    status = "OK";
                }
                else
                {
                    status = "Opps";

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

        [HttpPost]
        public IHttpActionResult InsertAddendumFileDetails(ACS.Core.Domain.Product.AddendumDetails AddendumDetails)
        {
            if (AddendumDetails.Id > 0)
            {
                ACS.Core.Domain.Product.AddendumDetails mobj_AddendumDetails = _AddendumServices.GetAddendumDetailsById(AddendumDetails);

                IList<AddendumFileDetails> _IAddendumFileDetails = new List<AddendumFileDetails>();



                foreach (AddendumFileDetails FileDetails in AddendumDetails.IAddendumFileDetails)
                {

                    // Author Details Set
                    AddendumFileDetails _AddendumFileDetails = new AddendumFileDetails();
                    _AddendumFileDetails.AddendumId = FileDetails.AddendumId;
                    _AddendumFileDetails.LicenseId = FileDetails.LicenseId;
                    _AddendumFileDetails.FileName = FileDetails.FileName;
                    _AddendumFileDetails.UploadFileName = FileDetails.UploadFileName;
                    _AddendumFileDetails.Deactivate = "N";
                    _AddendumFileDetails.EnteredBy = AddendumDetails.EnteredBy;
                    _AddendumFileDetails.EntryDate = DateTime.Now;
                    _AddendumFileDetails.ModifiedBy = null;
                    _AddendumFileDetails.ModifiedDate = null;
                    _AddendumFileDetails.DeactivateBy = null;
                    _AddendumFileDetails.DeactivateDate = null;
                    _IAddendumFileDetails.Add(_AddendumFileDetails);
                }

                mobj_AddendumDetails.IAddendumFileDetails = _IAddendumFileDetails;
                _AddendumServices.UpdateAddendumDetails(mobj_AddendumDetails);

            }



            return Json("OK");
        }

        [HttpPost]
        public IHttpActionResult ISBNBlocked(ACS.Core.Domain.Master.ISBNBag IListISBNBag)
        {
            try
            {
                ACS.Core.Domain.Master.ISBNBag mobj_ISBNbag = _AddendumServices.GetISBNBagById(IListISBNBag);
                if (mobj_ISBNbag.Blocked == "Y" && mobj_ISBNbag.ModifiedBy != IListISBNBag.EnteredBy)
                {
                    return Json("otheruser");
                }
                else
                {
                    mobj_ISBNbag.Blocked = "Y";
                    mobj_ISBNbag.ModifiedBy = IListISBNBag.EnteredBy;
                    _AddendumServices.UpdateISBNBag(mobj_ISBNbag);
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AddendumController.cs", "ISBNBlocked", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AddendumController.cs", "ISBNBlocked", ex);
            }

            return Json("OK");
        }

        [HttpPost]
        public IHttpActionResult ISBNAssign(IList<ACS.Core.Domain.Master.ISBNBag> IListISBNBag)
        {

            foreach (ACS.Core.Domain.Master.ISBNBag isbnbag in IListISBNBag)
            {

                ACS.Core.Domain.Master.ISBNBag mobj_ISBNbag = _AddendumServices.GetISBNBagById(isbnbag);
                if (mobj_ISBNbag.Blocked == "Y" && mobj_ISBNbag.ModifiedBy == isbnbag.EnteredBy && mobj_ISBNbag.Used == "N")
                {
                    mobj_ISBNbag.ProductId = isbnbag.ProductId;
                    mobj_ISBNbag.Used = "Y";
                    mobj_ISBNbag.ModifiedBy = isbnbag.EnteredBy;
                    mobj_ISBNbag.ModifiedDate = DateTime.Now;
                    _AddendumServices.UpdateISBNBag(mobj_ISBNbag);

                    ACS.Core.Domain.Product.ProductMaster mobj_ProductMaster = new ACS.Core.Domain.Product.ProductMaster();

                    mobj_ProductMaster.Id = isbnbag.ProductId.GetValueOrDefault();
                    ACS.Core.Domain.Product.ProductMaster mobj_Product = _ProductMasterService.GetProductById(mobj_ProductMaster);
                    mobj_Product.OUPISBN = mobj_ISBNbag.ISBN;
                    mobj_Product.ModifiedBy = isbnbag.EnteredBy;
                    mobj_Product.ModifiedDate = DateTime.Now;
                    _ProductMasterService.UpdateProductMaster(mobj_Product);

                    try
                    {
                        //Call Function to Send Mail
                        SendISBNAssignMail(mobj_Product);
                    }
                    catch (ACSException ex)
                    {
                        _ILog.LogException("", Severity.ProcessingError, "AddendumController.cs", "ISBNAssign", ex);
                    }
                    catch (Exception ex)
                    {
                        _ILog.LogException("", Severity.ProcessingError, "AddendumController.cs", "ISBNAssign", ex);
                    }

                }
                else
                {
                    return Json("otheruser");
                }

            }

            return Json("OK");
        }

        public void SendISBNAssignMail(ACS.Core.Domain.Product.ProductMaster mobj_ProductMaster)
        {
            try
            {
                ExecutiveMaster mobj_ExecutiveMaster = _ExecutiveService.GetExecutiveById(mobj_ProductMaster.EnteredBy);
                string mstr_body = string.Empty;
                using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ISBNAssignMail.html"))))
                {
                    mstr_body = reader.ReadToEnd();
                }
                mstr_body = mstr_body.Replace("{ExecutiveName}", mobj_ExecutiveMaster.executiveName);
                mstr_body = mstr_body.Replace("{ProductCode}", mobj_ProductMaster.ProductCode);
                mstr_body = mstr_body.Replace("{ISBN}", mobj_ProductMaster.OUPISBN);

                string mstrFromEmailID = _ISettingService.getValueByKey("FromEmailID");
                string mstrBCCEmailID = _ISettingService.getValueByKey("BCCEmailId");
                if (mstrFromEmailID != "" && mstrFromEmailID != null)
                {

                    MailSend.SendMail(mstr_body, mobj_ExecutiveMaster.Emailid, mstrFromEmailID, mstrBCCEmailID);
                }
            }
            catch (Exception ex)
            {
                //_ILogger.Error(string.Format("Problem in Send Reply For Service", ex.Message), ex);

            }
        }

        [HttpPost]
        public IHttpActionResult ImpressionDetails(ImpressionDetails ImpressionDetails)
        {
            IList<ImpressionDetails> _ImpressionDetails = _AddendumServices.GetImpressionDetails(ImpressionDetails).ToList();

            var data = (from Impression in _ImpressionDetails
                        select new
                        {
                            ImpressionDate = Impression.ImpressionDate.toDDMMYYYY(),
                            QunatityPrinted = Impression.QunatityPrinted,
                            //ExpiryDate = Addendum.ExpiryDate == null ? null : Convert.ToDateTime(Addendum.ExpiryDate).toDDMMYYYY()
                            BalanceQty = Impression.BalanceQty,
                            AddendumId = Impression.AddendumId,
                            //------------------------------------------------
                            Licenseprintquantity = _ProductLicense.Table.Where(a => a.Deactivate == "N" && a.Id == ImpressionDetails.LicenseId).Select(a => a.printquantity).FirstOrDefault(),
                            LicenseAddendumQuantity = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId).Select(a => a.AddendumQuantity).ToList(),

                            BalanceQuantityCarryForward = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQuantityCarryForward).FirstOrDefault(),
                            LicenseAddendumQuantity1 = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId && a.BalanceQuantityCarryForward == "N").OrderByDescending(a => a.EntryDate).Select(a => a.AddendumQuantity).FirstOrDefault(),
                            CarryForwardAddendunId = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId && a.BalanceQuantityCarryForward == "N").OrderByDescending(a => a.EntryDate).Select(a => a.Id).FirstOrDefault()
                            //------------------------------------------------
                        }).ToList();

            return Json(SerializeObj.SerializeObject(data));
        }

        [HttpPost]
        public IHttpActionResult InsertImpressionEntry(ImpressionDetails ImpressionDetails)
        {
            try
            {
                ImpressionDetails mobj_ImpressionDetails = new ImpressionDetails();
                if (ImpressionDetails.AddendumId > 0)
                {
                    AddendumDetails _AddendumDetails = new AddendumDetails();
                    _AddendumDetails.Id = ImpressionDetails.AddendumId.GetValueOrDefault();
                    AddendumDetails mobj_AddendumDetails = _AddendumServices.GetAddendumDetailsById(_AddendumDetails);
                    mobj_AddendumDetails.NoOfImpressions = mobj_AddendumDetails.NoOfImpressions - 1;
                    if (mobj_AddendumDetails.NoOfImpressions < 0)
                    {
                        return Json("No. of impression are finished");
                    }
                    mobj_AddendumDetails.BalanceQuantity = ImpressionDetails.BalanceQty - ImpressionDetails.QunatityPrinted;
                    if (mobj_AddendumDetails.BalanceQuantity < 0)
                    {
                        return Json("No. of print are exceed");
                    }
                    _AddendumServices.UpdateAddendumDetails(mobj_AddendumDetails);


                    ACS.Core.Domain.Product.ProductLicense _ProductLicense = new ACS.Core.Domain.Product.ProductLicense();
                    _ProductLicense.Id = ImpressionDetails.LicenseId.GetValueOrDefault();
                    ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = _ProductLicenseService.GetProductLicenseById(_ProductLicense);
                    mobj_ProductLicense.balanceqty = ImpressionDetails.BalanceQty - ImpressionDetails.QunatityPrinted;
                    _ProductLicenseService.UpdateProductLicense(mobj_ProductLicense);


                }
                else if (ImpressionDetails.LicenseId > 0)
                {
                    ACS.Core.Domain.Product.ProductLicense _ProductLicense = new ACS.Core.Domain.Product.ProductLicense();
                    _ProductLicense.Id = ImpressionDetails.LicenseId.GetValueOrDefault();
                    ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = _ProductLicenseService.GetProductLicenseById(_ProductLicense);
                    mobj_ProductLicense.noofimpressions = mobj_ProductLicense.noofimpressions - 1;
                    if (mobj_ProductLicense.noofimpressions < 0)
                    {
                        return Json("No. of impression are finished");
                    }
                    mobj_ProductLicense.balanceqty = ImpressionDetails.BalanceQty - ImpressionDetails.QunatityPrinted;
                    if (mobj_ProductLicense.balanceqty < 0)
                    {
                        return Json("No. of print are exceed");
                    }
                    _ProductLicenseService.UpdateProductLicense(mobj_ProductLicense);

                }
                mobj_ImpressionDetails.ProductId = ImpressionDetails.ProductId;
                mobj_ImpressionDetails.LicenseId = ImpressionDetails.LicenseId;
                mobj_ImpressionDetails.AddendumId = ImpressionDetails.AddendumId;
                mobj_ImpressionDetails.ContractId = ImpressionDetails.ContractId;
                mobj_ImpressionDetails.ImpressionNo = ImpressionDetails.ImpressionNo;
                mobj_ImpressionDetails.ImpressionDate = ImpressionDetails.ImpressionDate;
                mobj_ImpressionDetails.QunatityPrinted = ImpressionDetails.QunatityPrinted;
                mobj_ImpressionDetails.BalanceQty = ImpressionDetails.BalanceQty - ImpressionDetails.QunatityPrinted < 0 ? null : ImpressionDetails.BalanceQty - ImpressionDetails.QunatityPrinted;
                mobj_ImpressionDetails.Deactivate = "N";
                mobj_ImpressionDetails.EnteredBy = ImpressionDetails.EnteredBy;
                mobj_ImpressionDetails.EntryDate = DateTime.Now;
                mobj_ImpressionDetails.ModifiedBy = null;
                mobj_ImpressionDetails.ModifiedDate = null;
                mobj_ImpressionDetails.DeactivateBy = null;
                mobj_ImpressionDetails.DeactivateDate = null;
                _AddendumServices.ImsertImpressionDetails(mobj_ImpressionDetails);

                return Json("OK");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


        [HttpPost]
        public IHttpActionResult getLicenseExpiryDateById(int LicenseId)
        {
            var _expiryDate = _ProductLicenseUpdateDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == LicenseId).OrderByDescending(a => a.EntryDate).Select(a => a.Expirydate).FirstOrDefault();
            return Json(_expiryDate);
        }

        //--added by prakash on 03 April, 2018
        [HttpPost]
        public IHttpActionResult ImpressionDetailsList(ImpressionDetailsListModel _ImpressionDetails)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
            if (_ImpressionDetails.ProductId == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = _ImpressionDetails.ProductId;
            }

            parameters[1] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
            if (_ImpressionDetails.LicenseId == 0)
            {
                parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[1].Value = 0;
            }

            parameters[2] = new SqlParameter("ContractId", SqlDbType.VarChar, 50);
            if (_ImpressionDetails.ContractId == 0)
            {
                parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[2].Value = _ImpressionDetails.ContractId;
            }
            
            var _ImpressionDetailsList = _dbContext.ExecuteStoredProcedureListNewData<ImpressionDetailsListModel>("Proc_ImpressionDetailsList_get", parameters).ToList();
            return Json(_ImpressionDetailsList);
        }


        [HttpPost]
        public IHttpActionResult ImpressionDetailsListForLicense(ImpressionDetails ImpressionDetails)
        {
            //IList<ImpressionDetails> _ImpressionDetails = _AddendumServices.GetImpressionDetails(ImpressionDetails).ToList();

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
            if (ImpressionDetails.ProductId == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = ImpressionDetails.ProductId;
            }

            parameters[1] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
            if (ImpressionDetails.LicenseId == 0)
            {
                parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[1].Value = ImpressionDetails.LicenseId;
            }

            parameters[2] = new SqlParameter("ContractId", SqlDbType.VarChar, 50);
            if (ImpressionDetails.ContractId == 0)
            {
                parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[2].Value = 0;
            }
            var _ImpressionDetailsList = _dbContext.ExecuteStoredProcedureListNewData<ImpressionDetailsListModel>("Proc_ImpressionDetailsList_get", parameters).ToList();

            var data = (from Impression in _ImpressionDetailsList
                        select new
                        {
                            ISBN = Impression.ISBN,
                            ImpressionDate = Impression.ImpressionDate,
                            QunatityPrinted = Impression.QunatityPrinted,
                            BalanceQty = Impression.BalanceQty,
                            AddendumId = Impression.AddendumId,
                            //------------------------------------------------
                            Licenseprintquantity = _ProductLicense.Table.Where(a => a.Deactivate == "N" && a.Id == ImpressionDetails.LicenseId).Select(a => a.printquantity).FirstOrDefault(),
                            LicenseAddendumQuantity = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId).Select(a => a.AddendumQuantity).ToList(),

                            BalanceQuantityCarryForward = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId).OrderByDescending(a => a.EntryDate).Select(a => a.BalanceQuantityCarryForward).FirstOrDefault(),
                            LicenseAddendumQuantity1 = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId && a.BalanceQuantityCarryForward == "N").OrderByDescending(a => a.EntryDate).Select(a => a.AddendumQuantity).FirstOrDefault(),
                            CarryForwardAddendunId = _AddendumDetails.Table.Where(a => a.Deactivate == "N" && a.LicenseId == ImpressionDetails.LicenseId && a.BalanceQuantityCarryForward == "N").OrderByDescending(a => a.EntryDate).Select(a => a.Id).FirstOrDefault()
                            //------------------------------------------------
                        }).ToList();

            return Json(SerializeObj.SerializeObject(data));
        }


    }
}
