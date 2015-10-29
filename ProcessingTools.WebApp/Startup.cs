using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProcessingTools.WebApp.Startup))]

namespace ProcessingTools.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
