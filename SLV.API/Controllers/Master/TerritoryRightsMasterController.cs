//Added By Suranjana on 13/07/2016

using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.RightsSelling;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using SLV.Model.Common;
using System;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class TerritoryRightsMasterController : ApiController
    {
        #region Private Property
        private readonly ITerritoryRightsService _mobjTerritoryRightsService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly ILogger _mobjLoggerService;
        private readonly IRepository<TerritoryRightsMaster> _mobjTerritoryRightsRepository;
        private readonly IRepository<ProductLicense> _mobjProductLicenseRepository;
        private readonly IRepository<AuthorContractOriginal> _mobjAuthorContractOriginalRepository;
        private readonly IRepository<OtherContractMaster> _mobjOtherContractRepository;
        private readonly IRepository<RightsSellingMaster> _mobjRightsSellingRepository;
        private readonly IDbContext _dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor methos
        /// </summary>
        /// <param name="mobjTerritoryRightsService">accepts TerritoryRightsService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public TerritoryRightsMasterController(ILogger mobjLoggerService,
                                               ITerritoryRightsService mobjTerritoryRightsService,
                                               ILocalizationService mobjLocalizationService,
                                               IRepository<TerritoryRightsMaster> mobjTerritoryRightsRepository,
                                               IRepository<ProductLicense> mobjProductLicenseRepository,
                                               IRepository<AuthorContractOriginal> mobjAuthorContractOriginalRepository,
                                               IRepository<OtherContractMaster> mobjOtherContractRepository,
                                               IRepository<RightsSellingMaster> mobjRightsSellingRepository,
                                                IDbContext dbContext
            )
        {
            _mobjLoggerService = mobjLoggerService;
            _mobjTerritoryRightsService = mobjTerritoryRightsService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjTerritoryRightsRepository = mobjTerritoryRightsRepository;
            _mobjProductLicenseRepository = mobjProductLicenseRepository;
            _mobjAuthorContractOriginalRepository = mobjAuthorContractOriginalRepository;
            _mobjOtherContractRepository = mobjOtherContractRepository;
            _mobjRightsSellingRepository = mobjRightsSellingRepository;
            _dbContext = dbContext;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all TerritoryRights
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetTerritoryRightsList()
        {
            //var mvarTerritoryRightsList = (from T in _mobjTerritoryRightsRepository.Table.Where(a => a.Deactivate == "N")

            //                               join productLicense in _mobjProductLicenseRepository.Table.Where(a => a.Deactivate == "N")
            //                                  on T.Id equals productLicense.Territoryrightsid into productLicenseGroup
            //                               from P in productLicenseGroup.DefaultIfEmpty()

            //                               join authorContract in _mobjAuthorContractOriginalRepository.Table.Where(a => a.Deactivate == "N")
            //                                  on T.Id equals authorContract.Territoryrightsid into authorcontractGroup
            //                               from A in authorcontractGroup.DefaultIfEmpty()

            //                               join otherContract in _mobjOtherContractRepository.Table.Where(a => a.Deactivate == "N")
            //                                  on T.Id equals otherContract.Territoryrightsid into otherContractGroup
            //                               from O in otherContractGroup.DefaultIfEmpty()

            //                               join rightsSelling in _mobjRightsSellingRepository.Table.Where(a => a.Deactivate == "N")
            //                                  on T.Id equals rightsSelling.Territory_Rights into rightsSellingGroup
            //                               from R in rightsSellingGroup.DefaultIfEmpty()

            //                               select new
            //                               {
            //                                   Id = T.Id,
            //                                   Territoryrights = T.Territoryrights,
            //                                   Flag = P.Territoryrightsid != null || A.Territoryrightsid != null || O.Territoryrightsid != null || R.Territory_Rights != null ? "1" : "0",
            //                                   Deactivate = T.Deactivate,
            //                               }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.Territoryrights);

            try
            {
                var mvarTerritoryRightsList = _dbContext.ExecuteStoredProcedureListNewData<TerritoryRightsModel>("Proc_Get_TerritoryRightsList").ToList();

                var mvarTerritoryRightsList_finel = (from T in mvarTerritoryRightsList
                                            
                                               select new
                                               {
                                                   Id = T.Id,
                                                   Territoryrights = T.Territoryrights,
                                                   Flag = T.Territoryrightsid1 != null || T.Territoryrightsid2 != null || T.Territoryrightsid3 != null || T.Territoryrightsid4 != null ? "1" : "0",
                                                   Deactivate = T.Deactivate,
                                               }).ToList();

                return Json(mvarTerritoryRightsList_finel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json("OK");
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjTerritoryRights">accepts TerritoryRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult TerritoryRights(TerritoryRightsMaster mobjTerritoryRights)
        {
            TerritoryRightsMaster _TerritoryRights = _mobjTerritoryRightsService.GetTerritoryRightsById(mobjTerritoryRights);
            return Json(_TerritoryRights);
        }

        /// <summary>
        /// Api method to insert TerritoryRightsMaster
        /// </summary>
        /// <param name="mobjTerritoryRights">accepts TerritoryRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertTerritoryRights(TerritoryRightsMaster mobjTerritoryRights)
        {
            string message = "";
            try
            {
                message = _mobjTerritoryRightsService.DuplicateCheck(mobjTerritoryRights);
                if (message == "Y")
                {
                    if (mobjTerritoryRights.Id == 0)
                    {
                        _mobjTerritoryRightsService.InsertTerritoryRights(mobjTerritoryRights);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        TerritoryRightsMaster _mobjTerritoryRights = _mobjTerritoryRightsService.GetTerritoryRightsById(mobjTerritoryRights);
                        _mobjTerritoryRights.Territoryrights = mobjTerritoryRights.Territoryrights;
                        _mobjTerritoryRights.ModifiedBy = mobjTerritoryRights.EnteredBy;
                        _mobjTerritoryRights.ModifiedDate = DateTime.Now;
                        _mobjTerritoryRightsService.UpdateTerritoryRights(_mobjTerritoryRights);
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
                //status = ex.InnerException.Message;
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

        /// <summary>
        /// Api method to update TerritoryRightsMaster
        /// </summary>
        /// <param name="mobjTerritoryRights">accepts TerritoryRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateTerritoryRights(TerritoryRightsMaster mobjTerritoryRights)
        {
            string message = string.Empty;
            try
            {
                TerritoryRightsMaster _mobjTerritoryRights = _mobjTerritoryRightsService.GetTerritoryRightsById(mobjTerritoryRights);
                _mobjTerritoryRights.Territoryrights = mobjTerritoryRights.Territoryrights;
                _mobjTerritoryRights.ModifiedBy = mobjTerritoryRights.EnteredBy;
                _mobjTerritoryRights.ModifiedDate = DateTime.Now;
                _mobjTerritoryRightsService.UpdateTerritoryRights(_mobjTerritoryRights);
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
        /// Api method to delete TerritoryRightsMaster
        /// </summary>
        /// <param name="mobjTerritoryRights">accepts TerritoryRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult TerritoryRightsDelete(TerritoryRightsMaster mobjTerritoryRights)
        {
            string message = string.Empty;
            try
            {
                TerritoryRightsMaster _mobjTerritoryRights = _mobjTerritoryRightsService.GetTerritoryRightsById(mobjTerritoryRights);
                _mobjTerritoryRights.Deactivate = "Y";
                _mobjTerritoryRights.DeactivateBy = mobjTerritoryRights.EnteredBy;
                _mobjTerritoryRights.DeactivateDate = DateTime.Now;
                _mobjTerritoryRightsService.UpdateTerritoryRights(_mobjTerritoryRights);

                message = "OK";
            }
            catch (ACSException ex)
            {
                //status = ex.InnerException.Message;
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