namespace ProcessingTools.Geo.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Factories;

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
