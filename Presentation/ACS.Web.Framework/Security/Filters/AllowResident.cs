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
    public class AllowResident : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
  //          var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string host = webHelper.GetHost(false);
            string url = webHelper.GetThisPageUrl(true).Replace(host, "/");
            if (url == "/resident/resident/manageprofile?value=validate")// CHECK TO SET FIRST TIME PASSWORD SET
                return ;

            //if (workContext.CurrentContact == null)
            //    filterContext.Result = new RedirectResult("/?ReturnUrl=" + url + "");
            //else
            //{
            //    if(workContext.CurrentContact.IsRegistered())
            //        return;
            //    else
            //        filterContext.Result = new RedirectResult(host + "/resident/resident/manageprofile?value=validate");// check with rahul sir to open password set page or in tab
            //}
                
        }
    }
}
