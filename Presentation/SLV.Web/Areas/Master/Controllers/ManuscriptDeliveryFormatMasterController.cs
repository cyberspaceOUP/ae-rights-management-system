﻿using ACS.Core;
using System.Web.Mvc;

namespace SLV.Web.Areas.Master.Controllers
{
    public class ManuscriptDeliveryFormatMasterController : Controller
    {
        #region PrivateProperty
        private readonly IWorkContext _workContext; 
        #endregion

        #region Constructor
        public ManuscriptDeliveryFormatMasterController(IWorkContext workContext)
        {
            _workContext = workContext;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to get ManuscriptDeliveryFormatMaster URL
        /// </summary>
        /// <returns></returns>
        public ActionResult ManuscriptDeliveryFormatMaster()
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