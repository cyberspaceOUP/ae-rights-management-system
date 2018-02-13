using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Master;

namespace SLV.Web.Areas.Master.Controllers
{
    //Added By Ankush 14/07/2016
    public class LanguageMasterController : Controller
    {
        #region PrivateProperty
        private readonly IWorkContext _workContext;
        #endregion

        #region Constructor
        public LanguageMasterController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
         #endregion

         #region Methods
        //Added by Ankush on 11/07/2016 Subsidiary Rights Master
        public ActionResult LanguageMaster()
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