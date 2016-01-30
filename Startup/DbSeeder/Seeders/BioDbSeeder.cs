namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data;
    using ProcessingTools.Bio.Data.Migrations;

    public class BioDbSeeder : IDbSeeder
    {
        public Task Seed()
        {
            return Task.Run(() =>
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());
                var db = new BioDbContext();
                db.Database.Delete();
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                db.SaveChanges();
                db.Dispose();
            });
        }
    }
}