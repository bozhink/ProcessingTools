namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.MediaType.Data;
    using ProcessingTools.MediaType.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeMediaTypeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());
            var db = new MediaTypesDbContext();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}