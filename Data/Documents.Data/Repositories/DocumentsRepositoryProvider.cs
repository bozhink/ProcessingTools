namespace ProcessingTools.Documents.Data.Repositories
{
    using System;

    using ProcessingTools.Data.Common.Repositories.Contracts;
    using ProcessingTools.Documents.Data.Contracts;
    using ProcessingTools.Documents.Data.Repositories.Contracts;

    public class DocumentsRepositoryProvider<T> : IDocumentsRepositoryProvider<T>
        where T : class
    {
        private readonly IDocumentsDbContextProvider contextProvider;

        public DocumentsRepositoryProvider(IDocumentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<T> Create()
        {
            return new DocumentsRepository<T>(this.contextProvider);
        }
    }
}