using System.Web.Mvc;

namespace SLV.Web.Areas.PaymentTagging
{
    public class RightsSellingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PaymentTagging";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PaymentTagging_default",
                "PaymentTagging/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}