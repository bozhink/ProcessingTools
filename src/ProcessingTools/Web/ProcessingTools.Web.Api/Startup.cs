[assembly: Microsoft.Owin.OwinStartup(typeof(ProcessingTools.Web.Api.Startup))]

namespace ProcessingTools.Web.Api
{
    using System.Web.Http;
    using Owin;
    using ProcessingTools.Constants;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assemblies.WebApi);

            this.ConfigureAuth(app);

            var httpConfig = new HttpConfiguration();
            httpConfig.EnableCors();
            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();
        }
    }
}
