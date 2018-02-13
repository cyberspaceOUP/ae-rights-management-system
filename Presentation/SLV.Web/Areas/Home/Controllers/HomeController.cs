using ACS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Web.Framework.Controllers;
using ACS.Services.Contact;
using ACS.Services.Authentication;
using SLV.Model.Common;
using ACS.Core;
using ACS.Services.Security;
using System.Web.Helpers;
using System.IO;


namespace SLV.Web.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkContext _workContext;

        public HomeController(
            IWorkContext workContext
         )
        {
            _workContext = workContext;
        }
        
        //
        // GET: /Home/Home/
        public ActionResult Index()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                //Session["UserName"] = _workContext.CurrentUser.executiveName;
                //Session["UserDepartment"] = _workContext.CurrentUser.DepartmentM.DepartmentName;
                //Session["UserDepartment"] = "Rights Department";
                return View(_workContext.CurrentUser);
            }
            //return View();
        }

       

      


	}
}