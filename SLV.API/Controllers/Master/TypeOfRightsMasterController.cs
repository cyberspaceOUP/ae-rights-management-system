//Added By Suranjana on 11/07/2016

using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.RightsSelling;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using System;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class TypeOfRightsMasterController : ApiController
    {
        #region Private Property
        private readonly ITypeOfRightsService _mobjTypeOfRightsService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly ILogger _mobjLoggerService;
        private readonly IRepository<TypeOfRightsMaster> _objTypeOfRightsRepository;
        private readonly IRepository<RightsSellingMaster> _objRightsSellingRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor methos
        /// </summary>
        /// <param name="mobjTypeOfRightsService">accepts TypeOfRightsService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public TypeOfRightsMasterController(ILogger mobjLoggerService,
                                           ITypeOfRightsService mobjTypeOfRightsService,
                                           ILocalizationService mobjLocalizationService,
                                           IRepository<TypeOfRightsMaster> objTypeOfRightsRepository,
                                           IRepository<RightsSellingMaster> objRightsSellingRepository)
        {
            _mobjLoggerService = mobjLoggerService;
            _mobjTypeOfRightsService = mobjTypeOfRightsService;
            _mobjLocalizationService = mobjLocalizationService;
            _objTypeOfRightsRepository = objTypeOfRightsRepository;
            _objRightsSellingRepository = objRightsSellingRepository;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all TypeOfRights
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetTypeOfRightsList()
        {
            /*var mvarTypeOfRightsList = (from T in _objTypeOfRightsRepository.Table.Where(a => a.Deactivate == "N")
                                                    join selling in _objRightsSellingRepository.Table.Where(a => a.Deactivate == "N")
                                                    on T.Id equals selling.TypeofRights into sellingGroup
                                                    from S in sellingGroup.DefaultIfEmpty()

                                                    select new
                                                    {
                                                        Id = T.Id,
                                                        TypeOfRights = T.TypeOfRights,
                                                        Flag = S.TypeofRights != null ? "1" : "0",
                                                        Deactivate = T.Deactivate,
                                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.TypeOfRights);

            return Json(mvarTypeOfRightsList);*/

            var mvarTypeOfRightsList = (from T in _objTypeOfRightsRepository.Table.Where(a => a.Deactivate == "N")
                                       select new
                                                    {
                                                        Id = T.Id,
                                                        TypeOfRights = T.TypeOfRights,
                                                        Flag = "1",
                                                        Deactivate = T.Deactivate,
                                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.TypeOfRights);

            return Json(mvarTypeOfRightsList);
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjTypeOfRights">accepts TypeOfRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult TypeOfRights(TypeOfRightsMaster mobjTypeOfRights)
        {
            TypeOfRightsMaster _TypeOfRights = _mobjTypeOfRightsService.GetTypeOfRightsById(mobjTypeOfRights);
            return Json(_TypeOfRights);
        }

        /// <summary>
        /// Api method to insert TypeOfRightsMaster
        /// </summary>
        /// <param name="mobjTypeOfRights">accepts TypeOfRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertTypeOfRights(TypeOfRightsMaster mobjTypeOfRights)
        {
            string message = "";
            try
            {
                message = _mobjTypeOfRightsService.DuplicateCheck(mobjTypeOfRights);
                if (message == "Y")
                {
                    if (mobjTypeOfRights.Id == 0)
                    {
                        _mobjTypeOfRightsService.InsertTypeOfRights(mobjTypeOfRights);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        TypeOfRightsMaster _TypeOfRights = _mobjTypeOfRightsService.GetTypeOfRightsById(mobjTypeOfRights);
                        _TypeOfRights.TypeOfRights = mobjTypeOfRights.TypeOfRights;
                        _TypeOfRights.ModifiedBy = mobjTypeOfRights.EnteredBy;
                        _TypeOfRights.ModifiedDate = DateTime.Now;
                        _mobjTypeOfRightsService.UpdateTypeOfRights(_TypeOfRights);
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
        /// Api method to update TypeOfRightsMaster
        /// </summary>
        /// <param name="mobjTypeOfRights">accepts TypeOfRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateTypeOfRights(TypeOfRightsMaster mobjTypeOfRights)
        {
            string message = string.Empty;
            try
            {
                TypeOfRightsMaster _TypeOfRights = _mobjTypeOfRightsService.GetTypeOfRightsById(mobjTypeOfRights);
                _TypeOfRights.TypeOfRights = mobjTypeOfRights.TypeOfRights;
                _TypeOfRights.ModifiedBy = mobjTypeOfRights.EnteredBy;
                _TypeOfRights.ModifiedDate = DateTime.Now;
                _mobjTypeOfRightsService.UpdateTypeOfRights(_TypeOfRights);
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
        /// Api method to delete TypeOfRightsMaster
        /// </summary>
        /// <param name="mobjTypeOfRights">accepts TypeOfRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult TypeOfRightsDelete(TypeOfRightsMaster mobjTypeOfRights)
        {
            string message = string.Empty;
            try
            {
                TypeOfRightsMaster _TypeOfRights = _mobjTypeOfRightsService.GetTypeOfRightsById(mobjTypeOfRights);
                _TypeOfRights.Deactivate = "Y";
                _TypeOfRights.DeactivateBy = mobjTypeOfRights.EnteredBy;
                _TypeOfRights.DeactivateDate = DateTime.Now;
                _mobjTypeOfRightsService.UpdateTypeOfRights(_TypeOfRights);

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
                //status = ex.InnerException.Message;
                _mobjLoggerService.Error(ex.InnerException.Message, ex);
                message = _mobjLocalizationService.GetResource("Common.API.Exception.Message");
            }

            return Json(message);
        }
        #endregion
    }
}