using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ACS.Services.Security;

namespace ACS.Services.Master
{
    public partial class Exeutive : IExecutive
    {
        #region Fields
        private readonly IRepository<ExecutiveMaster> _ExecutiveRepository;

        //Added by sanjeet singh
        private readonly IRepository<ACS.Core.Domain.Master.LoginHistory> _loginHistoryRepository;
        private readonly IRepository<ACS.Core.Domain.Master.ExecutiveLoginHistory> _executiveloginRepository;
        private readonly IRepository<ACS.Core.Domain.Master.ExecutiveRoleMaster> _executiveRoleMaster;
        private readonly IRepository<ACS.Core.Domain.Master.ApplicationSetUp> _applicationsetup;
        private readonly IEncryptionService _encryptionService;
        private readonly IDepartmentService _departmentService;
        IRepository<ACS.Core.Domain.Master.ExecutiveReporting> _ExecutiveReporting;
        IRepository<ACS.Core.Domain.Master.ExecutiveDivisionLink> _ExecutiveDivisionLink;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>   
        /// 

        public Exeutive() { }

        public Exeutive(
             IRepository<ExecutiveMaster> ExecutiveRepository
                , IEncryptionService encryptionService
                 , IRepository<ACS.Core.Domain.Master.LoginHistory> loginHistoryRepository
                 , IRepository<ACS.Core.Domain.Master.ExecutiveLoginHistory> executiveloginRepository,
                 IRepository<ACS.Core.Domain.Master.DepartmentMaster> departmentyRepository,
              IDepartmentService departmentService,
            IRepository<ACS.Core.Domain.Master.ExecutiveRoleMaster> executiveRoleMaster,
             IRepository<ACS.Core.Domain.Master.ExecutiveReporting> ExecutiveReporting,
             IRepository<ACS.Core.Domain.Master.ExecutiveDivisionLink> ExecutiveDivisionLink
            , IRepository<ACS.Core.Domain.Master.ApplicationSetUp> applicationsetup
         )
        {
            _ExecutiveRepository = ExecutiveRepository;
            this._encryptionService = encryptionService;
            _loginHistoryRepository = loginHistoryRepository;
            _executiveloginRepository = executiveloginRepository;
            _departmentService = departmentService;
            _executiveRoleMaster = executiveRoleMaster;
            _ExecutiveReporting = ExecutiveReporting;
            _ExecutiveDivisionLink = ExecutiveDivisionLink;
            _applicationsetup = applicationsetup;
        }



        #endregion

        #region Methods


