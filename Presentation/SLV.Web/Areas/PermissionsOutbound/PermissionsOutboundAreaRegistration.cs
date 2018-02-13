using System.Web.Mvc;

namespace SLV.Web.Areas.PermissionsOutbound

{
    public class PermissionsOutboundAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PermissionsOutbound";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PermissionsOutbound_default",
                "PermissionsOutbound/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}