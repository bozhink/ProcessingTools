namespace ProcessingTools.DbSeeder.Seeders
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data;
    using ProcessingTools.Bio.Taxonomy.Data.Migrations;

    public class TaxonomyDbSeeder : IDbSeeder
    {
        public Task Seed()
        {
            return Task.Run(() =>
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaxonomyDbContext, Configuration>());
                var db = new TaxonomyDbContext();
                db.Database.CreateIfNotExists();
                db.Database.Initialize(true);
                db.SaveChanges();
                db.Dispose();
            });
        }
    }
}