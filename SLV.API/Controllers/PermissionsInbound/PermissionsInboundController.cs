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
using ACS.Services.PermissionsInbound;
using ACS.Core.Domain.OtherContract;


using SLV.Model.PermissionInboundModel;
using ACS.Core.Domain.PermissionInbound;
using System.Reflection;
using System.Data.OleDb;

using System.Web;
using System.IO;

using Microsoft.VisualBasic;

using System.Collections;


using System.Diagnostics;

using System.Xml;


using ACS.Core.Domain.Common;

using ACS.Web.Framework.Controllers;
using ACS.Services.Contact;
using ACS.Services.Authentication;


using ACS.Services.Security;
using System.Web.Helpers;

using ACS.Services.User;
using System.Web.UI;

using System.Web.Configuration;
using System.Text.RegularExpressions;
using System.Globalization;
using ACS.Core.Domain.OtherContract;
using Logger;

namespace SLV.API.Controllers.PermissionsInbound
{

    public class PermissionsInboundController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IPermissionsInboundService _IPermissionsInboundService;
        private readonly IRepository<OtherContractMaster> _OtherContractMaster;
        private readonly IDbContext _dbContext;

        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IApplicationSetUpService _ApplicationSetUpService;
        private readonly ILocalizationService _localizationService;

        private readonly IRepository<OtherContractImageBank> _OtherContractImageBank;
        private readonly IRepository<CurrencyMaster> _CurrencyMaster;

        private readonly IRepository<AssetSubType> _AssetSubType;
        private readonly IRepository<StatusMaster> _StatusMaster;

        private readonly IRepository<CopyRightHolderMaster> _CopyRightHolderMaster;
        private readonly IRepository<PermissionInbound> _PermissionInbound;
        private readonly IRepository<PermissionInboundOthersRightsLink> _PermissionInboundOthersRightsLink;
        private readonly IRepository<OtherRightsMaster> _OtherRightsMaster;
        private readonly IRepository<PermissionInboundOthers> _PermissionInboundOthers;
        private readonly IRepository<PermissionInboundCopyRightHolderMaster> _PermissionInboundCopyRightHolderMaster;

        private readonly IRepository<TerritoryRightsMaster> _TerritoryRightsMaster;
      
        DataTable _docDataTble = new DataTable();


        public PermissionsInboundController(

                 IDbContext dbContext

             , IPermissionsInboundService IPermissionsInboundService
            , IRepository<OtherContractMaster> OtherContractMaster

             , IRepository<ApplicationSetUp> ApplicationSetUp
             , IApplicationSetUpService ApplicationSetUpService
             , ILocalizationService localizationService
            , IRepository<OtherContractImageBank> OtherContractImageBank
            , IRepository<CurrencyMaster> CurrencyMaster
            , IRepository<AssetSubType> AssetSubType
            , IRepository<StatusMaster> StatusMaster
            , IRepository<CopyRightHolderMaster> CopyRightHolderMaster
            , IRepository<PermissionInbound> PermissionInbound
            , IRepository<PermissionInboundOthersRightsLink> PermissionInboundOthersRightsLink
            , IRepository<OtherRightsMaster> OtherRightsMaster
            , IRepository<PermissionInboundOthers> PermissionInboundOthers
            , IRepository<PermissionInboundCopyRightHolderMaster> PermissionInboundCopyRightHolderMaster
            ,IRepository<TerritoryRightsMaster> TerritoryRightsMaster
           
             )
        {



            this._dbContext = dbContext;

            this._IPermissionsInboundService = IPermissionsInboundService;
            this._OtherContractMaster = OtherContractMaster;
            this._ApplicationSetUp = ApplicationSetUp;
            this._ApplicationSetUpService = ApplicationSetUpService;
            this._localizationService = localizationService;
            this._OtherContractImageBank = OtherContractImageBank;
            this._CurrencyMaster = CurrencyMaster;
            this._AssetSubType = AssetSubType;
            this._StatusMaster = StatusMaster;
            this._CopyRightHolderMaster = CopyRightHolderMaster;
            this._PermissionInbound = PermissionInbound;
            this._PermissionInboundOthersRightsLink = PermissionInboundOthersRightsLink;
            this._OtherRightsMaster = OtherRightsMaster;
            this._PermissionInboundOthers = PermissionInboundOthers;
            this._PermissionInboundCopyRightHolderMaster = PermissionInboundCopyRightHolderMaster;
            this._TerritoryRightsMaster = TerritoryRightsMaster;
        }


