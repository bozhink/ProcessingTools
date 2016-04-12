using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProcessingTools.Web.Documents.Startup))]

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