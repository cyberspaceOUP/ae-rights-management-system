//Create by Saddam 
using ACS.Core;
using ACS.Services.Authentication;
using ACS.Services.Contact;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Services.Product;
using ACS.Services.Security;
using ACS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.OtherContract;
using ACS.Services.Other_Contract;
using ACS.Data;
using ACS.Core;
using SLV.Model.Common;
using ACS.Services.AuthorContract;

using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Services.User;
using System.Text;
using ACS.Services.Alert;
using ACS.Core.Domain.Alert;
using Logger;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Configuration;

namespace SLV.API.Controllers.Alert
{
    public class AlertController : ApiController
    {
        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        // GET: /Alert/
        private readonly IProductMasterService _ProductMasterService;
        private readonly IDbContext _dbContext;
        private readonly IServiceApplicationEmailSetup _IServiceApplicationEmailSetup;

        public AlertController(

             IProductMasterService ProductMasterService
              , IDbContext dbContext
            , IServiceApplicationEmailSetup IServiceApplicationEmailSetup
            )
        {
            this._ProductMasterService = ProductMasterService;
            this._IServiceApplicationEmailSetup = IServiceApplicationEmailSetup;
            this._dbContext = dbContext;
        }


        #region "Send Mail Code For Pending Author Contract"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Pending Author Contract
         */

        public IList<DashBoardModel> PendingAuthorContractRequestData(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PendingAuthorContractRequest_get", parameters).ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   : 27th Sep., 2016
     
       */
        public IHttpActionResult PendingAuthorContractRequestList(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PendingRequestContract";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);

                SendMailPendingRequestContract(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PendingAuthorContractRequestList", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PendingAuthorContractRequestList", ex);
            }
            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 27th Sep., 2016
      * Purpose      : To Send Mail Code For Pending Author Contract
        */
        public void SendMailPendingRequestContract(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();

                _mobjReportList = PendingAuthorContractRequestData(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();

                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Assignment Contract Request - Pending For Validation</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Assignment Contract Request - Pending For Validation</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Entry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Contract_Code + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PendingRequestContract.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#PendingRequestContractList", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PendingRequestContract"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PendingRequestContract");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PendingRequestContract");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PendingRequestContract");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PendingRequestContract");

                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestContract", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestContract", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Pending License"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Pending License
         */
        public IList<DashBoardModel> PendingPendingRequestLicense()
        {
            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicensePendingRequest_get").ToList();
            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ProductLicensePendingRequest()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PendingRequestLicense";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPendingRequestLicense();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicensePendingRequest", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicensePendingRequest", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Pending License
       */
        public void SendMailPendingRequestLicense()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = PendingPendingRequestLicense().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Pending Request for Product License</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Pending Request for Product License</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Request date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By </b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PendingRequestLicense.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PendingRequestContract"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PendingRequestLicense");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PendingRequestLicense");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PendingRequestLicense");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PendingRequestLicense");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestLicense", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestLicense", ex);
            }
        }

        #endregion

