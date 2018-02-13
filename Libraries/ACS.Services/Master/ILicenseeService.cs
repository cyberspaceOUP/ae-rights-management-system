using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface ILicenseeService
    {
        /// <summary>
        /// Method to get all LicenseeMaster
        /// </summary>
        /// <returns>returns list of LicenseeMaster</returns>
        IList<LicenseeMaster> GetAllLicensee();

        /// <summary> 
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        /// <returns>returns string</returns>
        String DuplicateCheck(LicenseeMaster mobjLicensee);

        /// <summary>
        /// Methos to insert LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        void InsertLicensee(LicenseeMaster mobjLicensee);

        /// <summary>
        /// Methos to Fetch LicenseeMaster Data
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        /// <returns>returns LicenseeMaster object</returns>
        LicenseeMaster GetLicenseeById(LicenseeMaster mobjLicensee);

        /// <summary>
        /// Methos to update LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        void UpdateLicensee(LicenseeMaster mobjLicensee);

        /// <summary>
        /// Methos to delete LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        void DeleteLicensee(LicenseeMaster mobjLicensee);
    }
}
