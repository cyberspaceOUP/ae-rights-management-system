using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ACS.Core.Domain.Staff;

namespace ACS.Services.Staff
{
    public partial interface IStaffMasterService
    {
        /// <summary>
        /// Insert StaffMaster 
        /// </summary>
        /// <param name="staff">StaffMaster class object</param>
        /// <returns></returns>
        void insertStaffMaster(StaffMaster staff);

        /// <summary>
        /// search StaffMaster according to mobile no or ID card number for a society
        /// </summary>
        /// <param name="SocietyId,FlatId,MobileNo,IDNumber">SocietyId as int, FlatId as int, MobileNo as string, IDNumber as string</param>
        /// <returns>StaffMaster</returns>
        IList<ACS.Core.Domain.Staff.StaffMaster> SearchSocietyStaffMasterByMobileAndIDNumber(int SocietyId, int FlatId, string MobileNo, string IDNumber);

        /// <summary>
        /// get all staff details by flatId
        /// </summary>
        /// <param name="flatId">flatId as int/param>
        /// <returns>StaffMaster</returns>
        IList<ACS.Core.Domain.Staff.StaffMaster> GetStaffMasterByFlatId(int flatId);

        /// <summary>
        /// Get StaffMaster according to StaffId
        /// </summary>
        /// <param name="StaffId">StaffId as int</param>
        /// <returns>StaffMaster</returns>
        ACS.Core.Domain.Staff.StaffMaster GetStaffMasterDetailsById(int staffId);

        /// <summary>
        /// search StaffMaster for validation according to mobile no and id number for a society
        /// </summary>
        /// <param name="SocietyId,MobileNo1,MobileNo2,IDNumber1,IDNumber2">SocietyId as int, MobileNo1 as string, MobileNo2 as string, IDNumber1 as string, IDNumber2 as string</param>
        /// <returns>StaffMaster</returns>
        ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffForValidation(int SocietyId, string MobileNo1, string MobileNo2, string IDNumber1, string IDNumber2);

        /// <summary>
        /// Insert FlatStaffLink 
        /// </summary>
        /// <param name="flatStaff">FlatStaffLink class object</param>
        /// <returns></returns>
        void insertFlatStaffLink(FlatStaffLink flatStaff);

        /// <summary>
        /// Update SocietyStaffLink
        /// </summary>
        /// <param name="societyStaff">SocietyStaffLink class object</param>
        /// <returns></returns>
        void updateSocietyStaffLink(SocietyStaffLink societyStaff);

        /// <summary>
        /// Insert SocietyStaffValidationDetails
        /// </summary>
        /// <param name="societyStaff">SocietyStaffValidationDetails class object</param>
        /// <returns></returns>
        void insertSocietyStaffValidationDetails(SocietyStaffValidationDetails societyStaff);

        /// <summary>
        /// search StaffMaster according to mobile no for a society
        /// </summary>
        /// <param name="SocietyId,MobileNo">SocietyId as int, MobileNo as string</param>
        /// <returns>StaffMaster</returns>
        ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffByMobile(int SocietyId, string MobileNo);

        /// <summary>
        /// search StaffMaster according to ID number for a society
        /// </summary>
        /// <param name="SocietyId,IDNumber">SocietyId as int, IDNumber as string</param>
        /// <returns>StaffMaster</returns>
        ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffByIDNumber(int SocietyId, string IDNumber);

        /// <summary>
        /// get all temporary validated staff details by societyId for Permanent validation by RWA
        /// </summary>
        /// <param name="societyId">societyId as int/param>
        /// <returns>StaffMaster</returns>
        IList<ACS.Core.Domain.Staff.StaffMaster> GetSocietyTemporaryStaffForRWAValidation(int societyId);
    }
}
