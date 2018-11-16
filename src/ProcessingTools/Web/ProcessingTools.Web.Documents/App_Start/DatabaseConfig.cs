namespace ProcessingTools.Web.Documents
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    public static class DatabaseConfig
    {
        public static async Task Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Users.Data.Entity.UsersDbContext, ProcessingTools.Users.Data.Entity.Migrations.Configuration>());
        }
    }
}
