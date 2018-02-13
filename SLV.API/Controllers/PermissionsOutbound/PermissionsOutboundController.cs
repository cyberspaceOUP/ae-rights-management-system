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
using ACS.Core.Domain.PermissionsOutbound;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.AuthorContract;
using ACS.Services.Master;
using ACS.Services.PermissionsOutbound;
using SLV.Model.RightsSelling;

using ACS.Services.User;
using SLV.Model.PaymentTaggingMaster;



namespace SLV.API.Controllers.PermissionsOutbound
{
   


    public class PermissionsOutboundController : ApiController
    {
        private readonly IRepository<LicenseeMaster> _LicenseeMaster;
        private readonly IPermissionsOutboundService  _IPermissionsOutboundService;
        private readonly IDbContext _dbContext;
        private readonly IRepository<AuthorContractSubsidiaryRights> _AuthorContractSubsidiaryRights;
        private readonly IRepository<SubsidiaryRightsMaster> _SubsidiaryRightsMaster;
        private readonly IRepository<ProductLicenseSubsidiaryRights> _ProductLicenseSubsidiaryRights;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IApplicationSetUpService _ApplicationSetUpService;
        private readonly ILocalizationService _localizationService;

        private readonly IRepository<PermissionsOutboundTypeOfRightsMaster> _PermissionsOutboundTypeOfRightsMaster;

        private readonly IRepository<PermissionsOutboundUpdate> _PermissionsOutboundUpdate;
        private readonly IRepository<PermissionsOutboundDocument> _PermissionsOutboundDocument;
        private readonly IRepository<TypeOfRightsMaster> _objTypeOfRightsRepository;

        private readonly IRepository<PermissionsOutboundLanguageMaster> _PermissionsOutboundLanguageMaster;
        private readonly IRepository<LanguageMaster> _LanguageMaster;

        private readonly IRepository<PermissionsOutboundMaster> _PermissionsOutboundMaster;

        private readonly IRepository<AuthorMaster> _AuthorMaster;

     ///   private readonly IRepository<AuthorContractSubsidiaryRights> _AuthorContractSubsidiaryRights;
       
        public PermissionsOutboundController(

                IDbContext dbContext
            , IRepository<LicenseeMaster> LicenseeMaster
            , IPermissionsOutboundService IPermissionsOutboundService
            ,IRepository<AuthorContractSubsidiaryRights> AuthorContractSubsidiaryRights
             , IRepository<SubsidiaryRightsMaster> SubsidiaryRightsMaster
            , IRepository<ProductLicenseSubsidiaryRights> ProductLicenseSubsidiaryRights
            ,IRepository<ApplicationSetUp> ApplicationSetUp
            ,IApplicationSetUpService ApplicationSetUpService
            ,ILocalizationService localizationService
            , IRepository<PermissionsOutboundTypeOfRightsMaster> PermissionsOutboundTypeOfRightsMaster
            ,IRepository<PermissionsOutboundUpdate> PermissionsOutboundUpdate
            , IRepository<PermissionsOutboundDocument> PermissionsOutboundDocument
            ,IRepository<TypeOfRightsMaster> objTypeOfRightsRepository
            , IRepository<PermissionsOutboundLanguageMaster> PermissionsOutboundLanguageMaster
           , IRepository<LanguageMaster> LanguageMaster
            , IRepository<PermissionsOutboundMaster> PermissionsOutboundMaster
            , IRepository<AuthorMaster> AuthorMaster
            )
        {

            this._dbContext = dbContext;
            this._LicenseeMaster = LicenseeMaster;
            this._IPermissionsOutboundService = IPermissionsOutboundService;
            this._AuthorContractSubsidiaryRights = AuthorContractSubsidiaryRights;
            this._SubsidiaryRightsMaster = SubsidiaryRightsMaster;
            this._ProductLicenseSubsidiaryRights = ProductLicenseSubsidiaryRights;
            this._ApplicationSetUp = ApplicationSetUp;
            this._ApplicationSetUpService = ApplicationSetUpService;
            this._localizationService = localizationService;
            this._PermissionsOutboundTypeOfRightsMaster = PermissionsOutboundTypeOfRightsMaster;
            this._PermissionsOutboundUpdate = PermissionsOutboundUpdate;
            this._PermissionsOutboundDocument = PermissionsOutboundDocument;
            this._objTypeOfRightsRepository = objTypeOfRightsRepository;
            this._PermissionsOutboundLanguageMaster = PermissionsOutboundLanguageMaster;
            this._LanguageMaster = LanguageMaster;
            this._PermissionsOutboundMaster = PermissionsOutboundMaster;
            this._AuthorMaster = AuthorMaster;
           
        }



        [HttpGet]
        public IHttpActionResult GetTypeOfRightsList()
        {
         

            var mvarTypeOfRightsList = (from T in _objTypeOfRightsRepository.Table.Where(a => a.Deactivate == "N")
                                        select new
                                        {
                                            Id = T.Id,
                                            TypeOfRights = T.TypeOfRights,
                                          SubTypeRights = T.SubTypeRights,
                                            Deactivate = T.Deactivate,
                                        }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.TypeOfRights);

            return Json(mvarTypeOfRightsList);
        }



        public IHttpActionResult getLicenseeList()
        {
            return Json(_IPermissionsOutboundService.GetLicenseeMasterList().ToList());
        }


        public IHttpActionResult LicenseeDetails(LicenseeMaster _LicenseeMaster)
        {
            return Json(_IPermissionsOutboundService.GetLicenseeById(_LicenseeMaster));
        }

        public IHttpActionResult SubsidiaryRightsOutBound(PermissionsOutboundModel _PermissionsOutBound)
        {
            var mobj_SubsidiaryRightsAuthorContcat = (from AUC in _AuthorContractSubsidiaryRights.Table.Where(a => a.Deactivate == "N") //&& a.AuthorPercentage !=0
                                                      join SRM in _SubsidiaryRightsMaster.Table.Where(a => a.Deactivate == "N")
                                                      on AUC.subsidiaryrightsid equals SRM.Id into output
                                                      from d in output.DefaultIfEmpty()

                                                      //added by Prakash on 14 June, 2017 
                                                      join am in _AuthorMaster.Table.Where(a => a.Deactivate == "N")
                                                      on AUC.AuthorId equals am.Id
                                                      into am_out
                                                      from amNew in am_out.DefaultIfEmpty()

                                                      select new
                                                      {
                                                          Id = AUC.AuthorContractid,
                                                          SubsidiaryRights = d.SubsidiaryRights,
                                                          AuthorPercentage = AUC.AuthorPercentage,
                                                          Ouppercentage = AUC.ouppercentage,
                                                          Deactivate = AUC.Deactivate,

                                                          //added by Prakash on 14 June, 2017 
                                                          AuthorName = amNew.FirstName + " " + amNew.LastName,
                                                          AuthorId = AUC.AuthorId,
                                                          SubsidiaryId = d.Id
                                                      }
                ).Distinct().Where(a => a.Id == _PermissionsOutBound.id && a.Deactivate == "N" ).OrderBy(a => a.SubsidiaryRights).ToList();
                return Json(mobj_SubsidiaryRightsAuthorContcat);
        }

