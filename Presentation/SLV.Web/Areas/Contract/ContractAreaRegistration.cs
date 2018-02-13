using System.Web.Mvc;

namespace SLV.Web.Areas.Contract
{
    public class ContractAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Contract";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Contract_default",
                "Contract/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}