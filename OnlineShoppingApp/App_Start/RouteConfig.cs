using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShoppingApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "OrderDetail",
                url: "Orders/Details/{orderId}",
                defaults: new { controller = "Orders", action = "Details" }
                );

            routes.MapRoute(
                "SearchItems",
                url: "Items/Search/{searchtext}/{categoryId}",
                defaults: new { controller = "Items", action = "Search" }
                );

            routes.MapRoute(
                "ItemsByCategory",
                url: "Items/Filtered/{categoryId}",
                defaults: new { controller = "Items", action = "Filtered"} 
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
