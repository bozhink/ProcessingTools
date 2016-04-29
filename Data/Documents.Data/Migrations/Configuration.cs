namespace ProcessingTools.Documents.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<DocumentsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = typeof(DocumentsDbContext).FullName;
        }

        protected override void Seed(DocumentsDbContext context)
        {
        }
    }
}