using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Alert;

namespace ACS.Services.Alert
{
 public partial interface  IServiceApplicationEmailSetup
    {
     string getSubjectByKey(string key);

     string getFromEmailIdByKey(string key);

     string getEmailToIdByKey(string key);

     string getEmailCCToIdByKey(string key);

     string getEmailBCCToIdByKey(string key);

     string getMailDescriptionByKey(string key);

     void InsertAlertScheduler(AlertSchedulerMaster Scheduler);
   
    }
}
