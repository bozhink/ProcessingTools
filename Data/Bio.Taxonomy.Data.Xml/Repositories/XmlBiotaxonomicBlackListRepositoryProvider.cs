namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class XmlBiotaxonomicBlackListRepositoryProvider : IXmlBiotaxonomicBlackListRepositoryProvider
    {
        private readonly IXmlBiotaxonomicBlackListContextProvider provider;

        public XmlBiotaxonomicBlackListRepositoryProvider(IXmlBiotaxonomicBlackListContextProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public ICrudRepository<IBlackListEntity> Create()
        {
            return new XmlBiotaxonomicBlackListRepository(this.provider, ConfigBuilder.Create());
        }
    }
}
