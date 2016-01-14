namespace ProcessingTools.Bio.Biorepositories.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BiorepositoriesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "ProcessingTools.Bio.Biorepositories.Data.BiorepositoriesDbContext";
        }

        protected override void Seed(BiorepositoriesDbContext context)
        {
        }
    }
}