namespace ProcessingTools.Bio.Taxonomy.Data
{
    using System;
    using Contracts;

    public class BioTaxonomyDbContextProvider : IBioTaxonomyDbContextProvider
    {
        private readonly IBioTaxonomyDbContextFactory contextFactory;

        public BioTaxonomyDbContextProvider(IBioTaxonomyDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this.contextFactory = contextFactory;
        }

        public BioTaxonomyDbContext Create()
        {
            return this.contextFactory.Create();
        }
    }
}