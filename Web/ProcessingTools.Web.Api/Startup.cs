using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProcessingTools.Web.Api.Startup))]

namespace ProcessingTools.Web.Api
{
    using System.Web.Http;

    using Common.Constants;

    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assemblies.WebApi);

            this.ConfigureAuth(app);

            var httpConfig = new HttpConfiguration();
            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();

            app
                .UseNinjectMiddleware(NinjectConfig.CreateKernel)
                .UseNinjectWebApi(httpConfig);
        }
    }
}