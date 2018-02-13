using ACS.Core;
using ACS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ACS.Web.Framework.Security.Filters
{
    public class AllowUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
           // var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string host = webHelper.GetHost(false);
            string url = webHelper.GetThisPageUrl(true).Replace(host, "/");
            
            //if (workContext.CurrentUser == null)
            //    filterContext.Result = new RedirectResult("../../login/login?ReturnUrl="+url+"");
            //else
            //    return;
        }
    }
}