        public IHttpActionResult SubsidiaryRightsLicense(PermissionsOutboundModel _PermissionsOutBound)
        {
            var mobj_SubsidiaryRightsProductLicense = (from PLSR in _ProductLicenseSubsidiaryRights.Table.Where(a => a.Deactivate == "N" && a.publisherpercentage !=0)
                                                       join SRM in _SubsidiaryRightsMaster.Table.Where(a => a.Deactivate == "N")
                                                       on PLSR.subsidiaryrightsid equals SRM.Id into output
                                                       from d in output.DefaultIfEmpty()
                                                       select new
                                                       {
                                                           Id = PLSR.ProductLicenseid,
                                                           SubsidiaryRights = d.SubsidiaryRights,
                                                           publisherpercentage = PLSR.publisherpercentage,
                                                           Ouppercentage = PLSR.ouppercentage,
                                                           Deactivate = PLSR.Deactivate
                                                       }
                ).Distinct().Where(a => a.Id == _PermissionsOutBound.id && a.Deactivate == "N").OrderBy(a => a.SubsidiaryRights);
            return Json(mobj_SubsidiaryRightsProductLicense);
        }


        public IHttpActionResult InsertPermissionsOutbound(PermissionsOutboundModel PermissionsOutBound)
        {
            string status = "";
            string PermissionsOutbound_CodeValue = string.Empty;
            int PermissionsOutboundIdId = 0;
            if (PermissionsOutBound.id == 0)
            {

                IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "PermissionsOutbound" && x.Deactivate == "N").ToList();
                var PermissionsOutboundSuggesation = _ApplicationSetUpList.Select(Au => new
                {
                    PermissionsOutboundCodeValue = Au.keyValue,
                    Id = Au.Id
                });


                PermissionsOutboundMaster _PermissionsOutBound = new PermissionsOutboundMaster();

                PermissionsOutBound.PermissionsOutboundCode = "PO" + PermissionsOutBound.ProductCode + PermissionsOutboundSuggesation.FirstOrDefault().PermissionsOutboundCodeValue;

                _PermissionsOutBound.PermissionsOutboundCode = PermissionsOutBound.PermissionsOutboundCode.ToUpper();

                PermissionsOutbound_CodeValue = PermissionsOutBound.PermissionsOutboundCode.ToUpper();

                _PermissionsOutBound.LicenseeID = PermissionsOutBound.LicenseeID;

                _PermissionsOutBound.Licenseecode = PermissionsOutBound.Licenseecode;

                _PermissionsOutBound.OrganizationName = PermissionsOutBound.OrganizationName;

                _PermissionsOutBound.ContactPerson = PermissionsOutBound.ContactPerson;

                _PermissionsOutBound.Address = PermissionsOutBound.Address;

                _PermissionsOutBound.CountryId = PermissionsOutBound.CountryId;

                _PermissionsOutBound.Stateid = PermissionsOutBound.Stateid;

                _PermissionsOutBound.Cityid = PermissionsOutBound.Cityid;

                _PermissionsOutBound.Pincode = PermissionsOutBound.Pincode;

                _PermissionsOutBound.Mobile = PermissionsOutBound.Mobile;

                _PermissionsOutBound.Email = PermissionsOutBound.Email;

                _PermissionsOutBound.URL = PermissionsOutBound.URL;

                _PermissionsOutBound.RequestDate = PermissionsOutBound.RequestDate;

                _PermissionsOutBound.LicenseePublicationTitle = PermissionsOutBound.LicenseePublicationTitle;

             //   _PermissionsOutBound.DateOfPermission = PermissionsOutBound.DateOfPermission;

              //  _PermissionsOutBound.PermissionPeriod = PermissionsOutBound.PermissionPeriod;

             //   _PermissionsOutBound.DateExpiry = PermissionsOutBound.DateExpiry;

                _PermissionsOutBound.RequestMaterial = PermissionsOutBound.RequestMaterial;

                _PermissionsOutBound.Will_be_material_be_translated = PermissionsOutBound.Will_be_material_be_translated;

                _PermissionsOutBound.Will_be_material_be_adepted = PermissionsOutBound.Will_be_material_be_adepted;

                _PermissionsOutBound.LanguageId = PermissionsOutBound.LanguageId;

                _PermissionsOutBound.Extent = PermissionsOutBound.Extent;

                _PermissionsOutBound.TerritoryId = PermissionsOutBound.TerritoryId;

                _PermissionsOutBound.DateOfInvoice = PermissionsOutBound.DateOfInvoice;

                _PermissionsOutBound.InvoiceApplicable = PermissionsOutBound.InvoiceApplicable;

                _PermissionsOutBound.InvoiceNo = PermissionsOutBound.InvoiceNo;

                _PermissionsOutBound.InvoiceCurrency = PermissionsOutBound.InvoiceCurrency;

                _PermissionsOutBound.InvoiceValue = PermissionsOutBound.InvoiceValue;

                _PermissionsOutBound.InvoiceDescription = PermissionsOutBound.InvoiceDescription;

                _PermissionsOutBound.Copies_To_Be_Received = PermissionsOutBound.Copies_To_Be_Received;

                _PermissionsOutBound.NumberOfCopies = PermissionsOutBound.NumberOfCopies;



                _PermissionsOutBound.PaymentReceived = PermissionsOutBound.PaymentReceived;

                _PermissionsOutBound.Remarks = PermissionsOutBound.Remarks;

                _PermissionsOutBound.productid = PermissionsOutBound.productid;
                _PermissionsOutBound.ContactId = PermissionsOutBound.ContactId;

                _PermissionsOutBound.Type = PermissionsOutBound.Type;

                _PermissionsOutBound.EnteredBy = PermissionsOutBound.EnteredBy;

                PermissionsOutboundIdId = _IPermissionsOutboundService.InsertPermissionsOutbound(_PermissionsOutBound);

                if (PermissionsOutboundIdId != 0)
                {

                    int i = 0;
                    foreach (var item in PermissionsOutBound.SupplyTypeOfRights)
                    {
                        PermissionsOutboundTypeOfRightsMaster obj_PermissionsOutboundTypeOfRights = new PermissionsOutboundTypeOfRightsMaster();
                        obj_PermissionsOutboundTypeOfRights.PermissionsOutboundId = PermissionsOutboundIdId;
                        obj_PermissionsOutboundTypeOfRights.TypeofRightsId = item.TypeofRightsId;
                        obj_PermissionsOutboundTypeOfRights.Quantity = item.Quantity;
                        obj_PermissionsOutboundTypeOfRights.EnteredBy = PermissionsOutBound.EnteredBy;
                        obj_PermissionsOutboundTypeOfRights.Deactivate = "N";
                        obj_PermissionsOutboundTypeOfRights.EntryDate = DateTime.Now;
                        _IPermissionsOutboundService.InsertPermissionsOutboundTypeOfRightsLinking(obj_PermissionsOutboundTypeOfRights);
                        i++;

                    }

                    PermissionsOutboundLanguageMaster PermissionsOutboundLanguageLink = new PermissionsOutboundLanguageMaster();

                    if (PermissionsOutBound.Language !=null)
                    {
                        int k = 0;

                        foreach (var item in PermissionsOutBound.Language)
                        {
                            PermissionsOutboundLanguageLink.PermissionsOutboundId = PermissionsOutboundIdId;
                            PermissionsOutboundLanguageLink.languageId = item;
                            PermissionsOutboundLanguageLink.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundLanguageLink(PermissionsOutboundLanguageLink);
                            k++;
                        }
                    }

                   



                }





                ApplicationSetUp Mobj_ApplicationSetUp = new ApplicationSetUp();

                Mobj_ApplicationSetUp.Id = PermissionsOutboundSuggesation.FirstOrDefault().Id;

                ApplicationSetUp _ApplicationSetUpUpdate = _ApplicationSetUpService.GetApplicationSetUpById(Mobj_ApplicationSetUp);

                _ApplicationSetUpUpdate.Id = PermissionsOutboundSuggesation.FirstOrDefault().Id;
                int Value = Int32.Parse(PermissionsOutboundSuggesation.FirstOrDefault().PermissionsOutboundCodeValue) + 1;

                _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');

                _ApplicationSetUpUpdate.ModifiedBy = PermissionsOutBound.EnteredBy;
                _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;

                _ApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);
            }

