namespace ProcessingTools.Web.Services
{
    using System;
    using System.Security.Claims;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;

    public class EnvironmentService : IEnvironment
    {
        private readonly IEnvironmentUser user = new EnvironmentUser();

        public IEnvironmentUser User => this.user;

        public Func<Guid> GuidProvider => () => Guid.NewGuid();

        public Func<DateTime> DateTimeProvider => () => DateTime.UtcNow;

        private class EnvironmentUser : IEnvironmentUser
        {
            private readonly string id;
            private readonly string name;
            private readonly string email;

            public EnvironmentUser()
            {
                if (HttpContext.Current.User?.Identity is ClaimsIdentity claimsIdentity)
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