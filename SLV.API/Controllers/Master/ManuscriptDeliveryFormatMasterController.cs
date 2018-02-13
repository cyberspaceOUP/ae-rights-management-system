//Added By Suranjana on 13/07/2016

using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Master;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using System;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class ManuscriptDeliveryFormatMasterController : ApiController
    {
        #region Private Property
        private readonly IManuscriptDeliveryFormatService _mobjManuscriptDeliveryFormatService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly ILogger _mobjLoggerService;
        private readonly IRepository<ManuscriptDeliveryFormatMaster> _mobjManuscriptDeliveryFormatRepository;
        private readonly IRepository<AuthorContractOriginal> _mobjAuthorContractRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor methos
        /// </summary>
        /// <param name="mobjManuscriptDeliveryFormatService">accepts ManuscriptDeliveryFormatService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public ManuscriptDeliveryFormatMasterController(ILogger mobjLoggerService,
                                                        IManuscriptDeliveryFormatService mobjManuscriptDeliveryFormatService,
                                                        ILocalizationService mobjLocalizationService,
                                                        IRepository<ManuscriptDeliveryFormatMaster> mobjManuscriptDeliveryFormatRepository,
                                                        IRepository<AuthorContractOriginal> mobjAuthorContractRepository)
        {
            _mobjLoggerService = mobjLoggerService;
            _mobjManuscriptDeliveryFormatService = mobjManuscriptDeliveryFormatService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjManuscriptDeliveryFormatRepository = mobjManuscriptDeliveryFormatRepository;
            _mobjAuthorContractRepository = mobjAuthorContractRepository;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all ManuscriptDeliveryFormat
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetManuscriptDeliveryFormatList()
        {
            var mvarManuscriptDeliveryFormatList = (from M in _mobjManuscriptDeliveryFormatRepository.Table.Where(a => a.Deactivate == "N")
                                                    join authorContract in _mobjAuthorContractRepository.Table.Where(a => a.Deactivate == "N")
                                                    on M.Id equals authorContract.ManuscriptId into authorcontractGroup
                                                    from A in authorcontractGroup.DefaultIfEmpty()

                                                    select new
                                                    {
                                                        Id = M.Id,
                                                        ManuscriptDeliveryFormat = M.ManuscriptDeliveryFormat,
                                                        Flag = A.ManuscriptId != null ? "1" : "0",
                                                        Deactivate = M.Deactivate,
                                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.ManuscriptDeliveryFormat);

            return Json(mvarManuscriptDeliveryFormatList);
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult ManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster mobjManuscriptDeliveryFormat)
        {
            ManuscriptDeliveryFormatMaster _ManuscriptDeliveryFormat = _mobjManuscriptDeliveryFormatService.GetManuscriptDeliveryFormatById(mobjManuscriptDeliveryFormat);
            return Json(_ManuscriptDeliveryFormat);
        }

        /// <summary>
        /// Api method to insert ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="mobjManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster mobjManuscriptDeliveryFormat)
        {
            string message = "";
            try
            {
                message = _mobjManuscriptDeliveryFormatService.DuplicateCheck(mobjManuscriptDeliveryFormat);
                if (message == "Y")
                {
                    if (mobjManuscriptDeliveryFormat.Id == 0)
                    {
                        _mobjManuscriptDeliveryFormatService.InsertManuscriptDeliveryFormat(mobjManuscriptDeliveryFormat);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        ManuscriptDeliveryFormatMaster _mobjManuscriptDeliveryFormat = _mobjManuscriptDeliveryFormatService.GetManuscriptDeliveryFormatById(mobjManuscriptDeliveryFormat);
                        _mobjManuscriptDeliveryFormat.ManuscriptDeliveryFormat = mobjManuscriptDeliveryFormat.ManuscriptDeliveryFormat;
                        _mobjManuscriptDeliveryFormat.ModifiedBy = mobjManuscriptDeliveryFormat.EnteredBy;
                        _mobjManuscriptDeliveryFormat.ModifiedDate = DateTime.Now;
                        _mobjManuscriptDeliveryFormatService.UpdateManuscriptDeliveryFormat(_mobjManuscriptDeliveryFormat);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                }
                else
                {
                    message = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }
            catch (Exception ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }

            return Json(message);
        }

        /// <summary>
        /// Api method to update ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="mobjManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster mobjManuscriptDeliveryFormat)
        {
            string message = string.Empty;
            try
            {
                ManuscriptDeliveryFormatMaster _mobjManuscriptDeliveryFormat = _mobjManuscriptDeliveryFormatService.GetManuscriptDeliveryFormatById(mobjManuscriptDeliveryFormat);
                _mobjManuscriptDeliveryFormat.ManuscriptDeliveryFormat = mobjManuscriptDeliveryFormat.ManuscriptDeliveryFormat;
                _mobjManuscriptDeliveryFormat.ModifiedBy = mobjManuscriptDeliveryFormat.EnteredBy;
                _mobjManuscriptDeliveryFormat.ModifiedDate = DateTime.Now;
                _mobjManuscriptDeliveryFormatService.UpdateManuscriptDeliveryFormat(_mobjManuscriptDeliveryFormat);
            }
            catch (ACSException ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }
            catch (Exception ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }

            return Json(message);
        }

        /// <summary>
        /// Api method to delete ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="mobjManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult ManuscriptDeliveryFormatDelete(ManuscriptDeliveryFormatMaster mobjManuscriptDeliveryFormat)
        {
            string message = string.Empty;
            try
            {
                ManuscriptDeliveryFormatMaster _mobjManuscriptDeliveryFormat = _mobjManuscriptDeliveryFormatService.GetManuscriptDeliveryFormatById(mobjManuscriptDeliveryFormat);
                _mobjManuscriptDeliveryFormat.Deactivate = "Y";
                _mobjManuscriptDeliveryFormat.DeactivateBy = mobjManuscriptDeliveryFormat.EnteredBy;
                _mobjManuscriptDeliveryFormat.DeactivateDate = DateTime.Now;
                _mobjManuscriptDeliveryFormatService.UpdateManuscriptDeliveryFormat(_mobjManuscriptDeliveryFormat);

                message = "OK";
            }
            catch (ACSException ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }
            catch (Exception ex)
            {
                //message = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }

            return Json(message);
        }
        #endregion
    }
}