using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;

namespace SLV.Web.Areas.PaymentTagging.Controllers
{
    public class PaymentTaggingController : Controller
    {

        private readonly IWorkContext _workContext;

        public PaymentTaggingController(
            IWorkContext workContext
         )
        {
            _workContext = workContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /PaymentTagging/PaymentTagging/
        public ActionResult PaymentTaggingSearch(string For="", string From = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (For != null)
                {
                    TempData["For"] = For;
                }
                if (From == "BackToSearch")
                {
                    TempData["BackToSearch"] = From.ToLower();
                }
                if (From.ToLower() == "dashboard")
                {
                    TempData["Dashboard"] = From.ToLower();
                }
                return View("PaymentTaggingSearch");
            }
        }

        public ActionResult PaymentTaggingList(string For = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (For != "")
                {
                    TempData["For"] = For.ToLower();
                }
                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View("PaymentTaggingList");
            }
        }


	}
}