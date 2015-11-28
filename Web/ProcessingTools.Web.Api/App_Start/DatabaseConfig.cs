namespace ProcessingTools.Web.Api
{
    using System.Data.Entity;

    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Mediatype.Data.MediatypesDbContext, Mediatype.Data.Migrations.Configuration>());
            ////Mediatype.Data.MediatypesDbContext.Create().Database.Initialize(true);
        }
    }
}