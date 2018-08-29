using ACS.Core;
using ACS.Services.Security;
using ACS.Web.Framework.Controllers;
using SLV.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SLV.Model.AuthorContract;
using ACS.Services.AuthorContract;
using ACS.Core.Domain.AuthorContract;
using System.Text;
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Services.Product;


namespace SLV.Web.Areas.Contract.Controllers
{
    public class AuthorContractController : BasePublicController
    {
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;
        private readonly IEncryptionService _encryptionService;
        private readonly IProductLicenseService _ProductLicenseService;
        public AuthorContractController(IWorkContext workContext, IEncryptionService encryptionService, IDbContext dbContext, IProductLicenseService ProductLicenseService
         )
        {

            _workContext = workContext;
            _encryptionService = encryptionService;
            _dbContext = dbContext;
            _ProductLicenseService = ProductLicenseService;
             
        }
        //
        // GET: /Contract/AuthorContract/
        public ActionResult Index(string SeriesIds, int? ProductId = 0, int? Id = 0, int? LicenceId = 0, string SeriesCode = "", string For = "")
        {

            if (ProductId == 0 && LicenceId !=0)
            {
                int _LicenceId = LicenceId.GetValueOrDefault();
                ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = _ProductLicenseService.GetProductLicenseById(_LicenceId);

                ViewBag.ProductId = mobj_ProductLicense.productid;
            }
            else
            {
                ViewBag.ProductId = ProductId;
            }


           
            ViewBag.ContractIdId = Id;
            ViewBag.LicenceId = LicenceId;
            ViewBag.SeriesIds = SeriesIds;
            ViewBag.SeriesCode = SeriesCode;

            if (For != "")
            {
                if (For.ToLower() == "update")
                {
                    ViewBag.Update = For.ToLower();
                }
                if (For.ToLower() == "seriesupdate")
                {
                    ViewBag.Update = For.ToLower();
                }
            }
            else
            {
                ViewBag.Update = "dashboard";
            }

            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                // return new HttpUnauthorizedResult();
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View(_workContext.CurrentUser);
            }
        }


        //public ActionResult Index(int? ProductId = 0, int? Id = 0)
        //{
        //    ViewBag.ProductId = ProductId;
        //    ViewBag.ContractIdId = Id;
        //    if (_workContext.CurrentUser == null)
        //    {
        //        // return new HttpUnauthorizedResult();
        //        TempData["From"] = "S";
        //        return RedirectToAction("Login", "Login", new { area = "" });
        //    }
        //    else
        //    {
        //        return View(_workContext.CurrentUser);
        //    }
        //}


