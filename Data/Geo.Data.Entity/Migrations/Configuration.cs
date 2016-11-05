namespace ProcessingTools.Geo.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GeoDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(GeoDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(GeoDbContext context)
        {
        }
    }
}
