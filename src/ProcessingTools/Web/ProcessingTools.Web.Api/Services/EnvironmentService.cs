namespace ProcessingTools.Web.Api.Services
{
    using System;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;

    public class EnvironmentService : IEnvironment
    {
        public IEnvironmentUser User => new EnvironmentUser();

        public IDateTimeProvider DateTime => new DateTimeProvider();

        private class EnvironmentUser : IEnvironmentUser
        {
            public string UserName => "system";

            public string Email => "system@localhost";

            public UserRole Role => UserRole.Administrator;

            public string Id => "99E084F3-28D7-4221-B9E9-63CC41D3533B";
        }

        private class DateTimeProvider : IDateTimeProvider
        {
            public DateTime Now => System.DateTime.UtcNow;
        }
    }
}
