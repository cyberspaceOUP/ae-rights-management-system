using ACS.Core;
using ACS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.Model.Common;

using ACS.Core.Infrastructure;
using System.Web.Http.Description;
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Services.User;
using System.Text;
using System.Web.Mvc;


namespace SLV.Web.Areas.Contract.Controllers
{
    public class OtherContractController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IDbContext _dbContext;

        public OtherContractController(IWorkContext workContext, IEncryptionService encryptionService, IDbContext dbContext)
        {
            _workContext = workContext;
            _encryptionService = encryptionService;
            _dbContext = dbContext;
        }

        public ActionResult OtherContractEntry()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
              //  return new HttpUnauthorizedResult();
            }
            else
            {
                //Added By Ankush Dated 22/09/2016
                 var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                 if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa" && obj_department.ToString() != "fc")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                TempData["DepartmentId"] = _workContext.CurrentUser.Id;
                TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                return View(_workContext.CurrentUser);
            }
            //return View();
        }


        public ActionResult OtherContractView()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
                //  return new HttpUnauthorizedResult();
            }
            else
            {
                    TempData["DepartmentId"] = _workContext.CurrentUser.Id;
                    TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["View"] = "View";

                    return View("OtherContractEntry", _workContext.CurrentUser);
            }
            //return View();
        }



        public ActionResult OtherContractSearch(string For, string TopSearch ,string ViewMore = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
                //  return new HttpUnauthorizedResult();
            }
            else
            {
                    if (For != null)
                    {
                        if (For == "Report")
                        {
                            TempData["Report"] = For;
                        }
                        if (For.ToLower() == "report")
                        {
                            ViewBag.Report = For.ToLower();
                        }
                        else if (For == "BackToSearch")
                        {
                            TempData["BackToSearch"] = For.ToLower();
                        }

                        else if (For.ToLower() == "view")
                        {
                            TempData["Action"] = For.ToLower();
                        }
                        else if (For.ToLower() == "update")
                        {
                            TempData["Action"] = For.ToLower();
                        }
                        else if (For.ToLower() == "delete")
                        {
                            TempData["Action"] = For.ToLower();
                        }
                    }
                    if (TopSearch != null)
                    {
                        TempData["TopSearch"] = TopSearch;
                    }
                    if (ViewMore != "")
                    {
                        ViewBag.ViewMore = ViewMore;
                    }

                    TempData["DepartmentId"] = _workContext.CurrentUser.Id;
                    TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                    return View(_workContext.CurrentUser);
            }
            //return View();
        }


        public ActionResult AutoComplete()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
              //  return new HttpUnauthorizedResult();
            }
            else
            {
                return View(_workContext.CurrentUser);
            }
            //return View();
        }




        public IList<OtherContractDetailsModel> getOtherContractList(String SessionId)
        {

            try
            {
                if (SessionId == "")
                {
                    return null;
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SessionId + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<OtherContractDetailsModel>("Proc_OtherContractSerch_get", parameters).ToList();
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public OtherContractDetailsModel GetOtherContractExcelParameterList(String SessionId)
        {
            try
            {
                if (SessionId == "")
                {
                    return null;
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SessionId + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<OtherContractDetailsModel>("Proc_OtherContractSerchParametter_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public ActionResult exportToExcelOtherContractList(OtherContractDetailsModel _mobjOtherContract)
        {
            try
            {


               


            
                List<OtherContractDetailsModel> _mobjReportList = new List<OtherContractDetailsModel>();
               

                OtherContractDetailsModel _mobjParametertList = new OtherContractDetailsModel();
                _mobjReportList = getOtherContractList(_mobjOtherContract.SessionId).ToList();

                _mobjParametertList = GetOtherContractExcelParameterList(_mobjOtherContract.SessionId);

               
               

               

                string sFileName = "OtherContractReport_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Other Contract Report</b>" + "</td>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</tr>");
                    if (!String.IsNullOrEmpty(_mobjParametertList.ReturnList))
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + _mobjParametertList.ReturnList + "</td>");
                        mstr_searchparameter.Append("</tr>");
                    }
                   

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
                    mstr_searchparameter.Append("<td ><b>Contract Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>Contract Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Contract Type</b></td>");
                    mstr_searchparameter.Append("<td><b>Expiry Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Vendor Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Nature of Work</b></td>");


                    mstr_searchparameter.Append("<td ><b> Service</b></td>");
                    mstr_searchparameter.Append("<td ><b> Sub Service</b></td>");
                    mstr_searchparameter.Append("<td ><b> Address</b></td>");

                    mstr_searchparameter.Append("<td ><b>Country</b></td>");
                    mstr_searchparameter.Append("<td ><b> State</b></td>");
                    mstr_searchparameter.Append("<td ><b> City</b></td>");
                    mstr_searchparameter.Append("<td ><b> PIN Code</b></td>");
                    mstr_searchparameter.Append("<td ><b> Mobile</b></td>");
                    mstr_searchparameter.Append("<td ><b> Email</b></td>");
                    mstr_searchparameter.Append("<td ><b> PANNo</b></td>");
                    mstr_searchparameter.Append("<td ><b> Request Date</b></td>");
                    mstr_searchparameter.Append("<td ><b> Project Title</b></td>");
                    mstr_searchparameter.Append("<td ><b> Project ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b> Territory</b></td>");

                    mstr_searchparameter.Append("<td ><b> Payment </b></td>");
                    mstr_searchparameter.Append("<td ><b> Payment Type </b></td>");
                 
                    mstr_searchparameter.Append("<td ><b> Enter By </b></td>");
                    mstr_searchparameter.Append("<td ><b> Remarks  </b></td>");
                    mstr_searchparameter.Append("<td ><b>Print Run Quantity  </b></td>");
                    mstr_searchparameter.Append("<td ><b>Print Rights  </b></td>");
                    mstr_searchparameter.Append("<td ><b> Electronic Rights </b></td>");
                    mstr_searchparameter.Append("<td ><b> E-book Rights </b></td>");
                    mstr_searchparameter.Append("<td ><b>Cost  </b></td>");
                    mstr_searchparameter.Append("<td ><b>Currency  </b></td>");
                    mstr_searchparameter.Append("<td ><b> Restriction  </b></td>");



                    mstr_searchparameter.Append("<td ><b> Cancellation Reason</b></td>");
                    mstr_searchparameter.Append("<td ><b>Contract Status </b></td>");
                    mstr_searchparameter.Append("<td ><b>Pending Remarks </b></td>");
                    mstr_searchparameter.Append("<td ><b> Agreement Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Effective Date </b></td>");
                    mstr_searchparameter.Append("<td ><b> Contract Period In Month </b></td>");
                    mstr_searchparameter.Append("<td ><b> Signed Contract Sent Date</b></td>");
                    mstr_searchparameter.Append("<td ><b> Signed Contract Received Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Cancellation Date </b></td>");
                  



                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (OtherContractDetailsModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Othercontractcode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.contractdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.contractname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Expirydate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.partyname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NatureOfWork + "</td>");



                        mstr_searchparameter.Append("<td align='left'>" + data.Service + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SubService + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OtherContractCountry + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OtherContractState + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OtherContractCity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PANNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.requestdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProjectTitle + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProjectISBN + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.territoryrights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Payment + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.paymenttype + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.executivename + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Printrunquantity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PrintRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.electronicrights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ebookrights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.cost + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.currencyname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.restriction + "</td>");
                       







                        mstr_searchparameter.Append("<td align='left'>" + data.Cancellation_Reason + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Status + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pending_Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AgreementDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Effectivedate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contractperiodinmonth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SignedContractSentDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SignedContractReceived_Date + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CancellationDate + "</td>");

                       
                        mstr_searchparameter.Append("</tr>");
                        mint_Counter++;
                    }
                    mstr_searchparameter.Append("</table></td></tr></table>");



                    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                    this.Response.ContentType = "application/excel";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                    return File(buffer, "application/vnd.ms-excel");

                }
            }
            catch (Exception ex)
            {

                return null;
                throw ex;
            }
        }
     
     






    }
}