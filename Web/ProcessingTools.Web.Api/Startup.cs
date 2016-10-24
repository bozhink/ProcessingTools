using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProcessingTools.Web.Api.Startup))]

namespace ProcessingTools.Web.Api
{
    using System.Web.Http;

    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

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

            app
                .UseNinjectMiddleware(NinjectConfig.CreateKernel)
                .UseNinjectWebApi(httpConfig);
        }
    }
}