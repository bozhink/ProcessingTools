namespace ProcessingTools.Bio.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BioDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(BioDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(BioDbContext context)
        {
            // Skip
        }
    }
}
