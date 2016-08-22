namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    using ProcessingTools.Configurator;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;

    public class XmlBiotaxonomicBlackListIterableRepository : IXmlBiotaxonomicBlackListIterableRepository
    {
        public XmlBiotaxonomicBlackListIterableRepository(IXmlBiotaxonomicBlackListContextProvider contextProvider, Config config)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.Context = contextProvider.Create();
            this.Context.LoadFromFile(this.Config.BlackListXmlFilePath).Wait();
        }

        public IEnumerable<IBlackListEntity> Entities => this.Context.DataSet.ToList();

        private Config Config { get; set; }

        private IXmlBiotaxonomicBlackListContext Context { get; set; }
    }
}
