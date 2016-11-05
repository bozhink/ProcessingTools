namespace ProcessingTools.Documents.Data.Entity.Providers
{
    using System;
    using Contracts;

    public class DocumentsDbContextProvider : IDocumentsDbContextProvider
    {
        private IDocumentsDbContextFactory contextFactory;

        public DocumentsDbContextProvider(IDocumentsDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public DocumentsDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}
