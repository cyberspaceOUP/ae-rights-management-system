using ACS.Core;
using ACS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
//using ACS.Services.Contact;

namespace ACS.Web.Framework.Security.Filters
{
    public class AuthenticateUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string host = webHelper.GetHost(false);
            string url = webHelper.GetThisPageUrl(true).Replace(host, "/");
            var routeData = getURLActionController(webHelper.GetThisPageUrl(true)).Values;
            var controllerName = routeData["controller"];
            var actionName = routeData["action"];

            //If NO currenct User
            //if (workContext.CurrentContact == null && workContext.CurrentUser == null)
            //{
            //    filterContext.Result = new RedirectResult("/?ReturnUrl=" + url + "");
            //}
            //else if (workContext.CurrentContact != null)    //If Contact (Resident/ Owner)
            //{
            //    if (workContext.CurrentContact.IsRegistered())
            //    {
            //        //Checking if controller/ action called is allowed for the contact
            //        if (workContext.CurrentContact.FlatContacts.FirstOrDefault().UserProfile.ApplicationActivities.Where(aa => aa.Action != null).Any(aa => aa.Controller.ToLower() == controllerName.ToString().ToLower() && aa.Action.ToLower() == actionName.ToString().ToLower()))
            //            return;
            //        else
            //            filterContext.Result = new RedirectResult("/");
            //    }
            //    else
            //        filterContext.Result = new RedirectResult(host + "/resident/resident/manageprofile?value=validate");// check with rahul sir to open password set page or in tab
            //}
            //else if (workContext.CurrentUser != null)   //Non Contact (Guard/ FMC Admin/ etc)
            //{
            //    //Checking if controller/ action called is allowed for the user
            //    if (workContext.CurrentUser.UserProfile.ApplicationActivities.Where(aa => aa.Action != null).Any(aa => aa.Controller.ToLower() == controllerName.ToString().ToLower() && aa.Action.ToLower() == actionName.ToString().ToLower()))
            //        return;
            //    else
            //        filterContext.Result = new RedirectResult("/");
            //}
        }

        //Function to break URL into Controller and Action
        private System.Web.Routing.RouteData getURLActionController(string fullUrl)
        {
            // Split the url to url + query string
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            // Arranges
            var request = new System.Web.HttpRequest(null, url, queryString);
            var response = new System.Web.HttpResponse(new System.IO.StringWriter());
            var httpContext = new System.Web.HttpContext(request, response);

            return System.Web.Routing.RouteTable.Routes.GetRouteData(new System.Web.HttpContextWrapper(httpContext));
        }
    }
}
