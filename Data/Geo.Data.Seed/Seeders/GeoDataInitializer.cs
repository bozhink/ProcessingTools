namespace ProcessingTools.Geo.Data.Seed
{
    using System.Data.Entity;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Factories;
    using ProcessingTools.Geo.Data.Migrations;
    using ProcessingTools.Geo.Data.Repositories.Contracts;

    public class GeoDataInitializer : DbContextInitializerFactory<GeoDbContext>, IGeoDataInitializer
    {
        public GeoDataInitializer(IGeoDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GeoDbContext, Configuration>());
        }
    }
}