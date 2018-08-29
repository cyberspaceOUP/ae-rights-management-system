using System.Web.Http;
using SLV.Model.Common;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.User;
using ACS.Core.Domain.Contact;
using System.Linq;
using ACS.Services.Localization;
using ACS.Core;
using System;
using ACS.Services.Security;
using ACS.Services.User;
using ACS.Services.Contact;
using ACS.Core.Domain.Contact;


using ACS.Services.Authentication;
using ACS.Services.Master;
using System.Text;
using System.IO;
using ACS.Services.Alert;

namespace SLV.API.Controllers.Common
{

    public class LoginController : ApiController
    {


        private readonly IUserService _UserService;
        private readonly ILocalizationService _localizationService;
        private readonly IEncryptionService _encryptionService;
        private readonly IContactService _contactService;
        // private readonly IAuthenticationService _authenticationService;
        //Added by sanjeet on 30th may 2016
        private readonly IUserAuthenticationService _userAuthenticationService;
        //
        private readonly IExecutive _execcutiveService;
        private readonly IServiceApplicationEmailSetup _IServiceApplicationEmailSetup;

        public LoginController(IUserService UserService
            , IEncryptionService encryptionService
           , IContactService contactService
            //,  ILocalizationService localizationService
            , IExecutive execcutiveService
            , IUserAuthenticationService userAuthenticationService
            , IServiceApplicationEmailSetup IServiceApplicationEmailSetup
            )
        {

            _UserService = UserService;
            _encryptionService = encryptionService;
            _contactService = contactService;
            // _localizationService = localizationService;
            _execcutiveService = execcutiveService;
            _userAuthenticationService = userAuthenticationService;
            this._IServiceApplicationEmailSetup = IServiceApplicationEmailSetup;
        }


        public IHttpActionResult Login(ExecutiveMaster login)
        {
            UserLoginResults loginResult = new UserLoginResults();
            string status = string.Empty; string value = string.Empty;

            try
            {
                string pwd = _encryptionService.EncryptText(login.Password, _execcutiveService.KeyValue("encriptionkey"));
                //string test_pwd = _encryptionService.DecryptText("VFL2yVFWp+S2z/in3jjI/EaK2E5OhOgi", _execcutiveService.KeyValue("encriptionkey"));

                loginResult = _UserService.ValidateUserLogin(login.Emailid, login.Password, "");

                //Added By Sanjeet Singh 0n 30th may 2016
                #region
                //ExecutiveLoginHistory exloginHistory = new ExecutiveLoginHistory();
                //exloginHistory.ExecutiveUserName = login.Emailid;
                //exloginHistory.LoginTime = DateTime.Now;
                //exloginHistory.LogoutTime = null;
                //if (loginResult.ToString() == "Successful")
                //{
                //    ExecutiveLoginHistory exeHistory = _execcutiveService.GetExecutiveHistoryByUserName(login.Emailid);

                //    string executiveStatus = _execcutiveService.ExecutiveLoginHistoryCheck(exloginHistory);

                //    if (executiveStatus == "Y")
                //    {
                //        if (string.IsNullOrEmpty(exeHistory.LogoutTime.ToString()))
                //        {
                //            loginResult = UserLoginResults.AllReadyLogged;
                //        }
                //        else
                //        {
                //            exeHistory.LoginTime = DateTime.Now;
                //            exeHistory.LogoutTime = null;
                //            _execcutiveService.UpdateExecutiveLoginHistory(exeHistory);
                //        }
                //    }
                //    else
                //    {
                //        _execcutiveService.InsertExecutiveLoginHistory(exloginHistory);
                //    }
                //}
                //else
                //{
                //    ExecutiveMaster objExecutive = _UserService.GetUserDetailByUserName(login.Emailid);

                //    if (objExecutive.block == "N")
                //    {
                //        LoginHistory loginHistory = new LoginHistory();
                //        loginHistory.UserName = login.Emailid;
                //        loginHistory.UserPassword = pwd;
                //        //To check on daily basis
                //        loginHistory.EntryDate = DateTime.Now.Date;
                //        string duplicate = _execcutiveService.LoginHistoryCheck(loginHistory);
                //        if (duplicate != "Y")
                //        {
                //            loginHistory.Attempt = 1;
                //            _execcutiveService.InsertLoginHistory(loginHistory);
                //        }
                //        else
                //        {
                //            LoginHistory objloginHistory = _execcutiveService.GetLoginHistoryByUserName(loginHistory);
                //            objloginHistory.Attempt = objloginHistory.Attempt + 1;
                //            // _execcutiveService.UpdateLoginHistory(objloginHistory);

                //            if (objloginHistory.Attempt >= 3)
                //            {
                //                //  ExecutiveMaster objExecutive = _UserService.GetUserDetailByUserName(login.Emailid);
                //                objExecutive.block = "Y";
                //                _execcutiveService.UpdateExecutive(objExecutive);

                //            }
                //            else
                //            {
                //                _execcutiveService.UpdateLoginHistory(objloginHistory);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        //To show message for blocked user
                //        loginResult = UserLoginResults.UserBlocked;
                //        //
                //    }
                //    //
                //}
                #endregion
                //

                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            //var contact = _contactService.GetContactDetailByEmailID(login.Email);

                            ////sign in new customer
                            //_authenticationService.SignIn(contact, false);// at the place of "false" use the boolean property to set the "remember me"
                            // status = _localizationService.GetResource("Common.Login.Successful");

                            status = "Successful";
                            break;
                        }
                    case UserLoginResults.CustomerNotExist:
                        // status = _localizationService.GetResource("Common.Login.WrongCredentials.ContactNotExist");
                        status = "Invalid UserName";

                        break;
                    case UserLoginResults.Deleted:
                        //   status = _localizationService.GetResource("Common.Login.WrongCredentials.Deleted");
                        break;
                    case UserLoginResults.NotActive:
                        //  status = _localizationService.GetResource("Common.Login.WrongCredentials.NotActive");
                        status = "Employee Not Active";
                        break;
                    case UserLoginResults.NotRegistered:
                        //  status = _localizationService.GetResource("Common.Login.WrongCredentials.NotRegistered"); value = _encryptionService.EncryptText(_contactService.GetContactDetailByEmailID(login.Username).Id.ToString(), "");// link generate to set password
                        status = "Employee Not Registered";
                        break;
                    //Added by sanjeet singh
                    case UserLoginResults.AllReadyLogged:
                        status = "User All Ready Logged";
                        break;

                    case UserLoginResults.UserBlocked:
                        status = "You are blocked";
                        break;
                    //
                    case UserLoginResults.WrongPassword:
                    default:
                        // status = _localizationService.GetResource("Common.Login.WrongCredentials");
                        status = "Wrong Password";

                        //status = "Worng password";
                        break;
                }

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            //return Json(new { status, value });
            return Json(new { status });
        }




