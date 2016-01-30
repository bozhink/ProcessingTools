namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Geo.Data;
    using ProcessingTools.Geo.Data.Migrations;

    public class GeoDbSeeder : IDbSeeder
    {
        public Task Seed()
        {
            return Task.Run(() =>
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<GeoDbContext, Configuration>());
                var db = new GeoDbContext();
                db.Database.Delete();
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                db.SaveChanges();
                db.Dispose();
            });
        }
    }
}