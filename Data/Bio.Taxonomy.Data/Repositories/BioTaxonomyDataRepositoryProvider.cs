namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

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

        public IGenericRepository<T> Create()
        {
            return new BioTaxonomyDataRepository<T>(this.contextProvider);
        }
    }
}