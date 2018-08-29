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

using ACS.Services.RightsSelling;
using ACS.Services.PermissionsOutbound;

namespace SLV.API.Controllers.Master
{
    public class LicenseeMasterController : ApiController
    {
        #region Private Property
        private readonly ILicenseeService _mobjLicenseeService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly IApplicationSetUpService _mobjApplicationSetUpService;
        private readonly IRepository<ApplicationSetUp> _mobjApplicationSetUpRepository;
        private readonly IRepository<GeographicalMaster> _mobjGeographicalMasterRepository;
        private readonly IRepository<LicenseeMaster> _mobjLicenseeMasterRepository;
        private readonly ILogger _mobjLoggerService;
        private readonly IDbContext _dbContext;

        private readonly IRightsSelling _mobjRightsSelling;
        private readonly IPermissionsOutboundService _mobjPermissionsOutboundService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mobjLicenseeService">accepts LicenseeService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public LicenseeMasterController(ILicenseeService mobjLicenseeService,
                                      ILocalizationService mobjLocalizationService,
                                      ILogger mobjLoggerService,
                                      IRepository<ApplicationSetUp> mobjApplicationSetUpRepository,
                                      IApplicationSetUpService mobjApplicationSetUpService,
                                      IRepository<LicenseeMaster> mobjLicenseeMasterRepository,
                                      IRepository<GeographicalMaster> mobjGeographicalMasterRepository,
                                      IDbContext dbContext,

