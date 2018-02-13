using System;
using System.Linq;
using System.Web;
using ACS.Core;
using ACS.Core.Domain.Localization;
using ACS.Core.Fakes;
using ACS.Core.Domain.Contact;
using ACS.Services.Common;
using ACS.Services.Authentication;
using ACS.Core.Domain.User;
using ACS.Core.Domain.Master;


namespace ACS.Web.Framework
{
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string CustomerCookieName = "ACS.customer";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        //private readonly IGenericAttributeService _genericAttributeService;
        private readonly LocalizationSettings _localizationSettings;
        //private readonly ACS.Services.Authentication.IAuthenticationService _authenticationService;
        private readonly ACS.Services.Authentication.IUserAuthenticationService _userAuthenticationService;


        //private Language _cachedLanguage;
        private ExecutiveMaster _cachedUser;

        #endregion

        #region Ctor

        public WebWorkContext(HttpContextBase httpContext,
            //IGenericAttributeService genericAttributeService,
            //ACS.Services.Authentication.IAuthenticationService authenticationService,
            LocalizationSettings localizationSettings
            , ACS.Services.Authentication.IUserAuthenticationService userAuthenticationService

            )
        {
            this._httpContext = httpContext;
            //this._genericAttributeService = genericAttributeService;
            this._localizationSettings = localizationSettings;
            //this._authenticationService = authenticationService;
            this._userAuthenticationService = userAuthenticationService;
        }

        #endregion

        #region Properties

        //public virtual Contact CurrentContact
        //{
        //    get
        //    {
        //        if (_cachedCustomer != null)
        //            return _cachedCustomer;

        //        Contact customer = null;

        //        customer = _authenticationService.GetAuthenticatedContact();

        //        //load guest customer
        //        if (customer == null || customer.DeactTag)  // || !customer.Active)
        //        {
        //            customer = _authenticationService.getRememberMeContact();
        //            //var customerCookie = GetCustomerCookie();
        //            //if (customerCookie != null && !String.IsNullOrEmpty(customerCookie.Value))
        //            //{
        //            //    Guid customerGuid;
        //            //    if (Guid.TryParse(customerCookie.Value, out customerGuid))
        //            //    {
        //            //        var customerByCookie = _contactService.GetCustomerByGuid(customerGuid);
        //            //        if (customerByCookie != null &&
        //            //            //this customer (from cookie) and registered
        //            //            customerByCookie.IsRegistered())
        //            //            customer = customerByCookie;
        //            //    }
        //            //}
        //        }


        //        //validation
        //        if (customer != null && !customer.DeactTag) // && customer.Active)
        //        {
        //            //SetCustomerCookie(customer.ContactGuid);
        //            _cachedCustomer = customer;
        //        }

        //        return _cachedCustomer;
        //    }
        //    set
        //    {
        //        //SetCustomerCookie(value.ContactGuid);
        //        _cachedCustomer = value;
        //    }
        //}

        /// <summary>
        /// Get or set current Logged in User belongs to the UserMaster Table
        /// </summary>
        public virtual ExecutiveMaster CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                ExecutiveMaster user = null;

                user = _userAuthenticationService.GetAuthenticatedUser();

                //load guest customer
                if (user == null || user.Deactivate =="Y")  // || !customer.Active)
                {
                    //var employeeCookie = GetEmployeeCookie();
                    //if (employeeCookie != null && !String.IsNullOrEmpty(employeeCookie.Value))
                    //{
                    //    Guid employeeGuid;
                    //    if (Guid.TryParse(employeeCookie.Value, out employeeGuid))
                    //    {
                    //        var employeeByCookie = _employeeService.GetEmployeeByGuid(employeeGuid);
                    //        if (employeeByCookie != null &&
                    //            //this customer (from cookie) and registered
                    //            employeeByCookie.IsRegistered())
                    //            employee = employeeByCookie;
                    //    }
                    //}
                }


                //validation
                if (user!= null && user.Deactivate =="N") // && customer.Active)
                {
                    //SetEmployeeCookie(employee.EmployeeGuid);
                    _cachedUser = user;
                }

                return _cachedUser;
            }
            set
            {
                //SetEmployeeCookie(value.EmployeeGuid);
                _cachedUser = value;
            }
        }

        ///// <summary>
        ///// Get or set current Logged in User belongs to the UserMaster Table
        ///// </summary>
        //public virtual Flat CurrentFlat
        //{
        //    get
        //    {
        //        if (_cachedFlat != null)
        //            return _cachedFlat;

        //        Flat flat = null;

        //        var _flatCookie = _httpContext.Request.Cookies.Get("ACSSLV.Flat");
        //        if (CurrentContact!=null)
        //        {
        //            if (_flatCookie == null)
        //            {
        //                flat = CurrentContact.FlatContacts.FirstOrDefault().Flat;
        //                var cookie = new HttpCookie("ACSSLV.Flat", flat.Id.ToString());//
        //                cookie.HttpOnly = true;
        //                cookie.Expires = DateTime.Now.AddMinutes(30);
        //                //cookie.Secure = true;
        //                cookie.Path = "/";
        //                _httpContext.Response.Cookies.Add(cookie);

        //            }
        //            else
        //            {
        //                flat = CurrentContact.FlatContacts.Where(c => c.FlatId == Convert.ToInt32(_flatCookie.Value)).FirstOrDefault().Flat;
        //            } 
        //        }

        //        //validation
        //        if (flat != null)
        //        {
        //            _cachedFlat = flat;
        //        }

        //        return _cachedFlat;
        //    }
        //    set
        //    {
        //        _cachedFlat = value;
        //        var cookie = new HttpCookie("ACSSLV.Flat", _cachedFlat.Id.ToString());//
        //        cookie.HttpOnly = true;
        //        cookie.Expires = DateTime.Now.AddMinutes(30);
        //        //cookie.Secure = true;
        //        cookie.Path = "/";
        //        _httpContext.Response.Cookies.Add(cookie);
        //    }
        //}

        ///// <summary>
        ///// Get or set current user working language
        ///// </summary>
        //public virtual Language WorkingLanguage
        //{
        //    get
        //    {
        //        if (_cachedLanguage != null)
        //            return _cachedLanguage;
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        var languageId = value != null ? value.Id : 2;
        //        //_genericAttributeService.SaveAttribute(null, languageId, null);
        //        if (value.Id == 2)
        //            _cachedLanguage = value;
        //        else
        //            //reset cache
        //            _cachedLanguage = null;
        //    }
        //}

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion

    }
}
