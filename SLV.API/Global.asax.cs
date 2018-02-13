using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;



using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Infrastructure;
using ACS.Core.Infrastructure.DependencyRegister;
using ACS.Services.Logging;
using ACS.Services.Tasks;
using ACS.Web.Framework;

using System.Web.Mvc;
using ACS.Web.Framework.Mvc;

using Autofac;
using Autofac.Integration.WebApi;

//using ACS.Services.Directory;
using System.Reflection;
using Newtonsoft.Json;
using System.Net.Http.Formatting;


namespace SLV.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.MapHttpRoute(
                name: "DefaultApi2",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "post", id = RouteParameter.Optional }
                );

            routes.MapHttpRoute(
                name: "DefaultApi3",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "PostDataToAPI", id = RouteParameter.Optional }
                );

            //   Route  GET method

            routes.MapHttpRoute(
               name: "DefaultApi1",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { action = "get", id = RouteParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌ferenceLoopHandling = ReferenceLoopHandling.Ignore;

            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);

            //initialize engine context
            EngineContext.Initialize(false);

            //model binders
            ModelBinders.Binders.Add(typeof(BaseNopModel), new NopModelBinder());

            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();


            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new NopMetadataProvider();

            RegisterRoutes(RouteTable.Routes);



            //var builder = new ContainerBuilder();

            //// Register your Web API controllers.
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<GeographyService>().As<IGeographyService>().InstancePerLifetimeScope();
            //builder.RegisterType<Geographies>().As<IGeographies>().InstancePerLifetimeScope();

            //// OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            //// Set the dependency resolver to be Autofac.
            //var container = builder.Build();
            ////config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //// Create the depenedency resolver.
            //var resolver = new AutofacWebApiDependencyResolver(container);

            //// Configure Web API with the dependency resolver.
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;



            //GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            //name: "DefaultApi2",
            //routeTemplate: "api/{controller}/{id}",
            //defaults: new {  id = RouteParameter.Optional }
            //);
        }
    }
}

