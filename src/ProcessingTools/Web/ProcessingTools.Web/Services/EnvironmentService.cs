namespace ProcessingTools.Web.Services
{
    using System.Web;
    using System.Web.Security;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Web.Managers;
    using Microsoft.AspNet.Identity.Owin;

    public class EnvironmentService : IEnvironment
    {
        private readonly IEnvironmentUser user = new EnvironmentUser();
        private readonly IDateTimeProvider datetimeProvider = new DateTimeProvider();

        public IEnvironmentUser User => this.user;

        public IDateTimeProvider DateTime => this.datetimeProvider;

        private class DateTimeProvider : IDateTimeProvider
        {
            public System.DateTime Now => System.DateTime.UtcNow;
        }

        private class EnvironmentUser : IEnvironmentUser
        {
            private readonly string id;
            private readonly ProcessingTools.Users.Data.Entity.Models.User user =  null;

            public EnvironmentUser()
            {
                this.id = HttpContext.Current.User?.Identity?.GetUserId() ?? null;

                if (HttpContext.Current.User != null)
                {
                    try
                    {
                        var userManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
                        this.user = userManager.UserManager.FindByName(HttpContext.Current.User.Identity.Name);
                    }
                    catch
                    {
                        this.user = null;
                    }
                }
            }

            public string UserName => this.user?.UserName ?? null;

            public string Email => this.user?.Email ?? null;

            // TODO
            public UserRole Role => UserRole.Administrator;

            public string Id => this.user?.Id;
        }
    }
}
