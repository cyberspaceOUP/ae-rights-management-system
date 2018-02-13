using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Services.Master
{
    public class TypeOfRightsService : ITypeOfRightsService
    {
        #region Private Property
        private readonly IRepository<TypeOfRightsMaster> _TypeOfRightsRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRightsRepository">accepts repository object as parameter</param>
        public TypeOfRightsService(IRepository<TypeOfRightsMaster> TypeOfRightsRepository)
        {
            _TypeOfRightsRepository = TypeOfRightsRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all TypeOfRightsMaster
        /// </summary>
        /// <returns>returns list of TypeOfRightsMaster</returns>
        public virtual IList<TypeOfRightsMaster> GetAllTypeOfRights()
        {
            var query = _TypeOfRightsRepository.Table;
            var TypeOfRights = query.Where(d => d.TypeOfRights != null && d.Deactivate == "N").OrderBy(c => c.TypeOfRights)
                .ToList();

            return TypeOfRights;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(TypeOfRightsMaster TypeOfRights)
        {
            var duplicate = _TypeOfRightsRepository.Table.Where(x => x.TypeOfRights == TypeOfRights.TypeOfRights
                                                            && x.Deactivate == "N"
                                                            && (TypeOfRights.Id != 0 ? x.Id : 0) != (TypeOfRights.Id != 0 ? TypeOfRights.Id : 1)).FirstOrDefault();

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
        /// Method to insert TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        public void InsertTypeOfRights(TypeOfRightsMaster TypeOfRights)
        {
            TypeOfRights.Deactivate = "N";
            TypeOfRights.EntryDate = DateTime.Now;
            TypeOfRights.ModifiedBy = null;
            TypeOfRights.ModifiedDate = null;
            TypeOfRights.DeactivateBy = null;
            TypeOfRights.DeactivateDate = null;
            _TypeOfRightsRepository.Insert(TypeOfRights);
        }

        /// <summary>
        /// Methos to Fetch TypeOfRightsMaster Data
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        /// <returns>returns TypeOfRightsMaster object</returns>
        public TypeOfRightsMaster GetTypeOfRightsById(TypeOfRightsMaster TypeOfRights)
        {
            return _TypeOfRightsRepository.Table.Where(i => i.Id == TypeOfRights.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        public void UpdateTypeOfRights(TypeOfRightsMaster TypeOfRights)
        {
            _TypeOfRightsRepository.Update(TypeOfRights);
        }

        /// <summary>
        /// Method to delete TypeOfRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts TypeOfRightsMaster object as parameter</param>
        public void DeleteTypeOfRights(TypeOfRightsMaster TypeOfRights)
        {
            _TypeOfRightsRepository.Delete(TypeOfRights);
        }
        #endregion
    }
}
