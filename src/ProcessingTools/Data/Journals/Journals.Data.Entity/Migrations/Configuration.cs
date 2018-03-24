namespace ProcessingTools.Journals.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<JournalsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(JournalsDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(JournalsDbContext context)
        {
            // Skip
        }
    }
}
