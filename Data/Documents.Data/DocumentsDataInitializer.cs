namespace ProcessingTools.Documents.Data
{
    using System.Data.Entity;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Factories;
    using ProcessingTools.Documents.Data.Migrations;

    public class DocumentsDataInitializer : DbContextInitializerFactory<DocumentsDbContext>, IDocumentsDataInitializer
    {
        public DocumentsDataInitializer(IDocumentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocumentsDbContext, Configuration>());
        }
    }
}