using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProcessingTools.Web.Startup))]
namespace ProcessingTools.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
