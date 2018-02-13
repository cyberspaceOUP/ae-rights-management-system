using System.Web.Mvc;
using System.Web.Routing;
using ACS.Core.Infrastructure;
using ACS.Web.Framework.Controllers;
using ACS.Core;
using ACS.Services.Authentication;
using ACS.Core.Domain.Master;

namespace SLV.Web.Controllers
{
    public class BasePublicController : BaseController
    {
       

        protected virtual ActionResult InvokeHttp404()
        {
            // Call target Controller and pass the routeData.
            IController errorController = EngineContext.Current.Resolve<SLV.Web.Controllers.CommonController>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }


    }
}