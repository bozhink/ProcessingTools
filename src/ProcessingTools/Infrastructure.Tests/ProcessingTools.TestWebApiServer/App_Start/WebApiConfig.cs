namespace ProcessingTools.TestWebApiServer
{
    using System.Web.Http;

    /// <summary>
    /// WebApi configuration.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register WebApi routes.
        /// </summary>
        /// <param name="config"><see cref="HttpConfiguration"/> to be updated.</param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
