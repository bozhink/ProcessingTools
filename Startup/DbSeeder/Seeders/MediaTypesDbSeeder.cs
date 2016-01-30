namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.MediaType.Data;
    using ProcessingTools.MediaType.Data.Migrations;

    public class MediaTypesDbSeeder : IDbSeeder
    {
        public Task Seed()
        {
            return Task.Run(() =>
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());
                var db = new MediaTypesDbContext();
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                db.SaveChanges();
                db.Dispose();
            });
        }
    }
}