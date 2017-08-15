namespace ProcessingTools.Bio.Environments.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BioEnvironmentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(BioEnvironmentsDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(BioEnvironmentsDbContext context)
        {
            // Skip
        }
    }
}
