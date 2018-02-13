//Added by Saddam on 27/07/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.RightsSelling;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using System.Data;

using System.Data.SqlClient;
using ACS.Data;
using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.RightsSelling;
using SLV.Model.RightsSelling;
using ACS.Services.Master;
using System.Web.Script.Serialization;
using ACS.Services.User;
using ACS.Core.Domain.Master;
using SLV.Model.PaymentTaggingMaster;


namespace SLV.API.Controllers.RightsSelling
{
    public class RightsSellingController : ApiController
    {
        private readonly IRepository<LicenseeMaster> _LicenseeMaster;
        private readonly IRightsSelling _IRightsSelling;
        private readonly IDbContext _dbContext;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IApplicationSetUpService _ApplicationSetUpService;
        private readonly IPublishingCompanyService _PublishingCompanyService;
        private readonly IRepository<RightsSellingUpdate> _RightsSellingUpdateTable;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<LanguageMaster> _LanguageMaster;
        private readonly IRepository<RightsSellingLanguageMaster> _RightsSellingLanguageMaster;

        private readonly IRepository<RightsSellingMaster> _RightsSellingMaster;
        private readonly IProductType _IProductType;

        public RightsSellingController(

                IDbContext dbContext
            , IRepository<LicenseeMaster> LicenseeMaster
            , IRightsSelling IRightsSelling
            , IRepository<ApplicationSetUp> ApplicationSetUp
            , IApplicationSetUpService ApplicationSetUpService
            , IPublishingCompanyService PublishingCompanyService
            , IRepository<RightsSellingUpdate> RightsSellingUpdateTable
            ,ILocalizationService localizationService
            , IRepository<LanguageMaster> LanguageMaster
            , IRepository<RightsSellingLanguageMaster> RightsSellingLanguageMaster
            , IRepository<RightsSellingMaster> RightsSellingMaster
            , IProductType IProductType
            )
        {
            this._dbContext = dbContext;
            this._LicenseeMaster = LicenseeMaster;
            this._IRightsSelling = IRightsSelling;
            _ApplicationSetUp = ApplicationSetUp;
            _ApplicationSetUpService = ApplicationSetUpService;
            _PublishingCompanyService = PublishingCompanyService;
            this._RightsSellingUpdateTable = RightsSellingUpdateTable;
            this._localizationService = localizationService;
            this._LanguageMaster = LanguageMaster;
            this._RightsSellingLanguageMaster = RightsSellingLanguageMaster;
            _IProductType = IProductType;
            _RightsSellingMaster = RightsSellingMaster;
        }

        public IHttpActionResult getLicenseeList()
        {
            return Json(_IRightsSelling.GetLicenseeMasterList().ToList());
        }

        public IHttpActionResult getLicenseeListNew()
        {
            var _List = _IRightsSelling.GetLicenseeMasterList().ToList();
            var _ListNew = (from data in _List
                            select new
                            {
                                Id = data.Id,
                                Master = data.OrganizationName
                            }).ToList();
            return Json(_ListNew);
        }

        public IHttpActionResult LicenseeDetails(LicenseeMaster _LicenseeMaster)
        {
            return Json(_IRightsSelling.GetLicenseeById(_LicenseeMaster));
        }


