namespace ProcessingTools.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Constants;
    using Controllers;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: RouteNames.MvcDefault,
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = HomeController.ControllerName,
                    action = HomeController.IndexActionName,
                    id = UrlParameter.Optional
                });
        }
    }
}
