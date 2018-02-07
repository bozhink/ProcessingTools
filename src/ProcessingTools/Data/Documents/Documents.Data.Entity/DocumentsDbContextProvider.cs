namespace ProcessingTools.Documents.Data.Entity
{
    using System;
    using ProcessingTools.Documents.Data.Entity.Contracts;

    public class DocumentsDbContextProvider : IDocumentsDbContextProvider
    {
        private readonly IDocumentsDbContextFactory contextFactory;

        public DocumentsDbContextProvider(IDocumentsDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public DocumentsDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
