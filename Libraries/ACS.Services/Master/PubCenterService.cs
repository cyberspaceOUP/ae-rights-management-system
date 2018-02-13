using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Services.Master
{
    public class PubCenterService : IPubCenterService
    {
        #region Private Property
        private readonly IRepository<PubCenterMaster> _mobjPubCenterRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenterRepository">accepts repository object as parameter</param>
        public PubCenterService(IRepository<PubCenterMaster> mobjPubCenterRepository)
        {
            _mobjPubCenterRepository = mobjPubCenterRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all PubCenterMaster
        /// </summary>
        /// <returns>returns list of PubCenterMaster</returns>
        public virtual IList<PubCenterMaster> GetAllPubCenter()
        {
            var query = _mobjPubCenterRepository.Table;
            var mvarPubCenter = query.Where(d => d.CenterName != null && d.Deactivate == "N").OrderBy(c => c.CenterName)
                .ToList();

            return mvarPubCenter;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(PubCenterMaster mobjPubCenter)
        {
            var duplicate = _mobjPubCenterRepository.Table.Where(x => x.CenterName == mobjPubCenter.CenterName
                                                            && x.Deactivate == "N"
                                                            && (mobjPubCenter.Id != 0 ? x.Id : 0) != (mobjPubCenter.Id != 0 ? mobjPubCenter.Id : 1)).FirstOrDefault();
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
        /// Method to insert PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        public void InsertPubCenter(PubCenterMaster mobjPubCenter)
        {
            mobjPubCenter.Deactivate = "N";
            mobjPubCenter.EntryDate = DateTime.Now;
            mobjPubCenter.ModifiedBy = null;
            mobjPubCenter.ModifiedDate = null;
            mobjPubCenter.DeactivateBy = null;
            mobjPubCenter.DeactivateDate = null;
            _mobjPubCenterRepository.Insert(mobjPubCenter);
        }

        /// <summary>
        /// Methos to Fetch PubCenterMaster Data
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        /// <returns>returns PubCenterMaster object</returns>
        public PubCenterMaster GetPubCenterById(PubCenterMaster mobjPubCenter)
        {
            return _mobjPubCenterRepository.Table.Where(i => i.Id == mobjPubCenter.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        public void UpdatePubCenter(PubCenterMaster mobjPubCenter)
        {
            _mobjPubCenterRepository.Update(mobjPubCenter);
        }

        /// <summary>
        /// Method to delete PubCenterMaster
        /// </summary>
        /// <param name="mobjPubCenter">accepts PubCenterMaster object as parameter</param>
        public void DeletePubCenter(PubCenterMaster mobjPubCenter)
        {
            _mobjPubCenterRepository.Delete(mobjPubCenter);
        }
        #endregion
    }
}