using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IExecutive
    {

        /// <summary>
        /// check Duplicity
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>

        String DuplicityCheck(ExecutiveMaster Executive);


        String DuplicityExecutiveCodeCheck(ExecutiveMaster Executive);

        /// <summary>
        /// Insert Division 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        int InsertExecutive(ExecutiveMaster Executive);

        /// <summary>
        /// Get Executive
        /// </summary>
        /// <param name="Geography city">Executive as object</param>
        /// <returns>Executive</returns>
       ExecutiveMaster  GetExecutiveById(int  Id);

       /// <summary>
       /// Get Repoting
       /// </summary>
       /// <param name="Geography city">Repoting as object</param>
       /// <returns>Repoting</returns>
       IList<ExecutiveReporting> GetRepotingListById(int Id);

        /// <summary>
        /// Update Division 
        /// </summary>
        /// <param name="Division">Division class object</param>
        /// <returns></returns>
        void UpdateExecutive(ExecutiveMaster Executive);


        /// <summary>
        /// Delete Division 
        /// </summary>
        /// <param name="Division">Delete Division Object</param>
        /// <returns></returns>
        void DeleteExecutive(ExecutiveMaster Executive);

        //Added by sanjeet singh

        /// <summary>
        /// Insert Login History 
        /// </summary>
        /// <returns></returns>
        
        void InsertLoginHistory(LoginHistory loginHistory);

        /// <summary>
        /// Get Login History 
        /// </summary>
        /// <param name="Geography city">Division as object</param>
        /// <returns>Division</returns>  
        LoginHistory GetLoginHistoryByUserName(LoginHistory loginHistory);

        /// <summary>
        /// Update Login History  
       /// </summary>
       /// <returns></returns>
       void UpdateLoginHistory(LoginHistory loginHistory);

        /// <summary>
       /// Insert ExecutiveLogin History 
       /// </summary>
      /// <returns></returns>
       /// 
       void InsertExecutiveLoginHistory(ExecutiveLoginHistory exloginHistory);

       /// <summary>
       /// check Login History  Duplicity
       /// </summary>
       /// <returns></returns>

      string LoginHistoryCheck(LoginHistory loginHistory);

       /// <summary>
      /// check ExecutiveLogin History  Duplicity
       /// </summary>
      /// <returns></returns>
       string ExecutiveLoginHistoryCheck(ExecutiveLoginHistory executiveLogin);

       /// <summary>
       /// Get ExecutiveLogin History 
      /// </summary>
       ExecutiveLoginHistory GetExecutiveHistoryByUserName(string executiveLogin);

        /// <summary>
       /// Update ExecutiveLogin History 
       /// </summary>
       /// <returns></returns>
       void UpdateExecutiveLoginHistory(ExecutiveLoginHistory executiveLogin);
        //

       /// <summary>
       /// Get Division
       /// </summary>
       /// <param name="Geography city">Division as object</param>
       /// <returns>Division</returns>
       IList<ExecutiveMaster> getExecutiveListBasedonDepartment(ExecutiveMaster Executive);

       /// <summary>
       /// Executive Role List
       /// </summary>
       /// <param name="Geography city">Executive Role List</param>
       /// <returns>Division</returns>
       IList<ExecutiveRoleMaster> getExecutiveRole();

       /// <summary>
       /// get all manager list department based
       /// </summary>
       /// <param name="Geography city">Executive Role List</param>
       /// <returns>Division</returns>
       IList<ExecutiveMaster> getManagerList(int deptId);

       /// <summary>
       /// Insert Executive Reporting History 
       /// </summary>
       /// <returns></returns>
       /// 
       void InsertExecutiveReporting(ExecutiveReporting Executive);

       /// <summary>
       /// Update Executive Reporting History 
       /// </summary>
       /// <returns></returns>
       /// 
       void UpdateExecutiveReporting(ExecutiveReporting Executive);

       /// <summary>
       /// DeavtivateReporting executive reporting
       /// </summary>
       /// <returns></returns>
       /// 

       void DeavtivateExecutiveReporting(int id, int enteredBy);

        /// <summary>
       /// Deavtivate executive division linking
       /// </summary>
       /// <returns></returns>
       /// 

       ExecutiveReporting getExecutiveReporting(int id);
        /// <summary>
       /// Deavtivate executive division linking
       /// </summary>
       /// <returns></returns>
       /// 


       void DeactivateExecutiveDivisionLinking(int LinkingId, int enteredBy);

       /// <summary>
       /// Insert Executive division linking
       /// </summary>
       /// <returns></returns>
       /// 
        
        IList<ExecutiveDivisionLink> getDivisionLinking(int id);
       /// <summary>
       /// Deavtivate executive division linking
       /// </summary>
       /// <returns></returns>
       /// 


       void InsertExecutiveDivisionLinking(ExecutiveDivisionLink ExecutiveLink);

       /// <summary>
       /// Get value from application setup
       /// </summary>
       /// <returns></returns>
       /// 
       string KeyValue(string KeyValue);

    }
   
}
