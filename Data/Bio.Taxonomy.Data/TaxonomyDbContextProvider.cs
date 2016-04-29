namespace ProcessingTools.Bio.Taxonomy.Data
{
    using System;
    using Contracts;

    public class TaxonomyDbContextProvider : ITaxonomyDbContextProvider
    {
        private readonly ITaxonomyDbContextFactory contextFactory;

        public TaxonomyDbContextProvider(ITaxonomyDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public TaxonomyDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}