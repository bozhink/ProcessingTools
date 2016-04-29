namespace ProcessingTools.Data.Seed
{
    using System.Data.Entity;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Factories;
    using ProcessingTools.Data.Migrations;

    public class DataInitializer : DbContextInitializerFactory<DataDbContext>, IDataInitializer
    {
        public DataInitializer(IDataDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataDbContext, Configuration>());
        }
    }
}