namespace ProcessingTools.Bio.Data
{
    using System.Data.Entity;

    using Contracts;
    using Migrations;

    using ProcessingTools.Data.Common.Entity.Factories;

    public class BioDataInitializer : DbContextInitializerFactory<BioDbContext>, IBioDataInitializer
    {
        public BioDataInitializer(IBioDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());
        }
    }
}