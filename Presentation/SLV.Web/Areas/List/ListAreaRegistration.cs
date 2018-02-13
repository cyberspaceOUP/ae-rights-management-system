using System.Web.Mvc;

namespace SLV.Web.Areas.List
{
    public class ListAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "List";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "List_default",
                "List/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}