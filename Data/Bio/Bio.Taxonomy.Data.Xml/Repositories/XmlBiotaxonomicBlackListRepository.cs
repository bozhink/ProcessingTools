namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlBiotaxonomicBlackListRepository : FileGenericRepository<IXmlBiotaxonomicBlackListContext, IBlackListEntity>, IXmlBiotaxonomicBlackListRepository, IXmlBiotaxonomicBlackListIterableRepository
    {
        public XmlBiotaxonomicBlackListRepository(IFactory<IXmlBiotaxonomicBlackListContext> contextFactory, IConfig config)
            : base(contextFactory)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.Context.LoadFromFile(this.Config.BlackListXmlFilePath).Wait();
        }

        private IConfig Config { get; set; }

        public override Task<long> SaveChanges() => this.Context.WriteToFile(this.Config.BlackListXmlFilePath);

        public override Task<object> Update(IBlackListEntity entity) => this.Add(entity);
    }
}
