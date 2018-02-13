using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
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
    public class PublishingCompanyMasterController : ApiController
    {
        #region Private Properties
        private readonly IPublishingCompanyService _publishingCompanyService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _loggerService;
        private readonly IApplicationSetUpService _mobjApplicationSetUpService;
        private readonly IRepository<ApplicationSetUp> _mobjApplicationSetUpRepository;
        private readonly IRepository<GeographicalMaster> _mobjGeographicalRepository;
        private readonly IRepository<PublishingCompanyMaster> _mobjPublishingCompanyRepository;
        private readonly IRepository<ProductLicense> _mobjProductLicenseRepository;
        private readonly IRepository<PubCenterMaster> _mobjPubCenterRepository;
        private readonly IRepository<ProprietorMaster> _mobjProprietorRepository;
        private readonly IRepository<ImprintMaster> _mobjImprintRepository;
        private readonly IDbContext _dbContext;
        #endregion

        #region Constructor
        public PublishingCompanyMasterController(IPublishingCompanyService publishingCompanyService,
                                                ILocalizationService localizationService,
                                                ILogger loggerService,
                                                IRepository<GeographicalMaster> geoMaster,
                                                IApplicationSetUpService mobjApplicationSetUpService,
                                                IRepository<ApplicationSetUp> mobjApplicationSetUpRepository,
                                                IRepository<PublishingCompanyMaster> mobjPublishingCompanyRepository,
                                                IRepository<ProductLicense> mobjProductLicenseRepository,
                                                IRepository<PubCenterMaster> mobjPubCenterRepository,
                                                IRepository<ProprietorMaster> mobjProprietorRepository,
                                                IRepository<ImprintMaster> mobjImprintRepository,
                                                IDbContext dbContext)
        {
            _publishingCompanyService = publishingCompanyService;
            _localizationService = localizationService;
            _loggerService = loggerService;
            _mobjGeographicalRepository = geoMaster;
            _mobjApplicationSetUpService = mobjApplicationSetUpService;
            _mobjApplicationSetUpRepository = mobjApplicationSetUpRepository;
            _mobjPublishingCompanyRepository = mobjPublishingCompanyRepository;
            _mobjProductLicenseRepository = mobjProductLicenseRepository;
            _mobjPubCenterRepository = mobjPubCenterRepository;
            _mobjProprietorRepository = mobjProprietorRepository;
            _mobjImprintRepository = mobjImprintRepository;
            _dbContext = dbContext;
        }
        #endregion

        #region Api Methods
        [HttpGet]
        public IHttpActionResult GetAllPublishingCompany()
        {
            try
            {
                var mvarPublishingComapanyList = (from P in _mobjPublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")

                                                  join product in _mobjProductLicenseRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Id equals product.publishingcompanyid into productLicenseGroup
                                                  from L in productLicenseGroup.DefaultIfEmpty()

                                                  join pubCenter in _mobjPubCenterRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Id equals pubCenter.PublishingCompanyid into pubCenterGroup
                                                  from A in pubCenterGroup.DefaultIfEmpty()

                                                  join proprietor in _mobjProprietorRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Id equals proprietor.PublishingCompanyId into proprietorGroup
                                                  from O in proprietorGroup.DefaultIfEmpty()

                                                  join imprint in _mobjImprintRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Id equals imprint.PublishingCompanyId into imprintGroup
                                                  from I in imprintGroup.DefaultIfEmpty()

                                                  //join Country in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                  //on P.CountryId equals Country.Id into countryGroup
                                                  //from C in countryGroup.DefaultIfEmpty()

                                                  //join State in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                  //   on P.Stateid equals State.Id into stateGroup
                                                  //from S in stateGroup.DefaultIfEmpty()

                                                  //join City in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                  //   on P.Cityid equals City.Id into cityGroup
                                                  //from T in cityGroup.DefaultIfEmpty()

                                                  join C in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                 on P.CountryId equals C.Id

                                                  join S in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Stateid equals S.Id

                                                  join T in _mobjGeographicalRepository.Table.Where(a => a.Deactivate == "N")
                                                     on P.Cityid equals T.Id

                                                  select new
                                                  {
                                                      Id = P.Id,
                                                      CompanyName = P.CompanyName,
                                                      ContactPerson = P.ContactPerson,
                                                      Address = P.Address,
                                                      CountryId = C.Id,
                                                      //OtherCountry = P.OtherCountry,
                                                      Website = P.Website,
                                                      Stateid = S.Id,
                                                      Email = P.Email,
                                                      //OtherState = P.OtherState,
                                                      Mobile = P.Mobile,
                                                      Cityid = T.Id,
                                                      //OtherCity = P.OtherCity,
                                                      Pincode = P.Pincode,
                                                      Phone = P.Phone,
                                                      CountryName = C.geogName,
                                                      StateName = S.geogName,
                                                      CityName = T.geogName,
                                                      IsEditable = P.PublishingCompanyCode != "PCM0002" ? "0" : "1",
                                                      Flag = (L.publishingcompanyid != null || A.PublishingCompanyid != null || O.PublishingCompanyId != null || I.PublishingCompanyId != null || P.PublishingCompanyCode == "PCM0002") ? "1" : "0",
                                                      Deactivate = P.Deactivate,
                                                  }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.CompanyName);

                return Json(mvarPublishingComapanyList);

            }
            catch (ACSException ex)
            {

                return Json(ex.InnerException.Message);
            }

            catch (Exception ex)
            {

                return Json(ex.Message);
            }
        }

        public IHttpActionResult InsertPublishingCompany(PublishingCompanyMaster _publishingCompany)
        {
            string status = string.Empty;
            try
            {
                status = _publishingCompanyService.DuplicityCheck(_publishingCompany);
                if (status == "Y")
                {
                    if (_publishingCompany.Id == 0)
                    {
                        IList<ApplicationSetUp> _mobjApplicationSetUpList = _mobjApplicationSetUpRepository.Table.Where(x => x.key == "PublishingCompanyCode" && x.Deactivate == "N").ToList();
                        var PublishingCompanySuggesation = _mobjApplicationSetUpList.Select(P => new
                        {
                            PublishingCompanyCodeValue = P.keyValue,
                            Id = P.Id
                        });

                        _publishingCompany.PublishingCompanyCode = "PCM" + PublishingCompanySuggesation.FirstOrDefault().PublishingCompanyCodeValue;
                        _publishingCompany.PublishingCompanyCode = _publishingCompany.PublishingCompanyCode.ToString().ToUpper();
                        _publishingCompanyService.InsertPublishingCompany(_publishingCompany);

                        ApplicationSetUp mobjApplicationSetUp = new ApplicationSetUp();
                        mobjApplicationSetUp.Id = PublishingCompanySuggesation.FirstOrDefault().Id;
                        ApplicationSetUp _ApplicationSetUpUpdate = _mobjApplicationSetUpService.GetApplicationSetUpById(mobjApplicationSetUp);
                        _ApplicationSetUpUpdate.Id = PublishingCompanySuggesation.FirstOrDefault().Id;
                        int Value = Int32.Parse(PublishingCompanySuggesation.FirstOrDefault().PublishingCompanyCodeValue) + 1;
                        _ApplicationSetUpUpdate.keyValue = Value.ToString().PadLeft(4, '0');
                        _ApplicationSetUpUpdate.ModifiedBy = _publishingCompany.EnteredBy;
                        _ApplicationSetUpUpdate.ModifiedDate = DateTime.Now;
                        _mobjApplicationSetUpService.UpdateApplication(_ApplicationSetUpUpdate);

                    }
                    else
                    {
                        PublishingCompanyMaster objPublishingCompany = _publishingCompanyService.GetPublishingCompanyById(_publishingCompany);
                        objPublishingCompany.CompanyName = _publishingCompany.CompanyName;
                        objPublishingCompany.ContactPerson = _publishingCompany.ContactPerson;
                        objPublishingCompany.Address = _publishingCompany.Address;
                        objPublishingCompany.Email = _publishingCompany.Email;
                        objPublishingCompany.Phone = _publishingCompany.Phone;
                        objPublishingCompany.Mobile = _publishingCompany.Mobile;
                        objPublishingCompany.Website = _publishingCompany.Website;
                        objPublishingCompany.CountryId = _publishingCompany.CountryId;
                        objPublishingCompany.OtherCountry = _publishingCompany.OtherCountry;
                        objPublishingCompany.Stateid = _publishingCompany.Stateid;
                        objPublishingCompany.OtherState = _publishingCompany.OtherState;
                        objPublishingCompany.Cityid = _publishingCompany.Cityid;
                        objPublishingCompany.OtherCity = _publishingCompany.OtherCity;
                        objPublishingCompany.Pincode = _publishingCompany.Pincode;

                        objPublishingCompany.ModifiedBy = _publishingCompany.EnteredBy;
                        objPublishingCompany.ModifiedDate = System.DateTime.Now;

                        _publishingCompanyService.UpdatePublishingCompany(objPublishingCompany);

                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                else
                {
                    status = "Duplicate";
                }
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);
        }

        public IHttpActionResult PublishingCompanyDelete(PublishingCompanyMaster _publishingCompany)
        {
            string status = string.Empty;
            try
            {
                PublishingCompanyMaster publishingCompany = _publishingCompanyService.GetPublishingCompanyById(_publishingCompany);

                publishingCompany.Deactivate = "Y";
                publishingCompany.ModifiedBy = _publishingCompany.EnteredBy;
                publishingCompany.ModifiedDate = DateTime.Now;
                _publishingCompanyService.UpdatePublishingCompany(publishingCompany);
                status = "OK";
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);
        }

        public IHttpActionResult PublishingCompany(PublishingCompanyMaster _publishingCompany)
        {
            return Json(_publishingCompanyService.GetPublishingCompanyById(_publishingCompany));
        }
        #endregion
    }
}