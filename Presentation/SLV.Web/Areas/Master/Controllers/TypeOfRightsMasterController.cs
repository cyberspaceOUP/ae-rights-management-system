using ACS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLV.Web.Areas.Master.Controllers
{
    public class TypeOfRightsMasterController : Controller
    {
        #region PrivateProperty
        private readonly IWorkContext _workContext; 
        #endregion

        #region Constructor
        public TypeOfRightsMasterController(IWorkContext workContext)
        {
            _workContext = workContext;
        } 
        #endregion

        #region Methods
        /// <summary>
        /// Method to get TypeOfRightsMaster
        /// </summary>
        /// <returns></returns>
        public ActionResult TypeOfRightsMaster()
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        } 
        #endregion
    }
}