        public IHttpActionResult getContractPartyType()
        {
            //var ContractPartyType = {};
            try
            {
              var   ContractPartyType = (from OCM in _OtherContractMaster.Table.Where(a => a.Deactivate == "N")
                                         join OCI in _OtherContractImageBank.Table.Where(a => a.Deactivate == "N")
                                         on OCM.Id equals OCI.othercontractid

                                         select new
                                         {
                                             Id = OCM.Id,
                                             partyname = OCM.partyname,
                                             OtherContractCode = OCM.othercontractcode
                                         }

                    ).Distinct().OrderBy(a => a.partyname);

              return Json(ContractPartyType);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getContractPartyType", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getContractPartyType", ex);
                return Json(ex.InnerException);
            }

           

        }

        /**************************************************************************************
         Created By  : Dheeraj Kumar sharma
         Created On  : 10th aug 2016
         Created For : getting the party details based on party Id
         *******************************************************************************************/
        public IHttpActionResult PartyDetailById(PermissionsInboundModel _OtherContract)
        {
            try
            {
                var partybasicDetail = _IPermissionsInboundService.GeOtherContractImageBankDetails(_OtherContract.Id);
                var mobj_partyDetails = new
                {
                    Restriction = partybasicDetail.restriction,
                    PrintRights = partybasicDetail.PrintRights,
                    Electronicrights = partybasicDetail.electronicrights,
                    Ebookrights = partybasicDetail.ebookrights,
                    Id = partybasicDetail.Id
                };
                var videoimagebank = partybasicDetail.VideoImageBank.ToList().Select(i => new
                {
                    BankType = i.Type,
                    fullname = i.Fullname,
                    CurrencySysbol = i.CurrencyMaster.Symbol,
                    Cost = i.Cost,
                    ShrotName = i.ShortName
                }).ToList();

                var PartyName = _OtherContractMaster.Table.Where(a => a.Deactivate == "N" && a.Id == _OtherContract.Id).FirstOrDefault();

                var mobj_partName = new
                {
                    PartyName = PartyName.partyname
                };


                return Json(new { mobj_partyDetails, videoimagebank, mobj_partName });
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "PartyDetailById", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "PartyDetailById", ex);
                return Json(ex.InnerException);
            }
        }

        /**************************************************************************************
         Created By  : Dheeraj Kumar sharma
         Created On  : 10th aug 2016
         Created For : getting others rights master
         *******************************************************************************************/
        //[HttpGet]
        public IHttpActionResult getOtherRightsMaster()
        {
            try
            {
                return Json(_IPermissionsInboundService.GeOtherRightsMaster().ToList().Select(i => new { Id = i.Id, RightsName = i.RightsName }).ToList());
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getOtherRightsMaster", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getOtherRightsMaster", ex);
                return Json(ex.InnerException);
            }
        }
       
        /**************************************************************************************
         Created By  : Dheeraj Kumar sharma
         Created On  : 11th Aug 2016
         Created For : Function for inserting and Updating Permission inbound process
         *******************************************************************************************/
        [HttpPost]
        public IHttpActionResult InsertPermissionsOutbound(PermissionInboundModel PermissionInboundModel)
        {
            string status = string.Empty;
            string PermissionInBoundCode = string.Empty;
            try
            {
                /**********************************************************************************************
                * Section for collecting the data for main inbound table
                ***********************************************************************************************/

                PermissionInbound _InboundTable = new PermissionInbound();
                _InboundTable.ProductId = PermissionInboundModel.ProductId;
                _InboundTable.TypeFor = PermissionInboundModel.TypeFor.ToString();
                _InboundTable.AuthorContractId = PermissionInboundModel.AuthorContractId;
                _InboundTable.ProductLicenseId = PermissionInboundModel.LicenseId;

                //if (_InboundTable.AuthorContractId != null)
                //{
                //    var mstr_Code = _PermissionInbound.Table.Where(a => a.AuthorContractId == PermissionInboundModel.AuthorContractId && a.Deactivate == "N").FirstOrDefault();
                //    if (mstr_Code != null)
                //    {
                //        PermissionInBoundCode = mstr_Code.Code;
                //    }
                //}

                //if (_InboundTable.ProductLicenseId != null)
                //{

                //    var mstr_Code = _PermissionInbound.Table.Where(a => a.ProductLicenseId == PermissionInboundModel.LicenseId && a.Deactivate == "N").FirstOrDefault();
                //    if (mstr_Code != null)
                //    {
                //        PermissionInBoundCode = mstr_Code.Code;
                //    }
                //}

                //get existing inbound code by productId
                if (_InboundTable.ProductId != null)
                {
                    var mstr_Code = _PermissionInbound.Table.Where(a => a.ProductId == PermissionInboundModel.ProductId && a.Deactivate == "N").FirstOrDefault();
                    if (mstr_Code != null)
                    {
                        PermissionInBoundCode = mstr_Code.Code;
                    }
                }


                if (PermissionInBoundCode != "")
                {
                    _InboundTable.Code = PermissionInBoundCode;
                }
                else
                {
                    _InboundTable.Code = GenerateSeriesCode("InboundCode", "IP");
                }

                _InboundTable.AssetsType = PermissionInboundModel.AssetsType;
                _InboundTable.EnteredBy = PermissionInboundModel.EnteredBy;

                _InboundTable.Deactivate = "N";
                _InboundTable.EntryDate = DateTime.Now;

                /**********************************************************************************************
                * Section for collecting the data for the link tables
                ***********************************************************************************************/
                if (_InboundTable.AssetsType == "B" || _InboundTable.AssetsType == "O")
                {
                    PermissionInboundOthers _InboundOther = new PermissionInboundOthers();
                    IList<PermissionInboundOthers> _InboundOtherList = new List<PermissionInboundOthers>();
                    var InboundOtherModel = PermissionInboundModel.PermissionsInboundDataModel;
                    _InboundOther.AssetSubTypeId = InboundOtherModel.AssetSubType == 0 ? null : (int?)InboundOtherModel.AssetSubType;
                    _InboundOther.AssetDescription = InboundOtherModel.AssetDescription;
                    _InboundOther.statusId = InboundOtherModel.Status;
                    _InboundOther.Restriction = InboundOtherModel.Restriction;
                    _InboundOther.SubLicensing = InboundOtherModel.SubLicensing;
                    _InboundOther.Fee = InboundOtherModel.Fee;
                    _InboundOther.CurrencyId = InboundOtherModel.CurrencyValue;
                    _InboundOther.Acknowledgementline = InboundOtherModel.Acknowledgementline;
                    _InboundOther.InboundRemarks = InboundOtherModel.InboundRemarks;
                    _InboundOther.Extent = InboundOtherModel.Extent;
                    _InboundOther.Gratiscopytobesent = InboundOtherModel.Gratiscopytobesent;
                    _InboundOther.Noofcopy = InboundOtherModel.Noofcopy;
                    _InboundOther.OriginalSource = InboundOtherModel.OriginalSource;
                    _InboundOther.InvoiceNumber = InboundOtherModel.InvoiceNumber;
                    _InboundOther.PermissionExpirydate = InboundOtherModel.PermissionExpirydate;
                    _InboundOther.EntryDate = DateTime.Now;
                    _InboundOther.EnteredBy = PermissionInboundModel.EnteredBy;
                    _InboundOther.Deactivate = "N";
                    _InboundOther.TerritoryRightsId = PermissionInboundModel.PermissionsInboundDataModel.TerritoryRights;

                    IList<OtherContractDateRequest> _OtherContractDateRequestList = new List<OtherContractDateRequest>();
                    foreach (var lst in PermissionInboundModel.DateRequestObject)
                    {
                        OtherContractDateRequest _OtherContractDateRequest = new OtherContractDateRequest();
                        _OtherContractDateRequest.dateOf = lst.DateOf;
                        _OtherContractDateRequest.dateValue = lst.DateValue;
                        _OtherContractDateRequest.EntryDate = DateTime.Now;
                        _OtherContractDateRequest.Deactivate = "N";
                        _OtherContractDateRequest.EnteredBy = PermissionInboundModel.EnteredBy;
                        _OtherContractDateRequestList.Add(_OtherContractDateRequest);
                    }
                    IList<PermissionInboundOthersRightsLink> _PermissionRightsObjectList = new List<PermissionInboundOthersRightsLink>();

                    foreach (var lst in PermissionInboundModel.PermissionRightsObject)
                    {
                        PermissionInboundOthersRightsLink _PermissionRightsObject = new PermissionInboundOthersRightsLink();
                        _PermissionRightsObject.RightsId = lst.RightsId.GetValueOrDefault();
                        _PermissionRightsObject.status = lst.Status;
                        _PermissionRightsObject.Number = lst.Number;
                        _PermissionRightsObject.RunGranted = lst.RunGranted;
                        _PermissionRightsObject.EntryDate = DateTime.Now;
                        _PermissionRightsObject.Deactivate = "N";
                        _PermissionRightsObject.EnteredBy = PermissionInboundModel.EnteredBy;
                        _PermissionRightsObjectList.Add(_PermissionRightsObject);
                    }
                    /**********************************************************************************************
                     * Section for dump copyright holder data into Inbound process rtable data to maintain history
                     ***********************************************************************************************/
                    IList<PermissionInboundCopyRightHolderMaster> _CpyHolderLst = new List<PermissionInboundCopyRightHolderMaster>();
                    PermissionInboundCopyRightHolderMaster RightHolder = new PermissionInboundCopyRightHolderMaster();
                    CopyRightHolderMaster holder = new CopyRightHolderMaster();
                    if (InboundOtherModel.CopyRightHolder != 0)
                    {
                        holder = _IPermissionsInboundService.getCopyRightHolderById(InboundOtherModel.CopyRightHolder);
                        CopyClass.CopyObject(holder, ref  RightHolder);
                        RightHolder.EnteredBy = PermissionInboundModel.EnteredBy;
                        RightHolder.EntryDate = DateTime.Now;
                        RightHolder.Deactivate = "N";
                        _CpyHolderLst.Add(RightHolder);
                        // _InboundTable.PermissionInboundCopyRightHolderMaster = _CpyHolderLst;
                    }

                    _InboundOther.OtherContractDateRequest = _OtherContractDateRequestList;
                    _InboundOther.PermissionInboundOthersRightsLink = _PermissionRightsObjectList;
                    _InboundOtherList.Add(_InboundOther);
                    _InboundOther.PermissionInboundCopyRightHolderMaster = _CpyHolderLst;
                    _InboundTable.PermissionInboundOthers = _InboundOtherList;
                }

                /**********************************************************************************************
              * Section for collecting the data for the link tables image video bank
              ***********************************************************************************************/
                if (_InboundTable.AssetsType == "B" || _InboundTable.AssetsType == "I")
                {
                   
                    PermissionInboundImageVideoBank _ImageVideoBank = new PermissionInboundImageVideoBank();
                    IList<PermissionInboundImageVideoBank> _ImageVideoBankList = new List<PermissionInboundImageVideoBank>();
                    //_ImageVideoBank.ImageBankId = PermissionInboundModel.ImageBankId;
                    //_ImageVideoBank.EnteredBy = PermissionInboundModel.EnteredBy;
                    //_ImageVideoBank.Deactivate = "N";
                    //_ImageVideoBank.EntryDate = DateTime.Now;
                    //_ImageVideoBankList.Add(_ImageVideoBank);
                    List<PermissionInboundImageVideoBankData> _list = new List<PermissionInboundImageVideoBankData>();
                    /*********************************************************************************************************************
                     * Section will used to upload data from excel validate if valid then insert other wise return CSV of invalid content
                     ************************************************************************************************************************/
                    if (PermissionInboundModel.DocFileName != null)
                    {                        
                        _docDataTble = CreateDataTableFromExcel(PermissionInboundModel.DocFileName);
                       
                        /**************************************************************************************************************************************************
                        * Checked weather datatable input is invalid or not if invalid then do not insert any record and send back with appropiate reasons against each row
                        ***************************************************************************************************************************************************/
                        DataTable _validateDataTable = ValidateDatatable(_docDataTble);
                        if (_validateDataTable.Rows.Count > 0)
                        {
                            status = "E";
                            return Json(new { status, _validateDataTable });
                        }                       


                /*********************************************************************
                 End Excel Section
                 *********************************************************************/
                        // _docDataTble.Select(r => _docDataTble.Columns.ToDictionary(c => c.ColumnName, c => r[c])).ToList();
                    }
                 //   _ImageVideoBank.PermissionInboundImageVideoBankData = _list;
                  //_InboundTable.PermissionInboundImageVideoBank = _ImageVideoBankList;
                }
                /**********************************************************************************************
                *Finally insert the data into the main table as well as all the link tables using main table service
                ***********************************************************************************************/
                using (var scope = new System.Transactions.TransactionScope())
                {
                    int PermissionInBoundId = _IPermissionsInboundService.InsertPermissionInboundData(_InboundTable);
                    
                    if (_InboundTable.AssetsType == "B" || _InboundTable.AssetsType == "I")
                    {

                        int countLength = _docDataTble.Rows.Count;
                        int newCount = countLength - 1;

                        foreach (DataRow row in _docDataTble.Rows)
                        {
                            PermissionInboundImageVideoBank _ImageVideoBank = new PermissionInboundImageVideoBank();

                            //int id = _IPermissionsInboundService.GetisValidPartyName(row[16].ToString());
                            ////----rearrange excel column on 27 dec 2017
                            int id = _IPermissionsInboundService.GetisValidPartyName(row[1].ToString());

                            if (id != 0)
                            {
                                _ImageVideoBank.ImageBankId = id;
                                _ImageVideoBank.PermissionInboundId = PermissionInBoundId;

                                _ImageVideoBank.EnteredBy = PermissionInboundModel.EnteredBy;
                                _ImageVideoBank.Deactivate = "N";
                                _ImageVideoBank.EntryDate = DateTime.Now;

                                int ImageVideoBankLinkId = _IPermissionsInboundService.InsertPermissionInboundImageVideoBankLink(_ImageVideoBank);

                                if (ImageVideoBankLinkId != null)
                                {
                                    PermissionInboundImageVideoBankData _ImageVideoBankData = new PermissionInboundImageVideoBankData();
                                                                       
                                    //_ImageVideoBankData.ContractTypes = row[0].ToString();
                                    //_ImageVideoBankData.imagevideobankid = row[1].ToString();
                                    //_ImageVideoBankData.Description = row[2].ToString();
                                    //_ImageVideoBankData.invoiceno = row[3].ToString();
                                    //_ImageVideoBankData.invoicevalue = Convert.ToDouble(row[4]);

                                    //if (row[5].ToString() != "" && row[6].ToString() != "" && row[7].ToString() != "")
                                    //{
                                    //    string date = addZero(row[5].ToString()) + "/" + addZero(row[6].ToString()) + "/" + addZero(row[7].ToString());

                                    //    _ImageVideoBankData.invoicedate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    //}
                                    //else
                                    //{
                                    //    _ImageVideoBankData.invoicedate = (DateTime?)null;
                                    //}
                                    
                                    //_ImageVideoBankData.printquantity = Convert.ToInt16(row[8].ToString() != "" ? row[8] : 0) == 0 ? (int?)null : Convert.ToInt16(row[8]);

                                    //if (row[9].ToString() != "" && row[10].ToString() != "" && row[11].ToString() != "")
                                    //{
                                    //    string date = addZero(row[9].ToString()) + "/" + addZero(row[10].ToString()) + "/" + addZero(row[11].ToString());

                                    //    _ImageVideoBankData.permissionexpirydate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    //}
                                    //else
                                    //{
                                    //    _ImageVideoBankData.permissionexpirydate = (DateTime?)null;
                                    //}

                                    //_ImageVideoBankData.weblink = row[12].ToString();
                                    //_ImageVideoBankData.creditlines = row[13].ToString();
                                    //_ImageVideoBankData.Remarks = row[14].ToString();
                                    //_ImageVideoBankData.EditorialonlyType = row[15].ToString();
                                    //_ImageVideoBankData.usage = row[17].ToString() == "" ? null : row[17].ToString();

                                    //_ImageVideoBankData.ImageBankPartyId = _IPermissionsInboundService.GetisValidPartyName(row[16].ToString());
                                    //_ImageVideoBankData.IVBId = ImageVideoBankLinkId;

                                    //_ImageVideoBankData.CurrencyId = _IPermissionsInboundService.GetisValidCurrency(row[18].ToString());


                                    ////----start rearrange excel column on 27 dec 2017
                                    _ImageVideoBankData.ContractTypes = row[0].ToString();

                                    _ImageVideoBankData.ImageBankPartyId = _IPermissionsInboundService.GetisValidPartyName(row[1].ToString());
                                    _ImageVideoBankData.IVBId = ImageVideoBankLinkId;

                                    _ImageVideoBankData.imagevideobankid = row[2].ToString();
                                    _ImageVideoBankData.Description = row[3].ToString();
                                    _ImageVideoBankData.creditlines = row[4].ToString();
                                    _ImageVideoBankData.EditorialonlyType = row[5].ToString();
                                    _ImageVideoBankData.invoiceno = row[6].ToString();
                                    _ImageVideoBankData.invoicevalue = Convert.ToDouble(row[7]);

                                    _ImageVideoBankData.CurrencyId = _IPermissionsInboundService.GetisValidCurrency(row[8].ToString());

                                    if (row[9].ToString() != "" && row[10].ToString() != "" && row[11].ToString() != "")
                                    {
                                        string date = addZero(row[9].ToString()) + "/" + addZero(row[10].ToString()) + "/" + addZero(row[11].ToString());

                                        _ImageVideoBankData.invoicedate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        _ImageVideoBankData.invoicedate = (DateTime?)null;
                                    }

                                    _ImageVideoBankData.printquantity = Convert.ToInt16(row[12].ToString() != "" ? row[12] : 0) == 0 ? (int?)null : Convert.ToInt16(row[12]);

                                    if (row[13].ToString() != "" && row[14].ToString() != "" && row[15].ToString() != "")
                                    {
                                        string date = addZero(row[13].ToString()) + "/" + addZero(row[14].ToString()) + "/" + addZero(row[15].ToString());

                                        _ImageVideoBankData.permissionexpirydate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    }
                                    else
                                    {
                                        _ImageVideoBankData.permissionexpirydate = (DateTime?)null;
                                    }

                                    _ImageVideoBankData.weblink = row[16].ToString();
                                    _ImageVideoBankData.usage = row[17].ToString() == "" ? null : row[17].ToString();
                                    _ImageVideoBankData.Remarks = row[18].ToString();

                                    ////----end rearrange excel column on 27 dec 2017

                                    _ImageVideoBankData.EntryDate = DateTime.Now;
                                    _ImageVideoBankData.EnteredBy = PermissionInboundModel.EnteredBy;
                                    _ImageVideoBankData.Deactivate = "N";

                                    _IPermissionsInboundService.InsertNewPermissionInboundImageVideoBankData(_ImageVideoBankData);

                                }

                            }

                            // }

                        }

                    }

                    scope.Complete();

                    status = "OK";
                    string code = _InboundTable.Code;
                    return Json(new { status, code });
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "InsertPermissionsOutbound", ex);
                status = ex.ToString();
                return Json(new { status });
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "InsertPermissionsOutbound", ex);
                status = ex.ToString();
                return Json(new { status });
            }


        }
       
        /**************************************************************************************
         Created By  : Dheeraj Kumar sharma
         Created On  : 17th aug 2016
         Created For : Function for inserting more copyright holder for inbound permission and also othres details
         *******************************************************************************************/
        [HttpPost]
        public IHttpActionResult postCopyRightHolderData(PermissionInboundModel PermissionInboundModel)
        {
            try
            {
                string status = string.Empty;
                PermissionInboundOthers _InboundOther = new PermissionInboundOthers();
                IList<PermissionInboundOthers> _InboundOtherList = new List<PermissionInboundOthers>();
                PermissionInbound _InboundTable = new PermissionInbound();
                _InboundTable = _IPermissionsInboundService.getInboundPermissionDetailsByCode(PermissionInboundModel.PermissionsInboundDataModel.Code).Where(i => i.AssetsType == "O" || i.AssetsType == "B").FirstOrDefault();
                var InboundOtherModel = PermissionInboundModel.PermissionsInboundDataModel;
                _InboundOther.AssetSubTypeId = InboundOtherModel.AssetSubType == 0 ? null : (int?)InboundOtherModel.AssetSubType;
                _InboundOther.AssetDescription = InboundOtherModel.AssetDescription;
                _InboundOther.statusId = InboundOtherModel.Status;
                _InboundOther.Restriction = InboundOtherModel.Restriction;
                _InboundOther.SubLicensing = InboundOtherModel.SubLicensing;
                _InboundOther.Fee = InboundOtherModel.Fee;
                _InboundOther.CurrencyId = InboundOtherModel.CurrencyValue;
                _InboundOther.Acknowledgementline = InboundOtherModel.Acknowledgementline;
                _InboundOther.InboundRemarks = InboundOtherModel.InboundRemarks;
                _InboundOther.Extent = InboundOtherModel.Extent;
                _InboundOther.Gratiscopytobesent = InboundOtherModel.Gratiscopytobesent;
                _InboundOther.Noofcopy = InboundOtherModel.Noofcopy;
                _InboundOther.OriginalSource = InboundOtherModel.OriginalSource;
                _InboundOther.InvoiceNumber = InboundOtherModel.InvoiceNumber;
                _InboundOther.PermissionExpirydate = InboundOtherModel.PermissionExpirydate;
                _InboundOther.EntryDate = DateTime.Now;
                _InboundOther.EnteredBy = PermissionInboundModel.EnteredBy;
                _InboundOther.PermissionInboundId = _InboundTable.Id;
                _InboundOther.Deactivate = "N";
                _InboundOtherList.Add(_InboundOther);
                IList<OtherContractDateRequest> _OtherContractDateRequestList = new List<OtherContractDateRequest>();
                foreach (var lst in PermissionInboundModel.DateRequestObject)
                {
                    OtherContractDateRequest _OtherContractDateRequest = new OtherContractDateRequest();
                    _OtherContractDateRequest.dateOf = lst.DateOf;
                    _OtherContractDateRequest.dateValue = lst.DateValue;
                    _OtherContractDateRequest.EntryDate = DateTime.Now;
                    _OtherContractDateRequest.Deactivate = "N";
                    _OtherContractDateRequest.EnteredBy = PermissionInboundModel.EnteredBy;
                    _OtherContractDateRequestList.Add(_OtherContractDateRequest);
                }
                IList<PermissionInboundOthersRightsLink> _PermissionRightsObjectList = new List<PermissionInboundOthersRightsLink>();

                foreach (var lst in PermissionInboundModel.PermissionRightsObject)
                {
                    PermissionInboundOthersRightsLink _PermissionRightsObject = new PermissionInboundOthersRightsLink();
                    _PermissionRightsObject.RightsId = lst.RightsId.GetValueOrDefault();
                    _PermissionRightsObject.status = lst.Status;
                    _PermissionRightsObject.Number = lst.Number;
                    _PermissionRightsObject.RunGranted = lst.RunGranted;
                    _PermissionRightsObject.EntryDate = DateTime.Now;
                    _PermissionRightsObject.Deactivate = "N";
                    _PermissionRightsObject.EnteredBy = PermissionInboundModel.EnteredBy;
                    _PermissionRightsObjectList.Add(_PermissionRightsObject);
                }
                _InboundOther.OtherContractDateRequest = _OtherContractDateRequestList;
                _InboundOther.PermissionInboundOthersRightsLink = _PermissionRightsObjectList;
                /**********************************************************************************************
                 * Section for dump copyright holder data into Inbound process rtable data to maintain history
                 ***********************************************************************************************/

                PermissionInboundCopyRightHolderMaster RightHolder = new PermissionInboundCopyRightHolderMaster();
                IList<PermissionInboundCopyRightHolderMaster> _RightHolderList = new List<PermissionInboundCopyRightHolderMaster>();
                CopyRightHolderMaster holder = new CopyRightHolderMaster();
                if (InboundOtherModel.CopyRightHolder != 0)
                {
                    holder = _IPermissionsInboundService.getCopyRightHolderById(InboundOtherModel.CopyRightHolder);
                    CopyClass.CopyObject(holder, ref  RightHolder);
                    RightHolder.EnteredBy = PermissionInboundModel.EnteredBy;
                    RightHolder.EntryDate = DateTime.Now;
                    RightHolder.Deactivate = "N";
                    _RightHolderList.Add(RightHolder);
                    // RightHolder.PermissionInboundId = PermissionInboundModel.PermissionsInboundDataModel.InboundId;
                }
                _InboundOther.PermissionInboundCopyRightHolderMaster = _RightHolderList;
                /**********************************************************************************************
                    *Finally insert the data into the main table as well as all the link tables using main table service
                    ***********************************************************************************************/
                using (var scope = new System.Transactions.TransactionScope())
                {
                     _InboundTable.ModifiedBy = PermissionInboundModel.EnteredBy;
                    _InboundTable.ModifiedDate = DateTime.Now;
                    _InboundTable.PermissionInboundOthers = _InboundOtherList;
                    _IPermissionsInboundService.UpdatePermissionInbound(_InboundTable);
                    scope.Complete();
                    status = "OK";

                }
                return Json(status);
            }
            catch (Exception Ex)
            {
                var status = Ex.ToString();
                return Json(status);
            }


        }
       
        /**********************************************************************************************
         Function for converting excel data into the datatable
        ***********************************************************************************************/
        public DataTable CreateDataTableFromExcel(string mstr_file)
        {
            //Fetch the file path from application set
            IList<ApplicationSetUp> _ApplicationSetUpList = _ApplicationSetUp.Table.Where(x => x.key == "ExcelFilePath" && x.Deactivate == "N").ToList();
            var ExcelPath = _ApplicationSetUpList.Select(Au => new
            {
                ExcelUploadPath = Au.keyValue,

            });

            string URL = ExcelPath.FirstOrDefault().ExcelUploadPath;
            OleDbConnection mobj_Conn = new OleDbConnection();
            OleDbDataAdapter mobj_dtAdapter = default(OleDbDataAdapter);
            DataTable mobj_dt = new DataTable();
            DataTable mobj_dtSchema = new DataTable();
            DataSet mobj_ds = new DataSet();
            string _CmplteUrl = string.Empty;
            _CmplteUrl = URL + "/" + mstr_file;

            try
            {

                if ((mstr_file.Length > 4))
                {
                    if ((mstr_file.Substring(mstr_file.Length - 4, 4) == ".xls"))
                    {
                        mobj_Conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + _CmplteUrl + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'");
                    }
                }
                if ((mstr_file.Length > 5))
                {
                    if ((mstr_file.Substring(mstr_file.Length - 5, 5) == ".xlsx"))
                    {
                        mobj_Conn = new OleDbConnection("Provider= Microsoft.ACE.OLEDB.12.0;Data Source=" + _CmplteUrl + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1\\'");
                    }
                }

                mobj_Conn.Open();


                mobj_dtSchema = mobj_Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (mobj_dtSchema == null || mobj_dtSchema.Rows.Count < 1)
                {
                    throw new Exception("Error: Could not determine the name of the first worksheet.");
                }

                string firstSheetName = mobj_dtSchema.Rows[0]["TABLE_NAME"].ToString();

                mobj_dtAdapter = new OleDbDataAdapter("select * from [" + firstSheetName + "]", mobj_Conn);
                mobj_dtAdapter.Fill(mobj_ds, mstr_file);
                mobj_dt = mobj_ds.Tables[0];
                mobj_dtAdapter.Dispose();
                mobj_Conn.Close();
                mobj_Conn.Dispose();

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "CreateDataTableFromExcel", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "CreateDataTableFromExcel", ex);
            }

            return mobj_dt;
            //*** Return DataTable ***'
        }

        public DateTime? FormatPostingDate(string str)
        {
            if (str != null && str != string.Empty)
            {
                DateTime postingDate = Convert.ToDateTime(str);
                return string.Format("{0:MM/dd/yyyy}", postingDate).toDateTime();
            }
            return null;
        }
        
        public IHttpActionResult getAssetSubType()
        {
            try
            {
                var piOther = _PermissionInboundOthers.Table.Where(o => o.Deactivate == "N").Select(o => o.AssetSubTypeId).Distinct().ToList();

                IList<AssetSubType> mobj_AssetType = _AssetSubType.Table.Where(a => a.Deactivate == "N").ToList();

                var mobj_AssetSubTypeValue = mobj_AssetType.Select(AST => new
                    {
                        Id = AST.Id,
                        AssetName = AST.AssetName,

                        flag = piOther.Where(r => r == AST.Id).Select(r => r).FirstOrDefault() != null ? 1 : 0,

                    }).Distinct().OrderBy(a => a.AssetName);

                return Json(mobj_AssetSubTypeValue);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        public IHttpActionResult getStatus()
        {
            try
            {
                var piOther = _PermissionInboundOthers.Table.Where(o => o.Deactivate == "N").Select(o => o.statusId).Distinct().ToList();

                IList<StatusMaster> mobj_Status = _StatusMaster.Table.Where(a => a.Deactivate == "N").ToList();
                var mobj_StatusVlaue = mobj_Status.Select(SM => new
                    {
                        Id = SM.Id,
                        Status = SM.Status,

                        flag = piOther.Where(r => r == SM.Id).Select(r => r).FirstOrDefault() != 0 ? 1 : 0,

                    }).Distinct().OrderBy(a => a.Status);
                return Json(mobj_StatusVlaue);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            return null;
        }
        
        public IHttpActionResult getCopyRightHolder()
        {
            IList<CopyRightHolderMaster> mobj_CopyRightHolder = _CopyRightHolderMaster.Table.Where(a => a.Deactivate == "N").ToList();
            var mobj_CopyRightHolderValue = mobj_CopyRightHolder.Select(CRH => new
            {
                Id = CRH.Id,
                CopyRightHolderName = CRH.CopyRightHolderName
            }
                ).Distinct().OrderBy(a => a.CopyRightHolderName);
            return Json(mobj_CopyRightHolderValue);

        }

        public IHttpActionResult CopyRightHolderById(CopyRightHolderMaster _CopyRightHolder)
        {
            CopyRightHolderMaster _CopyRight = _CopyRightHolderMaster.Table.Where(a => a.Deactivate == "N" && a.Id == _CopyRightHolder.Id).FirstOrDefault();
            return Json(_CopyRight);
        }

        public IHttpActionResult UpdateCopyRightHolderById(PermissionInboundCopyRightHolderMaster _CopyRightHolder)
        {
            PermissionInboundCopyRightHolderMaster CopyRight = _PermissionInboundCopyRightHolderMaster.Table.Where(a => a.Deactivate == "N" && a.Id == _CopyRightHolder.Id).FirstOrDefault();

            var _CopyRight = new {

                ContactPerson = CopyRight.ContactPerson,
                CopyRightHolderCode = CopyRight.CopyRightHolderCode,
                Mobile = CopyRight.Mobile,
                CopyRightHolderAddress = CopyRight.Address,
                Email = CopyRight.Email,
                URL = CopyRight.URL,
                AccountNo = CopyRight.AccountNo,
                BankName = CopyRight.BankName,
                BankAddress = CopyRight.BankAddress,
                IFSCCode = CopyRight.IFSCCode,


                PANNo = CopyRight.PANNo,
                Pincode = CopyRight.Pincode,
                CountryId = CopyRight.CountryId,
                Stateid = CopyRight.Stateid,
                Cityid = CopyRight.Cityid,


                CopyRightHolderName = CopyRight.CopyRightHolderName,
            };

            return Json(_CopyRight);
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

        /// <summary>
        ///this function will validate the excel data table and insert if all record are valid otherwise return excel with reason 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">/this function will validate the excel data table and insert if all record are valid otherwise return excel with reason </param>
        /// <param name="destObject">datatable will be returned</param>
        public DataTable ValidateDatatable(DataTable _dt)
        {
            DataTable _resultDT = new DataTable();
            _resultDT = _dt.Copy();
            _resultDT.Columns.Add("Reason");
            int _rowCount = 0;
            int flag = 0;
            foreach (DataRow row in _dt.Rows)
            {
                string _reason = string.Empty;
                int index = 0;

                int invoiceDay = 0;
                int invoiceMonth = 0;
                int invoiceYear = 0;

                int PermissionExpiryDay = 0;
                int PermissionExpiryMonth = 0;
                int PermissionExpiryYear = 0;

                foreach (DataColumn column in _dt.Columns)
                {

                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("invoicevalue") > -1) // This will check the null values also (if you want to check).
                    {

                        if (IsValidInput(row[column].ToString()) == false)
                        {
                            index++;
                            flag = 1;
                            _reason = _reason + index.ToString() + ". Invoice value is not in correct format(only integer and decimal value is accepted).\n";
                        }
                    }

                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("invoiceday") > -1) // This will check the null values also (if you want to check).
                    {

                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                            invoiceDay = Convert.ToInt32(row[column].ToString());
                        }
                    }
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("invoicemonth") > -1) // This will check the null values also (if you want to check).
                    {

                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                            invoiceMonth = Convert.ToInt32(row[column].ToString());
                        }
                    }
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("invoiceyear") > -1) // This will check the null values also (if you want to check).
                    {
                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                            invoiceYear = Convert.ToInt32(row[column].ToString());
                        }
                    }

                    //if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("invoicedate") > -1)
                    //{
                    //    if (row[column].ToString().Trim() != "")
                    //    {

                    //        if (row[column].ToString().Contains("-"))
                    //        {
                    //            if (IsValidDate(row[column].ToString().Replace("-", "/")) == false)
                    //            {
                    //                flag = 1;
                    //                index++;
                    //                _reason = _reason + index.ToString() + ". Invoice date is not in correct format(dd/mm/yyyy) or (dd-mm-yyyy).\n";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (IsValidDate(row[column].ToString()) == false)
                    //            {
                    //                flag = 1;
                    //                index++;
                    //                _reason = _reason + index.ToString() + ". Invoice date is not in correct format(dd/mm/yyyy) or (dd-mm-yyyy).\n";
                    //            }

                    //        }
                    //    }
                    
                    //}
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("printquantity") > -1)
                    {
                        if (row[column].ToString().Trim() != "")
                        {
                            if (IsValidInput(row[column].ToString()) == false)
                            {
                                flag = 1;
                                index++;
                                _reason = _reason + index.ToString() + ". Print Quantity is not in valid format(only numeric is accepted).\n";
                            }
                        }
                    }

                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("permissionexpiryday") > -1) // This will check the null values also (if you want to check).
                    {

                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                                PermissionExpiryDay = Convert.ToInt32(row[column].ToString());
                        }
                    }
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("permissionexpirymonth") > -1) // This will check the null values also (if you want to check).
                    {

                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                                PermissionExpiryMonth = Convert.ToInt32(row[column].ToString());
                        }
                    }
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("permissionexpiryyear") > -1) // This will check the null values also (if you want to check).
                    {
                        if (IsValidInput(row[column].ToString()) == true)
                        {
                            if (row[column].ToString() != "")
                                PermissionExpiryYear = Convert.ToInt32(row[column].ToString());
                        }
                    }

                    //if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("permissionexpirydate") > -1)
                    //{
                    //    if (row[column].ToString().Trim() != "")
                    //    {


                    //        if (row[column].ToString().Contains("-"))
                    //        {
                    //            if (IsValidDate(row[column].ToString().Replace("-", "/")) == false)
                    //            {
                    //                flag = 1;
                    //                index++;
                    //                _reason = _reason + index.ToString() + ". Permission Expiry date is not in correct format(dd/mm/yyyy) or (dd-mm-yyyy).\n";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (IsValidDate(row[column].ToString()) == false)
                    //            {
                    //                flag = 1;
                    //                index++;
                    //                _reason = _reason + index.ToString() + ". Permission Expiry date is not in correct format(dd/mm/yyyy) or (dd-mm-yyyy).\n";
                    //            }

                    //        }



                    //        //if (IsValidDate(row[column].ToString()) == false)
                    //        //{
                    //        //    flag = 1;
                    //        //    index++;
                    //        //    _reason = _reason + index.ToString() + ". Permission Expiry date is not in correct format(dd/mm/yyyy).\n";
                    //        //}
                    //    }
                    //}

                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("weblink") > -1)
                    {

                        if (Is_Valid_Url(row[column].ToString()) == false)
                        {
                            flag = 1;
                            index++;
                            _reason = _reason + index.ToString() + ". Web link is not in correct format.\n";
                        }
                    }

                    //if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("partyname") > -1)
                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("vendorname") > -1)
                    {
                        int id = _IPermissionsInboundService.GetisValidPartyName(row[column].ToString());

                        if (id  == 0)
                        {
                            flag = 1;
                            index++;
                            _reason = _reason + index.ToString() + ". Party name is not correct.\n";
                        }
                    }

                    if (column.ToString().Trim().Replace(" ", string.Empty).ToLower().IndexOf("currency") > -1)
                    {
                        int id = _IPermissionsInboundService.GetisValidCurrency(row[column].ToString());


                        if (id  == 0)
                        {
                            flag = 1;
                            index++;
                            _reason = _reason + index.ToString() + ". Enter valid Currency.\n";
                        }
                    }

                }

                if (invoiceDay == 0 && invoiceMonth == 0 && invoiceYear == 0)
                {
                }
                else if (invoiceDay != 0 && invoiceMonth != 0 && invoiceYear != 0)
                {
                    if (IsValidDate(addZero(invoiceDay.ToString()) + "/" + addZero(invoiceMonth.ToString()) + "/" + addZero(invoiceYear.ToString())) == false)
                    {
                        flag = 1;
                        index++;
                        _reason = _reason + index.ToString() + ". Invoice date is not valid.\n";
                    }
                }
                else
                {
                    flag = 1;
                    index++;
                    _reason = _reason + index.ToString() + ". Invoice date is not valid.\n";
                }

                if (PermissionExpiryDay == 0 && PermissionExpiryMonth == 0 && PermissionExpiryYear == 0)
                {
                }
                else if (PermissionExpiryDay != 0 && PermissionExpiryMonth != 0 && PermissionExpiryYear != 0)
                {
                    if (IsValidDate(addZero(PermissionExpiryDay.ToString()) + "/" + addZero(PermissionExpiryMonth.ToString()) + "/" + addZero(PermissionExpiryYear.ToString())) == false)
                    {
                        flag = 1;
                        index++;
                        _reason = _reason + index.ToString() + ". Permission Expiry date is not valid.\n";
                    }
                }
                else
                {
                    flag = 1;
                    index++;
                    _reason = _reason + index.ToString() + ". Permission Expiry date is not valid.\n";
                }

                _resultDT.Rows[_rowCount]["Reason"] = _reason;
                _reason = string.Empty;
                _rowCount++;
            }
            if (flag != 1)
            {
                _resultDT.Rows.OfType<DataRow>().ToList()
                     .ForEach(r => r.Delete());
                _resultDT.AcceptChanges();
            }

            return _resultDT;
        }
       
        /// <summary>
        ///function to validate valid integer or double value
        /// </summary>
        /// 
        private bool IsValidInput(string Number)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            return regex.IsMatch(Number);
        }
        
        /// <summary>
        ///function to validate valid date in DD/MM/YYYY format
        /// </summary>
        /// 
        private bool IsValidDate(string Date)
        {

         
           //var regex = new Regex(@"(((0|1)[1-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
            //var regex = new Regex(@"/^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$/");
            //  var regex = new Regex(@"([0-9]{4}[\](0[1-9]|1[0-2])[\]([0-2]{1}[0-9]{1}|3[0-1]{1})|([0-2]{1}[0-9]{1}|3[0-1]{1})[\](0[1-9]|1[0-2])[\][0-9]{4})");

            var regex = new Regex(@"^([0]?[0-9]|[12][0-9]|[3][01])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$");
            bool ret = regex.IsMatch(Date);
            if (ret == false)
                return ret;
            else
            {
                try
                {
                    DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }

        private string addZero(string num)
        {
            if (Convert.ToInt32(num) < 10)
            {
                return "0" + num.ToString();
            }
            else
                return num.ToString();
        }
               
        /// <summary>
        /// function to validate url 
        /// </summary>
        ///
        private bool Is_Valid_Url(string WebUrl)
        {
            return Regex.IsMatch(WebUrl, @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$");
        }

        /// <summary>
        /// Function for fetching details of inbound permission details and copyright holder
        /// </summary>
        ///

        [HttpGet]
        public IHttpActionResult getCopyRightHolderById(string code)
        {
            IList<PermissionInbound> PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(code).ToList();
            IList<PermissionInboundOthers> _OtherContractImageBank = new List<PermissionInboundOthers>();
            IList<PermissionInboundCopyRightHolderMaster> _CpyhlderData = new List<PermissionInboundCopyRightHolderMaster>();
            IList list = new ArrayList();
            IList partyDetails = new ArrayList();
            var InboundDetails = PermissionInbound.FirstOrDefault();
            var InboundObject = new
            {
                id = InboundDetails.Id,
                Code = InboundDetails.Code,
                ProductId = InboundDetails.ProductId,
                ProductCode = InboundDetails.ProductMaster.ProductCode,
                InboundType = InboundDetails.TypeFor,
                InboundForId =  "", // InboundDetails.TypeFor == "C" ? InboundDetails.AuthorContractId : InboundDetails.ProductLicenseId,
                TypeForCode = "", // InboundDetails.TypeFor == "C" ? InboundDetails.AuthorContractOriginal.AuthorContractCode.ToString() : InboundDetails.ProductLicense.ProductLicensecode.ToString(),
                AssestsType = InboundDetails.AssetsType,
                Isbn= InboundDetails.ProductMaster.OUPISBN
            };
            
            foreach (var item in PermissionInbound)
            {
                var _obj = item.PermissionInboundOthers.ToList();
                if (_obj != null)
                {
                    foreach(var cyhlder in _obj)
                    {
                        var _cpyhlderdata = cyhlder.PermissionInboundCopyRightHolderMaster.ToList().Select(d => new
                        {
                            Id = d.Id,
                            CopyRightHolderCode = d.CopyRightHolderCode,
                            CopyRightHolderName = d.CopyRightHolderName,
                            ContactPerson = d.ContactPerson,
                            Mobile = d.Mobile,
                            Email = d.Email,
                            PermissionInboundId = InboundDetails.Id,
                            code = InboundDetails.Code,
                        });

                        list.Add(_cpyhlderdata);
                    }
            } 

            }
            return Json(new { InboundObject,list});
             
         }
        
        /// <summary>
        /// Function for getting copyright holder details which is not used in inbound permission
        /// </summary>
        ///
        [HttpGet]
        public IHttpActionResult getNotUsedCopyRightHolder(string cpyIds)
        {
            var _list = _IPermissionsInboundService.getCopyRightHolderNotUsed(cpyIds).Select(i => new
            {
                Id = i.Id,
                CopyRightHolderCode = i.CopyRightHolderCode,
                CopyRightHolderName = i.CopyRightHolderName,
                ContactPerson = i.ContactPerson,
                Mobile = i.Mobile,
                Email = i.Email
            }).OrderBy(a => a.CopyRightHolderName);
            return Json(new { _list });
        }
        
        /// <summary>
        /// Function for Inserting search history into the database
        /// </summary>
        ///
        [HttpPost]
        public IHttpActionResult InsertIntoSearchHistory(PermissionInboundSearchHistory PermissionsInboundModel)
        {
            try
            {
                PermissionsInboundModel.EntryDate = DateTime.Now;
                PermissionsInboundModel.Deactivate = "N";
                _IPermissionsInboundService.PermissionInboundSearchHistory(PermissionsInboundModel);
                return Json("OK");
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }

        /// <summary>
        /// Function for getting desired result based on searching
        /// </summary>
        ///

        public IHttpActionResult getInboundPermissionSearchResult(String SessionId)
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
                var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInboundSearchModel>("Proc_getInboundPermissionResult_get", parameters).ToList();
                return Json(_GetAuthorReport);
            }
        }

        public IHttpActionResult getInboundPermissionSearchResultLess(String Data)
        {
            if (Data == "")
            {
                return Json("NOK");
            }
            else
            {
                var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInboundSearchModel>("Proc_InboundPermissionResultQuantityLess25_get").ToList();
                return Json(_GetAuthorReport);
            }
        }
        
        /**************************************************************************************
       Created By  : Saddam
       Created On  : 29th aug 2016
       Created For : getting the Image/vedio Bank details by Id
       *******************************************************************************************/
        public IHttpActionResult GetImageVideoBankDetails(int id)
        {


            var PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsById(id);


            var _permissionInbound = new
            {

                AssetsType = PermissionInbound.AssetsType
            };


            if (_permissionInbound.AssetsType == "I" || _permissionInbound.AssetsType == "B")
            {
                var ImageVideoBankLink = _IPermissionsInboundService.getPermissionInboundImageVideoBankById(PermissionInbound.Id);

                var _ImageVideoBankLinkId = new
                {
                    ImageBankId = ImageVideoBankLink.ImageBankId,
                    ImageVideoBankLinkId = ImageVideoBankLink.Id
                };


                var OtherContractImageBank = _IPermissionsInboundService.OtherContractImageBankById(_ImageVideoBankLinkId.ImageBankId);


                var _OtherContractImageBank = new
                {
                    PrintRights = OtherContractImageBank.PrintRights,
                    electronicrights = OtherContractImageBank.electronicrights,
                    ebookrights = OtherContractImageBank.ebookrights,
                    othercontractid = OtherContractImageBank.othercontractid,
                    restriction = OtherContractImageBank.restriction
                };


                var PartyNameDetail = _IPermissionsInboundService.OtherContractMasterById(_OtherContractImageBank.othercontractid);

                var _partname = new
                {
                    PartyName = PartyNameDetail.partyname,
                    Id = PartyNameDetail.Id
                };


                IList<PermissionInboundImageVideoBankData> PermissionInboundImageVideoBankData = _IPermissionsInboundService.getPermissionInboundImageVideoBankDataById(_ImageVideoBankLinkId.ImageVideoBankLinkId);

                ///   IList<PermissionInboundImageVideoBank> X = PermissionInbound.PermissionInboundImageVideoBank.ToList();



                var _PermissionInboundImageVideoBankData = PermissionInboundImageVideoBankData.ToList().Select(a => new
                {
                    ContractTypes = a.ContractTypes,
                    imagevideobankid = a.imagevideobankid,
                    Description = a.Description,
                    invoicen = a.invoiceno,
                    invoicevalue = a.invoicevalue,
                  //  invoicedate = a.invoicedate.toDDMMYYYY(),
                    invoicedate = a.invoicedate == null ? null : Convert.ToDateTime(a.invoicedate).toDDMMYYYY(),
                    
                    
                    printquantity = a.printquantity,
                    permissionexpirydate = a.permissionexpirydate == null ? null : Convert.ToDateTime(a.permissionexpirydate).toDDMMYYYY(),
                    weblink = a.weblink,
                    ImageVideoBankDataId = a.Id,
                    Imagevideobankid = a.imagevideobankid,
                    Credit_Lines = a.creditlines,
                    Remarks = a.Remarks,
                    Editorial_Only_Type = a.EditorialonlyType

                }).ToList();


                var mobj_partyDetailsView = new
                {
                    Restriction = OtherContractImageBank.restriction,
                    PrintRights = OtherContractImageBank.PrintRights,
                    Electronicrights = OtherContractImageBank.electronicrights,
                    Ebookrights = OtherContractImageBank.ebookrights,
                    Id = OtherContractImageBank.Id
                };
                var videoimagebankView = OtherContractImageBank.VideoImageBank.ToList().Select(i => new
                {
                    BankType = i.Type,
                    fullname = i.Fullname,
                    CurrencySysbol = i.CurrencyMaster.Symbol,
                    Cost = i.Cost,
                    ShrotName = i.ShortName
                }).ToList();


                return Json(new { _permissionInbound, _ImageVideoBankLinkId, _PermissionInboundImageVideoBankData, mobj_partyDetailsView, videoimagebankView, _OtherContractImageBank, _partname });

            }
            else
            {
                return Json("");

            }





        }

        /**************************************************************************************
       Created By  : Saddam
       Created On  : 29th aug 2016
       Created For : getting the Other Bank details by Id
       *******************************************************************************************/
        public IHttpActionResult GetOthersDetails(int id)
        {
            var PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsById(id);

            var _permissionInbound = new
            {

                AssetsType = PermissionInbound.AssetsType
            };


            if (_permissionInbound.AssetsType == "O" || _permissionInbound.AssetsType == "B")
            {
                // var PermissionInboundCopyRightHolder = _IPermissionsInboundService.getPermissionInboundCopyRightHolderById(PermissionInbound.Id);
                var PermissionInboundOthers = _IPermissionsInboundService.getPermissionInboundOtherById(PermissionInbound.Id);

                IList<PermissionInboundCopyRightHolderMaster> PermissionInboundCopyRightHolder = _IPermissionsInboundService.getPermissionInboundCopyRightHolderById(PermissionInboundOthers.Id);


                var _mobj_PermissionInboundOthers = new
                {
                    status = (PermissionInboundOthers.statusId == null ? null : PermissionInboundOthers.StatusMaster.Status),

                    AssetSubType = (PermissionInboundOthers.AssetSubType == null ? null : PermissionInboundOthers.AssetSubType.AssetName),

                    AssetDescription = PermissionInboundOthers.AssetDescription,
                    Extent = PermissionInboundOthers.Extent,
                    Gratiscopytobesent = PermissionInboundOthers.Gratiscopytobesent,
                    Noofcopy = PermissionInboundOthers.Noofcopy,
                    OriginalSource = PermissionInboundOthers.OriginalSource,


                    Restriction = PermissionInboundOthers.Restriction,
                    SubLicensing = PermissionInboundOthers.SubLicensing,
                    Fee = PermissionInboundOthers.Fee,
                    Currency = (PermissionInboundOthers.CurrencyId == null ? null : PermissionInboundOthers.CurrencyMaster.CurrencyName),
                    CurrencySymbol = (PermissionInboundOthers.CurrencyId == null ? null : PermissionInboundOthers.CurrencyMaster.Symbol),


                    InvoiceNumber = PermissionInboundOthers.InvoiceNumber,
                    Invoicevalue = PermissionInboundOthers.Invoicevalue,


                    PermissionExpirydate = PermissionInboundOthers.PermissionExpirydate,
                    Acknowledgementline = PermissionInboundOthers.Acknowledgementline,
                    InboundRemarks = PermissionInboundOthers.InboundRemarks

                    //PermissionInboundOthersId = PermissionInboundOthers.Id
                };



                var _PermissionInboundCopyRightHolder = (from PIO in _PermissionInboundOthers.Table.Where(a => a.Deactivate == "N")
                                                         join PICRM in _PermissionInboundCopyRightHolderMaster.Table.Where(a => a.Deactivate == "N")
                                                                         on PIO.Id equals PICRM.InboundOthersId into output
                                                         from d in output.DefaultIfEmpty()

                                                         join AST in _AssetSubType.Table.Where(a => a.Deactivate == "N")
                                                        on PIO.AssetSubTypeId equals AST.Id into outputAssetSubType

                                                         from Asset in outputAssetSubType.DefaultIfEmpty()


                                                         join CRM in _CopyRightHolderMaster.Table.Where(a => a.Deactivate == "N")
                                                         on d.CopyRightHolderCode equals CRM.CopyRightHolderCode into outputInner

                                                         from E in outputInner.DefaultIfEmpty()

                                                         select new
                                                         {
                                                             Id = d.Id,
                                                             CopyRightHolderCode = d.CopyRightHolderCode,
                                                             CopyRightHolderName = (d.CopyRightHolderName == null ? null : (d.CopyRightHolderName)),
                                                             ContactPerson = d.ContactPerson,
                                                             Mobile = d.Mobile,
                                                             Email = d.Email,
                                                             PermissionInboundId = PIO.PermissionInboundId,
                                                             InboundOthersId = d.InboundOthersId,
                                                             Address = d.Address,
                                                             Pincode = d.Pincode,
                                                             CopyRightHolderId = E.Id,
                                                             AssetName = Asset.AssetName,
                                                             AssetDescription = PIO.AssetDescription
                                                         }
            ).Distinct().Where(a => a.PermissionInboundId == PermissionInboundOthers.PermissionInboundId).OrderBy(a => a.CopyRightHolderCode);




                //var _PermissionInboundCopyRightHolder = PermissionInboundCopyRightHolder.ToList().Select(i => new
                //{
                //    CopyRightHolderName = i.CopyRightHolderName,
                //    ContactPerson = i.ContactPerson,
                //    Address = i.Address,
                //    Pincode = i.Pincode,
                //    Mobile = i.Mobile,

                //}).ToList();


                var _Rights = (from POBRL in _PermissionInboundOthersRightsLink.Table.Where(a => a.Deactivate == "N")
                               join ORM in _OtherRightsMaster.Table.Where(a => a.Deactivate == "N")
                               on POBRL.RightsId equals ORM.Id
                               select new
                               {
                                   RightsName = ORM.RightsName,
                                   status = POBRL.status,
                                   RunGranted = (POBRL.RunGranted == null ? "---" : POBRL.RunGranted),
                                   Number = (POBRL.Number.ToString() == "0" ? "---" : POBRL.Number.ToString()),
                                   PIOID = POBRL.PIOID
                               }

                                      ).ToList().Distinct().Where(a => a.PIOID == PermissionInboundOthers.Id).OrderBy(a => a.RightsName);



                IList<OtherContractDateRequest> DateRequest = _IPermissionsInboundService.getOtherContractDateRequestById(PermissionInboundOthers.Id);


                //var _DateRequest = new {

                //    dateOf = DateRequest.
                //};


                var _DateRequest = DateRequest.ToList().Select(a => new
                {
                    dateOf = a.dateOf,
                    dateValue = (a.dateValue == null ? "---" : a.dateValue.GetValueOrDefault().toDDMMYYYY()),

                }).ToList();


                return Json(new { _permissionInbound, _PermissionInboundCopyRightHolder, _mobj_PermissionInboundOthers, _Rights, _DateRequest });
            }
            else
            {
                return Json("");
            }

        }

        /**************************************************************************************
     Created By  : Saddam
     Created On  : 30th aug 2016
     Created For : getting the Assets type details by Id
     *******************************************************************************************/
        public IHttpActionResult GetAssetTypeDetails(int id)
        {
            var PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsById(id);

            var _permissionInbound = new
            {

                AssetsType = PermissionInbound.AssetsType
            };
            return Json(new { _permissionInbound });
        }

        public IHttpActionResult GetMultipleAssetTypeDetails(string Code)
        {
            var _permissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(Code).Select(i => new
            {

                AssetsType = i.AssetsType
            });


            return Json(new { _permissionInbound });
        }

        /**************************************************************************************
       Created By  : Saddam
       Created On  : 30 Aug 2016
       Created For : Function for inserting Pending Request for Permissions Inbound process
       *******************************************************************************************/
        [HttpPost]
        public IHttpActionResult UpdatePendingRequestPermissions(PendingRequestPermissionsInbound PendingRequestPermissionsInbound)
        {
            string status = string.Empty;
            try
            {


                if (PendingRequestPermissionsInbound.Code != null)
                {
                    if (PendingRequestPermissionsInbound.UpdateRight == "rt")
                    {

                      // 
                        IList<PermissionInbound> _PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(PendingRequestPermissionsInbound.Code).ToList();
                        if (_PermissionInbound != null)
                        {
                            if (_PermissionInbound.Count > 0)
                            {
                                for (int j = 0; j < _PermissionInbound.Count; j++)
                                {
                                    PermissionInboundUpdate PermissionInboundUpdate = new PermissionInboundUpdate();
                                    PermissionInboundUpdate.PermissionInboundId = _PermissionInbound[j].Id;
                                    PermissionInboundUpdate.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                    PermissionInboundUpdate.Remarks = PendingRequestPermissionsInbound.PendingRemarks;



                                    int PermissionsInboundUpdateId = _IPermissionsInboundService.InsertPermissionInboundUpdate(PermissionInboundUpdate);



                                    PermissionInboundDocuments _PermissionInboundDocumentsDocLink = new PermissionInboundDocuments();

                                    string[] docurl1 = PendingRequestPermissionsInbound.DocumentFile.Split(',');
                                    int i = 0;
                                    foreach (string doc in PendingRequestPermissionsInbound.Documentname)
                                    {
                                        PermissionInboundDocuments Link = new PermissionInboundDocuments();
                                        Link.PermissionsInboundUpdateId = PermissionsInboundUpdateId;
                                        Link.Documentname = doc;
                                        Link.DocumentFile = docurl1[i];
                                        Link.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                        _IPermissionsInboundService.InsertPermissionInboundDocuments(Link);
                                        i++;
                                    }
                                }
                            }
                        }


                       
                        




                    }
                    if (PendingRequestPermissionsInbound.UpdateRight == "ad" || PendingRequestPermissionsInbound.UpdateRight == "sa")
                    {
                        string mstr_AssetType = string.Empty;
                        string mstr_AssetTypevalue = string.Empty;
                        int PermissionInBoundLastId = 0;

                        int CountValue = 0;

                        IList<PermissionInbound> _PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(PendingRequestPermissionsInbound.Code).ToList();
                        //if (_PermissionInbound.Count > 0)
                        //{
                        //    for (int p = 0; p < _PermissionInbound.Count; p++)
                        //    {
                        //        if (_PermissionInbound[p].AssetsType == "I")
                        //        {
                        //            CountValue = 1;
                        //        }

                        //        if (_PermissionInbound[p].AssetsType == "O")
                        //        {
                        //            CountValue = 1;
                        //        }

                        //    CountValue = CountValue + 1;
                        //    }

                        //}
                       
                      

                     



                            if (_PermissionInbound != null)
                            {
                                int Id = _PermissionInbound.LastOrDefault().Id;
                                PermissionInBoundLastId = _PermissionInbound.LastOrDefault().Id;
                                PermissionInbound _InboundTable = _IPermissionsInboundService.getPermissionInboundById(Id);
                                _InboundTable.ProductId = PendingRequestPermissionsInbound.ProductId;
                                _InboundTable.TypeFor = PendingRequestPermissionsInbound.TypeFor.ToString();
                                _InboundTable.AuthorContractId = PendingRequestPermissionsInbound.AuthorContractId;
                                _InboundTable.ProductLicenseId = PendingRequestPermissionsInbound.LicenseId;

                                _InboundTable.AssetsType = PendingRequestPermissionsInbound.AssetsType;
                                _InboundTable.ModifiedBy = PendingRequestPermissionsInbound.EnteredBy;
                                _InboundTable.ModifiedDate = DateTime.Now;
                                _IPermissionsInboundService.UpdatePermissionInboundData(_InboundTable);
                            }




                            if (_PermissionInbound != null)
                            {
                                if (_PermissionInbound.Count > 0)
                                {
                                    for (int j = 0; j < _PermissionInbound.Count; j++)
                                    {
                                        PermissionInboundUpdate _PermissionInboundUpdate = _IPermissionsInboundService.getPermissionInboundUpdateById(_PermissionInbound[j].Id);

                                        if (_PermissionInboundUpdate != null)
                                        {

                                            _PermissionInboundUpdate.PermissionInboundId = _PermissionInbound[j].Id;
                                            _PermissionInboundUpdate.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                            _PermissionInboundUpdate.Remarks = PendingRequestPermissionsInbound.PendingRemarks;

                                            _IPermissionsInboundService.UpdatePermissionInboundUpdate(_PermissionInboundUpdate);



                                            PermissionInboundDocuments _PermissionInboundDocumentsDocLink = new PermissionInboundDocuments();

                                            string[] docurl1 = PendingRequestPermissionsInbound.DocumentFile.Split(',');
                                            int i = 0;
                                            foreach (string doc in PendingRequestPermissionsInbound.Documentname)
                                            {
                                                PermissionInboundDocuments Link = new PermissionInboundDocuments();
                                                Link.PermissionsInboundUpdateId = _PermissionInboundUpdate.Id;
                                                Link.Documentname = doc;
                                                Link.DocumentFile = docurl1[i];
                                                Link.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                                _IPermissionsInboundService.InsertPermissionInboundDocuments(Link);
                                                i++;
                                            }
                                        }

                                        else
                                        {

                                            PermissionInboundUpdate PermissionInboundUpdate = new PermissionInboundUpdate();
                                            PermissionInboundUpdate.PermissionInboundId = _PermissionInbound[j].Id;
                                            PermissionInboundUpdate.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                            PermissionInboundUpdate.Remarks = PendingRequestPermissionsInbound.PendingRemarks;



                                            int PermissionsInboundUpdateId = _IPermissionsInboundService.InsertPermissionInboundUpdate(PermissionInboundUpdate);



                                            PermissionInboundDocuments _PermissionInboundDocumentsDocLink = new PermissionInboundDocuments();

                                            string[] docurl1 = PendingRequestPermissionsInbound.DocumentFile.Split(',');
                                            int i = 0;
                                            foreach (string doc in PendingRequestPermissionsInbound.Documentname)
                                            {
                                                PermissionInboundDocuments Link = new PermissionInboundDocuments();
                                                Link.PermissionsInboundUpdateId = PermissionsInboundUpdateId;
                                                Link.Documentname = doc;
                                                Link.DocumentFile = docurl1[i];
                                                Link.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                                _IPermissionsInboundService.InsertPermissionInboundDocuments(Link);
                                                i++;
                                            }

                                        }

                                    }
                                }
                            }



                            if (PendingRequestPermissionsInbound.AssetsType == "B" || PendingRequestPermissionsInbound.AssetsType == "I")
                            {
                                if (PendingRequestPermissionsInbound.ImageVedioId != null)
                                {
                                    //int Id = PendingRequestPermissionsInbound.ImageVedioId.GetValueOrDefault();

                                    int Id = PendingRequestPermissionsInbound.hid_ImageVideoBankId;

                                    PermissionInboundImageVideoBankData _PermissionInboundImageVideoBankData = _IPermissionsInboundService.getPermissionInboundImageVideoBankDetialById(Id);


                                    int InboundImageVideoBankId = _IPermissionsInboundService.GetisValidPartyName(PendingRequestPermissionsInbound.PartyName);

                                    if (InboundImageVideoBankId != 0)
                                    {

                                        _PermissionInboundImageVideoBankData.ContractTypes = PendingRequestPermissionsInbound.ContractTypes;
                                        _PermissionInboundImageVideoBankData.imagevideobankid = PendingRequestPermissionsInbound.imagevideobankid;
                                        _PermissionInboundImageVideoBankData.Description = PendingRequestPermissionsInbound.Description;
                                        _PermissionInboundImageVideoBankData.invoiceno = PendingRequestPermissionsInbound.invoiceno;
                                        _PermissionInboundImageVideoBankData.invoicevalue = PendingRequestPermissionsInbound.invoicevalue;
                                        _PermissionInboundImageVideoBankData.invoicedate = PendingRequestPermissionsInbound.invoicedate;
                                        _PermissionInboundImageVideoBankData.printquantity = PendingRequestPermissionsInbound.printquantity;
                                        _PermissionInboundImageVideoBankData.permissionexpirydate = PendingRequestPermissionsInbound.permissionexpirydate;
                                        _PermissionInboundImageVideoBankData.weblink = PendingRequestPermissionsInbound.weblink;
                                        _PermissionInboundImageVideoBankData.creditlines = PendingRequestPermissionsInbound.creditlines;
                                        _PermissionInboundImageVideoBankData.EditorialonlyType = PendingRequestPermissionsInbound.EditorialonlyType;
                                        _PermissionInboundImageVideoBankData.Remarks = PendingRequestPermissionsInbound.Remarks;
                                        _PermissionInboundImageVideoBankData.ModifiedBy = PendingRequestPermissionsInbound.EnteredBy;


                                        _PermissionInboundImageVideoBankData.ImageBankPartyId = _IPermissionsInboundService.GetisValidPartyName(PendingRequestPermissionsInbound.PartyName);

                                        _PermissionInboundImageVideoBankData.CurrencyId = PendingRequestPermissionsInbound.ImageVideoCurrency;

                                        _IPermissionsInboundService.UpdatePermissionInboundImageVideoBankData(_PermissionInboundImageVideoBankData);



                                        PermissionInboundImageVideoBank PermissionInboundImageVideoBankLink = _IPermissionsInboundService.getPermissionInboundImageVideoBankById(_PermissionInboundImageVideoBankData.IVBId);


                                        if (PermissionInboundImageVideoBankLink != null)
                                        {
                                            if (PermissionInboundImageVideoBankLink.ImageBankId != PendingRequestPermissionsInbound.ImageBankId)
                                            {
                                                PermissionInboundImageVideoBankLink.ImageBankId = _IPermissionsInboundService.GetisValidPartyName(PendingRequestPermissionsInbound.PartyName);
                                                PermissionInboundImageVideoBankLink.ModifiedBy = PendingRequestPermissionsInbound.EnteredBy;

                                                _IPermissionsInboundService.UpdatePermissionInboundImageVideoBank(PermissionInboundImageVideoBankLink);

                                            }
                                        }
                                    }


                                    else
                                    {

                                        status = "notvalid";
                                        return Json(status);

                                    }





                                }
                                else
                                {

                                    if (PendingRequestPermissionsInbound.PartyName != null)
                                    {


                                        int InboundImageVideoBankId = _IPermissionsInboundService.GetisValidPartyName(PendingRequestPermissionsInbound.PartyName);

                                        if (InboundImageVideoBankId != 0)
                                        {
                                            PermissionInboundImageVideoBank _ImageVideoBank = new PermissionInboundImageVideoBank();
                                            _ImageVideoBank.ImageBankId = InboundImageVideoBankId;
                                            _ImageVideoBank.PermissionInboundId = PermissionInBoundLastId;

                                            _ImageVideoBank.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                            _ImageVideoBank.Deactivate = "N";
                                            _ImageVideoBank.EntryDate = DateTime.Now;


                                            int ImageVideoBankLinkId = _IPermissionsInboundService.InsertPermissionInboundImageVideoBankLink(_ImageVideoBank);

                                            if (ImageVideoBankLinkId != null)
                                            {

                                                PermissionInboundImageVideoBankData _ImageVideoBankData = new PermissionInboundImageVideoBankData();



                                                _ImageVideoBankData.ContractTypes = PendingRequestPermissionsInbound.ContractTypes;
                                                _ImageVideoBankData.imagevideobankid = PendingRequestPermissionsInbound.imagevideobankid;
                                                _ImageVideoBankData.Description = PendingRequestPermissionsInbound.Description;
                                                _ImageVideoBankData.invoiceno = PendingRequestPermissionsInbound.invoiceno;
                                                _ImageVideoBankData.invoicevalue = PendingRequestPermissionsInbound.invoicevalue;
                                                _ImageVideoBankData.invoicedate = PendingRequestPermissionsInbound.invoicedate;
                                                _ImageVideoBankData.printquantity = PendingRequestPermissionsInbound.printquantity;
                                                _ImageVideoBankData.permissionexpirydate = PendingRequestPermissionsInbound.permissionexpirydate;
                                                _ImageVideoBankData.weblink = PendingRequestPermissionsInbound.weblink;
                                                _ImageVideoBankData.creditlines = PendingRequestPermissionsInbound.creditlines;
                                                _ImageVideoBankData.EditorialonlyType = PendingRequestPermissionsInbound.EditorialonlyType;
                                                _ImageVideoBankData.Remarks = PendingRequestPermissionsInbound.Remarks;
                                                _ImageVideoBankData.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                                _ImageVideoBankData.ImageBankPartyId = _IPermissionsInboundService.GetisValidPartyName(PendingRequestPermissionsInbound.PartyName);

                                                _ImageVideoBankData.CurrencyId = PendingRequestPermissionsInbound.ImageVideoCurrency;

                                                _ImageVideoBankData.IVBId = ImageVideoBankLinkId;

                                                _ImageVideoBankData.EntryDate = DateTime.Now;
                                               
                                                _ImageVideoBankData.Deactivate = "N";


                                                _IPermissionsInboundService.InsertNewPermissionInboundImageVideoBankData(_ImageVideoBankData);

                                            }


                                        }
                                        else
                                        {
                                            status = "notvalid";
                                            return Json(status);

                                        }






                                       

                                       
                                    }
                                
                                }


                               


                            }

                            if (PendingRequestPermissionsInbound.AssetsType == "B" || PendingRequestPermissionsInbound.AssetsType == "O")
                            {

                                if (PendingRequestPermissionsInbound.hid_CopyrightholderId != 0)
                                {
                                    int Id = PendingRequestPermissionsInbound.hid_CopyrightholderId;
                                    PermissionInboundCopyRightHolderMaster _PermissionInboundCopyRightHolderMaster = _IPermissionsInboundService.getPermissionInboundCopyRightHolderMasterById(Id);


                                    _PermissionInboundCopyRightHolderMaster.CopyRightHolderCode = PendingRequestPermissionsInbound.CopyRightHolderCode;
                                    _PermissionInboundCopyRightHolderMaster.CopyRightHolderName = PendingRequestPermissionsInbound.CopyRightHolderName;
                                    _PermissionInboundCopyRightHolderMaster.ContactPerson = PendingRequestPermissionsInbound.ContactPerson;
                                    _PermissionInboundCopyRightHolderMaster.Address = PendingRequestPermissionsInbound.CopyRightHolderAddress;
                                    _PermissionInboundCopyRightHolderMaster.CountryId = PendingRequestPermissionsInbound.Country.GetValueOrDefault();
                                    _PermissionInboundCopyRightHolderMaster.Stateid = PendingRequestPermissionsInbound.State.GetValueOrDefault();
                                    _PermissionInboundCopyRightHolderMaster.Cityid = PendingRequestPermissionsInbound.City.GetValueOrDefault();
                                    _PermissionInboundCopyRightHolderMaster.Pincode = PendingRequestPermissionsInbound.Pincode;
                                    _PermissionInboundCopyRightHolderMaster.Mobile = PendingRequestPermissionsInbound.Mobile;
                                    _PermissionInboundCopyRightHolderMaster.Email = PendingRequestPermissionsInbound.CopyRightHolderEmail;
                                    _PermissionInboundCopyRightHolderMaster.URL = PendingRequestPermissionsInbound.CopyRightHolderURL;
                                    _PermissionInboundCopyRightHolderMaster.BankName = PendingRequestPermissionsInbound.CopyRightHolderBankName;
                                    _PermissionInboundCopyRightHolderMaster.AccountNo = PendingRequestPermissionsInbound.CopyRightHolderAccountNo;
                                    _PermissionInboundCopyRightHolderMaster.BankAddress = PendingRequestPermissionsInbound.CopyRightHolderBankAddress;
                                    _PermissionInboundCopyRightHolderMaster.IFSCCode = PendingRequestPermissionsInbound.CopyRightHolderIFSCCode;
                                    _PermissionInboundCopyRightHolderMaster.PANNo = PendingRequestPermissionsInbound.CopyRightHolderPANNo;
                                    _PermissionInboundCopyRightHolderMaster.InboundOthersId = PendingRequestPermissionsInbound.InboundOthersId.GetValueOrDefault();
                                    _PermissionInboundCopyRightHolderMaster.ModifiedBy = PendingRequestPermissionsInbound.EnteredBy;

                                    _IPermissionsInboundService.UpdatePermissionInboundCopyRightHolderMaster(_PermissionInboundCopyRightHolderMaster);

                                    PermissionInboundOthers _PermissionInboundOthers = _IPermissionsInboundService.getPermissionInboundOthersDetailsCopyRightHolderById(_PermissionInboundCopyRightHolderMaster.InboundOthersId);

                               
                                    _PermissionInboundOthers.AssetSubTypeId = PendingRequestPermissionsInbound.AssetSubType;
                                    _PermissionInboundOthers.AssetDescription = PendingRequestPermissionsInbound.AssetDescription;
                                    _PermissionInboundOthers.statusId = PendingRequestPermissionsInbound.Status.GetValueOrDefault();
                                    _PermissionInboundOthers.Restriction = PendingRequestPermissionsInbound.Restriction;
                                    _PermissionInboundOthers.SubLicensing = PendingRequestPermissionsInbound.SubLicensing;
                                    _PermissionInboundOthers.Fee = PendingRequestPermissionsInbound.Fee.GetValueOrDefault();
                                    _PermissionInboundOthers.CurrencyId = PendingRequestPermissionsInbound.CurrencyValue;
                                    _PermissionInboundOthers.Extent = PendingRequestPermissionsInbound.Extent;
                                    _PermissionInboundOthers.Gratiscopytobesent = PendingRequestPermissionsInbound.Gratiscopytobesent;
                                    _PermissionInboundOthers.Noofcopy = PendingRequestPermissionsInbound.Noofcopy;
                                    _PermissionInboundOthers.OriginalSource = PendingRequestPermissionsInbound.OriginalSource;
                                    _PermissionInboundOthers.InvoiceNumber = PendingRequestPermissionsInbound.InvoiceNumber;
                                    _PermissionInboundOthers.Invoicevalue = PendingRequestPermissionsInbound.InvoiceValue;
                                    _PermissionInboundOthers.PermissionExpirydate = PendingRequestPermissionsInbound.PermissionExpirydate;

                                    _PermissionInboundOthers.Acknowledgementline = PendingRequestPermissionsInbound.Acknowledgementline;
                                    _PermissionInboundOthers.InboundRemarks = PendingRequestPermissionsInbound.InboundRemarks;

                                    _PermissionInboundOthers.TerritoryRightsId = PendingRequestPermissionsInbound.TerritoryRights;
                                    
                                    _PermissionInboundOthers.ModifiedBy = PendingRequestPermissionsInbound.EnteredBy;
                                    _IPermissionsInboundService.UpdatePermissionInboundCopyRightHolderMaster(_PermissionInboundCopyRightHolderMaster);

                                 

                                    if (PendingRequestPermissionsInbound.DateRequestObject.Count != 0)
                                    {

                                      
                                        
                                      _IPermissionsInboundService.DeavtivateOtherContractDateRequest(_PermissionInboundOthers.Id, PendingRequestPermissionsInbound.EnteredBy);

                                       

                                        

                                        foreach (var lst in PendingRequestPermissionsInbound.DateRequestObject)
                                        {
                                            OtherContractDateRequest _OtherContractDateRequest = new OtherContractDateRequest();
                                            _OtherContractDateRequest.dateOf = lst.DateOf;
                                            _OtherContractDateRequest.dateValue = lst.DateValue;
                                            _OtherContractDateRequest.EntryDate = DateTime.Now;
                                            _OtherContractDateRequest.Deactivate = "N";
                                            _OtherContractDateRequest.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                            _OtherContractDateRequest.PIOID = _PermissionInboundOthers.Id;

                                            _IPermissionsInboundService.InsertOtherContractDateRequest(_OtherContractDateRequest);
                                            //  _OtherContractDateRequestList.Add(_OtherContractDateRequest);
                                        }


                                    }

                                    _IPermissionsInboundService.DeavtivatePermissionInboundOthersRightsLink(_PermissionInboundOthers.Id, PendingRequestPermissionsInbound.EnteredBy);

                                    IList<PermissionInboundOthersRightsLink> _PermissionRightsObjectList = new List<PermissionInboundOthersRightsLink>();

                                    foreach (var lst in PendingRequestPermissionsInbound.PermissionRightsObject)
                                    {
                                        PermissionInbound _InBoundPermission = new PermissionInbound();
                                        PermissionInboundOthersRightsLink _PermissionRightsObject = new PermissionInboundOthersRightsLink();
                                        _PermissionRightsObject.RightsId = lst.RightsId.GetValueOrDefault();
                                        _PermissionRightsObject.status = lst.Status;
                                        _PermissionRightsObject.Number = lst.Number;
                                        _PermissionRightsObject.RunGranted = lst.RunGranted;
                                        _PermissionRightsObject.EntryDate = DateTime.Now;
                                        _PermissionRightsObject.Deactivate = "N";
                                        _PermissionRightsObject.PIOID = _PermissionInboundOthers.Id;

                                      //  _InBoundPermission.PermissionInboundOthers.FirstOrDefault().PermissionInboundOthersRightsLink.FirstOrDefault().PIOID = _PermissionInboundOthers.Id;

                                        _PermissionRightsObject.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                        _IPermissionsInboundService.InsertPermissionInboundOthersRightsLink(_PermissionRightsObject);
                                        //_PermissionRightsObjectList.Add(_PermissionRightsObject);
                                    }
                                }
                                else
                                {
                                    //int Id = PendingRequestPermissionsInbound.InboundOthersId.GetValueOrDefault();
                                    //PermissionInboundOthers _PermissionInboundOthersValue = _IPermissionsInboundService.getPermissionInboundOthersDetailsCopyRightHolderById(Id);


                                    if (PendingRequestPermissionsInbound.CopyRightHolderName != null)
                                    {
                                        PermissionInboundOthers _PermissionInboundOthers = new PermissionInboundOthers();

                                        _PermissionInboundOthers.PermissionInboundId = PermissionInBoundLastId;
                                        _PermissionInboundOthers.AssetSubTypeId = PendingRequestPermissionsInbound.AssetSubType;
                                        _PermissionInboundOthers.AssetDescription = PendingRequestPermissionsInbound.AssetDescription;
                                        _PermissionInboundOthers.statusId = PendingRequestPermissionsInbound.Status.GetValueOrDefault();
                                        _PermissionInboundOthers.Restriction = PendingRequestPermissionsInbound.Restriction;
                                        _PermissionInboundOthers.SubLicensing = PendingRequestPermissionsInbound.SubLicensing;
                                        _PermissionInboundOthers.Fee = PendingRequestPermissionsInbound.Fee.GetValueOrDefault();
                                        _PermissionInboundOthers.CurrencyId = PendingRequestPermissionsInbound.CurrencyValue;
                                        _PermissionInboundOthers.Extent = PendingRequestPermissionsInbound.Extent;

                                        _PermissionInboundOthers.Gratiscopytobesent = PendingRequestPermissionsInbound.Gratiscopytobesent;

                                        _PermissionInboundOthers.Noofcopy = PendingRequestPermissionsInbound.Noofcopy;
                                        _PermissionInboundOthers.OriginalSource = PendingRequestPermissionsInbound.OriginalSource;
                                        _PermissionInboundOthers.InvoiceNumber = PendingRequestPermissionsInbound.InvoiceNumber;
                                        _PermissionInboundOthers.Invoicevalue = PendingRequestPermissionsInbound.InvoiceValue;
                                        _PermissionInboundOthers.PermissionExpirydate = PendingRequestPermissionsInbound.PermissionExpirydate;
                                        _PermissionInboundOthers.Acknowledgementline = PendingRequestPermissionsInbound.Acknowledgementline;
                                        _PermissionInboundOthers.InboundRemarks = PendingRequestPermissionsInbound.InboundRemarks;

                                        _PermissionInboundOthers.TerritoryRightsId = PendingRequestPermissionsInbound.TerritoryRights;

                                        _PermissionInboundOthers.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                        int PermissionInboundOthersId = _IPermissionsInboundService.InsertPermissionInboundOthers(_PermissionInboundOthers);




                                        PermissionInboundCopyRightHolderMaster _PermissionInboundCopyRightHolderMaster = new PermissionInboundCopyRightHolderMaster();
                                        _PermissionInboundCopyRightHolderMaster.CopyRightHolderCode = PendingRequestPermissionsInbound.CopyRightHolderCode;
                                        _PermissionInboundCopyRightHolderMaster.CopyRightHolderName = PendingRequestPermissionsInbound.CopyRightHolderName;
                                        _PermissionInboundCopyRightHolderMaster.ContactPerson = PendingRequestPermissionsInbound.ContactPerson;
                                        _PermissionInboundCopyRightHolderMaster.Address = PendingRequestPermissionsInbound.CopyRightHolderAddress;
                                        _PermissionInboundCopyRightHolderMaster.CountryId = PendingRequestPermissionsInbound.Country.GetValueOrDefault();
                                        _PermissionInboundCopyRightHolderMaster.Stateid = PendingRequestPermissionsInbound.State.GetValueOrDefault();
                                        _PermissionInboundCopyRightHolderMaster.Cityid = PendingRequestPermissionsInbound.City.GetValueOrDefault();
                                        _PermissionInboundCopyRightHolderMaster.Pincode = PendingRequestPermissionsInbound.Pincode;
                                        _PermissionInboundCopyRightHolderMaster.Mobile = PendingRequestPermissionsInbound.Mobile;
                                        _PermissionInboundCopyRightHolderMaster.Email = PendingRequestPermissionsInbound.CopyRightHolderEmail;
                                        _PermissionInboundCopyRightHolderMaster.URL = PendingRequestPermissionsInbound.CopyRightHolderURL;
                                        _PermissionInboundCopyRightHolderMaster.BankName = PendingRequestPermissionsInbound.CopyRightHolderBankName;
                                        _PermissionInboundCopyRightHolderMaster.AccountNo = PendingRequestPermissionsInbound.CopyRightHolderAccountNo;
                                        _PermissionInboundCopyRightHolderMaster.BankAddress = PendingRequestPermissionsInbound.CopyRightHolderBankAddress;
                                        _PermissionInboundCopyRightHolderMaster.IFSCCode = PendingRequestPermissionsInbound.CopyRightHolderIFSCCode;
                                        _PermissionInboundCopyRightHolderMaster.PANNo = PendingRequestPermissionsInbound.CopyRightHolderPANNo;
                                        _PermissionInboundCopyRightHolderMaster.InboundOthersId = PermissionInboundOthersId;
                                        _PermissionInboundCopyRightHolderMaster.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                        _IPermissionsInboundService.InsertPermissionInboundCopyRightHolderMaster(_PermissionInboundCopyRightHolderMaster);




                                        if (PendingRequestPermissionsInbound.DateRequestObject != null)
                                        {


                                            foreach (var lst in PendingRequestPermissionsInbound.DateRequestObject)
                                            {
                                                OtherContractDateRequest _OtherContractDateRequest = new OtherContractDateRequest();
                                                _OtherContractDateRequest.dateOf = lst.DateOf;
                                                _OtherContractDateRequest.dateValue = lst.DateValue;
                                                _OtherContractDateRequest.EntryDate = DateTime.Now;
                                                _OtherContractDateRequest.Deactivate = "N";
                                                _OtherContractDateRequest.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;
                                                _OtherContractDateRequest.PIOID = PermissionInboundOthersId;
                                                _IPermissionsInboundService.InsertOtherContractDateRequest(_OtherContractDateRequest);

                                            }


                                        }


                                        IList<PermissionInboundOthersRightsLink> _PermissionRightsObjectList = new List<PermissionInboundOthersRightsLink>();

                                        foreach (var lst in PendingRequestPermissionsInbound.PermissionRightsObject)
                                        {
                                            PermissionInboundOthersRightsLink _PermissionRightsObject = new PermissionInboundOthersRightsLink();
                                            _PermissionRightsObject.RightsId = lst.RightsId.GetValueOrDefault();
                                            _PermissionRightsObject.status = lst.Status;
                                            _PermissionRightsObject.Number = lst.Number;
                                            _PermissionRightsObject.RunGranted = lst.RunGranted;
                                            _PermissionRightsObject.EntryDate = DateTime.Now;
                                            _PermissionRightsObject.Deactivate = "N";
                                            _PermissionRightsObject.PIOID = PermissionInboundOthersId;

                                            _PermissionRightsObject.EnteredBy = PendingRequestPermissionsInbound.EnteredBy;

                                            _IPermissionsInboundService.InsertPermissionInboundOthersRightsLink(_PermissionRightsObject);

                                        }
                                    }


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

        /**************************************************************************************
       Created By  : Saddam
       Created On  : 29th aug 2016
       Created For : getting the Image/vedio Bank details by Id
       *******************************************************************************************/
        public IHttpActionResult GetPermissionInboundUpdateList(int id)
        {


            var InboundUpdate = _IPermissionsInboundService.getPermissionInboundUpdateById(id);

            if (InboundUpdate != null)
            {

                var _InboundUpdate = new
                {

                    Contractstatus = InboundUpdate.Contractstatus,
                    SignedContractSentDate = InboundUpdate.SignedContractSentDate,
                    SignedContractReceived_Date = InboundUpdate.SignedContractReceived_Date,
                    CancellationDate = InboundUpdate.CancellationDate,
                    Cancellation_Reason = InboundUpdate.Cancellation_Reason,
                    PendingRemarks = InboundUpdate.Remarks,
                    AgreementDate = InboundUpdate.AgreementDate,
                    Effectivedate = InboundUpdate.Effectivedate,
                    Contractperiodinmonth = InboundUpdate.Contractperiodinmonth,
                    Expirydate = InboundUpdate.Expirydate

                };

                if (InboundUpdate != null)
                {
                    IList<PermissionInboundDocuments> _PermissionInboundDocuments = _IPermissionsInboundService.getPermissionInboundDocumentsById(InboundUpdate.Id);

                    PendingRequestPermissionsInbound PendingRequestPermissionsInboundDetails = new PendingRequestPermissionsInbound();

                    PendingRequestPermissionsInboundDetails.DocumentIds = _PermissionInboundDocuments.Select(i => i.Id).ToArray();

                    PendingRequestPermissionsInboundDetails.Documentname = _PermissionInboundDocuments.Select(i => i.Documentname).ToArray();

                    foreach (var docs in _PermissionInboundDocuments)
                        PendingRequestPermissionsInboundDetails.DocumentFile = PendingRequestPermissionsInboundDetails.DocumentFile + docs.DocumentFile + ",";

                    return Json(new { _InboundUpdate, PendingRequestPermissionsInboundDetails });
                }

            }




            return Json("");


        }

        public IHttpActionResult RemovePermissionsInboundDocument(PermissionInboundDocuments Dcoument)
        {


            var documet = _IPermissionsInboundService.getInboundDocumentsDocumentsById(Dcoument.Id);
            string status = string.Empty;
            try
            {

                _IPermissionsInboundService.DeavtivatePermissionsInboundDocumentById(Dcoument.Id, Dcoument.EnteredBy);


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

        /**************************************************************************************
     Created By  : Saddam
     Created On  : 29th aug 2016
     Created For : getting the Other Bank details by Id
     *******************************************************************************************/
        public IHttpActionResult GetViewAssetStatus(int id)
        {


            var PermissionInboundOthers = _IPermissionsInboundService.getPermissionInboundOthersDetailsCopyRightHolderById(id);




            if (PermissionInboundOthers.PermissionInbound.AssetsType == "O" || PermissionInboundOthers.PermissionInbound.AssetsType == "B")
            {

                //var PermissionInboundOthers = _IPermissionsInboundService.getPermissionInboundOtherById(PermissionInbound.Id);


                var _mobj_PermissionInboundOthers = new
                {
                    status = (PermissionInboundOthers.statusId == null ? null : PermissionInboundOthers.StatusMaster.Status),
                    AssetSubType = (PermissionInboundOthers.AssetSubType == null ? null : PermissionInboundOthers.AssetSubType.AssetName),
                    AssetDescription = PermissionInboundOthers.AssetDescription,
                    Extent = PermissionInboundOthers.Extent,
                    Gratiscopytobesent = PermissionInboundOthers.Gratiscopytobesent,
                    Noofcopy = PermissionInboundOthers.Noofcopy,
                    OriginalSource = PermissionInboundOthers.OriginalSource,


                    Restriction = PermissionInboundOthers.Restriction,
                    SubLicensing = PermissionInboundOthers.SubLicensing,
                    Fee = (PermissionInboundOthers.Fee.ToString() == "0" ? null : PermissionInboundOthers.Fee.ToString()),
                    Currency = (PermissionInboundOthers.CurrencyId == null ? null : PermissionInboundOthers.CurrencyMaster.CurrencyName),


                    InvoiceNumber = PermissionInboundOthers.InvoiceNumber,
                    Invoicevalue = PermissionInboundOthers.Invoicevalue,


                    PermissionExpirydate = PermissionInboundOthers.PermissionExpirydate,
                    Acknowledgementline = PermissionInboundOthers.Acknowledgementline,
                    InboundRemarks = PermissionInboundOthers.InboundRemarks,


                    statusId = PermissionInboundOthers.statusId,
                    AssetSubTypeId = PermissionInboundOthers.AssetSubTypeId,
                    CurrencyId = PermissionInboundOthers.CurrencyId,
                  //  OthersDetailsCopyRightHolderId = PermissionInboundOthers.Id


                 
                    //OthersDetailsCopyRightHolderId =PermissionInboundOthers.PermissionInboundCopyRightHolderMaster.FirstOrDefault().Id,

                  OthersDetailsCopyRightHolderId =   _PermissionInboundCopyRightHolderMaster.Table.Where(a => a.Deactivate == "N" && a.InboundOthersId == PermissionInboundOthers.Id).FirstOrDefault().Id,
                    TerritoryRights =  PermissionInboundOthers.TerritoryRightsId

                    //PermissionInboundOthersId = PermissionInboundOthers.Id
                };




                var _Rights = (from POBRL in _PermissionInboundOthersRightsLink.Table.Where(a => a.Deactivate == "N")
                               join ORM in _OtherRightsMaster.Table.Where(a => a.Deactivate == "N")
                               on POBRL.RightsId equals ORM.Id
                               select new
                               {
                                   RightsName = ORM.RightsName,
                                   status = (POBRL.status == null ? null : POBRL.status),
                                   RunGranted = (POBRL.RunGranted == null ? "---" : POBRL.RunGranted),
                                   RunGrantedValue = (POBRL.RunGranted == null ? null : POBRL.RunGranted),

                                   Number = (POBRL.Number.ToString() == "0" ? "---" : POBRL.Number.ToString()),
                                   NumberValue = POBRL.Number,
                                   PIOID = POBRL.PIOID,
                                   RightsId = POBRL.RightsId
                               }

                                      ).ToList().Distinct().Where(a => a.PIOID == PermissionInboundOthers.Id).OrderBy(a => a.RightsId);



                IList<OtherContractDateRequest> DateRequest = _IPermissionsInboundService.getOtherContractDateRequestById(PermissionInboundOthers.Id);


                //var _DateRequest = new {

                //    dateOf = DateRequest.
                //};


                var _DateRequest = DateRequest.ToList().Select(a => new
                {
                    dateOf = a.dateOf,
                    dateValue = (a.dateValue == null ? "---" : a.dateValue.GetValueOrDefault().toDDMMYYYY()),
                    Date = a.dateValue


                }).ToList();


                return Json(new { _mobj_PermissionInboundOthers, _Rights, _DateRequest });
            }
            else
            {
                return Json("");
            }

        }

        /**************************************************************************************
    Created By  : Saddam
    Created On  : 29th aug 2016
    Created For : getting the Image/vedio Bank By id details by Id
    *******************************************************************************************/
        public IHttpActionResult GetImageVideoBankViewByIdDetails(int Id)
        {
            var PermissionInboundImageVideoBankData = _IPermissionsInboundService.getPermissionInboundImageVideoBankDetialById(Id);


            if (PermissionInboundImageVideoBankData != null)
            {

                var _PermissionInboundImageVideoBankData = new
                {
                    ContractTypes = PermissionInboundImageVideoBankData.ContractTypes,
                    imagevideobankid = PermissionInboundImageVideoBankData.imagevideobankid,
                    Description = PermissionInboundImageVideoBankData.Description,
                    invoicen = PermissionInboundImageVideoBankData.invoiceno,
                    invoicevalue = PermissionInboundImageVideoBankData.invoicevalue,
                    invoicedate = PermissionInboundImageVideoBankData.invoicedate,
                    printquantity = PermissionInboundImageVideoBankData.printquantity,
                    permissionexpirydate = PermissionInboundImageVideoBankData.permissionexpirydate,
                    weblink = PermissionInboundImageVideoBankData.weblink,
                    ImageVideoBankDataId = PermissionInboundImageVideoBankData.Id,
                    Imagevideobankid = PermissionInboundImageVideoBankData.imagevideobankid,
                    Credit_Lines = PermissionInboundImageVideoBankData.creditlines,
                    Remarks = PermissionInboundImageVideoBankData.Remarks,
                    Editorial_Only_Type = PermissionInboundImageVideoBankData.EditorialonlyType,

                    usage = PermissionInboundImageVideoBankData.usage,

                    ImageVideoBankId = PermissionInboundImageVideoBankData.Id,

                    Currency =  PermissionInboundImageVideoBankData.CurrencyId

                   // ImageBankLinkId = PermissionInboundImageVideoBankData.PermissionInboundImageVideoBank.Id
                    
                };

               // string PartyType = string.Empty;
                if (PermissionInboundImageVideoBankData.ImageBankPartyId != 0)
                {
                    var PartyType  = (from OCM in _OtherContractMaster.Table.Where(a => a.Deactivate == "N")
                                             join OCI in _OtherContractImageBank.Table.Where(a => a.Deactivate == "N")
                                             on OCM.Id equals OCI.othercontractid

                                             select new
                                             {
                                                 Id = OCI.Id,
                                                 partyname = OCM.partyname,

                                             }

                                 ).Distinct().Where(a => a.Id == PermissionInboundImageVideoBankData.ImageBankPartyId).FirstOrDefault();

                    return Json(new { _PermissionInboundImageVideoBankData, PartyType });
                }


                return Json(new { _PermissionInboundImageVideoBankData, PartyType = "" });



            }

            return Json("");
        }

        public IHttpActionResult TopSearch(String Code)
        {

            PermissionInbound PermissionInboundMaster = _PermissionInbound.Table.Where(a => a.Code == Code && a.Deactivate == "N").FirstOrDefault();

            if (PermissionInboundMaster != null)
            {
                if (PermissionInboundMaster.AuthorContractId != null)
                {
                    var _PermissionInboundMasterValue = new
                    {
                        Id = PermissionInboundMaster.Id,
                        CommeId = PermissionInboundMaster.AuthorContractId,
                        ProuductId = PermissionInboundMaster.TypeFor + PermissionInboundMaster.ProductId
                    };

                    return Json(new { _PermissionInboundMasterValue });
                }
                else
                {
                    var _PermissionInboundMasterValue = new
                    {
                        Id = PermissionInboundMaster.Id,
                        CommeId = PermissionInboundMaster.ProductLicenseId,
                        ProuductId = PermissionInboundMaster.TypeFor + PermissionInboundMaster.ProductId
                    };

                    return Json(new { _PermissionInboundMasterValue });

                }



            }
            else
            {
                string _PermissionInboundMasterValue = string.Empty;
                return Json(new { _PermissionInboundMasterValue });
            }

        }

        public IHttpActionResult GetMultipleImageVideoBankDetails(string Code)
        {


            //var _permissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(Code).Select(i => new
            //{

            //    AssetsType = i.AssetsType
            //});


            
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("code", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + Code + "'";
                var _GetImageVideoBankDetailsList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInBoundImageVideoBankDetails>("Proc_getInboundPermissionImageVideoBankDetails_get", parameters).ToList();


                //List<PermissionInBoundImageVideoBankDetails> _mobjGetCopyRightHolderList = new List<PermissionInBoundImageVideoBankDetails>();

                //SqlParameter[] parameters2 = new SqlParameter[1];
                //parameters2[0] = new SqlParameter("code", SqlDbType.VarChar, 200);
                //parameters2[0].Value = "'" + Code + "'";
                //_mobjGetCopyRightHolderList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInBoundImageVideoBankDetails>("Proc_getInboundPermissionCopyRightHolderDetails_get", parameters2).ToList();


            


            IList<PermissionInbound> PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(Code).ToList();
            IList<PermissionInboundOthers> _OtherContractImageBank = new List<PermissionInboundOthers>();
            IList<PermissionInboundImageVideoBank> _ImageBankData = new List<PermissionInboundImageVideoBank>();
            IList list = new ArrayList();
            IList partyDetails = new ArrayList();
            IList _cpyhlderdataList = new ArrayList();
            var InboundDetails = PermissionInbound.FirstOrDefault();
            var InboundObject = new
            {
                id = InboundDetails.Id,
                Code = InboundDetails.Code,
                ProductId = InboundDetails.ProductId,
                ProductCode = InboundDetails.ProductMaster.ProductCode,
                InboundType = InboundDetails.TypeFor,
                InboundForId = "", // InboundDetails.TypeFor == "C" ? InboundDetails.AuthorContractId : InboundDetails.ProductLicenseId,
                TypeForCode = "", //InboundDetails.TypeFor == "C" ? InboundDetails.AuthorContractOriginal.AuthorContractCode.ToString() : InboundDetails.ProductLicense.ProductLicensecode.ToString(),
                AssestsType = InboundDetails.AssetsType,
                Isbn = InboundDetails.ProductMaster.OUPISBN
            };
            
            foreach (var item in PermissionInbound)
            {
                //var _obj = item.PermissionInboundOthers.ToList();

                //if (_obj != null)
                //{
                //    //_OtherContractImageBank.Add(_obj.FirstOrDefault());
                //    //foreach (var cyhlder in _obj)
                //    //{
                //    //   _OtherContractImageBank.Add(cyhlder);
                //    //    ////var _cpyhlderdata = cyhlder.PermissionInboundCopyRightHolderMaster.ToList().Select(d => new
                //    //    ////{
                //    //    ////    Id = d.Id,
                //    //    ////    CopyRightHolderCode = d.CopyRightHolderCode,
                //    //    ////    CopyRightHolderName = d.CopyRightHolderName,
                //    //    ////    ContactPerson = d.ContactPerson,
                //    //    ////    Mobile = d.Mobile,
                //    //    ////    Email = d.Email,
                //    //    ////    PermissionInboundId = InboundDetails.Id,
                //    //    ////    code = InboundDetails.Code,
                //    //    ////});

                //    //    //_cpyhlderdataList.Add(cyhlder);
                //    //}
                //} 


                //if (_obj != null)
                //{



                //    _OtherContractImageBank.Add(_obj);
                    
                //}

                var _ImageBank = item.PermissionInboundImageVideoBank.FirstOrDefault();
                //_ImageBank.OtherContractImageBank.OtherContractMaster.partyname

              
                
                if (_ImageBank != null)
                {
                    var _partyDetails = new
                    {
                        partyName = _ImageBank.OtherContractImageBank.OtherContractMaster.partyname,
                        Restriction = _ImageBank.OtherContractImageBank.restriction,
                        printrights = _ImageBank.OtherContractImageBank.PrintRights,
                        ebookrights = _ImageBank.OtherContractImageBank.ebookrights,
                        ElectronicRights = _ImageBank.OtherContractImageBank.electronicrights,
                        fieldsetdata = _ImageBank.OtherContractImageBank.VideoImageBank.ToList().Select(i => new {
                                        type=i.Type,
                                        shortname=i.ShortName,
                                        fullname=i.Fullname,
                                        currencyid=i.CurrencyId,
                                        Cost=i.Cost,
                                        CurrencySysbol = i.CurrencyMaster.Symbol
                                    }).ToList(),

                        

                    };
                    partyDetails.Add(_partyDetails);  

                    var obj = _ImageBank.PermissionInboundImageVideoBankData.ToList().Select(i => new
                    {
                        ContractTypes = i.ContractTypes == null ? "" : i.ContractTypes,
                        description = i.Description == null ? "" : i.Description,
                        invoiceno = i.invoiceno == null ? "" : i.invoiceno,
                      //  invoicedate = i.invoicedate != null ? i.invoicedate.toDDMMYYYY() : "",

                        invoicedate =   i.invoicedate != null ? i.invoicedate.GetValueOrDefault().toDDMMYYYY() : "",

                        printquantity = i.printquantity != null ? i.printquantity : 0,
                        permissionExpirydate = i.permissionexpirydate != null ? i.permissionexpirydate.GetValueOrDefault().toDDMMYYYY() : "",
                        id = i.Id,
                        weblink = i.weblink != null ? i.weblink : null,
                        PartyName = _ImageBank.OtherContractImageBank.OtherContractMaster.partyname,

                        PartyNameId = _ImageBank.OtherContractImageBank.OtherContractMaster.Id,
                        creditlines = i.creditlines == null ? "" : i.creditlines,

                    });
                    list.Add(obj);
                }
            }


         //for (_GetCopyRightHolderDetailsList.Count > 0)
         //{

         //}

           // string  PermissionInBoundId = string.Empty;

           // string StringBVaLue = "1283,1284,1285";
           // if (PermissionInbound.Count > 0)
           // { 
           //for (int i = 0; i< PermissionInbound.Count; i++)
           //{
           //      PermissionInBoundId = PermissionInBoundId +  PermissionInbound[i].Id + ",";

           //}
           //if (PermissionInBoundId != null && PermissionInBoundId != "")
           //{
           //    _OtherContractImageBank = _IPermissionsInboundService.getPermissionInboundOthersList(StringBVaLue);
           //}
                
           // }
            var _obj = _PermissionInboundOthers.Table.Where(a => a.Deactivate == "N" && a.PermissionInbound.Code == Code).ToList();


            foreach (var cyhlder in _obj)
            {
                _OtherContractImageBank.Add(cyhlder);
            }

     
          
                foreach (var cyhlder in _OtherContractImageBank)
                {

                    //for (_GetCopyRightHolderDetailsList.Count > 0)
                    //{


                    //}
                    var _cpyhlderdata = _PermissionInboundCopyRightHolderMaster.Table.Where(a => a.Deactivate == "N" && a.InboundOthersId == cyhlder.Id).ToList().Select(i => new
                    // var _cpyhlderdata = cyhlder.PermissionInboundCopyRightHolderMaster.ToList().Select(i => new
                    {
                        //Id = d.Id,
                        //CopyRightHolderCode = d.CopyRightHolderCode,
                        //CopyRightHolderName = d.CopyRightHolderName,
                        //ContactPerson = d.ContactPerson,
                        //Mobile = d.Mobile,
                        //Email = d.Email,
                        //PermissionInboundId = InboundDetails.Id,
                        //code = InboundDetails.Code,
                        // Id = i.Id,

                        Id = i.InboundOthersId,
                        InboundId = cyhlder.PermissionInboundId,


                        CopyRightHolderId = i.Id,
                        InboundOthersId = i.InboundOthersId,

                        AssetSubType = cyhlder.AssetSubTypeId == null ? "" : cyhlder.AssetSubType.AssetName,
                        AssestDescription = cyhlder.AssetDescription == null ? "" : cyhlder.AssetDescription,
                        ContactPerson = i.ContactPerson,
                        Address = i.Address,
                        Mobile = i.Mobile,
                        CopyRightHolder = i.CopyRightHolderName,

                        StatusName = cyhlder.statusId == null ? "" : cyhlder.StatusMaster.Status,
                        PermissionExpirydate = cyhlder.PermissionExpirydate,

                        CopyRightHolderCode = i.CopyRightHolderCode,

                        //TotalQty = cyhlder.PermissionInboundOthersRightsLink.FirstOrDefault().Number,


                        //TotalQty = cyhlder.PermissionInboundOthersRightsLink.ToList().Select(j => new
                        //{
                        //    ToatlQtyValue = j.Number,

                        //}).Sum(k => k.ToatlQtyValue),


                        TotalQty = _PermissionInboundOthersRightsLink.Table.Where(a => a.Deactivate == "N" && a.PIOID == cyhlder.Id).ToList().Select(j => new
                        {
                            ToatlQtyValue = j.Number,
                        }).Sum(k => k.ToatlQtyValue),

                        //Rights = cyhlder.PermissionInboundOthersRightsLink.ToList().Select(j => new
                        //{
                        //    RightsName = j.OtherRightsMaster.RightsName,
                        //    Rightsvalue = j.status
                        //})


                        Rights = _PermissionInboundOthersRightsLink.Table.Where(a => a.Deactivate == "N" && a.PIOID == cyhlder.Id).ToList().Select(j => new
                        {
                            RightsName = j.OtherRightsMaster.RightsName,
                            Rightsvalue = j.status,
                            Number = j.Number == 0 ? null : j.Number,
                        }),
                         
                        CopyRightHolderMasterId = _CopyRightHolderMaster.Table.Where(c => c.Deactivate == "N"  &&
                                                   c.CopyRightHolderCode.Trim() == i.CopyRightHolderCode.Trim()).ToList().Select(c => new 
                                                    { CopyRightHolderMasterId = c.Id })

                    });
                    _cpyhlderdataList.Add(_cpyhlderdata);
                }

            
         

            



            //foreach (var cyhlder in _mobjGetCopyRightHolderList)
            //{

            //    //for (_GetCopyRightHolderDetailsList.Count > 0)
            //    //{


            //    //}

            //    //var _I_cpyhlderdata = cyhlder.
            //   var _cpyhlderdata =  new
            //    {
                   

            //        Id = cyhlder.Id,

            //        CopyRightHolderId = cyhlder.CopyRightHolderId,
            //        InboundOthersId =cyhlder.InboundOthersId,

            //        AssetSubType = cyhlder.AssetSubType,

            //   AssestDescription =  cyhlder.AssestDescription,
                    
            //        ContactPerson = cyhlder.ContactPerson,
            //        Address = cyhlder.Address,
            //        Mobile = cyhlder.Mobile,
            //        CopyRightHolder = cyhlder.CopyRightHolder,

            //        StatusName = cyhlder.StatusName,

            //       // CopyRightHolderCode = cyhlder.CopyRightHolderCode,

            //        //TotalQty = cyhlder.PermissionInboundOthersRightsLink.FirstOrDefault().Number,


            //        TotalQty = cyhlder.TotalQty,




            //        //Rights = cyhlder.PermissionInboundOthersRightsLink.ToList().Select(j => new
            //        //{
            //        //    RightsName = j.OtherRightsMaster.RightsName,
            //        //    Rightsvalue = j.status
            //        //})


            //    };
            //    _cpyhlderdataList.Add(_cpyhlderdata);
            //}




            //var _otherContract = _OtherContractImageBank.Select(i =>
            //    new
            //    {
            //        Id = i.Id,
            //        AssetSubType = i.AssetSubTypeId == null ? "" : i.AssetSubType.AssetName,
            //        AssestDescription = i.AssetSubTypeId == null ? "" : i.AssetDescription,
            //        ContactPerson = i.PermissionInboundCopyRightHolderMaster.FirstOrDefault().ContactPerson,
            //        Address = i.PermissionInboundCopyRightHolderMaster.FirstOrDefault().Address,
            //        Mobile = i.PermissionInboundCopyRightHolderMaster.FirstOrDefault().Mobile,
            //        CopyRightHolder = i.PermissionInboundCopyRightHolderMaster.CopyRightHolderName,
            //        Rights  = i.PermissionInboundOthersRightsLink.ToList().Select(j=>new{
            //                            RightsName = j.OtherRightsMaster.RightsName,
            //                            Rightsvalue = j.status
            //                    })
                    
            //         });


            //if (PermissionInbound != null)
            //{

            //    if (PermissionInbound.Count > 0)
            //    {
            //        int j = 0;
            //        for ( j = 0; j < PermissionInbound.Count; j++)
            //        {

            //            if (PermissionInbound[j].AssetsType == "I" || PermissionInbound[j].AssetsType == "B")
            //            {
            //                var ImageVideoBankLink = _IPermissionsInboundService.getPermissionInboundImageVideoBankById(PermissionInbound[j].Id);

            //                var _ImageVideoBankLinkId = new
            //                {
            //                    ImageBankId = ImageVideoBankLink.ImageBankId,
            //                    ImageVideoBankLinkId = ImageVideoBankLink.Id
            //                };


            //                var OtherContractImageBank = _IPermissionsInboundService.OtherContractImageBankById(_ImageVideoBankLinkId.ImageBankId);


            //                var _OtherContractImageBank = new
            //                {
            //                    PrintRights = OtherContractImageBank.PrintRights,
            //                    electronicrights = OtherContractImageBank.electronicrights,
            //                    ebookrights = OtherContractImageBank.ebookrights,
            //                    othercontractid = OtherContractImageBank.othercontractid,
            //                    restriction = OtherContractImageBank.restriction
            //                };


            //                var PartyNameDetail = _IPermissionsInboundService.OtherContractMasterById(_OtherContractImageBank.othercontractid);

            //                var _partname = new
            //                {
            //                    PartyName = PartyNameDetail.partyname,
            //                    Id = PartyNameDetail.Id
            //                };


            //                IList<PermissionInboundImageVideoBankData> PermissionInboundImageVideoBankData = _IPermissionsInboundService.getPermissionInboundImageVideoBankDataById(_ImageVideoBankLinkId.ImageVideoBankLinkId);

            //                ///   IList<PermissionInboundImageVideoBank> X = PermissionInbound.PermissionInboundImageVideoBank.ToList();



            //                var _PermissionInboundImageVideoBankData = PermissionInboundImageVideoBankData.ToList().Select(a => new
            //                {
            //                    ContractTypes = a.ContractTypes,
            //                    imagevideobankid = a.imagevideobankid,
            //                    Description = a.Description,
            //                    invoicen = a.invoiceno,
            //                    invoicevalue = a.invoicevalue,
            //                    invoicedate = a.invoicedate.toDDMMYYYY(),
            //                    printquantity = a.printquantity,
            //                    permissionexpirydate = a.permissionexpirydate == null ? null : Convert.ToDateTime(a.permissionexpirydate).toDDMMYYYY(),
            //                    weblink = a.weblink,
            //                    ImageVideoBankDataId = a.Id,
            //                    Imagevideobankid = a.imagevideobankid,
            //                    Credit_Lines = a.creditlines,
            //                    Remarks = a.Remarks,
            //                    Editorial_Only_Type = a.EditorialonlyType

            //                }).ToList();


            //                var mobj_partyDetailsView = new
            //                {
            //                    Restriction = OtherContractImageBank.restriction,
            //                    PrintRights = OtherContractImageBank.PrintRights,
            //                    Electronicrights = OtherContractImageBank.electronicrights,
            //                    Ebookrights = OtherContractImageBank.ebookrights,
            //                    Id = OtherContractImageBank.Id
            //                };
            //                var videoimagebankView = OtherContractImageBank.VideoImageBank.ToList().Select(i => new
            //                {
            //                    BankType = i.Type,
            //                    fullname = i.Fullname,
            //                    CurrencySysbol = i.CurrencyMaster.Symbol,
            //                    Cost = i.Cost,
            //                    ShrotName = i.ShortName
            //                }).ToList();


            //                return Json(new { _permissionInbound, _ImageVideoBankLinkId, _PermissionInboundImageVideoBankData, mobj_partyDetailsView, videoimagebankView, _OtherContractImageBank, _partname });

            //            }
            //            else
            //            {
            //                return Json("");

            //            }
            //        }

            //    }
            //}



            return Json(new { InboundObject, list, partyDetails, _cpyhlderdataList, _GetImageVideoBankDetailsList });


            //return Json("");


        }

        public IHttpActionResult GetAllPermissionInboundUpdateList(string code)
        {


            IList<PermissionInbound> PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsByCode(code).ToList();



            if (PermissionInbound != null)
            {
                if (PermissionInbound.Count > 0)
                {
                    for (int j = 0; j < PermissionInbound.Count; j++ )
                    {
                        var InboundUpdate = _IPermissionsInboundService.getPermissionInboundUpdateById(PermissionInbound[j].Id);

                        if (InboundUpdate != null)
                        {

                            var _InboundUpdate = new
                            {

                                Contractstatus = InboundUpdate.Contractstatus,
                                SignedContractSentDate = InboundUpdate.SignedContractSentDate,
                                SignedContractReceived_Date = InboundUpdate.SignedContractReceived_Date,
                                CancellationDate = InboundUpdate.CancellationDate,
                                Cancellation_Reason = InboundUpdate.Cancellation_Reason,
                                PendingRemarks = InboundUpdate.Remarks,
                                AgreementDate = InboundUpdate.AgreementDate,
                                Effectivedate = InboundUpdate.Effectivedate,
                                Contractperiodinmonth = InboundUpdate.Contractperiodinmonth,
                                Expirydate = InboundUpdate.Expirydate

                            };

                            if (InboundUpdate != null)
                            {
                                IList<PermissionInboundDocuments> _PermissionInboundDocuments = _IPermissionsInboundService.getPermissionInboundDocumentsById(InboundUpdate.Id);

                                PendingRequestPermissionsInbound PendingRequestPermissionsInboundDetails = new PendingRequestPermissionsInbound();

                                PendingRequestPermissionsInboundDetails.DocumentIds = _PermissionInboundDocuments.Select(i => i.Id).ToArray();

                                PendingRequestPermissionsInboundDetails.Documentname = _PermissionInboundDocuments.Select(i => i.Documentname).ToArray();

                                foreach (var docs in _PermissionInboundDocuments)
                                    PendingRequestPermissionsInboundDetails.DocumentFile = PendingRequestPermissionsInboundDetails.DocumentFile + docs.DocumentFile + ",";

                                return Json(new { _InboundUpdate, PendingRequestPermissionsInboundDetails });
                            }

                        }
                    }
                }
            }
            



            




            return Json("");


        }

        public IHttpActionResult GetTerritoryRightsName(int  Id)
        {

            TerritoryRightsMaster territoryRightsMaster = _TerritoryRightsMaster.Table.Where(a => a.Deactivate == "N" && a.Id == Id).FirstOrDefault();
            if (territoryRightsMaster != null)
            { 
            
            var TerritoryRightsMaster = new {
                Territoryrights = territoryRightsMaster.Territoryrights
            };

            return Json(TerritoryRightsMaster);
            }


            



            




            return Json("");


        }

        [HttpGet]
        public IHttpActionResult checkInboundDetailsByProductId(string productId)
        {
            List<PermissionInbound> mobj_PIDetails = _IPermissionsInboundService.getInboundPermissionDetailsByProductId(Convert.ToInt32(productId)).ToList();
            var inboundDetail= mobj_PIDetails.Take(1).FirstOrDefault();
            var inboundCode = new
            {
                Code = inboundDetail.Code
            };
            return Json(inboundCode);
        }

        /* Create By  : Prakash
       * Create on  : 20 Sep, 2017
       * Create for : Delete Permission Inbound
       */
        [HttpPost]
        public IHttpActionResult DeletePermissionInboundSet(PermissionInbound mobj_PermissionInbound)
        {
            string status = string.Empty;

            try
            {
                if (mobj_PermissionInbound.Code != null && mobj_PermissionInbound.Code != "")
                {
                    IList<PermissionInbound> _PermissionInboundList = _IPermissionsInboundService.getInboundPermissionDetailsByCode(mobj_PermissionInbound.Code);
                    foreach (PermissionInbound item in _PermissionInboundList)
                    {
                        PermissionInbound _PermissionInbound = _IPermissionsInboundService.getInboundPermissionDetailsById(item.Id);
                        _PermissionInbound.Deactivate = "Y";
                        _PermissionInbound.DeactivateBy = mobj_PermissionInbound.DeactivateBy;
                        _PermissionInbound.DeactivateDate = DateTime.Now;

                        _IPermissionsInboundService.DeletePermissionInbound(_PermissionInbound);
                    }
                }
                status = "OK";

                return Json(status);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }

        /* Create By  : Prakash
       * Create on  : 20 Sep, 2017
       * Create for : Delete Permission Inbound
       */
        [HttpPost]
        public IHttpActionResult DeleteImageVideoBankData(PermissionInBoundImageVideoBankDetails mobj_PermissionInBoundImageVideoBankDetails)
        {
            string status = string.Empty;

            try
            {
                if (mobj_PermissionInBoundImageVideoBankDetails.Id != null && mobj_PermissionInBoundImageVideoBankDetails.Id != 0)
                {

                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("Id", SqlDbType.VarChar, 100);
                    parameters[0].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.Id + "'";
                    parameters[1] = new SqlParameter("LinkId", SqlDbType.VarChar, 100);
                    parameters[1].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.LinkId + "'";
                    parameters[2] = new SqlParameter("DataId", SqlDbType.VarChar, 100);
                    parameters[2].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.DataId + "'";
                    parameters[3] = new SqlParameter("OthersId", SqlDbType.VarChar, 100);
                    parameters[3].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.OthersId + "'";
                    parameters[4] = new SqlParameter("Type", SqlDbType.VarChar, 100);
                    parameters[4].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.Type + "'";
                    parameters[5] = new SqlParameter("DeactivateBy", SqlDbType.VarChar, 100);
                    parameters[5].Value = "'" + mobj_PermissionInBoundImageVideoBankDetails.DeactivateBy + "'";

                    var _result = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInBoundImageVideoBankDetails>("Proc_deletePermissionInboundEntityData_set", parameters);
                    
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
    public class CopyClass
    {
        /// <summary>
        /// Copy an object to destination object, only matching fields will be copied
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceObject">An object with matching fields of the destination object</param>
        /// <param name="destObject">Destination object, must already be created</param>
        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            //  If either the source, or destination is null, return
            if (sourceObject == null || destObject == null)
                return;

            //  Get the type of each object
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();

            //  Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;

                //  Set the value in the destination
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }
    }
}