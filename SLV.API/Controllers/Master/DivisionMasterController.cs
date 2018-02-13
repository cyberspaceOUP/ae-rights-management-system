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
using ACS.Services.Product;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using ACS.Core.Data;

namespace SLV.API.Controllers.Master
{
    public class DivisionMasterController : ApiController
    {
        private readonly IDivisionService _DivisionService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _loggerService;
        private readonly ISeriesService _mobjSeriesService;
        private readonly IProductMasterService _ProductMasterService;


        public DivisionMasterController(IDivisionService DivisionService,
            ILocalizationService localizationService,
            ILogger loggerService,
            ISeriesService mobjSeriesService,
            IProductMasterService ProductMasterService
            )
        {
            _DivisionService = DivisionService;
            _localizationService = localizationService;
            _loggerService = loggerService;
            _mobjSeriesService = mobjSeriesService;
            _ProductMasterService = ProductMasterService;
        }


        //Modify By Ankush on 21/07/2016 for Dependency Check
        [HttpGet]
        public IHttpActionResult getDivisionList()
        {
            var DivisionList = _DivisionService.GetAllDivisions().ToList();
            var SubDivisionList = _DivisionService.GetAllSubDivisions().ToList();
            var SeriesList = _mobjSeriesService.GetAllSeries().ToList();
            var ProductList = _ProductMasterService.GetProductMasterList();

            var leftList = (from divisionList in DivisionList

                            //Check Division Used in subDivision
                            join subDivisionList in SubDivisionList
                            on divisionList.Id equals subDivisionList.parentdivisionid into subDivisionList_New
                            from subDivisionList in subDivisionList_New.DefaultIfEmpty()

                            //Check Division Used in Series Master
                            join seriesList in SeriesList
                            on divisionList.Id equals seriesList.divisionid into seriesList_New
                            from seriesList in seriesList_New.DefaultIfEmpty()

                            //Check Division Used in Product Master
                            join productList in ProductList
                            on divisionList.Id equals productList.DivisionId into productList_New
                            from productList in productList_New.DefaultIfEmpty()

                            select new
                {
                    Id = divisionList.Id,
                    divisionName = divisionList.divisionName,
                    Flag = (subDivisionList != null ? subDivisionList.parentdivisionid != null ? "1" : "0" : "0") == "1" ? "1" :
                           (seriesList != null ? seriesList.divisionid != null ? "1" : "0" : "0") == "1" ? "1" :
                           (productList != null ? productList.DivisionId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                           "0"
                    //,Flag1 = (productList != null ? productList.DivisionId != 0 ? "1" : "0" : "0") == "1" ? "1" : "0"
                }).Distinct().ToList();

            return Json(leftList);
        }


        //[HttpGet]
        //public IHttpActionResult getDivisionList()
        //{
        //    return Json(_DivisionService.GetAllDivisions().ToList());
        //}


        //Modify By Ankush on 21/07/2016 for Dependency Check
        [HttpGet]
        public IHttpActionResult getSubDivisionList()
        {
            var DivisionList = _DivisionService.GetAllDivisions().ToList();
            var SubDivisionList = _DivisionService.GetAllSubDivisions().ToList();
            var SeriesList = _mobjSeriesService.GetAllSeries().ToList();
            var ProductList = _ProductMasterService.GetProductMasterList();

            var leftList = (from subDivisionList in SubDivisionList

                            //Check Division Used in Series Master
                            join seriesList in SeriesList
                            on subDivisionList.Id equals seriesList.Subdivisionid into seriesList_New
                            from seriesList in seriesList_New.DefaultIfEmpty()

                            //Check Division Used in Product Master
                            join productList in ProductList
                            on subDivisionList.Id equals productList.SubdivisionId into productList_New
                            from productList in productList_New.DefaultIfEmpty()

                            join divisionList in DivisionList
                            on subDivisionList.parentdivisionid equals divisionList.Id

                            select new
                            {
                                DivisionId = subDivisionList.Id,
                                DivisionName = divisionList.divisionName,
                                SubDivisionName = subDivisionList.divisionName,
                                Flag = (seriesList != null ? seriesList.Subdivisionid != null ? "1" : "0" : "0") == "1" ? "1" :
                                       (productList != null ? productList.SubdivisionId != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                       "0"
                            }).Distinct().ToList();

            return Json(leftList);
        }

        //[HttpGet]
        //public IHttpActionResult getSubDivisionList()
        //{
        //    IList<DivisionMaster> _divisionList = _DivisionService.GetAllSubDivisions().ToList();
        //    var divisionData = _divisionList.Select(dl => new
        //    {
        //        DivisionId = dl.Id,
        //        SubDivisionName = dl.divisionName,
        //        DivisionName = dl.parentdivision.divisionName
        //    });
        //    return Json(SerializeObj.SerializeObject(new { divisionData = divisionData }));
        //}

        #region SubDivisionListByDivisionId
        /// <summary>
        /// Description	      :SubDevision List By DevisionId
        /// Function Name     :SubDivisionListByDivisionId
        /// OutPut parameter  :DataSet
        /// Create Date	      : 11 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        public IHttpActionResult SubDivisionListByDivisionId(DivisionMaster Division)
        {
            IList<DivisionMaster> _divisionList = _DivisionService.GetAllSubDivisionsbyDivisonId(Division).ToList();
            var divisionData = _divisionList.Select(dl => new
            {
                SubDivisionId = dl.Id,
                SubDivisionName = dl.divisionName
            });
            return Json(SerializeObj.SerializeObject(new { divisionData = divisionData }));
        }
        #endregion

        //Added by Dheeraj/
        public IHttpActionResult InsertDivision(DivisionMaster Division)
        {

            string status = "";
            try
            {
                if (Division.parentdivisionid == null)
                {
                    Division.divisionlevel = 1;
                }
                else
                {
                    Division.divisionlevel = 2;
                }
                status = _DivisionService.DuplicityCheck(Division);
                if (status == "Y")
                {
                    if (Division.Id == 0)
                    {
                        _DivisionService.InsertDivision(Division);

                    }
                    else
                    {
                        DivisionMaster mobj_division = _DivisionService.GetDivisionById(Division);
                        mobj_division.divisionName = Division.divisionName;
                        mobj_division.divisionlevel = Division.divisionlevel;
                        mobj_division.parentdivisionid = Division.parentdivisionid;
                        mobj_division.ModifiedBy = Division.EnteredBy;
                        mobj_division.ModifiedDate = System.DateTime.Now;
                        _DivisionService.UpdateDivision(mobj_division);

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



        //Update the Sub division
        public IHttpActionResult DivisionDelete(DivisionMaster _division)
        {

            string status = string.Empty;
            try
            {

                DivisionMaster _Division = _DivisionService.GetDivisionById(_division);
                _Division.Deactivate = "Y";
                _Division.ModifiedBy = _division.EnteredBy;
                _Division.ModifiedDate = DateTime.Now;
                _DivisionService.UpdateDivision(_Division);
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

        public IHttpActionResult SubDivision(DivisionMaster _division)
        {
            DivisionMaster _Division = _DivisionService.GetDivisionById(_division);
            return Json(_Division);
        }


    }
}
