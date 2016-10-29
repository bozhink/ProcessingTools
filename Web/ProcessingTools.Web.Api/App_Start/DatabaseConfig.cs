namespace ProcessingTools.Web.Api
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Users.Data.Entity.UsersDbContext, ProcessingTools.Users.Data.Entity.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Geo.Data.GeoDbContext, ProcessingTools.Geo.Data.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Bio.Data.BioDbContext, ProcessingTools.Bio.Data.Migrations.Configuration>());
        }
    }
}