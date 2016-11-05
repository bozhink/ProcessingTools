namespace ProcessingTools.Bio.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Bio.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<BioDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(BioDbContext context)
        {
        }
    }
}
