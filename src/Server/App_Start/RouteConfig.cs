using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Server
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Identity",
               url: "start.aspx",
               defaults: new { controller = "Identity", action = "StartIdentity", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "Check",
            url: "check.aspx",
            defaults: new { controller = "Identity", action = "Check", id = UrlParameter.Optional }
        );

            routes.MapRoute(
              name: "Login",
              url: "login.aspx",
              defaults: new { controller = "Identity", action = "Login", id = UrlParameter.Optional }
          );
        }
    }
}