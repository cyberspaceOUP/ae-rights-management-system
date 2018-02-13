using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Master;

namespace SLV.Web.Areas.Master.Controllers
{
    public class GeographicalMasterController : Controller
    {
        //Added by Ankush on 13/07/2016 Geographical Master

        #region PrivateProperty
        private readonly IWorkContext _workContext;
        #endregion

        #region Constructor
        public GeographicalMasterController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
         #endregion

         #region Methods
        //Added by Ankush on 13/07/2016 Country Master
        public ActionResult CountryMaster()
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

        //Added by Ankush on 13/07/2016 State Master 
        public ActionResult StateMaster()
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

        //Added by Ankush on 14/07/2016 City Master
        public ActionResult CityMaster ()
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                //Added By Ankush Dated 22/09/2016
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower() ;
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