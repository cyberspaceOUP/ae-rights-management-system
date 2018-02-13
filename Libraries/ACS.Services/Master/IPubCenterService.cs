using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IPubCenterService
    {
        /// <summary>
        /// Method to get all PubCenterMaster
        /// </summary>
        /// <returns>returns list of PubCenterMaster</returns>
        IList<PubCenterMaster> GetAllPubCenter();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(PubCenterMaster mobjPubCenter);

        /// <summary>
        /// Methos to insert PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        void InsertPubCenter(PubCenterMaster mobjPubCenter);

        /// <summary>
        /// Methos to Fetch PubCenterMaster Data
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        /// <returns>returns PubCenterMaster object</returns>
        PubCenterMaster GetPubCenterById(PubCenterMaster mobjPubCenter);

        /// <summary>
        /// Methos to update PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        void UpdatePubCenter(PubCenterMaster mobjPubCenter);

        /// <summary>
        /// Methos to delete PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        void DeletePubCenter(PubCenterMaster mobjPubCenter);
    }
}