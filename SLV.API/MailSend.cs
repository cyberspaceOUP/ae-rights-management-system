using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mail;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Web.Configuration;
//using ACS.Core.Domain.Directory;
//using ACS.Services.Directory;
//using System.Configuration;

namespace SLV.API
{
    public class MailSend
    {

        #region "Send Mail"
        public static void SendMail(string TemplateBody, string ToEmailID, string FromEmailId, string mstr_Subject)
        {
            try
            {
                List<MailTemplate> mobj_MailTemplate = new List<MailTemplate>();
                string mstr_mailbody = "";
                string mstr_mailSubject = "";
                string mstr_fromName = "";
                string mstr_fromEmail = FromEmailId;
                string mstr_to = ToEmailID;
                string mstr_cc = "";
                string mstr_bcc = "";
                string mstr_attachment = "";
                mstr_mailbody = TemplateBody;
                mstr_mailSubject = mstr_Subject;
                MailSender mobjMailSender = new MailSender(mstr_fromName, mstr_fromEmail);
                mobj_MailTemplate.Add(new MailTemplate(mstr_to.Split(','), mstr_cc.Split(','), mstr_bcc.Split(','), mstr_attachment.Split(','), mstr_mailbody, mstr_mailSubject, true));
                mobj_MailTemplate = mobjMailSender.SendMail(mobj_MailTemplate);

            }
            catch (Exception ex)
            {

            }
        }

        public static string SendMail_str(string TemplateBody, string ToEmailID, string FromEmailId, string mstr_Subject)
        {
            try
            {
                List<MailTemplate> mobj_MailTemplate = new List<MailTemplate>();
                string mstr_mailbody = "";
                string mstr_mailSubject = "";
                string mstr_fromName = "";
                string mstr_fromEmail = FromEmailId;
                string mstr_to = ToEmailID;
                string mstr_cc = "";
                string mstr_bcc = "";
                string mstr_attachment = "";
                mstr_mailbody = TemplateBody;
                mstr_mailSubject = mstr_Subject;
                bool mbool_IsSuccess = false;
                string mstr_StatusDesc = string.Empty;
                MailSender mobjMailSender = new MailSender(mstr_fromName, mstr_fromEmail);
                mobj_MailTemplate.Add(new MailTemplate(mstr_to.Split(','), mstr_cc.Split(','), mstr_bcc.Split(','), mstr_attachment.Split(','), mstr_mailbody, mstr_mailSubject, true));
                mobj_MailTemplate = mobjMailSender.SendMail(mobj_MailTemplate);
                //return "OK";
                if (mobj_MailTemplate.Count > 0)
                {
                    mbool_IsSuccess = mobj_MailTemplate[0].IsSuccess;
                    mstr_StatusDesc = mobj_MailTemplate[0].StatusDesc;
                }
                if (mbool_IsSuccess)
                {
                    return "OK";
                }
                else
                {
                    return mstr_StatusDesc;
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion



        #region "Send Mail With EmailCCTo and EmailBCCTo"
        public static void SendMailNew(string TemplateBody, string ToEmailID, string FromEmailId, string EmailCCTo, string EmailBCCTo, string mstr_Subject)
        {
            try
            {
                List<MailTemplate> mobj_MailTemplate = new List<MailTemplate>();
                string mstr_mailbody = "";
                string mstr_mailSubject = "";
                string mstr_fromName = "";
                string mstr_fromEmail = FromEmailId;
                string mstr_to = ToEmailID;
                string mstr_cc = EmailCCTo;
                string mstr_bcc = EmailBCCTo;
                string mstr_attachment = "";
                mstr_mailbody = TemplateBody;
                mstr_mailSubject = mstr_Subject;
                MailSender mobjMailSender = new MailSender(mstr_fromName, mstr_fromEmail);
                mobj_MailTemplate.Add(new MailTemplate(mstr_to.Split(','), mstr_cc.Split(','), mstr_bcc.Split(','), mstr_attachment.Split(','), mstr_mailbody, mstr_mailSubject, true));
                mobj_MailTemplate = mobjMailSender.SendMail(mobj_MailTemplate);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region "Send Mail With EmailCCTo and EmailBCCTo"
        public static void SendMailBySmtpClient(string TemplateBody, string ToEmailID, string FromEmailId, string EmailCCTo, string EmailBCCTo, string mstr_Subject)
        {
            try
            {
                string htmlBody = TemplateBody;
                //string htmlBody = "<html><body><h1>Picture</h1><br><img src=\"cid:Pic1\" alt='Rights Management System' /></body></html>";
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                // Create a LinkedResource object for each embedded image
                LinkedResource pic1 = new LinkedResource(WebConfigurationManager.AppSettings["Attach_Logo_Path"].ToString(), MediaTypeNames.Image.Jpeg);
                pic1.ContentId = "Pic1";
                avHtml.LinkedResources.Add(pic1);

                // Add the alternate views instead of using MailMessage.Body
                MailMessage message = new MailMessage();
                message.AlternateViews.Add(avHtml);

                message.Subject = mstr_Subject;

                // Address and send the message
                message.From = new MailAddress(FromEmailId, WebConfigurationManager.AppSettings["EmailFrom_Name"].ToString());
                message.To.Add(new MailAddress(ToEmailID, ToEmailID));
                if (!string.IsNullOrEmpty(EmailCCTo))
                    message.CC.Add(new MailAddress(EmailCCTo, EmailCCTo));
                if (!string.IsNullOrEmpty(EmailBCCTo))
                    message.Bcc.Add(new MailAddress(EmailBCCTo, EmailBCCTo)); 

                SmtpClient client = new SmtpClient();
                client.Host = WebConfigurationManager.AppSettings["SmtpHost"].ToString();
                client.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SmtpPort"]);
                client.UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["AuthenticationMail"]);
                client.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["SmtpUserName"].ToString(), WebConfigurationManager.AppSettings["SmtpPassword"].ToString());
                client.Send(message);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


    }
}