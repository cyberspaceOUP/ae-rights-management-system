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
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Data;

namespace SLV.API.Controllers.Master
{
    //Added by Prakash on 07 Oct, 2017
    public class AuthorTypeMasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly ICommonListService _CommonListService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<AuthorContractauthordetails> _AuthorContractauthordetails;
        private readonly IRepository<AuthorType> _AuthorType;

        public AuthorTypeMasterController(
            ICommonListService CommonListService,
             ILocalizationService localizationService,
             IRepository<AuthorContractauthordetails> authorContractauthordetails,
             IRepository<AuthorType> authorType
            )
        {
            _CommonListService = CommonListService;
            _localizationService = localizationService;
            this._AuthorContractauthordetails = authorContractauthordetails;
            this._AuthorType = authorType;
        }
        
        #region "Author Type"

        [HttpGet]
        public IHttpActionResult getAuthorTypeList()
        {
            try
            {
                var _used = _AuthorContractauthordetails.Table.Where(a => a.Deactivate == "N").Select(a => a.Authortype).ToList();

                var mobj_AT = _CommonListService.GetAllAuthorTypeList().ToList();
                var mobj_StatusVlaue = mobj_AT.Select(x => new
                {
                    Id = x.Id,
                    AuthorTypeName = x.AuthorTypeName,

                    flag = _used.Where(r => r == x.Id).Select(r => r).FirstOrDefault() != 0 ? 1 : 0,

                }).Distinct();
                return Json(mobj_StatusVlaue);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "getAssetSubType", ex);
                return Json(ex.InnerException);
            }
            return Json("");
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="StatusMaster">accepts StatusMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getAuthorTypeMasterById(int Id)
        {
            try
            {
                AuthorType _authorType = _CommonListService.GetAllAuthorTypeList().Where(a => a.Id == Id).FirstOrDefault();
                var _Master = new
                {
                    Id = _authorType.Id,
                    AuthorTypeName = _authorType.AuthorTypeName
                };

                return Json(_Master);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "getAuthorTypeMasterById", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "getAuthorTypeMasterById", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Api method to insert Author Type
        /// </summary>
        /// <param name="StatusMaster">accepts Author Type object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertAuthorType(AuthorType _obj)
        {
            string status = "";
            try
            {

                if (_obj.Id == 0)
                {
                    var check = _CommonListService.GetAllAuthorTypeList().Where(a => a.AuthorTypeName == _obj.AuthorTypeName).FirstOrDefault();
                    if (check == null)
                    {
                        _CommonListService.InsertAuthorType(_obj);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        status = "Duplicate";
                    }
                }
                else
                {
                    var check = _CommonListService.GetAllAuthorTypeList().Where(x => x.AuthorTypeName == _obj.AuthorTypeName
                                                                                 && x.Deactivate == "N"
                                                                                 && (_obj.Id != 0 ? x.Id : 0) != (_obj.Id != 0 ? _obj.Id : 1)).FirstOrDefault();
                    if (check == null)
                    {
                        AuthorType _authorType = _CommonListService.GetAllAuthorTypeList().Where(a => a.Id == _obj.Id).FirstOrDefault();
                        _authorType.AuthorTypeName = _obj.AuthorTypeName;
                        _authorType.ModifiedBy = _obj.EnteredBy;
                        _authorType.ModifiedDate = DateTime.Now;
                        _CommonListService.UpdateAuthorType(_authorType);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        status = "Duplicate";
                    }
                }
                

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "InsertStatusMaster", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "InsertStatusMaster", ex);
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
        public IHttpActionResult DeleteAuthorType(AuthorType _obj)
        {

            string status = string.Empty;
            try
            {
                AuthorType _authorType = _CommonListService.GetAllAuthorTypeList().Where(a => a.Id == _obj.Id).FirstOrDefault();
                _authorType.Deactivate = "Y";
                _authorType.DeactivateBy = _obj.EnteredBy;
                _authorType.DeactivateDate = DateTime.Now;
                _CommonListService.UpdateAuthorType(_authorType);
                status = _localizationService.GetResource("Master.API.Success.Message");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "DeleteAuthorType", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AuthorTypeMasterController.cs", "DeleteAuthorType", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        #endregion


    }
}