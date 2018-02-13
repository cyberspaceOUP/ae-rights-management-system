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
using ACS.Services.Product;
using Logger;

namespace SLV.API.Controllers.Master
{
    public class LanguageMasterController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        //Added by Ankush Kumar on 11/07/2016
        private readonly ILanguageMasterService _languageMasterService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductMasterService _ProductMasterService;

        public LanguageMasterController(
            ILanguageMasterService languageMasterService,
             ILocalizationService localizationService,
            IProductMasterService ProductMasterService
            )
        {
            _languageMasterService = languageMasterService;
            _localizationService = localizationService;
            _ProductMasterService = ProductMasterService;
        }


        //Added By Ankush Kumar on 15/07/2016
        #region Language

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="LanguageMaster">accepts LanguageMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getLanguage(int Id)
        {
            try
            {
                LanguageMaster LanguageMaster = _languageMasterService.GetLanguageById(Id);
                var _LanguageMaster = new
                {
                    Id = LanguageMaster.Id,
                    LanguageName = LanguageMaster.LanguageName
                };

                return Json(_LanguageMaster);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "getLanguage", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "getLanguage", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        /// <summary>
        /// Api method to insert LanguageMaster
        /// </summary>
        /// <param name="LanguageMaster">accepts LanguageMaster object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertLanguage(LanguageMaster Language)
        {
            string status = "";
            try
            {
                status = _languageMasterService.DuplicityCheck(Language);
                if (status == "Y")
                {
                    if (Language.Id == 0)
                    {
                        _languageMasterService.InsertLanguage(Language);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        LanguageMaster _Language = _languageMasterService.GetLanguageById(Language.Id);
                        _Language.LanguageName = Language.LanguageName;
                        _Language.ModifiedBy = Language.EnteredBy;
                        _Language.ModifiedDate = DateTime.Now;
                        _languageMasterService.UpdateLanguage(_Language);
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
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "InsertLanguage", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "InsertLanguage", ex);
                status = ex.InnerException.Message;
            }
            return Json(status);
        }


        /// <summary>
        /// Api method to update LanguageMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts LanguageMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateLanguage(LanguageMaster Language)
        {
            string status = string.Empty;
            try
            {
                LanguageMaster _Language = _languageMasterService.GetLanguageById(Language.Id);
                _Language.Deactivate = Language.LanguageName;
                _Language.ModifiedBy = Language.EnteredBy;
                _Language.ModifiedDate = DateTime.Now;
                _languageMasterService.UpdateLanguage(_Language);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "UpdateLanguage", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "UpdateLanguage", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        /// <summary>
        /// Api method to delete LanguageMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts LanguageMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteLanguage(LanguageMaster Language)
        {

            string status = string.Empty;
            try
            {
                LanguageMaster _Language = _languageMasterService.GetLanguageById(Language.Id);
                _Language.Deactivate = "Y";
                _Language.DeactivateBy = Language.EnteredBy;
                _Language.DeactivateDate = DateTime.Now;
                _languageMasterService.UpdateLanguage(_Language);

                status = "OK";
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "DeleteLanguage", ex);
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "DeleteLanguage", ex);
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        [HttpGet]
        public IHttpActionResult GetLanguageList()
        {
            try
            {
                var LanguageMasterList = _languageMasterService.GetAllLanguage().ToList().Select(a => new { Id = a.Id, LanguageName = a.LanguageName });
                var ProductMasterList = _ProductMasterService.GetProductMasterList();

                var LeftList = (from languageMasterList in LanguageMasterList

                                join productMasterList in ProductMasterList
                                on languageMasterList.Id equals productMasterList.LanguageId into productMasterList_New
                                from productMasterList in productMasterList_New.DefaultIfEmpty()

                                select new
                                {
                                    Id = languageMasterList.Id,
                                    LanguageName = languageMasterList.LanguageName,
                                    Flag = (productMasterList != null ? productMasterList.LanguageId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                            "0"
                                }).Distinct().ToList();

                return Json(LeftList);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "GetLanguageList", ex);
                return Json(ex.InnerException);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "LanguageMasterController.cs", "GetLanguageList", ex);
                return Json(ex.InnerException);
            }
            return null;
        }

        #endregion
        //Ended By Ankush Kumar
	}
}