using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Core;
using ACS.Services.Master;
using ACS.Core.Domain.Product;


using ACS.Core.Infrastructure;
using System.Web.Http.Description;
using ACS.Core.Data;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Services.User;
using System.Text;


namespace SLV.Web.Areas.Product.Controllers
{
    public class ProductMasterController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;
        public ProductMasterController(
            IWorkContext workContext
            , IDbContext dbContext
         )
        {
            _workContext = workContext;
            _dbContext = dbContext;
        }
        //
        // GET: /Product/ProductMaster/
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductMaster()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                //Added By Ankush Dated 22/09/2016
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "ed" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProductMaster(ProductMaster Product)
        {
             string submittype = Request.Form["hid_submit"];
             string hid_productId = Request.Form["hid_ProductId"];
             string hid_typeId = Request.Form["hid_ProductTypeId"];


             if (submittype == "1")
             {
                 return RedirectToAction("ProductMaster", "ProductMaster", new { area = "Product" });
             }
             else if (submittype == "2")
             {
                 return RedirectToAction("Index", "AuthorContract", new { area = "Contract", ProductId = hid_productId });
             }
             else if (submittype == "3")
             {
                 return RedirectToAction("ProductLicense", "ProductLicense", new { area = "Product", Id = hid_productId});
             }
             else
             {
                 return null;
             }
            
        }

