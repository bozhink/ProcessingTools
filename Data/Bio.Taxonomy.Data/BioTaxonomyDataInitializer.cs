namespace ProcessingTools.Bio.Taxonomy.Data.Seed
{
    using System.Data.Entity;

    using Contracts;
    using Migrations;

    using ProcessingTools.Data.Common.Entity.Factories;

    public class BioTaxonomyDataInitializer : DbContextInitializerFactory<BioTaxonomyDbContext>, IBioTaxonomyDataInitializer
    {
        public BioTaxonomyDataInitializer(IBioTaxonomyDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioTaxonomyDbContext, Configuration>());
        }
    }
}