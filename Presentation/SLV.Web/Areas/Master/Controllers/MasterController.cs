using ACS.Core;
using ACS.Services.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using SLV.Model.Common;
using ACS.Services.User;

using SLV.Model.AuthorContract;
using ACS.Services.AuthorContract;
using System.Text;
namespace SLV.Web.Areas.Master.Controllers
{
    public class MasterController : Controller
    {
        private readonly IWorkContext _workContext;
        private readonly IDbContext _dbContext;
        private readonly ICommonListService _commonList;

        //public MasterController(IWorkContext workContext)
        //{
        //    _workContext = workContext;
        //        ViewBag.enteredBy = _workContext.CurrentUser.Id;
        //}

        public MasterController(
            IWorkContext workContext
              , IDbContext dbContext
            ,ICommonListService commonList
         )
        {
            _workContext = workContext;
            this._dbContext = dbContext;
            _commonList = commonList;
        }



        //
        // GET: /Master/Master/

        public ActionResult Index()
        {
            return View();
        }
        //Get Record
        public ActionResult DepartmentMaster()
        {

            if (_workContext.CurrentUser == null || Session["UserId"] == null || Session["UserId"] == null)
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

        public ActionResult DivisionMaster()
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }
            // 
        }


        public ActionResult SubDivisionMaster()
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }



            // return View();
        }

        public ActionResult ExecutiveMaster()
        {

            //if (_workContext.CurrentUser == null || Session["UserId"]==null)
            //{
            //    return new HttpUnauthorizedResult();
            //}
            //else
            //{
            //     return View();
            //}

            if (_workContext.CurrentUser == null || Session["UserId"] == null)
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
                return View();
            }
        }

        //created by : sanjeet singh on 16/05/2016
        // ProductTypeMaster
        public ActionResult ProductTypeMaster()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
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

        public ActionResult SubProductType()
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }
            // return View();
        }

        //created by : sanjeet singh on 17/05/2016
        public ActionResult PublishingCompanyMaster()
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
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "rt" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                //End By Ankush
                return View();
            }
            // return View();
        }
        //

        //public JsonResult getProductTypeId(String Id)
        //{

        //    return Json();
        //}
        //Added by saddam on for Author MAster
        //public ActionResult AuthorMaster()
        //{
        //    if (_workContext.CurrentUser == null || Session["UserId"]==null)
        //    {
        //        TempData["From"] = "S";
        //        return RedirectToAction("Login", "Login", new { area = "" });
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //   // return View();
        //}

        public ActionResult AuthorMaster(int? viewId)
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }

            else
            {
                ViewBag.Department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (viewId != null)
                {
                    TempData["viewId"] = viewId;
                }
                else
                {
                    TempData["viewId"] = 0;
                }

                return View("AuthorMaster");
            }

            // return View();
        }

        public ActionResult AuthorSearch(string For)
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
                    if (For == "Report")
                    {
                        TempData["Report"] = For;
                    }
                    else if (For == "BackToSearch")
                    {
                        TempData["BackToSearch"] = For;
                    }
                    else if (For == "View")
                    {
                        TempData["Action"] = For;
                    }
                    else if (For == "Update")
                    {
                        TempData["Action"] = For;
                    }

                    else if (For == "Delete")
                    {
                        TempData["Action"] = For;
                    }
                }
                TempData["UpdateRights"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //  return View();
        }
        //ended by saddam
        public ActionResult CustomProductEntry()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                TempData["From"] = "S";
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View();
            }

            // return View();
        }

        //Added by saddam on 24/05/2016 for Custom Product
        public ActionResult CustomProduct()
        {

            if (_workContext.CurrentUser == null || Session["UserId"] == null)
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
        //ended by saddam

        //Added by saddam on 02/06/2016 for MultipleFileUpload
        public ActionResult MultipleFileUpload()
        {
            return View();
        }

        //Added by saddam on 20/06/2016 for MultipleFileUpload
        public ActionResult MultipleFileUploadPendingRequest()
        {
            return View();
        }



        public IList<ACS.Core.Domain.Master.AuthorMasterDetail> AuthorSerchExcel(ACS.Core.Domain.Master.AuthorMasterDetail Author)
        {

            SqlParameter[] parameters = new SqlParameter[73];



            parameters[0] = new SqlParameter("AuthorCode", SqlDbType.VarChar, 50);
            if (Author.AuthorCode == "undefined")
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = "'" + Author.AuthorCode + "'";
            }

            parameters[1] = new SqlParameter("Type", SqlDbType.VarChar, 50);
            if (Author.Type == "undefined")
            {
                parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[1].Value = Author.Type;
            }

            parameters[2] = new SqlParameter("FirstName", SqlDbType.VarChar, 50);
            if (Author.FirstName == "undefined")
            {
                parameters[2].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[2].Value = "'" + Author.FirstName + "'";
            }

            parameters[3] = new SqlParameter("LastName", SqlDbType.VarChar, 50);
            if (Author.LastName == "undefined")
            {
                parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[3].Value = "'" + Author.LastName + "'";
            }


            parameters[4] = new SqlParameter("Address", SqlDbType.VarChar, 50);
            if (Author.Address == "undefined")
            {
                parameters[4].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[4].Value = "'" + Author.Address + "'";
            }
            parameters[5] = new SqlParameter("ResidencyStatus", SqlDbType.VarChar, 50);
            if (Author.ResidencyStatus == "undefined")
            {
                parameters[5].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[5].Value = Author.ResidencyStatus;
            }

            parameters[6] = new SqlParameter("CountryId", SqlDbType.VarChar, 50);
            if (Author.CountryId == 0 || Author.CountryId == null)
            {
                parameters[6].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[6].Value = Author.CountryId;
            }

            parameters[7] = new SqlParameter("OtherCountry", SqlDbType.VarChar, 50);
            if (Author.OtherCountry == "null" || Author.OtherCountry == "")
            {
                parameters[7].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[7].Value = "'" + Author.OtherCountry + "'";
            }
            parameters[8] = new SqlParameter("StateId", SqlDbType.VarChar, 50);
            if (Author.StateId == 0 || Author.StateId == null)
            {
                parameters[8].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[8].Value = Author.StateId;
            }


            parameters[9] = new SqlParameter("OtherState", SqlDbType.VarChar, 50);
            if (Author.OtherState == "null" || Author.OtherState == "")
            {
                parameters[9].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[9].Value = "'" + Author.OtherState + "'";
            }

            parameters[10] = new SqlParameter("CityId", SqlDbType.VarChar, 50);
            if (Author.CityId == 0 || Author.CityId == null)
            {
                parameters[10].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[10].Value = Author.CityId;
            }

            parameters[11] = new SqlParameter("OtherCity", SqlDbType.VarChar, 50);
            if (Author.OtherCity == "null" || Author.OtherCity == "")
            {
                parameters[11].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[11].Value = "'" + Author.OtherCity + "'";
            }

            parameters[12] = new SqlParameter("PinCode", SqlDbType.VarChar, 50);
            if (Author.PinCode == "null")
            {
                parameters[12].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[12].Value = Author.PinCode;
            }


            parameters[13] = new SqlParameter("Email", SqlDbType.VarChar, 50);
            if (Author.Email == "undefined")
            {
                parameters[13].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[13].Value = "'" + Author.Email + "'";
            }
            parameters[14] = new SqlParameter("Phone", SqlDbType.VarChar, 50);
            if (Author.Phone == "undefined")
            {
                parameters[14].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[14].Value = Author.Phone;
            }


            parameters[15] = new SqlParameter("Mobile", SqlDbType.VarChar, 50);
            if (Author.Mobile == "undefined")
            {
                parameters[15].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[15].Value = "'" + Author.Mobile + "'";
            }

            parameters[16] = new SqlParameter("Fax", SqlDbType.VarChar, 50);
            if (Author.Fax == null || Author.Fax == "")
            {
                parameters[16].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[16].Value = Author.Fax;
            }

            parameters[17] = new SqlParameter("PANNo", SqlDbType.VarChar, 50);
            if (Author.PANNo == "undefined")
            {
                parameters[17].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[17].Value = Author.PANNo;
            }


            parameters[18] = new SqlParameter("AdharCardNo", SqlDbType.VarChar, 50);
            if (Author.AdharCardNo == "undefined")
            {
                parameters[18].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[18].Value = Author.AdharCardNo;
            }

            parameters[19] = new SqlParameter("ADateOfBirth", SqlDbType.VarChar, 50);
            if (Author.DateOfBirth == "null" || Author.DateOfBirth == "")
            {
                parameters[19].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[19].Value += "'" + Author.DateOfBirth + "'";
            }

            parameters[20] = new SqlParameter("DeathDate", SqlDbType.VarChar, 50);
            if (Author.DeathDate == "null")
            {
                parameters[20].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[20].Value += "'" + Author.DeathDate + "'";
            }

            parameters[21] = new SqlParameter("AccountNo", SqlDbType.VarChar, 50);
            if (Author.AccountNo == "undefined")
            {
                parameters[21].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[21].Value = Author.AccountNo;
            }

            parameters[22] = new SqlParameter("BankName", SqlDbType.VarChar, 50);
            if (Author.BankName == "undefined")
            {
                parameters[22].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[22].Value = "'" + Author.BankName + "'";
            }

            parameters[23] = new SqlParameter("BranchName", SqlDbType.VarChar, 50);
            if (Author.BranchName == "undefined")
            {
                parameters[23].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[23].Value = "'" + Author.BranchName + "'";
            }

            parameters[24] = new SqlParameter("IFSECode", SqlDbType.VarChar, 50);
            if (Author.IFSECode == "undefined")
            {
                parameters[24].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[24].Value = Author.IFSECode;
            }


            parameters[25] = new SqlParameter("InstituteCompanyName", SqlDbType.VarChar, 50);
            if (Author.InstituteCompanyName == "undefined")
            {
                parameters[25].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[25].Value = "'" + Author.InstituteCompanyName + "'";
            }


            parameters[26] = new SqlParameter("AffiliationDesignation", SqlDbType.VarChar, 50);
            if (Author.AffiliationDesignation == "undefined")
            {
                parameters[26].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[26].Value = "'" + Author.AffiliationDesignation + "'";
            }

            parameters[27] = new SqlParameter("AffiliationDepartment", SqlDbType.VarChar, 50);
            if (Author.AffiliationDepartment == "undefined")
            {
                parameters[27].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[27].Value = Author.AffiliationDepartment;
            }


            parameters[28] = new SqlParameter("AffiliationAddress", SqlDbType.VarChar, 50);
            if (Author.AffiliationAddress == null || Author.AffiliationAddress == "")
            {
                parameters[28].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[28].Value = "'" + Author.AffiliationAddress + "'";
            }

            parameters[29] = new SqlParameter("AffiliationCountryId", SqlDbType.VarChar, 50);
            if (Author.AffiliationCountryId == 0 || Author.AffiliationCountryId == null)
            {
                parameters[29].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[29].Value = Author.AffiliationCountryId;
            }

            parameters[30] = new SqlParameter("AffiliationOtherCountry", SqlDbType.VarChar, 50);
            if (Author.AffiliationOtherCountry == "null" || Author.AffiliationOtherCountry == "")
            {
                parameters[30].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[30].Value = "'" + Author.AffiliationOtherCountry + "'";
            }

            parameters[31] = new SqlParameter("AffiliationStateId", SqlDbType.VarChar, 50);
            if (Author.AffiliationStateId == 0 || Author.AffiliationStateId == null)
            {
                parameters[31].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[31].Value = Author.AffiliationStateId;
            }


            parameters[32] = new SqlParameter("AffiliationOtherState", SqlDbType.VarChar, 50);
            if (Author.AffiliationOtherState == "null" || Author.AffiliationOtherState == "")
            {
                parameters[32].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[32].Value = "'" + Author.AffiliationOtherState + "'";
            }


            parameters[33] = new SqlParameter("AffiliationCityId", SqlDbType.VarChar, 50);
            if (Author.AffiliationCityId == 0 || Author.AffiliationCityId == null)
            {
                parameters[33].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[33].Value = Author.AffiliationCityId;
            }

            parameters[34] = new SqlParameter("AffiliationOtherCity", SqlDbType.VarChar, 50);
            if (Author.AffiliationOtherCity == "null" || Author.AffiliationOtherCity == "")
            {
                parameters[34].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[34].Value = "'" + Author.AffiliationOtherCity + "'";
            }

            parameters[35] = new SqlParameter("AffiliationPinCode", SqlDbType.VarChar, 50);
            if (Author.AffiliationPinCode == null || Author.AffiliationPinCode == "")
            {
                parameters[35].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[35].Value = Author.AffiliationPinCode;
            }

            parameters[36] = new SqlParameter("AffiliationPhone", SqlDbType.VarChar, 50);
            if (Author.AffiliationPhone == "undefined")
            {
                parameters[36].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[36].Value = Author.AffiliationPhone;
            }

            parameters[37] = new SqlParameter("AffiliationEmail", SqlDbType.VarChar, 50);
            if (Author.AffiliationEmail == "undefined")
            {
                parameters[37].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[37].Value = "'" + Author.AffiliationEmail + "'";
            }

            parameters[38] = new SqlParameter("AffiliationWebSite", SqlDbType.VarChar, 50);
            if (Author.AffiliationWebSite == "undefined")
            {
                parameters[38].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[38].Value = "'" + Author.AffiliationWebSite + "'";
            }

            parameters[39] = new SqlParameter("BeneficiaryName", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryName == "undefined")
            {
                parameters[39].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[39].Value = "'" + Author.BeneficiaryName + "'";
            }

            parameters[40] = new SqlParameter("BeneficiaryRelation", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryRelation == "undefined")
            {
                parameters[40].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[40].Value = "'" + Author.BeneficiaryRelation + "'";
            }

            parameters[41] = new SqlParameter("BeneficiaryAddress", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryAddress == null || Author.BeneficiaryAddress == "")
            {
                parameters[41].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[41].Value = "'" + Author.BeneficiaryAddress + "'";
            }

            parameters[42] = new SqlParameter("BeneficiaryCountryId", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryCountryId == 0 || Author.BeneficiaryCountryId == null)
            {
                parameters[42].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[42].Value = Author.BeneficiaryCountryId;
            }

            parameters[43] = new SqlParameter("BeneficiaryOtherCountry", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryOtherCountry == null || Author.BeneficiaryOtherCountry == "")
            {
                parameters[43].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[43].Value = "'" + Author.BeneficiaryOtherCountry + "'";
            }

            parameters[44] = new SqlParameter("BeneficiaryStateId", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryStateId == 0 || Author.BeneficiaryStateId == null)
            {
                parameters[44].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[44].Value = Author.BeneficiaryStateId;
            }

            parameters[45] = new SqlParameter("BeneficiaryOtherState", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryOtherState == null || Author.BeneficiaryOtherState == "")
            {
                parameters[45].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[45].Value = "'" + Author.BeneficiaryOtherState + "'";
            }

            parameters[46] = new SqlParameter("BeneficiaryCityId", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryCityId == 0 || Author.BeneficiaryCityId == null)
            {
                parameters[46].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[46].Value = Author.BeneficiaryCityId;
            }

            parameters[47] = new SqlParameter("BeneficiaryOtherCity", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryOtherCity == null || Author.BeneficiaryOtherCity == "")
            {
                parameters[47].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[47].Value = "'" + Author.BeneficiaryOtherCity + "'";
            }


            parameters[48] = new SqlParameter("BeneficiaryPinCode", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryPinCode == null || Author.BeneficiaryPinCode == "")
            {
                parameters[48].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[48].Value = Author.BeneficiaryPinCode;
            }

            parameters[49] = new SqlParameter("BeneficiaryEmail", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryEmail == "undefined")
            {
                parameters[49].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[49].Value = "'" + Author.BeneficiaryEmail + "'";
            }

            parameters[50] = new SqlParameter("BeneficiaryPhone", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryPhone == "undefined")
            {
                parameters[50].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[50].Value = Author.BeneficiaryPhone;
            }

            parameters[51] = new SqlParameter("BeneficiaryMobile", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryMobile == "undefined")
            {
                parameters[51].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[51].Value = "'" + Author.BeneficiaryMobile + "'";
            }

            parameters[52] = new SqlParameter("BeneficiaryFax", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryFax == null || Author.BeneficiaryFax == "")
            {
                parameters[52].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[52].Value = Author.BeneficiaryFax;
            }

            parameters[53] = new SqlParameter("BeneficiaryPanNo", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryPanNo == "undefined")
            {
                parameters[53].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[53].Value = Author.BeneficiaryPanNo;
            }
            parameters[54] = new SqlParameter("BeneficiaryAccountNo", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryAccountNo == "undefined")
            {
                parameters[54].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[54].Value = Author.BeneficiaryAccountNo;
            }

            parameters[55] = new SqlParameter("BeneficiaryBankName", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryBankName == "undefined")
            {
                parameters[55].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[55].Value = "'" + Author.BeneficiaryBankName + "'";
            }

            parameters[56] = new SqlParameter("BeneficiaryBranchName", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryBranchName == "undefined")
            {
                parameters[56].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[56].Value = "'" + Author.BeneficiaryBranchName + "'";
            }

            parameters[57] = new SqlParameter("BeneficiaryIFSECode", SqlDbType.VarChar, 50);
            if (Author.BeneficiaryIFSECode == "undefined")
            {
                parameters[57].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[57].Value = Author.BeneficiaryIFSECode;
            }

            parameters[58] = new SqlParameter("NomineeName", SqlDbType.VarChar, 50);
            if (Author.NomineeName == "undefined")
            {
                parameters[58].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[58].Value = "'" + Author.NomineeName + "'";
            }

            parameters[59] = new SqlParameter("NomineeRelation", SqlDbType.VarChar, 50);
            if (Author.NomineeRelation == "undefined")
            {
                parameters[59].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[59].Value = "'" + Author.NomineeRelation + "'";
            }


            parameters[60] = new SqlParameter("NomineeAddress", SqlDbType.VarChar, 50);
            if (Author.NomineeAddress == null || Author.NomineeAddress == "")
            {
                parameters[60].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[60].Value = "'" + Author.NomineeAddress + "'";
            }

            parameters[61] = new SqlParameter("NomineeCountryId", SqlDbType.VarChar, 50);
            if (Author.NomineeCountryId == 0 || Author.NomineeCountryId == null)
            {
                parameters[61].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[61].Value = Author.NomineeCountryId;
            }


            parameters[62] = new SqlParameter("NomineeOtherCountry", SqlDbType.VarChar, 50);
            if (Author.NomineeOtherCountry == null || Author.NomineeOtherCountry == "")
            {
                parameters[62].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[62].Value = "'" + Author.NomineeOtherCountry + "'";
            }
            parameters[63] = new SqlParameter("NomineeStateId", SqlDbType.VarChar, 50);
            if (Author.NomineeStateId == 0 || Author.NomineeStateId == null)
            {
                parameters[63].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[63].Value = Author.NomineeStateId;
            }

            parameters[64] = new SqlParameter("NomineeOtherState", SqlDbType.VarChar, 50);
            if (Author.NomineeOtherState == null || Author.NomineeOtherState == "")
            {
                parameters[64].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[64].Value = "'" + Author.NomineeOtherState + "'";
            }

            parameters[65] = new SqlParameter("NomineeCityId", SqlDbType.VarChar, 50);
            if (Author.NomineeCityId == 0 || Author.NomineeCityId == null)
            {
                parameters[65].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[65].Value = Author.NomineeCityId;
            }

            parameters[66] = new SqlParameter("NomineeOtherCity", SqlDbType.VarChar, 50);
            if (Author.NomineeOtherCity == null || Author.NomineeOtherCity == "")
            {
                parameters[66].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[66].Value = "'" + Author.NomineeOtherCity + "'";
            }

            parameters[67] = new SqlParameter("NomineePinCode", SqlDbType.VarChar, 50);
            if (Author.NomineePinCode == null || Author.NomineePinCode == "")
            {
                parameters[67].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[67].Value = Author.NomineePinCode;
            }

            parameters[68] = new SqlParameter("NomineeEmail", SqlDbType.VarChar, 50);
            if (Author.NomineeEmail == "undefined")
            {
                parameters[68].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[68].Value = "'" + Author.NomineeEmail + "'";
            }

            parameters[69] = new SqlParameter("NomineePhone", SqlDbType.VarChar, 50);
            if (Author.NomineePhone == "undefined")
            {
                parameters[69].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[69].Value = Author.NomineePhone;
            }
            parameters[70] = new SqlParameter("NomineeMobile", SqlDbType.VarChar, 50);
            if (Author.NomineeMobile == "undefined")
            {
                parameters[70].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[70].Value = "'" + Author.NomineeMobile + "'";
            }

            parameters[71] = new SqlParameter("NomineeFax", SqlDbType.VarChar, 50);
            if (Author.NomineeFax == null || Author.NomineeFax == "")
            {
                parameters[71].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[71].Value = Author.NomineeFax;
            }


            parameters[72] = new SqlParameter("NomineePanNo", SqlDbType.VarChar, 50);
            if (Author.NomineePanNo == "undefined")
            {
                parameters[72].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[72].Value = Author.NomineePanNo;
            }



            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ACS.Core.Domain.Master.AuthorMasterDetail>("Proc_AuthorSerchReportExcel_get", parameters).ToList();


            return _GetAuthorReport;

        }


        public ActionResult exportToExcelAuthorList(ACS.Core.Domain.Master.AuthorMasterDetail _mobjAuthor)
        {
            try
            {


                List<ACS.Core.Domain.Master.AuthorMasterDetail> _mobjReportList = new List<ACS.Core.Domain.Master.AuthorMasterDetail>();


                _mobjReportList = AuthorSerchExcel(_mobjAuthor).ToList();




                string SearchParameter = string.Empty;
                if (_mobjAuthor.AuthorCode != "undefined")
                {
                    SearchParameter = SearchParameter + "Author Code =" + _mobjAuthor.AuthorCode + " | ";
                }
                if (_mobjAuthor.Type != "undefined")
                {
                    SearchParameter = SearchParameter + "Type =" + _mobjAuthor.Type + " | ";
                }




                if (_mobjAuthor.FirstName != "undefined")
                {
                    SearchParameter = SearchParameter + "First Name =" + _mobjAuthor.FirstName + " | ";
                }
                if (_mobjAuthor.LastName != "undefined")
                {
                    SearchParameter = SearchParameter + "Last Name=" + _mobjAuthor.LastName + " | ";
                }

                if (_mobjAuthor.Address != "undefined")
                {
                    SearchParameter = SearchParameter + "Address =" + _mobjAuthor.Address + " | ";
                }
                if (_mobjAuthor.ResidencyStatus != "undefined")
                {
                    SearchParameter = SearchParameter + "Residency Status =" + _mobjAuthor.ResidencyStatus + " | ";
                }

                if (_mobjAuthor.CountryId != 0)
                {
                    SearchParameter = SearchParameter + "Country =" + _mobjAuthor.CountryName + " | ";
                }
                if (_mobjAuthor.OtherCountry != "null")
                {
                    SearchParameter = SearchParameter + "Country=" + _mobjAuthor.OtherCountry + " | ";
                }

                if (_mobjAuthor.StateId != 0)
                {
                    SearchParameter = SearchParameter + "State =" + _mobjAuthor.StateName + " | ";
                }
                if (_mobjAuthor.OtherState != "null")
                {
                    SearchParameter = SearchParameter + "State=" + _mobjAuthor.OtherState + " | ";
                }

                if (_mobjAuthor.CityId != 0)
                {
                    SearchParameter = SearchParameter + "City=" + _mobjAuthor.CityName + " | ";
                }
                if (_mobjAuthor.OtherCity != "null")
                {
                    SearchParameter = SearchParameter + "City =" + _mobjAuthor.OtherCity + " | ";
                }

                if (_mobjAuthor.PinCode != "null")
                {
                    SearchParameter = SearchParameter + "Pin Code=" + _mobjAuthor.PinCode + " | ";
                }
                if (_mobjAuthor.Email != "undefined")
                {
                    SearchParameter = SearchParameter + "Email =" + _mobjAuthor.Email + " | ";
                }

                if (_mobjAuthor.Phone != "undefined")
                {
                    SearchParameter = SearchParameter + "Phone =" + _mobjAuthor.Phone + " | ";
                }
                if (_mobjAuthor.Mobile != "undefined")
                {
                    SearchParameter = SearchParameter + "Mobile =" + _mobjAuthor.Mobile + " | ";
                }

                if (_mobjAuthor.PANNo != "undefined")
                {
                    SearchParameter = SearchParameter + "PAN =" + _mobjAuthor.PANNo + " | ";
                }
                if (_mobjAuthor.AdharCardNo != "undefined")
                {
                    SearchParameter = SearchParameter + "Aadhar Card No. =" + _mobjAuthor.AdharCardNo + " | ";
                }

                if (_mobjAuthor.DateOfBirth != "null")
                {
                    SearchParameter = SearchParameter + "Date of Birth =" + _mobjAuthor.DateOfBirth + " | ";
                }
                if (_mobjAuthor.DeathDate != "null")
                {
                    SearchParameter = SearchParameter + "Demise Date =" + _mobjAuthor.DeathDate + " | ";
                }

                if (_mobjAuthor.AccountNo != "undefined")
                {
                    SearchParameter = SearchParameter + "Account No. =" + _mobjAuthor.AccountNo + " | ";
                }
                if (_mobjAuthor.BankName != "undefined")
                {
                    SearchParameter = SearchParameter + "Bank Name =" + _mobjAuthor.BankName + " | ";
                }

                if (_mobjAuthor.BranchName != "undefined")
                {
                    SearchParameter = SearchParameter + "Branch Name =" + _mobjAuthor.BranchName + " | ";
                }
                if (_mobjAuthor.IFSECode != "undefined")
                {
                    SearchParameter = SearchParameter + "IFSC Code =" + _mobjAuthor.IFSECode + " | ";
                }

                if (_mobjAuthor.InstituteCompanyName != "undefined")
                {
                    SearchParameter = SearchParameter + "Institute / Company Name =" + _mobjAuthor.InstituteCompanyName + " | ";
                }
                if (_mobjAuthor.AffiliationDesignation != "undefined")
                {
                    SearchParameter = SearchParameter + "Affiliation Designation =" + _mobjAuthor.AffiliationDesignation + " | ";
                }

                if (_mobjAuthor.AffiliationDepartment != "undefined")
                {
                    SearchParameter = SearchParameter + "Affiliation Department =" + _mobjAuthor.AffiliationDepartment + " | ";
                }
                if (_mobjAuthor.AffiliationCountryId != 0)
                {
                    SearchParameter = SearchParameter + "Affiliation Country =" + _mobjAuthor.AffiliationCountryName + " | ";
                }

                if (_mobjAuthor.AffiliationOtherCountry != "null")
                {
                    SearchParameter = SearchParameter + "Affiliation Country =" + _mobjAuthor.AffiliationOtherCountry + " | ";
                }
                if (_mobjAuthor.AffiliationStateId != 0)
                {
                    SearchParameter = SearchParameter + "Affiliation State =" + _mobjAuthor.AffiliationStateName + " | ";
                }

                if (_mobjAuthor.AffiliationOtherState != "null")
                {
                    SearchParameter = SearchParameter + "Affiliation State =" + _mobjAuthor.AffiliationOtherState + " | ";
                }
                if (_mobjAuthor.AffiliationCityId != 0)
                {
                    SearchParameter = SearchParameter + "Affiliation City =" + _mobjAuthor.AffiliationCityName + " | ";
                }







                if (_mobjAuthor.AffiliationOtherCity != "null")
                {
                    SearchParameter = SearchParameter + "Affiliation City =" + _mobjAuthor.AffiliationOtherCity + " | ";
                }

                if (_mobjAuthor.AffiliationPinCode != null)
                {
                    SearchParameter = SearchParameter + "Affiliation Pin Code =" + _mobjAuthor.AffiliationPinCode + " | ";
                }
                if (_mobjAuthor.AffiliationPhone != "undefined")
                {
                    SearchParameter = SearchParameter + "Affiliation Phone =" + _mobjAuthor.AffiliationPhone + " | ";
                }

                if (_mobjAuthor.AffiliationEmail != "undefined")
                {
                    SearchParameter = SearchParameter + "Affiliation Email =" + _mobjAuthor.AffiliationEmail + " | ";
                }



                if (_mobjAuthor.AffiliationWebSite != "undefined")
                {
                    SearchParameter = SearchParameter + "Affiliation WebSite =" + _mobjAuthor.AffiliationWebSite + " | ";
                }

                if (_mobjAuthor.BeneficiaryName != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Name =" + _mobjAuthor.BeneficiaryName + " | ";
                }
                if (_mobjAuthor.BeneficiaryRelation != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Relation =" + _mobjAuthor.BeneficiaryRelation + " | ";
                }

                if (_mobjAuthor.BeneficiaryEmail != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Email =" + _mobjAuthor.BeneficiaryEmail + " | ";
                }


                if (_mobjAuthor.BeneficiaryPhone != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Phone =" + _mobjAuthor.BeneficiaryPhone + " | ";
                }

                if (_mobjAuthor.BeneficiaryMobile != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Mobile =" + _mobjAuthor.BeneficiaryMobile + " | ";
                }
                if (_mobjAuthor.BeneficiaryPanNo != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary PAN =" + _mobjAuthor.BeneficiaryPanNo + " | ";
                }

                if (_mobjAuthor.BeneficiaryAccountNo != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Account No. =" + _mobjAuthor.BeneficiaryAccountNo + " | ";
                }


                if (_mobjAuthor.BeneficiaryBankName != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Bank Name =" + _mobjAuthor.BeneficiaryBankName + " | ";
                }

                if (_mobjAuthor.BeneficiaryBranchName != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary Branch Name =" + _mobjAuthor.BeneficiaryBranchName + " | ";
                }
                if (_mobjAuthor.BeneficiaryIFSECode != "undefined")
                {
                    SearchParameter = SearchParameter + "Beneficiary IFSC Code =" + _mobjAuthor.BeneficiaryIFSECode + " | ";
                }

                if (_mobjAuthor.NomineeName != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee Name =" + _mobjAuthor.NomineeName + " | ";
                }


                if (_mobjAuthor.NomineeRelation != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee Relation =" + _mobjAuthor.NomineeRelation + " | ";
                }

                if (_mobjAuthor.NomineeEmail != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee Email =" + _mobjAuthor.NomineeEmail + " | ";
                }
                if (_mobjAuthor.NomineePhone != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee Phone =" + _mobjAuthor.NomineePhone + " | ";
                }

                if (_mobjAuthor.NomineeMobile != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee Mobile =" + _mobjAuthor.NomineeMobile + " | ";
                }


                if (_mobjAuthor.NomineePanNo != "undefined")
                {
                    SearchParameter = SearchParameter + "Nominee PAN =" + _mobjAuthor.NomineePanNo + " | ";
                }





                string sFileName = "AuthorReport_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls";
                //{
                //    StringBuilder mstr_searchparameter = new StringBuilder();
                //    mstr_searchparameter.Append("<table width='100%'>");
                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                //    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Author Report</b>" + "</td>");
                //    mstr_searchparameter.Append("</tr>");

                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                //    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                //    mstr_searchparameter.Append("</tr>");
                //    if (!String.IsNullOrEmpty(SearchParameter))
                //    {
                //        mstr_searchparameter.Append("<tr>");
                //        mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + SearchParameter + "</td>");
                //        mstr_searchparameter.Append("</tr>");
                //    }
                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'></td>");
                //    mstr_searchparameter.Append("</tr>");

                //    mstr_searchparameter.Append("</table>");
                //    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td colspan='2'>");
                //    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                //    mstr_searchparameter.Append("<tr>");
                //    mstr_searchparameter.Append("<td><b>SNo.</b></td>");
                //    mstr_searchparameter.Append("<td ><b>Author Code</b></td>");

                //    mstr_searchparameter.Append("<td ><b>Author Name</b></td>");

                //    mstr_searchparameter.Append("<td><b>Address </b></td>");

                //    mstr_searchparameter.Append("<td><b>PAN </b></td>");
                //    mstr_searchparameter.Append("<td><b> Aadhar Card No. </b></td>");




                //    mstr_searchparameter.Append("</tr>");
                //    mstr_searchparameter.Append("</td>");
                //    int mint_Counter = 1;
                //    foreach (ACS.Core.Domain.Master.AuthorMasterDetail data in _mobjReportList)
                //    {
                //        mstr_searchparameter.Append("<tr>");
                //        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                //        mstr_searchparameter.Append("<td align='left'>" + data.AuthorCode + "</td>");
                //        mstr_searchparameter.Append("<td align='left'>" + data.AuthorName + "</td>");

                //        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");

                //        mstr_searchparameter.Append("<td align='left'>" + data.PANNo + "</td>");

                //        mstr_searchparameter.Append("<td align='left'>" + data.AdharCardNo + "</td>");




                //        mstr_searchparameter.Append("</tr>");
                //        mint_Counter++;
                //    }
                //    mstr_searchparameter.Append("</table></td></tr></table>");



                //    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
                //    this.Response.ContentType = "application/excel";
                //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(mstr_searchparameter.ToString());
                //    return File(buffer, "application/vnd.ms-excel");

                //}


                {
                    StringBuilder mstr_searchparameter = new StringBuilder();
                    mstr_searchparameter.Append("<table width='100%'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                    mstr_searchparameter.Append("<table width='100%' cellpadding='0' border='1%' cellspacing='0'>");
                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'>" + "<b>Author Report</b>" + "</td>");
                    mstr_searchparameter.Append("</tr>");

                    mstr_searchparameter.Append("<tr>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=left >" + "<b>Number of Records:</b> " + _mobjReportList.Count() + "</td>");
                    mstr_searchparameter.Append("<td  style='width: 50%;' valign='top' align=right >" + "<b>Report Created Date:</b> " + String.Format("{0:dd MMM yy HH:mm:ss}", DateTime.Now) + "</td>");
                    mstr_searchparameter.Append("</tr>");
                    if (!String.IsNullOrEmpty(SearchParameter))
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td colspan='2' style='width: 100%;' valign='top' align=left>" + "<b>" + "Search criteria:-" + "</b>" + SearchParameter + "</td>");
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
                    mstr_searchparameter.Append("<td ><b>Author Code</b></td>");

                    mstr_searchparameter.Append("<td ><b>First Name</b></td>");
                    mstr_searchparameter.Append("<td ><b>Last Name</b></td>");
                    mstr_searchparameter.Append("<td><b>Address </b></td>");
                    mstr_searchparameter.Append("<td><b> Residency Status</b></td>");
                    mstr_searchparameter.Append("<td><b> Country</b></td>");
                    mstr_searchparameter.Append("<td><b>State </b></td>");
                    mstr_searchparameter.Append("<td><b>City </b></td>");
                    mstr_searchparameter.Append("<td><b> Pin Code</b></td>");
                    mstr_searchparameter.Append("<td><b>Email </b></td>");
                    mstr_searchparameter.Append("<td><b>Phone </b></td>");
                    mstr_searchparameter.Append("<td><b> Mobile</b></td>");
                    mstr_searchparameter.Append("<td><b> Fax</b></td>");
                    mstr_searchparameter.Append("<td><b>PAN </b></td>");
                    mstr_searchparameter.Append("<td><b> Aadhar Card No. </b></td>");
                    mstr_searchparameter.Append("<td><b> Date of Birth</b></td>");
                    mstr_searchparameter.Append("<td><b>  Demise  Date</b></td>");
                    mstr_searchparameter.Append("<td><b> Account No. </b></td>");
                    mstr_searchparameter.Append("<td><b> Bank Name </b></td>");
                    mstr_searchparameter.Append("<td><b> Branch Name </b></td>");
                    mstr_searchparameter.Append("<td><b> IFSC Code </b></td>");
                    mstr_searchparameter.Append("<td><b> Institute/Company Name </b></td>");
                    mstr_searchparameter.Append("<td><b> Affiliation Designation </b></td>");
                    mstr_searchparameter.Append("<td><b> Department Name</b></td>");
                    mstr_searchparameter.Append("<td><b>Affiliation Address </b></td>");
                    mstr_searchparameter.Append("<td><b> Affiliation Country </b></td>");
                    mstr_searchparameter.Append("<td><b>Affiliation State </b></td>");
                    mstr_searchparameter.Append("<td><b>Affiliation City </b></td>");
                    mstr_searchparameter.Append("<td><b> Affiliation Pin Code </b></td>");
                    mstr_searchparameter.Append("<td><b>Affiliation Phone </b></td>");
                    mstr_searchparameter.Append("<td><b> Affiliation Email</b></td>");
                    mstr_searchparameter.Append("<td><b>Affiliation WebSite </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Name </b></td>");
                    mstr_searchparameter.Append("<td><b>Beneficiary Relation </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Address </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Country </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary State </b></td>");
                    mstr_searchparameter.Append("<td><b>Beneficiary City </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Pin Code</b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Email </b></td>");
                    mstr_searchparameter.Append("<td><b>  Beneficiary Phone</b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Mobile </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Fax </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary PAN </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Account No. </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Bank Name </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary Branch Name </b></td>");
                    mstr_searchparameter.Append("<td><b> Beneficiary IFSC Code </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Name </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Relation </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Address </b></td>");
                    mstr_searchparameter.Append("<td><b>Nominee Country </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee State </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee City </b></td>");
                    mstr_searchparameter.Append("<td><b>  Nominee Pin Code</b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Email </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Phone </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Mobile </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee Fax </b></td>");
                    mstr_searchparameter.Append("<td><b> Nominee PAN </b></td>");
                    mstr_searchparameter.Append("<td><b> Remark </b></td>");



                    mstr_searchparameter.Append("</tr>");
                    mstr_searchparameter.Append("</td>");
                    int mint_Counter = 1;
                    foreach (ACS.Core.Domain.Master.AuthorMasterDetail data in _mobjReportList)
                    {
                        mstr_searchparameter.Append("<tr>");
                        mstr_searchparameter.Append("<td align='right'>" + mint_Counter + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AuthorCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.FirstName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.LastName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Address + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.ResidencyStatus + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.C_Auth + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.S_Auth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City_Auth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PinCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Email + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Phone + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Mobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Fax + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.PANNo + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AdharCardNo + "</td>");


                        mstr_searchparameter.Append("<td align='left'>" + data.DateOfBirth + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.DeathDate + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.AccountNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BankName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BranchName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.IFSECode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.InstituteCompanyName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationDesignation + "</td>");


                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationDepartment + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationAddress + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.C_Aff + "</td>");


                        mstr_searchparameter.Append("<td align='left'>" + data.S_Aff + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City_Aff + "</td>");
                        //  mstr_searchparameter.Append("<td align='left'>" + data.PANNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationPinCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationPhone + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationEmail + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.AffiliationWebSite + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryRelation + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryAddress + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.C_Benff + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.S_Benff + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City_Benff + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryPinCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryEmail + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryPhone + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryMobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryFax + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryPanNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryAccountNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryBankName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryBranchName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.BeneficiaryIFSECode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeName + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeRelation + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeAddress + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.C_Nomi + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.S_Nomi + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.City_Nomi + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineePinCode + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeEmail + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineePhone + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeMobile + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.NomineeFax + "</td>");

                        mstr_searchparameter.Append("<td align='left'>" + data.NomineePanNo + "</td>");
                        mstr_searchparameter.Append("<td align='left'>" + data.Remark + "</td>");


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

        //added by Prakash on 23 Aug, 2017
        public ActionResult ISBNConveter()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }

            string[] str_BulkISBN;
            string str_txa_ISBN = Request.Form["txa_ISBN"];
            string str_btn_submit = Request.Form["btn_ISBN"];
            TempData["RequestISBN"] = str_txa_ISBN;
            var flag = "";

            if (!String.IsNullOrEmpty(str_btn_submit))
            {
                if (str_btn_submit.ToLower() == "convert")
                {
                    //convert ISBN
                    if (!String.IsNullOrEmpty(str_txa_ISBN))
                    {
                        str_BulkISBN = str_txa_ISBN.Split('\n');
                        if (str_BulkISBN != null)
                        {
                            for (int i = 0; i < str_BulkISBN.Count(); i++)
                            {
                                if (str_BulkISBN[i].Trim().Replace("\r", "").Length == 10)
                                {
                                    TempData["Isbn13"] += Isbn10to13(str_BulkISBN[i].Trim().Replace("\r", "")) + "\n";
                                    flag = Isbn10to13(str_BulkISBN[i].Trim().Replace("\r", "")) + "\n";
                                    if (TempData["Isbn13"] != null)
                                    {
                                        TempData["Isbn10"] += Isbn13to10(flag.ToString().Trim().Replace("\n", "")) + "\n";
                                        flag = "";
                                    }
                                }

                                if (str_BulkISBN[i].Trim().Replace("\r", "").Length == 13)
                                {
                                    TempData["Isbn10"] += Isbn13to10(str_BulkISBN[i].Trim().Replace("\r", "")) + "\n";
                                    flag = Isbn13to10(str_BulkISBN[i].Trim().Replace("\r", "")) + "\n";
                                    if (TempData["Isbn10"] != null)
                                    {
                                        TempData["Isbn13"] += Isbn10to13(flag.ToString().Trim().Replace("\n", "").Replace("\r", "")) + "\n";
                                        flag = "";
                                    }
                                }

                                if (str_BulkISBN[i].Trim().Replace("\r", "").Length != 10 && str_BulkISBN[i].Trim().Replace("\r", "").Length != 13)
                                {
                                    TempData["Isbn10"] += "INVALID" + "\n";
                                    TempData["Isbn13"] += "INVALID" + "\n";
                                }
                            }

                            HttpContext.Cache["RequestISBN"] = TempData["RequestISBN"];
                            HttpContext.Cache["Isbn10"] = TempData["Isbn10"];
                            HttpContext.Cache["Isbn13"] = TempData["Isbn13"];
                        }
                    }
                }
                else
                {
                    //export to csv            
                    if (!string.IsNullOrEmpty(HttpContext.Cache["RequestISBN"].ToString()) &&
                        !string.IsNullOrEmpty(HttpContext.Cache["Isbn10"].ToString()) &&
                        !string.IsNullOrEmpty(HttpContext.Cache["Isbn13"].ToString()))
                    {
                        ExportToCSV(HttpContext.Cache["RequestISBN"].ToString(), HttpContext.Cache["Isbn10"].ToString(), HttpContext.Cache["Isbn13"].ToString());
                    }
                }
            }

            return View();
        }

        //function for convert ISBN 13 to 10
        public static String Isbn13to10(String isbn13)
        {
            bool IsValid = IsValidIsbn13(isbn13);
            if (IsValid == false)
            {
                return "INVALID";
            }
            else
            {
                if (String.IsNullOrEmpty(isbn13))
                    throw new ArgumentNullException("isbn13");
                isbn13 = isbn13.Replace("-", "").Replace(" ", "");
                if (isbn13.Length != 13)
                {
                    return "INVALID";
                    //throw new ArgumentException("The ISBN doesn't contain 13 characters.", "isbn13");
                }
                String isbn10 = isbn13.Substring(3, 9);
                int checksum = 0;
                int weight = 10;

                foreach (Char c in isbn10)
                {
                    checksum += (int)Char.GetNumericValue(c) * weight;
                    weight--;
                }

                checksum = 11 - (checksum % 11);
                if (checksum == 10)
                    isbn10 += "X";
                else if (checksum == 11)
                    isbn10 += "0";
                else
                    isbn10 += checksum;

                return isbn10;
            }

        }

        //function for convert ISBN 10 to 13
        //public string Isbn10to13(string ISBN)
        //{
        //    bool IsValid = IsValidIsbn10(ISBN);
        //    if (IsValid == false)
        //    {
        //        return "INVALID";
        //    }
        //    else
        //    {
        //        if (String.IsNullOrEmpty(ISBN))
        //            throw new ArgumentNullException("ISBN");
        //        ISBN = ISBN.Replace("-", "").Replace(" ", "");
        //        if (ISBN.Length != 10)
        //        {
        //            return "INVALID";
        //            //throw new ArgumentException("The ISBN doesn't contain 10 characters.", "ISBN");
        //        }
        //        string isbn10 = "978" + ISBN.Substring(0, 9);
        //        int isbn10_1 = Convert.ToInt32(isbn10.Substring(0, 1));
        //        int isbn10_2 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(1, 1)) * 3);
        //        int isbn10_3 = Convert.ToInt32(isbn10.Substring(2, 1));
        //        int isbn10_4 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(3, 1)) * 3);
        //        int isbn10_5 = Convert.ToInt32(isbn10.Substring(4, 1));
        //        int isbn10_6 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(5, 1)) * 3);
        //        int isbn10_7 = Convert.ToInt32(isbn10.Substring(6, 1));
        //        int isbn10_8 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(7, 1)) * 3);
        //        int isbn10_9 = Convert.ToInt32(isbn10.Substring(8, 1));
        //        int isbn10_10 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(9, 1)) * 3);
        //        int isbn10_11 = Convert.ToInt32(isbn10.Substring(10, 1));
        //        int isbn10_12 = Convert.ToInt32(Convert.ToInt32(isbn10.Substring(11, 1)) * 3);
        //        int k = (isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12);
        //        int checkdigit = 10 - ((isbn10_1 + isbn10_2 + isbn10_3 + isbn10_4 + isbn10_5 + isbn10_6 + isbn10_7 + isbn10_8 + isbn10_9 + isbn10_10 + isbn10_11 + isbn10_12) % 10);
        //        if (checkdigit == 10)
        //            checkdigit = 0;

        //        string str_isbn = isbn10 + checkdigit.ToString();

        //        return str_isbn;
        //    }
        //}

        public string Isbn10to13(string ISBN)
        {
            bool validISBN = IsValidIsbn10(ISBN);
            string result = null;
            if (String.IsNullOrEmpty(ISBN))
                return "Please enter ISBN Number";
            ISBN = ISBN.Replace("-", "").Replace(" ", "");
            if (ISBN.Length != 10)
            {
                return "INVALID";
                //throw new ArgumentException("The ISBN doesn't contain 10 characters.", "ISBN");
            }
            if (validISBN == true)
            {
                string Isbn13Checksum = null;
                float sum = 0;
                result = "978" + ISBN.Substring(0, 9);

                for (int j = 0; j < 12; j++)
                {
                    sum += ((j % 2 == 0) ? 1 : 3) * (Int32.Parse(result[j].ToString()));
                }

                float div = sum / 10;
                float rem = sum % 10;
                if (rem == 0)
                    return "0";
                else
                    Isbn13Checksum = (10 - rem).ToString();
                result = result + Isbn13Checksum;
            }
            else
                return "INVALID";

            return result;

        }

        //function for validate ISBN 10 is EQUIVALENT or not
        static bool IsValidIsbn10(string isbn)
        {
            bool result = false;

            if (isbn == null)
                return false;
            //isbn = NormalizeIsbn(isbn);
            if (isbn.Length != 10)
                return false;

            int res;
            for (int i = 0; i < 9; i++)
                if (!int.TryParse(isbn[i].ToString(), out res))
                    return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (i + 1) * int.Parse(isbn[i].ToString());

            int r = sum % 11;
            if (r == 10)
                result = (isbn[9] == 'X');
            else
                result = (isbn[9] == (char)('0' + r));

            return result;
        }

        //function for validate ISBN 13 is EQUIVALENT or not
        private static bool IsValidIsbn13(string isbn13)
        {
            bool result = false;

            if (isbn13.Length != 13)
                return false;

            if (!string.IsNullOrEmpty(isbn13))
            {
                long j;
                if (isbn13.Contains('-')) isbn13 = isbn13.Replace("-", "");

                // Check if it contains any non numeric chars, if yes, return false
                if (!Int64.TryParse(isbn13, out j))
                    result = false;

                int sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    sum += Int32.Parse(isbn13[i].ToString()) * (i % 2 == 1 ? 3 : 1);
                }

                int remainder = sum % 10;
                int checkDigit = 10 - remainder;
                if (checkDigit == 10) checkDigit = 0;
                    result = (checkDigit == int.Parse(isbn13[12].ToString()));
            }
            return result;
        }

        //function for Export ISBN in CSV file
        public void ExportToCSV(string ISBN, string ResultISBN10, string ISBNResult13)
        {
            if (!string.IsNullOrEmpty(ISBN) && !string.IsNullOrEmpty(ResultISBN10) && !string.IsNullOrEmpty(ISBNResult13))
            {
                string[] strQuery = ISBN.Split('\n');
                string[] strQuery1 = ResultISBN10.Split('\n');
                string[] strQuery2 = ISBNResult13.Split('\n');

                ////-----------EXCEL File
                //string attachment = "attachment; filename=MyCsvLol.xls";
                //Response.Clear();
                //Response.ClearHeaders();
                //Response.ClearContent();
                //Response.AddHeader("content-disposition", attachment);
                //Response.ContentType = "application/vnd.ms-excel"; 
                //Response.AddHeader("Pragma", "public");

                //var sb = new StringBuilder();
                //sb.AppendLine("<table style='width=100%'>");
                //sb.AppendLine("<tr><td align='left'>ISBN</td>");
                //sb.AppendLine("<td align='left'>ISBN10</td>");
                //sb.AppendLine("<td align='left'>ISBN13</td></tr>");

                //for (int i = 0; i < strQuery.Length; i++)
                //{
                //    sb.AppendLine("<tr><td align='left'>" + strQuery[i].ToString() + "</td>");
                //    sb.AppendLine("<td align='left'>" + strQuery1[i].ToString() + "</td>");
                //    sb.AppendLine("<td align='left'>" + strQuery2[i].ToString() + "</td></tr>");
                //}
                //sb.AppendLine("</table>");
                ////-------------------------End EXCEL File

                ////-----------CSV File
                string fileName = "ISBNConvert_" + string.Format("{0:ddMMyyyy}", DateTime.Now) + "_" + string.Format("{0:hhmmss}", DateTime.Now) + "_.csv";
                string attachment = "attachment; filename=" + fileName;
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "text/csv";
                Response.AddHeader("Pragma", "public");

                StringBuilder sb = new StringBuilder();
                sb.Append("ISBN,ISBN10,ISBN13");
                sb.AppendLine();

                for (int i = 0; i < strQuery.Length; i++)
                {
                    sb.Append(strQuery[i].Trim().Replace("\r", "") + "," + strQuery1[i].Trim().Replace("\r", "") + "," + strQuery2[i].Trim().Replace("\r", ""));
                    sb.AppendLine();
                }
                ////-------------------------End CSV File              

                Response.Write(sb.ToString());
                Response.End();
            }
        }


        //Added by Ptakash on 25 Sep, 2017
        public ActionResult Ticker()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                var obj_department = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                if (obj_department.ToString() != "ad" && obj_department.ToString() != "sa")
                {
                    TempData["From"] = "You have no rights to access this page";
                    return RedirectToAction("Login", "Login", new { area = "" });

                }
                else
                {
                    //Start Ticker
                    var tickerList = _commonList.GetTickerList().ToList();
                    if (tickerList.Count > 0)
                    {
                        var _ticker = "";
                        foreach (var items in tickerList)
                        {
                            if (items.FromDate == null && items.ToDate == null)
                            {
                                if (_ticker != "")
                                {
                                    _ticker += "   |   ";
                                }
                                _ticker += items.Title;
                            }

                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate != null && items.ToDate == null)
                            {
                                if (Convert.ToDateTime(items.FromDate) <= Convert.ToDateTime(DateTime.Today))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate == null && items.ToDate != null)
                            {
                                if (Convert.ToDateTime(DateTime.Today) >= Convert.ToDateTime(items.ToDate))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        foreach (var items in tickerList)
                        {
                            if (items.FromDate != null && items.ToDate != null)
                            {
                                if (Convert.ToDateTime(items.FromDate) <= Convert.ToDateTime(DateTime.Today) && Convert.ToDateTime(items.ToDate) >= Convert.ToDateTime(DateTime.Today))
                                {
                                    if (_ticker != "")
                                    {
                                        _ticker += "   |   ";
                                    }
                                    _ticker += items.Title;
                                }
                            }
                        }

                        if (_ticker != "")
                        {
                            Session["Ticker"] = _ticker;
                        }
                    }
                    //End Ticker
                }
            }

            return View();
        }

        //Added on 05 Oct, 2017
        public ActionResult UploadDocument()
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //return View();
        }

        public ActionResult AssetSubType()
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //return View();
        }

        public ActionResult AssetStatus()
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //return View();
        }

        public ActionResult EscalationMatrix()
        {
            if (_workContext.CurrentUser == null || Session["UserId"] == null)
            {
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
            }

            return View();
        }

        public ActionResult AuthorType()
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //return View();
        }

        public ActionResult CurrencyMaster()
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
                TempData["Department"] = _workContext.CurrentUser.DepartmentM.DepartmentCode.ToLower();
                return View();
            }

            //return View();
        }

        
    }
}