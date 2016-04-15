using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProcessingTools.Web.Documents.Startup))]

namespace ProcessingTools.Web.Documents
{
    using System.Reflection;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            this.ConfigureAuth(app);
        }
    }
}