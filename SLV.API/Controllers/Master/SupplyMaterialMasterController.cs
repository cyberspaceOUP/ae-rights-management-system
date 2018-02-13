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
    public class SupplyMaterialMasterController : ApiController
    {
        #region Private Property
        private readonly ISupplyMaterialService _mobjSupplyMaterialService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly ILogger _mobjLoggerService;
        private readonly IRepository<SupplyMaterialMaster> _mobjSupplyMaterialRepository;
        private readonly IRepository<AuthorContractmaterialdetails> _mobjAuthorContractRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor methos
        /// </summary>
        /// <param name="mobjSupplyMaterialService">accepts SupplyMaterialService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public SupplyMaterialMasterController(ILogger mobjLoggerService,
                                              ISupplyMaterialService mobjSupplyMaterialService,
                                              ILocalizationService mobjLocalizationService,
                                              IRepository<SupplyMaterialMaster> mobjSupplyMaterialRepository,
                                              IRepository<AuthorContractmaterialdetails> mobjAuthorContractRepository)
        {
            _mobjLoggerService = mobjLoggerService;
            _mobjSupplyMaterialService = mobjSupplyMaterialService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjSupplyMaterialRepository = mobjSupplyMaterialRepository;
            _mobjAuthorContractRepository = mobjAuthorContractRepository;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all SupplyMaterial
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetSupplyMaterialList()
        {
            var mvarSupplyMaterialList = (from M in _mobjSupplyMaterialRepository.Table.Where(a => a.Deactivate == "N")
                                                    join authorContract in _mobjAuthorContractRepository.Table.Where(a => a.Deactivate == "N")
                                                    on M.Id equals authorContract.MaterialId into authorcontractGroup
                                                    from A in authorcontractGroup.DefaultIfEmpty()

                                                    select new
                                                    {
                                                        Id = M.Id,
                                                        SupplyMaterial = M.SupplyMaterial,
                                                        Flag = A.MaterialId != null ? "1" : "0",
                                                        Deactivate = M.Deactivate,
                                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.SupplyMaterial);

            return Json(mvarSupplyMaterialList);
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjSupplyMaterial">accepts SupplyMaterialMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult SupplyMaterial(SupplyMaterialMaster mobjSupplyMaterial)
        {
            SupplyMaterialMaster _SupplyMaterial = _mobjSupplyMaterialService.GetSupplyMaterialById(mobjSupplyMaterial);
            return Json(_SupplyMaterial);
        }

        /// <summary>
        /// Api method to insert SupplyMaterialMaster
        /// </summary>
        /// <param name="mobjSupplyMaterial">accepts SupplyMaterialMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertSupplyMaterial(SupplyMaterialMaster mobjSupplyMaterial)
        {
            string message = "";
            try
            {
                message = _mobjSupplyMaterialService.DuplicateCheck(mobjSupplyMaterial);
                if (message == "Y")
                {
                    if (mobjSupplyMaterial.Id == 0)
                    {
                        _mobjSupplyMaterialService.InsertSupplyMaterial(mobjSupplyMaterial);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        SupplyMaterialMaster _mobjSupplyMaterial = _mobjSupplyMaterialService.GetSupplyMaterialById(mobjSupplyMaterial);
                        _mobjSupplyMaterial.SupplyMaterial = mobjSupplyMaterial.SupplyMaterial;
                        _mobjSupplyMaterial.ModifiedBy = mobjSupplyMaterial.EnteredBy;
                        _mobjSupplyMaterial.ModifiedDate = DateTime.Now;
                        _mobjSupplyMaterialService.UpdateSupplyMaterial(_mobjSupplyMaterial);
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
        /// Api method to update SupplyMaterialMaster
        /// </summary>
        /// <param name="mobjSupplyMaterial">accepts SupplyMaterialMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateSupplyMaterial(SupplyMaterialMaster mobjSupplyMaterial)
        {
            string message = string.Empty;
            try
            {
                SupplyMaterialMaster _mobjSupplyMaterial = _mobjSupplyMaterialService.GetSupplyMaterialById(mobjSupplyMaterial);
                _mobjSupplyMaterial.SupplyMaterial = mobjSupplyMaterial.SupplyMaterial;
                _mobjSupplyMaterial.ModifiedBy = mobjSupplyMaterial.EnteredBy;
                _mobjSupplyMaterial.ModifiedDate = DateTime.Now;
                _mobjSupplyMaterialService.UpdateSupplyMaterial(_mobjSupplyMaterial);
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
        /// Api method to delete SupplyMaterialMaster
        /// </summary>
        /// <param name="mobjSupplyMaterial">accepts SupplyMaterialMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult SupplyMaterialDelete(SupplyMaterialMaster mobjSupplyMaterial)
        {
            string message = string.Empty;
            try
            {
                SupplyMaterialMaster _mobjSupplyMaterial = _mobjSupplyMaterialService.GetSupplyMaterialById(mobjSupplyMaterial);
                _mobjSupplyMaterial.Deactivate = "Y";
                _mobjSupplyMaterial.DeactivateBy = mobjSupplyMaterial.EnteredBy;
                _mobjSupplyMaterial.DeactivateDate = DateTime.Now;
                _mobjSupplyMaterialService.UpdateSupplyMaterial(_mobjSupplyMaterial);

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