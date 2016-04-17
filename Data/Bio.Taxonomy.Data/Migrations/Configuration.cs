namespace ProcessingTools.Bio.Taxonomy.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<TaxonomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = typeof(TaxonomyDbContext).FullName;
        }

        protected override void Seed(TaxonomyDbContext context)
        {
        }
    }
}