using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using ACS.Core;
using ACS.Core.Infrastructure;
using ACS.Services.Common;
using ACS.Services.Logging;
using System.Web;

namespace ACS.Web.Framework.Controllers
{
    public abstract class BaseController : Controller
    {

        
            //protected override void OnActionExecuting(ActionExecutingContext context)
            //{
            //    base.OnActionExecuting(context);
            //    // your code here
            //    Response.Redirect("https://worklog.vrvirtual.com/General/pgMainPage.aspx?defpage=..%2fTransaction%2fComplaint%2fpgTCompliaintList.aspx%3fFor%3dO");
            //}
        
        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    // Split the url to url + query string
        //    var fullUrl = Request.Url.ToString();
        //    var questionMarkIndex = fullUrl.IndexOf('?');
        //    string queryString = null;
        //    string url = fullUrl;
        //    if (questionMarkIndex != -1) // There is a QueryString
        //    {    
        //        url = fullUrl.Substring(0, questionMarkIndex); 
        //        queryString = fullUrl.Substring(questionMarkIndex + 1);
        //    }   

        //    // Arranges
        //    var request = new HttpRequest(null, url, queryString);
        //    var response = new HttpResponse(new StringWriter());
        //    var httpContext = new HttpContext(request, response);

        //    var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            

        //    // Extract the data    
        //    var values = routeData.Values;
        //    var controllerName = values["controller"];
        //    var actionName = values["action"];
        //    var areaName = values["area"];
        //}
    }
}
