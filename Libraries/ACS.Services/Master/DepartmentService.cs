using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ACS.Services.Master
{
    public partial class DepartmentService : IDepartmentService
    {
        #region Fields
            private readonly IRepository<DepartmentMaster> _DepartmentRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
            public DepartmentService(
            IRepository<DepartmentMaster> DepartmentRepository
        )
        {
            _DepartmentRepository = DepartmentRepository;
        }

        

        #endregion

        #region Methods

            /// <summary>
            /// Check The duplicity of record before insertion and deletion
            /// </summary>
            /// <param name="GeographyType">Geography</param>        
            public string DuplicityCheck(DepartmentMaster Department)
            {

                var dupes = _DepartmentRepository.Table.Where(x =>x.DepartmentName == Department.DepartmentName
                                                                && x.Deactivate == "N"
                                                                && (Department.Id != 0 ? x.Id : 0) != (Department.Id != 0 ? Department.Id : 1)).FirstOrDefault();
                if (dupes != null)
                {
                    return "N";

                }
                else
                {
                    return "Y";
                }
            }

        public void InsertDepartment(DepartmentMaster Department)
        {
            Department.Deactivate = "N";
         // Department.EnteredBy = 10;
            Department.EntryDate = DateTime.Now;
            Department.ModifiedBy = null;
            Department.ModifiedDate = null;
            Department.DeactivateBy = null;
            Department.DeactivateDate = null;
            _DepartmentRepository.Insert(Department);
        }

        public DepartmentMaster GetDepartmentById(DepartmentMaster Department)
        {
            return _DepartmentRepository.Table.Where(i => i.Id == Department.Id).FirstOrDefault();
        }

        public void UpdateDepartment(DepartmentMaster Department)
        {
            _DepartmentRepository.Update(Department);
        }

        public void DeleteDepartment(DepartmentMaster Department)
        {
            _DepartmentRepository.Delete(Department);
        }

        #endregion

    }
}
