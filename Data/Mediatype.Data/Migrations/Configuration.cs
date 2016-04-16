namespace ProcessingTools.MediaType.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<MediaTypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = typeof(MediaTypesDbContext).FullName;
        }

        protected override void Seed(MediaTypesDbContext context)
        {
        }
    }
}