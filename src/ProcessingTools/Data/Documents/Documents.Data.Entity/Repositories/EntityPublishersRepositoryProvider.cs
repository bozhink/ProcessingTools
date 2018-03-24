namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using ProcessingTools.Documents.Data.Entity.Contracts;

    public class EntityPublishersRepositoryProvider : IEntityPublishersRepositoryProvider
    {
        private readonly IDocumentsDbContextProvider contextProvider;

        public EntityPublishersRepositoryProvider(IDocumentsDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
        }

        public IEntityPublishersRepository Create()
        {
            return new EntityPublishersRepository(this.contextProvider);
        }
    }
}
