using ACS.Core.Domain.Master;
using ACS.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ACS.Services.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;
        private readonly TimeSpan _expirationTimeSpan;
        private ExecutiveMaster  _cachedUser;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="UserService">User service</param>
        public UserAuthenticationService(
            HttpContextBase httpContext
            ,IUserService userService
           
            )
        {
            this._httpContext = httpContext;
            _userService = userService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        public virtual void SignIn(ExecutiveMaster user, bool createPersistentCookie)
        {
            var now = DateTime.Now.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.Emailid ,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.Emailid,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        public virtual void SignOut()
        {
            //_cachedEmployee = null;
           // _httpContext.Response.Cookies.Remove("ACS.CallCenter");
            _httpContext.Response.Cookies.Remove("ACSSLV.AUTH");
            FormsAuthentication.SignOut();
        }

        public virtual ExecutiveMaster GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }


            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedContactFromTicket(formsIdentity.Ticket);
            //if (user != null && !user.DeactTag && user.IsRegistered())
                _cachedUser = user;
            return _cachedUser;
        }

        public virtual ExecutiveMaster GetAuthenticatedContactFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var usernameOrEmail = ticket.UserData;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var User = _userService.GetUserDetailByUserName(usernameOrEmail);
            return User;
        }
    }
}
