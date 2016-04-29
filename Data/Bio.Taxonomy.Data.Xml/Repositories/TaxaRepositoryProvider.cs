namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
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