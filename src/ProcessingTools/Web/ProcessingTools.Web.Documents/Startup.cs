using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProcessingTools.Web.Documents.Startup))]

namespace ProcessingTools.Web.Documents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
