using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public class LicenseeService : ILicenseeService
    {
        #region Private Property
        private readonly IRepository<LicenseeMaster> _mobjLicenseeRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of LicenseeMaster
        /// </summary>
        /// <param name="mobjLicenseeRepository">accepts repository object as parameter</param>
        public LicenseeService(IRepository<LicenseeMaster> mobjLicenseeRepository)
        {
            _mobjLicenseeRepository = mobjLicenseeRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all LicenseeMaster
        /// </summary>
        /// <returns>returns list of LicenseeMaster</returns>
        public virtual IList<LicenseeMaster> GetAllLicensee()
        {
            var query = _mobjLicenseeRepository.Table;
            var mvarLicensee = query.Where(d => d.OrganizationName != null && d.Deactivate == "N").OrderBy(c => c.OrganizationName)
                .ToList();

            return mvarLicensee;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(LicenseeMaster mobjLicensee)
        {
            var duplicate = _mobjLicenseeRepository.Table.Where(x => x.OrganizationName == mobjLicensee.OrganizationName && x.Cityid == mobjLicensee.Cityid
                                                            && x.Deactivate == "N"
                                                            && (mobjLicensee.Id != 0 ? x.Id : 0) != (mobjLicensee.Id != 0 ? mobjLicensee.Id : 1)).FirstOrDefault();
            if (duplicate != null)
            {
                return "N";
            }
            else
            {
                return "Y";
            }
        }

        /// <summary>
        /// Method to insert LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        public void InsertLicensee(LicenseeMaster mobjLicensee)
        {
            mobjLicensee.Deactivate = "N";
            mobjLicensee.EntryDate = DateTime.Now;
            mobjLicensee.ModifiedBy = null;
            mobjLicensee.ModifiedDate = null;
            mobjLicensee.DeactivateBy = null;
            mobjLicensee.DeactivateDate = null;
            _mobjLicenseeRepository.Insert(mobjLicensee);
        }

        /// <summary>
        /// Methos to Fetch LicenseeMaster Data
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        /// <returns>returns LicenseeMaster object</returns>
        public LicenseeMaster GetLicenseeById(LicenseeMaster mobjLicensee)
        {
            return _mobjLicenseeRepository.Table.Where(i => i.Id == mobjLicensee.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        public void UpdateLicensee(LicenseeMaster mobjLicensee)
        {
            _mobjLicenseeRepository.Update(mobjLicensee);
        }

        /// <summary>
        /// Method to delete LicenseeMaster
        /// </summary>
        /// <param name="mobjLicensee">accepts LicenseeMaster object as parameter</param>
        public void DeleteLicensee(LicenseeMaster mobjLicensee)
        {
            _mobjLicenseeRepository.Delete(mobjLicensee);
        }
        #endregion
    }
}
