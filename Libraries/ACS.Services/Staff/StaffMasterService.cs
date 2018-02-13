using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Data;
using ACS.Core.Domain.Staff;

namespace ACS.Services.Staff
{
    public partial class StaffMasterService : IStaffMasterService
    {
        #region Fields
        private readonly IRepository<StaffMaster> _staffMasterRepository;
        private readonly IRepository<FlatStaffLink> _flatStaffLinkRepository;
        private readonly IRepository<SocietyStaffLink> _societyStaffLinkRepository;
        private readonly IRepository<SocietyStaffValidationDetails> _societyStaffValidationDetailsRepository;
        #endregion

        #region Ctor
        /// <param name="StaffMaster">StaffMaster repository</param>        
        public StaffMasterService(
            IRepository<StaffMaster> staffMasterRepository,
            IRepository<FlatStaffLink> flatStaffLinkRepository,
            IRepository<SocietyStaffLink> societyStaffLinkRepository,
            IRepository<SocietyStaffValidationDetails> societyStaffValidationDetailsRepository
            )
        {
            this._staffMasterRepository = staffMasterRepository;
            this._flatStaffLinkRepository = flatStaffLinkRepository;
            this._societyStaffLinkRepository = societyStaffLinkRepository;
            this._societyStaffValidationDetailsRepository = societyStaffValidationDetailsRepository;
        }
        #endregion

        #region Methods

        // insert staff master data
        public void insertStaffMaster(StaffMaster staff)
        {
            if (staff == null)
                throw new ArgumentNullException("staff");

            _staffMasterRepository.Insert(staff);
        }

        // search StaffMaster according to mobile no or ID card number for a society
        public IList<ACS.Core.Domain.Staff.StaffMaster> SearchSocietyStaffMasterByMobileAndIDNumber(int SocietyId, int FlatId, string MobileNo, string IDNumber)
        {
            if (SocietyId == 0 && (!String.IsNullOrEmpty(MobileNo) || !String.IsNullOrEmpty(IDNumber)))
                return null;

            var _staffData = _staffMasterRepository.Table.ToList();

            // filter staffmaster data according to SocietyId
            var _societyStaff = _staffMasterRepository.Table.Select(s => s.SocietyStaffLinks).ToList();
            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();

            foreach (var staff in _societyStaff)
            {
                if (staff.Where(st => st.SocietyId == SocietyId).Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.SocietyId == SocietyId).FirstOrDefault().StaffId).FirstOrDefault());
            }

            // filter upper step filtered staffmaster data according to FlatId
            //var _flatStaff = _staffMasterRepository.Table.Select(s => s.FlatStaffLinks).ToList();
            //List<ACS.Core.Domain.Staff.StaffMaster> _staffFlatList = new List<ACS.Core.Domain.Staff.StaffMaster>();

            //foreach (var flatStaff in _flatStaff)
            //{
            //    if (flatStaff.Where(fs => fs.FlatId != FlatId).Any())
            //        _staffFlatList.Add(_staffList.Where(sl => sl.Id == flatStaff.Where(fs => fs.FlatId != FlatId).FirstOrDefault().StaffId).FirstOrDefault());
            //}

