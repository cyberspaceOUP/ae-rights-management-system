using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface ICopyrightHolderService
    {
        /// <summary>
        /// Method to get all CopyRightHolderMaster
        /// </summary>
        /// <returns>returns list of CopyRightHolderMaster</returns>
        IList<CopyRightHolderMaster> GetAllCopyrightHolder();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(CopyRightHolderMaster mobjCopyrightHolder);

        /// <summary>
        /// Methos to insert CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        void InsertCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder);

        /// <summary>
        /// Methos to Fetch CopyRightHolderMaster Data
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        /// <returns>returns CopyRightHolderMaster object</returns>
        CopyRightHolderMaster GetCopyrightHolderById(CopyRightHolderMaster mobjCopyrightHolder);

        /// <summary>
        /// Methos to update CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        void UpdateCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder);

        /// <summary>
        /// Methos to delete CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        void DeleteCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder);
    }
}
