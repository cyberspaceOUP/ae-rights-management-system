//Added by Saddam on 14/06/2016
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
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Services.User;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using System.Text;



using ACS.Core;
using ACS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SLV.API.Controllers.Other_Contract
{
    public class OtherContractController : ApiController
    {
        private readonly IOtherContractService _OtherContractService;
        private readonly IApplicationSetUpService _ApplicationSetUpService;
        private readonly ICommonListService _CommonListService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IRepository<OtherContractImageBank> _OtherContractImageBank;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IRepository<OtherContractMaster> _OtherContractMaster;
        private readonly IRepository<OtherContractDocuments> _OtherContractDocuments;
        private readonly IRepository<ContractType> _ContractType;
        private readonly IRepository<CurrencyMaster> _CurrencyMaster;
        private readonly IRepository<OtherContractLink> _OtherContractLink;
        private readonly IRepository<OtherContractLinkDocument> _OtherContractLinkDocument;
        private readonly IRepository<OtherContractDivisionLink> _OtherContractDivisionLink;
        private readonly IRepository<DivisionMaster> _DivisionMaster;
        private readonly IRepository<VideoImageBank> _VideoImageBank;
        public OtherContractController(

             IOtherContractService OtherContractService
          , ILocalizationService localizationService
            , ICommonListService CommonListService
                , IDbContext dbContext
            , IRepository<OtherContractImageBank> OtherContractImageBank
            , IRepository<ApplicationSetUp> ApplicationSetUp
            ,IApplicationSetUpService ApplicationSetUpService
            , IRepository<OtherContractMaster> OtherContractMaster
            , IRepository<OtherContractDocuments> OtherContractDocuments
              , IRepository<ContractType> ContractType
             , IRepository<OtherContractLink> OtherContractLink
             , IRepository<CurrencyMaster> CurrencyMaster
                , IRepository<OtherContractLinkDocument> OtherContractLinkDocument
            , IRepository<OtherContractDivisionLink> OtherContractDivisionLink
            , IRepository<DivisionMaster> DivisionMaster
            , IRepository<VideoImageBank> VideoImageBank
            )
        {
            _OtherContractImageBank = OtherContractImageBank;
            _localizationService = localizationService;
            _OtherContractService = OtherContractService;
            _CommonListService = CommonListService;
            _ApplicationSetUp = ApplicationSetUp;
        _ApplicationSetUpService = ApplicationSetUpService;
        _OtherContractMaster = OtherContractMaster;
        _OtherContractDocuments = OtherContractDocuments;
        _ContractType = ContractType;
        _CurrencyMaster = CurrencyMaster;
        _OtherContractLink = OtherContractLink;
        _OtherContractLinkDocument = OtherContractLinkDocument;
            this._dbContext = dbContext;
            this._OtherContractDivisionLink = OtherContractDivisionLink;
            this._DivisionMaster = DivisionMaster;
            this._VideoImageBank = VideoImageBank;
        }
        public IHttpActionResult InsertOtherContract(Contract_Signed_By_Executive OtherContract)
        {

            string status = "";
            int OtherContractIdId = 0;
            string OtherContactCode = string.Empty;
            try
            {
                //if (status == "Y")
                //{
                if (OtherContract.Id == 0)
                {

                    IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "Other Contract" && x.Deactivate == "N").ToList();
                    var OtherContractSuggesation = _ApplicationSetUpList.Select(Au => new
                    {
                        OtherContractValue = Au.keyValue,
                        Id = Au.Id
                    });

                    IList<ContractType> _ContractTypeList = _ContractType.Table.Where(x => x.Id == OtherContract.Contracttypeid && x.Deactivate == "N").ToList();
                    var ContractType = _ContractTypeList.Select(CT => new
                    {
                        ContractName = CT.ContractName

                    });

                    OtherContractMaster _OtherContract = new OtherContractMaster();

                   
                    string stringTokens = ContractType.FirstOrDefault().ContractName;
                    string strVlaue = string.Empty;
                    string[] words;
                    if (stringTokens.Contains("-"))
                    {
                        words = stringTokens.Split('-');
                    }
                    else
                    {
                        words = stringTokens.Split(' ');
                    }

                   
                    foreach (string word in words)
                    {
                     
                        strVlaue +=  word.FirstOrDefault();
                    }

                    _OtherContract.othercontractcode = "OC" + strVlaue.ToString() + OtherContractSuggesation.FirstOrDefault().OtherContractValue;
                    _OtherContract.othercontractcode = _OtherContract.othercontractcode.ToString().ToUpper();
                    OtherContactCode = _OtherContract.othercontractcode.ToString().ToUpper();

                    _OtherContract.partyname = OtherContract.partyname;
                    _OtherContract.natureofserviceid = OtherContract.natureofserviceid;
                    _OtherContract.natureofsubserviceid = OtherContract.natureofsubserviceid;
                    _OtherContract.Address = OtherContract.Address;
                    _OtherContract.CountryId = OtherContract.CountryId;
                    _OtherContract.OtherCountry = OtherContract.OtherCountry;
                    _OtherContract.Stateid = OtherContract.Stateid;
                    _OtherContract.OtherState = OtherContract.OtherState;
                    _OtherContract.Cityid = OtherContract.Cityid;
                    _OtherContract.OtherCity = OtherContract.OtherCity;
                    _OtherContract.Pincode = OtherContract.Pincode;
                    _OtherContract.Mobile = OtherContract.Mobile;
                    _OtherContract.Email = OtherContract.Email;
                    _OtherContract.PANNo = OtherContract.PANNo;
                    _OtherContract.Requestdate = OtherContract.Requestdate;
                    _OtherContract.ProjectTitle = OtherContract.ProjectTitle;
                    _OtherContract.ProjectISBN = OtherContract.ProjectISBN;
                    _OtherContract.Contracttypeid = OtherContract.Contracttypeid;
                   // _OtherContract.ContractDate = OtherContract.ContractDate;
                  //  _OtherContract.Periodofagreement = OtherContract.Periodofagreement;
                  //  _OtherContract.Expirydate = OtherContract.Expirydate;
                    _OtherContract.Territoryrightsid = OtherContract.Territoryrightsid;
                    _OtherContract.Payment = OtherContract.Payment;
                    _OtherContract.paymentperiodid = OtherContract.paymentperiodid;
                    _OtherContract.NatureOfWork = OtherContract.NatureOfWork;
                   // _OtherContract.divisionid = OtherContract.divisionid;
                    _OtherContract.ContractSignedByExecutiveid = OtherContract.ContractSignedByExecutiveid;
                    _OtherContract.Remarks = OtherContract.Remarks;
                    _OtherContract.EnteredBy = OtherContract.EnteredBy;
                    _OtherContract.PaymentAmount = OtherContract.PaymentAmount;
                    _OtherContract.CurrencyMasterId = OtherContract.CurrencyMasterId;

                    OtherContractIdId = _OtherContractService.InsertOtherContract(_OtherContract);

                    if (OtherContractIdId != 0)
                    {

                        OtherContractDivisionLink DivisionLink = new OtherContractDivisionLink();
                        int k = 0;
                        foreach (var item in OtherContract.Division)
                        {
                            DivisionLink.othercontractid = OtherContractIdId;
                            DivisionLink.divisionid = item;
                            DivisionLink.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractDivisionLink(DivisionLink);
                            k++;
                        }


                        OtherContractDocuments _OtherContractDoc = new OtherContractDocuments();

                        string[] docurl = OtherContract.documentfile.Split(',');
                        int i = 0;
                        foreach (string doc in OtherContract.Documentname)
                        {
                            OtherContractDocuments Link = new OtherContractDocuments();
                            Link.othercontractid = OtherContractIdId;
                            Link.Documentname = doc;
                            Link.documentfile = docurl[i];
                            Link.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractDocumentsLinking(Link);
                            i++;
                        }

                        if (OtherContract.ForImageBank == "1")
                        {
                            /* Added by dheeraj Kumar sharma */
                            OtherContractImageBank _OtherContractImageBank = new OtherContractImageBank();
                            IList<VideoImageBank> ImageBankList = new List<VideoImageBank>();
                          
                            if(OtherContract.VideoImageBank.Count>0)
                            {
                                foreach(var obj in OtherContract.VideoImageBank)
                                {
                                    VideoImageBank obj_VideoImageBank = new VideoImageBank();
                                    obj_VideoImageBank.Type = obj.Type;
                                    obj_VideoImageBank.ShortName = obj.ShortName;
                                    obj_VideoImageBank.Fullname = obj.Fullname;
                                    obj_VideoImageBank.CurrencyId = obj.CurrencyId;
                                    obj_VideoImageBank.Cost = obj.Cost;
                                    obj_VideoImageBank.Deactivate = "N";
                                    obj_VideoImageBank.EnteredBy = OtherContract.EnteredBy;
                                    obj_VideoImageBank.EntryDate = DateTime.Now;
                                    ImageBankList.Add(obj_VideoImageBank);
                                }
                            }
                            _OtherContractImageBank.VideoImageBank = ImageBankList;
                            /*End by dheeraj*/
                            _OtherContractImageBank.Printrunquantity = OtherContract.Printrunquantity;
                            _OtherContractImageBank.PrintRights = OtherContract.PrintRights;
                            _OtherContractImageBank.electronicrights = OtherContract.electronicrights;
                            _OtherContractImageBank.ebookrights = OtherContract.ebookrights;
                            _OtherContractImageBank.cost = OtherContract.cost;
                            _OtherContractImageBank.currencyid = OtherContract.currencyid;
                            _OtherContractImageBank.restriction = OtherContract.restriction;

                            _OtherContractImageBank.othercontractid = OtherContractIdId;
                            _OtherContractImageBank.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractImageBank(_OtherContractImageBank);

                        }

                    }


                    OtherContractLink _OtherContractLink1 = new OtherContractLink();
                    _OtherContractLink1.Contractstatus = null;
                    _OtherContractLink1.SignedContractSentDate = null;
                    _OtherContractLink1.SignedContractReceived_Date = null;
                    _OtherContractLink1.CancellationDate = null;
                    _OtherContractLink1.Cancellation_Reason = null;
                    _OtherContractLink1.othercontractid = OtherContractIdId;
                    _OtherContractLink1.Status = "Pending";
                    _OtherContractLink1.EnteredBy = OtherContract.EnteredBy;
                    _OtherContractLink1.Remarks = OtherContract.PendingRemarks;

                    int OtherContractLinkIdId = _OtherContractService.InsertOtherContractLink(_OtherContractLink1);


                    ApplicationSetUp Mobj_ApplicationSetUp = new ApplicationSetUp();

                    Mobj_ApplicationSetUp.Id = OtherContractSuggesation.FirstOrDefault().Id;

                    ApplicationSetUp _ApplicationSetUpUpdate = _ApplicationSetUpService.GetApplicationSetUpById(Mobj_ApplicationSetUp);

                    _ApplicationSetUpUpdate.Id = OtherContractSuggesation.FirstOrDefault().Id;
                    int Value = Int32.Parse(OtherContractSuggesation.FirstOrDefault().OtherContractValue) + 1;

                    _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');

                    _ApplicationSetUpUpdate.ModifiedBy = OtherContract.EnteredBy;
                    _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;

                    _ApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);
                }
                else
                {

                    if (OtherContract.UpdateRight=="rt" )
                    {
                        OtherContractLink mobj_OtherContractLink = _OtherContractLink.Table.Where(i => i.othercontractid == OtherContract.Id && i.Deactivate == "N").FirstOrDefault();

                        if (mobj_OtherContractLink != null)
                        {
                            mobj_OtherContractLink.Contractstatus = OtherContract.Contractstatus;
                            mobj_OtherContractLink.SignedContractSentDate = OtherContract.SignedContractSentDate;
                            mobj_OtherContractLink.SignedContractReceived_Date = OtherContract.SignedContractReceived_Date;
                            mobj_OtherContractLink.CancellationDate = OtherContract.CancellationDate;
                            mobj_OtherContractLink.Cancellation_Reason = OtherContract.Cancellation_Reason;
                            mobj_OtherContractLink.othercontractid = OtherContract.Id;
                            mobj_OtherContractLink.ModifiedBy = OtherContract.EnteredBy;
                            mobj_OtherContractLink.Status = OtherContract.Contractstatus;
                            mobj_OtherContractLink.Remarks = OtherContract.PendingRemarks;

                            mobj_OtherContractLink.AgreementDate = OtherContract.AgreementDate;
                            mobj_OtherContractLink.Effectivedate = OtherContract.Effectivedate;
                            //mobj_OtherContractLink.Contractperiodinmonth = OtherContract.Contractperiodinmonth;

                            mobj_OtherContractLink.Expirydate = OtherContract.Expirydate;

                            _OtherContractService.UpdateOtherContractLink(mobj_OtherContractLink);


                            OtherContractLinkDocument _OtherContractDocLink = new OtherContractLinkDocument();

                            string[] docurl1 = OtherContract.documentfileLink.Split(',');
                            int i = 0;
                            foreach (string doc in OtherContract.DocumentnameLink)
                            {
                                OtherContractLinkDocument Link = new OtherContractLinkDocument();
                                Link.othercontractLinkid = mobj_OtherContractLink.Id;
                                Link.DocumentnameLink = doc;
                                Link.documentfileLink = docurl1[i];
                                Link.EnteredBy = OtherContract.EnteredBy;
                                _OtherContractService.InsertOtherContractDocumentsLinkingLink(Link);
                                i++;
                            }
                        }
                     



                    }
                    else if (OtherContract.UpdateRight == "ad" || OtherContract.UpdateRight == "sa")
                    {
                        OtherContractMaster mobj_OtherDocument = _OtherContractService.GetOtherContractMasterId(OtherContract.Id);

                        mobj_OtherDocument.partyname = OtherContract.partyname;
                        mobj_OtherDocument.natureofserviceid = OtherContract.natureofserviceid;
                        mobj_OtherDocument.natureofsubserviceid = OtherContract.natureofsubserviceid;
                        mobj_OtherDocument.Address = OtherContract.Address;
                        mobj_OtherDocument.CountryId = OtherContract.CountryId;
                        mobj_OtherDocument.OtherCountry = OtherContract.OtherCountry;
                        mobj_OtherDocument.Stateid = OtherContract.Stateid;
                        mobj_OtherDocument.OtherState = OtherContract.OtherState;
                        mobj_OtherDocument.Cityid = OtherContract.Cityid;
                        mobj_OtherDocument.OtherCity = OtherContract.OtherCity;
                        mobj_OtherDocument.Pincode = OtherContract.Pincode;
                        mobj_OtherDocument.Mobile = OtherContract.Mobile;
                        mobj_OtherDocument.Email = OtherContract.Email;
                        mobj_OtherDocument.PANNo = OtherContract.PANNo;
                        mobj_OtherDocument.Requestdate = OtherContract.Requestdate;
                        mobj_OtherDocument.ProjectTitle = OtherContract.ProjectTitle;
                        mobj_OtherDocument.ProjectISBN = OtherContract.ProjectISBN;
                        mobj_OtherDocument.Contracttypeid = OtherContract.Contracttypeid;
                      //  mobj_OtherDocument.ContractDate = OtherContract.ContractDate;
                      //  mobj_OtherDocument.Periodofagreement = OtherContract.Periodofagreement;
                      //  mobj_OtherDocument.Expirydate = OtherContract.Expirydate;
                        mobj_OtherDocument.Territoryrightsid = OtherContract.Territoryrightsid;
                        mobj_OtherDocument.Payment = OtherContract.Payment;
                        mobj_OtherDocument.paymentperiodid = OtherContract.paymentperiodid;
                        mobj_OtherDocument.NatureOfWork = OtherContract.NatureOfWork;
                      //  mobj_OtherDocument.divisionid = OtherContract.divisionid;
                        mobj_OtherDocument.ContractSignedByExecutiveid = OtherContract.ContractSignedByExecutiveid;
                        mobj_OtherDocument.Remarks = OtherContract.Remarks;
                        mobj_OtherDocument.ModifiedBy = OtherContract.EnteredBy;
                        mobj_OtherDocument.PaymentAmount = OtherContract.PaymentAmount;
                        mobj_OtherDocument.CurrencyMasterId = OtherContract.CurrencyMasterId;

                        _OtherContractService.UpdateOtherContractMaster(mobj_OtherDocument);

                        _OtherContractService.DeleteOtherContractDivisionLink(OtherContract.Id, OtherContract.EnteredBy);

                        OtherContractDivisionLink DivisionLink = new OtherContractDivisionLink();
                        int k = 0;
                        foreach (var item in OtherContract.Division)
                        {
                            DivisionLink.othercontractid = OtherContract.Id;
                            DivisionLink.divisionid = item;
                            DivisionLink.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractDivisionLink(DivisionLink);
                            k++;
                        }


                        OtherContractImageBank mobj_OtherContractImageBank = _OtherContractImageBank.Table.Where(i => i.othercontractid == OtherContract.Id && i.Deactivate == "N").FirstOrDefault();

                        if (mobj_OtherContractImageBank !=null)
                        {
                            _OtherContractService.DeleteVideoImageBankLink(mobj_OtherContractImageBank.Id, OtherContract.EnteredBy);
                         
                            if (OtherContract.VideoImageBank.Count > 0)
                            {
                                int m = 0;

                               

                                foreach (var obj in OtherContract.VideoImageBank)
                                {
                                 

                                        VideoImageBank obj_VideoImageBank = new VideoImageBank();
                                        obj_VideoImageBank.Type = obj.Type;
                                        obj_VideoImageBank.ShortName = obj.ShortName;
                                        obj_VideoImageBank.Fullname = obj.Fullname;
                                        obj_VideoImageBank.CurrencyId = obj.CurrencyId;
                                        obj_VideoImageBank.Cost = obj.Cost;
                                        obj_VideoImageBank.ImageBankId = mobj_OtherContractImageBank.Id;
                                        obj_VideoImageBank.EnteredBy = OtherContract.EnteredBy;
                                        obj_VideoImageBank.EntryDate = DateTime.Now;
                                        obj_VideoImageBank.Deactivate = "N";
                                        _OtherContractService.InsertVideoImageBankLink(obj_VideoImageBank);
                                        m++;
                                }
                            }

                            mobj_OtherContractImageBank.Printrunquantity = OtherContract.Printrunquantity;
                            mobj_OtherContractImageBank.PrintRights = OtherContract.PrintRights;
                            mobj_OtherContractImageBank.electronicrights = OtherContract.electronicrights;
                            mobj_OtherContractImageBank.ebookrights = OtherContract.ebookrights;
                            mobj_OtherContractImageBank.cost = OtherContract.cost;
                            mobj_OtherContractImageBank.currencyid = OtherContract.currencyid;
                            mobj_OtherContractImageBank.restriction = OtherContract.restriction;

                            mobj_OtherContractImageBank.othercontractid = OtherContract.Id;
                            mobj_OtherContractImageBank.ModifiedBy = OtherContract.EnteredBy;
                            _OtherContractService.UpdateOtherContractImageBank(mobj_OtherContractImageBank);
                        }

                        else  if (OtherContract.ForImageBank == "1" )
                        {
                            //OtherContractImageBank _OtherConImageBank = new OtherContractImageBank();
                            //_OtherConImageBank.Printrunquantity = OtherContract.Printrunquantity;
                            //_OtherConImageBank.PrintRights = OtherContract.PrintRights;
                            //_OtherConImageBank.electronicrights = OtherContract.electronicrights;
                            //_OtherConImageBank.ebookrights = OtherContract.ebookrights;
                            //_OtherConImageBank.cost = OtherContract.cost;
                            //_OtherConImageBank.currencyid = OtherContract.currencyid;
                            //_OtherConImageBank.restriction = OtherContract.restriction;

                            //_OtherConImageBank.othercontractid = OtherContract.Id;
                            //_OtherConImageBank.EnteredBy = OtherContract.EnteredBy;
                            //_OtherContractService.InsertOtherContractImageBank(_OtherConImageBank);

                            OtherContractImageBank _OtherConImageBank = new OtherContractImageBank();
                            IList<VideoImageBank> ImageBankList = new List<VideoImageBank>();

                            if (OtherContract.VideoImageBank.Count > 0)
                            {
                                foreach (var obj in OtherContract.VideoImageBank)
                                {
                                    VideoImageBank obj_VideoImageBank = new VideoImageBank();
                                    obj_VideoImageBank.Type = obj.Type;
                                    obj_VideoImageBank.ShortName = obj.ShortName;
                                    obj_VideoImageBank.Fullname = obj.Fullname;
                                    obj_VideoImageBank.CurrencyId = obj.CurrencyId;
                                    obj_VideoImageBank.Cost = obj.Cost;
                                    obj_VideoImageBank.Deactivate = "N";
                                    obj_VideoImageBank.EnteredBy = OtherContract.EnteredBy;
                                    obj_VideoImageBank.EntryDate = DateTime.Now;
                                    ImageBankList.Add(obj_VideoImageBank);
                                }
                            }
                            _OtherConImageBank.VideoImageBank = ImageBankList;
                            /*End by dheeraj*/
                            _OtherConImageBank.Printrunquantity = OtherContract.Printrunquantity;
                            _OtherConImageBank.PrintRights = OtherContract.PrintRights;
                            _OtherConImageBank.electronicrights = OtherContract.electronicrights;
                            _OtherConImageBank.ebookrights = OtherContract.ebookrights;
                            _OtherConImageBank.cost = OtherContract.cost;
                            _OtherConImageBank.currencyid = OtherContract.currencyid;
                            _OtherConImageBank.restriction = OtherContract.restriction;

                            _OtherConImageBank.othercontractid = OtherContract.Id;
                            _OtherConImageBank.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractImageBank(_OtherConImageBank);

                        }


                        OtherContractDocuments _OtherContractDoc = new OtherContractDocuments();

                        string[] docurl = OtherContract.documentfile.Split(',');
                        int j = 0;
                        foreach (string doc in OtherContract.Documentname)
                        {
                            OtherContractDocuments Link = new OtherContractDocuments();
                            Link.othercontractid = OtherContract.Id;
                            Link.Documentname = doc;
                            Link.documentfile = docurl[j];
                            Link.EnteredBy = OtherContract.EnteredBy;
                            _OtherContractService.InsertOtherContractDocumentsLinking(Link);
                            j++;
                        }


                        OtherContractLink mobj_OtherContractLink = _OtherContractLink.Table.Where(i => i.othercontractid == OtherContract.Id && i.Deactivate == "N").FirstOrDefault();

                        if (mobj_OtherContractLink != null)
                        {
                            mobj_OtherContractLink.Contractstatus = OtherContract.Contractstatus;
                            mobj_OtherContractLink.SignedContractSentDate = OtherContract.SignedContractSentDate;
                            mobj_OtherContractLink.SignedContractReceived_Date = OtherContract.SignedContractReceived_Date;
                            mobj_OtherContractLink.CancellationDate = OtherContract.CancellationDate;
                            mobj_OtherContractLink.Cancellation_Reason = OtherContract.Cancellation_Reason;
                            mobj_OtherContractLink.othercontractid = OtherContract.Id;
                            mobj_OtherContractLink.ModifiedBy = OtherContract.EnteredBy;
                            mobj_OtherContractLink.Status = OtherContract.Contractstatus;
                            mobj_OtherContractLink.Remarks = OtherContract.PendingRemarks;

                            mobj_OtherContractLink.AgreementDate = OtherContract.AgreementDate;
                            mobj_OtherContractLink.Effectivedate = OtherContract.Effectivedate;
                            //mobj_OtherContractLink.Contractperiodinmonth = OtherContract.Contractperiodinmonth;

                            mobj_OtherContractLink.Expirydate = OtherContract.Expirydate;

                            _OtherContractService.UpdateOtherContractLink(mobj_OtherContractLink);


                            OtherContractLinkDocument _OtherContractDocLink = new OtherContractLinkDocument();

                            string[] docurl1 = OtherContract.documentfileLink.Split(',');
                            int i = 0;
                            foreach (string doc in OtherContract.DocumentnameLink)
                            {
                                OtherContractLinkDocument Link = new OtherContractLinkDocument();
                                Link.othercontractLinkid = mobj_OtherContractLink.Id;
                                Link.DocumentnameLink = doc;
                                Link.documentfileLink = docurl1[i];
                                Link.EnteredBy = OtherContract.EnteredBy;
                                _OtherContractService.InsertOtherContractDocumentsLinkingLink(Link);
                                i++;
                            }
                        }                       

                    }

                    //-------------------------------------------
                    OtherContractIdId = OtherContract.Id;
                    OtherContactCode = _OtherContractMaster.Table.Where(a => a.Id == OtherContract.Id).Select(b => b.othercontractcode).SingleOrDefault();

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
            return Json(SerializeObj.SerializeObject(new { status, OtherContactCode, OtherContractIdId }));
           
        }


        public IHttpActionResult OtherContractSerch(OtherContractSearch SearchParam)
        {

            if (SearchParam.SessionId == "")
            {
                return Json("NOK");
            }
            else
            {
                var status = "";
                _OtherContractService.InsertSearchHistory(SearchParam);
                status = "OK";
                return Json(status);
            }



        }



        public IHttpActionResult GetOtherContractSearchList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<OtherContractDetailsModel>("Proc_OtherContractSerch_get", parameters).ToList();
                    return Json(_GetAuthorReport);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IHttpActionResult ContractSignedByExecutive(Contract_Signed_By_Executive OtherContractExecutive)
        {

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 50);
            if (OtherContractExecutive.Id == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = OtherContractExecutive.Id;
            }




            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<Contract_Signed_By_Executive>("Proc_Contract_Signed_By_Executive_get", parameters).ToList();


            return Json(_GetAuthorReport);

        }




        public IHttpActionResult WebGetaOtherContractById(OtherContractMaster OtherContract)
        {
            
            int ImageBankId = 0;

            OtherContractMaster  _OtherContract = _OtherContractService.GetOtherContractMasterById(OtherContract);
            var OtherContractList = new
            {
                Id = _OtherContract.Id,
                partyname = _OtherContract.partyname,
                natureofserviceid = _OtherContract.natureofserviceid,
                natureofsubserviceid = _OtherContract.natureofsubserviceid,
                Address = _OtherContract.Address,
                CountryId = _OtherContract.CountryId,
                OtherCountry = _OtherContract.OtherCountry,
                Stateid = _OtherContract.Stateid,
                OtherState = _OtherContract.OtherState,
                Cityid = _OtherContract.Cityid,
                OtherCity = _OtherContract.OtherCity,
                Pincode = _OtherContract.Pincode,
                Mobile = _OtherContract.Mobile,
                Email = _OtherContract.Email,
                PANNo = _OtherContract.PANNo,
                Requestdate = _OtherContract.Requestdate,
                ProjectTitle = _OtherContract.ProjectTitle,
                ProjectISBN = _OtherContract.ProjectISBN,
                Contracttypeid = _OtherContract.Contracttypeid,
              //  ContractDate = _OtherContract.ContractDate,
            //    Periodofagreement = _OtherContract.Periodofagreement,
            //    Expirydate = _OtherContract.Expirydate,
                Territoryrightsid = _OtherContract.Territoryrightsid,
                Payment = _OtherContract.Payment,
                paymentperiodid = _OtherContract.paymentperiodid,
                NatureOfWork = _OtherContract.NatureOfWork,
             //   divisionid = _OtherContract.divisionid,
                ContractSignedByExecutiveid = _OtherContract.ContractSignedByExecutiveid,
                Remarks = _OtherContract.Remarks,
                PaymentAmount = _OtherContract.PaymentAmount,
                CurrencyMasterId = _OtherContract.CurrencyMasterId,
              
            };


        //    var OtherContractDivisionLink = _OtherContractDivisionLink.Table.Where(a => a.othercontractid == _OtherContract.Id && a.Deactivate == "N").ToList();

            var OtherContractDivisionLink = (from OCDL in _OtherContractDivisionLink.Table.Where(a => a.Deactivate == "N")
                                             join DM in _DivisionMaster.Table.Where(a => a.Deactivate == "N")
                                             on OCDL.divisionid equals DM.Id

                                             select new
                                             {
                                                 divisionid = OCDL.divisionid,
                                                 divisionName = DM.divisionName,
                                                 othercontractid = OCDL.othercontractid
                                             }

                ).Distinct().Where(a => a.othercontractid == _OtherContract.Id).ToList();
                
            OtherContractImageBank _ContractImageBank = _OtherContractImageBank.Table.Where(a => a.othercontractid == _OtherContract.Id && a.Deactivate == "N").FirstOrDefault();


            if(_ContractImageBank != null)
            {
                ImageBankId = _ContractImageBank.Id ;
            }
           IList<VideoImageBank> ImageTypeOtherContact = _VideoImageBank.Table.Where(a => a.Deactivate == "N" && a.ImageBankId == ImageBankId  && a.Type == "I").ToList();
           IList<VideoImageBank> VideoTypeOtherContact = _VideoImageBank.Table.Where(a => a.Deactivate == "N" && a.ImageBankId == ImageBankId && a.Type == "V").ToList();

        
           
            if (_ContractImageBank !=null)
            {
                var _ContractImageBankList = new
                {
                    Id = _ContractImageBank.Id,
                    Printrunquantity = _ContractImageBank.Printrunquantity,
                    PrintRights = _ContractImageBank.PrintRights,
                    electronicrights = _ContractImageBank.electronicrights,
                    ebookrights = _ContractImageBank.ebookrights,
                    cost = _ContractImageBank.cost,
                    currencyid = _ContractImageBank.currencyid,
                    restriction = _ContractImageBank.restriction,
                    othercontractid = _ContractImageBank.othercontractid,

                };


            }



            Contract_Signed_By_Executive OtherContractDocuments = new Contract_Signed_By_Executive();

            var documents = _OtherContractDocuments.Table.Where(a => a.othercontractid == _OtherContract.Id && a.Deactivate == "N").ToList();


            OtherContractDocuments.DocumentIds = documents.Select(i => i.Id).ToArray();


            OtherContractDocuments.Documentname = documents.Select(i => i.Documentname).ToArray();
            foreach (var docs in documents)
                OtherContractDocuments.documentfile = OtherContractDocuments.documentfile + docs.documentfile + ",";



         



            OtherContractLink _OtherContractLintList = _OtherContractLink.Table.Where(a => a.othercontractid == _OtherContract.Id && a.Deactivate == "N").FirstOrDefault();
            Contract_Signed_By_Executive OtherContractDocuments2 = new Contract_Signed_By_Executive();

            if(_OtherContractLintList != null)
            {
              var OtherConractlinkData = new
            {
            Contractstatus = _OtherContractLintList.Contractstatus,
            Signed_Contract_Sent_Date = _OtherContractLintList.SignedContractSentDate,
            Signed_Contract_received_Date = _OtherContractLintList.SignedContractReceived_Date,
            Cancellation_Date = _OtherContractLintList.CancellationDate,
            Cancellation_Reason = _OtherContractLintList.Cancellation_Reason,
            PendingRemarks = _OtherContractLintList.Remarks
            };


            


              var documents2 = _OtherContractLinkDocument.Table.Where(a => a.othercontractLinkid == _OtherContractLintList.Id && a.Deactivate == "N").ToList();


              OtherContractDocuments2.DocumentlinkIds = documents2.Select(i => i.Id).ToArray();


              OtherContractDocuments2.DocumentnameLink = documents2.Select(i => i.DocumentnameLink).ToArray();
              foreach (var docs in documents2)
                  OtherContractDocuments2.documentfileLink = OtherContractDocuments2.documentfileLink + docs.documentfileLink + ",";


            }




            return Json(SerializeObj.SerializeObject(new { _OtherContract, _ContractImageBank, OtherContractDocuments, _OtherContractLintList, OtherContractDocuments2, OtherContractDivisionLink, ImageTypeOtherContact, VideoTypeOtherContact }));



        }


        public IHttpActionResult WebGetaCUrrencyByDefault()
        {

            CurrencyMaster _currency = _CurrencyMaster.Table.Where(a => a.Symbol == "INR" && a.Deactivate == "N").FirstOrDefault();

            var _currencyList = new
            {
                CurrencyCode = _currency.Id
            };


            return Json(SerializeObj.SerializeObject(new { _currencyList}));



        }



        public IHttpActionResult OtherContractSerchView(Contract_Signed_By_Executive OtherContract)
        {

            SqlParameter[] parameters = new SqlParameter[1];



            parameters[0] = new SqlParameter("OthercontractId", SqlDbType.VarChar, 50);
            if (OtherContract.Id == null)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = OtherContract.Id;
            }



            var _GetOtherContractSearch = _dbContext.ExecuteStoredProcedureListNewData<Contract_Signed_By_Executive>("Proc_OtherContractList_get", parameters).ToList();


            var OtherContractDivisionLink = (from OCDL in _OtherContractDivisionLink.Table.Where(a => a.Deactivate == "N")
                                             join DM in _DivisionMaster.Table.Where(a => a.Deactivate == "N")
                                             on OCDL.divisionid equals DM.Id

                                             select new
                                             {
                                                 divisionid = OCDL.divisionid,
                                                 divisionName = DM.divisionName,
                                                 othercontractid = OCDL.othercontractid
                                             }

              ).Distinct().Where(a => a.othercontractid == OtherContract.Id).ToList();


            OtherContractImageBank _ContractImageBank = _OtherContractImageBank.Table.Where(a => a.othercontractid == OtherContract.Id && a.Deactivate == "N").FirstOrDefault();

            Contract_Signed_By_Executive OtherContractDocuments = new Contract_Signed_By_Executive();

            var documents = _OtherContractDocuments.Table.Where(a => a.othercontractid == OtherContract.Id && a.Deactivate == "N").ToList();


            OtherContractDocuments.DocumentIds = documents.Select(i => i.Id).ToArray();


            OtherContractDocuments.Documentname = documents.Select(i => i.Documentname).ToArray();
            foreach (var docs in documents)
                OtherContractDocuments.documentfile = OtherContractDocuments.documentfile + docs.documentfile + ",";


            OtherContractLink _OtherContractLintList = _OtherContractLink.Table.Where(a => a.othercontractid == OtherContract.Id && a.Deactivate == "N").FirstOrDefault();
            
            Contract_Signed_By_Executive OtherContractDocuments2 = new Contract_Signed_By_Executive();


            var documents2 = _OtherContractLinkDocument.Table.Where(a => a.othercontractLinkid == _OtherContractLintList.Id && a.Deactivate == "N").ToList();


            OtherContractDocuments2.DocumentlinkIds = documents2.Select(i => i.Id).ToArray();


            OtherContractDocuments2.DocumentnameLink = documents2.Select(i => i.DocumentnameLink).ToArray();
            foreach (var docs in documents2)
                OtherContractDocuments2.documentfileLink = OtherContractDocuments2.documentfileLink + docs.documentfileLink + ",";


            if (_ContractImageBank != null)
            {

                var ImageTypeOtherContact = (from VIBL in _VideoImageBank.Table.Where(a => a.Deactivate == "N" && a.Type == "I")
                                             join CM in _CurrencyMaster.Table.Where(a => a.Deactivate == "N")
                                             on VIBL.CurrencyId equals CM.Id
                                             select new
                                             {
                                                 Fullname = VIBL.Fullname,
                                                 Cost = VIBL.Cost,
                                                 Currency = CM.CurrencyName,
                                                 CurrencySymbol = CM.Symbol,
                                                 CurrencyId = VIBL.CurrencyId,
                                                 ImageBankId = VIBL.ImageBankId

                                             }).Distinct().Where(a => a.ImageBankId == _ContractImageBank.Id).ToList();

                var VideoTypeOtherContact = (from VIBL in _VideoImageBank.Table.Where(a => a.Deactivate == "N" && a.Type == "V")
                                             join CM in _CurrencyMaster.Table.Where(a => a.Deactivate == "N")
                                             on VIBL.CurrencyId equals CM.Id
                                             select new
                                             {
                                                 Fullname = VIBL.Fullname,
                                                 Cost = VIBL.Cost,
                                                 Currency = CM.CurrencyName,
                                                 CurrencySymbol = CM.Symbol,
                                                 CurrencyId = VIBL.CurrencyId,
                                                 ImageBankId = VIBL.ImageBankId
                                             }
                                                  ).Distinct().Where(a => a.ImageBankId == _ContractImageBank.Id).ToList();

                return Json(SerializeObj.SerializeObject(new { _GetOtherContractSearch, OtherContractDivisionLink, ImageTypeOtherContact, VideoTypeOtherContact, OtherContractDocuments, OtherContractDocuments2 }));

            }
            else
            {
                var ImageTypeOtherContact = new { };
                var VideoTypeOtherContact = new { };
                return Json(SerializeObj.SerializeObject(new { _GetOtherContractSearch, OtherContractDivisionLink, ImageTypeOtherContact, VideoTypeOtherContact, OtherContractDocuments, OtherContractDocuments2 }));

            }

            

         //   return Json(_GetOtherContractSearch);

        }



        public IHttpActionResult RemoveAuhtorDocument(Contract_Signed_By_Executive Dcoument)
        {
            OtherContractDocuments document = _OtherContractService.getOtherContractDocumentsDetail(Dcoument.Id);

            string status = string.Empty;
            try
            {

                _OtherContractService.DeavtivateOtherContractDocumentsById(Dcoument.Id, Dcoument.EnteredBy);


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


        public IHttpActionResult RemoveAuhtorDocumentLink(Contract_Signed_By_Executive Dcoument)
        {
            OtherContractLinkDocument document = _OtherContractLinkDocument.Table.Where(x => x.Id == Dcoument.Id && x.Deactivate == "N").FirstOrDefault();

            string status = string.Empty;
            try
            {

                _OtherContractService.DeavtivateOtherContractDocumentsLinkById(Dcoument.Id, Dcoument.EnteredBy);


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




        public IList<VideoImageBank> getVideoImageBank(int id)
        {
            return _VideoImageBank.Table.Where(i => i.ImageBankId == id && i.Deactivate == "N").ToList();
        }
         
        public IHttpActionResult TopSearch(String Code)
        {

            OtherContractMaster OtherContract = _OtherContractMaster.Table.Where(a => a.othercontractcode == Code && a.Deactivate == "N").FirstOrDefault();


            if (OtherContract != null)
            {
                var _OtherContract = new
                {
                    othercontractcode = OtherContract.Id
                };


                return Json(SerializeObj.SerializeObject(new { _OtherContract }));
            }
            else
            {
                string _OtherContract = string.Empty;
                return Json(SerializeObj.SerializeObject(new { _OtherContract }));
            }

           
        }

        /* Create By  : Prakash
        * Create on  : 05 May, 2017
        * Create for : Delete OtherContract
        */
        public IHttpActionResult OtherContractSearch1(OtherContractSearch SearchParam)
        {
            string status = string.Empty;

            try
            {
                OtherContractMaster OtherContract = new OtherContractMaster();
                OtherContract.Id = SearchParam.Id;
                if (OtherContract.Id != 0)
                {
                    OtherContractMaster mobj_OtherContractMaster = _OtherContractService.GetOtherContractMasterById(OtherContract);
                    mobj_OtherContractMaster.Deactivate = "Y";
                    mobj_OtherContractMaster.DeactivateBy = SearchParam.DeactivateBy;
                    mobj_OtherContractMaster.DeactivateDate = DateTime.Now;

                    _OtherContractService.DeleteOtherContractMaster(mobj_OtherContractMaster);
                }
                status = "OK";

                return Json(status);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }

      
	}
}