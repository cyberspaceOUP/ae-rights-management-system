using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public class CopyrightHolderService : ICopyrightHolderService
    {
        #region Private Property
        private readonly IRepository<CopyRightHolderMaster> _mobjCopyrightHolderRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolderRepository">accepts repository object as parameter</param>
        public CopyrightHolderService(IRepository<CopyRightHolderMaster> mobjCopyrightHolderRepository)
        {
            _mobjCopyrightHolderRepository = mobjCopyrightHolderRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all CopyRightHolderMaster
        /// </summary>
        /// <returns>returns list of CopyRightHolderMaster</returns>
        public virtual IList<CopyRightHolderMaster> GetAllCopyrightHolder()
        {
            var query = _mobjCopyrightHolderRepository.Table;
            var mvarCopyrightHolder = query.Where(d => d.CopyRightHolderName != null && d.Deactivate == "N").OrderBy(c => c.CopyRightHolderName)
                .ToList();

            return mvarCopyrightHolder;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(CopyRightHolderMaster mobjCopyrightHolder)
        {
            var duplicate = _mobjCopyrightHolderRepository.Table.Where(x => x.CopyRightHolderName == mobjCopyrightHolder.CopyRightHolderName
                                                            && x.Deactivate == "N"
                                                            && x.Cityid == mobjCopyrightHolder.Cityid
                                                            && (mobjCopyrightHolder.Id != 0 ? x.Id : 0) != (mobjCopyrightHolder.Id != 0 ? mobjCopyrightHolder.Id : 1)).FirstOrDefault();
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
        /// Method to insert CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        public void InsertCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder)
        {
            mobjCopyrightHolder.Deactivate = "N";
            mobjCopyrightHolder.EntryDate = DateTime.Now;
            mobjCopyrightHolder.ModifiedBy = null;
            mobjCopyrightHolder.ModifiedDate = null;
            mobjCopyrightHolder.DeactivateBy = null;
            mobjCopyrightHolder.DeactivateDate = null;
            _mobjCopyrightHolderRepository.Insert(mobjCopyrightHolder);
        }

        /// <summary>
        /// Methos to Fetch CopyRightHolderMaster Data
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        /// <returns>returns CopyRightHolderMaster object</returns>
        public CopyRightHolderMaster GetCopyrightHolderById(CopyRightHolderMaster mobjCopyrightHolder)
        {
            return _mobjCopyrightHolderRepository.Table.Where(i => i.Id == mobjCopyrightHolder.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        public void UpdateCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder)
        {
            _mobjCopyrightHolderRepository.Update(mobjCopyrightHolder);
        }

        /// <summary>
        /// Method to delete CopyRightHolderMaster
        /// </summary>
        /// <param name="mobjCopyrightHolder">accepts CopyRightHolderMaster object as parameter</param>
        public void DeleteCopyrightHolder(CopyRightHolderMaster mobjCopyrightHolder)
        {
            _mobjCopyrightHolderRepository.Delete(mobjCopyrightHolder);
        }
        #endregion
    }
}
