using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace KStypemig
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //
          //  Register API route for BotController

           //routes.MapHttpRoute(
           //    name: "BotApi",
           //    routeTemplate: "api/messages",
           //    defaults: new { controller = "Bot" }
           //);

        }
    }
}
