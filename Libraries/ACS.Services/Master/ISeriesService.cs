using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface ISeriesService
    {
        /// <summary>
        /// Method to get all SeriesMaster
        /// </summary>
        /// <returns>returns list of SeriesMaster</returns>
        IList<SeriesMaster> GetAllSeries();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(SeriesMaster mobjSeries);

        /// <summary>
        /// Methos to insert SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        void InsertSeries(SeriesMaster mobjSeries);

        /// <summary>
        /// Methos to Fetch SeriesMaster Data
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        /// <returns>returns SeriesMaster object</returns>
        SeriesMaster GetSeriesById(SeriesMaster mobjSeries);

        /// <summary>
        /// Methos to update SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        void UpdateSeries(SeriesMaster mobjSeries);

        /// <summary>
        /// Methos to delete SeriesMaster
        /// </summary>
        /// <param name="mobjSeries">accepts SeriesMaster object as parameter</param>
        void DeleteSeries(SeriesMaster mobjSeries);
    }
}
