namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using ProcessingTools.Data.Common.Entity.Models.Contracts;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Contracts.Repositories;

    public class DocumentsRepositoryProvider<T> : IDocumentsRepositoryProvider<T>
        where T : class, IEntityWithPreJoinedFields
    {
        private readonly IDocumentsDbContextProvider contextProvider;

        public DocumentsRepositoryProvider(IDocumentsDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
        }

        public ICrudRepository<T> Create()
        {
            return new DocumentsRepository<T>(this.contextProvider);
        }
    }
}
