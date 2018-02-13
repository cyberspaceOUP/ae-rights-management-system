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
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Services.User;

namespace SLV.API.Controllers.Master
{
    public class GeographicalMasterController : ApiController
    {
        //Added by Ankush Kumar on 13/07/2016
        private readonly IGeographicalService _geographicalService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IPublishingCompanyService _publishingCompanyService;
        private readonly IPubCenterService _mobjPubCenterService;
        private readonly IAuthorService _AuthorTypeService;
        private readonly ICopyrightHolderService _mobjCopyrightHolderService;
        private readonly ILicenseeService _mobjLicenseeService;

        public GeographicalMasterController(
            IGeographicalService geographicalService,
             IDbContext dbContext,
             ILocalizationService localizationService,
            IPublishingCompanyService publishingCompanyService,
            IPubCenterService mobjPubCenterService,
            IAuthorService AuthorService,
            ICopyrightHolderService mobjCopyrightHolderService,
            ILicenseeService mobjLicenseeService
            )
        {
            _geographicalService = geographicalService;
            _localizationService = localizationService;
            this._dbContext = dbContext;
            _publishingCompanyService = publishingCompanyService;
            _mobjPubCenterService = mobjPubCenterService;
            _AuthorTypeService = AuthorService;
            _mobjCopyrightHolderService = mobjCopyrightHolderService;
            _mobjLicenseeService = mobjLicenseeService;
        }


