namespace ProcessingTools.Bio.Environments.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BioEnvironmentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = typeof(BioEnvironmentsDbContext).FullName;
        }

        protected override void Seed(BioEnvironmentsDbContext context)
        {
        }
    }
}