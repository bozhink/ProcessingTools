namespace ProcessingTools.Documents.Data.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Documents.Data.Contracts;

    public class EntityPublishersRepositoryProvider : IEntityPublishersRepositoryProvider
    {
        private readonly IDocumentsDbContextProvider contextProvider;

        public EntityPublishersRepositoryProvider(IDocumentsDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IEntityPublishersRepository Create()
        {
            return new EntityPublishersRepository(this.contextProvider);
        }
    }
}
