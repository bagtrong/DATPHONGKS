using Microsoft.Bot.Builder.Integration.AspNet.WebApi;
using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KStypemig.App_Start;

//

using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.WebApi;
using System.Reflection;
//

//using Microsoft.Bot.Builder.Integration.AspNet.WebApi;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Builder.Dialogs.Adaptive;
//using Microsoft.Bot.Builder.Dialogs.Declarative.Resources;
//using System.Reflection;
//using System.Web.Http;
namespace KStypemig
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //theem moiws


            //
           // GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            // Register Bot Framework Adapter
            builder.RegisterType<BotFrameworkHttpAdapter>().As<IBotFrameworkHttpAdapter>().InstancePerDependency();

            // Register your bot
            builder.RegisterType<KStypemig.Bot.EchoBot>().As<IBot>().InstancePerDependency();

            // Set the dependency resolver to be Autofac
            var config = GlobalConfiguration.Configuration;
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}


 

