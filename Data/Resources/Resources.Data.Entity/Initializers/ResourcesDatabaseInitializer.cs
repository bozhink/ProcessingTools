namespace ProcessingTools.DataResources.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Factories;

    public class ResourcesDatabaseInitializer : DbContextInitializerFactory<ResourcesDbContext>, IResourcesDatabaseInitializer
    {
        public ResourcesDatabaseInitializer(IResourcesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ResourcesDbContext, Configuration>());
        }
    }
}
