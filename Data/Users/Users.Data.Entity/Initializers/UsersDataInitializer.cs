namespace ProcessingTools.Users.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class UsersDataInitializer : GenericDbContextInitializer<UsersDbContext>, IUsersDataInitializer
    {
        public UsersDataInitializer(IUsersDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UsersDbContext, Configuration>());
        }
    }
}