        public ActionResult UpdateProduct(int Id, string For = "")
        {
            if (For.ToLower() == "update")
            {
                TempData["update"] = For.ToLower();
            }
            else
            {
                TempData["update"] = "dashboard";
            }

            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                //Added By Ankush Dated 22/09/2016
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "ed" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush

                ViewBag.Id = Id;
                return View("ProductMaster");
            }
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductMaster Product)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return RedirectToAction("ProductSearch", "ProductMaster", new { area = "Product" });
            }
        }

        public ActionResult ProductSearch(string For = "", string ViewMore = "", string ForPI = "", string type = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (type != "")
                {
                    TempData["type"] = type.ToLower();
                }

                if (For !=null)
                {
                    TempData["ForType"] = For;
                    //Added By Ankush Dated 22/09/2016
                    if (For.ToLower() == "finalproductentry")
                    {
                        var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                        if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "pr" && obj_department.ToString() != "sa")
                        {
                            TempData["From"] = "You have no rights to access this page";
                            return RedirectToAction("Login", "Login", new { area = "" });

                        }
                    }

                    if (For.ToLower() == "isbnassign" || For.ToLower() == "sapaggrement" || For.ToLower() == "multiplelinking")
                    {
                        var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                        if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                        {
                            TempData["From"] = "You have no rights to access this page";
                            return RedirectToAction("Login", "Login", new { area = "" });
                        }
                    }
                    //End By Ankush



                    if(For.ToLower() == "license")
                    {
                        TempData["License"] = For;
                    }
                    else if (For.ToLower() == "delete")
                    {
                        TempData["Action"] = For.ToLower();
                    }
                    else
                    {
                        TempData["Report"] = For;
                    }

                    if(For.ToLower() == "list")
                    {
                        TempData["List"] = For.ToLower();
                    }

                    if (For.ToLower() == "permissionsinbound")
                    {
                        TempData["PermissionsInbound"] = For.ToLower();
                    }
                    if (ForPI.ToLower() == "permissionsinbound")
                    {
                        TempData["PermissionsInbound"] = ForPI.ToLower();
                    }

                }
                if (ViewMore != "")
                {
                    ViewBag.ViewMore = ViewMore;
                }
                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode;

                 ViewBag.For = 1;
                return View();
            }

           
        }
        
        public ActionResult ProductListing()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult FinalProductEntry(int Id)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
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

                ViewBag.ProductId = Id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult FinalProductEntry()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return RedirectToAction("ProductSearch", new { type = "finalproductentry" });
            }
        }
        
        public ActionResult SAPAggrementAssign(int Id)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
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

                ViewBag.ProductId = Id;
               
                return View();
            }
        }
        
        //Added by Saddam on 25/08/2017
        public ActionResult SAPAggrementList()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                ViewBag.Department = obj_department;

                return View();
            }
        }
        //Ended by Saddam

        //Added by Saddam
        public ActionResult SAPAggrementUpdate(string OUPISBN)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                ViewBag.OUPISBN = OUPISBN;

                return View();
            }
        }
       
        //Ended by Saddam
        public ActionResult RenderProductPartial(int ProductIdId)
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                ViewBag.ProductId = ProductIdId;
                return View("_ProductDetails");
            }
        }
        
        [HttpPost]
        public ActionResult SAPAggrementAssign()
        {
            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
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

                return RedirectToAction("ProductSearch", new { type = "sapaggrementassign" });
            }
        }
        
        public IList<ProductSearchDetails> GetProductExcelList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductSerchReport_get", parameters).ToList();
                    return _GetAuthorReport;
                }
            }
            catch( Exception ex)
            {
                return null;
            }

        }
        
        public ProductSearchDetails GetProductExcelParameterList(String SessionId)
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
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductSerchReportSearchParametter_get", parameters).FirstOrDefault();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
        public ActionResult exportToExcelProductList(ACS.Core.Domain.Master.ProductSearchDetails _mobjOtherContract, string SeriesId = "", string SeriesName = "")
        {
            try
            {


                List<ACS.Core.Domain.Master.ProductSearchDetails> _mobjReportList = new List<ACS.Core.Domain.Master.ProductSearchDetails>();
                ACS.Core.Domain.Master.ProductSearchDetails _mobjParametertList = new ACS.Core.Domain.Master.ProductSearchDetails();

                if (SeriesId == "")
                {
                    _mobjReportList = GetProductExcelList(_mobjOtherContract.SessionId).ToList();
                    _mobjParametertList = GetProductExcelParameterList(_mobjOtherContract.SessionId);
                }
                else
                {
                    _mobjReportList = GetProductSeriesExcelList(SeriesId).ToList();
                }

                string sFileName = "ProductReport_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Product Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");
                  
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    if (SeriesId == "")
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
                    mstr_searchparameter.Append("<td ><b>Product Code</b></td>");
                    mstr_searchparameter.Append("<td ><b>SAP Agreement No(s)</b></td>");

                    mstr_searchparameter.Append("<td ><b>Division</b></td>");
                    mstr_searchparameter.Append("<td ><b>Sub-Division</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Category</b></td>");
                    mstr_searchparameter.Append("<td ><b>Product Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>Sub-Product Type</b></td>");
                    mstr_searchparameter.Append("<td ><b>ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>Working Sub-Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>OUP Edition</b></td>");
                    mstr_searchparameter.Append("<td ><b>Volume</b></td>");
                    mstr_searchparameter.Append("<td ><b>Copyright Year</b></td>");
                    mstr_searchparameter.Append("<td ><b>Imprint</b></td>");
                    mstr_searchparameter.Append("<td ><b>Language</b></td>");
                    mstr_searchparameter.Append("<td ><b>Series</b></td>");
                    mstr_searchparameter.Append("<td ><b>Derivatives</b></td>");
                    mstr_searchparameter.Append("<td ><b>Org ISBN</b></td>");
                    mstr_searchparameter.Append("<td ><b>Projected Publishing Date</b></td>");
                    mstr_searchparameter.Append("<td ><b>Projected Price</b></td>");
                    mstr_searchparameter.Append("<td ><b>Projected Currency</b></td>");
                    mstr_searchparameter.Append("<td ><b>Pub Center</b></td>");
                    mstr_searchparameter.Append("<td ><b>Final Product Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Final Publishing Date</b></td>");
                    mstr_searchparameter.Append("<td><b>Author(s)</b></td>");
                    ;
                    mstr_searchparameter.Append("<td ><b>Proprietor Isbn</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Product</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Edition</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Copyright Year</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Publishing Company</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Pub Center</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Imprint</b></td>");
                    mstr_searchparameter.Append("<td ><b>Proprietor Author Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Linked Product</b></td>");



                   


                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (ACS.Core.Domain.Master.ProductSearchDetails data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SapAgreementNo + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.divisionname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SubdivName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductCategory + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProductType + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.SubtypeName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OupIsbn + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.WorkingSubProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.OupEdition + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Volume + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.CopyrightYear + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.imprintname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.languagename + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.seriesname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Derivatives + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.orgisbn + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProjectedDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProjectedPrice + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.currencyname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.centername + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.finalproductname + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.FinalPublishingDate + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorIsbn + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorProduct + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorEdition + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorCopyright + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorPublishingCompany + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorPubCenter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorImprint + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ProprietorAuthorName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LinkedProduct + "</td>");
                       

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

        public IList<ProductSearchDetails> GetProductSeriesExcelList(String SeriesId = "")
        {
            try
            {
                if (SeriesId == "")
                {
                    return null;
                }
                else
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("SeriesId", SqlDbType.VarChar, 200);
                    parameters[0].Value = "'" + SeriesId + "'";
                    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductSeries_Report_get", parameters).ToList();
                    return _GetAuthorReport;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
             
        //Added by suranjana on 29/07/2016
      //  [HttpPost]
        public ActionResult ProductDetailsView(int Id, string type = "", string For = "")
        {
            if(For.ToLower() == "view")
            {
                TempData["view"] = For.ToLower();
            }
            else
            {
                TempData["view"] = "dashboard";
            }

            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                if (type != "")
                {
                    TempData["ExpiryReport"] = type;
                }

                ViewBag.ProductId = Id;
                return View();
            }
        }
 
        //Added by Ankush on 06/06/2017
        public ActionResult KitIsbn(int Id = 0)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                ViewBag.KitId = Id;
                return View();
            }
        }

        //Added by Prakash on 06/06/2017
        public ActionResult KitIsbnSearch(string For = "")
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                if(For != "")
                {
                    TempData["Action"] = For.ToLower();
                }
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }
        }

        //Added by Prakash on 13 Oct, 2017
        public ActionResult KitIsbnView(int Id = 0)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                ViewBag.KitId = Id;
                return View();
            }
        }
                
        //Addded By Saddam 10/11/2017 for ISBNKit Impression
        public ActionResult KitImpression(int Id  )
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }

                TempData["KitIsbnId"] = Id;

                return View();
            }
        }
        //ended by Saddam
        

	}
}