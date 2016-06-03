namespace ProcessingTools.Geo.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Geo.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<GeoDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(GeoDbContext context)
        {
        }
    }
}