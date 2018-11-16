namespace ProcessingTools.Data.Entity.Documents
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class DocumentsDataInitializer : DbContextInitializer<DocumentsDbContext>, IDocumentsDataInitializer
    {
        public DocumentsDataInitializer(DocumentsDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
