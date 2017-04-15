namespace ProcessingTools.Web.Services
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Enumerations;

    public class EnvironmentService : IEnvironment
    {
        public IEnvironmentUser User => new EnvironmentUser();

        public IDateTimeProvider DateTime => new DateTimeProvider();

        private class DateTimeProvider : IDateTimeProvider
        {
            public System.DateTime Now => System.DateTime.UtcNow;
        }

        private class EnvironmentUser : IEnvironmentUser
        {
            public string UserName => "system";

            public string Email => "system@system.system";

            public UserRole Role => UserRole.Administrator;

            public string Id => "system";
        }
    }
}
