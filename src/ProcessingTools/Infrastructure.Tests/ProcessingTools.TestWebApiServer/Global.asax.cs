namespace ProcessingTools.TestWebApiServer
{
    using System.Web;
    using System.Web.Http;

#pragma warning disable SA1649 // File name must match first type name
    public class WebApiApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
