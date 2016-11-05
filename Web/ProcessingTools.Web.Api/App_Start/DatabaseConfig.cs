namespace ProcessingTools.Web.Api
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public static class DatabaseConfig
    {
        public static async Task Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Users.Data.Entity.UsersDbContext, ProcessingTools.Users.Data.Entity.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Geo.Data.GeoDbContext, ProcessingTools.Geo.Data.Migrations.Configuration>());

            await DependencyResolver.Current
                .GetService<ProcessingTools.Bio.Data.Entity.Contracts.IBioDataInitializer>()
                .Initialize();
        }
    }
}