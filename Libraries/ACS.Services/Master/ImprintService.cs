using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Data;
using ACS.Core.Domain.Master;

namespace ACS.Services.Master
{
    /// <summary>
    /// CountryService
    /// created By : Ankush Kumar
    /// Date : 13/07/2016
    /// </summary>
    public partial class ImprintService : IImprintService
    {
        #region Fields
        private readonly IRepository<ImprintMaster> _imprintServiceRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public ImprintService(
            IRepository<ImprintMaster> ImprintServiceRepository
        )
        {
            _imprintServiceRepository = ImprintServiceRepository;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Check The duplicity of record before insertion and deletion
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public string DuplicityCheck(ImprintMaster Imprint)
        {

            var dupes = _imprintServiceRepository.Table.Where(x => x.ImprintName == Imprint.ImprintName
                                                            && x.Deactivate == "N"
                                                            && (Imprint.Id != 0 ? x.Id : 0) != (Imprint.Id != 0 ? Imprint.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }

        public void InsertImprint(ImprintMaster Imprint)
        {
            Imprint.Deactivate = "N";
            Imprint.EntryDate = DateTime.Now;
            Imprint.ModifiedBy = null;
            Imprint.ModifiedDate = null;
            Imprint.DeactivateBy = null;
            Imprint.DeactivateDate = null;
            _imprintServiceRepository.Insert(Imprint);
        }

        public ImprintMaster GetImprintById(int Id)
        {
            return _imprintServiceRepository.Table.Where(i => i.Id == Id).FirstOrDefault();
        }

        public void UpdateImprint(ImprintMaster Imprint)
        {
            _imprintServiceRepository.Update(Imprint);
        }

        public void DeleteImprint(ImprintMaster Imprint)
        {
            _imprintServiceRepository.Delete(Imprint);
        }

        public IList<ImprintMaster> GetImprintList()
        {
            var query = _imprintServiceRepository.Table;
            var Departments = query.Where(d => d.ImprintName != null && d.Deactivate == "N").OrderBy(c => c.ImprintName)
                .ToList();

            return Departments;

        }

        #endregion

    }
}
