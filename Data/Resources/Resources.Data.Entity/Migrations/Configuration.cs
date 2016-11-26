namespace ProcessingTools.DataResources.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<DataResourcesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(DataResourcesDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(DataResourcesDbContext context)
        {
        }
    }
}
