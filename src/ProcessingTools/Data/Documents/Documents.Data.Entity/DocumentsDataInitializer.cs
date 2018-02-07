namespace ProcessingTools.Documents.Data.Entity
{
    using System.Data.Entity;
    using ProcessingTools.Data.Common.Entity.Abstractions;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Migrations;

    public class DocumentsDataInitializer : GenericDbContextInitializer<DocumentsDbContext>, IDocumentsDataInitializer
    {
        public DocumentsDataInitializer(IDocumentsDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocumentsDbContext, Configuration>());
        }
    }
}
