namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Configurator;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class XmlBiotaxonomicBlackListIterableRepositoryProvider : IXmlBiotaxonomicBlackListIterableRepositoryProvider
    {
        private readonly IXmlBiotaxonomicBlackListContextProvider provider;

        public XmlBiotaxonomicBlackListIterableRepositoryProvider(IXmlBiotaxonomicBlackListContextProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public IIterableRepository<IBlackListEntity> Create()
        {
            return new XmlBiotaxonomicBlackListIterableRepository(this.provider, ConfigBuilder.Create());
        }
    }
}
