namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.Bio.Data;
    using ProcessingTools.Bio.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeBioDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());
            var db = new BioDbContext();
            db.Database.Delete();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}