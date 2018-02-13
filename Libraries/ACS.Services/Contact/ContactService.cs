using ACS.Core;
using ACS.Core.Caching;
using ACS.Core.Configuration;
using ACS.Core.Data;
using ACS.Core.Domain.Contact;
using ACS.Core.Infrastructure;
using ACS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.Services.Contact
{
    public partial class ContactService : IContactService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string CONTACTROLES_ALL_KEY = "ACS.customerrole.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        private const string CONTACTROLES_BY_SYSTEMNAME_KEY = "ACS.customerrole.systemname-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CONTACTROLES_PATTERN_KEY = "ACS.customerrole.";

        #endregion

        private readonly IRepository<ACS.Core.Domain.Master.ExecutiveMaster> _contactRepository;
        //private readonly IRepository<ContactRole> _contactRoleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ACS.Core.Domain.Master.ExecutiveMaster> _societyRepository;
       

        public ContactService(
           IRepository<ACS.Core.Domain.Master.ExecutiveMaster> contactRepository
           , IEncryptionService encryptionService
           //, IRepository<ContactRole> contactRoleRepository
          
           )
        {
            this._contactRepository = contactRepository;
            this._encryptionService = encryptionService;
            //this._contactRoleRepository = contactRoleRepository;
           
        }

        // insert contact details........
        public void insertContact(ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {
            if (Executive == null)
                throw new ArgumentNullException("Executive");

            _contactRepository.Insert(Executive);
        }

        // udate contact details.........
        public void updateContact(ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {

            if (Executive == null)
                throw new ArgumentNullException("Executive");

            Executive.ModifiedDate = DateTime.Now;
            _contactRepository.Update(Executive);
        }

        // delete contact details
        public void deleteContact(ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {
            if (Executive == null)
                throw new ArgumentNullException("Executive");

            _contactRepository.Delete(Executive);
        }

        // fetch contact details basis on ContactId
        public ACS.Core.Domain.Master.ExecutiveMaster  GetContactDetailById(int Executive)
        {
            if (Executive == 0)
                return null;

            if (_contactRepository.Table.Any(i => i.Id == Executive))
                return _contactRepository.Table.Where(i => i.Id == Executive).FirstOrDefault();
            else
                return null;
        }

        // using in password setting form return emailid on the basis of Id **
        // ** Id can be change on the basis of generated link will have
        public ACS.Core.Domain.Master.ExecutiveMaster  GetContactDetailByEmailID(string EmailId)
        {
            if (!string.IsNullOrEmpty(EmailId))
            {
                return _contactRepository.Table.Where(i => i.Emailid == EmailId).FirstOrDefault();
            }
            else
                return null;
        }

        // fetch all contact of flat basis on flatId
     

     

        public ContactLoginResults ValidateContactLogin(string email, string password, string ipAddress)
        {
            ACS.Core.Domain.Master.ExecutiveMaster Executive = null;
            Executive = GetContactDetailByEmailID(email);

            if (Executive == null)
                return ContactLoginResults.CustomerNotExist;
           
         
            string pwd = "";

            //pwd = _encryptionService.CreatePasswordHash(password, contact.PasswordKey, _contactSettings.HashedPasswordFormat);
            pwd = _encryptionService.EncryptText(password, "");

            bool isValid = pwd == Executive.Password;

            //save last login date
            if (isValid)
            {
                // Login history//
                // end

                return ContactLoginResults.Successful;
            }
            else
                return ContactLoginResults.WrongPassword;
        }

       
        #region Customer roles

        

        #endregion

    }
}
