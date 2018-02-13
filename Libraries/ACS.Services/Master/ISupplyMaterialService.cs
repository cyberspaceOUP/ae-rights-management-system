//Added By Suranjana on 13/07/2016

using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;

namespace ACS.Services.Master
{
    public partial interface ISupplyMaterialService
    {
        /// <summary>
        /// Method to get all SupplyMaterialMaster
        /// </summary>
        /// <returns>returns list of SupplyMaterialMaster</returns>
        IList<SupplyMaterialMaster> GetAllSupplyMaterial();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(SupplyMaterialMaster SupplyMaterial);

        /// <summary>
        /// Methos to insert SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        void InsertSupplyMaterial(SupplyMaterialMaster SupplyMaterial);

        /// <summary>
        /// Methos to Fetch SupplyMaterialMaster Data
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        /// <returns>returns SupplyMaterialMaster object</returns>
        SupplyMaterialMaster GetSupplyMaterialById(SupplyMaterialMaster SupplyMaterial);

        /// <summary>
        /// Methos to update SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        void UpdateSupplyMaterial(SupplyMaterialMaster SupplyMaterial);

        /// <summary>
        /// Methos to delete SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        void DeleteSupplyMaterial(SupplyMaterialMaster SupplyMaterial);
    }
}
