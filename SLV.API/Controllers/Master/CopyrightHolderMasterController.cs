using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class CopyrightHolderMasterController : ApiController
    {
        #region Private Property
        private readonly ICopyrightHolderService _mobjCopyrightHolderService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly IApplicationSetUpService _mobjApplicationSetUpService;
        private readonly IRepository<ApplicationSetUp> _mobjApplicationSetUpRepository;
        private readonly IRepository<CopyRightHolderMaster> _mobjCopyRightHolderMasterRepository;
        private readonly IRepository<GeographicalMaster> _mobjGeographicalMasterRepository;
        private readonly ILogger _mobjLoggerService;
        private readonly IDbContext _dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mobjCopyrightHolderService">accepts CopyrightHolderService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public CopyrightHolderMasterController(ICopyrightHolderService mobjCopyrightHolderService,
                                      ILocalizationService mobjLocalizationService,
                                      ILogger mobjLoggerService,
                                      IRepository<ApplicationSetUp> mobjApplicationSetUpRepository,
                                      IApplicationSetUpService mobjApplicationSetUpService,
                                      IRepository<GeographicalMaster> mobjGeographicalMasterRepository,
                                      IRepository<CopyRightHolderMaster> mobjCopyRightHolderMasterRepository,
                                      IDbContext dbContext)
        {
            _mobjCopyrightHolderService = mobjCopyrightHolderService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjLoggerService = mobjLoggerService;
            _dbContext = dbContext;
            _mobjApplicationSetUpRepository = mobjApplicationSetUpRepository;
            _mobjCopyRightHolderMasterRepository = mobjCopyRightHolderMasterRepository;
            _mobjGeographicalMasterRepository = mobjGeographicalMasterRepository;
            _mobjApplicationSetUpService = mobjApplicationSetUpService;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all CopyrightHolder By Division and Subdivision Name
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetCopyrightHolderList()
        {
            var mvarCopyrightHolderList = (from CR in _mobjCopyRightHolderMasterRepository.Table.Where(a => a.Deactivate == "N")

                                    join Country in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on CR.CountryId equals Country.Id into countryGroup
                                    from c in countryGroup.DefaultIfEmpty()

                                    join State in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on CR.Stateid equals State.Id into stateGroup
                                    from s in stateGroup.DefaultIfEmpty()

                                    join City in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on CR.Cityid equals City.Id into cityGroup
                                    from t in cityGroup.DefaultIfEmpty()

                                    select new
                                    {
                                        Id = CR.Id,
                                        CopyRightHolderName = CR.CopyRightHolderName,
                                        ContactPerson = CR.ContactPerson,
                                        Address = CR.Address,
                                        Country = c.geogName,
                                        State = s.geogName,
                                        City = t.geogName,
                                        Pincode = CR.Pincode,
                                        Mobile = CR.Mobile,
                                        Email = CR.Email,
                                        URL = CR.URL,
                                        BankName = CR.BankName,
                                        AccountNo = CR.AccountNo,
                                        BankAddress = CR.BankAddress,
                                        IFSCCode = CR.IFSCCode,
                                        PANNo = CR.PANNo,
                                        VendorCode = CR.VendorCOde,
                                        Deactivate = CR.Deactivate,
                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.CopyRightHolderName);

            return Json(mvarCopyrightHolderList);
        }

        /// <summary>
        /// Methods to get all CopyrightHolder
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetAllCopyrightHolderList()
        {
            return Json(_mobjCopyrightHolderService.GetAllCopyrightHolder().ToList());
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult CopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder)
        {
            CopyRightHolderMaster _CopyrightHolder = _mobjCopyrightHolderService.GetCopyrightHolderById(mobjCopyrightHolder);
            return Json(_CopyrightHolder);
        }

        /// <summary>
        /// Api method to insert CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder)
        {
            string message = "";
            try
            {
                message = _mobjCopyrightHolderService.DuplicateCheck(mobjCopyrightHolder);
                if (message == "Y")
                {
                    if (mobjCopyrightHolder.Id == 0)
                    {
                        IList<ApplicationSetUp> _mobjApplicationSetUpList = _mobjApplicationSetUpRepository.Table.Where(x => x.key == "CopyrightHolderCode" && x.Deactivate == "N").ToList();
                        var CopyrightHolderSuggesation = _mobjApplicationSetUpList.Select(P => new
                        {
                            CopyrightHolderCodeValue = P.keyValue,
                            Id = P.Id
                        });

                        mobjCopyrightHolder.CopyRightHolderCode = "CR" + mobjCopyrightHolder.CopyRightHolderName.Substring(0, 1).ToString() + CopyrightHolderSuggesation.FirstOrDefault().CopyrightHolderCodeValue;
                        mobjCopyrightHolder.CopyRightHolderCode = mobjCopyrightHolder.CopyRightHolderCode.ToString().ToUpper();
                        _mobjCopyrightHolderService.InsertCopyrightHolder(mobjCopyrightHolder);

                        ApplicationSetUp mobjApplicationSetUp = new ApplicationSetUp();
                        mobjApplicationSetUp.Id = CopyrightHolderSuggesation.FirstOrDefault().Id;
                        ApplicationSetUp _ApplicationSetUpUpdate = _mobjApplicationSetUpService.GetApplicationSetUpById(mobjApplicationSetUp);
                        _ApplicationSetUpUpdate.Id = CopyrightHolderSuggesation.FirstOrDefault().Id;
                        int Value = Int32.Parse(CopyrightHolderSuggesation.FirstOrDefault().CopyrightHolderCodeValue) + 1;
                        _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');
                        _ApplicationSetUpUpdate.ModifiedBy = mobjCopyrightHolder.EnteredBy;
                        _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;
                        _mobjApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);

                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        CopyRightHolderMaster _CopyrightHolder = _mobjCopyrightHolderService.GetCopyrightHolderById(mobjCopyrightHolder);
                        _CopyrightHolder.CopyRightHolderName = mobjCopyrightHolder.CopyRightHolderName;
                        _CopyrightHolder.ContactPerson = mobjCopyrightHolder.ContactPerson;
                        _CopyrightHolder.Address = mobjCopyrightHolder.Address;
                        _CopyrightHolder.CountryId = mobjCopyrightHolder.CountryId;
                        _CopyrightHolder.Stateid = mobjCopyrightHolder.Stateid;
                        _CopyrightHolder.Cityid = mobjCopyrightHolder.Cityid;
                        _CopyrightHolder.Pincode = mobjCopyrightHolder.Pincode;
                        _CopyrightHolder.Mobile = mobjCopyrightHolder.Mobile;
                        _CopyrightHolder.Email = mobjCopyrightHolder.Email;
                        _CopyrightHolder.URL = mobjCopyrightHolder.URL;
                        _CopyrightHolder.BankName = mobjCopyrightHolder.BankName;
                        _CopyrightHolder.AccountNo = mobjCopyrightHolder.AccountNo;
                        _CopyrightHolder.BankAddress = mobjCopyrightHolder.BankAddress;
                        _CopyrightHolder.IFSCCode = mobjCopyrightHolder.IFSCCode;
                        _CopyrightHolder.PANNo = mobjCopyrightHolder.PANNo;
                        _CopyrightHolder.VendorCOde = mobjCopyrightHolder.VendorCOde;
                        _CopyrightHolder.ModifiedBy = mobjCopyrightHolder.EnteredBy;
                        _CopyrightHolder.ModifiedDate = DateTime.Now;
                        _mobjCopyrightHolderService.UpdateCopyrightHolder(_CopyrightHolder);
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
        /// Api method to delete CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult CopyrightHolderDelete(CopyRightHolderMaster mobjCopyrightHolder)
        {
            string message = string.Empty;
            try
            {
                CopyRightHolderMaster _CopyrightHolder = _mobjCopyrightHolderService.GetCopyrightHolderById(mobjCopyrightHolder);
                _CopyrightHolder.Deactivate = "Y";
                _CopyrightHolder.DeactivateBy = mobjCopyrightHolder.EnteredBy;
                _CopyrightHolder.DeactivateDate = DateTime.Now;
                _mobjCopyrightHolderService.UpdateCopyrightHolder(_CopyrightHolder);

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