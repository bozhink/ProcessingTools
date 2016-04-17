namespace ProcessingTools.Bio.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BioDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = typeof(BioDbContext).FullName;
        }

        protected override void Seed(BioDbContext context)
        {
        }
    }
}