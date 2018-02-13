using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ACS.Services.Master
{
    public partial class DivisionService : IDivisionService
    {
        #region Fields
        private readonly IRepository<DivisionMaster> _DivisionRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public DivisionService(
        IRepository<DivisionMaster> DivisionRepository
    )
        {
            _DivisionRepository = DivisionRepository;
        }



        #endregion

        #region Methods

        public string DuplicityCheck(DivisionMaster Division)
        {
           
            var dupes = _DivisionRepository.Table.Where(x => x.divisionlevel == Division.divisionlevel
                                                      && x.parentdivisionid == Division.parentdivisionid
                                                      && x.divisionName == Division.divisionName
                                                      && x.Deactivate =="N"
                                                      && (Division.Id != 0 ? x.Id : 0) != (Division.Id != 0 ? Division.Id : 1)).FirstOrDefault();
            if (dupes!=null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }




        public void InsertDivision(DivisionMaster Division)
        {
            Division.Deactivate = "N";
            Division.EnteredBy = 10;
            Division.EntryDate = DateTime.Now;
            Division.ModifiedBy = null;
            Division.ModifiedDate = null;
            Division.DeactivateBy = null;
            Division.DeactivateDate = null;
            _DivisionRepository.Insert(Division);
        }

        public DivisionMaster GetDivisionById(DivisionMaster Division)
        {
            return _DivisionRepository.Table.Where(i => i.Id == Division.Id).FirstOrDefault();
        }

        public void UpdateDivision(DivisionMaster Division)
        {
            _DivisionRepository.Update(Division);
        }
        [HttpOptions]
        public void DeleteDivision(DivisionMaster Division)
        {
            _DivisionRepository.Delete(Division);
        }

        //
        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<DivisionMaster> GetAllDivisions()
        {
            var _Division = _DivisionRepository.Table.Where(d => d.parentdivisionid == null && d.Deactivate == "N").OrderBy(c => c.divisionName).ToList();
            return _Division;

        }

        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Country collection</returns>
        public virtual IList<DivisionMaster> GetAllSubDivisions()
        {
            var _Division = _DivisionRepository.Table.Where(d => d.parentdivisionid != null && d.Deactivate == "N").OrderBy(c => c.divisionName).ToList();
            return _Division;

        }


        /// <summary>
        /// Gets all Divisions
        /// </summary>
        /// <param name="showDeactivated">A value indicating whether to show hidden records</param>
        /// <returns>Sub Division collection</returns>
        public virtual IList<DivisionMaster> GetAllSubDivisionsbyDivisonId(DivisionMaster Division)
        {
            return _DivisionRepository.Table.Where(d => d.parentdivisionid == Division.Id).OrderBy(c => c.divisionName).ToList();

        }
        //
        #endregion

    }
}
