namespace ProcessingTools.Web.Documents
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
                new MigrateDatabaseToLatestVersion<ProcessingTools.Documents.Data.DocumentsDbContext, ProcessingTools.Documents.Data.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Geo.Data.GeoDbContext, ProcessingTools.Geo.Data.Migrations.Configuration>());

            await DependencyResolver.Current
                .GetService<ProcessingTools.Bio.Data.Entity.Contracts.IBioDataInitializer>()
                .Initialize();

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.DataResources.Data.Entity.DataResourcesDbContext, ProcessingTools.DataResources.Data.Entity.Migrations.Configuration>());
        }
    }
}