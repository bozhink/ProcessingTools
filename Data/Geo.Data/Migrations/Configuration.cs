namespace ProcessingTools.Geo.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<GeoDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = typeof(GeoDbContext).FullName;
        }

        protected override void Seed(GeoDbContext context)
        {
        }
    }
}