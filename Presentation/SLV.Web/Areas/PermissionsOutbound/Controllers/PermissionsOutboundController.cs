//Create by Saddam on 27/07/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using ACS.Core;
using ACS.Services.Master;

using System.Text;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using SLV.Model.Common;
using System.IO;
using Logger;

namespace SLV.Web.Areas.PermissionsOutbound.Controllers
{
    public class PermissionsOutboundController : Controller
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;

        public PermissionsOutboundController(
            IWorkContext workContext
            , IDbContext dbContext
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }
        //
        // GET: /PermissionsOutbound/PermissionsOutbound/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PermissionsOutboundMaster(int? Id, string type, int? OutboundId, string OutboundView)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }

                if (OutboundId !=null)
                {
                    TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["OutboundId"] = OutboundId;
                }
                if (OutboundView != null)
                {
                    TempData["OutboundId"] = OutboundView;
                    return View("PermissionsOutboundView");
                }


                return View("PermissionsOutboundMaster");
            }
            // return View();
        }

        public ActionResult PDF(string OutboundView)
        {
            try
            {

                if (_workContext.CurrentUser == null || Session["UserId"] == null)
                {
                    TempData["From"] = "S";
                    return RedirectToAction("Login", "Login", new { area = "" });
                }
                else
                {
                    ViewBag.OutboundId = OutboundView;



                    SqlParameter[] parameters = new SqlParameter[1];

                    parameters[0] = new SqlParameter("PermissionsoutboundId", SqlDbType.VarChar, 50);
                    if (OutboundView == null)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = OutboundView;
                    }


                    var _GetPermissionsOutBound = _dbContext.ExecuteStoredProcedureListNewData<PermissionsoutboundDetials>("Proc_PermissionsoutboundDetails_get", parameters).FirstOrDefault();

                    PermissionsoutboundDetials _mobjReportList = new PermissionsoutboundDetials();

                    _mobjReportList.organizationname = _GetPermissionsOutBound.organizationname;

                    _mobjReportList.contactperson = _GetPermissionsOutBound.contactperson;

                    _mobjReportList.address = _GetPermissionsOutBound.address;

                    _mobjReportList.Country = _GetPermissionsOutBound.Country;
                    _mobjReportList.State = _GetPermissionsOutBound.State;

                    _mobjReportList.City = _GetPermissionsOutBound.City;
                    _mobjReportList.pincode = _GetPermissionsOutBound.pincode;

                    _mobjReportList.DateOfInvoiceView = _GetPermissionsOutBound.DateOfInvoiceView;
                    _mobjReportList.invoiceno = _GetPermissionsOutBound.invoiceno;

                    _mobjReportList.invoicevalue = _GetPermissionsOutBound.invoicevalue;

                    _mobjReportList.invoicedescription = _GetPermissionsOutBound.invoicedescription;

                    _mobjReportList.WorkingProduct = _GetPermissionsOutBound.WorkingProduct;

                    _mobjReportList.AuthorName = _GetPermissionsOutBound.AuthorName;

                    _mobjReportList.InvoiceCurrencySymbol = _GetPermissionsOutBound.InvoiceCurrencySymbol.ToLower();

                    _mobjReportList.ISBN = _GetPermissionsOutBound.ISBN;


                    string header = Server.MapPath(@"~\Areas\PermissionsOutbound\Views\Shared\InvoiceHeader.html");//Path of PrintHeader.html File
                    String footer = "Oxford University Press World Trade Tower (12th Floor) C-1, Sector 16, Main DND Road Rajnigandha Chowk, Noida Uttar Pradesh – 201306 India";


                    string customSwitches = string.Format("--header-html  \"{0}\" " +
                                           "--header-spacing \"0\" " +
                                           "--footer-center \"{1}\" " +
                                           "--footer-line --footer-font-size \"7\" --footer-spacing -7 --footer-font-name \"open_sansregular\"  ", header, footer);

                    return new Rotativa.ViewAsPdf("PermissionsOutboundInvoiceViewPdf", _mobjReportList)
                    {
                        CustomSwitches = customSwitches,
                        PageMargins = new Rotativa.Options.Margins(60, 15, 20, 15), // it's in millimeters
                        PageSize = Rotativa.Options.Size.A4
                    };

                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsOutboundController.cs", "PDF", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "PermissionsOutboundController.cs", "PDF", ex);
            }
            return null;
        }

        public ActionResult abc(string OutboundView)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.OutboundId = OutboundView;



                SqlParameter[] parameters = new SqlParameter[1];

                parameters[0] = new SqlParameter("PermissionsoutboundId", SqlDbType.VarChar, 50);
                if (OutboundView == null)
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = OutboundView;
                }
                var _GetPermissionsOutBound = _dbContext.ExecuteStoredProcedureListNewData<PermissionsoutboundDetials>("Proc_PermissionsoutboundDetails_get", parameters).FirstOrDefault();
                SqlParameter[] parameters2 = new SqlParameter[1];

                parameters2[0] = new SqlParameter("PermissionsoutboundId", SqlDbType.VarChar, 50);
                if (OutboundView == null)
                {
                    parameters2[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters2[0].Value = OutboundView;
                }
                var _GetPermissionsOutBoundUpdate = _dbContext.ExecuteStoredProcedureListNewData<PermissionsoutboundDetials>("Proc_PermissionsoutboundUpdateDetails_get", parameters2).FirstOrDefault();

                PermissionsoutboundDetials _mobjReportList = new PermissionsoutboundDetials();

                _mobjReportList.organizationname = _GetPermissionsOutBound.organizationname;

                _mobjReportList.contactperson = _GetPermissionsOutBound.contactperson;

                _mobjReportList.address = _GetPermissionsOutBound.address;

                _mobjReportList.Country = _GetPermissionsOutBound.Country;
                _mobjReportList.State = _GetPermissionsOutBound.State;

                _mobjReportList.City = _GetPermissionsOutBound.City;
                _mobjReportList.pincode = _GetPermissionsOutBound.pincode;

                _mobjReportList.DateOfInvoiceView = _GetPermissionsOutBound.DateOfInvoiceView;
                _mobjReportList.invoiceno = _GetPermissionsOutBound.invoiceno;

                _mobjReportList.invoicevalue = _GetPermissionsOutBound.invoicevalue;

                _mobjReportList.invoicedescription = _GetPermissionsOutBound.invoicedescription;
                _mobjReportList.currencyname = _GetPermissionsOutBoundUpdate.currencyname;

                _mobjReportList.ISBN = _GetPermissionsOutBound.ISBN;

                //return new Rotativa.ViewAsPdf("PermissionsOutboundInvoiceViewPdf", _mobjReportList);
                return View("PermissionsOutboundInvoiceViewPdf", _mobjReportList);
            }
        }

        public ActionResult PermissionsOutboundSearchMaster(string For)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                TempData["InvoiceView"] = "0";
                if (For != null)
                {
                    if (For == "BackToSearch")
                    {
                        TempData["BackToSearch"] = For.ToLower();
                    }

                    if (For.ToLower() == "InvoiceView".ToLower())
                    {
                        TempData["InvoiceView"] = For;
                    }

                    if (For.ToLower() == "report")
                    {
                        ViewBag.Report = For.ToLower();
                    }

                     if (For.ToLower() == "view")
                    {
                        TempData["Action"] = For.ToLower();
                    }
                     if (For.ToLower() == "update")
                    {
                        TempData["Action"] = For.ToLower();
                    }

                     if (For.ToLower() == "delete")
                     {
                         TempData["Action"] = For.ToLower();
                     }

                }

                TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                return View();
            }
        }


        //public ActionResult PermissionsOutboundPaymentTagging()
        //{
        //    if (_workContext.CurrentUser == null)
        //    {
        //        TempData["From"] = "S";
        //        return RedirectToAction("Login", "Login", new { area = "" });
        //    }
        //    else
        //    {
                
        //        return View();
        //    }
        //}

        public ActionResult PermissionsOutboundPaymentTagging(string AuthorId = "", string AuthorContractId = "", string PublishingCompanyId = "", string ProductLicenseId = "", string OutboundId = "", int ProuductId = 0)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (AuthorId != "")
                {
                    TempData["AuthorId"] = AuthorId;
                }
                if (AuthorContractId != "")
                {
                    TempData["AuthorContractId"] = AuthorContractId;
                }
                if (PublishingCompanyId != "")
                {
                    TempData["PublishingCompanyId"] = PublishingCompanyId;
                }
                if (ProductLicenseId != "")
                {
                    TempData["ProductLicenseId"] = ProductLicenseId;
                }
                if (OutboundId != "")
                {
                    TempData["OutboundId"] = OutboundId;
                }
                TempData["ProuductId"] = ProuductId;
                return View("PermissionsOutboundPaymentTagging");
            }
        }





        public IList<PermissionOutBoundSearch> GetPermissionOutBoundExcelList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionOutBoundSearch>("Proc_PermissionsOutboundSerch_get", parameters).ToList();
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public PermissionOutBoundSearch GetPermissionOutBoundExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<PermissionOutBoundSearch>("Proc_PermissionsOutboundSearchParametterReturn_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public ActionResult exportToExcelProductList(PermissionOutBoundSearch _mobjOtherContract)
        {
            try
            {


                List<PermissionOutBoundSearch> _mobjReportList = new List<PermissionOutBoundSearch>();
                PermissionOutBoundSearch _mobjParametertList = new PermissionOutBoundSearch();


                _mobjReportList = GetPermissionOutBoundExcelList(_mobjOtherContract.SessionId).ToList();

                _mobjParametertList = GetPermissionOutBoundExcelParameterList(_mobjOtherContract.SessionId);




                string sFileName = "PermissionOutBound_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Permission OutBound Report</b>" + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Permissions Outbound Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");




                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");

                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td><b>Author Name</b></td>");



                    mstr_searchparameter.Append("<td ><b>Contract Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>Licensee Name</b></td>");

                    mstr_searchparameter.Append("<td><b>Licensee Code</b></td>");
                    mstr_searchparameter.Append("<td><b>Licensee Name</b></td>");
                    mstr_searchparameter.Append("<td><b>Contact Person</b></td>");
                    mstr_searchparameter.Append("<td><b>Address</b></td>");
                    mstr_searchparameter.Append("<td><b>Country</b></td>");
                    mstr_searchparameter.Append("<td><b>State</b></td>");
                    mstr_searchparameter.Append("<td><b>City</b></td>");
                    mstr_searchparameter.Append("<td><b>PIN Code</b></td>");
                    mstr_searchparameter.Append("<td><b>Mobile</b></td>");
                    mstr_searchparameter.Append("<td><b>Email</b></td>");
                    mstr_searchparameter.Append("<td><b>URL</b></td>");
                    mstr_searchparameter.Append("<td><b>Request Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Licensee Publication Title</b></td>");
                    mstr_searchparameter.Append("<td><b>Request Material</b></td>");
                    mstr_searchparameter.Append("<td><b>Type of Rights</b></td>");
                    mstr_searchparameter.Append("<td><b>Will Be Material Me Translated</b></td>");
                    mstr_searchparameter.Append("<td><b>Will Be Material Be Adepted</b></td>");
                    mstr_searchparameter.Append("<td><b>Language</b></td>");
                    mstr_searchparameter.Append("<td><b>Extent</b></td>");
                    mstr_searchparameter.Append("<td><b>Territory Rights</b></td>");
                    mstr_searchparameter.Append("<td><b>Date of Invoice</b></td>");
                    mstr_searchparameter.Append("<td><b>Invoice Applicable</b></td>");
                    mstr_searchparameter.Append("<td><b>Invoice Number</b></td>");
                    mstr_searchparameter.Append("<td><b>Invoice Currency</b></td>");
                    mstr_searchparameter.Append("<td><b>Invoice Value</b></td>");
                    mstr_searchparameter.Append("<td><b>Invoice Description</b></td>");
                    mstr_searchparameter.Append("<td><b>Copies To Be Received</b></td>");
                    mstr_searchparameter.Append("<td><b>No of Copies</b></td>");
                    mstr_searchparameter.Append("<td><b>Remarks</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Status</b></td>");
                    mstr_searchparameter.Append("<td><b>Payment Received</b></td>");
                    mstr_searchparameter.Append("<td><b>PaymentCurrency</b></td>");
                    mstr_searchparameter.Append("<td><b>Payment Amount</b></td>");
                    mstr_searchparameter.Append("<td><b>Agreement Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Signed Contract Sent Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Signed Contract Received Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Cancellation Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Cancellation Reason</b></td>");
                    mstr_searchparameter.Append("<td><b>Contributor Agreement</b></td>");
                    mstr_searchparameter.Append("<td><b>PendingRemarks</b></td>");
                    mstr_searchparameter.Append("<td><b>Effective Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Period In Month</b></td>");
                    mstr_searchparameter.Append("<td><b>Expiry Date</b></td>");


                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (PermissionOutBoundSearch data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PermissionsOutboundCode + "</td>");



                        mstr_searchparameter.Append("<td align='left'>" + data.ISBN + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + data.WorkingSubProduct + "</span>" + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");



                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeName + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Country + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.State + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RequestDateView + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseePublicationTitle + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RequestMaterial + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TypeOfRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Will_be_material_be_translated + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Will_be_material_be_adepted + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Language + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Extent + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TerritoryRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DateOfInvoice + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceApplicable + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceNumber + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceCurrency + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceValue + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InvoiceDescription + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Copies_To_Be_Received + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NoOfCopies + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractStatus + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PaymentReceived + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PaymentCurrency + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PaymentAmount + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Date_of_agreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Signed_Contract_sent_date + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Signed_Contract_receiveddate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CancellationDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Cancellation_Reason + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contributor_Agreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PendingRemarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Effectivedate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contractperiodinmonth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ExpiryDateView + "</td>");



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