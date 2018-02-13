using System.Web.Routing;

namespace ACS.Web.Framework.Mvc.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}
