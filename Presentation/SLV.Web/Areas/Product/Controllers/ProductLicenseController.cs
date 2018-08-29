using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ACS.Core;
using ACS.Services.Master;
using ACS.Core.Domain.Product;

using System.Data;
using System.Data.SqlClient;
using SLV.Model.Common;
using ACS.Data;
using System.Text;
using ACS.Services.Product;




namespace SLV.Web.Areas.Product.Controllers
{
    public class ProductLicenseController : Controller
    {
        //

          private readonly IWorkContext _workContext;
          private readonly IDbContext _dbContext;
          private readonly IProductLicenseService _ProductLicenseService;

          public ProductLicenseController(
            IWorkContext workContext
              , IDbContext dbContext
              , IProductLicenseService ProductLicenseService
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
            _ProductLicenseService = ProductLicenseService;
        }
        // GET: /Product/ProductLicense/
        public ActionResult Index()
        {
            return View();
        }


        // GET: /Product/ProductLicense/
        public ActionResult ProductLicense(int Id)
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

                ViewBag.LicenseId = 0;
                ViewBag.ProductId = Id;
                ViewBag.typeId = 0;

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                return View();
            }
            // return View();
        }

        public ActionResult ProductLicenseSearch(string Back, string For = "", string ViewMore = "", string Show = "")
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
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
                else if (For.ToLower() == "report")
                {
                    ViewBag.Report = For.ToLower();
                }
                else if (For == "BackToserach")
                {
                 TempData["BackToList"] = For;
                }
                else if (For.ToLower() == "update")
                {
                    TempData["Action"] = For.ToLower();
                }
                else if (For.ToLower() == "view")
                {
                    TempData["Action"] = For.ToLower();
                }

                else if (For.ToLower() == "delete")
                {
                    TempData["Action"] = For.ToLower();
                }
                //else if (For.ToLower() == "view")
                //{
                //    ViewBag.View = "1";
                //}

                //ViewBag.Id = Id;
                //ViewBag.typeId = typeId;
                if (ViewMore != "")
                {
                    ViewBag.ViewMore = ViewMore;
                }

                if (Show == "dashboard")
                {
                    ViewBag.Show = Show.ToLower();
                }
                else
                {
                    ViewBag.Show = "";
                }

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode;

                return View();
            }
        }


        public ActionResult UpdateProductLicense(int Id)
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

                ViewBag.LicenseId = Id;
                ViewBag.ProductId = 0;

                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();

                if (_workContext.CurrentUser.DepartmentM.DepartmentCode == "AD" || _workContext.CurrentUser.DepartmentM.DepartmentCode == "SA" || _workContext.CurrentUser.DepartmentM.DepartmentCode == "RT")
                {
                    TempData["BackToserach"] = "BackToserach";
                    return View("ProductLicense");
                }
                else {
                    TempData["BackToserach"] = "BackToserach";
                    return View("ProductLicenseView");
                }
                
            }
        }
        public ActionResult View(int Id)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.LicenseId = Id;

                if (Id != 0)
                {
                    int _LicenceId = Id;
                    ACS.Core.Domain.Product.AddendumDetails mobj_ProductLicense = _ProductLicenseService.GetAddendumDetailById(_LicenceId);

                    if (mobj_ProductLicense != null)
                        ViewBag.AddendumId = mobj_ProductLicense.Id;
                }

                ViewBag.ProductId = 0;
                if (_workContext.CurrentUser.DepartmentM.DepartmentCode == "AD" || _workContext.CurrentUser.DepartmentM.DepartmentCode == "SA")
                {
                    //TempData["BackToserach"] = "BackToserach";
                    //return View("ProductLicense");
                    return View("ViewOnly");
                }
                else
                {
                    TempData["BackToserach"] = "BackToserach";
                    return View("ViewOnly");
                }

            }
        }

        public ActionResult DeleteProductLicense(int Id)
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush

                ViewBag.LicenseId = Id;
                ViewBag.ProductId = 0;
                if (_workContext.CurrentUser.DepartmentM.DepartmentCode == "AD" || _workContext.CurrentUser.DepartmentM.DepartmentCode == "SA")
                {
                    TempData["BackToserach"] = "BackToserach";
                    return View("ProductLicenseDelete");
                }
                else
                {
                    TempData["BackToserach"] = "BackToserach";
                    return View("ProductLicense");
                }

            }
        }

        public ActionResult LicenseAddendumEntry(int Id)
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
                TempData["BackToserach"] = "BackToserach";
                ViewBag.LicenseId = Id;
                ViewBag.AddendumId = 0;
                return View("AddendumEntry");

            }
        }

        [HttpPost]
        public ActionResult LicenseAddendumEntry()
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return RedirectToAction("ProductLicenseSearch");

            }
        }

        public ActionResult UpdateAddendumEntry(int Id,int AddendumId)
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

                ViewBag.LicenseId = Id;
                ViewBag.AddendumId = AddendumId;
                if (_workContext.CurrentUser.DepartmentM.DepartmentCode == "AD" || _workContext.CurrentUser.DepartmentM.DepartmentCode == "SA")
                {
                    TempData["BackToserach"] = "BackToserach";
                    return View("AddendumEntry");
                }
                else
                {
                    TempData["BackToserach"] = "BackToserach";
                    ViewBag.UpdateMode = "Y";
                    return View("AddendumView");
                }
                

            }
        }

        [HttpPost]
        public ActionResult UpdateAddendumEntry()
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {

                return RedirectToAction("ProductLicenseSearch");
            }
        }


        public ActionResult ViewAddendum(int Id, int AddendumId)
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.LicenseId = Id;
                ViewBag.AddendumId = AddendumId;
                ViewBag.UpdateMode = "N";
                TempData["BackToserach"] = "BackToserach";
                return View("AddendumView");


            }
        }

        [HttpGet]
        public ActionResult ImpressionSearch()
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "pr" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush

                return View();
            }
        }


        [HttpPost]
        public ActionResult ImpressionSearch(int Id)
        {
            if (_workContext.CurrentUser == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return RedirectToAction("ImpressionSearch");
            }
        }


        public ActionResult ImpressionEntry(int ProductId, int? LicenseId, int? ContractId)
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "pr" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush

                ViewBag.ProductId =ProductId;
                ViewBag.LicenseId = LicenseId;
                ViewBag.ContractId = ContractId;
                return View();
            }
        }


        public IList<ProductLicenseSearch.ClsSearchReport> GetProductLicenseExcelList(String SessionId)
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
                    var _GetLicenseReport = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseSearch.ClsSearchReport>("Proc_ProductLicenseDetails_get", parameters).ToList();
                    return _GetLicenseReport;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }



        public ProductLicenseSearch.ClsSearchReport GetProductLicenseExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseSearch.ClsSearchReport>("Proc_ProductLicenseParameterDetails_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public ActionResult exportToExcelProductLicenseList(ProductLicenseSearch.ClsSearchReport _mobjOtherContract)
        {
            try
            {


                List<ProductLicenseSearch.ClsSearchReport> _mobjReportList = new List<ProductLicenseSearch.ClsSearchReport>();
                ProductLicenseSearch.ClsSearchReport _mobjParametertList = new ProductLicenseSearch.ClsSearchReport();

                _mobjReportList = GetProductLicenseExcelList(_mobjOtherContract.SessionId).ToList();
                _mobjParametertList = GetProductLicenseExcelParameterList(_mobjOtherContract.SessionId);

                string sFileName = "ProductLicense_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Product License Report</b>" + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Product License Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Title</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Sub Title</b></td>");
                    mstr_searchparameter.Append("<td><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td><b>Author Name </b></td>");
                    mstr_searchparameter.Append("<td><b>Contract Date</b></td>");

                    mstr_searchparameter.Append("<td ><b>Proprietor Company</b></td>");
                    mstr_searchparameter.Append("<td ><b>Contact Person</b></td>");
                    mstr_searchparameter.Append("<td ><b>Address</b></td>");
                    mstr_searchparameter.Append("<td ><b>Country</b></td>");
                    mstr_searchparameter.Append("<td ><b>State</b></td>");
                    mstr_searchparameter.Append("<td ><b>City</b></td>");
                    mstr_searchparameter.Append("<td ><b>PIN Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Mobile</b></td>");
                    mstr_searchparameter.Append("<td ><b>Email</b></td>");
                    mstr_searchparameter.Append("<td ><b>Request Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Territory Rights</b></td>");
                    mstr_searchparameter.Append("<td ><b>Impression With In Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>No of Impressions</b></td>");
                    mstr_searchparameter.Append("<td ><b>Print Quantity Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>Print Quantity</b></td>");
                    mstr_searchparameter.Append("<td ><b>Royality Terms</b></td>");
                    mstr_searchparameter.Append("<td ><b>Payment Amount</b></td>");
                    mstr_searchparameter.Append("<td ><b>Advanced Amount</b></td>");
                    mstr_searchparameter.Append("<td ><b>Copies For Proprietor</b></td>");
                    mstr_searchparameter.Append("<td ><b>Price Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>Currency</b></td>");
                    mstr_searchparameter.Append("<td ><b>Price</b></td>");
                    mstr_searchparameter.Append("<td ><b>Third Party Permission</b></td>");
                    mstr_searchparameter.Append("<td ><b>Remarks</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Copies Sent Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>EFiles Cost</b></td>");
                    mstr_searchparameter.Append("<td ><b>EFiles Request Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>EFiles Received Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Mode</b></td>");
                    mstr_searchparameter.Append("<td ><b>Effective Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Agreement Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Contract Period In Month</b></td>");
                    mstr_searchparameter.Append("<td ><b>Expiry Date</b></td>");

                    mstr_searchparameter.Append("<td ><b>Addendum Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum First Impression With in Date</b></td>");
                    //mstr_searchparameter.Append("<td ><b>Addendum Period of Agreement</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Expiry Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum No of Impressions</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Balance Quantity Carry Forward</b></td>");
                    mstr_searchparameter.Append("<td ><b>Additional Number of Copies</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Royalty Terms</b></td>");
                    mstr_searchparameter.Append("<td ><b>Addendum Remarks</b></td>");


                    List<string> typename = new List<string>();
                    List<string> subsiadiryname = new List<string>();
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
                                        }
                                    }
                                }
                            }

                            if (_mobjReportList[i].SubsidiaryRights != null)
                            {
                                string[] slab = _mobjReportList[i].SubsidiaryRights.Split(',');
                                foreach (string s in slab)
                                {
                                    string[] slabvalue = s.Split('#');
                                    for (int k = 0; k < slabvalue.Length; k++)
                                    {
                                        if (k % 2 == 0)
                                        {
                                            subsiadiryname.Add(slabvalue[k].ToString());
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

                    List<string> distinctsubsiadiryname = subsiadiryname.Distinct().ToList();
                    if (distinctsubsiadiryname.Count > 0)
                    {
                        foreach (string subsiadirynames in distinctsubsiadiryname)
                        {
                            mstr_searchparameter.Append("<td ><b>" + subsiadirynames.ToUpper() + " </b></td>");
                        }
                    }


                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    int Count_value = 0;
                    foreach (ProductLicenseSearch.ClsSearchReport data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.productlicensecode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.productcode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingTitle + "<br />" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingSubProduct + "<br />" + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + (data.OUPISBN == null || data.OUPISBN == "" ? "" : Convert.ToString("&nbsp;" + data.OUPISBN)) + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContractDate + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorCompany + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ContactPerson + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Country + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.State + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Pincode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Requestdate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.TerritoryRights + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Impressionwithindate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.noofimpressions + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.printquantitytype + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.printquantity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.RoyalityTerms + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PaymentAmount + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AdvancedAmount + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.copiesforlicensor + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.pricetype + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Currencyid + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.price + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.thirdpartypermission + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remarks + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LicensorCopiesSentDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.EFilesCost + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.EFilesRequestDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.EFilesReceivedDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.effectivedate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AgreementDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.contractperiodinmonth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Expirydate + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumFirstImpressionWithinDate + "</td>");
                        //mstr_searchparameter.Append("<td align='left'>" + data.AddendumPeriodofagreement + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumExpiryDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumNoOfImpressions + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumBalanceQuantityCarryForward + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumQuantity + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumRoyaltyTerms + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AddendumRemarks + "</td>");


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


                        if (distinctsubsiadiryname.Count > 0)
                        {
                            for (int j = 0; j < distinctsubsiadiryname.Count; j++)
                            {
                                string slabdata = "";
                                if (_mobjReportList[Count_value].SubsidiaryRights != null)
                                {
                                    string[] slab = _mobjReportList[Count_value].SubsidiaryRights.Split(',');
                                    foreach (string s in slab)
                                    {
                                        string[] slabvalue = s.Split('#');

                                        for (int k = 0; k < slabvalue.Length; k = k + 2)
                                        {
                                            if (slabvalue[k].ToString() == distinctsubsiadiryname[j].ToString())
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