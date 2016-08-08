using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AngularMVCAuthentication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
            name: "People",
            url: "People/{*catch-all}",
            defaults: new
            {
                controller = "Home",
                action = "People"
            });


            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "MoviesByReleaseDate"
            //    , url: "movies/released/{year}/{month}"
            //    , defaults: new { controller = "Movies", action = "ByReleaseDate" }
            //    , constraints: new { year = @"2015|2016", month = @"\d{2}" }
            //    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