        #region "Send Mail Code For ISBN_Not_Entered"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of ISBN_Not_Entered
         */
        public IList<DashBoardModel> ISBN_Not_Entered(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Product_ISBN_Entered_Is_NOT_Null_Dash_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult Product_ISBN_enteredList(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ISBNNotEntered";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);

                SendMailPendingISBN_Not_Entered(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "Product_ISBN_enteredList", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "Product_ISBN_enteredList", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Pending License
       */
        public void SendMailPendingISBN_Not_Entered(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ISBN_Not_Entered(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>ISBN Not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>ISBN Not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Projected Publishing Date</b></td>");
                            //mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProjectedPublishingDate + "</td>");
                                //mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ISBNNotEntered.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ISBNNotEntered"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ISBNNotEntered");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ISBNNotEntered");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ISBNNotEntered");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ISBNNotEntered");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }

                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingISBN_Not_Entered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingISBN_Not_Entered", ex);
            }
        }

        #endregion

        #region "Send Mail Code For SAP Agreement Number Not Entered"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of SAP Agreement Number Not Entered
         */
        public IList<DashBoardModel> SAPAgreementNumberNotEntered(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Product_SAP_Agr_No_Not_Entered_Desh_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult Product_SAP_Agr_No_Not_Entered(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "SAPAgreementNumberNotEntered";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailSAPAgreementNumberNotEntered(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "Product_SAP_Agr_No_Not_Entered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "Product_SAP_Agr_No_Not_Entered", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For SAP Agreement Number Not Entered
       */
        public void SendMailSAPAgreementNumberNotEntered(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = SAPAgreementNumberNotEntered(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>SAP Agreement Number Not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>SAP Agreement Number Not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Projected Publishing Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/SAPAgreementNumberNotEntered.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("SAPAgreementNumberNotEntered"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("SAPAgreementNumberNotEntered");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("SAPAgreementNumberNotEntered");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("SAPAgreementNumberNotEntered");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("SAPAgreementNumberNotEntered");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailSAPAgreementNumberNotEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailSAPAgreementNumberNotEntered", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Rights Selling Contract Expir yDate"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Rights Selling Contract Expiry Date
         */
        public IList<DashBoardModel> RightsSellingContractExpiryDate(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightSaleContractExpiring_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult RightSaleContractExpiring(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "RightsSellingContractExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);

                SendMailRightsSellingContractExpiryDate(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightSaleContractExpiring", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightSaleContractExpiring", ex);
            }
            return null;
        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Rights Selling Contract Expiry Date
       */
        public void SendMailRightsSellingContractExpiryDate(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = RightsSellingContractExpiryDate(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {

                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Right Sales Contracts Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Right Sales Contracts Expiring Within 3 Months</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");

                            //  mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product	</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");


                            //   mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Territorial Rights</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Projected Publishing Date</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date	</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");



                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");
                                //  mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");

                                //  mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Territoryrights + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/RightsSellingContractExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("RightsSellingContractExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("RightsSellingContractExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("RightsSellingContractExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("RightsSellingContractExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("RightsSellingContractExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingContractExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingContractExpiryDate", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Author Contract Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Author Contract Expiry Date
         */
        public IList<DashBoardModel> AuthorContractExpiryDate()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_AuthorContractContractExpiring_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult AuthorContractContractExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "AuthorContractExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailAuthorContractExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "AuthorContractContractExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "AuthorContractContractExpiryDate", ex);
            }

            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Rights Selling Contract Expiry Date
       */
        public void SendMailAuthorContractExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = AuthorContractExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Assignment Contracts Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Assignment Contracts Expiring Within 3 Months</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Code</b></td>");
                            //mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Entry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Expiry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");



                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Contract_Code + "</td>");
                                //mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");                                
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/AuthorContractExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("AuthorContractExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("AuthorContractExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("AuthorContractExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("AuthorContractExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("AuthorContractExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailAuthorContractExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailAuthorContractExpiryDate", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Product license Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Product license Expiry Date
         */
        public IList<DashBoardModel> ProductlicenseExpiryDate()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductlicenseExpiring_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ProductlicenseExpiryDateExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ProductlicenseExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailProductlicenseExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductlicenseExpiryDateExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductlicenseExpiryDateExpiryDate", ex);
            }
            return null;
        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Product license Expiry Date
       */
        public void SendMailProductlicenseExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ProductlicenseExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Product license Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Product license Expiring Within 3 Months</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Request Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");



                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.WorkingProduct + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");


                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ProductlicenseExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ProductlicenseExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ProductlicenseExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ProductlicenseExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ProductlicenseExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ProductlicenseExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductlicenseExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductlicenseExpiryDate", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Contract Addendum Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Contract Addendum Expiry Date
         */
        public IList<DashBoardModel> ListContractAddendumExpiryDate()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ContractAddendumExpiryDate_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ContractAddendumExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ContractAddendumExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailContractAddendumExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ContractAddendumExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ContractAddendumExpiryDate", ex);
            }

            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Contract Addendum Expiry Date
       */
        public void SendMailContractAddendumExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListContractAddendumExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Contract Addendum Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Contract Addendum Expiring Within 3 Months</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");                            

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Addendum Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Contract_Code + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ContractAddendumExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ContractAddendumExpiryDate"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ContractAddendumExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ContractAddendumExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ContractAddendumExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ContractAddendumExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailContractAddendumExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailContractAddendumExpiryDate", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Product License Addendum Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Product License Addendum Expiry Date
         */
        public IList<DashBoardModel> ListProductLicenseAddendumExpiryDate()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseAddendumExpiryDate_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ProductLicenseAddendumExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ProductLicenseAddendumExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailProductLicenseAddendumExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicenseAddendumExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicenseAddendumExpiryDate", ex);
            }

            return null;
        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Product License Addendum Expiry Date
       */
        public void SendMailProductLicenseAddendumExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListProductLicenseAddendumExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Product License Addendum Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Product License Addendum Expiring Within 3 Months</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Addendum Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ProductLicenseAddendumExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ProductLicenseAddendumExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ProductLicenseAddendumExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ProductLicenseAddendumExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ProductLicenseAddendumExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ProductLicenseAddendumExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductLicenseAddendumExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductLicenseAddendumExpiryDate", ex);
            }
        }

        #endregion

        #region "Send Mail Code For ISBN left"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of ISBN left
         */
        public IList<DashBoardModel> ListISBNleft()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ISBN_left_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ISBNleft()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ISBNleft";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailISBNleft();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ISBNleft", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ISBNleft", ex);
            }

            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For ISBN left
       */
        public void SendMailISBNleft()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();

                _mobjReportList = ListISBNleft().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>ISBN left</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>ISBN left</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Type</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Status</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.typename + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Status + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ISBNleft.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ISBNleft"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ISBNleft");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ISBNleft");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ISBNleft");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ISBNleft");

                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailISBNleft", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailISBNleft", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Contributor Entered"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Contributor Entered
         */
        public IList<DashBoardModel> ListContributorEntered()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ContributorEntered_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ContributorEntered()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ContributorEntered";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailContributorEntered();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ContributorEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ContributorEntered", ex);
            }

            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Contributor Entered
       */
        public void SendMailContributorEntered()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();

                _mobjReportList = ListContributorEntered().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();

                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Contributor is Entered and Contributor Agreement is not Uploaded</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Contributor is Entered and Contributor Agreement is not Uploaded</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Entry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Contract_Code + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.WorkingProduct + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.EntryDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");
                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ContributorEntered .html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ContributorEntered"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ContributorEntered");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ContributorEntered");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ContributorEntered");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ContributorEntered");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailContributorEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailContributorEntered", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Product License Entered "
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Product License Entered
         */
        public IList<DashBoardModel> ListProductLicenseEntered()
        {
            var _GetStatus = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseEntered_get").ToList();
            return _GetStatus;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 27th Sep., 2016
     
      */
        public IHttpActionResult ProductLicenseEntered()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "ProductLicenseEntered";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailProductLicenseEntered();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicenseEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "ProductLicenseEntered", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   : 27th Sep., 2016
     * Purpose      : To Send Mail Code For Product License Entered
       */
        public void SendMailProductLicenseEntered()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListProductLicenseEntered().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Product license is Entered for Translation, Adaptation and Custom and Assignment Contract is not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Product license is Entered for Translation, Adaptation and Custom and Assignment Contract is not Entered</b> " + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Working Product</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry Date</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");



                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.WorkingProduct + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.EntryDate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");


                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/ProductLicenseEntered.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("ProductLicenseEntered"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("ProductLicenseEntered");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("ProductLicenseEntered");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("ProductLicenseEntered");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("ProductLicenseEntered");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }                     
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductLicenseEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailProductLicenseEntered", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Pending Request Other Contract"
        /*
        * Added by     : Saddam
        * Added Date   : 27th Sep., 2016
        * Purpose      : To Get List Of Pending Request Other Contract
         */

        public IList<DashBoardModel> ListPendingRequestOtherContract(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_Pending_Request_OtherContract_get", parameters).ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   : 27th Sep., 2016
     
       */
        public IHttpActionResult PendingRequestOtherContract(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PendingRequestOtherContract";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPendingRequestOtherContract(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PendingRequestOtherContract", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PendingRequestOtherContract", ex);
            }

            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 27th Sep., 2016
      * Purpose      : To Send Mail Code For Pending Request Other Contract
        */
        public void SendMailPendingRequestOtherContract(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListPendingRequestOtherContract(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Pending Request For Other Contract</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Pending Request For Other Contract</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Other Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Vendor Name</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Date </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expire Date </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Status </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.othercontractcode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.partyname + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ContractDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expiredate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Contractstatus + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }


                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PendingRequestOtherContract.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PendingRequestOtherContract"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PendingRequestOtherContract");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PendingRequestOtherContract");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PendingRequestOtherContract");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PendingRequestOtherContract");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestOtherContract", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPendingRequestOtherContract", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Other Contract Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Other Contract Expiry Date
         */

        public IList<DashBoardModel> ListOtherContractExpiryDate()
        {
            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_OtherContractExpiryDate_get").ToList();
            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :29th Sep., 2016
     
       */
        public IHttpActionResult OtherContractExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "OtherContractExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailOtherContractExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "OtherContractExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "OtherContractExpiryDate", ex);
            }

