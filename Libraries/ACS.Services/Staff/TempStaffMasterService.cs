using ACS.Core.Data;
using ACS.Core.Domain.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Staff
{
    public partial class TempStaffMasterService : ITempStaffMasterService
    {
        #region Fields
        private readonly IRepository<TempStaffMaster> _tempStaffMasterRepository;
        private readonly IRepository<TempStaffIDProofLink> _tempStaffIDProofLinkRepository;
        private readonly IRepository<ResidentStaffRequest> _residentStaffRequestRepository;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="TempStaffMaster">TempStaffMaster</param>        
        public TempStaffMasterService(
            IRepository<TempStaffMaster> tempStaffMasterRepository,
            IRepository<TempStaffIDProofLink> tempStaffIDProofLinkRepository,
            IRepository<ResidentStaffRequest> residentStaffRequestRepository
            )
        {
            this._tempStaffMasterRepository = tempStaffMasterRepository;
            this._tempStaffIDProofLinkRepository = tempStaffIDProofLinkRepository;
            this._residentStaffRequestRepository = residentStaffRequestRepository;
        }
        #endregion

        #region Methods

        // get all temp staff list
        public IList<TempStaffMaster> GetAllTempStaffs()
        {
            return _tempStaffMasterRepository.Table.ToList();
        }

        // get temp staff details basis on flatId
        public IList<ACS.Core.Domain.Staff.TempStaffMaster> GetTempStaffMasterByFlatId(int flatId)
        {
            if (flatId == 0)
                return null;

            if (_tempStaffMasterRepository.Table.Any(i => i.FlatId == flatId))
                return _tempStaffMasterRepository.Table.Where(i => i.FlatId == flatId).ToList();
            else
                return null;
        }

        // insert temp staff master data
        public void insertTempStaffMaster(TempStaffMaster tempStaff)
        {
            if (tempStaff == null)
                throw new ArgumentNullException("tempStaff");

            _tempStaffMasterRepository.Insert(tempStaff);
        }

        // get temp staff details basis on tempstaffid
        public ACS.Core.Domain.Staff.TempStaffMaster GetTempStaffMasterById(int tempStaffId)
        {
            if (tempStaffId == 0)
                return null;

            if (_tempStaffMasterRepository.Table.Any(i => i.Id == tempStaffId))
                return _tempStaffMasterRepository.Table.Where(i => i.Id == tempStaffId).FirstOrDefault();
            else
                return null;
        }

        // update temp staff master data
        public void updateTempStaffMaster(TempStaffMaster tempStaff)
        {
            if (tempStaff == null)
                throw new ArgumentNullException("tempStaff");

            _tempStaffMasterRepository.Update(tempStaff);
        }

        // delete TempStaff Details
        public void deleteTempStaff(TempStaffMaster tempStaff)
        {
            if (tempStaff == null)
                throw new ArgumentNullException("tempStaff");

            _tempStaffMasterRepository.Delete(tempStaff);
        }

        // delete TempStaffIDProofLink details
        public void deleteTempStaffIDProofLink(TempStaffIDProofLink tempStaffProofLink)
        {
            if (tempStaffProofLink == null)
                throw new ArgumentNullException("tempStaffProofLink");

            _tempStaffIDProofLinkRepository.Delete(tempStaffProofLink);
        }

        // fetch all pending staff for RWA validation
        public IList<ACS.Core.Domain.Staff.TempStaffMaster> GetAllPendingStaffForRWAValidation(int societyId)
        {
            if (societyId == 0)
                return null;

            var _tempStaffList = _tempStaffMasterRepository.Table.Where(t => t.Flat.Tower.SocietyId == societyId).ToList();

            if (_tempStaffList.Count > 0)
                return _tempStaffList;
            else
                return null;
        }

        // insert ResidentStaffRequest data
        public void insertResidentStaffRequest(ResidentStaffRequest residentStaff)
        {
            if (residentStaff == null)
                throw new ArgumentNullException("residentStaff");

            _residentStaffRequestRepository.Insert(residentStaff);
        }

        #endregion
    }
}
