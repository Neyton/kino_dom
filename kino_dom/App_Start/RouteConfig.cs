using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace kino_dom
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Index",
                url: "index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About_us",
                url: "about",
                defaults: new { controller = "Home", action = "About_us", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Articles",
                url: "articles",
                defaults: new { controller = "Home", action = "Articles", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Site_Map",
                url: "site_map",
                defaults: new { controller = "Home", action = "Site_Map", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Contact",
                url: "contact_us",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Autor",
                url: "log_in",
                defaults: new { controller = "Home", action = "Autorization", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Registr",
                url: "registration",
                defaults: new { controller = "Home", action = "Registration", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Article",
                url: "articles/{art}",
                defaults: new { controller = "Home", action = "Article", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Movie",
                url: "movie/{id_n}",
                defaults: new { controller = "Home", action = "Item", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Dell",
               url: "dell/{id_n}",
               defaults: new { controller = "Home", action = "Dell", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Add",
               url: "add",
               defaults: new { controller = "Home", action = "Add", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}