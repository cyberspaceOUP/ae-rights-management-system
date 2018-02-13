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
    public partial interface IImprintService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="Geographical">Geographical class object</param>
        /// <returns></returns>
        String DuplicityCheck(ImprintMaster Imprint);

        /// <summary>
        /// Insert Geographical 
        /// </summary>
        /// <param name="Geographical">Geographical class object</param>
        /// <returns></returns>
        void InsertImprint(ImprintMaster Imprint);

        /// <summary>
        /// Get /// <param name="city">Geographical class object</param>
        /// </summary>
        /// <returns>Geographical</returns>
        ImprintMaster GetImprintById(int Id);

        /// <summary>
        /// Update Geographical 
        /// </summary>
        /// <param name="SubsidiaryRights">Geographical class object</param>
        /// <returns></returns>
        void UpdateImprint(ImprintMaster Imprint);


        /// <summary>
        /// Delete Geographical 
        /// </summary>
        /// <param name="Geographical">Delete Geographical Object</param>
        /// <returns></returns>
        void DeleteImprint(ImprintMaster Imprint);

        /// <summary>
        /// GetAll Countries 
        /// </summary>
        /// <param name="Geographical">GetAll Countries Object</param>
        /// <returns>Geographical List</returns>
        IList<ImprintMaster> GetImprintList();
        
    }
}
