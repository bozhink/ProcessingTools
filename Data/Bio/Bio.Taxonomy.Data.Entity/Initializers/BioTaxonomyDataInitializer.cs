namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class BioTaxonomyDataInitializer : GenericDbContextInitializer<BioTaxonomyDbContext>, IBioTaxonomyDataInitializer
    {
        public BioTaxonomyDataInitializer(IBioTaxonomyDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioTaxonomyDbContext, Configuration>());
        }
    }
}
