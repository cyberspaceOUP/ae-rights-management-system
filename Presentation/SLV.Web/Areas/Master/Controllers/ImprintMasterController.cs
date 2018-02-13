using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;

namespace SLV.Web.Areas.Master.Controllers
{
    //Added By Ankush on 15/07/2016
    public class ImprintMasterController : Controller
    {
        #region PrivateProperty
        private readonly IWorkContext _workContext; 
        #endregion

        #region Constructor
        public ImprintMasterController(IWorkContext workContext)
        {
            _workContext = workContext;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to get ImprintMasterController
        /// </summary>
        /// <returns></returns>
        public ActionResult ImprintMaster()
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                //Added By Ankush Dated 22/09/2016
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }
        } 
        #endregion
	}
}