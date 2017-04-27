namespace ProcessingTools.History.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HistoryDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = this.GetType().Assembly.FullName;
            this.ContextType = typeof(HistoryDbContext);
        }

        protected override void Seed(HistoryDbContext context)
        {
        }
    }
}
