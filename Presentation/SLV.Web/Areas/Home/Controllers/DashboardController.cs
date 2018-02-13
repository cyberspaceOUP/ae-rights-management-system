using ACS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLV.Web.Areas.Home.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IWorkContext _workContext;

        public DashboardController(
           IWorkContext workContext
        )
        {
            _workContext = workContext;
        }

        public ActionResult Dashboard()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                //return new HttpUnauthorizedResult();
                 TempData["From"] = "S";
                 return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                TempData["Dashboard"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View(_workContext.CurrentUser);
            }
           // return View();

            //if (_workContext.CurrentUser == null)
            //{
            //    TempData["From"] = "S";
            //    return RedirectToAction("Login", "Login", new { area = "" });
            //}
            //else
            //{
            //    return View();
            //}
        }
    }
}