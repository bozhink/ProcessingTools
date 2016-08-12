namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class XmlTaxonRankRepositoryProvider : IXmlTaxonRankRepositoryProvider
    {
        private readonly ITaxaContextProvider contextProvider;

        public XmlTaxonRankRepositoryProvider(ITaxaContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<Taxon> Create()
        {
            return new XmlTaxonRankRepository(this.contextProvider, ConfigBuilder.Create());
        }
    }
}