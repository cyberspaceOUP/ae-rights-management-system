//Create by Saddam on 27/07/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ACS.Core;
using ACS.Services.Master;


using System.Data;
using System.Data.SqlClient;

using SLV.Model.PermissionInboundModel;

using System.Text;
using ACS.Core.Data;
using ACS.Data;
using Logger;

namespace SLV.Web.Areas.PermissionsInbound.Controllers
{
    public class PermissionsInboundController : Controller
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;

        public PermissionsInboundController(
            IWorkContext workContext
            , IDbContext dbContext
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }

        //
        // GET: /PermissionsInbound/PermissionsInbound/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionsInboundMaster(int? Id, string type, int? InboundId)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() == "ed")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    //TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }
                
                if (InboundId != null)
                {
                    TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["InboundId"] = InboundId;
                }


                return View("PermissionsInboundMaster");
            }
            // return View();
        }
        public ActionResult AddCopyrightHolder(string code)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.code = code;
                return View();
            }
            // return View();
        }
        public ActionResult Search(string For, string Type, string Data = "")
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
                    if (For == "BackToSearch")
                    {
                        TempData["BackToSearch"] = For;
                    }
                    if (For.ToLower() == "report")
                    {
                        ViewBag.Report = For.ToLower();
                    }
                    else
                    {
                        TempData["Action"] = For.ToLower();
                    }
                }

                if (Type != null)
                {
                    if (Type == "Update")
                    {
                        TempData["Type"] = Type;
                    }
                    else if (Type == "View")
                    {
                        TempData["Type"] = Type;
                    }

                    else if (Type == "CopyrightHolder")
                    {
                        TempData["Type"] = Type;
                    }
                    else if (Type.ToLower() == "delete")
                    {
                        TempData["Type"] = Type;
                    }

                }

                if (Data != "")
                {
                    TempData["Data"] = Data.ToLower();
                }

                TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }
            // return View();
        }

        public ActionResult ViewInbound(int? Id, string type, string InboundId, string For = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    //TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);

                }
                
                if (For != "")
                {
                    TempData["For"] = For.ToLower();
                }

                if (InboundId != null)
                {
                    TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["InboundId"] = InboundId;
                }


                ViewBag.Id = Id;
                return View("ViewInbound");
            }
            // return View();
        }
        
        public ActionResult UpdateInbound(int? Id, string type, string InboundId)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() == "ed")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }

                if (InboundId != null)
                {
                    //TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    if (_workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower() == "rt")
                        TempData["User"] = "ad";
                    else if (_workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower() == "sa")
                         TempData["User"] = "sa";
                    else
                        TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                    TempData["InboundId"] = InboundId;
                }


                return View();
            }
            // return View();
        }

        public IList<PermissionInboundSearchModel> GetPermissionsInboundExcelList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionInboundSearchModel>("Proc_getInboundPermissionResultExcel_get", parameters).ToList();
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public PermissionInboundSearchModel GetPermissionsInboundExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionInboundSearchModel>("Proc_getInboundPermissionParametterResult_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult exportToExcelPermissionsInboundList(String SessionId)
        {
            try
            {
                List<PermissionInboundSearchModel> _mobjReportList = new List<PermissionInboundSearchModel>();
                PermissionInboundSearchModel _mobjParametertList = new PermissionInboundSearchModel();
                
                _mobjReportList = GetPermissionsInboundExcelList(SessionId).ToList();

                _mobjParametertList = GetPermissionsInboundExcelParameterList(SessionId);
                
                string sFileName = "PermissionsInbound_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Permissions In-bound Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Permission Inbound Code</b></td>");
                    //mstr_searchparameter.Append("<td ><b>License Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                    //mstr_searchparameter.Append("<td ><b>Inbound Type </b></td>");
                    mstr_searchparameter.Append("<td ><b> Contract Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>Image Id </b></td>");
                    mstr_searchparameter.Append("<td ><b>Description </b></td>");
                    mstr_searchparameter.Append("<td ><b> Invoice Number</b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Value </b></td>");
                    mstr_searchparameter.Append("<td ><b> Invoice Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Print Quantity </b></td>");
                    mstr_searchparameter.Append("<td ><b>Permission Expirydate </b></td>");
                    mstr_searchparameter.Append("<td ><b> web-Link</b></td>");

                    mstr_searchparameter.Append("<td ><b>Credit Line </b></td>");
                    mstr_searchparameter.Append("<td ><b>Remarks </b></td>");
                    mstr_searchparameter.Append("<td ><b> Usage</b></td>");
                    mstr_searchparameter.Append("<td ><b>Vendor Name </b></td>");
                    mstr_searchparameter.Append("<td ><b>Currency </b></td>");
                    mstr_searchparameter.Append("<td ><b>Editorial only </b></td>");
                    mstr_searchparameter.Append("<td ><b>Copy Right Holder </b></td>");
                    mstr_searchparameter.Append("<td ><b>Address </b></td>");
                    mstr_searchparameter.Append("<td ><b>Contact Person </b></td>");
                    mstr_searchparameter.Append("<td ><b> Telephone No. </b></td>");
                    mstr_searchparameter.Append("<td ><b>Status </b></td>");
                    mstr_searchparameter.Append("<td ><b>Asset Sub-Type </b></td>");
                    mstr_searchparameter.Append("<td ><b>Assest Description </b></td>");
                    mstr_searchparameter.Append("<td ><b> Restriction</b></td>");
                    mstr_searchparameter.Append("<td ><b>Run Granted Qty </b></td>");
                    mstr_searchparameter.Append("<td ><b>Sub-Licensing </b></td>");
                    mstr_searchparameter.Append("<td ><b> Fee</b></td>");
                    mstr_searchparameter.Append("<td ><b>Copyright holder  Currency </b></td>");
                    mstr_searchparameter.Append("<td ><b> Territory</b></td>");

                    mstr_searchparameter.Append("<td ><b>Extent </b></td>");
                    mstr_searchparameter.Append("<td ><b> Gratis copies to be sent </b></td>");
                    mstr_searchparameter.Append("<td ><b>Number of copies </b></td>");
                    mstr_searchparameter.Append("<td ><b>Original Source </b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Number </b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Value </b></td>");
                    mstr_searchparameter.Append("<td ><b>Permission Expiry Date </b></td>");
                    mstr_searchparameter.Append("<td ><b> Acknowledgement Line</b></td>");
                    mstr_searchparameter.Append("<td ><b> Remarks</b></td>");
                    mstr_searchparameter.Append("<td ><b>Date Requst Details </b></td>");
                   
                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (PermissionInboundSearchModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Code + "</td>");
                        //mstr_searchparameter.Append("<td align='left'>" + data.TypeCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + (data.WorkingSubProduct == null || data.WorkingSubProduct == "" ? "" : data.WorkingSubProduct.Replace("‟", "&#34;").Replace("”", "&#34;").Replace("“", "&#34;").Replace("‘", "&#39;").Replace("’", "&#39;").ToString()) + "</span>" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        //mstr_searchparameter.Append("<td align='left'>" + data.TypeFor + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.ContractTypes + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.imagevideobankid + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Description + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.invoiceno + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.invoicevalue + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.invoicedate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.printquantity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.permissionexpirydate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.weblink + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.creditlines == null ? null : data.creditlines.Replace("©", "&copy;").ToString()) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.usage + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.partyname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.EditorialonlyType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CopyRightHolder + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + (data.Mobile == null || data.Mobile == "" ? "" : Convert.ToString("&nbsp;" + data.Mobile)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.StatusName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AssetSubType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AssestDescription + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Restriction + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RunGrantedQty + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SubLicensing + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Fee + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CurrencyNameCopyRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Territoryrights + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.Extent + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Gratiscopytobesent + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Noofcopy + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.OriginalSource == null ? null : data.OriginalSource.Replace("–", "&#45;").ToString()) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceNumber + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.invoicevalue + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.permissionexpirydate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.Acknowledgementline == null ? null : data.Acknowledgementline.Replace("©", "&copy;").ToString()) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InboundRemarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DateRequstDetails + "</td>");
                      
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
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "exportToExcelPermissionsInboundList", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "exportToExcelPermissionsInboundList", ex);
                throw ex;
            }
        }

        public ActionResult exportToExcelPermissionsInboundLessQuantityList()
        {
            try
            {
                List<PermissionInboundSearchModel> _mobjReportList = new List<PermissionInboundSearchModel>();
                //PermissionInboundSearchModel _mobjParametertList = new PermissionInboundSearchModel();

                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + Session["UserId"].ToString() + "'";

                _mobjReportList = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.PermissionInboundModel.PermissionInboundSearchModel>("Proc_InboundPermissionResultQuantityLess25_get", parameters).ToList();

                string sFileName = "PermissionsInboundQuantityLessThan25_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Permissions In-bound Report Quantity Less Than 25%</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Permission Inbound Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                    mstr_searchparameter.Append("<td ><b> Assets Type</b></td>");
                    mstr_searchparameter.Append("<td ><b> Qunatity Printed </b></td>");
                    mstr_searchparameter.Append("<td ><b> Minimum Quantity </b></td>");
                    mstr_searchparameter.Append("<td ><b> Balance Qunatity </b></td>");
                    mstr_searchparameter.Append("<td ><b> Permission Expiry Date </b></td>");

                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (PermissionInboundSearchModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Code + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + data.WorkingSubProduct + "</span>" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AssetsType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.QunatityPrinted + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.quantity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BalanceCounts + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.permissionexpirydate + "</td>");

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
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "exportToExcelPermissionsInboundLessQuantityList", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsInboundController.cs", "exportToExcelPermissionsInboundLessQuantityList", ex);
                throw ex;
            }
        }

        public IList<PermissionInboundSearchModel> GetPermissionsInboundExcelViewList(string code, string Flag)
        {
            try
            {
                if (code == "")
                {
                    return null;
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("code", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + code + "'";
                    parameters[1] = new SqlParameter("flag", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + Flag + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionInboundSearchModel>("Proc_getInboundPermissionDetailsExcel_get", parameters).ToList();
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult PermissionsInboundViewExcel(String code, String ImageVideo, String CopyRight, String Product, String InBound)
        {

            try
               
            {
                List<PermissionInboundSearchModel> _mobjReportProduct = new List<PermissionInboundSearchModel>();
                List<PermissionInboundSearchModel> _mobjReportInBound = new List<PermissionInboundSearchModel>();

                List<PermissionInboundSearchModel> _mobjReportImageVideoList = new List<PermissionInboundSearchModel>();
                List<PermissionInboundSearchModel> _mobjReportCopyRightList = new List<PermissionInboundSearchModel>();
                StringBuilder mstr_searchparameter = new StringBuilder();


                if (Product != "null")
                {
                    _mobjReportProduct = GetPermissionsInboundExcelViewList(code, Product).ToList();
                }

                if (InBound != "null")               
                {

                    _mobjReportInBound = GetPermissionsInboundExcelViewList(code, InBound).ToList();

                }

                if(ImageVideo != "null")
                {
                    _mobjReportImageVideoList = GetPermissionsInboundExcelViewList(code, ImageVideo).ToList();                  
                
                }

                if (CopyRight != "null")
                {
                    _mobjReportCopyRightList = GetPermissionsInboundExcelViewList(code, CopyRight).ToList();                  

                }




                string sFileName = "PermissionsInbound_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                   
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%; font-size:16px;' valign='top' align=center colspan='2'>" + "<b>Permissions In-bound Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");
                   
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("</table>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td colspan='2'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");


                    if (_mobjReportProduct.Count > 0)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%; font-size:16px;' valign='top' align=left colspan='23'><b>(Product Details)</b></td>");
                        mstr_searchparameter.Append("</tr>");                      

                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                        mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                        mstr_searchparameter.Append("<td ><b>Product Category</b></td>");
                        mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                        mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                        mstr_searchparameter.Append("<td ><b>Product Type</b></td>");
                        mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                        mstr_searchparameter.Append("</tr>");
                        mstr_searchparameter.Append("</td>");
                        int mint_Counter = 1;
                        foreach (PermissionInboundSearchModel data in _mobjReportProduct)
                        {
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.productcategory + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + (data.WorkingSubProduct == null || data.WorkingSubProduct == "" ? "" : data.WorkingSubProduct.Replace("‟", "&#34;").Replace("”", "&#34;").Replace("“", "&#34;").Replace("‘", "&#39;").Replace("’", "&#39;").ToString()) + "</span>" + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.ProductTypeName + "<br />" + "<span style='font-size:11px;'>" + data.ProductSubTypeName + "</span>" + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");

                            
                            mstr_searchparameter.Append("</tr>");
                            mint_Counter++;
                        }

                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=left colspan='23'><b></b></td>");
                        mstr_searchparameter.Append("</tr>");
                    }

                    if (_mobjReportInBound.Count > 0)
                    {


                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%; font-size:16px;' valign='top' align=left colspan='23'><b> (Inbound Permission)</b></td>");
                        mstr_searchparameter.Append("</tr>");

                

                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                        mstr_searchparameter.Append("<td ><b>Inbound Code</b></td>");
                        mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                        //mstr_searchparameter.Append("<td ><b>Inbound Type</b></td>");
                        //mstr_searchparameter.Append("<td ><b>License Code</b></td>");
                        mstr_searchparameter.Append("<td ><b>ISBN</b></td>");

                        mstr_searchparameter.Append("</tr>");
                        mstr_searchparameter.Append("</td>");
                        int mint_Counter = 1;
                        foreach (PermissionInboundSearchModel data in _mobjReportInBound)
                        {
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Code + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                            //mstr_searchparameter.Append("<td align='left'>" + data.InBoundType + "</td>");
                            //mstr_searchparameter.Append("<td align='left'>" + data.LicenseCode + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");

                            mstr_searchparameter.Append("</tr>");
                            mint_Counter++;
                        }
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=left colspan='23'><b></b></td>");
                        mstr_searchparameter.Append("</tr>");
                      
                    }







                    if (_mobjReportImageVideoList.Count > 0)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%; font-size:16px;' valign='top' align=left colspan='23'><b>(Image Video Bank Details)</b></td>");
                        mstr_searchparameter.Append("</tr>");
                        mstr_searchparameter.Append("<tr>");

                        mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                        mstr_searchparameter.Append("<td ><b>Contract Type</b></td>");
                        mstr_searchparameter.Append("<td ><b>Vendor Name</b></td>");
                        mstr_searchparameter.Append("<td ><b>Image ID</b></td>");
                        mstr_searchparameter.Append("<td ><b>Description</b></td>");
                        mstr_searchparameter.Append("<td ><b>Credit Line</b></td>");
                        mstr_searchparameter.Append("<td ><b>Editorial only image</b></td>");
                        mstr_searchparameter.Append("<td ><b>Invoice No</b></td>");
                        mstr_searchparameter.Append("<td ><b>Invoice Value</b></td>");
                        mstr_searchparameter.Append("<td ><b>Currency</b></td>");
                        mstr_searchparameter.Append("<td ><b>Invoice Date</b></td>");
                        mstr_searchparameter.Append("<td ><b>Print Quantity</b></td>");
                        mstr_searchparameter.Append("<td ><b>Permission Expiry Date</b></td>");
                        mstr_searchparameter.Append("<td ><b>Web link</b></td>");
                        mstr_searchparameter.Append("<td ><b>Usage</b></td>");
                        mstr_searchparameter.Append("<td ><b>Remarks</b></td>");

                        mstr_searchparameter.Append("</tr>");
                        mstr_searchparameter.Append("</td>");
                        int mint_Counter = 1;
                        foreach (PermissionInboundSearchModel data in _mobjReportImageVideoList)
                        {
                            mstr_searchparameter.Append("<tr>");

                            mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.ContractTypes + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.partyname + "</td>");
                            mstr_searchparameter.Append("<td align='left'><a href='" + data.weblink + "'>" + data.imagevideobankid + "</a></td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Description + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.creditlines == null || data.creditlines == "" ? "" : data.creditlines.Replace("©", "&copy;").ToString()) + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.EditorialonlyType + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.invoiceno + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.invoicevalue + "</td>");

                            //mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.CurrencySymbol.ToUpper() + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.invoicedate + "</td>");

                            if (data.printquantity == "")
                            {
                                data.printquantity = "Unrestricted";
                            }
                            mstr_searchparameter.Append("<td align='left'>" + data.printquantity + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.permissionexpirydate + "</td>");
                            mstr_searchparameter.Append("<td align='left'><a href='" + data.weblink + "'>" + data.weblink + "</a></td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.usage + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");

                            mstr_searchparameter.Append("</tr>");
                            mint_Counter++;
                        }
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=left colspan='23'><b></b></td>");
                        mstr_searchparameter.Append("</tr>");
                      
                    }




                    if (_mobjReportCopyRightList.Count > 0)
                    {


                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%; font-size:16px;' valign='top' align=left colspan='23'><b>(Copy Right holder Details)</b></td>");
                        mstr_searchparameter.Append("</tr>");

                      
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                        mstr_searchparameter.Append("<td ><b>Copy Right Holder</b></td>");
                        mstr_searchparameter.Append("<td ><b>Address</b></td>");

                        mstr_searchparameter.Append("<td ><b>Country</b></td>");
                        mstr_searchparameter.Append("<td ><b>State</b></td>");
                        mstr_searchparameter.Append("<td ><b>City</b></td>");
                        mstr_searchparameter.Append("<td ><b>PIN Code</b></td>");
                        mstr_searchparameter.Append("<td ><b>Email</b></td>");
                        mstr_searchparameter.Append("<td ><b>Website URL</b></td>");

                        mstr_searchparameter.Append("<td ><b>Contact Person</b></td>");
                        mstr_searchparameter.Append("<td ><b>Telephone No.</b></td>");
                        mstr_searchparameter.Append("<td ><b>Status</b></td>");
                        mstr_searchparameter.Append("<td ><b>Asset Sub-Type</b></td>");
                        mstr_searchparameter.Append("<td ><b>Assest Description</b></td>");
                        mstr_searchparameter.Append("<td ><b>Restriction</b></td>");
                        mstr_searchparameter.Append("<td ><b>Run Granted Qty</b></td>");
                        mstr_searchparameter.Append("<td ><b>Sub-Licensing</b></td>");
                        mstr_searchparameter.Append("<td ><b>Fee</b></td>");

                        mstr_searchparameter.Append("<td ><b>Currency </b></td>");
                        mstr_searchparameter.Append("<td ><b>Territory </b></td>");
                        mstr_searchparameter.Append("<td ><b> Extent </b></td>");
                        mstr_searchparameter.Append("<td ><b> Gratis copies to be sent </b></td>");
                        mstr_searchparameter.Append("<td ><b>Number of copies  </b></td>");
                        mstr_searchparameter.Append("<td ><b>Original Source </b></td>");
                        mstr_searchparameter.Append("<td ><b> Invoice Number</b></td>");
                        mstr_searchparameter.Append("<td ><b>Invoice Value </b></td>");
                        mstr_searchparameter.Append("<td ><b>Permission Expiry date </b></td>");

                        mstr_searchparameter.Append("<td ><b>Acknowledgement line </b></td>");
                        mstr_searchparameter.Append("<td ><b>Remarks </b></td>");

                        mstr_searchparameter.Append("<td ><b>Date Requst Details </b></td>");


                        mstr_searchparameter.Append("</tr>");
                        mstr_searchparameter.Append("</td>");
                        int mint_Counter = 1;
                        foreach (PermissionInboundSearchModel data in _mobjReportCopyRightList)
                        {
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.CopyRightHolder + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.StateName + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.CityName + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.Mobile == null || data.Mobile == "" ? "" : Convert.ToString("&nbsp;" + data.Mobile)) + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.StatusName + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.AssetSubType + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.AssestDescription == null || data.AssestDescription == "" ? "" : data.AssestDescription.Replace("‟", "&#34;").Replace("”", "&#34;").Replace("“", "&#34;").Replace("‘", "&#39;").Replace("’", "&#39;").ToString()) + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Restriction + "</td>");



                            mstr_searchparameter.Append("<td align='left'>" + data.RunGrantedQty + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.SubLicensing + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.Fee + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.Territoryrights + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.Extent + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.Gratiscopytobesent + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.Noofcopy + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + (data.OriginalSource == null || data.OriginalSource == "" ? "" : data.OriginalSource.Replace("–", "&#45;").ToString()) + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.InvoiceNumber + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.invoicevalue + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.permissionexpirydate + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + (data.Acknowledgementline == null || data.Acknowledgementline == "" ? "" : data.Acknowledgementline.Replace("©", "&copy;").Replace("‟", "&#34;").Replace("”", "&#34;").Replace("“", "&#34;").Replace("‘", "&#39;").Replace("’", "&#39;").ToString()) + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.InboundRemarks + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.DateRequstDetails + "</td>");


                            mstr_searchparameter.Append("</tr>");
                            mint_Counter++;
                        }

                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=left colspan='23'><b></b></td>");
                        mstr_searchparameter.Append("</tr>");
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