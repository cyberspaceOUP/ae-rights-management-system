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
using ACS.Services.PermissionsInbound;
using Logger;

namespace SLV.API.Controllers.Master
{
    //Added by Prakash on 07 Oct, 2017
    public class AssetMasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IPermissionsInboundService _PermissionsInboundService;
        private readonly ILocalizationService _localizationService;

        public AssetMasterController(
            IPermissionsInboundService PermissionsInboundService,
             ILocalizationService localizationService
            )
        {
            //_AssetMasterService = AssetMasterService;
            _PermissionsInboundService = PermissionsInboundService;
            _localizationService = localizationService;
        }


        #region "Asset Sub-Type"

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="AssetSubType">accepts Asset Sub-Type object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getAssetSubType(int Id)
        {
            try
            {
                AssetSubType assetSubType = _PermissionsInboundService.GetAssetSubTypeById(Id);
                var _AssetSubType = new
                {
                    Id = assetSubType.Id,
                    AssetName = assetSubType.AssetName
                };

                return Json(_AssetSubType);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Api method to insert Asset Sub-Type
        /// </summary>
        /// <param name="AssetSubType">accepts Asset Sub-Type object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertAssetSubType(AssetSubType _AssetSubType)
        {
            string status = "";
            try
            {
                status = _PermissionsInboundService.DuplicityCheckAssetSubType(_AssetSubType);
                if (status == "Y")
                {
                    if (_AssetSubType.Id == 0)
                    {
                        _PermissionsInboundService.InsertAssetSubType(_AssetSubType);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        AssetSubType assetSubType = _PermissionsInboundService.GetAssetSubTypeById(_AssetSubType.Id);
                        assetSubType.AssetName = _AssetSubType.AssetName;
                        assetSubType.ModifiedBy = _AssetSubType.EnteredBy;
                        assetSubType.ModifiedDate = DateTime.Now;
                        _PermissionsInboundService.UpdateAssetSubType(assetSubType);
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
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "InsertAssetSubType", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "InsertAssetSubType", ex);
                status = ex.InnerException.Message;
            }
            return Json(status);
        }

        /// <summary>
        /// Api method to delete Asset Sub-Type
        /// </summary>
        /// <param name="AssetSubType">accepts Asset Sub-Type object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteAssetSubType(AssetSubType _AssetSubType)
        {

            string status = string.Empty;
            try
            {
                AssetSubType assetSubType = _PermissionsInboundService.GetAssetSubTypeById(_AssetSubType.Id);
                assetSubType.Deactivate = "Y";
                assetSubType.DeactivateBy = _AssetSubType.EnteredBy;
                assetSubType.DeactivateDate = DateTime.Now;
                _PermissionsInboundService.UpdateAssetSubType(assetSubType);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "DeleteAssetSubType", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "DeleteAssetSubType", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }
        
        #endregion

        #region "Asset Status"

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="StatusMaster">accepts StatusMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getStatusMaster(int Id)
        {
            try
            {
                StatusMaster statusMaster = _PermissionsInboundService.GetStatusMasterById(Id);
                var _StatusMaster = new
                {
                    Id = statusMaster.Id,
                    Status = statusMaster.Status
                };

                return Json(_StatusMaster);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "getStatusMaster", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "getStatusMaster", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Api method to insert StatusMaster
        /// </summary>
        /// <param name="StatusMaster">accepts StatusMaster object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertStatusMaster(StatusMaster _StatusMaster)
        {
            string status = "";
            try
            {
                status = _PermissionsInboundService.DuplicityCheckStatusMaster(_StatusMaster);
                if (status == "Y")
                {
                    if (_StatusMaster.Id == 0)
                    {
                        _PermissionsInboundService.InsertStatusMaster(_StatusMaster);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        StatusMaster atatusMaster = _PermissionsInboundService.GetStatusMasterById(_StatusMaster.Id);
                        atatusMaster.Status = _StatusMaster.Status;
                        atatusMaster.ModifiedBy = _StatusMaster.EnteredBy;
                        atatusMaster.ModifiedDate = DateTime.Now;
                        _PermissionsInboundService.UpdateStatusMaster(atatusMaster);
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
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "InsertStatusMaster", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "InsertStatusMaster", ex);
                status = ex.InnerException.Message;
            }
            return Json(status);
        }
        
        /// <summary>
        /// Api method to delete StatusMaster
        /// </summary>
        /// <param name="StatusMaster">accepts StatusMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteStatusMaster(StatusMaster _StatusMaster)
        {

            string status = string.Empty;
            try
            {
                StatusMaster statusMaster = _PermissionsInboundService.GetStatusMasterById(_StatusMaster.Id);
                statusMaster.Deactivate = "Y";
                statusMaster.DeactivateBy = _StatusMaster.EnteredBy;
                statusMaster.DeactivateDate = DateTime.Now;
                _PermissionsInboundService.UpdateStatusMaster(statusMaster);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "DeleteStatusMaster", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AssetMasterController.cs", "DeleteStatusMaster", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        #endregion


    }
}