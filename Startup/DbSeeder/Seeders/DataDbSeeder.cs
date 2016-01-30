namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data;
    using ProcessingTools.Data.Migrations;

    public class DataDbSeeder : IDbSeeder
    {
        public Task Seed()
        {
            return Task.Run(() =>
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataDbContext, Configuration>());
                var db = new DataDbContext();
                db.Database.Delete();
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                db.SaveChanges();
                db.Dispose();
            });
        }
    }
}