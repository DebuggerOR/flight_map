using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ajax_Minimal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "First", action = "Index" }
            );

            routes.MapRoute(
                name: "Display",
                url: "display/{ip}/{port}",
                defaults: new { controller = "First", action = "Display" }
            );

            routes.MapRoute(
                name: "DisplayAnime",
                url: "display/{file}/{rate}",
                defaults: new { controller = "First", action = "DisplayAnime" }
            );

            routes.MapRoute(
                name: "DisplayRate",
                url: "display/{ip}/{port}/{rate}",
                defaults: new { controller = "First", action = "DisplayRate" }
            );

            routes.MapRoute(
                name: "Save",
                url: "save/{ip}/{port}/{rate}/{time}/{file}",
                defaults: new { controller = "First", action = "Save" }
            );
        }
    }
}
