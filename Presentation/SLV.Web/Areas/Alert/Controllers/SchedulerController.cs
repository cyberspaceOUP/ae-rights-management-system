using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;

namespace SLV.Web.Areas.Alert.Controllers
{
    public class SchedulerController : Controller
    {
        private readonly IWorkContext _workContext;

        public SchedulerController(IWorkContext workContext)
        {
            _workContext = workContext;
        }


        // GET: /Alert/Scheduler/
        public ActionResult UnblockISBN()
        {
            return View();
        }


	}
}