            return null;

        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Other Contract Expiry Date
        */
        public void SendMailOtherContractExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListOtherContractExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Other Contract Expiring Within 3 Months </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Other Contract Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Other Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Date </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expire Date </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Status </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.othercontractcode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ContractDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expiredate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Contractstatus + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/OtherContractExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("OtherContractExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("OtherContractExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("OtherContractExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("OtherContractExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("OtherContractExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailOtherContractExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailOtherContractExpiryDate", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Balance Quantity Addendum"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Balance Quantity Addendum
         */
        public IList<DashBoardModel> ListBalanceQuantityAddendum(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseAddendumExpired_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 29th Sep., 2016
     
      */
        public IHttpActionResult BalanceQuantityAddendum(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "BalanceQuantityAddendum";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailBalanceQuantityAddendum(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "BalanceQuantityAddendum", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "BalanceQuantityAddendum", ex);
            }
            return null;
        }

        /*
      * Added by     : Saddam
      * Added Date   :29th Sep., 2016
     * Purpose      : To Send Mail Code For Balance Quantity Addendum
       */
        public void SendMailBalanceQuantityAddendum(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListBalanceQuantityAddendum(Flag).ToList();


                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Balance Quantity of Addendum less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Balance Quantity of Addendum less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Addendum Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AddendumDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");
                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/BalanceQuantityAddendum.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("BalanceQuantityAddendum"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("BalanceQuantityAddendum");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("BalanceQuantityAddendum");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("BalanceQuantityAddendum");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("BalanceQuantityAddendum");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }

                    }
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailBalanceQuantityAddendum", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailBalanceQuantityAddendum", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Inbound Permission Not Entered"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Inbound Permission Not Entered
         */
        public IList<DashBoardModel> ListInboundPermissionNotEntered(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_InboundPermissionNotEntered_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 29th Sep., 2016
     
      */
        public IHttpActionResult InboundPermissionNotEntered(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "InboundPermissionNotEntered";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailInboundPermissionNotEntered(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "InboundPermissionNotEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "InboundPermissionNotEntered", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   :29th Sep., 2016
     * Purpose      : To Send Mail Code For Inbound Permission Not Entered
       */
        public void SendMailInboundPermissionNotEntered(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();

                _mobjReportList = ListInboundPermissionNotEntered(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();

                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>List of Product – Inbound Permission not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>List of Product – Inbound Permission not Entered</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            //mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Contract Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                //mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Contract_Code + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }

                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/InboundPermissionNotEntered.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("InboundPermissionNotEntered"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("InboundPermissionNotEntered");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("InboundPermissionNotEntered");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("InboundPermissionNotEntered");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("InboundPermissionNotEntered");

                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailInboundPermissionNotEntered", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailInboundPermissionNotEntered", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Balance Quantity Product License"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Balance Quantity Product License
         */
        public IList<DashBoardModel> ListBalanceQuantityProductLicense(DashBoardModel Flag)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("Flag", SqlDbType.VarChar, 200);
            parameters[0].Value = "'" + Flag.Flag + "'";

            var _GetList = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_ProductLicenseExpired_get", parameters).ToList();

            return _GetList;
        }

        /*
         * Added by     : Saddam
         * Added Date   : 29th Sep., 2016
     
      */
        public IHttpActionResult BalanceQuantityProductLicense(DashBoardModel Flag)
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "BalanceQuantityProductLicense";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailBalanceQuantityProductLicense(Flag);
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "BalanceQuantityProductLicense", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "BalanceQuantityProductLicense", ex);
            }
            return null;

        }

        /*
      * Added by     : Saddam
      * Added Date   :29th Sep., 2016
     * Purpose      : To Send Mail Code For Balance Quantity Product License
       */
        public void SendMailBalanceQuantityProductLicense(DashBoardModel Flag)
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListBalanceQuantityProductLicense(Flag).ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Balance quantity of Product License less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Balance quantity of Product License less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product License Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Product Code</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>ISBN</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Publishing Company</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Agreement Date</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry Date</b></td>");
                            // mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Author Name</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Enter By</b></td>");

                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.ProductLicensecode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ProductCode + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ISBN + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.PublishingCompany + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AgreementDate + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");
                                //  mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AuthorName + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");
                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/BalanceQuantityProductLicense.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("BalanceQuantityProductLicense"));

                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("BalanceQuantityProductLicense");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("BalanceQuantityProductLicense");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("BalanceQuantityProductLicense");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("BalanceQuantityProductLicense");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }

                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailBalanceQuantityProductLicense", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailBalanceQuantityProductLicense", ex);
            }
        }

