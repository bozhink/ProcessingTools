namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Environments.Data;
    using ProcessingTools.Bio.Environments.Data.Migrations;

    public class BioEnvironmentsDbSeeder : IBioEnvironmentsDbSeeder
    {
        public async Task Seed()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioEnvironmentsDbContext, Configuration>());

            using (var db = new BioEnvironmentsDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }
    }
}