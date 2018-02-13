using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Master;
namespace SLV.Web.Areas.Product.Controllers
{
    public class SeriesProductEntryController : Controller
    {

          private readonly IWorkContext _workContext;

          public SeriesProductEntryController(
            IWorkContext workContext
         )
        {
            _workContext = workContext;
        }
        //
        // GET: /Product/SeriesProductEntry/
        public ActionResult Index()
        {
            return View();
        }

      

        public ActionResult SeriesProductEntry()
        {
            if (_workContext.CurrentUser == null)
            {
                
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
              
            }
            // return View();
        }

        //[HttpPost]
        //public ActionResult SeriesProductEntry()
        //{
        //    string hid_submittype = Request.Form["hid-submit"];
        //}
	}
}