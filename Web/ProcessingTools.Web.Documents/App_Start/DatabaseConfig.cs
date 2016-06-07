namespace ProcessingTools.Web.Documents
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Api.Data.ApplicationDbContext, ProcessingTools.Api.Data.Migrations.Configuration>());

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Documents.Data.DocumentsDbContext, ProcessingTools.Documents.Data.Migrations.Configuration>());
            //new ProcessingTools.Data.Factories.DataDbContextFactory().Create().Database.Initialize(true);
        }
    }
}