using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Services.Master
{
    /// <summary>
    /// ILanguageMasterService
    /// created By : Ankush Kumar
    /// Date : 11/07/2016
    /// </summary>
    public partial interface ILanguageMasterService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="LanguageMaster">LanguageMaster class object</param>
        /// <returns></returns>
        string DuplicityCheck(LanguageMaster Language);

        /// <summary>
        /// Insert LanguageMaster 
        /// </summary>
        /// <param name="LanguageMaster">LanguageMaster class object</param>
        /// <returns></returns>
        void InsertLanguage(LanguageMaster Language);

        /// <summary>
        /// Get /// <param name="city">LanguageMaster class object</param>
        /// </summary>
        /// <returns>LanguageMaster</returns>
        LanguageMaster GetLanguageById(int Id);

        /// <summary>
        /// Update LanguageMaster 
        /// </summary>
        /// <param name="LanguageMaster">LanguageMaster class object</param>
        /// <returns></returns>
        void UpdateLanguage(LanguageMaster Language);


        /// <summary>
        /// Delete LanguageMaster 
        /// </summary>
        /// <param name="LanguageMaster">Delete LanguageMaster Object</param>
        /// <returns></returns>
        void DeleteSubsidiaryRights(LanguageMaster Language);

        /// <summary>
        /// GetAll LanguageMaster 
        /// </summary>
        /// <param name="LanguageMaster">GetAll LanguageMaster Object</param>
        /// <returns></returns>
        IList<LanguageMaster> GetAllLanguage();

    }
}
