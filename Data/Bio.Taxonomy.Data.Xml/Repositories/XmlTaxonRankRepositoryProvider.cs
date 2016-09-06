namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;

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

        public ITaxonRankRepository Create()
        {
            return new XmlTaxonRankRepository(this.contextProvider, ConfigBuilder.Create());
        }
    }
}
