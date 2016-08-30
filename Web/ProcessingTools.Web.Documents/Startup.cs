[assembly: Microsoft.Owin.OwinStartup(typeof(ProcessingTools.Web.Documents.Startup))]

namespace ProcessingTools.Web.Documents
{
    using System.Reflection;
    using System.Web.Http;

    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            this.ConfigureAuth(app);

            var httpConfig = new HttpConfiguration();
            httpConfig.EnableCors();
            WebApiConfig.Register(httpConfig);
            httpConfig.EnsureInitialized();

            app
                .UseWebApi(httpConfig)
                .UseNinjectMiddleware(NinjectWebCommon.CreateKernel)
                .UseNinjectWebApi(httpConfig);
        }
    }
}
