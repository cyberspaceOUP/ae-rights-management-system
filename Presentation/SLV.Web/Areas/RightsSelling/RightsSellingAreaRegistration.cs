using System.Web.Mvc;

namespace SLV.Web.Areas.RightsSelling
{
    public class RightsSellingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RightsSelling";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RightsSelling_default",
                "RightsSelling/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}