        public string DuplicityCheck(ExecutiveMaster Executive)
        {

            var dupes = _ExecutiveRepository.Table.Where(x => x.executiveName == Executive.executiveName
                                                      && x.executivecode == Executive.executivecode
                                                      && x.Emailid == Executive.Emailid
                                                      && x.Deactivate == "N"
                                                      && (Executive.Id != 0 ? x.Id : 0) != (Executive.Id != 0 ? Executive.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }
        }


        public string DuplicityExecutiveCodeCheck(ExecutiveMaster Executive)
        {

            var dupesCode = _ExecutiveRepository.Table.Where(x => x.executivecode == Executive.executivecode && x.Deactivate == "N"  && (Executive.Id != 0 ? x.Id : 0) != (Executive.Id != 0 ? Executive.Id : 1)).FirstOrDefault();                                          
                                                      

            var dupesEmail = _ExecutiveRepository.Table.Where(x => x.Emailid == Executive.Emailid
                                                    && (Executive.Id != 0 ? x.Id : 0) != (Executive.Id != 0 ? Executive.Id : 1)).FirstOrDefault();
            if (dupesCode != null || dupesEmail !=null)
            {
                return "N";

            }
            else
            {
                return "Y";
            }


        }


        public int InsertExecutive(ExecutiveMaster Executive)
        {

            //Executive.Password = _encryptionService.DecryptText(Executive.Password, KeyValue("encriptionkey"));
            Executive.Deactivate = "N";
            Executive.EntryDate = DateTime.Now;
            Executive.ModifiedBy = null;
            Executive.ModifiedDate = null;
            Executive.DeactivateBy = null;
            Executive.DeactivateDate = null;

            //Added by sanjeet
            Executive.block = "N";
            Executive.PwdChanged = "N";

           _ExecutiveRepository.Insert(Executive);
            return Executive.Id;
        }

        public void InsertExecutiveReporting(ExecutiveReporting Executive)
        {
            Executive.Deactivate = "N";
            Executive.EntryDate = DateTime.Now;
            Executive.ModifiedBy = null;
            Executive.ModifiedDate = null;
            Executive.DeactivateBy = null;
            Executive.DeactivateDate = null;
            _ExecutiveReporting.Insert(Executive);
        }

        public void UpdateExecutiveReporting(ExecutiveReporting Executive)
        {
            Executive.Deactivate = "Y";
            Executive.ModifiedBy = null;
            Executive.ModifiedDate = null;
            Executive.DeactivateBy = Executive.EnteredBy;
            Executive.DeactivateDate = DateTime.Now;
            _ExecutiveReporting.Update(Executive);
        }

        public void InsertExecutiveDivisionLinking(ExecutiveDivisionLink Executive)
        {
            Executive.Deactivate = "N";
            Executive.EntryDate = DateTime.Now;
            Executive.ModifiedBy = null;
            Executive.ModifiedDate = null;
            Executive.DeactivateBy = null;
            Executive.DeactivateDate = null;
            _ExecutiveDivisionLink.Insert(Executive);
        }
       
        public ExecutiveMaster GetExecutiveById(int Id)
        {
            return _ExecutiveRepository.Table.Where(i => i.Id == Id && i.Deactivate == "N").FirstOrDefault();
        }

        public IList<ExecutiveReporting> GetRepotingListById(int Id)
        {
            return _ExecutiveReporting.Table.Where(x => x.Deactivate == "N" && x.reportingidto == Id).ToList();
        }

        public void UpdateExecutive(ExecutiveMaster Executive)
        {
            _ExecutiveRepository.Update(Executive);
        }

        public void DeleteExecutive(ExecutiveMaster Executive)
        {
            _ExecutiveRepository.Delete(Executive);
        }



        //Added by sanjeet singh

        public void InsertLoginHistory(LoginHistory loginHistory)
        {

            loginHistory.UserPassword = _encryptionService.EncryptText(loginHistory.UserPassword, KeyValue("encriptionkey"));
            loginHistory.Deactivate = "N";
            loginHistory.EnteredBy = 10;
            loginHistory.EntryDate = DateTime.Now.Date;
            loginHistory.ModifiedBy = null;
            loginHistory.ModifiedDate = null;
            loginHistory.DeactivateBy = null;
            loginHistory.DeactivateDate = null;
            _loginHistoryRepository.Insert(loginHistory);
        }


        public void InsertExecutiveLoginHistory(ExecutiveLoginHistory exloginHistory)
        {
            exloginHistory.Deactivate = "N";
            //exloginHistory.EnteredBy = 10;
            exloginHistory.EntryDate = DateTime.Now;
            exloginHistory.ModifiedBy = null;
            exloginHistory.ModifiedDate = null;
            exloginHistory.DeactivateBy = null;
            exloginHistory.DeactivateDate = null;
            _executiveloginRepository.Insert(exloginHistory);
        }


        public string LoginHistoryCheck(LoginHistory loginHistory)
        {
            var dupes = _loginHistoryRepository.Table.Where(x => x.UserName == loginHistory.UserName && x.EntryDate == loginHistory.EntryDate
                                                        && (loginHistory.Id != 0 ? x.Id : 0) != (loginHistory.Id != 0 ? loginHistory.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "Y";

            }
            else
            {
                return "N";
            }
        }


        public string ExecutiveLoginHistoryCheck(ExecutiveLoginHistory executiveLogin)
        {
            var dupes = _executiveloginRepository.Table.Where(x => x.ExecutiveUserName == executiveLogin.ExecutiveUserName
                                                        && (executiveLogin.Id != 0 ? x.Id : 0) != (executiveLogin.Id != 0 ? executiveLogin.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                return "Y";

            }
            else
            {
                return "N";
            }
        }


        public void UpdateLoginHistory(LoginHistory loginHistory)
        {
            _loginHistoryRepository.Update(loginHistory);
        }


        public LoginHistory GetLoginHistoryByUserName(LoginHistory loginHistory)
        {
            return _loginHistoryRepository.Table.Where(i => i.UserName == loginHistory.UserName).FirstOrDefault();
        }


        public ExecutiveLoginHistory GetExecutiveHistoryByUserName(string executiveName)
        {
            return _executiveloginRepository.Table.Where(i => i.ExecutiveUserName == executiveName).OrderByDescending(i => i.Id).FirstOrDefault();
        }


        public void UpdateExecutiveLoginHistory(ExecutiveLoginHistory executiveLogin)
        {
            _executiveloginRepository.Update(executiveLogin);
        }

        //Create by : dheeraj kumar sharma
        //Created for: handled by of product according department based 

        public IList<ExecutiveMaster> getExecutiveListBasedonDepartment(ExecutiveMaster Executive)
        {
            DepartmentMaster deptmaster = new DepartmentMaster();
            deptmaster.Id = Executive.DepartmentId;
            string Code = _departmentService.GetDepartmentById(deptmaster).DepartmentCode;
            if (Code == "ED")
            {
                return _ExecutiveRepository.Table.Where(i => i.Id == Executive.Id && i.Deactivate=="N").OrderBy(i => i.executiveName).ToList();
            }
            else if (Code == "RT")
            {
                return _ExecutiveRepository.Table.Where(i => i.DepartmentId == deptmaster.Id && i.Deactivate=="N").OrderBy(i => i.executiveName).ToList();
            }
            else
            {
                return  _ExecutiveRepository.Table.Where(i=>i.Deactivate=="N").OrderBy(i => i.executiveName).ToList();
            }


        }
        public IList<ExecutiveRoleMaster> getExecutiveRole()
        {
            return _executiveRoleMaster.Table.Where(i => i.Deactivate.ToLower() == "n").OrderBy(i => i.Role).ToList();
        }

        public IList<ExecutiveMaster> getManagerList(int deptId)
        {
            return _ExecutiveRepository.Table.Where(s => s.DepartmentId == deptId && s.Deactivate.ToLower() == "n" && s.block.ToLower() == "n" &&
                                                !_executiveRoleMaster.Table.Where(i => i.Id == s.Id).Any()).ToList();

        }
        //



       public  string KeyValue(string mstrKey)
        {
            try
            {
                //return _applicationsetup.Table.Where(i => i.key == KeyValue).Select(i => new
                //{
                //    key = i.keyValue
                //}).ToString();

                return _applicationsetup.Table.Where(i => i.key.ToLower() == mstrKey.ToLower()).Select(e => e.keyValue).FirstOrDefault();
            } 
           catch (Exception ex)
            {
                return null;
            }
          
        }

       
       public  ExecutiveReporting getExecutiveReporting(int id)
       {
           return _ExecutiveReporting.Table.Where(i => i.executiveid == id && i.Deactivate == "N").FirstOrDefault();
       }
       public void DeavtivateExecutiveReporting(int id, int enteredBy)
       {
           ExecutiveReporting Reporting = getExecutiveReporting(id);
           if (Reporting!=null)
           {
               Reporting.Deactivate = "Y";
               Reporting.DeactivateDate = DateTime.Now;
               Reporting.DeactivateBy = enteredBy;
               _ExecutiveReporting.Update(Reporting);
           }
           
       }
       public IList<ExecutiveDivisionLink> getDivisionLinking(int id)
       {
           return _ExecutiveDivisionLink.Table.Where(i => i.executiveid == id && i.Deactivate == "N").ToList();
       }
       public void DeactivateExecutiveDivisionLinking(int id, int enteredBy)
       {
            IList<ExecutiveDivisionLink> Linking = getDivisionLinking(id);
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.EnteredBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _ExecutiveDivisionLink.Update(lst);
            }
       }

        #endregion

       public IList<ExecutiveLoginHistory> GetExecutiveHistoryList(string executiveName)
       {
           return _executiveloginRepository.Table.Where(i => i.ExecutiveUserName == executiveName).ToList();
       }

    }
}
