namespace ProcessingTools.Bio.Environments.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class BioEnvironmentsDataInitializer : GenericDbContextInitializer<BioEnvironmentsDbContext>, IBioEnvironmentsDataInitializer
    {
        public BioEnvironmentsDataInitializer(IBioEnvironmentsDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioEnvironmentsDbContext, Configuration>());
        }
    }
}
