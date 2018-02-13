using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Master;
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
    public class SeriesMasterController : ApiController
    {
        #region Private Property
        private readonly ISeriesService _mobjSeriesService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductMasterRepository;
        private readonly IRepository<SeriesMaster> _mobjSeriesMasterRepository;
        private readonly IRepository<DivisionMaster> _mobjDivisionMasterRepository;
        private readonly IRepository<AuthorContractOriginal> _mobjAuthorContractRepository;
        private readonly ILogger _mobjLoggerService;
        private readonly IDbContext _dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor methos
        /// </summary>
        /// <param name="mobjSeriesService">accepts SeriesService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public SeriesMasterController(ISeriesService mobjSeriesService,
                                      ILocalizationService mobjLocalizationService,
                                      ILogger mobjLoggerService,
                                      IRepository<SeriesMaster> mobjSeriesMasterRepository,
                                      IRepository<DivisionMaster> mobjDivisionMasterRepository,
                                      IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductMasterRepository,
                                      IRepository<AuthorContractOriginal> mobjAuthorContractRepository,
                                      IDbContext dbContext)
        {
            _mobjSeriesService = mobjSeriesService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjLoggerService = mobjLoggerService;
            _mobjSeriesMasterRepository = mobjSeriesMasterRepository;
            _mobjDivisionMasterRepository = mobjDivisionMasterRepository;
            _mobjProductMasterRepository = mobjProductMasterRepository;
            _mobjAuthorContractRepository = mobjAuthorContractRepository;
            _dbContext = dbContext;
        }
        #endregion

        #region Api Methods

        /// <summary>
        /// Methods to get all Series By Division and Subdivision Name
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetSeriesByDivisionSubdivision()
        {
            var mvarSeriesList = (from S in _mobjSeriesMasterRepository.Table.Where(a => a.Deactivate == "N")

                                  join division in _mobjDivisionMasterRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.divisionid equals division.Id into divisionGroup
                                  from D in divisionGroup.DefaultIfEmpty()

                                  join subdivision in _mobjDivisionMasterRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.Subdivisionid equals subdivision.Id into subdivisionGroup
                                  from Su in subdivisionGroup.DefaultIfEmpty()

                                  join product in _mobjProductMasterRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.Id equals product.SeriesId into productGroup
                                  from T in productGroup.DefaultIfEmpty()

                                  join authorContract in _mobjAuthorContractRepository.Table.Where(a => a.Deactivate == "N")
                                     on S.Id equals authorContract.SeriesId into authorContractGroup
                                  from A in authorContractGroup.DefaultIfEmpty()

                                  select new
                                  {
                                      Id = S.Id,
                                      DivisionName = D.divisionName,
                                      SubdivisionName = Su.divisionName,
                                      SeriesName = S.Seriesname,
                                      Flag = T.SeriesId != null || A.SeriesId != null ? "1" : "0",
                                      Deactivate = S.Deactivate,
                                  }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.SeriesName);

            return Json(mvarSeriesList);
        }


        [HttpGet]
        public IHttpActionResult GetProductSeries()
        {
            var mobj_Product = (from PM in _mobjProductMasterRepository.Table.Where(x => x.Deactivate == "N")
                                join SM in _mobjSeriesMasterRepository.Table.Where(x => x.Deactivate == "N")
                                on PM.SeriesId equals SM.Id into Output
                                from D in Output.DefaultIfEmpty()
                                select new
                                {
                                    Id = D.Id,
                                    SeriesName = D.Seriesname,
                                    Deactivate = D.Deactivate
                                }).Distinct().Where(a => a.Deactivate=="N").OrderBy(a => a.SeriesName);

            return Json(mobj_Product);
        }


        /// <summary>
        /// Methods to get all Series
        /// </summary>
        /// <returns>returns list of TyprOfRights object</returns>
        [HttpGet]
        public IHttpActionResult GetSeriesList()
        {
            return Json(_mobjSeriesService.GetAllSeries().ToList());
        }

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult Series(SeriesMaster mobjSeries)
        {
            SeriesMaster _Series = _mobjSeriesService.GetSeriesById(mobjSeries);
            return Json(_Series);
        }

        /// <summary>
        /// Api method to insert SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult InsertSeries(SeriesMaster mobjSeries)
        {
            string message = "";
            try
            {
                message = _mobjSeriesService.DuplicateCheck(mobjSeries);
                if (message == "Y")
                {
                    if (mobjSeries.Id == 0)
                    {
                        _mobjSeriesService.InsertSeries(mobjSeries);
                        message = _mobjLocalizationService.GetResource("Master.API.Success.Message");
                    }
                    else
                    {
                        SeriesMaster _Series = _mobjSeriesService.GetSeriesById(mobjSeries);
                        _Series.divisionid = mobjSeries.divisionid;
                        _Series.Subdivisionid = mobjSeries.Subdivisionid;
                        _Series.Seriesname = mobjSeries.Seriesname;
                        _Series.ModifiedBy = mobjSeries.EnteredBy;
                        _Series.ModifiedDate = DateTime.Now;
                        _mobjSeriesService.UpdateSeries(_Series);
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
        /// Api method to update SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateSeries(SeriesMaster mobjSeries)
        {
            string message = string.Empty;
            try
            {
                SeriesMaster _Series = _mobjSeriesService.GetSeriesById(mobjSeries);
                _Series.divisionid = mobjSeries.divisionid;
                _Series.Subdivisionid = mobjSeries.Subdivisionid;
                _Series.Seriesname = mobjSeries.Seriesname;
                _Series.ModifiedBy = mobjSeries.EnteredBy;
                _Series.ModifiedDate = DateTime.Now;
                _mobjSeriesService.UpdateSeries(_Series);
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
        /// Api method to delete SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult SeriesDelete(SeriesMaster mobjSeries)
        {
            string message = string.Empty;
            try
            {
                SeriesMaster _Series = _mobjSeriesService.GetSeriesById(mobjSeries);
                _Series.Deactivate = "Y";
                _Series.DeactivateBy = mobjSeries.EnteredBy;
                _Series.DeactivateDate = DateTime.Now;
                _mobjSeriesService.UpdateSeries(_Series);

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