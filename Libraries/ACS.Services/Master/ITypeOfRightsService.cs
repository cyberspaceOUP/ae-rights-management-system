using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace ACS.Services.Master
{
    public partial interface ITypeOfRightsService
    {
        /// <summary>
        /// Method to get all TypeOfRightsMaster
        /// </summary>
        /// <returns>returns list of TypeOfRightsMaster</returns>
        IList<TypeOfRightsMaster> GetAllTypeOfRights();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(TypeOfRightsMaster TypeOfRights);

        /// <summary>
        /// Methos to insert TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        void InsertTypeOfRights(TypeOfRightsMaster TypeOfRights);

        /// <summary>
        /// Methos to Fetch TypeOfRightsMaster Data
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        /// <returns>returns TypeOfRightsMaster object</returns>
        TypeOfRightsMaster GetTypeOfRightsById(TypeOfRightsMaster TypeOfRights);

        /// <summary>
        /// Methos to update TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        void UpdateTypeOfRights(TypeOfRightsMaster TypeOfRights);

        /// <summary>
        /// Methos to delete TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        void DeleteTypeOfRights(TypeOfRightsMaster TypeOfRights);
    }
}
