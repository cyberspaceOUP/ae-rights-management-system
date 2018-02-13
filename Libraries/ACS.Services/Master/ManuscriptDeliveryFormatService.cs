//Added By Suranjana on 13/07/2016

using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Services.Master
{
    public class ManuscriptDeliveryFormatService : IManuscriptDeliveryFormatService
    {
        #region Private Property
        private readonly IRepository<ManuscriptDeliveryFormatMaster> _ManuscriptDeliveryFormatRepository; 
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormatRepository">accepts repository object as parameter</param>
        public ManuscriptDeliveryFormatService(IRepository<ManuscriptDeliveryFormatMaster> ManuscriptDeliveryFormatRepository)
        {
            _ManuscriptDeliveryFormatRepository = ManuscriptDeliveryFormatRepository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Method to get all ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <returns>returns list of ManuscriptDeliveryFormatMaster</returns>
        public virtual IList<ManuscriptDeliveryFormatMaster> GetAllManuscriptDeliveryFormat()
        {
            var query = _ManuscriptDeliveryFormatRepository.Table;
            var ManuscriptDeliveryFormat = query.Where(d => d.ManuscriptDeliveryFormat != null && d.Deactivate == "N").OrderBy(c => c.ManuscriptDeliveryFormat)
                .ToList();

            return ManuscriptDeliveryFormat;
        }

        /// <summary>
        /// Method to check duplicate value
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        /// <returns>returns string</returns>
        public string DuplicateCheck(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat)
        {
            var duplicate = _ManuscriptDeliveryFormatRepository.Table.Where(x => x.ManuscriptDeliveryFormat == ManuscriptDeliveryFormat.ManuscriptDeliveryFormat
                                                            && x.Deactivate == "N"
                                                            && (ManuscriptDeliveryFormat.Id != 0 ? x.Id : 0) != (ManuscriptDeliveryFormat.Id != 0 ? ManuscriptDeliveryFormat.Id : 1)).FirstOrDefault();

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
        /// Method to insert ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        public void InsertManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat)
        {
            ManuscriptDeliveryFormat.Deactivate = "N";
            ManuscriptDeliveryFormat.EntryDate = DateTime.Now;
            ManuscriptDeliveryFormat.ModifiedBy = null;
            ManuscriptDeliveryFormat.ModifiedDate = null;
            ManuscriptDeliveryFormat.DeactivateBy = null;
            ManuscriptDeliveryFormat.DeactivateDate = null;
            _ManuscriptDeliveryFormatRepository.Insert(ManuscriptDeliveryFormat);
        }

        /// <summary>
        /// Methos to Fetch ManuscriptDeliveryFormatMaster Data
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        /// <returns>returns ManuscriptDeliveryFormatMaster object</returns>
        public ManuscriptDeliveryFormatMaster GetManuscriptDeliveryFormatById(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat)
        {
            return _ManuscriptDeliveryFormatRepository.Table.Where(i => i.Id == ManuscriptDeliveryFormat.Id).FirstOrDefault();
        }

        /// <summary>
        /// Method to update ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        public void UpdateManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat)
        {
            _ManuscriptDeliveryFormatRepository.Update(ManuscriptDeliveryFormat);
        }

        /// <summary>
        /// Method to delete ManuscriptDeliveryFormatMaster
        /// </summary>
        /// <param name="ManuscriptDeliveryFormat">accepts ManuscriptDeliveryFormatMaster object as parameter</param>
        public void DeleteManuscriptDeliveryFormat(ManuscriptDeliveryFormatMaster ManuscriptDeliveryFormat)
        {
            _ManuscriptDeliveryFormatRepository.Delete(ManuscriptDeliveryFormat);
        }
        #endregion
    }
}
