using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IDivisionService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        String DuplicityCheck(DivisionMaster Division);

        /// <summary>
        /// Insert Division 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void InsertDivision(DivisionMaster Division);

        /// <summary>
        /// Get Division
        /// </summary>
        /// <param name="Geography city">Division as object</param>
        /// <returns>Division</returns>
        DivisionMaster GetDivisionById(DivisionMaster Division);

        /// <summary>
        /// Update Division 
        /// </summary>
        /// <param name="Division">Division class object</param>
        /// <returns></returns>
        void UpdateDivision(DivisionMaster Division);


        /// <summary>
        /// Delete Division 
        /// </summary>
        /// <param name="Division">Delete Division Object</param>
        /// <returns></returns>
        void DeleteDivision(DivisionMaster Division);

        /// <summary>
        /// Gets all Division
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Division collection</returns>
        IList<DivisionMaster> GetAllDivisions();

        /// <summary>
        /// Gets all SubDivision
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Division collection</returns>
        IList<DivisionMaster> GetAllSubDivisions();

        /// <summary>
        /// Gets all SubDivision By DivisionId
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>SubDivision collection</returns>
        IList<DivisionMaster> GetAllSubDivisionsbyDivisonId(DivisionMaster Division);

    }
   
}
