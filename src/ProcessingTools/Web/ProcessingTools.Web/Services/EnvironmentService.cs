namespace ProcessingTools.Web.Services
{
    using System.Security.Claims;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;

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
            private readonly string name;
            private readonly string email;

            public EnvironmentUser()
            {
                var claimsIdentity = HttpContext.Current.User?.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    this.id = claimsIdentity.FindFirstValue(ClaimTypes.NameIdentifier);
                    this.name = claimsIdentity.FindFirstValue(ClaimTypes.Name);
                    this.email = claimsIdentity.FindFirstValue(ClaimTypes.Email);
                }
            }

            public string UserName => this.name;

            // TODO
            public UserRole Role => UserRole.Administrator;

            public string Id => this.id;

            public string Email => this.email;
        }
    }
}