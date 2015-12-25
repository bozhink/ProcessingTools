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

            ////Database.SetInitializer(
            ////    new MigrateDatabaseToLatestVersion<ProcessingTools.Bio.Data.BioDbContext, ProcessingTools.Bio.Data.Migrations.Configuration>());
            ////ProcessingTools.Bio.Data.BioDbContext.Create().Database.Initialize(true);

            ////Database.SetInitializer(
            ////    new MigrateDatabaseToLatestVersion<ProcessingTools.Data.DataDbContext, ProcessingTools.Data.Migrations.Configuration>());
            ////ProcessingTools.Data.DataDbContext.Create().Database.Initialize(true);

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.MediaType.Data.MediaTypesDbContext, ProcessingTools.MediaType.Data.Migrations.Configuration>());
            ////ProcessingTools.MediaType.Data.MediaTypesDbContext.Create().Database.Initialize(true);

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Bio.Environments.Data.BioEnvironmentsDbContext, ProcessingTools.Bio.Environments.Data.Migrations.Configuration>());
            ////ProcessingTools.Bio.Environments.Data.BioEnvironmentsDbContext.Create().Database.Initialize(true);
        }
    }
}