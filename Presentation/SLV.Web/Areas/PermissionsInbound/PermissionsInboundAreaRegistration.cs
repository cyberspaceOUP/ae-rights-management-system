using System.Web.Mvc;

namespace SLV.Web.Areas.PermissionsInbound

{
    public class PermissionsInboundAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PermissionsInbound";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PermissionsInbound",
                "PermissionsInbound/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}