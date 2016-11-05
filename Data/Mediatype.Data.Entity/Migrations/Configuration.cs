namespace ProcessingTools.MediaType.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MediaTypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(MediaTypesDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(MediaTypesDbContext context)
        {
        }
    }
}
