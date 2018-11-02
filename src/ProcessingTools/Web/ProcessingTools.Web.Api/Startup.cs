[assembly: Microsoft.Owin.OwinStartup(typeof(ProcessingTools.Web.Api.Startup))]

namespace ProcessingTools.Web.Api
{
    using System.Web.Http;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(this.GetType().Assembly.FullName);

            this.ConfigureAuth(app);

            var httpConfig = new HttpConfiguration();
            httpConfig.EnableCors();
            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();
        }
    }
}
