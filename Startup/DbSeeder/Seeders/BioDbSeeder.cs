namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data;
    using ProcessingTools.Bio.Data.Migrations;

    public class BioDbSeeder : IBioDbSeeder
    {
        public async Task Seed()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());

            using (var db = new BioDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }
    }
}