using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Services.Master
{
    /// <summary>
    /// ISubsidiaryRightsService
    /// created By : Ankush Kumar
    /// Date : 11/07/2016
    /// </summary>
    public partial interface ISubsidiaryRightsService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="SubsidiaryRights">SubsidiaryRights class object</param>
        /// <returns></returns>
        String DuplicityCheck(SubsidiaryRightsMaster SubsidiaryRights);

        /// <summary>
        /// Insert SubsidiaryRights 
        /// </summary>
        /// <param name="SubsidiaryRights">SubsidiaryRights class object</param>
        /// <returns></returns>
        void InsertSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights);

        /// <summary>
        /// Get /// <param name="city">SubsidiaryRights class object</param>
        /// </summary>
        /// <returns>SubsidiaryRights</returns>
        SubsidiaryRightsMaster GetSubsidiaryRightsById(int Id);

        /// <summary>
        /// Update SubsidiaryRights 
        /// </summary>
        /// <param name="SubsidiaryRights">SubsidiaryRights class object</param>
        /// <returns></returns>
        void UpdateSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights);


        /// <summary>
        /// Delete SubsidiaryRights 
        /// </summary>
        /// <param name="SubsidiaryRights">Delete SubsidiaryRights Object</param>
        /// <returns></returns>
        void DeleteSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights);

        /// <summary>
        /// GetAll SubsidiaryRights 
        /// </summary>
        /// <param name="SubsidiaryRights">GetAll SubsidiaryRights Object</param>
        /// <returns></returns>
        IList<SubsidiaryRightsMaster> GetAllSubsidiaryRights();

    }
}
