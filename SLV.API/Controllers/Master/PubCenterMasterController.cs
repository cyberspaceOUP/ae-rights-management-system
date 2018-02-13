using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using ACS.Core.Domain.Product;
using SLV.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class PubCenterMasterController : ApiController
    {
        #region Private Property
        private readonly IPubCenterService _mobjPubCenterService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly IApplicationSetUpService _mobjApplicationSetUpService;
        private readonly IRepository<ApplicationSetUp> _mobjApplicationSetUpRepository;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductMasterRepository;
        private readonly IRepository<ProprietorMaster> _mobjProprietorMasterRepository;
        private readonly IRepository<PubCenterMaster> _mobjPubCenterMasterRepository;
        private readonly IRepository<PublishingCompanyMaster> _mobjPublishingCompanyMasterRepository;
        private readonly ILogger _mobjLoggerService;
        private readonly IDbContext _dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mobjPubCenterService">accepts PubCenterService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public PubCenterMasterController(IPubCenterService mobjPubCenterService,
                                      ILocalizationService mobjLocalizationService, 
                                      ILogger mobjLoggerService,
                                      IRepository<ApplicationSetUp> mobjApplicationSetUpRepository,
                                      IApplicationSetUpService mobjApplicationSetUpService,
                                      IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductMasterRepository,
                                      IRepository<ProprietorMaster> mobjProprietorMasterRepository,
                                      IRepository<PubCenterMaster> mobjPubCenterMasterRepository,
                                      IRepository<PublishingCompanyMaster> mobjPublishingCompanyMasterRepository,
                                      IDbContext dbContext)
        {
            _mobjPubCenterService = mobjPubCenterService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjLoggerService = mobjLoggerService;
            _dbContext = dbContext;
            _mobjApplicationSetUpRepository = mobjApplicationSetUpRepository;
            _mobjProductMasterRepository = mobjProductMasterRepository;
            _mobjProprietorMasterRepository = mobjProprietorMasterRepository;
            _mobjPubCenterMasterRepository = mobjPubCenterMasterRepository;
            _mobjPublishingCompanyMasterRepository = mobjPublishingCompanyMasterRepository;
            _mobjApplicationSetUpService = mobjApplicationSetUpService;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all PubCenter By Division and Subdivision Name
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetPubCenterList() 
        {
            var mvarPubCenterList = _dbContext.ExecuteStoredProcedureListNewData<PubCenterWithPublishingCompanyModel>("Proc_Get_PubCenterWithPublishingCompany").ToList();

            return Json(mvarPubCenterList);
        }

        /// <summary>
        /// Methods to get all PubCenter
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetAllPubCenterList()
        {
            return Json(_mobjPubCenterService.GetAllPubCenter().ToList());
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult PubCenter(PubCenterMaster mobjPubCenter)
        {
            PubCenterMaster _PubCenter = _mobjPubCenterService.GetPubCenterById(mobjPubCenter);
            return Json(_PubCenter);
        }

        /// <summary>
        /// Api method to insert PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertPubCenter(PubCenterMaster mobjPubCenter)
        {
            string message = "";
            try
            {
                message = _mobjPubCenterService.DuplicateCheck(mobjPubCenter);
                if (message == "Y")
                {
                    if (mobjPubCenter.Id == 0)
                    {
                        IList<ApplicationSetUp> _mobjApplicationSetUpList = _mobjApplicationSetUpRepository.Table.Where(x => x.key == "PubCenterCode" && x.Deactivate == "N").ToList();
                        var PubCenterSuggesation = _mobjApplicationSetUpList.Select(P => new
                        {
                            PubCenterCodeValue = P.keyValue,
                            Id = P.Id
                        });

                        mobjPubCenter.PubcenterCode = "PCC" + PubCenterSuggesation.FirstOrDefault().PubCenterCodeValue;
                        mobjPubCenter.PubcenterCode = mobjPubCenter.PubcenterCode.ToString().ToUpper();
                        _mobjPubCenterService.InsertPubCenter(mobjPubCenter);
                   
                        ApplicationSetUp mobjApplicationSetUp = new ApplicationSetUp();
                        mobjApplicationSetUp.Id = PubCenterSuggesation.FirstOrDefault().Id;
                        ApplicationSetUp _ApplicationSetUpUpdate = _mobjApplicationSetUpService.GetApplicationSetUpById(mobjApplicationSetUp);
                        _ApplicationSetUpUpdate.Id = PubCenterSuggesation.FirstOrDefault().Id;
                        int Value = Int32.Parse(PubCenterSuggesation.FirstOrDefault().PubCenterCodeValue) + 1;
                        _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');
                        _ApplicationSetUpUpdate.ModifiedBy = mobjPubCenter.EnteredBy;
                        _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;
                        _mobjApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);

                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");


                    }
                    else
                    {
                        PubCenterMaster _PubCenter = _mobjPubCenterService.GetPubCenterById(mobjPubCenter);
                        _PubCenter.PublishingCompanyid = mobjPubCenter.PublishingCompanyid;
                        _PubCenter.CenterName = mobjPubCenter.CenterName;
                        _PubCenter.ContactPerson = mobjPubCenter.ContactPerson;
                        _PubCenter.PublishingCompanyDivision = mobjPubCenter.PublishingCompanyDivision;
                        _PubCenter.Address = mobjPubCenter.Address;
                        _PubCenter.CountryId = mobjPubCenter.CountryId;
                        _PubCenter.Stateid = mobjPubCenter.Stateid;
                        _PubCenter.Cityid = mobjPubCenter.Cityid;
                        _PubCenter.Pincode = mobjPubCenter.Pincode;
                        _PubCenter.Phone = mobjPubCenter.Phone;
                        _PubCenter.Mobile = mobjPubCenter.Mobile;
                        _PubCenter.Email = mobjPubCenter.Email;
                        _PubCenter.Fax = mobjPubCenter.Fax;
                        _PubCenter.ModifiedBy = mobjPubCenter.EnteredBy;
                        _PubCenter.ModifiedDate = DateTime.Now;
                        _mobjPubCenterService.UpdatePubCenter(_PubCenter);
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
        /// Api method to delete PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult PubCenterDelete(PubCenterMaster mobjPubCenter)
        {
            string message = string.Empty;
            try
            {
                PubCenterMaster _PubCenter = _mobjPubCenterService.GetPubCenterById(mobjPubCenter);
                _PubCenter.Deactivate = "Y";
                _PubCenter.DeactivateBy = mobjPubCenter.EnteredBy;
                _PubCenter.DeactivateDate = DateTime.Now;
                _mobjPubCenterService.UpdatePubCenter(_PubCenter);

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