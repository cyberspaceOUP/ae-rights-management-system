using System.Web.Mvc;
using ACS.Core.Domain.Common;

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



using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Xml;
using System.Data.OleDb;

using System.Web.Mvc;
using ACS.Core.Domain.Common;

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


namespace SLV.Web.Controllers
{
    public partial class CommonController : BasePublicController
    {
        private readonly CommonSettings _commonSettings;

        public CommonController(CommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
        }

        #region Methods

        //page not found
        public ActionResult PageNotFound()
        {
            this.Response.StatusCode = 404;
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }

        //page not found
        public ActionResult GenericError()
        {
            this.Response.StatusCode = 400; //Bad Request
            this.Response.TrySkipIisCustomErrors = true;

            return View();
        }

        //footer
        [ChildActionOnly]
        public ActionResult JavaScriptDisabledWarning()
        {
            if (!_commonSettings.DisplayJavaScriptDisabledWarning)
                return Content("");

            return PartialView();
        }

        public ActionResult GenericUrl()
        {
            //seems that no entity was found
            return InvokeHttp404();
        }
        [HttpPost]
        public ActionResult upload(HttpPostedFileBase files)
        {
         
            string extension = System.IO.Path.GetExtension(files.FileName);

            string folderName = string.Empty;
            folderName = Request.Form["hid_foldername"];

            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
            var newUrl = "";
            //Added by dheeraj kumar sharma
            if(folderName!=null)
            {
                newUrl = Url.Content(Path.Combine("~/uploads/" + filename + "", Path.GetFileName(filename)));
            }
            else
            {
                 newUrl = Url.Content(Path.Combine("~/uploads", Path.GetFileName(filename)));
            }
           //end by dheeraj kumar sharma

            files.SaveAs(Server.MapPath(newUrl));

            return Content(filename);
        }
        

        public ActionResult deletedocument(string filename)
        {
            string msg = String.Empty;
            try
            {
                var CompleteUrl = Url.Content(Path.Combine("~/uploads", filename));
                if (System.IO.File.Exists(Server.MapPath(CompleteUrl)))
                {
                    System.IO.File.Delete(Server.MapPath(CompleteUrl));
                }

            }
            catch (ACSException ex)
            {
                msg = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                msg = ex.InnerException.Message;
            }

            return Json("Deleted");

        }




        [HttpPost]
        public ActionResult uploadExcel(HttpPostedFileBase files)
        {

            string extension = System.IO.Path.GetExtension(files.FileName);
            string folderName = string.Empty;
            folderName = Request.Form["hid_foldername"];

            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + extension;
            var newUrl = "";
            //Added by dheeraj kumar sharma
            if (folderName != null)
            {
                newUrl = Url.Content(Path.Combine("~/uploads/" + filename + "", Path.GetFileName(filename)));
            }
            else
            {
                newUrl = Url.Content(Path.Combine("~/uploads", Path.GetFileName(filename)));
            }
            //end by dheeraj kumar sharma

            files.SaveAs(Server.MapPath(newUrl));



            return Json(filename);


       
        }
        #endregion
    }
}