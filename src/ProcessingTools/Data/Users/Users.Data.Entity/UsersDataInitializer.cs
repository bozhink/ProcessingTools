namespace ProcessingTools.Users.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Abstractions;
    using ProcessingTools.Users.Data.Entity.Contracts;
    using ProcessingTools.Users.Data.Entity.Migrations;

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
