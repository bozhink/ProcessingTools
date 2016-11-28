namespace ProcessingTools.Documents.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

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
