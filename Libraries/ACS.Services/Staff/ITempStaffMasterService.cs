using ACS.Core.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Staff
{
    public partial interface ITempStaffMasterService
    {
        /// <summary>
        /// Gets all TempStaffMaster
        /// </summary>
        /// <param name=""></param>
        /// <returns>TempStaffMaster collection</returns>
        IList<TempStaffMaster> GetAllTempStaffs();

        /// <summary>
        /// Get TempStaffMaster according to flatId
        /// </summary>
        /// <param name="flatId">FlatId as int</param>
        /// <returns>TempStaffMaster</returns>
        IList<ACS.Core.Domain.Staff.TempStaffMaster> GetTempStaffMasterByFlatId(int flatId);

        /// <summary>
        /// Insert TempStaffMaster 
        /// </summary>
        /// <param name="tempStaff">TempStaffMaster class object</param>
        /// <returns></returns>
        void insertTempStaffMaster(TempStaffMaster tempStaff);

        /// <summary>
        /// Get TempStaffMaster according to TempStaffId
        /// </summary>
        /// <param name="TempStaffId">TempStaffId as int</param>
        /// <returns>TempStaffMaster</returns>
        ACS.Core.Domain.Staff.TempStaffMaster GetTempStaffMasterById(int tempStaffId);

        /// <summary>
        /// Update TempStaffMaster 
        /// </summary>
        /// <param name="tempStaff">TempStaffMaster class object</param>
        /// <returns></returns>
        void updateTempStaffMaster(TempStaffMaster tempStaff);

        /// <summary>
        /// Delete TempStaff Details 
        /// </summary>
        /// <param name="tempStaff">TempStaffMaster class object</param>
        /// <returns></returns>
        void deleteTempStaff(TempStaffMaster tempStaff);

        /// <summary>
        /// Delete TempStaffIDProofLink data based on TempStaffId 
        /// </summary>
        /// <param name="tempStaffProofLink">TempStaffIDProofLink class object</param>
        /// <returns></returns>
        void deleteTempStaffIDProofLink(TempStaffIDProofLink tempStaffProofLink);

        /// <summary>
        /// Get all pending staff for RWA validation
        /// </summary>
        /// <param name="societyId">societyId as int</param>
        /// <returns>TempStaff</returns>
        IList<ACS.Core.Domain.Staff.TempStaffMaster> GetAllPendingStaffForRWAValidation(int societyId);

        /// <summary>
        /// Insert ResidentStaffRequest 
        /// </summary>
        /// <param name="residentStaff">ResidentStaffRequest class object</param>
        /// <returns></returns>
        void insertResidentStaffRequest(ResidentStaffRequest residentStaff);
    }
}
