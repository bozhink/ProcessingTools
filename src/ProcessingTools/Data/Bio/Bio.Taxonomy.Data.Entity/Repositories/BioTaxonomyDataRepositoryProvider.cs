namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Contracts.Repositories;

    public class BioTaxonomyDataRepositoryProvider<T> : IBioTaxonomyDataRepositoryProvider<T>
        where T : class
    {
        private readonly IBioTaxonomyDbContextProvider contextProvider;

        public BioTaxonomyDataRepositoryProvider(IBioTaxonomyDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ICrudRepository<T> Create()
        {
            return new BioTaxonomyDataRepository<T>(this.contextProvider);
        }
    }
}