        public IHttpActionResult InsertRightsSellingMaster(RightsSellingModel _RightsSellingModel)
        {
            try
            {
                string status = string.Empty;
                string RightsSellingCode = string.Empty;
                int id = 0;

                IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "RightsSelling" && x.Deactivate == "N").ToList();
                var AuthorSuggesation = _ApplicationSetUpList.Select(Au => new
                {
                    AuthorCodeValue = Au.keyValue,
                    Id = Au.Id
                });

                RightsSellingMaster _RightsSellingMaster = new RightsSellingMaster();


                //_RightsSellingMaster.RightsSellingCode = _RightsSellingModel.RightsSellingCode;
              //  _RightsSellingMaster.RightsSellingCode = "RS" + _RightsSellingModel.ProuductCode + AuthorSuggesation.FirstOrDefault().AuthorCodeValue;

                _RightsSellingModel.ProuductCode = "RS" + _RightsSellingModel.ProuductCode + AuthorSuggesation.FirstOrDefault().AuthorCodeValue;
                _RightsSellingMaster.RightsSellingCode = _RightsSellingModel.ProuductCode.ToUpper();

                RightsSellingCode = _RightsSellingModel.ProuductCode.ToUpper();

                _RightsSellingMaster.LicenseeID = _RightsSellingModel.LicenseeID;
                _RightsSellingMaster.Licenseecode = _RightsSellingModel.Licenseecode;
                _RightsSellingMaster.OrganizationName = _RightsSellingModel.OrganizationName;
                _RightsSellingMaster.ContactPerson = _RightsSellingModel.ContactPerson;
                _RightsSellingMaster.Address = _RightsSellingModel.Address;
                _RightsSellingMaster.CountryId = _RightsSellingModel.CountryId;
                _RightsSellingMaster.OtherCountry = _RightsSellingModel.OtherCountry;
                _RightsSellingMaster.Stateid = _RightsSellingModel.Stateid;
                _RightsSellingMaster.OtherState = _RightsSellingModel.OtherState;
                _RightsSellingMaster.Cityid = _RightsSellingModel.Cityid;
                _RightsSellingMaster.OtherCity = _RightsSellingModel.OtherCity;
                _RightsSellingMaster.Pincode = _RightsSellingModel.Pincode;
                _RightsSellingMaster.Mobile = _RightsSellingModel.Mobile;
                _RightsSellingMaster.Email = _RightsSellingModel.Email;
                _RightsSellingMaster.URL = _RightsSellingModel.URL;
                _RightsSellingMaster.RequestDate = _RightsSellingModel.RequestDate;
                _RightsSellingMaster.DateContract = _RightsSellingModel.DateContract;
                _RightsSellingMaster.ContractPeriod = _RightsSellingModel.ContractPeriod;
                _RightsSellingMaster.First_Impression_within_date = _RightsSellingModel.First_Impression_within_date;
                _RightsSellingMaster.DateExpiry = _RightsSellingModel.DateExpiry;
                _RightsSellingMaster.Contract_Effective_Date = _RightsSellingModel.Contract_Effective_Date;
                _RightsSellingMaster.ProductCategory = _RightsSellingModel.ProductCategory;
                _RightsSellingMaster.Will_be_material_be_translated = _RightsSellingModel.Will_be_material_be_translated;
               // _RightsSellingMaster.Language = _RightsSellingModel.Language;
                _RightsSellingMaster.Print_Run_Quantity_Allowed = _RightsSellingModel.Print_Run_Quantity_Allowed;
                _RightsSellingMaster.Number_of_Impression_Allowed = _RightsSellingModel.Number_of_Impression_Allowed;
                _RightsSellingMaster.Advance_Payment = _RightsSellingModel.Advance_Payment;
                _RightsSellingMaster.Currency = _RightsSellingModel.Currency;
                _RightsSellingMaster.Payment_Term = _RightsSellingModel.Payment_Term;
                _RightsSellingMaster.Payment_Amount = _RightsSellingModel.Payment_Amount;
                _RightsSellingMaster.Territory_Rights = _RightsSellingModel.Territory_Rights;
                _RightsSellingMaster.Advance_Royalty_Amount = _RightsSellingModel.Advance_Royalty_Amount;
                _RightsSellingMaster.RoyaltyType = _RightsSellingModel.RoyaltyType;
                _RightsSellingMaster.Royalty_Recurring = _RightsSellingModel.Royalty_Recurring;
                _RightsSellingMaster.Recurring_From_Period = _RightsSellingModel.Recurring_From_Period;
                _RightsSellingMaster.Recurring_To_Period = _RightsSellingModel.Recurring_To_Period;
                _RightsSellingMaster.ContractId = _RightsSellingModel.ContractId;
                _RightsSellingMaster.ProductLicenseId = _RightsSellingModel.ProductLicenseId;
                _RightsSellingMaster.Status = _RightsSellingModel.Status;
                _RightsSellingMaster.Remarks = _RightsSellingModel.Remarks;
                _RightsSellingMaster.Frequency = _RightsSellingModel.Frequency;
                _RightsSellingMaster.ProuductId = _RightsSellingModel.ProuductId;

                //added by Prakash on 10 July, 2017
                _RightsSellingMaster.Print_Run_Quantity_Type = _RightsSellingModel.Print_Run_Quantity_Type;
                _RightsSellingMaster.FirstPublicationDate = _RightsSellingModel.FirstPublicationDate;


                _RightsSellingMaster.Deactivate = "N";
                _RightsSellingMaster.EnteredBy = _RightsSellingModel.EnteredBy;
                _RightsSellingMaster.EntryDate = DateTime.Now;
                //_RightsSellingMaster.Contract_Effective_Date = DateTime.Now;
                //_RightsSellingMaster.First_Impression_within_date = DateTime.Now;
                //_RightsSellingMaster.RequestDate = DateTime.Now;
                id = _IRightsSelling.InsertRightsSellingMaster(_RightsSellingMaster);

                if (id > 0)
                {

                    ApplicationSetUp Mobj_ApplicationSetUp = new ApplicationSetUp();

                    Mobj_ApplicationSetUp.Id = AuthorSuggesation.FirstOrDefault().Id;

                    ApplicationSetUp _ApplicationSetUpUpdate = _ApplicationSetUpService.GetApplicationSetUpById(Mobj_ApplicationSetUp);

                    _ApplicationSetUpUpdate.Id = AuthorSuggesation.FirstOrDefault().Id;
                    int Value = Int32.Parse(AuthorSuggesation.FirstOrDefault().AuthorCodeValue) + 1;

                    _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');

                    _ApplicationSetUpUpdate.ModifiedBy = _RightsSellingModel.EnteredBy;
                    _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;

                    _ApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);



                    foreach (var Royalty in _RightsSellingModel.RightsSellingRoyalty)
                    {
                        RightsSellingRoyalty _Royalty = new RightsSellingRoyalty();

                        _Royalty.ContractId = Royalty.ContractId;
                        _Royalty.CopiesFrom = Royalty.CopiesFrom;
                        _Royalty.CopiesTo = Royalty.CopiesTo;
                        _Royalty.Percentage = Royalty.Percentage;
                        _Royalty.ProductLicenseId = Royalty.ProductLicenseId;
                        _Royalty.subproducttypeid = Royalty.subproducttypeid;

                        _Royalty.Deactivate = "N";
                        _Royalty.EnteredBy = _RightsSellingModel.EnteredBy;
                        _Royalty.EntryDate = DateTime.Now;

                        _Royalty.RightsSellingID = id;
                        _IRightsSelling.InsertRightsSellingRoyalty(_Royalty);
                    }

                    if (_RightsSellingModel.Language !=null)
                    {
                        RightsSellingLanguageMaster RightsSellingLanguageLink = new RightsSellingLanguageMaster();

                        int k = 0;

                        foreach (var item in _RightsSellingModel.Language)
                        {
                            RightsSellingLanguageLink.RightsSellingId = id;
                            RightsSellingLanguageLink.languageId = item;
                            RightsSellingLanguageLink.EnteredBy = _RightsSellingModel.EnteredBy;
                            _IRightsSelling.InsertRightsSellingLanguageLink(RightsSellingLanguageLink);
                            k++;
                        }
                    }

                    status = _localizationService.GetResource("Master.API.Success.Message");
                    return Json(SerializeObj.SerializeObject(new { status, RightsSellingCode, id }));

                   // return Json("OK");
                }
                else
                {
                    return Json("");
                }
            }
            catch
            {
                return Json("");
            }
        }

        public IHttpActionResult GetRightsSellingDocumentList(int RightsSellingId)
        {
            try
            {
                var docs = _IRightsSelling.GetRightsSellingDocumentList(RightsSellingId).ToList();
                return Json(SerializeObj.SerializeObject(docs));
            }
            catch { return Json(""); }
        }

        public IHttpActionResult RemoveRightsSellingDocument(RightsSellingDocument doc)
        {
            string status = string.Empty;
            try
            {

                _IRightsSelling.DeavtivateRightsSellingUpdateById(doc.Id);
                status = "Deleted";

            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        public IHttpActionResult GetRightsSellingUpdateList(int RightsSellingId)
        {
            try
            {
                //return Json(_IRightsSelling.GetRightsSellingDocumentList(RightsSellingId));
                var _RightsSellingUpdate = _IRightsSelling.GetRightsSellingUpdateById(RightsSellingId);
                RightsSellingUpdate rightsSellingUpdate = new RightsSellingUpdate();
                rightsSellingUpdate.Cancellation_Reason = _RightsSellingUpdate.Cancellation_Reason;
                rightsSellingUpdate.CancellationDate = _RightsSellingUpdate.CancellationDate;
                rightsSellingUpdate.ContractStatus = _RightsSellingUpdate.ContractStatus;
                rightsSellingUpdate.Date_of_agreement = _RightsSellingUpdate.Date_of_agreement;
                rightsSellingUpdate.Signed_Contract_receiveddate = _RightsSellingUpdate.Signed_Contract_receiveddate;
                rightsSellingUpdate.Signed_Contract_sent_date = _RightsSellingUpdate.Signed_Contract_sent_date;
                rightsSellingUpdate.Remarks = _RightsSellingUpdate.Remarks;

                rightsSellingUpdate.Effectivedate = _RightsSellingUpdate.Effectivedate;
                rightsSellingUpdate.Contractperiodinmonth = _RightsSellingUpdate.Contractperiodinmonth;
                rightsSellingUpdate.Expirydate = _RightsSellingUpdate.Expirydate;


                return Json(SerializeObj.SerializeObject(rightsSellingUpdate));
            }
            catch
            {
                return Json("");
            }
        }


        public IHttpActionResult InsertRightsSellingUpdate(RightsSellingUpdateModel _RightsSellingUpdateModel)
        {
             string status = "";
            try
            {
               
                int id = 0;
                RightsSellingUpdate RightsSellingUpdate;
                RightsSellingUpdate = _IRightsSelling.GetRightsSellingUpdateById((int)_RightsSellingUpdateModel.RightsSellingID);

                if (_RightsSellingUpdateModel.UserType == "rt")
                {

                    if (RightsSellingUpdate == null)
                    {

                        RightsSellingUpdate _RightsSellingUpdate = new RightsSellingUpdate();


                        _RightsSellingUpdate.ContractStatus = _RightsSellingUpdateModel.ContractStatus;
                        _RightsSellingUpdate.Date_of_agreement = _RightsSellingUpdateModel.Date_of_agreement;
                        _RightsSellingUpdate.Signed_Contract_sent_date = _RightsSellingUpdateModel.Signed_Contract_sent_date;
                        _RightsSellingUpdate.Signed_Contract_receiveddate = _RightsSellingUpdateModel.Signed_Contract_receiveddate;
                        _RightsSellingUpdate.CancellationDate = _RightsSellingUpdateModel.CancellationDate;
                        _RightsSellingUpdate.Cancellation_Reason = _RightsSellingUpdateModel.Cancellation_Reason;
                        _RightsSellingUpdate.Contributor_Agreement = _RightsSellingUpdateModel.Contributor_Agreement;
                        _RightsSellingUpdate.RightsSellingID = _RightsSellingUpdateModel.RightsSellingID;
                        _RightsSellingUpdate.Remarks = _RightsSellingUpdateModel.RemarksUpdate;

                        _RightsSellingUpdate.Effectivedate = _RightsSellingUpdateModel.Effectivedate;
                        _RightsSellingUpdate.Contractperiodinmonth = _RightsSellingUpdateModel.ContractperiodUpload;
                        _RightsSellingUpdate.Expirydate = _RightsSellingUpdateModel.Expirydate; 

                        _RightsSellingUpdate.Deactivate = "N";
                        _RightsSellingUpdate.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                        _RightsSellingUpdate.EntryDate = DateTime.Now;

                        id = _IRightsSelling.InsertRightsSellingUpdate(_RightsSellingUpdate);
                    }
                    else
                    {
                        RightsSellingUpdate.ContractStatus = _RightsSellingUpdateModel.ContractStatus;
                        RightsSellingUpdate.Date_of_agreement = _RightsSellingUpdateModel.Date_of_agreement;
                        RightsSellingUpdate.Signed_Contract_sent_date = _RightsSellingUpdateModel.Signed_Contract_sent_date;
                        RightsSellingUpdate.Signed_Contract_receiveddate = _RightsSellingUpdateModel.Signed_Contract_receiveddate;
                        RightsSellingUpdate.CancellationDate = _RightsSellingUpdateModel.CancellationDate;
                        RightsSellingUpdate.Cancellation_Reason = _RightsSellingUpdateModel.Cancellation_Reason;
                        RightsSellingUpdate.Contributor_Agreement = _RightsSellingUpdateModel.Contributor_Agreement;
                        RightsSellingUpdate.RightsSellingID = _RightsSellingUpdateModel.RightsSellingID;
                        RightsSellingUpdate.Remarks = _RightsSellingUpdateModel.Remarks;

                        RightsSellingUpdate.ModifiedBy = _RightsSellingUpdateModel.EnteredBy;
                        RightsSellingUpdate.ModifiedDate = DateTime.Now;

                        id = _IRightsSelling.UpdateRightsSellingUpdate(RightsSellingUpdate);
                    }

                    if (id > 0)
                    {

                        RightsSellingDocument _Document = new RightsSellingDocument();

                        string[] docurl = _RightsSellingUpdateModel.UploadFile.Split(',');
                        int i = 0;
                        foreach (string doc in _RightsSellingUpdateModel.DocumentName)
                        {
                            RightsSellingDocument Link = new RightsSellingDocument();
                            Link.RightsSellingUpdateId = id;
                            Link.Documentname = doc;
                            Link.DocumentFile = docurl[i];
                            Link.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                            Link.Deactivate = "N";
                            Link.EntryDate = DateTime.Now;
                            _IRightsSelling.InsertRightsSellingDocument(Link);
                            i++;
                        }
                       
                    }
                  
                }

                else if (_RightsSellingUpdateModel.UserType == "ad" || _RightsSellingUpdateModel.UserType == "sa")
                {

                    RightsSellingMaster mobj_RightsSellingMaster = new RightsSellingMaster();
                    mobj_RightsSellingMaster.Id = _RightsSellingUpdateModel.Id;
                    RightsSellingMaster _RightsSellingMaster = _IRightsSelling.GetRightsSellingById(mobj_RightsSellingMaster);



                        _RightsSellingMaster.LicenseeID = _RightsSellingUpdateModel.LicenseeID;
                        _RightsSellingMaster.Licenseecode = _RightsSellingUpdateModel.Licenseecode;
                        _RightsSellingMaster.OrganizationName = _RightsSellingUpdateModel.OrganizationName;
                        _RightsSellingMaster.ContactPerson = _RightsSellingUpdateModel.ContactPerson;
                        _RightsSellingMaster.Address = _RightsSellingUpdateModel.Address;
                        _RightsSellingMaster.CountryId = _RightsSellingUpdateModel.CountryId;
                        _RightsSellingMaster.OtherCountry = _RightsSellingUpdateModel.OtherCountry;
                        _RightsSellingMaster.Stateid = _RightsSellingUpdateModel.Stateid;
                        _RightsSellingMaster.OtherState = _RightsSellingUpdateModel.OtherState;
                        _RightsSellingMaster.Cityid = _RightsSellingUpdateModel.Cityid;
                        _RightsSellingMaster.OtherCity = _RightsSellingUpdateModel.OtherCity;
                        _RightsSellingMaster.Pincode = _RightsSellingUpdateModel.Pincode;
                        _RightsSellingMaster.Mobile = _RightsSellingUpdateModel.Mobile;
                        _RightsSellingMaster.Email = _RightsSellingUpdateModel.Email;
                        _RightsSellingMaster.URL = _RightsSellingUpdateModel.URL;
                        _RightsSellingMaster.RequestDate = _RightsSellingUpdateModel.RequestDate;
                       // _RightsSellingMaster.DateContract = _RightsSellingUpdateModel.DateContract;
                       // _RightsSellingMaster.ContractPeriod = _RightsSellingUpdateModel.ContractPeriod;
                   //     _RightsSellingMaster.First_Impression_within_date = _RightsSellingUpdateModel.First_Impression_within_date;
                     //   _RightsSellingMaster.DateExpiry = _RightsSellingUpdateModel.DateExpiry;
                      //  _RightsSellingMaster.Contract_Effective_Date = _RightsSellingUpdateModel.Contract_Effective_Date;
                        _RightsSellingMaster.ProductCategory = _RightsSellingUpdateModel.ProductCategory;
                        _RightsSellingMaster.Will_be_material_be_translated = _RightsSellingUpdateModel.Will_be_material_be_translated;
                        //_RightsSellingMaster.Language = _RightsSellingUpdateModel.Language;
                        _RightsSellingMaster.Print_Run_Quantity_Allowed = _RightsSellingUpdateModel.Print_Run_Quantity_Allowed;
                        _RightsSellingMaster.Number_of_Impression_Allowed = _RightsSellingUpdateModel.Number_of_Impression_Allowed;
                        _RightsSellingMaster.Advance_Payment = _RightsSellingUpdateModel.Advance_Payment;
                        _RightsSellingMaster.Currency = _RightsSellingUpdateModel.Currency;
                        _RightsSellingMaster.Payment_Term = _RightsSellingUpdateModel.Payment_Term;
                        _RightsSellingMaster.Payment_Amount = _RightsSellingUpdateModel.Payment_Amount;
                        _RightsSellingMaster.Territory_Rights = _RightsSellingUpdateModel.Territory_Rights;
                        _RightsSellingMaster.Advance_Royalty_Amount = _RightsSellingUpdateModel.Advance_Royalty_Amount;
                        _RightsSellingMaster.RoyaltyType = _RightsSellingUpdateModel.RoyaltyType;
                        _RightsSellingMaster.Royalty_Recurring = _RightsSellingUpdateModel.Royalty_Recurring;
                        _RightsSellingMaster.Recurring_From_Period = _RightsSellingUpdateModel.Recurring_From_Period;
                        _RightsSellingMaster.Recurring_To_Period = _RightsSellingUpdateModel.Recurring_To_Period;
                        _RightsSellingMaster.ContractId = _RightsSellingUpdateModel.ContractId;
                        _RightsSellingMaster.ProductLicenseId = _RightsSellingUpdateModel.ProductLicenseId;
                        _RightsSellingMaster.Status = _RightsSellingUpdateModel.Status;
                        _RightsSellingMaster.Remarks = _RightsSellingUpdateModel.Remarks;
                        _RightsSellingMaster.Frequency = _RightsSellingUpdateModel.Frequency;
                        _RightsSellingMaster.ProuductId = _RightsSellingUpdateModel.ProuductId;

                        //added by Prakash on 10 July, 2017
                        _RightsSellingMaster.Print_Run_Quantity_Type = _RightsSellingUpdateModel.Print_Run_Quantity_Type;
                        _RightsSellingMaster.FirstPublicationDate = _RightsSellingUpdateModel.FirstPublicationDate;

                        _RightsSellingMaster.ModifiedBy = _RightsSellingUpdateModel.EnteredBy;
                        _RightsSellingMaster.ModifiedDate = DateTime.Now;
                      
                       _IRightsSelling.UpdateRightsSellingMaster(_RightsSellingMaster);



                       if (_RightsSellingUpdateModel.Payment_Term.ToLower() == "royalty")
                        {

                            if (_RightsSellingUpdateModel.ContractId !=null)
                           {
                               _IRightsSelling.DeleteRoyaltySlabLink(_RightsSellingUpdateModel.ContractId, _RightsSellingUpdateModel.EnteredBy, _RightsSellingUpdateModel.Type);
                           }
                            else if (_RightsSellingUpdateModel.ProductLicenseId != null)
                            {
                                _IRightsSelling.DeleteRoyaltySlabLink(_RightsSellingUpdateModel.ProductLicenseId, _RightsSellingUpdateModel.EnteredBy, _RightsSellingUpdateModel.Type);
                            }

                           

                            foreach (var Royalty in _RightsSellingUpdateModel.RightsSellingRoyalty)
                            {
                                RightsSellingRoyalty _Royalty = new RightsSellingRoyalty();

                                _Royalty.ContractId = Royalty.ContractId;
                                _Royalty.CopiesFrom = Royalty.CopiesFrom;
                                _Royalty.CopiesTo = Royalty.CopiesTo;
                                _Royalty.Percentage = Royalty.Percentage;
                                _Royalty.ProductLicenseId = Royalty.ProductLicenseId;
                                _Royalty.subproducttypeid = Royalty.subproducttypeid;

                                _Royalty.Deactivate = "N";
                                _Royalty.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                                _Royalty.EntryDate = DateTime.Now;
                                _Royalty.RightsSellingID = _RightsSellingUpdateModel.Id;
                                _IRightsSelling.InsertRightsSellingRoyalty(_Royalty);
                            }
                        }



                       if (_RightsSellingUpdateModel.Language != null)
                       {
                           _IRightsSelling.DeleteRightsSellingLanguageLink(_RightsSellingUpdateModel.Id, _RightsSellingUpdateModel.EnteredBy);



                           RightsSellingLanguageMaster RightsSellingLanguageLink = new RightsSellingLanguageMaster();

                           int k = 0;

                           foreach (var item in _RightsSellingUpdateModel.Language)
                           {
                               RightsSellingLanguageLink.RightsSellingId = _RightsSellingUpdateModel.Id;
                               RightsSellingLanguageLink.languageId = item;
                               RightsSellingLanguageLink.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                               _IRightsSelling.InsertRightsSellingLanguageLink(RightsSellingLanguageLink);
                               k++;
                           }
                       }



                       var RightsSellingUpdateValue = _RightsSellingUpdateTable.Table.Where(a => a.RightsSellingID == _RightsSellingUpdateModel.Id && a.Deactivate == "N").ToList();
                       if (RightsSellingUpdateValue.Count == 0)
                       {

                           RightsSellingUpdate _RightsSellingUpdate = new RightsSellingUpdate();


                           _RightsSellingUpdate.ContractStatus = _RightsSellingUpdateModel.ContractStatus;
                           _RightsSellingUpdate.Date_of_agreement = _RightsSellingUpdateModel.Date_of_agreement;
                           _RightsSellingUpdate.Signed_Contract_sent_date = _RightsSellingUpdateModel.Signed_Contract_sent_date;
                           _RightsSellingUpdate.Signed_Contract_receiveddate = _RightsSellingUpdateModel.Signed_Contract_receiveddate;
                           _RightsSellingUpdate.CancellationDate = _RightsSellingUpdateModel.CancellationDate;
                           _RightsSellingUpdate.Cancellation_Reason = _RightsSellingUpdateModel.Cancellation_Reason;
                           _RightsSellingUpdate.Contributor_Agreement = _RightsSellingUpdateModel.Contributor_Agreement;
                           _RightsSellingUpdate.RightsSellingID = _RightsSellingUpdateModel.RightsSellingID;
                           _RightsSellingUpdate.Remarks = _RightsSellingUpdateModel.RemarksUpdate;
                           
                           _RightsSellingUpdate.Effectivedate = _RightsSellingUpdateModel.Effectivedate;
                           _RightsSellingUpdate.Contractperiodinmonth = _RightsSellingUpdateModel.ContractperiodUpload;
                           _RightsSellingUpdate.Expirydate = _RightsSellingUpdateModel.Expirydate; 

                           _RightsSellingUpdate.Deactivate = "N";
                           _RightsSellingUpdate.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                           _RightsSellingUpdate.EntryDate = DateTime.Now;

                           id = _IRightsSelling.InsertRightsSellingUpdate(_RightsSellingUpdate);

                           if (id > 0)
                           {

                               RightsSellingDocument _Document = new RightsSellingDocument();

                               string[] docurl = _RightsSellingUpdateModel.UploadFile.Split(',');
                               int i = 0;
                               foreach (string doc in _RightsSellingUpdateModel.DocumentName)
                               {
                                   RightsSellingDocument Link = new RightsSellingDocument();
                                   Link.RightsSellingUpdateId = id;
                                   Link.Documentname = doc;
                                   Link.DocumentFile = docurl[i];
                                   Link.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                                   Link.Deactivate = "N";
                                   Link.EntryDate = DateTime.Now;
                                   _IRightsSelling.InsertRightsSellingDocument(Link);
                                   i++;
                               }
                              
                           }
                          
                       }
                       else
                       {
                           RightsSellingUpdate.ContractStatus = _RightsSellingUpdateModel.ContractStatus;
                           RightsSellingUpdate.Date_of_agreement = _RightsSellingUpdateModel.Date_of_agreement;
                           RightsSellingUpdate.Signed_Contract_sent_date = _RightsSellingUpdateModel.Signed_Contract_sent_date;
                           RightsSellingUpdate.Signed_Contract_receiveddate = _RightsSellingUpdateModel.Signed_Contract_receiveddate;
                           RightsSellingUpdate.CancellationDate = _RightsSellingUpdateModel.CancellationDate;
                           RightsSellingUpdate.Cancellation_Reason = _RightsSellingUpdateModel.Cancellation_Reason;
                           RightsSellingUpdate.Contributor_Agreement = _RightsSellingUpdateModel.Contributor_Agreement;
                           RightsSellingUpdate.RightsSellingID = _RightsSellingUpdateModel.RightsSellingID;
                           RightsSellingUpdate.Remarks = _RightsSellingUpdateModel.RemarksUpdate;

                           RightsSellingUpdate.ModifiedBy = _RightsSellingUpdateModel.EnteredBy;
                           RightsSellingUpdate.ModifiedDate = DateTime.Now;

                           RightsSellingUpdate.Effectivedate = _RightsSellingUpdateModel.Effectivedate;
                           RightsSellingUpdate.Contractperiodinmonth = _RightsSellingUpdateModel.ContractperiodUpload;
                           RightsSellingUpdate.Expirydate = _RightsSellingUpdateModel.Expirydate; 


                           id = _IRightsSelling.UpdateRightsSellingUpdate(RightsSellingUpdate);

                           if (id > 0)
                           {

                               RightsSellingDocument _Document = new RightsSellingDocument();

                               string[] docurl = _RightsSellingUpdateModel.UploadFile.Split(',');
                               int i = 0;
                               foreach (string doc in _RightsSellingUpdateModel.DocumentName)
                               {
                                   RightsSellingDocument Link = new RightsSellingDocument();
                                   Link.RightsSellingUpdateId = id;
                                   Link.Documentname = doc;
                                   Link.DocumentFile = docurl[i];
                                   Link.EnteredBy = _RightsSellingUpdateModel.EnteredBy;
                                   Link.Deactivate = "N";
                                   Link.EntryDate = DateTime.Now;
                                   _IRightsSelling.InsertRightsSellingDocument(Link);
                                   i++;
                               }
                            
                           }
                          
                       }

                        
                  
                    
                }
                status = _localizationService.GetResource("Master.API.Success.Message");
               
               
            }


            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(status);
        }



        public IHttpActionResult InsertRightsSellingHistory(RightsSellingHistory SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _IRightsSelling.InsertRightsSellingHistory(SearchParam);
                status = "OK";
                return Json(status);
            }
        }
        
        public IHttpActionResult GetRightsSellingSearchList(String SessionId)
        {
            try
            {
                if (SessionId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    //    SqlParameter[] parameters = new SqlParameter[1];
                    //    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    //    parameters[0].Value = "'" + SessionId + "'";
                    //    var _GetRightsSelling = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingSearchModel>("Proc_RightsSellingSearch_get", parameters).ToList();

                    //    return Json(_GetRightsSelling);

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SessionId + "'";
                    var _GetRightsSelling1 = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingSearchModel>("Proc_RightsSellingSearch_get", parameters).ToList();

                    var royalty = _IRightsSelling.GetRightsSellingRoyaltyList();
                    var ProductTypeMaster = _IProductType.GetAllProductTypeList();

                    var _GetRightsSelling = (from data in _GetRightsSelling1
                                             select new
                                             {
                                                 Id = data.Id,
                                                 RightsSellingCode = data.RightsSellingCode,
                                                 ProuductId = data.ProuductId,
                                                 ProductCode = data.ProductCode,
                                                 ContractId = data.ContractId,
                                                 AuthorContractCode = data.AuthorContractCode,
                                                 ProductLicenseId = data.ProductLicenseId,
                                                 ProductLicensecode = data.ProductLicensecode,
                                                 RequestDate = data.RequestDate,
                                                 DateContract = data.DateContract,
                                                 DateExpiry = data.DateExpiry,
                                                 OrganizationName = data.OrganizationName,
                                                 Flag = data.Flag,
                                                 ISBN = data.ISBN,
                                                 WorkingProduct = data.WorkingProduct,
                                                 WorkingSubProduct = data.WorkingSubProduct,
                                                 AuthorName = data.AuthorName,

                                                 DateContractForSort = data.DateContractForSort,
                                                 DateExpiryForSort = data.DateExpiryForSort,

                                                 Royalty = from rol in royalty.Where(a => (a.ContractId == data.ContractId && a.ProductLicenseId == null) || a.ContractId == null && a.ProductLicenseId == data.ProductLicenseId).OrderBy(a => a.subproducttypeid)
                                                           join type in ProductTypeMaster
                                                           on rol.subproducttypeid equals type.Id
                                                           select new
                                                           {
                                                               SubProductType = type.typeName,
                                                               CopiesFrom = rol.CopiesFrom,
                                                               CopiesTo = rol.CopiesTo
                                                           }
                                             });

                    return Json(JsonSerializer.SerializeObj.SerializeObject(_GetRightsSelling));

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpPost]
        public IHttpActionResult RightsSellingSerchView(RightsSellingMaster RightsSelling)
        {

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("RightsSellingId", SqlDbType.VarChar, 50);
            if (RightsSelling.Id == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = RightsSelling.Id;
            }

            var _GetRightsSelling = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingViewModel>("Proc_RightsSellingSerchView_get", parameters).ToList();



            var mobj_language = (from RSLM in _RightsSellingLanguageMaster.Table.Where(a => a.Deactivate == "N")
                                 join LM in _LanguageMaster.Table.Where(a => a.Deactivate == "N")
                                 on RSLM.languageId equals LM.Id
                                 select new
                                 {
                                     languageId = RSLM.languageId,
                                     RightsSellingId = RSLM.RightsSellingId,
                                     LanguageName = LM.LanguageName
                                 }
                                      ).Distinct().Where(a => a.RightsSellingId == RightsSelling.Id).ToList();


            //return Json(_GetAuthorReport);


            return Json(new { _GetRightsSelling, mobj_language });
        }


        public IHttpActionResult RightsSellingRoyaltySalbView(RightsSellingRoyalty RightsSelling)
        {

            IList<RightsSellingRoyalty_WITH_SubProductType> _royalty = new List<RightsSellingRoyalty_WITH_SubProductType>();
            foreach (var item in _IRightsSelling.GetRightsSellingRoyaltyList(RightsSelling))
            {
                RightsSellingRoyalty_WITH_SubProductType Royalty = new RightsSellingRoyalty_WITH_SubProductType();
                Royalty.CopiesFrom = item.CopiesFrom;
                Royalty.CopiesTo = item.CopiesTo;
                Royalty.SubProductType = item.ProductTypeMaster.typeName;
                Royalty.subproducttypeid = item.subproducttypeid;
                Royalty.Percentage = item.Percentage;
                _royalty.Add(Royalty);
            }

            return Json(_royalty);
        }

        [HttpGet]
        public IHttpActionResult GetPaymentTaggingSubSidiaryRights(String AuthorId = "", string AuthorContractId = "")
        {
            try
            {
                if (AuthorId == "" && AuthorContractId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    int sln = 0;
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("AuthorId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + AuthorId + "'";
                    parameters[1] = new SqlParameter("AuthorContractId", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + AuthorContractId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_AuthorPaymentTaggingSubSidiaryRights_RS_get", parameters).ToList();
                    var _GetAuthorReport = _GetAuthorReport1.Select(i => new 
                                                                            { 
                                                                                sln = sln++, 
                                                                                authorcontractid = i.authorcontractid, 
                                                                                AuthorId = i.AuthorId, AuthorContractCode = i.AuthorContractCode, 
                                                                                AuthorName = i.AuthorName, 
                                                                                ISBN = i.ISBN == null ? "--" : i.ISBN, 
                                                                                AuthorCode = i.AuthorCode == null ? "--" : i.AuthorCode ,
                                                                                //SAPagreementNo = i.SAPagreementNo == null ? "--" : i.SAPagreementNo,
                                                                                AuthorSAPCode = i.AuthorSAPCode == null ? "--" : i.AuthorSAPCode,
                                                                           
                                                                            }).ToList();
                    var distictAuthorId = _GetAuthorReport1.Select(i => new { AuthorId = i.AuthorId }).Distinct().ToList();

                    return Json(new { _GetAuthorReport, distictAuthorId });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [HttpGet]
        public IHttpActionResult GetPaymentTaggingSubSidiaryRightsByPublishingCompany(String PublishingCompanyId, string ProductLicenseId)
        {
            try
            {
                if (PublishingCompanyId == "" && ProductLicenseId=="")
                {
                    return Json("NOK");
                }
                else
                {
                    int sln = 0;
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("PublishingCompanyId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + PublishingCompanyId + "'";
                    parameters[1] = new SqlParameter("ProductLicenseId", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + ProductLicenseId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_PublishingCompany_PaymentTaggingSubSidiaryRights_get", parameters).ToList();
                    var _GetAuthorReport = _GetAuthorReport1.Select(i => new { 
                                                                                sln = sln++, 
                                                                                PublishingCompanyId = i.PublishingCompanyId, 
                                                                                ProductLicenseId = i.ProductLicenseId, 
                                                                                ProductLicensecode = i.ProductLicensecode, 
                                                                                PublishingCompanyName = i.PublishingCompanyName, 
                                                                                ISBN = i.ISBN == null ? "--" : i.ISBN, 
                                                                                AuthorCode = i.AuthorCode == null ? "--" : i.AuthorCode,
                                                                               // SAPagreementNo = i.SAPagreementNo == null ? "--" : i.SAPagreementNo,
                                                                                AuthorSAPCode = i.AuthorSAPCode == null ? "--" : i.AuthorSAPCode,
                                                                    }).ToList();

                    var distictAuthorId = _GetAuthorReport1.Select(i => new { PublishingCompanyId = i.PublishingCompanyId }).Distinct().ToList();

                    return Json(new { _GetAuthorReport, distictAuthorId });
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IHttpActionResult InsertRightsSellingPaymentTagging(RightsSellingPaymentTaggingModelData _RightsSellingPaymentTaggingModelData)
        {
            try
            {
                foreach (RightsSellingPaymentTaggingModel _PaymentTaggingModel in _RightsSellingPaymentTaggingModelData.RightsSellingPaymentTagging)
                {
                    RightsSellingPaymentTagging _RightsSellingPaymentTagging = new RightsSellingPaymentTagging();
                    _RightsSellingPaymentTagging.Amount = _PaymentTaggingModel.Amount;
                    _RightsSellingPaymentTagging.AuthorAmount = _PaymentTaggingModel.AuthorAmount;
                    _RightsSellingPaymentTagging.BankName = _PaymentTaggingModel.BankName;
                    _RightsSellingPaymentTagging.ChequeDate = _PaymentTaggingModel.ChequeDate;
                    _RightsSellingPaymentTagging.ChequeNumber = _PaymentTaggingModel.ChequeNumber;
                    _RightsSellingPaymentTagging.ContractId = _PaymentTaggingModel.ContractId;
                    _RightsSellingPaymentTagging.OupAmount = _PaymentTaggingModel.OupAmount;
                    _RightsSellingPaymentTagging.PaymentMode = _PaymentTaggingModel.PaymentMode;
                    _RightsSellingPaymentTagging.Percentage = _PaymentTaggingModel.Percentage;
                    _RightsSellingPaymentTagging.ProductLicenseId = _PaymentTaggingModel.ProductLicenseId;
                    _RightsSellingPaymentTagging.subproducttypeid = _PaymentTaggingModel.subproducttypeid;

                    _RightsSellingPaymentTagging.AuthorId = _PaymentTaggingModel.AuthorId;
                    _RightsSellingPaymentTagging.RightsSellingMasterId = _PaymentTaggingModel.RightsSellingId;
                    _RightsSellingPaymentTagging.PublishingCompanyId = _PaymentTaggingModel.PublishingCompanyId;

                    _RightsSellingPaymentTagging.WithHoldingTax = _PaymentTaggingModel.WithHoldingTax;
                    _RightsSellingPaymentTagging.ConverisonRate = _PaymentTaggingModel.ConverisonRate;

                    _RightsSellingPaymentTagging.Deactivate = "N";
                    _RightsSellingPaymentTagging.EnteredBy = _PaymentTaggingModel.EnteredBy;
                    _RightsSellingPaymentTagging.EntryDate = DateTime.Now;
                    _IRightsSelling.InsertRightsSellingPaymentTagging(_RightsSellingPaymentTagging);

                }
                return Json("OK");
            }
            catch
            {
                return Json("");
            }
        }


     

        public IHttpActionResult getRightProductCategoryList()
        {
            IList<ProductCategoryRightMaster> _RightsProductCategory = _IRightsSelling.GetRightProductCategory().ToList();

            var RightsProductCategory = _RightsProductCategory.Select(RPC => new
            {
                Id = RPC.Id,
                ProductCategory = RPC.ProductCategory
            });



            return Json(RightsProductCategory);
        }


        public IHttpActionResult getRightsSellingLanguageList(int Id)
        {


          //  RightsSellingLanguageMaster RightsSellingLanguageMaster = _RightsSellingLanguageMaster.Table.Where(a => a.Deactivate == "N" && a.RightsSellingId == RightsSellingLanguage.Id).FirstOrDefault();

            //if (RightsSellingLanguageMaster != null)
            //{
                var mobj_LanguageDetail = (from RSLM in _RightsSellingLanguageMaster.Table.Where(a => a.Deactivate == "N")
                                           join LM in _LanguageMaster.Table.Where(a => a.Deactivate == "N")
                                           on RSLM.languageId equals LM.Id
                                           select new
                                           {
                                               LanguageName = LM.LanguageName,
                                               RightsSellingId = RSLM.RightsSellingId
                                           }


                                              ).ToList().Distinct().Where(a => a.RightsSellingId == Id);


                return (Json(mobj_LanguageDetail));
            //}
            //else
            //{
            //    return Json("");
            //}

           
        }

        public IHttpActionResult TopSearch(String Code)
        {

            RightsSellingMaster RightsSellingMaster = _RightsSellingMaster.Table.Where(a => a.RightsSellingCode == Code && a.Deactivate == "N").FirstOrDefault();

            if (RightsSellingMaster != null)
            {
                if (RightsSellingMaster.ContractId != null)
                {
                    var _RightsSellingMasterValue = new
                    {
                        Id = RightsSellingMaster.Id,
                        CommeId =  RightsSellingMaster.ContractId,
                        ProuductId = "A" + RightsSellingMaster.ProuductId
                    };

                    return Json(new { _RightsSellingMasterValue });
                }
                else
                {
                    var _RightsSellingMasterValue = new
                    {
                        Id = RightsSellingMaster.Id,
                        CommeId = RightsSellingMaster.ProductLicenseId,
                        ProuductId = "P" + RightsSellingMaster.ProuductId
                    };

                    return Json(new { _RightsSellingMasterValue });
                
                }


             
            }
            else
            {
                string _RightsSellingMasterValue = string.Empty;
                return Json(new { _RightsSellingMasterValue });
            }

        }

        //added by prakash on 29 june, 2017
         [HttpGet]
        public IHttpActionResult GetRightsSellingMasterDetailById(string RightsSellingId)
        {
            try
            {
                var data = _IRightsSelling.GetAllRightsSellingMasterList().Where(x => x.Id == Convert.ToInt32(RightsSellingId)).Select(a => a.RightsSellingCode).SingleOrDefault();
                return Json(data);
            }
            catch { return Json(""); }
        }


         /* Create By  : Prakash
        * Create on  : 20 Sep, 2017
        * Create for : Delete Rights Selling
        */
         [HttpPost]
         public IHttpActionResult DeleteRightsSellingSet(RightsSellingMaster mobj_delete)
         {
             string status = string.Empty;

             try
             {
                 if (mobj_delete.Id != 0)
                 {
                     RightsSellingMaster _RightsSellingMaster = _IRightsSelling.GetRightsSellingById(mobj_delete);
                     _RightsSellingMaster.Deactivate = "Y";
                     _RightsSellingMaster.DeactivateBy = mobj_delete.DeactivateBy;
                     _RightsSellingMaster.DeactivateDate = DateTime.Now;

                     _IRightsSelling.DeleteRightsSellingMaster(_RightsSellingMaster);

                 }
                 status = "OK";

                 return Json(status);
             }
             catch (Exception ex)
             {
                 return Json(ex.ToString());
             }

         }

         /** Create By    :   Saddam
       Create On    :   16/10/2017
       Created For   :   Fetching the details of RightsSelling Detail
 */

         public IHttpActionResult RightsSelling_Detail(PaymentTaggingList PaymentTagging)
         {
             SqlParameter[] parameters = new SqlParameter[1];

             try
             {
                 if (PaymentTagging != null)
                 {
                     parameters[0] = new SqlParameter("RightsSellingId", SqlDbType.VarChar, 50);
                     if (PaymentTagging.Id == 0)
                     {
                         parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                     }
                     else
                     {
                         parameters[0].Value = PaymentTagging.Id;
                     }



                 }


             }
             catch (Exception ex)
             {
             }
             var _GeRightsSellingList = _dbContext.ExecuteStoredProcedureListNewData<PaymentTaggingList>("Proc_RightsSelling_Detail_get", parameters).ToList();


             return Json(_GeRightsSellingList);
         }


    }
}