        public IHttpActionResult ForgetPassword(ExecutiveMaster contact)
        {
            // initialize string for status 
            string status = string.Empty;
            string _message = string.Empty;

            try
            {

                // initialize values 
                ACS.Core.Domain.Master.ExecutiveMaster _contactDetails = _contactService.GetContactDetailByEmailID(contact.Emailid);

                if (_contactDetails != null)
                {
                    string result = SendMailForgetPassword(_contactDetails);
                    status = result;
                }
                else
                {
                    status = "NOK";
                }


            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(new { status, _message });
        }


        public string SendMailForgetPassword(ExecutiveMaster executive)
        {
            try
            {
                string mstr_body = string.Empty;
                string EmailTO = string.Empty;


                EmailTO = executive.Emailid;

                {
                    StringBuilder mstr_searchparameter = new StringBuilder();


                    using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ForgetPassword.html"))))
                    {
                        mstr_body = reader.ReadToEnd();
                    }

                    //    mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                    DateTime now = DateTime.Now;

                    mstr_body = mstr_body.Replace("#Date", now.ToString("D"));


                    mstr_body = mstr_body.Replace("#MailDescription", "Your Password is : " + _encryptionService.DecryptText(executive.Password, "5152549987117761"));



                    mstr_body = mstr_body.Replace("#RequstPersonName", executive.FullName());
                    mstr_body = mstr_body.Replace("#UpdateDate", now.toDDMMYYYY());
                    //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                    string mstrEmailToID = EmailTO;
                    string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                    string mstrSubject = "Forgot Password";


                    if (mstrEmailToID != "" && mstrEmailToID != null)
                    {

                        return MailSend.SendMail_str(mstr_body, mstrEmailToID, mstrFromEmailID, mstrSubject);
                    }
                    else
                    {
                        return "";
                    }


                }


            }
            catch (Exception ex)
            {

                return ex.Message;

            }
        }

    }
}

