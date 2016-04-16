namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Data;
    using ProcessingTools.Data.Migrations;

    public class DataDbSeeder : IDataDbSeeder
    {
        public async Task Seed()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataDbContext, Configuration>());

            using (var db = new DataDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }
    }
}