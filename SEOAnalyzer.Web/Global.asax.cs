using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SEOAnalyzer.Core.Domain;
using SEOAnalyzer.Web.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SEOAnalyzer.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Initialize AppcConfig class
            AppConfig.Initialize();


            //Logger Configuration
            ILogger logger = new LoggerConfiguration()
                                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + @"logs\log.txt", 
                                                rollingInterval: RollingInterval.Day, 
                                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                                                fileSizeLimitBytes: 3145728)
                                .CreateLogger();

            logger.Information("Application started");
        }

        protected void Application_Error(Object sender, EventArgs e)
        {

            Exception ex = Server.GetLastError();

            if (ex is ThreadAbortException)
                return;

            Log.Error(ex, "Application Level Exception Occurred");

            var error = SEOError.GetError(Core.Domain.Enums.SEOErrorType.InternalException);
            var response = new ISEOResult<SEOError>(error);
            string data = JsonConvert.SerializeObject(response);

            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(data);
            Response.Flush();
            Response.End();
        }
    }
}
