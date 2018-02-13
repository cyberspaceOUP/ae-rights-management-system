using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Services.Master
{
    /// <summary>
    /// IGeographicalService
    /// created By : Ankush Kumar
    /// Date : 13/07/2016
    /// </summary>
    public partial interface IGeographicalService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="Geographical">Geographical class object</param>
        /// <returns></returns>
        String DuplicityCheck(GeographicalMaster Geographical);

        /// <summary>
        /// Insert Geographical 
        /// </summary>
        /// <param name="Geographical">Geographical class object</param>
        /// <returns></returns>
        void InsertGeographical(GeographicalMaster Geographical);

        /// <summary>
        /// Get /// <param name="city">Geographical class object</param>
        /// </summary>
        /// <returns>Geographical</returns>
        GeographicalMaster GetGeographicalById(int Id);

        /// <summary>
        /// Update Geographical 
        /// </summary>
        /// <param name="SubsidiaryRights">Geographical class object</param>
        /// <returns></returns>
        void UpdateGeographical(GeographicalMaster Geographical);


        /// <summary>
        /// Delete Geographical 
        /// </summary>
        /// <param name="Geographical">Delete Geographical Object</param>
        /// <returns></returns>
        void DeleteGeographical(GeographicalMaster Geographical);

        /// <summary>
        /// GetAll Countries 
        /// </summary>
        /// <param name="Geographical">GetAll Countries Object</param>
        /// <returns>Geographical List</returns>
        IList<GeographicalMaster> GetGeographicalList();

        /// <summary>
        /// GetAll Countries 
        /// </summary>
        /// <param name="Geographical">GetAll Countries Object</param>
        /// <returns>Geographical List</returns>
        IList<GeographicalMaster> GetGeographicalList(string geogtype, int? parentid = null);

    }
}
