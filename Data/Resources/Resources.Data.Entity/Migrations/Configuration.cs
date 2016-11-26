namespace ProcessingTools.Resources.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ResourcesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(ResourcesDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(ResourcesDbContext context)
        {
        }
    }
}
