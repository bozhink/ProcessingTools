namespace ProcessingTools.Bio.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class BioDataInitializer : GenericDbContextInitializer<BioDbContext>, IBioDataInitializer
    {
        public BioDataInitializer(IBioDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());
        }
    }
}
