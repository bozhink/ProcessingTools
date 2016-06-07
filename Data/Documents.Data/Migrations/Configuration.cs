namespace ProcessingTools.Documents.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using ProcessingTools.Documents.Data.Common.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<DocumentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = ConnectionConstants.ContextKey;
        }

        protected override void Seed(DocumentsDbContext context)
        {
        }
    }
}