                                        IRightsSelling mobjRightsSelling,
                                        IPermissionsOutboundService mobjPermissionsOutboundService)
        {
            _mobjLicenseeService = mobjLicenseeService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjLoggerService = mobjLoggerService;
            _dbContext = dbContext;
            _mobjApplicationSetUpRepository = mobjApplicationSetUpRepository;
            _mobjLicenseeMasterRepository = mobjLicenseeMasterRepository;
            _mobjGeographicalMasterRepository = mobjGeographicalMasterRepository;
            _mobjApplicationSetUpService = mobjApplicationSetUpService;

            _mobjRightsSelling = mobjRightsSelling;
            _mobjPermissionsOutboundService = mobjPermissionsOutboundService;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all Licensee By Division and Subdivision Name
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetLicenseeList()
        {
            var rightsList = _mobjRightsSelling.GetAllRightsSellingMasterList().Select(r => r.LicenseeID).Distinct().ToList();
            var poList = _mobjPermissionsOutboundService.getAllPermissionsOutboundMasterList().Select(r => r.LicenseeID).Distinct().ToList();

            var mvarLicenseeList = (from L in _mobjLicenseeMasterRepository.Table.Where(a => a.Deactivate == "N")

                                    join Country in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.CountryId equals Country.Id into countryGroup
                                    from c in countryGroup.DefaultIfEmpty()

                                    join State in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.Stateid equals State.Id into stateGroup
                                    from s in stateGroup.DefaultIfEmpty()

                                    join City in _mobjGeographicalMasterRepository.Table.Where(a => a.Deactivate == "N")
                                       on L.Cityid equals City.Id into cityGroup
                                    from t in cityGroup.DefaultIfEmpty()
                                    
                                    select new
                                    {
                                        Id = L.Id,
                                        OrganizationName = L.OrganizationName,
                                        ContactPerson = L.ContactPerson,
                                        Address = L.Address,
                                        Country = c.geogName,
                                        State = s.geogName,
                                        City = t.geogName,
                                        Pincode = L.Pincode,
                                        Mobile = L.Mobile,
                                        Email = L.Email,
                                        URL = L.URL,
                                        Deactivate = L.Deactivate,
                                        Licenseecode = L.Licenseecode,

                                        flag = rightsList.Where(r => r == L.Id).Select(r => r).FirstOrDefault() != 0 ? 1 : (poList.Where(p => p == L.Id).Select(p => p).FirstOrDefault() != 0 ? 1 : 0)

                                    }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.OrganizationName);

            return Json(mvarLicenseeList);
        }

        /// <summary>
        /// Methods to get all Licensee
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetAllLicenseeList()
        {
            return Json(_mobjLicenseeService.GetAllLicensee().ToList());
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult Licensee(LicenseeMaster mobjLicensee)
        {
            LicenseeMaster _Licensee = _mobjLicenseeService.GetLicenseeById(mobjLicensee);
            return Json(_Licensee);
        }

        /// <summary>
        /// Api method to insert LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertLicensee(LicenseeMaster mobjLicensee)
        {
            string message = "";
            try
            {
                message = _mobjLicenseeService.DuplicateCheck(mobjLicensee);
                if (message == "Y")
                {
                    if (mobjLicensee.Id == 0)
                    {
                        IList<ApplicationSetUp> _mobjApplicationSetUpList = _mobjApplicationSetUpRepository.Table.Where(x => x.key == "LicenseeCode" && x.Deactivate == "N").ToList();
                        var LicenseeSuggesation = _mobjApplicationSetUpList.Select(P => new
                        {
                            LicenseeCodeValue = P.keyValue,
                            Id = P.Id
                        });

                        mobjLicensee.Licenseecode = "LC" + mobjLicensee.OrganizationName.Substring(0, 1).ToString() + LicenseeSuggesation.FirstOrDefault().LicenseeCodeValue;
                        mobjLicensee.Licenseecode = mobjLicensee.Licenseecode.ToString().ToUpper();
                        _mobjLicenseeService.InsertLicensee(mobjLicensee);

                        ApplicationSetUp mobjApplicationSetUp = new ApplicationSetUp();
                        mobjApplicationSetUp.Id = LicenseeSuggesation.FirstOrDefault().Id;
                        ApplicationSetUp _ApplicationSetUpUpdate = _mobjApplicationSetUpService.GetApplicationSetUpById(mobjApplicationSetUp);
                        _ApplicationSetUpUpdate.Id = LicenseeSuggesation.FirstOrDefault().Id;
                        int Value = Int32.Parse(LicenseeSuggesation.FirstOrDefault().LicenseeCodeValue) + 1;
                        _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');
                        _ApplicationSetUpUpdate.ModifiedBy = mobjLicensee.EnteredBy;
                        _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;
                        _mobjApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);

                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        LicenseeMaster _Licensee = _mobjLicenseeService.GetLicenseeById(mobjLicensee);
                        _Licensee.OrganizationName = mobjLicensee.OrganizationName;
                        _Licensee.ContactPerson = mobjLicensee.ContactPerson;
                        _Licensee.Address = mobjLicensee.Address;
                        _Licensee.CountryId = mobjLicensee.CountryId;
                        _Licensee.Stateid = mobjLicensee.Stateid;
                        _Licensee.Cityid = mobjLicensee.Cityid;
                        _Licensee.Pincode = mobjLicensee.Pincode;
                        _Licensee.Mobile = mobjLicensee.Mobile;
                        _Licensee.Email = mobjLicensee.Email;
                        _Licensee.URL = mobjLicensee.URL;
                        _Licensee.ModifiedBy = mobjLicensee.EnteredBy;
                        _Licensee.ModifiedDate = DateTime.Now;
                        _mobjLicenseeService.UpdateLicensee(_Licensee);
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
        /// Api method to delete LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult LicenseeDelete(LicenseeMaster mobjLicensee)
        {
            string message = string.Empty;
            try
            {
                LicenseeMaster _Licensee = _mobjLicenseeService.GetLicenseeById(mobjLicensee);
                _Licensee.Deactivate = "Y";
                _Licensee.DeactivateBy = mobjLicensee.EnteredBy;
                _Licensee.DeactivateDate = DateTime.Now;
                _mobjLicenseeService.UpdateLicensee(_Licensee);

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