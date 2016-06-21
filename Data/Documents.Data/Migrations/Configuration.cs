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
#if DEBUG
            this.AutomaticMigrationDataLossAllowed = true;
#else
            this.AutomaticMigrationDataLossAllowed = false;
#endif
        }

        protected override void Seed(DocumentsDbContext context)
        {
        }
    }
}