namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;

    public class DocumentsRepositoryProvider<T> : IDocumentsRepositoryProvider<T>
        where T : class, IEntityWithPreJoinedFields
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

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new DocumentsRepository<T>(this.contextProvider);
        }
    }
}