        /// <summary>
        /// Api method to insert Geographical
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertGeographical(GeographicalMaster Geographical)
        {
            string status = "";
            try
            {
                status = _geographicalService.DuplicityCheck(Geographical);
                if (status == "Y")
                {
                    if (Geographical.Id == 0)
                    {
                        _geographicalService.InsertGeographical(Geographical);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        GeographicalMaster _Geographical = _geographicalService.GetGeographicalById(Geographical.Id);
                        _Geographical.parentid = Geographical.parentid;
                        _Geographical.geogName = Geographical.geogName;
                        _Geographical.geogcode = Geographical.geogName.Replace(".", string.Empty).Substring(0, 2).ToUpper();
                        _Geographical.ModifiedBy = Geographical.EnteredBy;
                        _Geographical.ModifiedDate = DateTime.Now;
                        _geographicalService.UpdateGeographical(_Geographical);
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
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);
        }


        /// <summary>
        /// Api method to delete GeographicalMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts GeographicalMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteGeographical(GeographicalMaster Geographical)
        {

            string status = string.Empty;
            try
            {
                GeographicalMaster _Geographical = _geographicalService.GetGeographicalById(Geographical.Id);
                _Geographical.Deactivate = "Y";
                _Geographical.DeactivateBy = Geographical.EnteredBy;
                _Geographical.DeactivateDate = DateTime.Now;
                _geographicalService.UpdateGeographical(_Geographical);

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


        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getGeographical(int Id)
        {
            GeographicalMaster _geographicalMaster = _geographicalService.GetGeographicalById(Id);
            return Json(SerializeObj.SerializeObject(new { _geographicalMaster }));
        }

        [HttpGet]
        public IHttpActionResult GetGeographicalList(string geogtype, int? parentid = null)
        {
            return Json(_geographicalService.GetGeographicalList(geogtype, parentid).ToList());
        }

        [HttpGet]
        public IHttpActionResult GetCountryList()
        {
            var CountryList = _geographicalService.GetGeographicalList("Country").ToList();
            var StateList = _geographicalService.GetGeographicalList("State").ToList();
            //var PubLishingList = _publishingCompanyService.GetAllPublishingCompany().ToList();
            //var PubCenterList = _mobjPubCenterService.GetAllPubCenter().ToList();
            //var AuthorList = _AuthorTypeService.getAuthorMaster().ToList();
            //var LicenseeList = _mobjLicenseeService.GetAllLicensee();
            //var CopyrightHolderList = _mobjCopyrightHolderService.GetAllCopyrightHolder();

            var leftList = (from countryList in CountryList

                            //Check country Used in state
                            join stateList in StateList
                            on countryList.Id equals stateList.parentid into stateList_New
                            from stateList in stateList_New.DefaultIfEmpty()

                            ////Check country in PubLishing Company Master
                            //join pubLishingList in PubLishingList
                            //on countryList.Id equals pubLishingList.CountryId into pubLishingList_New
                            //from pubLishingList in pubLishingList_New.DefaultIfEmpty()

                            ////Check country in Pub Center Master
                            //join pubCenterList in PubCenterList
                            //on countryList.Id equals pubCenterList.CountryId into pubCenterList_New
                            //from pubCenterList in pubCenterList_New.DefaultIfEmpty()

                            ////Check country in Licensee Master
                            //join licenseeList in LicenseeList
                            //on countryList.Id equals licenseeList.CountryId into licenseeList_New
                            //from licenseeList in licenseeList_New.DefaultIfEmpty()

                            ////Check country in Copyright Holder Master
                            //join copyrightHolderList in CopyrightHolderList
                            //on countryList.Id equals copyrightHolderList.CountryId into copyrightHolderList_New
                            //from copyrightHolderList in copyrightHolderList_New.DefaultIfEmpty()

                            ////Check country in Author Master
                            ////On Author Country Column
                            //join authorList in AuthorList
                            //on countryList.Id equals authorList.CountryId into authorList_New
                            //from authorList in authorList_New.DefaultIfEmpty()

                            ////On Author Affiliation Country Column
                            //join author_AffiliationList in AuthorList
                            //on countryList.Id equals author_AffiliationList.AffiliationCountryId into author_AffiliationList_New
                            //from author_AffiliationList in author_AffiliationList_New.DefaultIfEmpty()

                            ////On Author Beneficiary Country Column
                            //join author_BeneficiaryList in AuthorList
                            //on countryList.Id equals author_BeneficiaryList.BeneficiaryCountryId into author_BeneficiaryList_New
                            //from author_BeneficiaryList in author_BeneficiaryList_New.DefaultIfEmpty()

                            ////On Author Nominee Country Column
                            //join author_NomineeList in AuthorList
                            //on countryList.Id equals author_NomineeList.NomineeCountryId into author_NomineeList_New
                            //from author_NomineeList in author_NomineeList_New.DefaultIfEmpty()

                            select new
                            {
                                Id = countryList.Id,
                                geogName = countryList.geogName,
                                Flag = (stateList != null ? stateList.parentid != null ? "1" : "0" : "0") == "1" ? "1" :
                                        //(pubLishingList != null ? pubLishingList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(pubCenterList != null ? pubCenterList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(licenseeList != null ? licenseeList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(copyrightHolderList != null ? copyrightHolderList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(authorList != null ? authorList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_AffiliationList != null ? author_AffiliationList.AffiliationCountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_BeneficiaryList != null ? author_BeneficiaryList.BeneficiaryCountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_NomineeList != null ? author_NomineeList.NomineeCountryId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        "0"
                                //,
                                //Flag1 = (copyrightHolderList != null ? copyrightHolderList.CountryId != 0 ? "1" : "0" : "0") == "1" ? "1" : "0",
                            }).Distinct().ToList();

            return Json(leftList);

        }


        [HttpGet]
        public IHttpActionResult GetCountryStateList()
        {
            var CountryList = _geographicalService.GetGeographicalList("Country").ToList();
            var StateList = _geographicalService.GetGeographicalList("State").ToList();
            var CityList = _geographicalService.GetGeographicalList("City").ToList();
            //var PubLishingList = _publishingCompanyService.GetAllPublishingCompany().ToList();
            //var PubCenterList = _mobjPubCenterService.GetAllPubCenter().ToList();
            //var AuthorList = _AuthorTypeService.getAuthorMaster().ToList();
            //var LicenseeList = _mobjLicenseeService.GetAllLicensee();
            //var CopyrightHolderList = _mobjCopyrightHolderService.GetAllCopyrightHolder();

            var leftList = (from stateList in StateList

                            //Check State Used in City
                            join cityList in CityList
                            on stateList.Id equals cityList.parentid into cityList_New
                            from cityList in cityList_New.DefaultIfEmpty()

                            ////Check State in PubLishing Company Master
                            //join pubLishingList in PubLishingList
                            //on stateList.Id equals pubLishingList.Stateid into pubLishingList_New
                            //from pubLishingList in pubLishingList_New.DefaultIfEmpty()

                            ////Check State in Pub Center Master
                            //join pubCenterList in PubCenterList
                            //on stateList.Id equals pubCenterList.Stateid into pubCenterList_New
                            //from pubCenterList in pubCenterList_New.DefaultIfEmpty()

                            ////Check state in Licensee Master
                            //join licenseeList in LicenseeList
                            //on stateList.Id equals licenseeList.Stateid into licenseeList_New
                            //from licenseeList in licenseeList_New.DefaultIfEmpty()

                            ////Check state in Copyright Holder Master
                            //join copyrightHolderList in CopyrightHolderList
                            //on stateList.Id equals copyrightHolderList.Stateid into copyrightHolderList_New
                            //from copyrightHolderList in copyrightHolderList_New.DefaultIfEmpty()

                            //join authorList in AuthorList
                            //on stateList.Id equals authorList.StateId into authorList_New
                            //from authorList in authorList_New.DefaultIfEmpty()

                            //join author_AffiliationList in AuthorList
                            //on stateList.Id equals author_AffiliationList.AffiliationStateId into author_AffiliationList_New
                            //from author_AffiliationList in author_AffiliationList_New.DefaultIfEmpty()

                            //join author_BeneficiaryList in AuthorList
                            //on stateList.Id equals author_BeneficiaryList.BeneficiaryStateId into author_BeneficiaryList_New
                            //from author_BeneficiaryList in author_BeneficiaryList_New.DefaultIfEmpty()

                            //join author_NomineeList in AuthorList
                            //on stateList.Id equals author_NomineeList.NomineeStateId into author_NomineeList_New
                            //from author_NomineeList in author_NomineeList_New.DefaultIfEmpty()

                            join countryList in CountryList
                            on stateList.parentid equals countryList.Id
                            select new
                            {
                                Id = stateList.Id,
                                StateName = stateList.geogName,
                                CountryName = countryList.geogName,
                                Flag = (cityList != null ? cityList.parentid != null ? "1" : "0" : "0") == "1" ? "1" :
                                        //(pubLishingList != null ? pubLishingList.Stateid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(pubCenterList != null ? pubCenterList.Stateid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(licenseeList != null ? licenseeList.Stateid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(copyrightHolderList != null ? copyrightHolderList.Stateid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(authorList != null ? authorList.StateId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_AffiliationList != null ? author_AffiliationList.AffiliationStateId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_BeneficiaryList != null ? author_BeneficiaryList.BeneficiaryStateId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        //(author_NomineeList != null ? author_NomineeList.NomineeStateId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        "0"
                                //,Flag1 = (authorList != null ? authorList.StateId != 0 ? "1" : "0" : "0") == "1" ? "1" : "0"
                            }).Distinct().ToList();

            return Json(leftList);


            /*var stateList = _geographicalService.GetGeographicalList().ToList().Where(i => i.parentid != null && i.geogtype == "State");
            var CountryList = _geographicalService.GetGeographicalList().ToList().Where(i => i.parentid == null && i.geogtype == "Country");
            var _final = stateList.Join(CountryList, p => p.parentid, s => s.Id, (p, s) =>
                        new
                        {

                            Id = p.Id,
                            StateName = p.geogName,
                            CountryName = s.geogName
                        });



            return Json(_final);*/
        }

        [HttpGet]
        public IHttpActionResult GetCountryStateCityList()
        {


            var _GetCityList = _dbContext.ExecuteStoredProcedureListNewData<GeographicalModel>("Proc_CityMaster_get").ToList();
            return Json(_GetCityList);

            /*var CountryList = _geographicalService.GetGeographicalList("Country").ToList();
            var StateList = _geographicalService.GetGeographicalList("State").ToList();
            var CityList = _geographicalService.GetGeographicalList("City").ToList();
            var PubLishingList = _publishingCompanyService.GetAllPublishingCompany().ToList();
            var PubCenterList = _mobjPubCenterService.GetAllPubCenter().ToList();
            var AuthorList = _AuthorTypeService.getAuthorMaster().ToList();
            var LicenseeList = _mobjLicenseeService.GetAllLicensee();
            var CopyrightHolderList = _mobjCopyrightHolderService.GetAllCopyrightHolder();

            var leftList = (from cityList in CityList

                            //Check City in PubLishing Company Master
                            join pubLishingList in PubLishingList
                            on cityList.Id equals pubLishingList.Cityid into pubLishingList_New
                            from pubLishingList in pubLishingList_New.DefaultIfEmpty()

                            //Check City in Pub Center Master
                            join pubCenterList in PubCenterList
                            on cityList.Id equals pubCenterList.Cityid into pubCenterList_New
                            from pubCenterList in pubCenterList_New.DefaultIfEmpty()


                            //Check state in Licensee Master
                            join licenseeList in LicenseeList
                            on cityList.Id equals licenseeList.Cityid into licenseeList_New
                            from licenseeList in licenseeList_New.DefaultIfEmpty()

                            //Check state in Copyright Holder Master
                            join copyrightHolderList in CopyrightHolderList
                            on cityList.Id equals copyrightHolderList.Cityid into copyrightHolderList_New
                            from copyrightHolderList in copyrightHolderList_New.DefaultIfEmpty()

                            join authorList in AuthorList
                            on cityList.Id equals authorList.CityId into authorList_New
                            from authorList in authorList_New.DefaultIfEmpty()

                            join author_AffiliationList in AuthorList
                            on cityList.Id equals author_AffiliationList.AffiliationCityId into author_AffiliationList_New
                            from author_AffiliationList in author_AffiliationList_New.DefaultIfEmpty()

                            join author_BeneficiaryList in AuthorList
                            on cityList.Id equals author_BeneficiaryList.BeneficiaryCityId into author_BeneficiaryList_New
                            from author_BeneficiaryList in author_BeneficiaryList_New.DefaultIfEmpty()

                            join author_NomineeList in AuthorList
                            on cityList.Id equals author_NomineeList.NomineeCityId into author_NomineeList_New
                            from author_NomineeList in author_NomineeList_New.DefaultIfEmpty()

                            join stateList in StateList
                            on cityList.parentid equals stateList.Id
                            select new
                            {
                                Id = cityList.Id,
                                CityName = cityList.geogName,
                                StateParentId = stateList.parentid,
                                StateName = stateList.geogName,
                                Flag = (pubLishingList != null ? pubLishingList.Cityid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (pubCenterList != null ? pubCenterList.Cityid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (licenseeList != null ? licenseeList.Cityid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (copyrightHolderList != null ? copyrightHolderList.Cityid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (authorList != null ? authorList.CityId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (author_AffiliationList != null ? author_AffiliationList.AffiliationCityId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (author_BeneficiaryList != null ? author_BeneficiaryList.BeneficiaryCityId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        (author_NomineeList != null ? author_NomineeList.NomineeCityId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        "0"
                            }).Join(CountryList, LastList => LastList.StateParentId, countryList => countryList.Id, (LastList, countryList) =>
                           new
                           {
                               Id = LastList.Id,
                               CityName = LastList.CityName,
                               StateName = LastList.StateName,
                               CountryName = countryList.geogName,
                               Flag = LastList.Flag
                           }).Distinct().ToList();

            return Json(leftList);
             
             */




            //var stateList = _geographicalService.GetGeographicalList("State").ToList();
            //var CountryList = _geographicalService.GetGeographicalList("Country").ToList();
            //var CityList = _geographicalService.GetGeographicalList("City").ToList();
            //var _final = CityList.Join(stateList, c => c.parentid, s => s.Id, (c, s) =>
            //            new
            //            {

            //                Id = c.Id,
            //                CityName = c.geogName,
            //                StateParentId = s.parentid,
            //                StateName = s.geogName
            //            }).Join(CountryList, Last => Last.StateParentId, co => co.Id, (Last, co) =>
            //            new
            //            {

            //                Id = Last.Id,
            //                CityName = Last.CityName,
            //                StateName = Last.StateName,
            //                CountryName = co.geogName,
            //            }
            //            );


            //return Json(_final);
        }

        [HttpGet]
        public IHttpActionResult GetCountryStateCityList(int Id)
        {
            var CityList = _geographicalService.GetGeographicalList("City").ToList().Where(x => x.Id == Id);
            var stateList = _geographicalService.GetGeographicalList("State").ToList();
            var CountryList = _geographicalService.GetGeographicalList("Country").ToList();

            var _final = CityList.Join(stateList, c => c.parentid, s => s.Id, (c, s) =>
                        new
                        {

                            Id = c.Id,
                            CityName = c.geogName,
                            StateId = s.Id,
                            StateName = s.geogName,
                            StateParentId = s.parentid
                        }).Join(CountryList, Last => Last.StateParentId, co => co.Id, (Last, co) =>
                        new
                        {

                            Id = Last.Id,
                            CityName = Last.CityName,
                            StateId = Last.StateId,
                            CountryId = co.Id
                        }
                        );


            return Json(_final);
        }

        /*[HttpGet]
        public IHttpActionResult GetCountryStateList()
        {
            var stateList = _geographicalService.GetGeographicalList().ToList().Where(i => i.parentid != null && i.geogtype == "State");
            var CountryList = _geographicalService.GetGeographicalList().ToList().Where(i => i.parentid == null && i.geogtype == "Country");
            var _final = stateList.Join(CountryList, p => p.parentid, s => s.Id, (p, s) =>
                        new
                        {

                            Id = p.Id,
                            StateName = p.geogName,
                            CountryName = s.geogName
                        });



            return Json(_final);
        }*/

        /*[HttpGet]
        public IHttpActionResult GetCountryStateCityList()
        {

            var stateList = _geographicalService.GetGeographicalList("State").ToList();
            var CountryList = _geographicalService.GetGeographicalList("Country").ToList();
            var CityList = _geographicalService.GetGeographicalList("City").ToList();
            var _final = CityList.Join(stateList, c => c.parentid, s => s.Id, (c, s) =>
                        new
                        {

                            Id = c.Id,
                            CityName = c.geogName,
                            StateParentId = s.parentid,
                            StateName = s.geogName
                        }).Join(CountryList, Last => Last.StateParentId, co => co.Id, (Last, co) =>
                        new
                        {

                            Id = Last.Id,
                            CityName = Last.CityName,
                            StateName = Last.StateName,
                            CountryName = co.geogName,
                        }
                        );


            return Json(_final);
        }*/
    }
}