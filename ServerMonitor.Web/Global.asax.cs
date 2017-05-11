using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TwitterBootstrapMVC;

namespace ServerMonitor.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                Bootstrap.Configure();
                AreaRegistration.RegisterAllAreas();

                // this returns the instance of the cache repo for use in other filters
                var repo = RepositoryConfig.Register(GlobalConfiguration.Configuration);

                FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
                AuthConfig.RegisterAuth();
            }
            catch (Exception ex)
            {
                //Send to rollbar when we get it implemented
                throw;
            }
        }

    }
}