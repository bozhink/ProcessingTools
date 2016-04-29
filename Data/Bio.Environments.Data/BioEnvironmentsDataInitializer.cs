namespace ProcessingTools.Bio.Environments.Data.Seed
{
    using System.Data.Entity;

    using Contracts;
    using Migrations;

    using ProcessingTools.Data.Common.Entity.Factories;

    public class BioEnvironmentsDataInitializer : DbContextInitializerFactory<BioEnvironmentsDbContext>, IBioEnvironmentsDataInitializer
    {
        public BioEnvironmentsDataInitializer(IBioEnvironmentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioEnvironmentsDbContext, Configuration>());
        }
    }
}