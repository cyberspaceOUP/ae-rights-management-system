//Added By Suranjana on 13/07/2016

using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Services.Master
{
    public class TerritoryRightsService : ITerritoryRightsService
    {
        #region Private Property
        private readonly IRepository<TerritoryRightsMaster> _TerritoryRightsRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRightsRepository">accepts repository object as parameter</param>
        public TerritoryRightsService(IRepository<TerritoryRightsMaster> TerritoryRightsRepository)
        {
            _TerritoryRightsRepository = TerritoryRightsRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all TerritoryRightsMaster
        /// </summary>
        /// <returns>returns list of TerritoryRightsMaster</returns>
        public virtual IList<TerritoryRightsMaster> GetAllTerritoryRights()
        {
            var query = _TerritoryRightsRepository.Table;
            var TerritoryRights = query.Where(d => d.Territoryrights != null && d.Deactivate == "N").OrderBy(c => c.Territoryrights)
                .ToList();

            return TerritoryRights;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(TerritoryRightsMaster TerritoryRights)
        {
            var duplicate = _TerritoryRightsRepository.Table.Where(x => x.Territoryrights == TerritoryRights.Territoryrights
                                                            && x.Deactivate == "N"
                                                            && (TerritoryRights.Id != 0 ? x.Id : 0) != (TerritoryRights.Id != 0 ? TerritoryRights.Id : 1)).FirstOrDefault();

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
        /// Method to insert TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        public void InsertTerritoryRights(TerritoryRightsMaster TerritoryRights)
        {
            TerritoryRights.Deactivate = "N";
            TerritoryRights.EntryDate = DateTime.Now;
            TerritoryRights.ModifiedBy = null;
            TerritoryRights.ModifiedDate = null;
            TerritoryRights.DeactivateBy = null;
            TerritoryRights.DeactivateDate = null;
            _TerritoryRightsRepository.Insert(TerritoryRights);
        }

        /// <summary>
        /// Methos to Fetch TerritoryRightsMaster Data
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        /// <returns>returns TerritoryRightsMaster object</returns>
        public TerritoryRightsMaster GetTerritoryRightsById(TerritoryRightsMaster TerritoryRights)
        {
            return _TerritoryRightsRepository.Table.Where(i => i.Id == TerritoryRights.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        public void UpdateTerritoryRights(TerritoryRightsMaster TerritoryRights)
        {
            _TerritoryRightsRepository.Update(TerritoryRights);
        }

        /// <summary>
        /// Method to delete TerritoryRightsMaster
        /// </summary>
        /// <param name="TerritoryRights">accepts TerritoryRightsMaster object as parameter</param>
        public void DeleteTerritoryRights(TerritoryRightsMaster TerritoryRights)
        {
            _TerritoryRightsRepository.Delete(TerritoryRights);
        }
        #endregion
    }
}
