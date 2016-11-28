namespace ProcessingTools.Geo.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class GeoDataInitializer : GenericDbContextInitializer<GeoDbContext>, IGeoDataInitializer
    {
        public GeoDataInitializer(IGeoDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<GeoDbContext, Configuration>());
        }
    }
}
