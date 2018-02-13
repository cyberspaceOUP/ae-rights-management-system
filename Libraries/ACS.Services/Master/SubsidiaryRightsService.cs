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
    /// SubsidiaryRightsService
    /// created By : Ankush Kumar
    /// Date : 11/07/2016
    /// </summary>
    public partial class SubsidiaryRightsService : ISubsidiaryRightsService
    {
        #region Fields
        private readonly IRepository<SubsidiaryRightsMaster> _subsidiaryRightsServiceRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
            public SubsidiaryRightsService(
            IRepository<SubsidiaryRightsMaster> SubsidiaryRightsServiceRepository
        )
        {
            _subsidiaryRightsServiceRepository = SubsidiaryRightsServiceRepository;
        }
        #endregion


            #region Methods

            /// <summary>
            /// Check The duplicity of record before insertion and deletion
            /// </summary>
            /// <param name="GeographyType">Geography</param>        
            public string DuplicityCheck(SubsidiaryRightsMaster SubsidiaryRights)
            {

                var dupes = _subsidiaryRightsServiceRepository.Table.Where(x => x.SubsidiaryRights == SubsidiaryRights.SubsidiaryRights
                                                                && x.Deactivate == "N"
                                                                && (SubsidiaryRights.Id != 0 ? x.Id : 0) != (SubsidiaryRights.Id != 0 ? SubsidiaryRights.Id : 1)).FirstOrDefault();
                if (dupes != null)
                {
                    return "N";

                }
                else
                {
                    return "Y";
                }
            }

            public void InsertSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
            {
                SubsidiaryRights.Deactivate = "N";
                //SubsidiaryRights.EnteredBy = 10;
                SubsidiaryRights.EntryDate = DateTime.Now;
                SubsidiaryRights.ModifiedBy = null;
                SubsidiaryRights.ModifiedDate = null;
                SubsidiaryRights.DeactivateBy = null;
                SubsidiaryRights.DeactivateDate = null;
                _subsidiaryRightsServiceRepository.Insert(SubsidiaryRights);
            }

            public SubsidiaryRightsMaster GetSubsidiaryRightsById(int Id)
            {
                return _subsidiaryRightsServiceRepository.Table.Where(i => i.Id == Id).FirstOrDefault();
            }

            public void UpdateSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
            {
                _subsidiaryRightsServiceRepository.Update(SubsidiaryRights);
            }

            public void DeleteSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
            {
                _subsidiaryRightsServiceRepository.Delete(SubsidiaryRights);
            }


            public IList<SubsidiaryRightsMaster> GetAllSubsidiaryRights()
            {
                var query = _subsidiaryRightsServiceRepository.Table;
                var Departments = query.Where(d => d.SubsidiaryRights != null && d.Deactivate == "N").OrderBy(c => c.SubsidiaryRights)
                    .ToList();

                return Departments;

            }

            #endregion

    }
}
