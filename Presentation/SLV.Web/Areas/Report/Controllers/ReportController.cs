using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;

using ACS.Core;
using ACS.Services.Master;


using System.Data;
using System.Data.SqlClient;

using SLV.Model.Report;

using System.Text;
using ACS.Core.Data;
using ACS.Data;


namespace SLV.Web.Areas.Report.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;

        public ReportController
            (
            IWorkContext workContext
            , IDbContext dbContext
            )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }

        //Get - Author / Publisher Statement Search on 20/09/2016
        public ActionResult StatementSearch(string For="", string Type="")
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (For != null && (For.ToLower() == "rights" || For.ToLower() == "permissionsoutbound"))
                {
                    Session["For"] = For.ToLower();
                }

                if (Type.ToLower() == "report")
                {
                    ViewBag.Report = Type.ToLower();
                }

                return View();
            }
        }

        //Get - Author / Publisher Statement View Detail on 21/09/2016
        public ActionResult StatementView(string PLId,string PubId, string ACId, string AId)
        {
            TempData["For"] = Session["For"];
            string contractId = "";
            string AuthorId = "";
            string licenseId = "";
            string publishingCompanyId = "";
            string mobj_for = "";
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                contractId = ACId;
                AuthorId = AId;
                licenseId = PLId;
                publishingCompanyId = PubId;

                //Added By Parkash == Redirect from Payment Tagging Page
                mobj_for = Request.QueryString["For"];
                if (!string.IsNullOrEmpty(mobj_for))
                {
                    TempData["For"] = mobj_for.ToLower();
                }
                //End By Parkash

                if (!string.IsNullOrEmpty(contractId) || !string.IsNullOrEmpty(AuthorId))
                {
                    string data = "";
                    if (!string.IsNullOrEmpty(contractId))
                    {
                        data += "C_" + contractId + ",";
                    }
                    if (!string.IsNullOrEmpty(AuthorId))
                    {
                        data += "A_" + AuthorId + ",";
                    }
                    if (data != "")
                    {
                        TempData["ContractId"] = data.Substring(0, data.Length - 1);
                    }
                }
                else if (!string.IsNullOrEmpty(licenseId) || !string.IsNullOrEmpty(publishingCompanyId))
                {
                    string data = "";
                    if (!string.IsNullOrEmpty(licenseId))
                    {
                        data += "L_" + licenseId + ",";
                    }
                    if (!string.IsNullOrEmpty(publishingCompanyId))
                    {
                        data += "P_" + publishingCompanyId + ",";
                    }
                    if (data != "")
                    {
                        TempData["LicenseId"] = data.Substring(0, data.Length - 1);
                    }
                }
                else
                {
                    return RedirectToActionPermanent("RightsStatementSearch", "Report");
                }
                return View();
            }
        }
        

        //Invoice Report
        public ActionResult InvoiceReport()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }


        //Author Contract Expiry Report Added by Ankush Kumar on 25/10/2016
        public ActionResult AuthorContractExpiryReport()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }


        public IList<InvoiceReportModel> GetInvoiceReportExcelList(InvoiceReportModel _InvoiceRModel)
        {
            try
            {
               
                    SqlParameter[] parameters = new SqlParameter[9];


                    parameters[0] = new SqlParameter("InvoiceFromDate", SqlDbType.VarChar, 50);
                    if (_InvoiceRModel.InvoiceFromDate == "null")
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = "'" + _InvoiceRModel.InvoiceFromDate + "'";
                    }

                    parameters[1] = new SqlParameter("InvoiceToDate", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.InvoiceToDate == "null")
                    {
                        parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[1].Value = "'" + _InvoiceRModel.InvoiceToDate + "'";
                    }

                    parameters[2] = new SqlParameter("InvoiceNo", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.InvoiceNo == "undefined")
                    {
                        parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[2].Value = "'" + _InvoiceRModel.InvoiceNo + "'";
                    }



                    parameters[3] = new SqlParameter("InvoiceValue", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.InvoiceValue == "undefined")
                    {
                        parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[3].Value = "'" + _InvoiceRModel.InvoiceValue + "'";
                    }

                    parameters[4] = new SqlParameter("InvoiceStatus", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.InvoiceStatus == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _InvoiceRModel.InvoiceStatus + "'";
                    }


                    parameters[5] = new SqlParameter("LicenseeName", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.LicenseeName == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _InvoiceRModel.LicenseeName + "'";
                    }
                   

                    parameters[6] = new SqlParameter("Country", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.Country == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _InvoiceRModel.Country + "'";
                    }


                    parameters[7] = new SqlParameter("State", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.State == "undefined")
                    {
                        parameters[7].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[7].Value = "'" + _InvoiceRModel.State + "'";
                    }

                    parameters[8] = new SqlParameter("City", SqlDbType.VarChar, 200);

                    if (_InvoiceRModel.City == "undefined")
                    {
                        parameters[8].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[8].Value = "'" + _InvoiceRModel.City + "'";
                    }


                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<InvoiceReportModel>("Proc_InvoiceReport_get", parameters).ToList();
                    return _GetAuthorReport;

                
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public InvoiceReportModel GetInvoiceReportExcelParameterList(InvoiceReportModel _InvoiceRModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[9];


                parameters[0] = new SqlParameter("InvoiceFromDate", SqlDbType.VarChar, 50);
                if (_InvoiceRModel.InvoiceFromDate == "null")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _InvoiceRModel.InvoiceFromDate + "'";
                }

                parameters[1] = new SqlParameter("InvoiceToDate", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.InvoiceToDate == "null")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _InvoiceRModel.InvoiceToDate + "'";
                }

                parameters[2] = new SqlParameter("InvoiceNo", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.InvoiceNo == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _InvoiceRModel.InvoiceNo + "'";
                }



                parameters[3] = new SqlParameter("InvoiceValue", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.InvoiceValue == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _InvoiceRModel.InvoiceValue + "'";
                }

                parameters[4] = new SqlParameter("InvoiceStatus", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.InvoiceStatus == "undefined")
                {
                    parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[4].Value = "'" + _InvoiceRModel.InvoiceStatus + "'";
                }


                parameters[5] = new SqlParameter("LicenseeName", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.LicenseeName == "undefined")
                {
                    parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[5].Value = "'" + _InvoiceRModel.LicenseeName + "'";
                }


                parameters[6] = new SqlParameter("Country", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.Country == "undefined")
                {
                    parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[6].Value = "'" + _InvoiceRModel.Country + "'";
                }


                parameters[7] = new SqlParameter("State", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.State == "undefined")
                {
                    parameters[7].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[7].Value = "'" + _InvoiceRModel.State + "'";
                }

                parameters[8] = new SqlParameter("City", SqlDbType.VarChar, 200);

                if (_InvoiceRModel.City == "undefined")
                {
                    parameters[8].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[8].Value = "'" + _InvoiceRModel.City + "'";
                }
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<InvoiceReportModel>("Proc_InvoiceReportReturnParameter_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
               
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public ActionResult exportToExcelInvoiceReportList(InvoiceReportModel _mobjOtherContract)
        {
            try
            {


                List<InvoiceReportModel> _mobjReportList = new List<InvoiceReportModel>();
                InvoiceReportModel _mobjParametertList = new InvoiceReportModel();


                _mobjReportList = GetInvoiceReportExcelList(_mobjOtherContract).ToList();

                _mobjParametertList = GetInvoiceReportExcelParameterList(_mobjOtherContract);




                string sFileName = "InvoiceReport_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Invoice Report</b>" + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Licensee Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Number</b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Value</b></td>");
                    mstr_searchparameter.Append("<td ><b>Currency</b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Description</b></td>");




                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");





                    mstr_searchparameter.Append("<td ><b> Invoice Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Contact Person </b></td>");
                    mstr_searchparameter.Append("<td ><b>Address </b></td>");
                    mstr_searchparameter.Append("<td ><b>Country </b></td>");
                    mstr_searchparameter.Append("<td ><b> State</b></td>");
                    mstr_searchparameter.Append("<td ><b>City </b></td>");
                    mstr_searchparameter.Append("<td ><b>PIN Code </b></td>");
                    mstr_searchparameter.Append("<td ><b>Mobile </b></td>");
                    mstr_searchparameter.Append("<td ><b>Email </b></td>");
                    mstr_searchparameter.Append("<td ><b> URL </b></td>");

                    mstr_searchparameter.Append("<td ><b>Entry Date</b></td>");


                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (InvoiceReportModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeOrganizationName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceValue + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceDescription + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + data.WorkingSubProduct + "</span>" + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + (data.OUPISBN == null || data.OUPISBN == "" ? "" : Convert.ToString("&nbsp;" + data.OUPISBN)) + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");


                        mstr_searchparameter.Append("<td align='left'>" + data.DateOfInvoice + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Country + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.State + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");




                        mstr_searchparameter.Append("<td align='left'>" + data.EntryDate + "</td>");
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


        //--report list for Permissions Outbound
        public IList<StatementModel> GetStatementSearchReportExcelList(StatementModel _AuthorPubStModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];

                //parameters[0] = new SqlParameter("Year", SqlDbType.VarChar, 4);
                //if (_AuthorPubStModel.Year == "undefined")
                //{
                //    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[0].Value = "'" + _AuthorPubStModel.Year + "'";
                //}

                //parameters[1] = new SqlParameter("Month", SqlDbType.VarChar, 2);
                //if (_AuthorPubStModel.Month == "undefined")
                //{
                //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[1].Value = "'" + _AuthorPubStModel.Month + "'";
                //}

                parameters[0] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.FromYear == "undefined")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _AuthorPubStModel.FromYear + "'";
                }

                parameters[1] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.FromMonth == "undefined")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                }

                parameters[2] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.ToYear == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _AuthorPubStModel.ToYear + "'";
                }

                parameters[3] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.ToMonth == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _AuthorPubStModel.ToMonth + "'";
                }

                //if ((_AuthorPubStModel.AuthorName != "undefined" && _AuthorPubStModel.AuthorName != "undefined") ||
                //    (_AuthorPubStModel.AuthorCode != "undefined" && _AuthorPubStModel.AuthorCode != "undefined") ||
                //    (_AuthorPubStModel.AuthorContractCode != "undefined" && _AuthorPubStModel.AuthorContractCode != "undefined"))
                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[4] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorContractCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    }

                    parameters[5] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    }


                    parameters[6] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    }

                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_A_get", parameters).ToList();
                    return (_GetAuthorStatement);
                }
                else
                {
                    parameters[4] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.ProductLicenseCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    }

                    parameters[5] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    }

                    parameters[6] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    }

                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_P_get", parameters).ToList();
                    return (_GetStatement);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--report list for Rights Selling
        public IList<StatementModel> GetStatementSearchReportExcelListRights(StatementModel _AuthorPubStModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];

                //parameters[0] = new SqlParameter("Year", SqlDbType.VarChar, 4);
                //if (_AuthorPubStModel.Year == "undefined")
                //{
                //    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[0].Value = "'" + _AuthorPubStModel.Year + "'";
                //}

                //parameters[1] = new SqlParameter("Month", SqlDbType.VarChar, 2);
                //if (_AuthorPubStModel.Month == "undefined")
                //{
                //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[1].Value = "'" + _AuthorPubStModel.Month + "'";
                //}

                parameters[0] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.FromYear == "undefined")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _AuthorPubStModel.FromYear + "'";
                }

                parameters[1] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.FromMonth == "undefined")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                }

                parameters[2] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.ToYear == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _AuthorPubStModel.ToYear + "'";
                }

                parameters[3] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.ToMonth == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _AuthorPubStModel.ToMonth + "'";
                }

                //if ((_AuthorPubStModel.AuthorName != "undefined" && _AuthorPubStModel.AuthorName != "undefined") ||
                //    (_AuthorPubStModel.AuthorCode != "undefined" && _AuthorPubStModel.AuthorCode != "undefined") ||
                //    (_AuthorPubStModel.AuthorContractCode != "undefined" && _AuthorPubStModel.AuthorContractCode != "undefined"))
                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[4] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorContractCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    }

                    parameters[5] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    }


                    parameters[6] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    }

                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_A_get", parameters).ToList();
                    return (_GetAuthorStatement);
                }
                else
                {
                    parameters[4] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.ProductLicenseCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    }

                    parameters[5] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    }

                    parameters[6] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    }

                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_P_get", parameters).ToList();
                    return (_GetStatement);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public StatementModel GetStatementSearchReportExcelParameterList(StatementModel _AuthorPubStModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];

                //parameters[0] = new SqlParameter("Year", SqlDbType.VarChar, 4);
                //if (_AuthorPubStModel.Year == "undefined")
                //{
                //    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[0].Value = "'" + _AuthorPubStModel.Year + "'";
                //}

                //parameters[1] = new SqlParameter("Month", SqlDbType.VarChar, 2);
                //if (_AuthorPubStModel.Month == "undefined")
                //{
                //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                //}
                //else
                //{
                //    parameters[1].Value = "'" + _AuthorPubStModel.Month + "'";
                //}

                parameters[0] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.FromYear == "undefined")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _AuthorPubStModel.FromYear + "'";
                }

                parameters[1] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.FromMonth == "undefined")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                }

                parameters[2] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.ToYear == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _AuthorPubStModel.ToYear + "'";
                }

                parameters[3] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.ToMonth == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _AuthorPubStModel.ToMonth + "'";
                }

                //if ((_AuthorPubStModel.AuthorName != "undefined" && _AuthorPubStModel.AuthorName != "undefined") ||
                //    (_AuthorPubStModel.AuthorCode != "undefined" && _AuthorPubStModel.AuthorCode != "undefined") ||
                //    (_AuthorPubStModel.AuthorContractCode != "undefined" && _AuthorPubStModel.AuthorContractCode != "undefined"))
                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[4] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorContractCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    }

                    parameters[5] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    }

                    parameters[6] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    }

                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Parameter_Search_PO_A_get", parameters).FirstOrDefault();
                    return _GetAuthorStatement;
                }
                else
                {
                    parameters[4] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.ProductLicenseCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    }

                    parameters[5] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    }

                    parameters[6] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    }

                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Parameter_Search_PO_P_get", parameters).FirstOrDefault();
                    return _GetStatement;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public ActionResult exportToExcelStatementSearchReportList(StatementModel _AuthorPubStModel)
        {
            try
            {
                List<StatementModel> _mobjReportList = new List<StatementModel>();
                StatementModel _mobjParametertList = new StatementModel();

                if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                    _mobjReportList = GetStatementSearchReportExcelList(_AuthorPubStModel).ToList();
                else
                    _mobjReportList = GetStatementSearchReportExcelListRights(_AuthorPubStModel).ToList();

                _mobjParametertList = GetStatementSearchReportExcelParameterList(_AuthorPubStModel);

                string sFileName = "Author_PublisherStatement_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Author / Publisher Statement Report</b>" + "</td>");
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

                    //if ((_AuthorPubStModel.AuthorName != "undefined" && _AuthorPubStModel.AuthorName != "undefined") ||
                    //    (_AuthorPubStModel.AuthorCode != "undefined" && _AuthorPubStModel.AuthorCode != "undefined") ||
                    //     (_AuthorPubStModel.AuthorContractCode != "undefined" && _AuthorPubStModel.AuthorContractCode != "undefined"))
                    if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                    {
                        mstr_searchparameter.Append("<td ><b>Author Contract Code</b></td>");

                        mstr_searchparameter.Append("<td ><b>Author Name</b></td>");
                    }
                    else
                    {

                        mstr_searchparameter.Append("<td ><b>Product License Code</b></td>");

                        mstr_searchparameter.Append("<td ><b>Publishing Company Name</b></td>");
                    
                    }                 

                    mstr_searchparameter.Append("<td ><b>Total Amount</b></td>");

                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (StatementModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");

                        //if ((_AuthorPubStModel.AuthorName != "undefined" && _AuthorPubStModel.AuthorName != "undefined") ||
                        //    (_AuthorPubStModel.AuthorCode != "undefined" && _AuthorPubStModel.AuthorCode != "undefined") ||
                        //    (_AuthorPubStModel.AuthorContractCode != "undefined" && _AuthorPubStModel.AuthorContractCode != "undefined"))
                        if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                        {

                            mstr_searchparameter.Append("<td align='left'>" + data.AuthorContractCode + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<td align='left'>" + data.ProductLicenseCode + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.PublishingCompanyName + "</td>");
                        }                       
                      
                        mstr_searchparameter.Append("<td align='left'>" + data.TotalAmount + "</td>");

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

        //Added on 25 oct, 2016
        public ActionResult LicenseList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        //Added on 25 oct, 2016
        public ActionResult AddendumList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }

        //Added for LicenseList Export Excel on 25 oct, 2016
        public ActionResult exportToExcelLicenseList(LicenseListModel _mobjLicenseList)
        {
            try
            {
                List<LicenseListModel> _mobjReportList = new List<LicenseListModel>();
                LicenseListModel _mobjParametertList = new LicenseListModel();

                _mobjReportList = GetLicenseListExcelList(_mobjLicenseList).ToList();
                //_mobjParametertList = GetLicenseListReportExcelParameterList(_mobjLicenseList);

                string sFileName = "ProductLicense-Report_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='4'>" + "<b>Product License Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left colspan='2'>" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right colspan='2'>" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    //if (!String.IsNullOrEmpty(_mobjParametertList.ReturnList))
                    //{
                    //    mstr_searchparameter.Append("<tr>");
                    //    mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + _mobjParametertList.ReturnList + "</td>");
                    //    mstr_searchparameter.Append("</tr>");
                    //}
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("</table>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td colspan='2'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product License code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author</b></td>");
                    mstr_searchparameter.Append("<td ><b>Expiry Date</b></td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (LicenseListModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductLicensecode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Expirydate + "</td>");
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

        public IList<LicenseListModel> GetLicenseListExcelList(LicenseListModel _objModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + _objModel.Flag + "'";
                parameters[1] = new SqlParameter("ExecutiveId", SqlDbType.VarChar, 200);
                parameters[1].Value = "'" + Session["UserId"].ToString() + "'"; ;

                var _GetProductLicenseExpired = _dbContext.ExecuteStoredProcedureListNewData<LicenseListModel>("Proc_ProductLicenseExpired_get", parameters).ToList();
                return _GetProductLicenseExpired;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Added for AddendumList Export Excel on 25 oct, 2016
        public ActionResult exportToExcelAddendumList(AddendumListModel _mobjLicenseList)
        {
            try
            {
                List<AddendumListModel> _mobjReportList = new List<AddendumListModel>();
                //AddendumListModel _mobjParametertList = new AddendumListModel();

                _mobjReportList = GetAddendumListExcelList(_mobjLicenseList).ToList();
                //_mobjParametertList = GetLicenseListReportExcelParameterList(_mobjLicenseList);

                string sFileName = "ProductLicenseAddendums-Report_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='4'>" + "<b>Product License Addendums Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left colspan='2'>" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right  colspan='2'>" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    //if (!String.IsNullOrEmpty(_mobjParametertList.ReturnList))
                    //{
                    //    mstr_searchparameter.Append("<tr>");
                    //    mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + _mobjParametertList.ReturnList + "</td>");
                    //    mstr_searchparameter.Append("</tr>");
                    //}
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("</table>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td colspan='2'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product License code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author</b></td>");
                    mstr_searchparameter.Append("<td ><b>Expiry Date</b></td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (AddendumListModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductLicensecode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Expirydate + "</td>");
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

        public IList<AddendumListModel> GetAddendumListExcelList(AddendumListModel _objModel)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
                parameters[0].Value = "'" + _objModel.Flag + "'";

                var _GetProductLicenseExpired = _dbContext.ExecuteStoredProcedureListNewData<AddendumListModel>("Proc_ProductLicenseAddendumExpired_get", parameters).ToList();
                return _GetProductLicenseExpired;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //Export to Excel of Detail(Statement Search) // on 02 Sep, 2017
        public ActionResult exportToExcelStatementSearchReportDetails(StatementModel _AuthorPubStModel)
        {
            try
            {
                List<StatementModel> _mobjReportList = new List<StatementModel>();
                StatementModel _mobjParametertList = new StatementModel();

                if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                    _mobjReportList = GetStatementSearchReportExcelDetails(_AuthorPubStModel).ToList();
                else
                    _mobjReportList = GetStatementSearchReportExcelDetailsRights(_AuthorPubStModel).ToList();

                _mobjParametertList = GetStatementSearchReportExcelParameterList(_AuthorPubStModel);

                string sFileName = "Author_PublisherStatement_Details_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Author / Publisher Statement Details Report</b>" + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Party Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Country</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Title</b></td>");
                    mstr_searchparameter.Append("<td ><b>Division</b></td>");

                    if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                    {
                        mstr_searchparameter.Append("<td ><b>Invoice Amount</b></td>");
                    }

                    mstr_searchparameter.Append("<td ><b>Invoice Currency</b></td>");
                    mstr_searchparameter.Append("<td ><b>Receipt Amount</b></td>");

                    mstr_searchparameter.Append("<td ><b>WHT</b></td>");
                    mstr_searchparameter.Append("<td ><b>Converison Rate</b></td>");
                    mstr_searchparameter.Append("<td ><b>Invoice Amount (INR)</b></td>");
                    mstr_searchparameter.Append("<td ><b>Receipt Amount (INR)</b></td>");

                    mstr_searchparameter.Append("<td ><b>Author Share (%)</b></td>");
                    mstr_searchparameter.Append("<td ><b>SAP Author Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                    mstr_searchparameter.Append("<td ><b>Further Split</b></td>");
                    mstr_searchparameter.Append("<td ><b>Purpose</b></td>");

                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    string IA = "", RA = "";                
                    foreach (StatementModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");

                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CountryName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.ISBN == null || data.ISBN == "" ? "" : Convert.ToString("&nbsp;" + data.ISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "</b align='left'><br/>" + data.WorkingSubProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DivisionName + "</td>");

                        //--------Start (Repeat data view one time) 
                        if (IA != data.InvoiceValue || RA != (data.Amount == null ? "" : data.Amount.ToString()))
                        {
                            if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                            {
                                mstr_searchparameter.Append("<td align='left'>" + data.InvoiceValue + "</td>");
                            }

                            mstr_searchparameter.Append("<td align='left'>" + data.InvoiceCurrency.ToUpper() + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.Amount + "</td>");

                            mstr_searchparameter.Append("<td align='left'>" + data.WithHoldingTax + "</td>");
                            mstr_searchparameter.Append("<td align='left'>" + data.ConverisonRate + "</td>");

                            if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                            {
                                //Invoice Amount (INR)
                                if (data.InvoiceValue == "")
                                {
                                    mstr_searchparameter.Append("<td align='left'>" + data.ConverisonRate + "</td>");
                                }
                                else if (data.ConverisonRate == null)
                                {
                                    mstr_searchparameter.Append("<td align='left'>" + data.InvoiceValue + "</td>");
                                }
                                else
                                {
                                    mstr_searchparameter.Append("<td align='left'>" + Convert.ToDecimal(data.InvoiceValue) * data.ConverisonRate + "</td>");
                                }
                            }
                            else
                            {
                                mstr_searchparameter.Append("<td align='left'>" + data.ConverisonRate + "</td>");
                            }

                            //Receipt Amount - INR 
                            if (data.Amount == null)
                            {
                                mstr_searchparameter.Append("<td align='left'>" + data.ConverisonRate + "</td>");
                            }
                            else if (data.ConverisonRate == null)
                            {
                                mstr_searchparameter.Append("<td align='left'>" + data.Amount + "</td>");
                            }
                            else
                            {
                                mstr_searchparameter.Append("<td align='left'>" + data.Amount * data.ConverisonRate + "</td>");
                            }
                            
                        }
                        else
                        {
                            if (_AuthorPubStModel.For.ToLower() == "permissionsoutbound")
                            {
                                mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            }

                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                            mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                        }

                        IA = data.InvoiceValue;
                        RA = data.Amount == null ? "" : data.Amount.ToString();
                        //-----------End (Repeat data view one time)

                        mstr_searchparameter.Append("<td align='left'>" + data.Percentage + " %</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorSAPCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");

                        mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");
                        mstr_searchparameter.Append("<td align='left'>&nbsp;</td>");

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

        //--report Detail for Permissions Outbound 
        public IList<StatementModel> GetStatementSearchReportExcelDetails(StatementModel _AuthorPubStModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];
                
                parameters[0] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.FromYear == "undefined")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _AuthorPubStModel.FromYear + "'";
                }

                parameters[1] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.FromMonth == "undefined")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                }

                parameters[2] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.ToYear == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _AuthorPubStModel.ToYear + "'";
                }

                parameters[3] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.ToMonth == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _AuthorPubStModel.ToMonth + "'";
                }

                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[4] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorContractCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    }

                    parameters[5] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    }


                    parameters[6] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    }

                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_A_DetailsReport_get", parameters).ToList();
                    return (_GetAuthorStatement);
                }
                else
                {
                    parameters[4] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.ProductLicenseCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    }

                    parameters[5] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    }

                    parameters[6] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    }

                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_PO_P_DetailsReport_get", parameters).ToList();
                    return (_GetStatement);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //--report Detail for Rights Selling 
        public IList<StatementModel> GetStatementSearchReportExcelDetailsRights(StatementModel _AuthorPubStModel)
        {
            try
            {

                SqlParameter[] parameters = new SqlParameter[7];

                parameters[0] = new SqlParameter("FromYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.FromYear == "undefined")
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = "'" + _AuthorPubStModel.FromYear + "'";
                }

                parameters[1] = new SqlParameter("FromMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.FromMonth == "undefined")
                {
                    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[1].Value = "'" + _AuthorPubStModel.FromMonth + "'";
                }

                parameters[2] = new SqlParameter("ToYear", SqlDbType.VarChar, 4);
                if (_AuthorPubStModel.ToYear == "undefined")
                {
                    parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[2].Value = "'" + _AuthorPubStModel.ToYear + "'";
                }

                parameters[3] = new SqlParameter("ToMonth", SqlDbType.VarChar, 2);
                if (_AuthorPubStModel.ToMonth == "undefined")
                {
                    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[3].Value = "'" + _AuthorPubStModel.ToMonth + "'";
                }

                if (_AuthorPubStModel.Type != null && _AuthorPubStModel.Type != "" && _AuthorPubStModel.Type.ToLower() == "authorcontractcode")
                {
                    parameters[4] = new SqlParameter("AuthorContractCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorContractCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.AuthorContractCode + "'";
                    }

                    parameters[5] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.AuthorCode + "'";
                    }


                    parameters[6] = new SqlParameter("AuthorName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.AuthorName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.AuthorName + "'";
                    }

                    var _GetAuthorStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_A_DetailsReport_get", parameters).ToList();
                    return (_GetAuthorStatement);
                }
                else
                {
                    parameters[4] = new SqlParameter("ProductLicenseCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.ProductLicenseCode == "undefined")
                    {
                        parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[4].Value = "'" + _AuthorPubStModel.ProductLicenseCode + "'";
                    }

                    parameters[5] = new SqlParameter("PublishingCompanyCode", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyCode == "undefined")
                    {
                        parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[5].Value = "'" + _AuthorPubStModel.PublishingCompanyCode + "'";
                    }

                    parameters[6] = new SqlParameter("PublishingCompanyName", SqlDbType.VarChar, 200);
                    if (_AuthorPubStModel.PublishingCompanyName == "undefined")
                    {
                        parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[6].Value = "'" + _AuthorPubStModel.PublishingCompanyName + "'";
                    }

                    var _GetStatement = _dbContext.ExecuteStoredProcedureListNewData<StatementModel>("Proc_Statement_Search_RS_P_DetailsReport_get", parameters).ToList();
                    return (_GetStatement);
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}