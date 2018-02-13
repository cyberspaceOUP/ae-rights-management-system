using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IDepartmentService
    {


        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        String DuplicityCheck(DepartmentMaster Department);

        /// <summary>
        /// Insert Department 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        void InsertDepartment(DepartmentMaster Department);

        /// <summary>
        /// Get Department
        /// </summary>
        /// <param name="Geography city">Department as object</param>
        /// <returns>Department</returns>
        DepartmentMaster GetDepartmentById(DepartmentMaster Department);

        /// <summary>
        /// Update Department 
        /// </summary>
        /// <param name="Department">Department class object</param>
        /// <returns></returns>
        void UpdateDepartment(DepartmentMaster Department);


        /// <summary>
        /// Delete Department 
        /// </summary>
        /// <param name="Department">Delete Department Object</param>
        /// <returns></returns>
        void DeleteDepartment(DepartmentMaster Department);

    }
}
