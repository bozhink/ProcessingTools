namespace ProcessingTools.Resources.Data.Entity
{
    using System.Data.Entity;

    using Contracts;
    using Migrations;

    using ProcessingTools.Data.Common.Entity.Factories;

    public class DataResourcesDatabaseInitializer : DbContextInitializerFactory<DataResourcesDbContext>, IDataResourcesDatabaseInitializer
    {
        public DataResourcesDatabaseInitializer(IDataResourcesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataResourcesDbContext, Configuration>());
        }
    }
}
