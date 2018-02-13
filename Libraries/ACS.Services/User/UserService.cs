using ACS.Core.Data;
using ACS.Core.Domain.User;
using ACS.Core.Domain.Master;
using ACS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using ACS.Core;
using ACS.Services.Master;
namespace ACS.Services.User
{

    public partial class UserService : IUserService
    {
        private readonly IRepository<ExecutiveMaster > _userMasterRepository;
        private readonly IRepository<DepartmentMaster> _userDepartmentRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IExecutive _execcutiveService;
        private readonly IRepository<ApplicationSetUp> _ApplicationSetUp;
        public UserService(
            IRepository<ExecutiveMaster> userMasterRepository
            , IEncryptionService encryptionService
            , IRepository<DepartmentMaster> userDepartmentRepository
            , IExecutive execcutiveService
            , IRepository<ApplicationSetUp> ApplicationSetUp
        )
        {
            _userMasterRepository = userMasterRepository;
            _encryptionService = encryptionService;
            _userDepartmentRepository = userDepartmentRepository;
            _execcutiveService = execcutiveService;
            _ApplicationSetUp = ApplicationSetUp;

        }

        /// <summary>
        /// Gets a customer by GUID
        /// </summary>
        /// <param name="customerGuid">Customer GUID</param>
        /// <returns>A customer</returns>
        //public virtual ExecutiveMaster GetUserByGuid(Guid userGuid)
        //{
        //    ;
        //    if (userGuid == Guid.Empty)
        //        return null;

        //    var query = from u in _userMasterRepository.Table
        //                where u.Id == userGuid
        //                orderby u.Id
        //                select u;
        //    var employee = query.FirstOrDefault();
        //    return employee;
        //}

        /// <summary>
        /// Gets a user by userName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>A user</returns>
        public ACS.Core.Domain.Master.ExecutiveMaster  GetUserDetailByUserName(string userName)
        {

            ExecutiveMaster ExecutiveMastertbl = _userMasterRepository.Table.Where(i => i.Emailid == userName && i.Deactivate == "N" && i.block == "N").FirstOrDefault();
            if (ExecutiveMastertbl!=null)
            {
                ExecutiveMastertbl.DepartmentM = _userDepartmentRepository.Table.Where(d => d.Id == ExecutiveMastertbl.DepartmentId && d.Deactivate == "N").FirstOrDefault();
            }
            return ExecutiveMastertbl;
        }


        public ACS.Core.Domain.Master.ApplicationSetUp GetFileUploadURL()
        {
            ApplicationSetUp ApplicationSetUp = _ApplicationSetUp.Table.Where(a => a.Deactivate == "N" && a.key == "FileUploadURL").FirstOrDefault();

            return ApplicationSetUp;
       
        }



      
        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>A user</returns>
        public ACS.Core.Domain.Master.ExecutiveMaster GetUserDetailById(int userId)
        {
            if (_userMasterRepository.Table.Any(i => i.Id == userId))
                return _userMasterRepository.Table.Where(i => i.Id == userId).FirstOrDefault();
            else
                return null;
        }

        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userName, password, ipAddress">User Name, Password, IPAddress</param>
        /// <returns>A user</returns>
        public UserLoginResults ValidateUserLogin(string userName, string password, string ipAddress)
        {
            ACS.Core.Domain.Master.ExecutiveMaster user = null;
            user = GetUserDetailByUserName(userName);

            if (user == null)
                return UserLoginResults.UserNotExist;
            //if (user.DeactTag)
            //    return UserLoginResults.Deleted;
            ////only registered can login
            if (!user.IsRegistered())
                return UserLoginResults.NotRegistered;

            string pwd = "";
           
            //pwd = _encryptionService.CreatePasswordHash(password, contact.PasswordKey, _contactSettings.HashedPasswordFormat);
            pwd = _encryptionService.EncryptText(password,  _execcutiveService.KeyValue("encriptionkey"));

         


         bool isValid = pwd == user.Password;
           // bool isValid = pwd == userPassword;

            //bool isValid = pwd == "086lyBc4qw2HCNnynU7iFw==";

            //save last login date
            if (isValid)
            {
                // Login history//
                // end
                  return UserLoginResults.Successful;
               
            }
            else
                return UserLoginResults.WrongPassword;
        }

        //Added by Prakash on 16/09/20016
        public ACS.Core.Domain.Master.ApplicationSetUp GetPrintPermissions()
        {
            ApplicationSetUp _objAppSetUp = _ApplicationSetUp.Table.Where(i => i.Deactivate == "N" && i.key.ToLower() == "print").SingleOrDefault();
            if (_objAppSetUp != null)
            {
                return _objAppSetUp;
            }
            return null;
        }

        //Added by Ankush on 13/10/20016
        public ACS.Core.Domain.Master.ApplicationSetUp GetGenerateExcelPermissions()
        {
            ApplicationSetUp _objAppSetUp = _ApplicationSetUp.Table.Where(i => i.Deactivate == "N" && i.key.ToLower() == "GenerateExcel").SingleOrDefault();
            if (_objAppSetUp != null)
            {
                return _objAppSetUp;
            }
            return null;
        }

    }
}