        public ActionResult View(string For = "", int? Id = 0, string SeriesCode = "", int? LicenceId = 0, string type = "", int AddendumId = 0, string Addendum = "")
        {
            ViewBag.ContractIdId = Id;
            ViewBag.SeriesCode = SeriesCode;
            ViewBag.LicenceId = LicenceId;

            if (AddendumId != 0)
            {
                TempData["AddendumIdForView"] = AddendumId;
            }

            if (Addendum != "")
            {
                TempData["AddendumNew"] = Addendum.ToLower();
            }

            if (For != "")
            {
                if (For.ToLower() == "view")
                {
                    ViewBag.View = For.ToLower();
                }
                else if (For.ToLower() == "seriesview")
                {
                    ViewBag.View = For.ToLower();
                }
                else if (For.ToLower() == "update")
                {
                    ViewBag.Update = For.ToLower();
                }
                else if (For.ToLower() == "seriesupdate")
                {
                    ViewBag.Update = For.ToLower();
                }
                else
                {
                    TempData["Addendum"] = For;
                }
            }
            else
            {
                ViewBag.View = "dashboard";
                TempData["Addendum"] = null;
            }

            if (type != "")
            {
                TempData["ExpiryReport"] = type;
            }

            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                // return new HttpUnauthorizedResult();
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                return View("ContractView", _workContext.CurrentUser);
            }
        }


     



        public ActionResult GetPassword(string password)
        {
            return Content(_encryptionService.DecryptText(password, "5152549987117761").ToString());
        }

        //Added by Saddam on 27/06/2016
        public ActionResult AuthorContractSearch(string Back, string For = "", string ViewMore = "")
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
                    if (For.ToLower() == "list")
                    {
                        TempData["List"] = For.ToLower();
                    }

                    if (For.ToLower() == "serieslist")
                    {
                        TempData["List"] = For.ToLower();
                    }

                    if (For.ToLower() == "delete")
                    {
                        TempData["delete"] = For.ToLower();
                    }
                }
                else
                {
                    TempData["contractsearch"] = "contractsearch";
                }

                if (For == "Rights")
                {
                    //Added By Ankush Dated 22/09/2016
                    var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                    {
                        TempData["From"] = "You have no rights to access this page";
                        return RedirectToAction("Login", "Login", new { area = "" });

                    }
                    //End By Ankush

                    TempData["Rights"] = For;
                    if (Back == "BackToserach")
                    {
                        TempData["BackToList"] = Back;
                    }
                }
                else if (For == "PermissionsOutbound")
                {
                    //Added By Ankush Dated 22/09/2016
                    var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                    {
                        TempData["From"] = "You have no rights to access this page";
                        return RedirectToAction("Login", "Login", new { area = "" });

                    }
                    //End By Ankush

                    TempData["PermissionsOutbound"] = For;

                    if (Back == "BackToserach")
                    {
                        TempData["BackToList"] = Back;
                    }
                }
                else if (For == "PermissionsInbound")
                {
                    //Added By Ankush Dated 22/09/2016
                    var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                    if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "ed" && obj_department.ToString() != "sa")
                    {
                        TempData["From"] = "You have no rights to access this page";
                        return RedirectToAction("Login", "Login", new { area = "" });

                    }
                    //End By Ankush

                    TempData["PermissionsInbound"] = For;

                    if (Back == "BackToserach")
                    {
                        TempData["BackToList"] = Back;
                    }
                }
                else if (For == "Addendum")
                {
                    TempData["Addendum"] = For;

                    if (Back == "BackToserach")
                    {
                        TempData["BackToList"] = Back;
                    }
                }
                else if (For.ToLower() == "report")
                {
                    ViewBag.Report = For.ToLower();
                }
                else if (Back == "BackToserach")
                {
                    TempData["BackToList"] = Back;
                }

                if (ViewMore != "")
                {
                    ViewBag.ViewMore = ViewMore;
                }

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode;
                //ViewBag.Id = Id;
                //ViewBag.typeId = typeId;
                return View(_workContext.CurrentUser);
            }
        }
        //ended by Saddam



        public IList<SLV.Model.AuthorContract.AuthorContractSearchmodel> GetAuthorContractExcelList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_AuthorContractDetails_get", parameters).ToList();

                    if (_GetAuthorReport.Count == 0)
                    {
                        parameters = new SqlParameter[1];
                        parameters[0] = new SqlParameter("sessionId", SqlDbType.VarChar, 200);
                        parameters[0].Value = "'" + SessionId + "'";

                        _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_AuthorContractDetailsByChild_get", parameters).ToList();
                    }

                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public AuthorContractSearchmodel GetAuthorContractExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorContractSearchmodel>("Proc_AuthorContractDetailsParametterReturn_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public ActionResult exportToExcelProductList(AuthorContractSearchmodel _mobjOtherContract, string SeriesId = "", string SeriesName = "", string For = "")
        {
            try
            {


                List<AuthorContractSearchmodel> _mobjReportList = new List<AuthorContractSearchmodel>();
                AuthorContractSearchmodel _mobjParametertList = new AuthorContractSearchmodel();

                if (SeriesName == "")
                {
                    _mobjReportList = GetAuthorContractExcelList(_mobjOtherContract.SessionId).ToList();
                    _mobjParametertList = GetAuthorContractExcelParameterList(_mobjOtherContract.SessionId);
                }
                else
                {
                    _mobjReportList = GetAuthorContractSeriesExcelList(SeriesId, For).ToList();
                }





                string sFileName = "AuthorContract_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Author Contract Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    if (SeriesName == "")
                    {
                        if (!String.IsNullOrEmpty(_mobjParametertList.ReturnList))
                        {
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + _mobjParametertList.ReturnList + "</td>");
                            mstr_searchparameter.Append("</tr>");
                        }
                    }
                    else
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + SeriesName + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Author Contract Code</b></td>");
                    mstr_searchparameter.Append("<td><b>Series Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Title</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Sub Title</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Author Name(s)</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Date</b></td>");
                    mstr_searchparameter.Append("<td><b>No of Authors</b></td>");
                    mstr_searchparameter.Append("<td><b>Price Type</b></td>");
                    mstr_searchparameter.Append("<td><b>Terms of Copyright</b></td>");
                    mstr_searchparameter.Append("<td><b>Buy Back</b></td>");
                    mstr_searchparameter.Append("<td><b>Nature of Work</b></td>");
                    mstr_searchparameter.Append("<td><b>Copyright Owner</b></td>");
                    mstr_searchparameter.Append("<td><b>Territory Rights</b></td>");
                    mstr_searchparameter.Append("<td><b>Third Party Permission</b></td>");
                    mstr_searchparameter.Append("<td><b>Contributors</b></td>");
                    mstr_searchparameter.Append("<td><b>Amendment</b></td>");
                    mstr_searchparameter.Append("<td><b>Amendment Remarks</b></td>");
                    mstr_searchparameter.Append("<td><b>Restriction</b></td>");
                    mstr_searchparameter.Append("<td><b>subject Matter And Treatment</b></td>");
                    mstr_searchparameter.Append("<td><b>Min No of words</b></td>");
                    mstr_searchparameter.Append("<td><b>Max No of words</b></td>");
                    mstr_searchparameter.Append("<td><b>Min No of Pages</b></td>");
                    mstr_searchparameter.Append("<td><b>Max No of Pages</b></td>");
                    mstr_searchparameter.Append("<td><b>Material to be Supplied</b></td>");
                    mstr_searchparameter.Append("<td><b>Currency Name</b></td>");
                    mstr_searchparameter.Append("<td><b>Price</b></td>");
                    mstr_searchparameter.Append("<td><b>Medium of Delivery</b></td>");
                    mstr_searchparameter.Append("<td><b>Manuscript Delivery Format</b></td>");
                    mstr_searchparameter.Append("<td><b>Delivery Schedule</b></td>");
                    mstr_searchparameter.Append("<td><b>Product Remarks</b></td>");
                    mstr_searchparameter.Append("<td><b>Series Name</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Status</b></td>");
                    mstr_searchparameter.Append("<td><b>Date of Agreement</b></td>");
                    mstr_searchparameter.Append("<td><b>Signed Contract Sent Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Signed Contract Received</b></td>");
                    mstr_searchparameter.Append("<td><b>Author Copies Sent Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Contributor Copies Sent Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Cancellation Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Cancellation Reason</b></td>");
                    mstr_searchparameter.Append("<td><b>Remarks</b></td>");
                    mstr_searchparameter.Append("<td><b>Effective Date</b></td>");
                    //mstr_searchparameter.Append("<td><b>Period of Agreement</b></td>");
                    mstr_searchparameter.Append("<td><b>Expiry Date</b></td>");

                    List<List<string>> AuthorList = new List<List<string>>();
                    List<List<string>> typenameList = new List<List<string>>();
                    List<List<string>> subsiadirynameList = new List<List<string>>();
                    
                    List<string> Authors;
                    List<string> typename;
                    List<string> subsiadiryname;
                    if (_mobjReportList.Count > 0)
                    {
                        for (int i = 0; i < _mobjReportList.Count; i++)
                        {
                            if (_mobjReportList[i].AuthorData != null && _mobjReportList[i].AuthorData != "")
                            {
                                int AuthorNo = 0;
                                string[] AuthorSaperations = _mobjReportList[i].AuthorData.Split('~');
                                foreach (string AuthorSaperation in AuthorSaperations)
                                {
                                    AuthorNo++;
                                    string[] AuthorDetails = AuthorSaperation.Split('!');
                                    string LeftDetails = AuthorDetails[1];
                                    if (AuthorNo > AuthorList.Count)
                                    {
                                        Authors = new List<string>();
                                        typename = new List<string>();
                                        subsiadiryname = new List<string>();
                                        AuthorList.Add(Authors);
                                        typenameList.Add(typename);
                                        subsiadirynameList.Add(subsiadiryname);
                                    }
                                    AuthorList[AuthorNo - 1].Add("Author" + AuthorNo);

                                    string[] Details = LeftDetails.Split('^');
                                    string[] Slabs = Details[1].Split('$');
                                    string slab = Slabs[1];
                                    string subsidiry = Slabs[0];

                                    if (slab != "")
                                    {
                                        string[] slabData = slab.Split(',');
                                        foreach (string s in slabData)
                                        {
                                            string[] slabvalue = s.Split('#');
                                            for (int k = 0; k < slabvalue.Length; k = k + 2)
                                            {
                                                typenameList[AuthorNo - 1].Add(slabvalue[k].ToString());
                                                //typename.Add(slabvalue[k].ToString());
                                            }
                                        }
                                    }

                                    if (subsidiry != "")
                                    {
                                        string[] subsidiryData = subsidiry.Split(',');
                                        foreach (string s in subsidiryData)
                                        {
                                            string[] subsidiryvalue = s.Split('#');
                                            for (int k = 0; k < subsidiryvalue.Length; k = k + 2)
                                            {
                                                subsiadirynameList[AuthorNo - 1].Add(subsidiryvalue[k].ToString());
                                                //subsiadiryname.Add(subsidiryvalue[k].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    for (int k = 0; k < AuthorList.Count; k++)
                    {

                        List<string> distinctauthors = AuthorList[k].Distinct().ToList();
                        if (distinctauthors.Count > 0)
                        {
                            foreach (string author in distinctauthors)
                            {
                                mstr_searchparameter.Append("<td ><b>" + author + " </b></td>");
                            }
                        }

                        mstr_searchparameter.Append("<td ><b>Contract Type</b></td>");
                        mstr_searchparameter.Append("<td ><b>Type</b></td>");
                        mstr_searchparameter.Append("<td ><b>Payment Period</b></td>");
                        mstr_searchparameter.Append("<td ><b>Author Copies</b></td>");
                        mstr_searchparameter.Append("<td ><b>Seed Money</b></td>");
                        mstr_searchparameter.Append("<td ><b>One-Time Payment</b></td>");
                        mstr_searchparameter.Append("<td ><b>Advance Royalty</b></td>");


                        List<string> distincttypename = typenameList[k].Distinct().ToList();
                        if (distincttypename.Count > 0)
                        {
                            foreach (string Typenames in distincttypename)
                            {
                                mstr_searchparameter.Append("<td ><b>" + Typenames + " </b></td>");     //Typenames.ToUpper()
                            }
                        }

                        List<string> distinctsubsiadiryname = subsiadirynameList[k].Distinct().ToList();
                        if (distinctsubsiadiryname.Count > 0)
                        {
                            foreach (string subsiadiry in distinctsubsiadiryname)
                            {
                                mstr_searchparameter.Append("<td ><b>" + subsiadiry + " </b></td>");    //subsiadiry.ToUpper()
                            }
                        }
                    }





                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    int Count_value = 0;
                    foreach (AuthorContractSearchmodel data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorContractCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SeriesCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingTitle + "<br />"+ "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingSubProduct + "<br />" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.oupisbn == null || data.oupisbn == "" ? "" : Convert.ToString("&nbsp;" + data.oupisbn)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractEntryDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NoOfAuthors + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PriceType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TermsOfCopyright + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BuyBack + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NatureOfWork + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CopyrightOwner + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TerritoryRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ThirdPartyPermission + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contributors + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Amendment + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AmendmentRemarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Restriction + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.subjectMatterAndTreatment + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MinNoOfwords + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MaxNoOfwords + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MinNoOfPages + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MaxNoOfPages + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MaterialtobeSupplied + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CurrencyName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Price + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.MediumOfdelivery + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ManuscriptDeliveryFormat + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Deliveryschedule + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductRemarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SeriesName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractDate + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + (data.Contractstatus == "Pending" ? "Issued" : (data.Contractstatus == "Issued" ? "Received" : data.Contractstatus)) + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.DateOfAgreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SignedContractsentdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SignedContractreceived + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Authorcopiessentdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Contributorcopiessentdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Cancellationdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Cancellationreason + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.effectiveDate + "</td>");
                        //mstr_searchparameter.Append("<td align='left'>" + data.periodofagreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractExpiryDate + "</td>");



                        if (_mobjReportList[Count_value].AuthorData != null && _mobjReportList[Count_value].AuthorData != "")
                        {
                            int AuthorNo = 0;
                            string[] AuthorSaperations = _mobjReportList[Count_value].AuthorData.Split('~');
                            foreach (string AuthorSaperation in AuthorSaperations)
                            {
                                AuthorNo++;
                                string[] AuthorDetails = AuthorSaperation.Split('!');
                                string LeftDetails = AuthorDetails[1];

                                mstr_searchparameter.Append("<td align='left'>" + AuthorDetails[0] + "</td>");

                                string[] Details = LeftDetails.Split('^');

                                string[] OtherData = Details[0].Split('*');
                                foreach (string s in OtherData)
                                {
                                    mstr_searchparameter.Append("<td align='left'>" + s.Replace("–", " ").Replace("-"," ") + "</td>");
                                }

                                string[] Slabs = Details[1].Split('$');
                                string slab = Slabs[1];
                                string subsidiry = Slabs[0];

                                List<string> distincttypename = typenameList[AuthorNo - 1].Distinct().ToList();

                                if (distincttypename.Count > 0)
                                {
                                    for (int j = 0; j < distincttypename.Count; j++)
                                    {
                                        if (slab != "")
                                        {
                                            string slabdata = "";
                                            string[] slabData = slab.Split(',');

                                            foreach (string s in slabData)
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

                                List<string> distinctsubsiadiryname = subsiadirynameList[AuthorNo - 1].Distinct().ToList();

                                if (distinctsubsiadiryname.Count > 0)
                                {
                                    for (int j = 0; j < distinctsubsiadiryname.Count; j++)
                                    {
                                        if (subsidiry != "")
                                        {
                                            string subsidirydata = "";
                                            string[] subsidiryData = subsidiry.Split(',');

                                            foreach (string s in subsidiryData)
                                            {
                                                string[] subsidiryvalue = s.Split('#');
                                                for (int k = 0; k < subsidiryvalue.Length; k = k + 2)
                                                {
                                                    if (subsidiryvalue[k].ToString() == distinctsubsiadiryname[j].ToString())
                                                        subsidirydata += subsidiryvalue[k + 1].ToString() + ", ";
                                                }
                                            }
                                            if (subsidirydata.Length > 0)
                                                subsidirydata = subsidirydata.Substring(0, subsidirydata.Length - 2);
                                            mstr_searchparameter.Append("<td align='left'>" + subsidirydata + "</td>");
                                        }
                                        else
                                            mstr_searchparameter.Append("<td align='left'></td>");
                                    }
                                }
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


        public IList<SLV.Model.AuthorContract.AuthorContractSearchmodel> GetAuthorContractSeriesExcelList(String SeriesId, String For = "")
        {
            try
            {
                if (SeriesId == "")
                {
                    return null;
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("SeriesId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SeriesId + "'";
                    parameters[1] = new SqlParameter("For", SqlDbType.VarChar, 200);
                    parameters[1].Value = "'" + For + "'";

                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<SLV.Model.AuthorContract.AuthorContractSearchmodel>("Proc_GetProductSeriesContractList_Report_Get", parameters).ToList();
                    
                    return _GetAuthorReport;

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

	}
}