        #endregion

        #region "Send Mail Code For Permission Out-bound Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Permission Out-bound Expiry Date
         */

        public IList<DashBoardModel> ListPermissionOutboundExpiryDate()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionOutboundExpiryDate_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :29th Sep., 2016
     
       */
        public IHttpActionResult PermissionOutboundExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PermissionOutboundExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPermissionOutboundExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionOutboundExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionOutboundExpiryDate", ex);
            }

            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Permission Out-bound Expiry Date
        */
        public void SendMailPermissionOutboundExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListPermissionOutboundExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Permission Out-bound Expiring Within 3 Months </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Permission Out-bound Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Permissions Out-bound Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Licensee Code </b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Request Date</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.PermissionsOutboundCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.LicenseeCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PermissionOutboundExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PermissionOutboundExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PermissionOutboundExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PermissionOutboundExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PermissionOutboundExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PermissionOutboundExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionOutboundExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionOutboundExpiryDate", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Permission In-bound Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 29th Sep., 2016
        * Purpose      : To Get List Of Permission In-bound Expiry Date
         */

        public IList<DashBoardModel> ListPermissionInboundExpiryDate()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionInboundExpiryDate_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :29th Sep., 2016
     
       */
        public IHttpActionResult PermissionInboundExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PermissionInboundExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPermissionInboundExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionInboundExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionInboundExpiryDate", ex);
            }

            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Permission In-bound Expiry Date
        */
        public void SendMailPermissionInboundExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListPermissionInboundExpiryDate().ToList();


                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Permission In-bound Expiring Within 3 Months </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Permission In-bound Expiring Within 3 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Permissions In-bound Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Assets Type </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Expiry Date </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AssetsType + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PermissionInboundExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PermissionInboundExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PermissionInboundExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PermissionInboundExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PermissionInboundExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PermissionInboundExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionInboundExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionInboundExpiryDate", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Recurring Expiry Date"
        /*
        * Added by     : Saddam
        * Added Date   : 30th Sep., 2016
        * Purpose      : To Get List Of Recurring Expiry Date
         */
        public IList<DashBoardModel> ListRecurringExpiryDate()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RecurringExpiryDate_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :30th Sep., 2016
     
       */
        public IHttpActionResult RecurringExpiryDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "RecurringExpiryDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailRecurringExpiryDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RecurringExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RecurringExpiryDate", ex);
            }
            return null;

        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Recurring Expiry Date
        */
        public void SendMailRecurringExpiryDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListRecurringExpiryDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Rights Selling Recurring Expiring Within 2 Months </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Rights Selling Recurring Expiring Within 2 Months</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Rights Selling Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Licensee Code </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring From Period </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring To Period </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.LicenseeCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Expirydate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/RecurringExpiryDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("RecurringExpiryDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("RecurringExpiryDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("RecurringExpiryDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("RecurringExpiryDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("RecurringExpiryDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRecurringExpiryDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRecurringExpiryDate", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Payment is not Received 30 days Post Invoice Date"
        /*
        * Added by     : Saddam
        * Added Date   : 30th Sep., 2016
        * Purpose      : To Get List Of Payment is not Received 30 days Post Invoice Date
         */

        public IList<DashBoardModel> ListPermissionOutboundReceivedInvoiceDate()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionOutboundReceivedInvoiceDate_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :30th Sep., 2016
     
       */
        public IHttpActionResult PermissionOutboundReceivedInvoiceDate()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PermissionOutboundReceivedInvoiceDate";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPermissionOutboundReceivedInvoiceDate();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionOutboundReceivedInvoiceDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionOutboundReceivedInvoiceDate", ex);
            }

            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Payment is not Received 30 days Post Invoice Date
        */
        public void SendMailPermissionOutboundReceivedInvoiceDate()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListPermissionOutboundReceivedInvoiceDate().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Payment is not Received 30 days Post For Invoice Date </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Payment is not Received 30 days Post For Invoice Date</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Permissions Out-bound Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Licensee Code </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Date of Invoice </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Status </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.LicenseeCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.DateOfInvoice + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Status + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");
                        }


                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PermissionOutboundReceivedInvoiceDate.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PermissionOutboundReceivedInvoiceDate"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PermissionOutboundReceivedInvoiceDate");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PermissionOutboundReceivedInvoiceDate");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PermissionOutboundReceivedInvoiceDate");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PermissionOutboundReceivedInvoiceDate");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {
                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionOutboundReceivedInvoiceDate", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionOutboundReceivedInvoiceDate", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Permission - Inbound Balance quantity less than 25%"
        /*
        * Added by     : Saddam
        * Added Date   : 30th Sep., 2016
        * Purpose      : To Get List Of Permission - Inbound Balance quantity less than 25%
         */

        public IList<DashBoardModel> ListPermissionInboundBalanceQuantity()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_PermissionInboundBalanceQuantity_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :30th Sep., 2016
     
       */
        public IHttpActionResult PermissionInboundBalanceQuantity()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "PermissionInboundBalanceQuantity";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailPermissionInboundBalanceQuantity();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionInboundBalanceQuantity", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "PermissionInboundBalanceQuantity", ex);
            }

            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail Code For Permission - Inbound Balance quantity less than 25%
        */
        public void SendMailPermissionInboundBalanceQuantity()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListPermissionInboundBalanceQuantity().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Permission - Inbound Balance quantity less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Permission - Inbound Balance quantity less than 25%</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Permissions In-bound Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Assets Type </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Balance Percentage </b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Balance Counts </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.AssetsType + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.BalancePercentage + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.BalanceCounts + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/PermissionInboundBalanceQuantity.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("PermissionInboundBalanceQuantity"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("PermissionInboundBalanceQuantity");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("PermissionInboundBalanceQuantity");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("PermissionInboundBalanceQuantity");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("PermissionInboundBalanceQuantity");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionInboundBalanceQuantity", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailPermissionInboundBalanceQuantity", ex);
            }
        }
        #endregion

        #region "Send Mail Code For RightsSelling Advance Payment 30 Days"
        /*
        * Added by     : Saddam
        * Added Date   : 14 Oct., 2016
        * Purpose      : To Get List Of RightsSelling Advance Payment 30 Days
         */

        public IList<DashBoardModel> ListRightsSellingAdvancePayment()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightsSelling_AdvancePayment_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :14 Oct, 2016
     
       */
        public IHttpActionResult RightsSellingAdvancePayment()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "RightsSellingAdvancePayment";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);

                SendMailRightsSellingAdvancePayment();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAdvancePayment", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAdvancePayment", ex);
            }
            return null;
        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail RightsSelling Advance Payment 30 Days
        */
        public void SendMailRightsSellingAdvancePayment()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListRightsSellingAdvancePayment().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Rights Selling Advance Payment within 30 Days</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Rights Selling Advance Payment with in 30 Days</b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Rights Selling Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Licensee Code</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring From Period</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring To Period </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Amount</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry Date</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.LicenseeCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.FromDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ToDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Amount + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/RightsSellingAdvancePayment.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("RightsSellingAdvancePayment"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("RightsSellingAdvancePayment");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("RightsSellingAdvancePayment");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("RightsSellingAdvancePayment");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("RightsSellingAdvancePayment");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }



                    }
                }



            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingAdvancePayment", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingAdvancePayment", ex);
            }
        }
        #endregion

        #region "Send Mail Code For Rights Selling Alert Frequency"
        /*
        * Added by     : Saddam
        * Added Date   : 14 Oct., 2016
        * Purpose      : To Get List Of Rights Selling Alert Frequency
         */

        public IList<DashBoardModel> ListRightsSellingAlertFrequency()
        {

            var _GetPendingAuthorContractRequest = _dbContext.ExecuteStoredProcedureListNewData<DashBoardModel>("Proc_RightsSellingAlertFrequency_get").ToList();

            return _GetPendingAuthorContractRequest;
        }

        /*
          * Added by     : Saddam
          * Added Date   :14 Oct, 2016
     
       */
        public IHttpActionResult RightsSellingAlertFrequency()
        {
            try
            {
                AlertSchedulerMaster _AlertSchedulerMaster = new AlertSchedulerMaster();
                _AlertSchedulerMaster.SchedulerName = "RightsSellingAlertFrequency";
                _AlertSchedulerMaster.SchedulerDate = DateTime.Now;

                _IServiceApplicationEmailSetup.InsertAlertScheduler(_AlertSchedulerMaster);
                SendMailRightsSellingAlertFrequency();
                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAlertFrequency", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAlertFrequency", ex);
            }
            return null;

        }

        /*
       * Added by     : Saddam
       * Added Date   : 29th Sep., 2016
      * Purpose      : To Send Mail RightsSelling Advance Payment 30 Days
        */
        public void SendMailRightsSellingAlertFrequency()
        {
            try
            {
                string mstr_body = string.Empty;

                List<DashBoardModel> _mobjReportList = new List<DashBoardModel>();


                _mobjReportList = ListRightsSellingAlertFrequency().ToList();

                if (_mobjReportList.Count > 0)
                {
                    {
                        StringBuilder mstr_searchparameter = new StringBuilder();


                        if (_mobjReportList.Count == 0)
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center>" + "<b>Rights Selling Alert Frequency </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center >No result found</td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");

                            mstr_searchparameter.Append("</td></tr></table>");
                        }
                        else
                        {
                            mstr_searchparameter.Append("<table width='100%'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td  style='width: 100%;' align='left' valign='top'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;font-size: 13px' valign='top' align=center colspan='2'>" + "<b>Rights Selling Alert Frequency </b>" + "</td>");
                            mstr_searchparameter.Append("</tr>");



                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td style='width: 100%;' valign='top' align=center colspan='2'><br /></td>");
                            mstr_searchparameter.Append("</tr>");

                            mstr_searchparameter.Append("</table>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td colspan='2'>");
                            mstr_searchparameter.Append("<table width='100%' cellpadding='0'border='1%'  cellspacing='0'>");
                            mstr_searchparameter.Append("<tr>");
                            mstr_searchparameter.Append("<td    style='font-size: 13px'  align='Center'><b>SNo.</b></td>");
                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Rights Selling Code</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Licensee Code</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring From Period</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Recurring To Period </b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Amount</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Frequency</b></td>");

                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry Date</b></td>");


                            mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'><b>Entry By </b></td>");


                            mstr_searchparameter.Append("</tr>");
                            mstr_searchparameter.Append("</td>");
                            int mint_Counter = 1;
                            foreach (DashBoardModel data in _mobjReportList)
                            {
                                mstr_searchparameter.Append("<tr>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + mint_Counter + "</td>");
                                mstr_searchparameter.Append("<td style='font-size: 13px'   align='Center'>" + data.Code + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.LicenseeCode + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.FromDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ToDate + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Amount + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.Frequency + "</td>");

                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.EntryDate + "</td>");


                                mstr_searchparameter.Append("<td style='font-size: 13px'  align='Center'>" + data.ExecutiveName + "</td>");

                                mstr_searchparameter.Append("</tr>");
                                mint_Counter++;
                            }
                            mstr_searchparameter.Append("</table></td></tr></table>");

                        }



                        using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/RightsSellingAlertFrequency.html"))))
                        {
                            mstr_body = reader.ReadToEnd();
                        }

                        mstr_body = mstr_body.Replace("#List", mstr_searchparameter.ToString());

                        DateTime now = DateTime.Now;

                        //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                        mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));


                        mstr_body = mstr_body.Replace("#MailDescription", _IServiceApplicationEmailSetup.getMailDescriptionByKey("RightsSellingAlertFrequency"));


                        //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));


                        string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("RightsSellingAlertFrequency");
                        string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                        string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("RightsSellingAlertFrequency");

                        string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("RightsSellingAlertFrequency");

                        string mstrSubject = _IServiceApplicationEmailSetup.getSubjectByKey("RightsSellingAlertFrequency");


                        if (mstrEmailToID != "" && mstrEmailToID != null)
                        {

                            //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                            MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                        }
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingAlertFrequency", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "SendMailRightsSellingAlertFrequency", ex);
            }
        }
        #endregion


        #region "Send Mail Test"
        public IHttpActionResult TestMail()
        {
            try
            {
                string mstr_body = string.Empty;

                using (StreamReader reader = new StreamReader(Path.Combine(System.Web.HttpContext.Current.Server.MapPath(@"~/MailHtml/TestMail.html"))))
                {
                    mstr_body = reader.ReadToEnd();
                }

                mstr_body = mstr_body.Replace("#List", "Thank you for mail.");

                DateTime now = DateTime.Now;
                //mstr_body = mstr_body.Replace("#Date", now.ToString("D"));
                mstr_body = mstr_body.Replace("#Date", now.ToString("dd MMMM yyyy"));

                mstr_body = mstr_body.Replace("#MailDescription", "Test Mail");

                //mstr_body = mstr_body.Replace("#websiteImageURl#", _IServiceApplicationEmailSetup.getMailDescriptionByKey("FileUploadURL"));

                string mstrEmailToID = _IServiceApplicationEmailSetup.getEmailToIdByKey("RightsSellingAlertFrequency");

                string mstrFromEmailID = _IServiceApplicationEmailSetup.getFromEmailIdByKey("FromEmailId");

                string mstrEmailCCToID = _IServiceApplicationEmailSetup.getEmailCCToIdByKey("RightsSellingAlertFrequency");

                string mstrEmailBCCToID = _IServiceApplicationEmailSetup.getEmailBCCToIdByKey("RightsSellingAlertFrequency");

                string mstrSubject = "Test Mail OUP-RMS";

                if (mstrEmailToID != "" && mstrEmailToID != null)
                {
                    //MailSend.SendMailNew(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                    MailSend.SendMailBySmtpClient(mstr_body, mstrEmailToID, mstrFromEmailID, mstrEmailCCToID, mstrEmailBCCToID, mstrSubject);
                }

                return null;
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAlertFrequency", ex);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "AlertController.cs", "RightsSellingAlertFrequency", ex);
            }
            return null;

        }
        #endregion
        

    }
}
