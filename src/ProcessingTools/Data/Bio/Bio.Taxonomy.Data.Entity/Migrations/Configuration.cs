namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BioTaxonomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(BioTaxonomyDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(BioTaxonomyDbContext context)
        {
            // Skip
        }
    }
}
