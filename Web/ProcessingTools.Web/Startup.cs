[assembly: Microsoft.Owin.OwinStartup(typeof(ProcessingTools.Web.Startup))]

namespace ProcessingTools.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(this.GetType().Assembly);

            this.ConfigureAuth(app);
        }
    }
}
