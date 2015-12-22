namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.Bio.Environments.Data;
    using ProcessingTools.Bio.Environments.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeBioEnvironmentsDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioEnvironmentsDbContext, Configuration>());
            var db = new BioEnvironmentsDbContext();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