            // filter data according to the search parameter ID number and mobile No
            List<ACS.Core.Domain.Staff.StaffMaster> _staffMasterList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            if (!String.IsNullOrEmpty(MobileNo) && !String.IsNullOrEmpty(IDNumber))
            {
                _staffMasterList = _staffList.Where(sl => sl.FlatStaffLinks.Any(fsl => fsl.FlatId != FlatId) && sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo || ssl.Mobile2 == MobileNo) && sl.StaffIDProofLinks.Any(si => si.IDNumber == IDNumber)).ToList();
            }
            else if (!string.IsNullOrEmpty(MobileNo))
            {
                _staffMasterList = _staffList.Where(sl => sl.FlatStaffLinks.Any(fsl => fsl.FlatId != FlatId) && sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo || ssl.Mobile2 == MobileNo)).ToList();
            }
            else if (!string.IsNullOrEmpty(IDNumber))
            {
                _staffMasterList = _staffList.Where(sl => sl.FlatStaffLinks.Any(fsl => fsl.FlatId != FlatId) && sl.StaffIDProofLinks.Any(si => si.IDNumber == IDNumber)).ToList();
            }

            if (_staffMasterList.Count > 0)
                return _staffMasterList;
            else
                return null;
        }

        // get all staff details by flatId
        public IList<ACS.Core.Domain.Staff.StaffMaster> GetStaffMasterByFlatId(int flatId)
        {
            if (flatId == 0)
                return null;

            var _flatStaff = _staffMasterRepository.Table.Select(sm => sm.FlatStaffLinks).ToList();

            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            var _staffData = _staffMasterRepository.Table.ToList();

            foreach (var staff in _flatStaff)
            {
                if (staff.Where(st => st.FlatId == flatId).Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.FlatId == flatId).FirstOrDefault().StaffId).FirstOrDefault());
            }

            if (_staffList.Count > 0)
                return _staffList;
            else
                return null;
        }

        // get staff details basis on staffid
        public ACS.Core.Domain.Staff.StaffMaster GetStaffMasterDetailsById(int staffId)
        {
            if (staffId == 0)
                return null;

            if (_staffMasterRepository.Table.Any(i => i.Id == staffId))
                return _staffMasterRepository.Table.Where(i => i.Id == staffId).FirstOrDefault();
            else
                return null;
        }

        // search StaffMaster according to mobile no and ID number for a society
        public ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffForValidation(int SocietyId, string MobileNo1, string MobileNo2, string IDNumber1, string IDNumber2)
        {
            if (SocietyId == 0 && (!string.IsNullOrEmpty(MobileNo1) || !string.IsNullOrEmpty(MobileNo2) || !string.IsNullOrEmpty(IDNumber1) || !string.IsNullOrEmpty(IDNumber2)))
                return null;

            var _societyStaff = _staffMasterRepository.Table.Select(s => s.SocietyStaffLinks).ToList();

            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            var _staffData = _staffMasterRepository.Table.ToList();

            foreach (var staff in _societyStaff)
            {
                if (staff.Where(st => st.SocietyId == SocietyId).Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.SocietyId == SocietyId).FirstOrDefault().StaffId).FirstOrDefault());
            }

            ACS.Core.Domain.Staff.StaffMaster _staffMasterData = new ACS.Core.Domain.Staff.StaffMaster();

            if (!string.IsNullOrEmpty(MobileNo1) && (_staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo1)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo1)).FirstOrDefault();
            else if (!string.IsNullOrEmpty(MobileNo1) && (_staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile2 == MobileNo1)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile2 == MobileNo1)).FirstOrDefault();
            else if (!string.IsNullOrEmpty(MobileNo2) && (_staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo2)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo2)).FirstOrDefault();
            else if (!string.IsNullOrEmpty(MobileNo2) && (_staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile2 == MobileNo2)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile2 == MobileNo2)).FirstOrDefault();
            else if (!string.IsNullOrEmpty(IDNumber1) && (_staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber1)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber1)).FirstOrDefault();
            else if (!string.IsNullOrEmpty(IDNumber2) && (_staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber2)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber2)).FirstOrDefault();

            if (_staffMasterData != null && _staffMasterData.Id != 0)
                return _staffMasterData;
            else
                return null;
        }

        // insert FlatStaffLink data
        public void insertFlatStaffLink(FlatStaffLink flatStaff)
        {
            if (flatStaff == null)
                throw new ArgumentNullException("flatStaff");

            _flatStaffLinkRepository.Insert(flatStaff);
        }

        // update SocietyStaffLink data
        public void updateSocietyStaffLink(SocietyStaffLink societyStaff)
        {
            if (societyStaff == null)
                throw new ArgumentNullException("societyStaff");

            _societyStaffLinkRepository.Update(societyStaff);
        }

        // insert SocietyStaffValidationDetails data
        public void insertSocietyStaffValidationDetails(SocietyStaffValidationDetails societyStaff)
        {
            if (societyStaff == null)
                throw new ArgumentNullException("societyStaff");

            _societyStaffValidationDetailsRepository.Insert(societyStaff);
        }

        // search StaffMaster according to mobile no for a society
        public ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffByMobile(int SocietyId, string MobileNo)
        {
            if (SocietyId == 0 && !string.IsNullOrEmpty(MobileNo))
                return null;

            var _societyStaff = _staffMasterRepository.Table.Select(s => s.SocietyStaffLinks).ToList();

            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            var _staffData = _staffMasterRepository.Table.ToList();

            foreach (var staff in _societyStaff)
            {
                if (staff.Where(st => st.SocietyId == SocietyId).Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.SocietyId == SocietyId).FirstOrDefault().StaffId).FirstOrDefault());
            }

            ACS.Core.Domain.Staff.StaffMaster _staffMasterData = new ACS.Core.Domain.Staff.StaffMaster();

            if (!string.IsNullOrEmpty(MobileNo) && (_staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo || ssl.Mobile2 == MobileNo)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.SocietyStaffLinks.Any(ssl => ssl.Mobile1 == MobileNo || ssl.Mobile2 == MobileNo)).FirstOrDefault();

            if (_staffMasterData != null && _staffMasterData.Id != 0)
                return _staffMasterData;
            else
                return null;
        }

        // search StaffMaster according to ID number for a society
        public ACS.Core.Domain.Staff.StaffMaster SearchSocietyStaffByIDNumber(int SocietyId, string IDNumber)
        {
            if (SocietyId == 0 && !string.IsNullOrEmpty(IDNumber))
                return null;

            var _societyStaff = _staffMasterRepository.Table.Select(s => s.SocietyStaffLinks).ToList();

            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            var _staffData = _staffMasterRepository.Table.ToList();

            foreach (var staff in _societyStaff)
            {
                if (staff.Where(st => st.SocietyId == SocietyId).Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.SocietyId == SocietyId).FirstOrDefault().StaffId).FirstOrDefault());
            }

            ACS.Core.Domain.Staff.StaffMaster _staffMasterData = new ACS.Core.Domain.Staff.StaffMaster();

            if (!string.IsNullOrEmpty(IDNumber) && (_staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber)).Any()))
                _staffMasterData = _staffList.Where(sl => sl.StaffIDProofLinks.Any(ssl => ssl.IDNumber == IDNumber)).FirstOrDefault();

            if (_staffMasterData != null && _staffMasterData.Id != 0)
                return _staffMasterData;
            else
                return null;
        }

        // get all temporary validated staff details by societyId for Permanent validation by RWA
        public IList<ACS.Core.Domain.Staff.StaffMaster> GetSocietyTemporaryStaffForRWAValidation(int societyId)
        {
            if (societyId == 0)
                return null;

            var _societyStaff = _staffMasterRepository.Table.Select(s => s.SocietyStaffLinks).ToList();

            List<ACS.Core.Domain.Staff.StaffMaster> _staffList = new List<ACS.Core.Domain.Staff.StaffMaster>();
            var _staffData = _staffMasterRepository.Table.ToList();

            foreach (var staff in _societyStaff)
            {
                if (staff.Where(st => st.SocietyId == societyId && st.ValidationStatus == "T").Any())
                    _staffList.Add(_staffData.Where(sd => sd.Id == staff.Where(st => st.SocietyId == societyId && st.ValidationStatus == "T").FirstOrDefault().StaffId).FirstOrDefault());
            }

            if (_staffList.Count > 0)
                return _staffList;
            else
                return null;
        }
        #endregion
    }
}
