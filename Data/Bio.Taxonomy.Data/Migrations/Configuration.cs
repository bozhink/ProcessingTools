namespace ProcessingTools.Bio.Taxonomy.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BioTaxonomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = typeof(BioTaxonomyDbContext).FullName;
        }

        protected override void Seed(BioTaxonomyDbContext context)
        {
        }
    }
}