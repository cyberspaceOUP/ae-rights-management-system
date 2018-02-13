
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ACS.Core;
using ACS.Core.Caching;
using ACS.Core.Configuration;
using ACS.Core.Data;
using ACS.Core.Domain.Configuration;
using ACS.Core.Domain.Alert;
using ACS.Core.Domain.Master;
namespace ACS.Services.Alert
{

    public partial class ServiceApplicationEmailSetup : IServiceApplicationEmailSetup
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string SETTINGS_ALL_KEY = "ACS.setting.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string SETTINGS_PATTERN_KEY = "ACS.setting.";

        #endregion

        #region Fields

        private readonly IRepository<ApplicationEmailSetup> _ApplicationEmailSetup;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        private readonly IRepository<AlertSchedulerMaster> _AlertSchedulerMaster;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="settingRepository">Setting repository</param>
        public ServiceApplicationEmailSetup
            (
            IRepository<ApplicationEmailSetup> ApplicationEmailSetup
            , IRepository<ApplicationSetUp> ApplicationSetUp
             , IRepository<AlertSchedulerMaster> AlertSchedulerMaster
            )
        {
            this._ApplicationEmailSetup = ApplicationEmailSetup;
            this._ApplicationSetUp = ApplicationSetUp;
            this._AlertSchedulerMaster = AlertSchedulerMaster;

        }

        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get Subjedct Value By Key
         */
        public string getSubjectByKey(string key)
        {



            return _ApplicationEmailSetup.Table.Where(x => x.Deactivate == "N" && x.EmailType.ToLower() == key.ToLower())
                                            .Select(x => x.subject).FirstOrDefault();
        }
        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get EmailTo Value By Key
         */
        public string getEmailToIdByKey(string key)
        {



            return _ApplicationEmailSetup.Table.Where(x => x.Deactivate == "N" && x.EmailType.ToLower() == key.ToLower())
                                            .Select(x => x.EmailTo).FirstOrDefault();
        }
        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get EmailCCTo Value By Key
         */
        public string getEmailCCToIdByKey(string key)
        {



            return _ApplicationEmailSetup.Table.Where(x => x.Deactivate == "N" && x.EmailType.ToLower() == key.ToLower())
                                            .Select(x => x.EmailCCTo).FirstOrDefault();
        }
        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get EmailCCTo Value By Key
         */
        public string getEmailBCCToIdByKey(string key)
        {



            return _ApplicationEmailSetup.Table.Where(x => x.Deactivate == "N" && x.EmailType.ToLower() == key.ToLower())
                                            .Select(x => x.EmailBCCTo).FirstOrDefault();
        }
        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get FromEmailId Value By Key
         */
        public string getFromEmailIdByKey(string key)
        {



            return _ApplicationSetUp.Table.Where(x => x.Deactivate == "N" && x.key.ToLower() == key.ToLower())
                                            .Select(x => x.keyValue).FirstOrDefault();
        }
        #endregion

        #region "get Value By Key"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get Mail Description Value By Key
         */
        public string getMailDescriptionByKey(string key)
        {



            return _ApplicationSetUp.Table.Where(x => x.Deactivate == "N" && x.key.ToLower() == key.ToLower())
                                            .Select(x => x.keyValue).FirstOrDefault();
        }
        #endregion

        public void InsertAlertScheduler(AlertSchedulerMaster Scheduler)
        {
            Scheduler.Deactivate = "N";
            Scheduler.EnteredBy = 10;
            Scheduler.EntryDate = DateTime.Now;
            Scheduler.ModifiedBy = null;
            Scheduler.ModifiedDate = null;
            Scheduler.DeactivateBy = null;
            Scheduler.DeactivateDate = null;
            _AlertSchedulerMaster.Insert(Scheduler);
        }

    }
}
