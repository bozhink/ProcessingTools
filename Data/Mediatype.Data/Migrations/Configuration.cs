namespace ProcessingTools.MediaType.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.MediaType.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<MediaTypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(MediaTypesDbContext context)
        {
        }
    }
}