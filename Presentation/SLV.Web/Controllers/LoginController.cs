using ACS.Core;
using ACS.Core.Domain.User;
using ACS.Core.Domain.Master;
using ACS.Services.Authentication;
using ACS.Services.User;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Services.Master;


namespace SLV.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IWorkContext _workContext;
        //added by sanjeet singh
        private readonly IExecutive _execcutiveService;
        //
        private readonly ICommonListService _commonList;

        public LoginController(
            IUserService userService
            , IUserAuthenticationService userAuthenticationService
            , IWorkContext workContext
            ,IExecutive execcutiveService
            ,ICommonListService commonList
        )
        {
            _userService = userService;
            _userAuthenticationService = userAuthenticationService;
            _workContext = workContext;
            _execcutiveService = execcutiveService;
            _commonList = commonList;
        }

        
        // GET: /Login/

        public ActionResult Index()
        {
            return View("Login");
        }
        
        public ActionResult login()
        {
            string msg = TempData["From"] != null ? TempData["From"].ToString() : "";
            if (msg!="")
            {
                ViewBag.Msg = msg == "L" ? _execcutiveService.KeyValue("Logout") : (msg == "S" ? _execcutiveService.KeyValue("SesseionTimeout") : _execcutiveService.KeyValue("NotAuthenticate"));
            }

           return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

         [HttpGet]
        public ActionResult ChangePassword()
        {

            if (_workContext.CurrentUser == null || Session["UserId"]==null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            else
            {
                return View(_workContext.CurrentUser);
            }

           // return View();
        }

       [HttpPost]
        public ActionResult login(ExecutiveMaster  user)
        {
           

            //sign in new customer
           //Commented and added by sanjeet singh 27th May 2016
            //  user.executiveName = "vishalv@vrvirtual.com";
            //ACS.Core.Domain.Master.ExecutiveMaster _user = null;
               var _user = _userService.GetUserDetailByUserName(user.Emailid);
              _userAuthenticationService.SignIn(_user, false);
             //

              var _FileUpload = _userService.GetFileUploadURL();

              Session["FileUploadPath"] = _FileUpload.keyValue;



              Session["UserName"] = _workContext.CurrentUser.executiveName;
              Session["UserDepartment"] = _workContext.CurrentUser.DepartmentM.DepartmentName;
              Session["DepartmentId"] = _workContext.CurrentUser.DepartmentM.Id;
              Session["UserId"] = _workContext.CurrentUser.Id;
              Session["SessionId"] = Session.SessionID;
              Session["deptCode"] = _workContext.CurrentUser.DepartmentM.DepartmentCode;
           
              //Added by Prakash on 16/09/2016
              #region Print Permissions
              var _objPrintPermissions = _userService.GetPrintPermissions();
              string printValue = null;
              if (!string.IsNullOrEmpty(_objPrintPermissions.keyValue))
              {
                  var keyValue = _objPrintPermissions.keyValue.Split(',');
                  //foreach (var items in keyValue)
                  //{
                  //    if (Session["deptCode"].ToString() == items)
                  //        printValue = items;
                  //    else
                  //        printValue = null;
                  //}
                  string value = Session["deptCode"].ToString();
                  int pos = Array.IndexOf(keyValue, value);
                  if (pos > -1)
                      printValue = value;
                  else
                      printValue = null;
              }
              else
              {
                  printValue = null;
              }
              Session["PrintPermissions"] = printValue;
              #endregion
           
              //Added by Ankush on 13/10/2016
              #region GenerateExcel Permissions
              var _objGenerateExcelPermissions = _userService.GetGenerateExcelPermissions();
              string GenerateExcelValue = null;
              if (!string.IsNullOrEmpty(_objGenerateExcelPermissions.keyValue))
              {
                  var keyValue = _objGenerateExcelPermissions.keyValue.Split(',');
                  string value = Session["deptCode"].ToString();
                  int pos = Array.IndexOf(keyValue, value);
                  if (pos > -1)
                      GenerateExcelValue = value;
                  else
                      GenerateExcelValue = null;
              }
              else
              {
                  GenerateExcelValue = null;
              }
              Session["GenerateExcelPermissions"] = GenerateExcelValue;
              #endregion

              //if (Request.QueryString["ReturnUrl"] != null && Request.QueryString["ReturnUrl"].ToString() != "")
            //{
            //    Response.Redirect("~/" + Request.QueryString["ReturnUrl"].ToString());
            //    return null;
            //}
            //else
            //    return RedirectToAction("../Staff/Staff/index");

               //Added by sanjeet 
               #region
             
               #endregion
                // return RedirectToAction("../Home/Home/Index");

               if (_user.PwdChanged == "N")
               {
                   //return RedirectToAction("../Login/ChangePassword");


                   return RedirectToAction("../Home/Dashboard/Dashboard");
               }
               else
               {
                   return RedirectToAction("../Home/Dashboard/Dashboard");
               }

             //
        }

       //Added by sanjeet singh

       public ActionResult Logout()
       {
           if (_workContext.CurrentUser != null)
           {
               ExecutiveLoginHistory exeHistory = _execcutiveService.GetExecutiveHistoryByUserName(_workContext.CurrentUser.Emailid);
               _userAuthenticationService.SignOut();
              
               if (exeHistory != null)
               {
                    exeHistory.LogoutTime = DateTime.Now;
                   _execcutiveService.UpdateExecutiveLoginHistory(exeHistory);
               }
               TempData["From"] = "L";

               Session.Abandon();
               Session.Clear();
           }
           return RedirectToAction("login");
       }
        //

        [HttpGet]
       public ActionResult logintest()
       {
           string msg = TempData["From"] != null ? TempData["From"].ToString() : "";
           if (msg != "")
           {
               ViewBag.Msg = msg == "L" ? _execcutiveService.KeyValue("Logout") : (msg == "S" ? _execcutiveService.KeyValue("SesseionTimeout") : _execcutiveService.KeyValue("NotAuthenticate"));
           }

           return View();
       }


        [HttpPost]
        public ActionResult logintest(ExecutiveMaster user)
        {


            //sign in new customer
            //Commented and added by sanjeet singh 27th May 2016
            //  user.executiveName = "vishalv@vrvirtual.com";
            //ACS.Core.Domain.Master.ExecutiveMaster _user = null;
            var _user = _userService.GetUserDetailByUserName(user.Emailid);

            //if (_user.Emailid == "prakashc@cyberspace.in" || _user.Emailid == "admin@oup.com" || _user.Emailid == "superadmin@oup.com")
            //{

                _userAuthenticationService.SignIn(_user, false);
                //

                var _FileUpload = _userService.GetFileUploadURL();

                Session["FileUploadPath"] = _FileUpload.keyValue;



                Session["UserName"] = _workContext.CurrentUser.executiveName;
                Session["UserDepartment"] = _workContext.CurrentUser.DepartmentM.DepartmentName;
                Session["DepartmentId"] = _workContext.CurrentUser.DepartmentM.Id;
                Session["UserId"] = _workContext.CurrentUser.Id;
                Session["SessionId"] = Session.SessionID;
                Session["deptCode"] = _workContext.CurrentUser.DepartmentM.DepartmentCode;

                //Added by Prakash on 16/09/2016
                #region Print Permissions
                var _objPrintPermissions = _userService.GetPrintPermissions();
                string printValue = null;
                if (!string.IsNullOrEmpty(_objPrintPermissions.keyValue))
                {
                    var keyValue = _objPrintPermissions.keyValue.Split(',');
                    //foreach (var items in keyValue)
                    //{
                    //    if (Session["deptCode"].ToString() == items)
                    //        printValue = items;
                    //    else
                    //        printValue = null;
                    //}
                    string value = Session["deptCode"].ToString();
                    int pos = Array.IndexOf(keyValue, value);
                    if (pos > -1)
                        printValue = value;
                    else
                        printValue = null;
                }
                else
                {
                    printValue = null;
                }
                Session["PrintPermissions"] = printValue;
                #endregion

                //Added by Ankush on 13/10/2016
                #region GenerateExcel Permissions
                var _objGenerateExcelPermissions = _userService.GetGenerateExcelPermissions();
                string GenerateExcelValue = null;
                if (!string.IsNullOrEmpty(_objGenerateExcelPermissions.keyValue))
                {
                    var keyValue = _objGenerateExcelPermissions.keyValue.Split(',');
                    string value = Session["deptCode"].ToString();
                    int pos = Array.IndexOf(keyValue, value);
                    if (pos > -1)
                        GenerateExcelValue = value;
                    else
                        GenerateExcelValue = null;
                }
                else
                {
                    GenerateExcelValue = null;
                }
                Session["GenerateExcelPermissions"] = GenerateExcelValue;
                #endregion

                //if (Request.QueryString["ReturnUrl"] != null && Request.QueryString["ReturnUrl"].ToString() != "")
                //{
                //    Response.Redirect("~/" + Request.QueryString["ReturnUrl"].ToString());
                //    return null;
                //}
                //else
                //    return RedirectToAction("../Staff/Staff/index");

                //Added by sanjeet 
                #region

                #endregion
                // return RedirectToAction("../Home/Home/Index");

                if (_user.PwdChanged == "N")
                {
                    //return RedirectToAction("../Login/ChangePassword");


                    return RedirectToAction("../Home/Dashboard/Dashboard");
                }
                else
                {
                    return RedirectToAction("../Home/Dashboard/Dashboard");
                }

                //
            //}
            //else
            //{
            //    return RedirectToAction("login");
            //}
        }



      
	}

   }
