namespace ProcessingTools.Documents.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;

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
