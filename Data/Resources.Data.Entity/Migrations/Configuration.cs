namespace ProcessingTools.Resources.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Resources.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<DataResourcesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(DataResourcesDbContext context)
        {
        }
    }
}
