
using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Data;
using System.Data.SqlClient;
using ACS.Services.Security;
using System.Data;
using System.Web.Mvc;
using System.IO;
using ACS.Web.Framework.Controllers;
using ACS.Services.Contact;
using ACS.Services.Authentication;
using SLV.Model.Common;
using ACS.Core;
using System.Web;
using System;







namespace SLV.Web.Areas.Master.Controllers
{
    public class ISBNBagController : Controller
    {
       private readonly IWorkContext _workContext;
       private readonly IDbContext _dbContext;
       

        //public MasterController(IWorkContext workContext)
        //{
        //    _workContext = workContext;
        //        ViewBag.enteredBy = _workContext.CurrentUser.Id;
        //}

       public ISBNBagController(
            IWorkContext workContext,
           IDbContext dbContext
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }
        
        
        //
        // GET: /Master/ISBNBag/
        public ActionResult ISBNBag()
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



          //  return View();
            
        }


        public ActionResult ISBNBagSearch()
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


          //  return View();

        }
        //[HttpPost]
        //public ActionResult Upload(HttpPostedFileBase file)
        //{
        //    //HttpPostedFileBase file = Request.Files["UploadFile"];
        //    if (Request != null)
        //    {

        //    }
        //    return View("ISBNBag");

        //}





        //Added by Ankush on 29/08/2016 ISbnList Excel
        #region ISbnList Excel

        public IList<ISBNBagModel> GetAllISbnList()
        {

            var mvarISbnList = _dbContext.ExecuteStoredProcedureListNewData<ISBNBagModel>("Proc_ISBNBag_get").ToList();
            return mvarISbnList;
        }


        public ActionResult exportToExcelISbnList()
        {
            try
            {
                var _mobjReportList = GetAllISbnList();

                string SearchParameter = string.Empty;


                StringBuilder mstr_searchparameter = new StringBuilder();
                mstr_searchparameter.Append("<table width='100%'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>ISBN List</b>" + "</td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>List Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                mstr_searchparameter.Append("</tr>");


                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                mstr_searchparameter.Append("</tr>");

                mstr_searchparameter.Append("</table>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td colspan='2'>");
                mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                mstr_searchparameter.Append("<tr>");
                mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                mstr_searchparameter.Append("<td ><b>Product Type</b></td>");
                mstr_searchparameter.Append("<td ><b>Status</b></td>");
                mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                mstr_searchparameter.Append("<td ><b>Product Name</b></td>");

                mstr_searchparameter.Append("</tr>");
                mstr_searchparameter.Append("</td>");
                int mint_Counter = 1;
                foreach (var data in _mobjReportList)
                {
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ISBN + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ProductType + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.Status + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                    mstr_searchparameter.Append("<td align='left'>" + data.FinalProductName + "</td>");

                    mstr_searchparameter.Append("</tr>");
                    mint_Counter++;
                }

                mstr_searchparameter.Append("</table></td></tr></table>");

                string sFileName = "IsbnList_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";

                HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                this.Response.ContentType = "application/excel";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                return File(buffer, "application/vnd.ms-excel");
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }

        #endregion ISbnList Excel
       //Ended by Ankush

    }
}