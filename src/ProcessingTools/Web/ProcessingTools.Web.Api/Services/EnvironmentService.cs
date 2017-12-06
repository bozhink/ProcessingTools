namespace ProcessingTools.Web.Api.Services
{
    using System;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Contracts.Models;

    public class EnvironmentService : IEnvironment
    {
        public IEnvironmentUser User => new EnvironmentUser();

        public Func<Guid> GuidProvider => () => Guid.NewGuid();

        public Func<DateTime> DateTimeProvider => () => DateTime.UtcNow;

        private class EnvironmentUser : IEnvironmentUser
        {
            public string UserName => "system";

            public string Email => "system@localhost";

            public UserRole Role => UserRole.Administrator;

            public string Id => "99E084F3-28D7-4221-B9E9-63CC41D3533B";
        }
    }
}
