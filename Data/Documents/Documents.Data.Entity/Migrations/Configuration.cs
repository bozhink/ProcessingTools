namespace ProcessingTools.Documents.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DocumentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(DocumentsDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(DocumentsDbContext context)
        {
        }
    }
}
