namespace ProcessingTools.Bio.Taxonomy.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<BioTaxonomyDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(BioTaxonomyDbContext context)
        {
        }
    }
}