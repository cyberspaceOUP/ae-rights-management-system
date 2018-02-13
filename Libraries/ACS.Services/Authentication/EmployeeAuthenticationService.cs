//using ACS.Services.Organization;
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
    public partial class EmployeeAuthenticationService : IEmployeeAuthenticationService
    {
        private readonly HttpContextBase _httpContext;
        //private readonly IEmployeeService _employeeService;
        private readonly TimeSpan _expirationTimeSpan;
        //private ACS.Core.Domain.Organization.Employee _cachedEmployee;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="EmployeeService">Employee service</param>
        public EmployeeAuthenticationService(
            HttpContextBase httpContext
            //,IEmployeeService employeeService
            )
        {
            this._httpContext = httpContext;
            //this._employeeService = employeeService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        //public virtual void SignIn(ACS.Core.Domain.Organization.Employee employee, bool createPersistentCookie)
        //{
        //    var now = DateTime.Now.ToLocalTime();

        //    var ticket = new FormsAuthenticationTicket(
        //        1 /*version*/,
        //        employee.EmailID,
        //        now,
        //        now.Add(_expirationTimeSpan),
        //        createPersistentCookie,
        //        employee.EmailID,
        //        FormsAuthentication.FormsCookiePath);

        //    var encryptedTicket = FormsAuthentication.Encrypt(ticket);

        //    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //    cookie.HttpOnly = true;
        //    if (ticket.IsPersistent)
        //    {
        //        cookie.Expires = ticket.Expiration;
        //    }
        //    cookie.Secure = FormsAuthentication.RequireSSL;
        //    cookie.Path = FormsAuthentication.FormsCookiePath;
        //    if (FormsAuthentication.CookieDomain != null)
        //    {
        //        cookie.Domain = FormsAuthentication.CookieDomain;
        //    }

        //    _httpContext.Response.Cookies.Add(cookie);
        //    _cachedEmployee = employee;
        //}

        //public virtual void SignOut()
        //{
        //    //_cachedEmployee = null;
        //    _httpContext.Response.Cookies.Remove("ACS.CallCenter");
        //    FormsAuthentication.SignOut();
        //}

        //public virtual ACS.Core.Domain.Organization.Employee GetAuthenticatedEmployee()
        //{
        //    if (_cachedEmployee != null)
        //        return _cachedEmployee;

        //    if (_httpContext == null ||
        //        _httpContext.Request == null ||
        //        !_httpContext.Request.IsAuthenticated ||
        //        !(_httpContext.User.Identity is FormsIdentity))
        //    {
        //        return null;
        //    }


        //    var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
        //    var employee = GetAuthenticatedContactFromTicket(formsIdentity.Ticket);
        //    if (employee != null && !employee.DeactTag && employee.IsRegistered())
        //        _cachedEmployee = employee;
        //    return _cachedEmployee;
        //}

        //public virtual ACS.Core.Domain.Organization.Employee GetAuthenticatedContactFromTicket(FormsAuthenticationTicket ticket)
        //{
        //    if (ticket == null)
        //        throw new ArgumentNullException("ticket");

        //    var usernameOrEmail = ticket.UserData;

        //    if (String.IsNullOrWhiteSpace(usernameOrEmail))
        //        return null;
        //    var Employee = _employeeService.GetEmployeeDetailByEmailID(usernameOrEmail);
        //    return Employee;
        //}
    }
}
