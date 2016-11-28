namespace ProcessingTools.DataResources.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class ResourcesDatabaseInitializer : GenericDbContextInitializer<ResourcesDbContext>, IResourcesDatabaseInitializer
    {
        public ResourcesDatabaseInitializer(IResourcesDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ResourcesDbContext, Configuration>());
        }
    }
}
