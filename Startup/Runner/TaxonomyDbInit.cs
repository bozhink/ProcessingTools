namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeTaxonomyDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaxonomyDbContext, Configuration>());
            var db = new TaxonomyDbContext();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}