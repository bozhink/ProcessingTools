namespace ProcessingTools.Web.Documents
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Users.Data.Entity.UsersDbContext, ProcessingTools.Users.Data.Entity.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Documents.Data.DocumentsDbContext, ProcessingTools.Documents.Data.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Geo.Data.GeoDbContext, ProcessingTools.Geo.Data.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Bio.Data.Entity.BioDbContext, ProcessingTools.Bio.Data.Entity.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.DataResources.Data.Entity.DataResourcesDbContext, ProcessingTools.DataResources.Data.Entity.Migrations.Configuration>());
        }
    }
}