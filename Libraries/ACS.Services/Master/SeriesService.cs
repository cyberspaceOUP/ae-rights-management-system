using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public class SeriesService :ISeriesService
    {
        #region Private Property
        private readonly IRepository<SeriesMaster> _mobjSeriesRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of SeriesMaster
        /// </summary>
        /// <param name="mobjSeriesRepository">accepts repository object as parameter</param>
        public SeriesService(IRepository<SeriesMaster> mobjSeriesRepository)
        {
            _mobjSeriesRepository = mobjSeriesRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all SeriesMaster
        /// </summary>
        /// <returns>returns list of SeriesMaster</returns>
        public virtual IList<SeriesMaster> GetAllSeries()
        {
            var query = _mobjSeriesRepository.Table;
            var mvarSeries = query.Where(d => d.Seriesname != null && d.Deactivate == "N").OrderBy(c => c.Seriesname)
                .ToList();

            return mvarSeries;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(SeriesMaster mobjSeries)
        {
            var duplicate = _mobjSeriesRepository.Table.Where(x => x.Seriesname == mobjSeries.Seriesname 
                                                            && x.divisionid == mobjSeries.divisionid
                                                            && x.Subdivisionid == mobjSeries.Subdivisionid
                                                            && x.Deactivate == "N"
                                                            && (mobjSeries.Id != 0 ? x.Id : 0) != (mobjSeries.Id != 0 ? mobjSeries.Id : 1)).FirstOrDefault();

            if (duplicate != null)
            {
                return "N";
            }
            else
            {
                return "Y";
            }
        }

        /// <summary>
        /// Method to insert SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        public void InsertSeries(SeriesMaster mobjSeries)
        {
            mobjSeries.Deactivate = "N";
            mobjSeries.EntryDate = DateTime.Now;
            mobjSeries.ModifiedBy = null;
            mobjSeries.ModifiedDate = null;
            mobjSeries.DeactivateBy = null;
            mobjSeries.DeactivateDate = null;
            _mobjSeriesRepository.Insert(mobjSeries);
        }

        /// <summary>
        /// Methos to Fetch SeriesMaster Data
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        /// <returns>returns SeriesMaster object</returns>
        public SeriesMaster GetSeriesById(SeriesMaster mobjSeries)
        {
            return _mobjSeriesRepository.Table.Where(i => i.Id == mobjSeries.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        public void UpdateSeries(SeriesMaster mobjSeries)
        {
            _mobjSeriesRepository.Update(mobjSeries);
        }

        /// <summary>
        /// Method to delete SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        public void DeleteSeries(SeriesMaster mobjSeries)
        {
            _mobjSeriesRepository.Delete(mobjSeries);
        }
        #endregion
    }
}
