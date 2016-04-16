namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Migrations;

    public class TaxonomyDbSeeder : ITaxonomyDbSeeder
    {
        public async Task Seed()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaxonomyDbContext, Configuration>());

            using (var db = new TaxonomyDbContext())
            {
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                await db.SaveChangesAsync();
            }
        }
    }
}