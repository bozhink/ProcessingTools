using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Host;

[assembly: OwinStartup(typeof(ProcessingTools.TestWebApiServer.Startup))]

namespace ProcessingTools.TestWebApiServer
{
    using System.Web.Http;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();
            WebApiConfig.Register(configuration);

            configuration.EnsureInitialized();

            app.UseWebApi(configuration);
        }
    }
}
