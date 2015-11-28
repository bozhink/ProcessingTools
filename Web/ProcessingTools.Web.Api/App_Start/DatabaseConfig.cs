namespace ProcessingTools.Web.Api
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Api.Data.ApplicationDbContext, ProcessingTools.Api.Data.Migrations.Configuration>());
            ////ProcessingTools.Api.Data.ApplicationDbContext.Create().Database.Initialize(true);

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Mediatype.Data.MediatypesDbContext, ProcessingTools.Mediatype.Data.Migrations.Configuration>());
            ////ProcessingTools.Mediatype.Data.MediatypesDbContext.Create().Database.Initialize(true);
        }
    }
}