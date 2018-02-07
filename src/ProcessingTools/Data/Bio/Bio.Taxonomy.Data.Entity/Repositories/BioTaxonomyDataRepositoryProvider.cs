namespace ProcessingTools.Bio.Taxonomy.Data.Entity.Repositories
{
    using System;
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Contracts;

    public class BioTaxonomyDataRepositoryProvider<T> : IBioTaxonomyDataRepositoryProvider<T>
        where T : class
    {
        private readonly IBioTaxonomyDbContextProvider contextProvider;

        public BioTaxonomyDataRepositoryProvider(IBioTaxonomyDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
        }

        public ICrudRepository<T> Create()
        {
            return new BioTaxonomyDataRepository<T>(this.contextProvider);
        }
    }
}
