using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace XBBS.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "section", new { controller = "Home", action = "Section", id = UrlParameter.Optional });
            routes.MapRoute(null, "admin", new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional });
            routes.MapRoute(null, "admin/{action}", new { controller = "Admin", action = "Dashboard", id = UrlParameter.Optional });
            routes.MapRoute(null, "settings", new { controller = "Settings", action = "Profile", id = UrlParameter.Optional });
            routes.MapRoute(null, "user", new { controller = "Home", action = "User", id = UrlParameter.Optional });
            routes.MapRoute(null, "topic/{id}/{page}", new { controller = "Forum", action = "Topic", page = 1, id = UrlParameter.Optional });
            routes.MapRoute(null, "node/{id}", new { controller = "Forum", action = "Node", id = UrlParameter.Optional });
            routes.MapRoute(null, "forum/del/{id}/{category}/{uid}", new { controller = "Forum", action = "del", id = UrlParameter.Optional, category = UrlParameter.Optional, uid = UrlParameter.Optional });
            routes.MapRoute(null, "tag/{title}", new { controller = "Forum", action = "Tag", title = UrlParameter.Optional });
            
            routes.MapRoute(name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}