            else
            {

                if (PermissionsOutBound.UserProfile == "ad" || PermissionsOutBound.UserProfile == "sa")
                {
                    PermissionsOutboundMaster mobj_PermissionsOutbound = new PermissionsOutboundMaster();
                    mobj_PermissionsOutbound.Id = PermissionsOutBound.id;

                    PermissionsOutboundMaster _PermissionsOutbound = _IPermissionsOutboundService.GetPermissionsOutboundById(mobj_PermissionsOutbound);

                    _PermissionsOutbound.LicenseeID = PermissionsOutBound.LicenseeID;

                    _PermissionsOutbound.Licenseecode = PermissionsOutBound.Licenseecode;

                    _PermissionsOutbound.OrganizationName = PermissionsOutBound.OrganizationName;

                    _PermissionsOutbound.ContactPerson = PermissionsOutBound.ContactPerson;

                    _PermissionsOutbound.Address = PermissionsOutBound.Address;

                    _PermissionsOutbound.CountryId = PermissionsOutBound.CountryId;

                    _PermissionsOutbound.Stateid = PermissionsOutBound.Stateid;

                    _PermissionsOutbound.Cityid = PermissionsOutBound.Cityid;

                    _PermissionsOutbound.Pincode = PermissionsOutBound.Pincode;

                    _PermissionsOutbound.Mobile = PermissionsOutBound.Mobile;

                    _PermissionsOutbound.Email = PermissionsOutBound.Email;

                    _PermissionsOutbound.URL = PermissionsOutBound.URL;

                    _PermissionsOutbound.RequestDate = PermissionsOutBound.RequestDate;

                    _PermissionsOutbound.LicenseePublicationTitle = PermissionsOutBound.LicenseePublicationTitle;

                 //   _PermissionsOutbound.DateOfPermission = PermissionsOutBound.DateOfPermission;

                   // _PermissionsOutbound.PermissionPeriod = PermissionsOutBound.PermissionPeriod;

                 //   _PermissionsOutbound.DateExpiry = PermissionsOutBound.DateExpiry;

                    _PermissionsOutbound.RequestMaterial = PermissionsOutBound.RequestMaterial;

                    _PermissionsOutbound.Will_be_material_be_translated = PermissionsOutBound.Will_be_material_be_translated;

                    _PermissionsOutbound.Will_be_material_be_adepted = PermissionsOutBound.Will_be_material_be_adepted;

                    _PermissionsOutbound.LanguageId = PermissionsOutBound.LanguageId;

                    _PermissionsOutbound.Extent = PermissionsOutBound.Extent;

                    _PermissionsOutbound.TerritoryId = PermissionsOutBound.TerritoryId;

                    _PermissionsOutbound.DateOfInvoice = PermissionsOutBound.DateOfInvoice;

                    _PermissionsOutbound.InvoiceApplicable = PermissionsOutBound.InvoiceApplicable;

                    _PermissionsOutbound.InvoiceNo = PermissionsOutBound.InvoiceNo;

                    _PermissionsOutbound.InvoiceCurrency = PermissionsOutBound.InvoiceCurrency;

                    _PermissionsOutbound.InvoiceValue = PermissionsOutBound.InvoiceValue;

                    _PermissionsOutbound.InvoiceDescription = PermissionsOutBound.InvoiceDescription;

                    _PermissionsOutbound.Copies_To_Be_Received = PermissionsOutBound.Copies_To_Be_Received;

                    _PermissionsOutbound.NumberOfCopies = PermissionsOutBound.NumberOfCopies;

                    _PermissionsOutbound.productid = PermissionsOutBound.productid;
                    _PermissionsOutbound.ContactId = PermissionsOutBound.ContactId;

                    _PermissionsOutbound.Type = PermissionsOutBound.Type;
                    _PermissionsOutbound.Remarks = PermissionsOutBound.Remarks;

                    _IPermissionsOutboundService.UpdatePermissionsOutbound(_PermissionsOutbound);


                    if (PermissionsOutBound.SupplyTypeOfRights != null)
                    {

                        _IPermissionsOutboundService.DeavtivatePermissionsOutboundTypeOfRights(PermissionsOutBound.id, PermissionsOutBound.EnteredBy);


                        int i = 0;
                        foreach (var item in PermissionsOutBound.SupplyTypeOfRights)
                        {
                            PermissionsOutboundTypeOfRightsMaster obj_PermissionsOutboundTypeOfRights = new PermissionsOutboundTypeOfRightsMaster();
                            obj_PermissionsOutboundTypeOfRights.PermissionsOutboundId = _PermissionsOutbound.Id;
                            obj_PermissionsOutboundTypeOfRights.TypeofRightsId = item.TypeofRightsId;
                            obj_PermissionsOutboundTypeOfRights.Quantity = item.Quantity;
                            obj_PermissionsOutboundTypeOfRights.EnteredBy = PermissionsOutBound.EnteredBy;
                            obj_PermissionsOutboundTypeOfRights.Deactivate = "N";
                            obj_PermissionsOutboundTypeOfRights.EntryDate = DateTime.Now;
                            _IPermissionsOutboundService.InsertPermissionsOutboundTypeOfRightsLinking(obj_PermissionsOutboundTypeOfRights);
                            i++;

                        }
                    }



                    if (PermissionsOutBound.Language != null)
                    {
                        _IPermissionsOutboundService.DeletePermissionsOutboundLanguageLink(_PermissionsOutbound.Id, PermissionsOutBound.EnteredBy);



                        PermissionsOutboundLanguageMaster PermissionsOutboundLanguageLink = new PermissionsOutboundLanguageMaster();

                        int k = 0;

                        foreach (var item in PermissionsOutBound.Language)
                        {
                            PermissionsOutboundLanguageLink.PermissionsOutboundId = _PermissionsOutbound.Id;
                            PermissionsOutboundLanguageLink.languageId = item;
                            PermissionsOutboundLanguageLink.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundLanguageLink(PermissionsOutboundLanguageLink);
                            k++;
                        }
                    }



                    PermissionsOutboundUpdate mobj_PermissionsOutboundUpdate = new PermissionsOutboundUpdate();
                    mobj_PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;

                    PermissionsOutboundUpdate _PermissionsOutboundUpdate = _IPermissionsOutboundService.GetPermissionsOutboundUpdateById(mobj_PermissionsOutboundUpdate);


                    if (_PermissionsOutboundUpdate == null)
                    {
                        PermissionsOutboundUpdate _mobj_PermissionsOutboundUpdate = new PermissionsOutboundUpdate();
                        _mobj_PermissionsOutboundUpdate.ContractStatus = PermissionsOutBound.ContractStatus;
                        _mobj_PermissionsOutboundUpdate.PaymentAmount = PermissionsOutBound.PaymentAmount;
                        _mobj_PermissionsOutboundUpdate.CurrencyId = PermissionsOutBound.CurrencyId;
                        _mobj_PermissionsOutboundUpdate.Date_of_agreement = PermissionsOutBound.Date_of_agreement;
                        _mobj_PermissionsOutboundUpdate.Signed_Contract_sent_date = PermissionsOutBound.Signed_Contract_sent_date;
                        _mobj_PermissionsOutboundUpdate.Signed_Contract_receiveddate = PermissionsOutBound.Signed_Contract_receiveddate;
                        _mobj_PermissionsOutboundUpdate.CancellationDate = PermissionsOutBound.CancellationDate;
                        _mobj_PermissionsOutboundUpdate.Cancellation_Reason = PermissionsOutBound.Cancellation_Reason;
                        _mobj_PermissionsOutboundUpdate.Contributor_Agreement = PermissionsOutBound.Contributor_Agreement;
                        _mobj_PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;
                        _mobj_PermissionsOutboundUpdate.PendingRemarks = PermissionsOutBound.PendingRemarks;
                        _mobj_PermissionsOutboundUpdate.EnteredBy = PermissionsOutBound.EnteredBy;
                        _mobj_PermissionsOutboundUpdate.PaymentReceived = PermissionsOutBound.PaymentReceived;

                        _mobj_PermissionsOutboundUpdate.Effectivedate = PermissionsOutBound.Effectivedate;

                        _mobj_PermissionsOutboundUpdate.Contractperiodinmonth = PermissionsOutBound.Contractperiodinmonth;

                        _mobj_PermissionsOutboundUpdate.Expirydate = PermissionsOutBound.DateExpiry;


                        int PermissionsOutboundUpdateId = _IPermissionsOutboundService.InsertPermissionsOutboundUpdate(_mobj_PermissionsOutboundUpdate);


                        PermissionsOutboundDocument _PermissionsOutboundDocument = new PermissionsOutboundDocument();

                        string[] docurl = PermissionsOutBound.DocumentFile.Split(',');
                        int j = 0;
                        foreach (string doc in PermissionsOutBound.Documentname)
                        {
                            PermissionsOutboundDocument Link = new PermissionsOutboundDocument();
                            Link.PermissionsOutboundUpdateId = PermissionsOutboundUpdateId;
                            Link.Documentname = doc;
                            Link.DocumentFile = docurl[j];
                            Link.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundDocument(Link);
                            j++;
                        }
                    }
                    else
                    {
                        _PermissionsOutboundUpdate.ContractStatus = PermissionsOutBound.ContractStatus;
                        _PermissionsOutboundUpdate.PaymentAmount = PermissionsOutBound.PaymentAmount;
                        _PermissionsOutboundUpdate.CurrencyId = PermissionsOutBound.CurrencyId;
                        _PermissionsOutboundUpdate.Date_of_agreement = PermissionsOutBound.Date_of_agreement;
                        _PermissionsOutboundUpdate.Signed_Contract_sent_date = PermissionsOutBound.Signed_Contract_sent_date;
                        _PermissionsOutboundUpdate.Signed_Contract_receiveddate = PermissionsOutBound.Signed_Contract_receiveddate;
                        _PermissionsOutboundUpdate.CancellationDate = PermissionsOutBound.CancellationDate;
                        _PermissionsOutboundUpdate.Cancellation_Reason = PermissionsOutBound.Cancellation_Reason;
                        _PermissionsOutboundUpdate.Contributor_Agreement = PermissionsOutBound.Contributor_Agreement;
                        _PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;
                        _PermissionsOutboundUpdate.PendingRemarks = PermissionsOutBound.PendingRemarks;
                        _PermissionsOutboundUpdate.ModifiedBy = PermissionsOutBound.EnteredBy;
                        _PermissionsOutboundUpdate.PaymentReceived = PermissionsOutBound.PaymentReceived;


                        _PermissionsOutboundUpdate.Effectivedate = PermissionsOutBound.Effectivedate;

                        _PermissionsOutboundUpdate.Contractperiodinmonth = PermissionsOutBound.Contractperiodinmonth;

                        _PermissionsOutboundUpdate.Expirydate = PermissionsOutBound.DateExpiry;


                        _IPermissionsOutboundService.UpdatePermissionsOutboundUpdate(_PermissionsOutboundUpdate);

                        string[] docurl = PermissionsOutBound.DocumentFile.Split(',');
                        int j = 0;
                        foreach (string doc in PermissionsOutBound.Documentname)
                        {
                            PermissionsOutboundDocument Link = new PermissionsOutboundDocument();
                            Link.PermissionsOutboundUpdateId = _PermissionsOutboundUpdate.Id;
                            Link.Documentname = doc;
                            Link.DocumentFile = docurl[j];
                            Link.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundDocument(Link);
                            j++;
                        }

                    }
                   
                }
                else if (PermissionsOutBound.UserProfile == "rt")
                {
                    //PermissionsOutboundUpdate _mobj_PermissionsOutboundUpdate = new PermissionsOutboundUpdate();
                    //_mobj_PermissionsOutboundUpdate.ContractStatus = PermissionsOutBound.ContractStatus;
                    //_mobj_PermissionsOutboundUpdate.PaymentAmount = PermissionsOutBound.PaymentAmount;
                    //_mobj_PermissionsOutboundUpdate.CurrencyId = PermissionsOutBound.CurrencyId;
                    //_mobj_PermissionsOutboundUpdate.Date_of_agreement = PermissionsOutBound.Date_of_agreement;
                    //_mobj_PermissionsOutboundUpdate.Signed_Contract_sent_date = PermissionsOutBound.Signed_Contract_sent_date;
                    //_mobj_PermissionsOutboundUpdate.Signed_Contract_receiveddate = PermissionsOutBound.Signed_Contract_receiveddate;
                    //_mobj_PermissionsOutboundUpdate.CancellationDate = PermissionsOutBound.CancellationDate;
                    //_mobj_PermissionsOutboundUpdate.Cancellation_Reason = PermissionsOutBound.Cancellation_Reason;
                    //_mobj_PermissionsOutboundUpdate.Contributor_Agreement = PermissionsOutBound.Contributor_Agreement;
                    //_mobj_PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;
                    //_mobj_PermissionsOutboundUpdate.PendingRemarks = PermissionsOutBound.PendingRemarks;
                    //_mobj_PermissionsOutboundUpdate.EnteredBy = PermissionsOutBound.EnteredBy;
                    //_mobj_PermissionsOutboundUpdate.PaymentReceived = PermissionsOutBound.PaymentReceived;

                    //_mobj_PermissionsOutboundUpdate.Effectivedate = PermissionsOutBound.Effectivedate;

                    //_mobj_PermissionsOutboundUpdate.Contractperiodinmonth = PermissionsOutBound.Contractperiodinmonth;

                    //_mobj_PermissionsOutboundUpdate.Expirydate = PermissionsOutBound.DateExpiry;

                    //int PermissionsOutboundUpdateId = _IPermissionsOutboundService.InsertPermissionsOutboundUpdate(_mobj_PermissionsOutboundUpdate);


                    //PermissionsOutboundDocument _PermissionsOutboundDocument = new PermissionsOutboundDocument();

                    //string[] docurl = PermissionsOutBound.DocumentFile.Split(',');
                    //int j = 0;
                    //foreach (string doc in PermissionsOutBound.Documentname)
                    //{
                    //    PermissionsOutboundDocument Link = new PermissionsOutboundDocument();
                    //    Link.PermissionsOutboundUpdateId = PermissionsOutboundUpdateId;
                    //    Link.Documentname = doc;
                    //    Link.DocumentFile = docurl[j];
                    //    Link.EnteredBy = PermissionsOutBound.EnteredBy;
                    //    _IPermissionsOutboundService.InsertPermissionsOutboundDocument(Link);
                    //    j++;
                    //}


                    PermissionsOutboundUpdate mobj_PermissionsOutboundUpdate = new PermissionsOutboundUpdate();
                    mobj_PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;

                    PermissionsOutboundUpdate _PermissionsOutboundUpdate = _IPermissionsOutboundService.GetPermissionsOutboundUpdateById(mobj_PermissionsOutboundUpdate);


                    if (_PermissionsOutboundUpdate == null)
                    {
                        PermissionsOutboundUpdate _mobj_PermissionsOutboundUpdate = new PermissionsOutboundUpdate();
                        _mobj_PermissionsOutboundUpdate.ContractStatus = PermissionsOutBound.ContractStatus;
                        _mobj_PermissionsOutboundUpdate.PaymentAmount = PermissionsOutBound.PaymentAmount;
                        _mobj_PermissionsOutboundUpdate.CurrencyId = PermissionsOutBound.CurrencyId;
                        _mobj_PermissionsOutboundUpdate.Date_of_agreement = PermissionsOutBound.Date_of_agreement;
                        _mobj_PermissionsOutboundUpdate.Signed_Contract_sent_date = PermissionsOutBound.Signed_Contract_sent_date;
                        _mobj_PermissionsOutboundUpdate.Signed_Contract_receiveddate = PermissionsOutBound.Signed_Contract_receiveddate;
                        _mobj_PermissionsOutboundUpdate.CancellationDate = PermissionsOutBound.CancellationDate;
                        _mobj_PermissionsOutboundUpdate.Cancellation_Reason = PermissionsOutBound.Cancellation_Reason;
                        _mobj_PermissionsOutboundUpdate.Contributor_Agreement = PermissionsOutBound.Contributor_Agreement;
                        _mobj_PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;
                        _mobj_PermissionsOutboundUpdate.PendingRemarks = PermissionsOutBound.PendingRemarks;
                        _mobj_PermissionsOutboundUpdate.EnteredBy = PermissionsOutBound.EnteredBy;
                        _mobj_PermissionsOutboundUpdate.PaymentReceived = PermissionsOutBound.PaymentReceived;

                        _mobj_PermissionsOutboundUpdate.Effectivedate = PermissionsOutBound.Effectivedate;

                        _mobj_PermissionsOutboundUpdate.Contractperiodinmonth = PermissionsOutBound.Contractperiodinmonth;

                        _mobj_PermissionsOutboundUpdate.Expirydate = PermissionsOutBound.DateExpiry;


                        int PermissionsOutboundUpdateId = _IPermissionsOutboundService.InsertPermissionsOutboundUpdate(_mobj_PermissionsOutboundUpdate);


                        PermissionsOutboundDocument _PermissionsOutboundDocument = new PermissionsOutboundDocument();

                        string[] docurl = PermissionsOutBound.DocumentFile.Split(',');
                        int j = 0;
                        foreach (string doc in PermissionsOutBound.Documentname)
                        {
                            PermissionsOutboundDocument Link = new PermissionsOutboundDocument();
                            Link.PermissionsOutboundUpdateId = PermissionsOutboundUpdateId;
                            Link.Documentname = doc;
                            Link.DocumentFile = docurl[j];
                            Link.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundDocument(Link);
                            j++;
                        }
                    }
                    else
                    {
                        _PermissionsOutboundUpdate.ContractStatus = PermissionsOutBound.ContractStatus;
                        _PermissionsOutboundUpdate.PaymentAmount = PermissionsOutBound.PaymentAmount;
                        _PermissionsOutboundUpdate.CurrencyId = PermissionsOutBound.CurrencyId;
                        _PermissionsOutboundUpdate.Date_of_agreement = PermissionsOutBound.Date_of_agreement;
                        _PermissionsOutboundUpdate.Signed_Contract_sent_date = PermissionsOutBound.Signed_Contract_sent_date;
                        _PermissionsOutboundUpdate.Signed_Contract_receiveddate = PermissionsOutBound.Signed_Contract_receiveddate;
                        _PermissionsOutboundUpdate.CancellationDate = PermissionsOutBound.CancellationDate;
                        _PermissionsOutboundUpdate.Cancellation_Reason = PermissionsOutBound.Cancellation_Reason;
                        _PermissionsOutboundUpdate.Contributor_Agreement = PermissionsOutBound.Contributor_Agreement;
                        _PermissionsOutboundUpdate.PermissionsOutboundID = PermissionsOutBound.id;
                        _PermissionsOutboundUpdate.PendingRemarks = PermissionsOutBound.PendingRemarks;
                        _PermissionsOutboundUpdate.ModifiedBy = PermissionsOutBound.EnteredBy;
                        _PermissionsOutboundUpdate.PaymentReceived = PermissionsOutBound.PaymentReceived;


                        _PermissionsOutboundUpdate.Effectivedate = PermissionsOutBound.Effectivedate;

                        _PermissionsOutboundUpdate.Contractperiodinmonth = PermissionsOutBound.Contractperiodinmonth;

                        _PermissionsOutboundUpdate.Expirydate = PermissionsOutBound.DateExpiry;


                        _IPermissionsOutboundService.UpdatePermissionsOutboundUpdate(_PermissionsOutboundUpdate);

                        string[] docurl = PermissionsOutBound.DocumentFile.Split(',');
                        int j = 0;
                        foreach (string doc in PermissionsOutBound.Documentname)
                        {
                            PermissionsOutboundDocument Link = new PermissionsOutboundDocument();
                            Link.PermissionsOutboundUpdateId = _PermissionsOutboundUpdate.Id;
                            Link.Documentname = doc;
                            Link.DocumentFile = docurl[j];
                            Link.EnteredBy = PermissionsOutBound.EnteredBy;
                            _IPermissionsOutboundService.InsertPermissionsOutboundDocument(Link);
                            j++;
                        }

                    }

                }
                

            }
            status = _localizationService.GetResource("Master.API.Success.Message");
            return Json(SerializeObj.SerializeObject(new { status, PermissionsOutbound_CodeValue, PermissionsOutboundIdId }));
        }




        public IHttpActionResult PermissionsOutBoundDetails(PermissionsoutboundDetials PermissionsOutBound)
        {

          

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("PermissionsoutboundId", SqlDbType.VarChar, 50);
            if (PermissionsOutBound.PermissionsoutboundId == null)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = PermissionsOutBound.PermissionsoutboundId;
            }




            var _GetPermissionsOutBound = _dbContext.ExecuteStoredProcedureListNewData<PermissionsoutboundDetials>("Proc_PermissionsoutboundDetails_get", parameters).ToList();


            //var _OutboundTypeOfRightsMaster = _PermissionsOutboundTypeOfRightsMaster.Table.Where(a => a.Deactivate == "N" && a.PermissionsOutboundId == PermissionsOutBound.PermissionsoutboundId).ToList()
            //                                    .Select(PTEM => new {
            //                                        PermissionsOutboundId = PTEM.PermissionsOutboundId,
            //                                        TypeofRights = PTEM.TypeOfRights.TypeOfRights,
            //                                        Quantity = PTEM.Quantity,
            //                                        TypeofRightsId = PTEM.TypeofRightsId
            //                                        ///SubTypeRights = T.SubTypeRights,
            //                                    }).ToList();


            var _OutboundTypeOfRightsMaster = (from PTRM in _PermissionsOutboundTypeOfRightsMaster.Table.Where(a => a.Deactivate == "N" && a.PermissionsOutboundId == PermissionsOutBound.PermissionsoutboundId)
                                               join TRM in _objTypeOfRightsRepository.Table.Where(a => a.Deactivate == "N")
                                                 on PTRM.TypeofRightsId equals TRM.Id

                                               select new
                                               {
                                                   PermissionsOutboundId = PTRM.PermissionsOutboundId,
                                                   TypeofRights = PTRM.TypeOfRights.TypeOfRights,
                                                   Quantity = PTRM.Quantity,
                                                   TypeofRightsId = PTRM.TypeofRightsId,
                                                   SubTypeRights = TRM.SubTypeRights
                                               }

               ).ToList();
           /// return Json(ContractPartyType);
          

            
           


              PermissionsOutboundUpdate mobj_PermissionsOutboundUpdate = _PermissionsOutboundUpdate.Table.Where(a => a.Deactivate == "N" && a.PermissionsOutboundID == PermissionsOutBound.PermissionsoutboundId).FirstOrDefault();
            
            PermissionsoutboundDetials PermissionsoutboundDetialsDocuments = new PermissionsoutboundDetials();

              if (mobj_PermissionsOutboundUpdate != null)
              {
                

                  var documents = _PermissionsOutboundDocument.Table.Where(a => a.PermissionsOutboundUpdateId == mobj_PermissionsOutboundUpdate.Id && a.Deactivate == "N").ToList();


                  PermissionsoutboundDetialsDocuments.DocumentIds = documents.Select(i => i.Id).ToArray();


                  PermissionsoutboundDetialsDocuments.Documentname = documents.Select(i => i.Documentname).ToArray();
                  foreach (var docs in documents)
                      PermissionsoutboundDetialsDocuments.DocumentFile = PermissionsoutboundDetialsDocuments.DocumentFile + docs.DocumentFile + ",";


              
              }

            



              SqlParameter[] parameters2 = new SqlParameter[1];

              parameters2[0] = new SqlParameter("PermissionsoutboundId", SqlDbType.VarChar, 50);
              if (PermissionsOutBound.PermissionsoutboundId == null)
              {
                  parameters2[0].Value = System.Data.SqlTypes.SqlInt32.Null;
              }
              else
              {
                  parameters2[0].Value = PermissionsOutBound.PermissionsoutboundId;
              }




              var _GetPermissionsOutBoundUpdate = _dbContext.ExecuteStoredProcedureListNewData<PermissionsoutboundDetials>("Proc_PermissionsoutboundUpdateDetails_get", parameters2).ToList();



              var mobj_language = (from POLM in _PermissionsOutboundLanguageMaster.Table.Where(a => a.Deactivate == "N")
                                   join LM in _LanguageMaster.Table.Where(a => a.Deactivate == "N")
                                   on POLM.languageId equals LM.Id
                                   select new
                                   {
                                       languageId = POLM.languageId,
                                       PermissionsOutboundId = POLM.PermissionsOutboundId,
                                       LanguageName = LM.LanguageName
                                   }
                                      ).Distinct().Where(a => a.PermissionsOutboundId == PermissionsOutBound.PermissionsoutboundId).ToList();




              return Json(SerializeObj.SerializeObject(new { _GetPermissionsOutBound, _OutboundTypeOfRightsMaster, mobj_PermissionsOutboundUpdate, PermissionsoutboundDetialsDocuments, _GetPermissionsOutBoundUpdate, mobj_language }));


            }





        public IHttpActionResult RemovePermissionsOutboundDocument(PermissionsoutboundDetials Dcoument)
        {
            PermissionsOutboundDocument document = _PermissionsOutboundDocument.Table.Where(x => x.Id == Dcoument.Id && x.Deactivate == "N").FirstOrDefault();

            string status = string.Empty;
            try
            {

                _IPermissionsOutboundService.DeavtivatePermissionsOutboundDocumentById(Dcoument.Id, Dcoument.EnteredBy);


                status = _localizationService.GetResource("Master.API.Success.Message");

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




        public IHttpActionResult PermissionsOutboundSearch(PermissionsOutboundSearchHistory SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _IPermissionsOutboundService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }
        }


        [HttpGet]
        public IHttpActionResult GetPermissionsOutboundList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionOutBoundSearch>("Proc_PermissionsOutboundSerch_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpGet]
        public IHttpActionResult GetPaymentTaggingSubSidiaryRights(String AuthorId = "", string AuthorContractId = "", string OutboundId = "")
        {
            try
            {
                if (AuthorId == "" || AuthorContractId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    int sln = 0;
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("AuthorId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + AuthorId + "'";
                    parameters[1] = new SqlParameter("AuthorContractId", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + AuthorContractId + "'";
                    parameters[2] = new SqlParameter("OutboundId", SqlDbType.VarChar, 200);
                    parameters[2].Value = "'" + OutboundId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_AuthorPaymentTaggingSubSidiaryRights_PO_get", parameters).ToList();
                    var _GetAuthorReport = _GetAuthorReport1.Select(i => new 
                                                                            { 
                                                                                sln = sln++, 
                                                                                authorcontractid = i.authorcontractid, 
                                                                                AuthorId = i.AuthorId,
                                                                                AuthorContractCode = i.AuthorContractCode, 
                                                                                AuthorName = i.AuthorName, 
                                                                                InvoiceNo = i.InvoiceNo == null ? "--" : i.InvoiceNo, 
                                                                                InvoiceValue = i.InvoiceValue == null ? "--" : i.InvoiceValue, 
                                                                                InvoiceCurrencySymbol = i.InvoiceCurrencySymbol == null ? null : i.InvoiceCurrencySymbol, 
                                                                                ISBN = i.ISBN == null ? "--" : i.ISBN,
                                                                                AuthorCode = i.AuthorCode == null ? "--" : i.AuthorCode,
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
        public IHttpActionResult GetPaymentTaggingSubSidiaryRightsByPublishingCompany(String PublishingCompanyId = "", string ProductLicenseId = "", string OutboundId = "")
        {
            try
            {
                if (PublishingCompanyId == "" && ProductLicenseId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    int sln = 0;
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("PublishingCompanyId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + PublishingCompanyId + "'";
                    parameters[1] = new SqlParameter("ProductLicenseId", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + ProductLicenseId + "'";
                    parameters[2] = new SqlParameter("OutboundId", SqlDbType.VarChar, 200);
                    parameters[2].Value = "'" + OutboundId + "'";
                    var _GetAuthorReport1 = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_PublishingCompany_PaymentTaggingPermissionOutbound_get", parameters).ToList();
                    var _GetAuthorReport = _GetAuthorReport1.Select(i => new 
                                                                            { 
                                                                                sln = sln++, 
                                                                                PublishingCompanyId = i.PublishingCompanyId, 
                                                                                ProductLicenseId = i.ProductLicenseId, 
                                                                                ProductLicensecode = i.ProductLicensecode, 
                                                                                PublishingCompanyName = i.PublishingCompanyName, 
                                                                                InvoiceNo = i.InvoiceNo == null ? "--" : i.InvoiceNo, 
                                                                                InvoiceValue = i.InvoiceValue == null ? "--" : i.InvoiceValue, 
                                                                                InvoiceCurrencySymbol = i.InvoiceCurrencySymbol == null ? null : i.InvoiceCurrencySymbol, 
                                                                                ISBN = i.ISBN == null ? "--" : i.ISBN,
                                                                                AuthorCode = i.AuthorCode == null ? "--" : i.AuthorCode,
                                                                                //SAPagreementNo = i.SAPagreementNo == null ? "--" : i.SAPagreementNo,
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



        [HttpGet]
        public IHttpActionResult SubSidiaryRightsByAuthorcontract(String authorcontractid)
        {
            try
            {
                if (authorcontractid == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("authorcontractid", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + authorcontractid + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_PaymentTaggingSubSidiaryRightsByAuthorcontract_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpGet]
        public IHttpActionResult SubSidiaryRightsByProductLicense(String ProductLicenseId)
        {
            try
            {
                if (ProductLicenseId == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("ProductLicenseId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + ProductLicenseId + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PaymentTagging>("Proc_PaymentTaggingSubSidiaryRightsByProductLicense_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        [HttpGet]
        public IHttpActionResult SubSidiaryRightsByPercentage(int subsidiaryrightsid, int AuthorContractId, int authorId = 0)
        {

            var mvarAuthorContractSubsidiaryRights = (from T in _AuthorContractSubsidiaryRights.Table.Where(a => a.Deactivate == "N" && 
                                                                                                            a.subsidiaryrightsid == subsidiaryrightsid && 
                                                                                                            a.AuthorContractid == AuthorContractId  &&
                                                                                                            a.AuthorId == authorId)
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          AuthorPercentage = T.AuthorPercentage,
                                                      }).Distinct().FirstOrDefault();

            return Json(mvarAuthorContractSubsidiaryRights);
        }

        //Add by Ankush On 17/08/2016

        public IHttpActionResult InsertPermissionsOutboundPaymentTagging(RightsSellingPaymentTaggingModelData _PaymentTaggingModelData)
        {
            try
            {
                foreach (RightsSellingPaymentTaggingModel _PaymentTaggingModel in _PaymentTaggingModelData.RightsSellingPaymentTagging)
                {
                    PermissionsOutboundPaymentTagging _PermissionsOutboundPaymentTagging = new PermissionsOutboundPaymentTagging();
                    _PermissionsOutboundPaymentTagging.Amount = _PaymentTaggingModel.Amount;
                    _PermissionsOutboundPaymentTagging.AuthorAmount = _PaymentTaggingModel.AuthorAmount;
                    _PermissionsOutboundPaymentTagging.BankName = _PaymentTaggingModel.BankName;
                    _PermissionsOutboundPaymentTagging.ChequeDate = _PaymentTaggingModel.ChequeDate;
                    _PermissionsOutboundPaymentTagging.ChequeNumber = _PaymentTaggingModel.ChequeNumber;
                    _PermissionsOutboundPaymentTagging.ContractId = _PaymentTaggingModel.ContractId;
                    _PermissionsOutboundPaymentTagging.OupAmount = _PaymentTaggingModel.OupAmount;
                    _PermissionsOutboundPaymentTagging.PaymentMode = _PaymentTaggingModel.PaymentMode;
                    _PermissionsOutboundPaymentTagging.Percentage = _PaymentTaggingModel.Percentage;
                    _PermissionsOutboundPaymentTagging.ProductLicenseId = _PaymentTaggingModel.ProductLicenseId;
                    _PermissionsOutboundPaymentTagging.subproducttypeid = _PaymentTaggingModel.subproducttypeid;

                    _PermissionsOutboundPaymentTagging.AuthorId = _PaymentTaggingModel.AuthorId;
                    _PermissionsOutboundPaymentTagging.PermissionsOutboundMasterId = _PaymentTaggingModel.PermissionsOutboundId;
                    _PermissionsOutboundPaymentTagging.PublishingCompanyId = _PaymentTaggingModel.PublishingCompanyId;

                    _PermissionsOutboundPaymentTagging.WithHoldingTax = _PaymentTaggingModel.WithHoldingTax;
                    _PermissionsOutboundPaymentTagging.ConverisonRate = _PaymentTaggingModel.ConverisonRate;

                    _PermissionsOutboundPaymentTagging.Deactivate = "N";
                    _PermissionsOutboundPaymentTagging.EnteredBy = _PaymentTaggingModel.EnteredBy;
                    _PermissionsOutboundPaymentTagging.EntryDate = DateTime.Now;
                    _IPermissionsOutboundService.InsertPermissionsOutboundPaymentTagging(_PermissionsOutboundPaymentTagging);

                }
                return Json("OK");
            }
            catch
            {
                return Json("");
            }
        }

        [HttpGet]
        public IHttpActionResult SubSidiaryRightsByPercentageForProductLicense(int subsidiaryrightsid, int ProductLicenseid)
        {

            var mvar_ProductLicenseSubsidiaryRights = (from T in _ProductLicenseSubsidiaryRights.Table.Where(a => a.Deactivate == "N" && a.subsidiaryrightsid == subsidiaryrightsid && a.ProductLicenseid == ProductLicenseid)
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          AuthorPercentage = T.publisherpercentage,
                                                      }).Distinct().FirstOrDefault();

            return Json(mvar_ProductLicenseSubsidiaryRights);
        }



        public IHttpActionResult getPermissionsLanguageList(int Id)
        {



            var mobj_LanguageDetail = (from POLM in _PermissionsOutboundLanguageMaster.Table.Where(a => a.Deactivate == "N")
                                       join LM in _LanguageMaster.Table.Where(a => a.Deactivate == "N")
                                       on POLM.languageId equals LM.Id
                                       select new
                                       {
                                           LanguageName = LM.LanguageName,
                                           PermissionsOutboundId = POLM.PermissionsOutboundId
                                       }


                                          ).ToList().Distinct().Where(a => a.PermissionsOutboundId == Id);


            return (Json(mobj_LanguageDetail));
           


        }



        public IHttpActionResult TopSearch(String Code)
        {

            PermissionsOutboundMaster PermissionsOutboundMaster = _PermissionsOutboundMaster.Table.Where(a => a.PermissionsOutboundCode == Code && a.Deactivate == "N").FirstOrDefault();

            if (PermissionsOutboundMaster != null)
            {

                var _PermissionsOutboundMasterValue = new
                    {
                        Id = PermissionsOutboundMaster.Id,
                        CommeId =  PermissionsOutboundMaster.ContactId,
                        ProuductId = PermissionsOutboundMaster.Type + PermissionsOutboundMaster.productid

                       
                    };

                return Json(new { _PermissionsOutboundMasterValue });

               


            }
            else
            {
                string _PermissionsOutboundMasterValue = string.Empty;
                return Json(new { _PermissionsOutboundMasterValue });
            }

        }

        //added by prakash on 29 june, 2017
        [HttpGet]
        public IHttpActionResult GetPermissionsOutboundMasterDetailById(string OutboundId)
        {
            try
            {
                var data = _IPermissionsOutboundService.getAllPermissionsOutboundMasterList().Where(x => x.Id == Convert.ToInt32(OutboundId)).Select(a => a.PermissionsOutboundCode).SingleOrDefault();
                return Json(data);
            }
            catch { return Json(""); }
        }

        /* Create By  : Prakash
       * Create on  : 20 Sep, 2017
       * Create for : Delete Permissions Outbound
       */
        [HttpPost]
        public IHttpActionResult DeletePermissionsOutboundSet(PermissionsOutboundMaster mobj_delete)
        {
            string status = string.Empty;

            try
            {
                if (mobj_delete.Id != 0)
                {
                    PermissionsOutboundMaster _PermissionsOutboundMaster = _IPermissionsOutboundService.GetPermissionsOutboundById(mobj_delete);
                    _PermissionsOutboundMaster.Deactivate = "Y";
                    _PermissionsOutboundMaster.DeactivateBy = mobj_delete.DeactivateBy;
                    _PermissionsOutboundMaster.DeactivateDate = DateTime.Now;

                    _IPermissionsOutboundService.DeletePermissionsOutbound(_PermissionsOutboundMaster);
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
       Created For   :   Fetching the details of PermissionsOutbound Detail
 */

        public IHttpActionResult PermissionsOutbound_Detail(PaymentTaggingList PaymentTagging)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (PaymentTagging != null)
                {
                    parameters[0] = new SqlParameter("PermissionsOutboundId", SqlDbType.VarChar, 50);
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
            var _GetPermissionsOutboundList = _dbContext.ExecuteStoredProcedureListNewData<PaymentTaggingList>("Proc_PermissionsOutbound_Detail_get", parameters).ToList();


            return Json(_GetPermissionsOutboundList);
        }


        /** Create By    :   Saddam
        Create On    :   20/10/2017
         Created For   :   Fetching the details of OUP Percentage
*/
        [HttpGet]
        public IHttpActionResult OUPPercentageDettails(int subsidiaryrightsid, int AuthorContractId)
        {
            var mvarAuthorContractSubsidiaryRights = (from T in _AuthorContractSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                          a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                          a.AuthorContractid == AuthorContractId
                                                                                                        )
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          ouppercentage = T.ouppercentage,
                                                      }).Distinct().FirstOrDefault();

            return Json(mvarAuthorContractSubsidiaryRights);
        }


        /** Create By    :   Saddam
     Create On    :   20/10/2017
      Created For   :   Fetching the details of OUP Percentage
*/
        [HttpGet]
        public IHttpActionResult OUPAuthorPercentageDettails(int subsidiaryrightsid, int AuthorContractId, int AuthorId)
        {
            var mvarAuthorContractSubsidiaryRights = (from T in _AuthorContractSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                          a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                          a.AuthorContractid == AuthorContractId &&
                                                                                                          a.AuthorId == AuthorId
                                                                                                          
                                                                                                        )
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          AuthorPercentage = T.AuthorPercentage,
                                                      }).Distinct().FirstOrDefault();





            return Json(mvarAuthorContractSubsidiaryRights);
        }


        /** Create By    :   Saddam
      Create On    :   20/10/2017
       Created For   :   Fetching the details of OUP Percentage or Product License
*/
        [HttpGet]
        public IHttpActionResult OUPPercentageProductLicenseDettails(int subsidiaryrightsid, int ProductLicenseid)
        {
            var mobj_ProductLicenseSubsidiaryRights = (from T in _ProductLicenseSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                          a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                          a.ProductLicenseid == ProductLicenseid
                                                                                                        )
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          ouppercentage = T.ouppercentage,
                                                      }).Distinct().FirstOrDefault();

            return Json(mobj_ProductLicenseSubsidiaryRights);
        }



        /** Create By    :   Saddam
      Create On    :   20/10/2017
       Created For   :   Fetching the details of OUP Percentage or Product License
*/
        [HttpGet]
        public IHttpActionResult OUPProductLicensePercentageDettails(int subsidiaryrightsid, int ProductLicenseid)
        {
            var mobj_ProductLicenseSubsidiaryRights = (from T in _ProductLicenseSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                          a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                          a.ProductLicenseid == ProductLicenseid
                                                                                                        )
                                                       select new
                                                       {
                                                           Id = T.Id,
                                                           publisherpercentage = T.publisherpercentage,
                                                       }).Distinct().FirstOrDefault();

            return Json(mobj_ProductLicenseSubsidiaryRights);
        }



        /** Create By    :   Saddam
   Create On    :   20/10/2017
    Created For   :   Fetching the details of OUP Percentage
*/
        [HttpGet]
        public IHttpActionResult OUPPermssionOutBoundAuthorPercentageDettails(int subsidiaryrightsid, int AuthorContractId, int OutboundId)
        {
            var mvarAuthorContractSubsidiaryRights = (from T in _AuthorContractSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                            a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                            a.AuthorContractid == AuthorContractId
                                                                                                          )
                                                      select new
                                                      {
                                                          Id = T.Id,
                                                          ouppercentage = T.ouppercentage,
                                                          InvoiceValue = _PermissionsOutboundMaster.Table.Where(a => a.Id == OutboundId && a.Deactivate == "N").FirstOrDefault().InvoiceValue,
                                                      }).Distinct().FirstOrDefault();

            return Json(mvarAuthorContractSubsidiaryRights);

        }



        /** Create By    :   Saddam
Create On    :   20/10/2017
Created For   :   Fetching the details of OUP Percentage or Product License
*/
        [HttpGet]
        public IHttpActionResult OUPPermssionOutBoundPercentageProductLicenseDettails(int subsidiaryrightsid, int ProductLicenseid, int OutboundId)
        {
            var mobj_ProductLicenseSubsidiaryRights = (from T in _ProductLicenseSubsidiaryRights.Table.Where(a => a.Deactivate == "N" &&
                                                                                                          a.subsidiaryrightsid == subsidiaryrightsid &&
                                                                                                          a.ProductLicenseid == ProductLicenseid
                                                                                                        )
                                                       select new
                                                       {
                                                           Id = T.Id,
                                                           ouppercentage = T.ouppercentage,
                                                           InvoiceValue = _PermissionsOutboundMaster.Table.Where(a => a.Id == OutboundId && a.Deactivate == "N").FirstOrDefault().InvoiceValue,
                                                       }).Distinct().FirstOrDefault();

            return Json(mobj_ProductLicenseSubsidiaryRights);
        }

        }
    }
