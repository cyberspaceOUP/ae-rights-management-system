using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using ACS.Core;

using ACS.Services.Master;

using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using SLV.Model.RightsSelling;
using System.Web;
using System.Web.Mvc;
using System.Text;
namespace SLV.Web.Areas.RightsSelling.Controllers
{
    public class RightsSellingController : Controller
    {
        //
        // GET: /RightsSelling/RightsSelling/

        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;

        public RightsSellingController(
            IWorkContext workContext
            , IDbContext dbContext
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult RightsSellingMaster()
        //{
        //    return View();
        //}


        public ActionResult RightsSellingMaster(int? Id, string type)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
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

                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }


                return View("RightsSellingMaster");
            }
            // return View();
        }

        public ActionResult RightsSellingUpdate(int? Id, string type, int? RightsSellingId)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
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
                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }
                if (RightsSellingId != null)
                {
                    ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["User"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    TempData["RightsSellingId"] = RightsSellingId;
                }

                return View("RightsSellingUpdate");
            }
            // return View();
        }

        public ActionResult RightsSellingSearch(string For = "", string ViewMore = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (For != "")
                {
                    if (For.ToLower() == "report")
                    {
                        ViewBag.Report = For.ToLower();
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
                    else if (For.ToLower() == "backtolist")
                    {
                        TempData["BackToList"] = For.ToLower();
                    }

                }
                if (ViewMore != "")
                {
                    ViewBag.ViewMore = ViewMore;
                }
                TempData["DepartmentId"] = _workContext.CurrentUser.Id;
                TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                return View("RightsSellingSearch");
            }
        }


        public ActionResult RightsSellingView(int? Id, string type, int? RightsSellingId, string For)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (type != null)
                {
                    TempData["type"] = type.FirstOrDefault();
                    TempData["id"] = Id;
                    TempData["ProductId"] = type.Remove(0, 1);
                }
                if (RightsSellingId != null)
                {
                    TempData["RightsSellingId"] = RightsSellingId;
                }

                if (For != null)
                {
                    TempData["For"] = For;
                }

                return View("RightsSellingView");
            }
            // return View();
        }

        public ActionResult RightsSellingPaymentTagging(string AuthorId = "", string AuthorContractId = "", string PublishingCompanyId = "", string ProductLicenseId = "", string RightsSellingId = "", int ProuductId  =0)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
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
                if (RightsSellingId != "")
                {
                    TempData["RightsSellingId"] = RightsSellingId;
                }
                TempData["ProuductId"] = ProuductId;
                return View("RightsSellingPaymentTagging");
            }
        }




        public IList<RightsSellingSearchModel> GetRightsSellingExcelList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingSearchModel>("Proc_RightsSellingSearch_get", parameters).ToList();
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public RightsSellingSearchModel GetRightsSellingExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<RightsSellingSearchModel>("Proc_RightsSellingParameterSearch_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public ActionResult exportToExcelRightsSellingList(RightsSellingSearchModel _mobjOtherContract)
        {
            try
            {


                List<RightsSellingSearchModel> _mobjReportList = new List<RightsSellingSearchModel>();
                RightsSellingSearchModel _mobjParametertList = new RightsSellingSearchModel();

                _mobjReportList = GetRightsSellingExcelList(_mobjOtherContract.SessionId).ToList();
                _mobjParametertList = GetRightsSellingExcelParameterList(_mobjOtherContract.SessionId);

                

                string sFileName = "RightsSelling_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Rights Selling Report</b>" + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Right Sales Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");


                    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                    mstr_searchparameter.Append("<td ><b>Contract Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>Product License Code</b></td>");
                    mstr_searchparameter.Append("<td><b>Requested Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Date of Expiry</b></td>");
                    mstr_searchparameter.Append("<td ><b>Licensee Name</b></td>");

                    mstr_searchparameter.Append("<td ><b>Licensee Code </b></td>");

                    mstr_searchparameter.Append("<td ><b>Contact Person </b></td>");
                    mstr_searchparameter.Append("<td ><b>Address </b></td>");
                    mstr_searchparameter.Append("<td ><b>Country </b></td>");
                    mstr_searchparameter.Append("<td ><b>State </b></td>");
                    mstr_searchparameter.Append("<td ><b>City </b></td>");
                    mstr_searchparameter.Append("<td ><b>PIN Code </b></td>");
                    mstr_searchparameter.Append("<td ><b>Mobile </b></td>");
                    mstr_searchparameter.Append("<td ><b>Email </b></td>");
                    mstr_searchparameter.Append("<td ><b>URL </b></td>");
                    mstr_searchparameter.Append("<td ><b> Request Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Will Be Material Be Translated </b></td>");
                    mstr_searchparameter.Append("<td ><b>Language </b></td>");
                    mstr_searchparameter.Append("<td ><b> Print Run Quantity Allowed </b></td>");
                    mstr_searchparameter.Append("<td ><b>Number of Impression </b></td>");
                    mstr_searchparameter.Append("<td ><b>Advance Payment </b></td>");
                    mstr_searchparameter.Append("<td ><b>Currency </b></td>");
                    mstr_searchparameter.Append("<td ><b>Payment Term </b></td>");
                    mstr_searchparameter.Append("<td ><b>Payment Amount </b></td>");
                    mstr_searchparameter.Append("<td ><b>Territory  </b></td>");
                    mstr_searchparameter.Append("<td ><b> Advance Royalty Amount</b></td>");
                    mstr_searchparameter.Append("<td ><b> Royalty Recurring</b></td>");
                    mstr_searchparameter.Append("<td ><b>Recurring From Period </b></td>");
                    mstr_searchparameter.Append("<td ><b>Recurring To Period </b></td>");
                    mstr_searchparameter.Append("<td ><b> Status</b></td>");
                    mstr_searchparameter.Append("<td ><b> Remarks</b></td>");
                    mstr_searchparameter.Append("<td ><b>Royalty Type </b></td>");
                    mstr_searchparameter.Append("<td ><b> Frequency</b></td>");


                    mstr_searchparameter.Append("<td ><b>Contract Status </b></td>");
                    mstr_searchparameter.Append("<td ><b>Date of Agreement </b></td>");
                    mstr_searchparameter.Append("<td ><b>Signed Contract Sent Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Signed Contract Received Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Cancellation Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Cancellation Reason </b></td>");
                    mstr_searchparameter.Append("<td ><b>Contributor Agreement </b></td>");
                    mstr_searchparameter.Append("<td ><b>Pending Remarks </b></td>");
                    mstr_searchparameter.Append("<td ><b>Effective Date </b></td>");
                    mstr_searchparameter.Append("<td ><b>Contract Period In Month </b></td>");

                    List<string> typename = new List<string>();
                    int typenamecounter = 0;
                    if (_mobjReportList.Count > 0)
                    {
                        for (int i = 0; i < _mobjReportList.Count; i++)
                        {
                            if (_mobjReportList[i].RoyaltySlab != null)
                            {
                                string[] slab = _mobjReportList[i].RoyaltySlab.Split(',');
                                foreach (string s in slab)
                                {
                                    string[] slabvalue = s.Split('#');
                                    for (int k = 0; k < slabvalue.Length; k++)
                                    {
                                        if (k % 2 == 0)
                                        {
                                            typename.Add(slabvalue[k].ToString());
                                            typenamecounter++;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    List<string> distincttypename = typename.Distinct().ToList();
                    if (distincttypename.Count > 0)
                    {
                        foreach (string Typenames in distincttypename)
                        {
                            mstr_searchparameter.Append("<td ><b>" + Typenames.ToUpper() + " </b></td>");
                        }
                    }

                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    int Count_value = 0;
                    foreach (RightsSellingSearchModel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RightsSellingCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ISBN + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "<br />" + "<span style='font-size:11px;'>" + data.WorkingSubProduct + "</span>" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorContractCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductLicensecode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RequestDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DateContract + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DateExpiry + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OrganizationName + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.Licenseecode + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Country + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.State + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicenseeMobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.URL + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RequestDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Will_be_material_be_translated + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Language + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Print_Run_Quantity_Allowed + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Number_of_Impression_Allowed + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Advance_Payment + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Payment_Term + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Payment_Amount + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TerritoryRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Advance_Royalty_Amount + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Royalty_Recurring + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Recurring_From_Period + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Recurring_To_Period + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Status + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RoyaltyType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Frequency + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractStatus + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Date_of_agreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Signed_Contract_sent_date + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Signed_Contract_receiveddate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CancellationDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Cancellation_Reason + "</td>");



                        mstr_searchparameter.Append("<td align='left'>" + data.Contributor_Agreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pending_Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Effectivedate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contractperiodinmonth + "</td>");

                        //if (_mobjReporRoyaltytList.Count > 0)
                        //{
                        //    for (int j = 0; j < distincttypename.Count; j++)
                        //    {
                        //        string slabdata = "";
                        //        if (_mobjReporRoyaltytList[Count_value].RoyaltySlab != null)
                        //        {
                        //            string[] slab = _mobjReporRoyaltytList[Count_value].RoyaltySlab.Split(',');
                        //            foreach (string s in slab)
                        //            {
                        //                string[] slabvalue = s.Split('#');

                        //                for (int k = 0; k < slabvalue.Length; k = k + 2)
                        //                {
                        //                    if (slabvalue[k].ToString() == distincttypename[j].ToString())
                        //                        slabdata += slabvalue[k + 1].ToString() + ", ";

                        //                }
                        //            }
                        //            if (slabdata.Length > 0)
                        //                slabdata = slabdata.Substring(0, slabdata.Length - 2);
                        //            mstr_searchparameter.Append("<td align='left'>" + slabdata + "</td>");
                        //        }
                        //        else
                        //            mstr_searchparameter.Append("<td align='left'></td>");
                        //    }

                        //}


                        if (distincttypename.Count > 0)
                        {
                            for (int j = 0; j < distincttypename.Count; j++)
                            {
                                string slabdata = "";
                                if (_mobjReportList[Count_value].RoyaltySlab != null)
                                {
                                    string[] slab = _mobjReportList[Count_value].RoyaltySlab.Split(',');
                                    foreach (string s in slab)
                                    {
                                        string[] slabvalue = s.Split('#');

                                        for (int k = 0; k < slabvalue.Length; k = k + 2)
                                        {
                                            if (slabvalue[k].ToString() == distincttypename[j].ToString())
                                                slabdata += slabvalue[k + 1].ToString() + ", ";

                                        }
                                    }
                                    if (slabdata.Length > 0)
                                        slabdata = slabdata.Substring(0, slabdata.Length - 2);
                                    mstr_searchparameter.Append("<td align='left'>" + slabdata + "</td>");
                                }
                                else
                                    mstr_searchparameter.Append("<td align='left'></td>");
                            }

                        }
                        


                        mstr_searchparameter.Append("</tr>");
                        mint_Counter++;
                        Count_value++;
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