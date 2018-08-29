using ACS.Core;
using ACS.Services.Authentication;
using ACS.Services.Contact;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Services.Product;
using ACS.Services.Security;
using ACS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Data;
using ACS.Core;
using SLV.Model.Common;
using ACS.Services.AuthorContract;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Services.User;
using System.Text;
using ACS.Services.Alert;
using ACS.Core.Domain.Alert;
using Logger;

namespace SLV.API.Controllers.Alert
{
    public class SchedulerController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IDbContext _dbContext;
        private readonly IServiceApplicationEmailSetup _IServiceApplicationEmailSetup;

        public SchedulerController(
                 IDbContext dbContext
            , IServiceApplicationEmailSetup IServiceApplicationEmailSetup
            )
        {
            this._dbContext = dbContext;
            this._IServiceApplicationEmailSetup = IServiceApplicationEmailSetup;
        }


        /*
          * Added by     : Prakash
          * Added Date   : 30 Sep, 2017    
       */
        public IHttpActionResult unblockISBNset()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "UnblockISBN";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);

                var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.Common.Common>("Proc_unblockISBN_set").ToList();
                return Json("OK");
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "SchedulerController.cs", "unblockISBNset", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "SchedulerController.cs", "unblockISBNset", ex);
                return Json(ex.InnerException.Message);
            }
        }



    }
}