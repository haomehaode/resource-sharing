using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZZULIWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "CourseList",
                url: "{controller}/{action}/nav_{nav0}/nav_{nav1}/page_{param}",
                defaults: new { },
                constraints: new { }
                );
            routes.MapRoute(
                name: "Page",
                url: "{controller}/{action}/page_{pageIndex}",
                defaults: new { },
                constraints: new { }
                );
            routes.MapRoute(
                name: "Page_ID",
                url: "{controller}/{action}/{id}/page_{pageIndex}",
                defaults: new { },
                constraints: new { }
                );
            //routes.MapRoute(
            //    name: "Video",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { },
            //    constraints: new { }
            //    );
            routes.MapRoute(
                name: "AjaxTab",
                url: "{controller}/{action}/{id}/tab_{page}",
                defaults: new { }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
