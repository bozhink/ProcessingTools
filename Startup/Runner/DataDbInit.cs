namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.Data;
    using ProcessingTools.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeDataDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataDbContext, Configuration>());
            var db = new DataDbContext();
            db.Database.Delete();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}