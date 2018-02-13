//Added By Suranjana on 13/07/2016

using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace ACS.Services.Master
{
    public partial interface ITerritoryRightsService
    {
        /// <summary>
        /// Method to get all TerritoryRightsMaster
        /// </summary>
        /// <returns>returns list of TerritoryRightsMaster</returns>
        IList<TerritoryRightsMaster> GetAllTerritoryRights();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(TerritoryRightsMaster TerritoryRights);

        /// <summary>
        /// Methos to insert TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        void InsertTerritoryRights(TerritoryRightsMaster TerritoryRights);

        /// <summary>
        /// Methos to Fetch TerritoryRightsMaster Data
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        /// <returns>returns TerritoryRightsMaster object</returns>
        TerritoryRightsMaster GetTerritoryRightsById(TerritoryRightsMaster TerritoryRights);

        /// <summary>
        /// Methos to update TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        void UpdateTerritoryRights(TerritoryRightsMaster TerritoryRights);

        /// <summary>
        /// Methos to delete TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        void DeleteTerritoryRights(TerritoryRightsMaster TerritoryRights);
    }
}
