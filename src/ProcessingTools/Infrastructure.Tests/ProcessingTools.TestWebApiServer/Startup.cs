[assembly: Microsoft.Owin.OwinStartup(typeof(ProcessingTools.TestWebApiServer.Startup))]

namespace ProcessingTools.TestWebApiServer
{
    using System.Web.Http;
    using Owin;

    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configure the application.
        /// </summary>
        /// <param name="app"><see cref="IAppBuilder"/> to be configured.</param>
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);

            configuration.EnsureInitialized();

            app.UseWebApi(configuration);
        }
    }
}
