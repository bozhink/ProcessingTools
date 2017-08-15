namespace ProcessingTools.Documents.Data.Entity.Providers
{
    using System;
    using Contracts;

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
