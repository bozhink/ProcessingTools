namespace Runner
{
    using System.Data.Entity;

    using ProcessingTools.Geo.Data;
    using ProcessingTools.Geo.Data.Migrations;

    public partial class RunnerStartup
    {
        private static void InitializeGeoDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GeoDbContext, Configuration>());
            var db = new GeoDbContext();
            db.Database.Delete();
            db.Database.CreateIfNotExists();
            db.Database.Initialize(true);
            db.SaveChanges();
            db.Dispose();
        }
    }
}