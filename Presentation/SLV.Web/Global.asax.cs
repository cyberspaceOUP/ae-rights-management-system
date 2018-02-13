using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using FluentValidation.Mvc;

using ACS.Core;
using ACS.Core.Data;
using ACS.Core.Infrastructure;
using ACS.Core.Infrastructure.DependencyRegister;
using ACS.Services.Logging;
using ACS.Services.Tasks;
using ACS.Web.Framework;
using ACS.Web.Framework.Mvc;
using ACS.Web.Framework.Mvc.Routes;
//using ACS.Web.Framework.Themes;
using StackExchange.Profiling;
using ACS.Core.Domain.Common;

namespace SLV.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Login", action = "Login", id = UrlParameter.Optional },
                new[] { "SLV.Web.Controllers" }
            );
        }

        protected void Application_Start()
            {            
            //initialize engine context
            EngineContext.Initialize(false);

            //model binders
            ModelBinders.Binders.Add(typeof(BaseNopModel), new NopModelBinder());

            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (databaseInstalled)
            {
                //remove all view engines
                //ViewEngines.Engines.Clear();
                //except the themeable razor view engine we use
                //ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }

            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new NopMetadataProvider();

            //Registering some regular mvc stuff
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            
            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new ACSValidatorFactory()));

            //start scheduled tasks
            if (databaseInstalled)
            {
                // SACHIN SAXENA 17.02.2016
                 
                //TaskManager.Instance.Initialize();
                //TaskManager.Instance.Start();
               
            }

            //log application start
            if (databaseInstalled)
            {
                try
                {
                    //log
                    var logger = EngineContext.Current.Resolve<ILogger>();
                  //  logger.Information("Application started", null);
                }
                catch (Exception)
                {
                    //don't throw new exception if occurs
                }
            }

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;
            //Security setup
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
            }
        }

        //protected void Application_Error(Object sender, EventArgs e)
        //{

        //    var _workContext = EngineContext.Current.Resolve<IWorkContext>();

        //    var exception = Server.GetLastError();
        //    //log error
        //    LogException(exception);

        //    //process 404 HTTP errors
        //    var httpException = exception as HttpException;
        //    if (httpException != null && httpException.GetHttpCode() == 404)
        //    {
        //        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
        //        if (!webHelper.IsStaticResource(this.Request))
        //        {
        //            Response.Clear();
        //            Server.ClearError();
        //            Response.TrySkipIisCustomErrors = true;

        //            // Call target Controller and pass the routeData.
        //            IController errorController = EngineContext.Current.Resolve<SLV.Web.Controllers.CommonController>();

        //            var routeData = new RouteData();
        //            routeData.Values.Add("controller", "Common");
        //            routeData.Values.Add("action", "PageNotFound");

        //            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //        }
        //    }
        //    else
        //    {
        //        Response.Clear();
        //        Server.ClearError();
        //        Response.TrySkipIisCustomErrors = true;

        //        // Call target Controller and pass the routeData.
        //        IController errorController = EngineContext.Current.Resolve<SLV.Web.Controllers.CommonController>();

        //        var routeData = new RouteData();
        //        routeData.Values.Add("controller", "Common");
        //        routeData.Values.Add("action", "GenericError");

        //        errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        //    }
        //}

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            //ignore 404 HTTP errors
            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404 &&
                !EngineContext.Current.Resolve<CommonSettings>().Log404Errors)
                return;

            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                //var workContext = EngineContext.Current.Resolve<IWorkContext>();
                //logger.Error(exc.Message, exc, workContext.CurrentContact); //, workContext.CurrentCustomer
            }
            catch (Exception ex)
            {
                //don't throw new exception if occurs
            }
        }
    }
}
