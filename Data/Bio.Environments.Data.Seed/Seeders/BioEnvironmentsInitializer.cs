namespace ProcessingTools.Bio.Environments.Data.Seed.Seeders
{
    using System.Data.Entity;

    using Contracts;

    using ProcessingTools.Bio.Environments.Data.Migrations;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Data.Common.Entity.Factories;

    public class BioEnvironmentsInitializer : DbContextInitializerFactory<BioEnvironmentsDbContext>, IBioEnvironmentsInitializer
    {
        public BioEnvironmentsInitializer(IDbContextProvider<BioEnvironmentsDbContext> contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioEnvironmentsDbContext, Configuration>());
        }
    }
}