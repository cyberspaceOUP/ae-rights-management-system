using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Services.Contact;
using ACS.Core.Domain.Contact;
using ACS.Core;
using Newtonsoft.Json;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Domain.Master;
using ACS.Services.Common;

using SLV.Model;
using ACS.Services.Localization;
using ACS.Services.Security;
using System.Transactions;

using ACS.Services.Master;

namespace SLV.API.Controllers.Contact
{
    public class ContactController : ApiController
    {
        private readonly IContactService _contactService;
        private readonly IExecutive _ExecutiveService;
        private readonly ILocalizationService _localizationService;
        private readonly IEncryptionService _encryptionService;
      
     
        // constructor
        public ContactController(
            IContactService contactService
         ,IExecutive ExecutiveService
            , ILocalizationService localizationService
            , IEncryptionService encryptionService
       
            )
        {
            _contactService = contactService;
            _ExecutiveService = ExecutiveService;
            _localizationService = localizationService;
            _encryptionService = encryptionService;
          
        }

        // function for fetching contact details basis on contactId
        public IHttpActionResult ContactById(ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {
            // call ACS.Services function for fetching contact details
            ACS.Core.Domain.Master.ExecutiveMaster _contactData = _contactService.GetContactDetailById(Executive.Id);

            


            return Json(SerializeObj.SerializeObject(new
            {
                Id = _contactData.Id,
               
              
                FirstName = _contactData.executiveName,
                Email = _contactData.Emailid,
                Password = _contactData.Password,
                Mobile = _contactData.Mobile,
                Phone = _contactData.Phoneno,
                DeptId = _contactData.DepartmentId,
                Proc_Trns_To = _contactData.ProcessTransferTo,
                
            }));
        }

        [HttpGet]
        public IHttpActionResult ContactDetailById(string Id)
        {
            // call ACS.Services function for fetching contact details
            int ContactId = Convert.ToInt32(Id);
            ACS.Core.Domain.Master.ExecutiveMaster Executive = _contactService.GetContactDetailById(ContactId);
            return Json(SerializeObj.SerializeObject(new { Executive.Id, Name = Executive.executiveName }));
        }

        // function for update contact details
     

        //CREATED BY AMAN KUMAR ON DATE 16/03/2016 TO UPDATECONTACTPHOTO
       

        // function for add additional contact/ family member details
       

        //function for get all contacts of the flat
     

    

        // change password for contact
        public IHttpActionResult ChangePassword(ExecutiveMaster  contact)
        {
            // initialize string for status 
            string status = string.Empty;

            try
            {

                // initialize values 
                ACS.Core.Domain.Master.ExecutiveMaster  _contactDetails = _contactService.GetContactDetailById(contact.Id);
                string OldPassword = _encryptionService.EncryptText(contact.executiveName, _ExecutiveService.KeyValue("encriptionkey"));
              
                
                if (_contactDetails.Password == OldPassword)
                {

                    _contactDetails.Password = _encryptionService.EncryptText(contact.Password, _ExecutiveService.KeyValue("encriptionkey"));
                    //Added by sanjeet on 30th may 2016
                    _contactDetails.PwdChanged = "Y";
                    //ended by sanjeet
                    _contactService.updateContact(_contactDetails);
                    ///   status = _localizationService.GetResource("Common.API.Success.Message");
                    status = "Password Changed Successfully";
                }
                else
                    //  status = _localizationService.GetResource("Common.OldPassword.NotMatch.Message");
                    status = "OldPassword Not Match";

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

  }
}
