namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.MediaType.Data;
    using ProcessingTools.MediaType.Data.Migrations;

    public class MediaTypesDbSeeder : IMediaTypesDbSeeder
    {
        public async Task Seed()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());

            using (var db = new MediaTypesDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }
    }
}