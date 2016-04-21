namespace ProcessingTools.Bio.Taxonomy.Data.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class TaxaRepositoryProvider : ITaxaRepositoryProvider
    {
        private readonly ITaxaContextProvider contextProvider;

        public TaxaRepositoryProvider(ITaxaContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<Taxon> Create()
        {
            return new TaxaRepository(this.contextProvider, ConfigBuilder.Create());
        }
    }
}
