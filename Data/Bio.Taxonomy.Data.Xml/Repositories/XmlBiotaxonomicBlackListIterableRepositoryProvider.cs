namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts.Data.Repositories;

    public class XmlBiotaxonomicBlackListIterableRepositoryProvider : IXmlBiotaxonomicBlackListIterableRepositoryProvider
    {
        private readonly IXmlBiotaxonomicBlackListContextProvider contextProvider;

        public XmlBiotaxonomicBlackListIterableRepositoryProvider(IXmlBiotaxonomicBlackListContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IIterableRepository<IBlackListEntity> Create()
        {
            return new XmlBiotaxonomicBlackListIterableRepository(this.contextProvider, ConfigBuilder.Create());
        }
    }
}
