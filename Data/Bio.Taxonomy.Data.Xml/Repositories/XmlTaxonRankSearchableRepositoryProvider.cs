namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class XmlTaxonRankSearchableRepositoryProvider : IXmlTaxonRankSearchableRepositoryProvider
    {
        private readonly ITaxaContextProvider contextProvider;

        public XmlTaxonRankSearchableRepositoryProvider(ITaxaContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableRepository<Taxon> Create()
        {
            return new XmlTaxonRankSearchableRepository(this.contextProvider, ConfigBuilder.Create());
        }
    }
}
