using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Authentication
{
    public partial interface IUserAuthenticationService
    {
        void SignIn(ACS.Core.Domain.Master.ExecutiveMaster  user, bool createPersistentCookie);
        void SignOut();
        ACS.Core.Domain.Master.ExecutiveMaster GetAuthenticatedUser();
    }
}
