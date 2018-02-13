using ACS.Core.Domain.Master ;
using ACS.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.User
{
    public interface IUserService
    {
        /// <summary>
        /// Gets a user by GUID
        /// </summary>
        /// <param name="userGuid">User GUID</param>
        /// <returns>A user</returns>
      //  ACS.Core.Domain.Master.ExecutiveMaster  GetUserByGuid(System.Guid userGuid);

        /// <summary>
        /// Gets a user by userName
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>A user</returns>
        ACS.Core.Domain.Master.ExecutiveMaster GetUserDetailByUserName(string userName);

        ACS.Core.Domain.Master.ApplicationSetUp GetFileUploadURL();


        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>A user</returns>
        //ACS.Core.Domain.Master.ExecutiveMaster GetUserDetailById(int userId);

        /// <summary>
        /// Gets a user by userId
        /// </summary>
        /// <param name="userName, password, ipAddress">User Name, Password, IPAddress</param>
        /// <returns>A user</returns>
        UserLoginResults ValidateUserLogin(string userName, string password, string ipAddress);

        /// <summary>
        /// Gets ApplicationSetUp for Print
        /// </summary>
        /// <param ></param>
        /// <returns>A user</returns>
        ACS.Core.Domain.Master.ApplicationSetUp GetPrintPermissions();


        /// <summary>
        /// Gets ApplicationSetUp for GenerateExcel By Ankush 
        /// </summary>
        /// <param ></param>
        /// <returns>A user</returns>
        ACS.Core.Domain.Master.ApplicationSetUp GetGenerateExcelPermissions();

    }
}
