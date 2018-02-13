//Added By Suranjana on 13/07/2016

using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public class SupplyMaterialService : ISupplyMaterialService
    {
        #region Private Property
        private readonly IRepository<SupplyMaterialMaster> _SupplyMaterialRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterialRepository">accepts repository object as parameter</param>
        public SupplyMaterialService(IRepository<SupplyMaterialMaster> SupplyMaterialRepository)
        {
            _SupplyMaterialRepository = SupplyMaterialRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all SupplyMaterialMaster
        /// </summary>
        /// <returns>returns list of SupplyMaterialMaster</returns>
        public virtual IList<SupplyMaterialMaster> GetAllSupplyMaterial()
        {
            var query = _SupplyMaterialRepository.Table;
            var SupplyMaterial = query.Where(d => d.SupplyMaterial != null && d.Deactivate == "N").OrderBy(c => c.SupplyMaterial)
                .ToList();

            return SupplyMaterial;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(SupplyMaterialMaster SupplyMaterial)
        {
            var duplicate = _SupplyMaterialRepository.Table.Where(x => x.SupplyMaterial == SupplyMaterial.SupplyMaterial
                                                            && x.Deactivate == "N"
                                                            && (SupplyMaterial.Id != 0 ? x.Id : 0) != (SupplyMaterial.Id != 0 ? SupplyMaterial.Id : 1)).FirstOrDefault();

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
        /// Method to insert SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        public void InsertSupplyMaterial(SupplyMaterialMaster SupplyMaterial)
        {
            SupplyMaterial.Deactivate = "N";
            SupplyMaterial.EntryDate = DateTime.Now;
            SupplyMaterial.ModifiedBy = null;
            SupplyMaterial.ModifiedDate = null;
            SupplyMaterial.DeactivateBy = null;
            SupplyMaterial.DeactivateDate = null;
            _SupplyMaterialRepository.Insert(SupplyMaterial);
        }

        /// <summary>
        /// Methos to Fetch SupplyMaterialMaster Data
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        /// <returns>returns SupplyMaterialMaster object</returns>
        public SupplyMaterialMaster GetSupplyMaterialById(SupplyMaterialMaster SupplyMaterial)
        {
            return _SupplyMaterialRepository.Table.Where(i => i.Id == SupplyMaterial.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        public void UpdateSupplyMaterial(SupplyMaterialMaster SupplyMaterial)
        {
            _SupplyMaterialRepository.Update(SupplyMaterial);
        }

        /// <summary>
        /// Method to delete SupplyMaterialMaster
        /// </summary>
        /// <param name="SupplyMaterial">accepts SupplyMaterialMaster object as parameter</param>
        public void DeleteSupplyMaterial(SupplyMaterialMaster SupplyMaterial)
        {
            _SupplyMaterialRepository.Delete(SupplyMaterial);
        }
        #endregion
    }
}
