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
using ACS.Services.Master;
using System.Web.Script.Serialization;
using ACS.Services.User;
using SLV.Model.PaymentTaggingMaster;
using SLV.Model.RightsSelling;
using Logger;
using ACS.Core.Domain.RightsSelling;
using ACS.Core.Domain.PermissionsOutbound;
using ACS.Services.PermissionsOutbound;



namespace SLV.API.Controllers.PaymentTaggingMaster
{
    public class PaymentTaggingMasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IDbContext _dbContext;
        private readonly IRightsSelling _RightsSelling;
        private readonly IPermissionsOutboundService _PermissionsOutboundService;

        public PaymentTaggingMasterController(

                IDbContext dbContext
            , IRepository<LicenseeMaster> LicenseeMaster
            , IRightsSelling IRightsSelling
            , IRepository<ApplicationSetUp> ApplicationSetUp
            , IApplicationSetUpService ApplicationSetUpService

            , IRightsSelling RightsSelling
            , IPermissionsOutboundService PermissionsOutboundService

            )
        {
            this._dbContext = dbContext;

            this._RightsSelling = RightsSelling;
            this._PermissionsOutboundService = PermissionsOutboundService;
        }

        public IHttpActionResult GetListForRights(String SessionId)
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
                    var _GetPaymentTaggingList = _dbContext.ExecuteStoredProcedureListNewData<PaymentTaggingModel>("Proc_PaymentTagging_Search_RS_get", parameters).ToList();
                    return Json(_GetPaymentTaggingList);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public IHttpActionResult GetListForOutbound(String SessionId)
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
                    var _GetPaymentTaggingList = _dbContext.ExecuteStoredProcedureListNewData<PaymentTaggingModel>("Proc_PaymentTagging_Search_PO_get", parameters).ToList();
                    return Json(_GetPaymentTaggingList);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //added on 11 Aug, 2017
        public IHttpActionResult GetList_PaymentNotReceived_ByDashboard(String For)
        {
            try
            {
                if (For == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("For", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + For + "'";
                    var _GetPaymentTaggingList = _dbContext.ExecuteStoredProcedureListNewData<PaymentTaggingModel>("Proc_PaymentTagging_Search_ByDashboard_get", parameters).ToList();
                    return Json(_GetPaymentTaggingList);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        //Payment Tagging List get
        public IHttpActionResult getPaymentTaggingList(String Type)
        {
            try
            {

                if (Type == "")
                {
                    return Json("NOK");
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("Type", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + Type + "'";
                    var _GetPaymentTaggingList = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingPaymentTaggingModel>("Proc_PaymentTaggingList_get", parameters).ToList();
                    return Json(_GetPaymentTaggingList);
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PaymentTaggingMasterController.cs", "getPaymentTaggingList", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PaymentTaggingMasterController.cs", "getPaymentTaggingList", ex);
                return Json(ex.InnerException.Message);
            }

        }

        /* Create By  : Prakash
       * Create on  : 009 Oct, 2017
       * Create for : Delete Payment Information
       */
        [HttpPost]
        public IHttpActionResult DeletePaymentTaggingDetailsSet(RightsSellingPaymentTaggingModel _RightsSellingPaymentTaggingModel)
        {
            string status = string.Empty;

            try
            {
                if (_RightsSellingPaymentTaggingModel.PaymentTaggingId != 0)
                {
                    if (_RightsSellingPaymentTaggingModel.Type == "rights")
                    {
                        RightsSellingPaymentTagging obj_RightsSellingPaymentTaggingModel = _RightsSelling.getRightsSellingPaymentTaggingById(_RightsSellingPaymentTaggingModel.PaymentTaggingId);
                        obj_RightsSellingPaymentTaggingModel.Deactivate = "Y";
                        obj_RightsSellingPaymentTaggingModel.DeactivateBy = _RightsSellingPaymentTaggingModel.DeactivateBy;
                        obj_RightsSellingPaymentTaggingModel.DeactivateDate = DateTime.Now;
                        _RightsSelling.DeleteRightsSellingPaymentTagging(obj_RightsSellingPaymentTaggingModel);
                    }
                    else
                    {
                        PermissionsOutboundPaymentTagging obj_PermissionsOutboundPaymentTagging = _PermissionsOutboundService.getPermissionsOutboundPaymentTaggingById(_RightsSellingPaymentTaggingModel.PaymentTaggingId);
                        obj_PermissionsOutboundPaymentTagging.Deactivate = "Y";
                        obj_PermissionsOutboundPaymentTagging.DeactivateBy = _RightsSellingPaymentTaggingModel.DeactivateBy;
                        obj_PermissionsOutboundPaymentTagging.DeactivateDate = DateTime.Now;
                        _PermissionsOutboundService.DeletePermissionsOutboundPaymentTagging(obj_PermissionsOutboundPaymentTagging);
                    }

                    status = "OK";
                }

                return Json(status);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PaymentTaggingMasterController.cs", "DeletePaymentTaggingDetailsSet", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PaymentTaggingMasterController.cs", "DeletePaymentTaggingDetailsSet", ex);
                return Json(ex.InnerException.Message);
            }

        }

        
    }
}