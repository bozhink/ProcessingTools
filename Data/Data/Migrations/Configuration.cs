namespace ProcessingTools.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<DataDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(DataDbContext context)
        {
        }
    }
}
