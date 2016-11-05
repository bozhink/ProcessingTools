namespace ProcessingTools.Bio.Environments.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Bio.Environments.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<BioEnvironmentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(BioEnvironmentsDbContext context)
        {
        }
    }
}