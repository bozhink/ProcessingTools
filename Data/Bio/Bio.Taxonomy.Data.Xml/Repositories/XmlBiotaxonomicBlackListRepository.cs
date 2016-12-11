namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlBiotaxonomicBlackListRepository : FileGenericRepository<IXmlBiotaxonomicBlackListContext, IBlackListEntity>, IXmlBiotaxonomicBlackListRepository, IXmlBiotaxonomicBlackListIterableRepository
    {
        public XmlBiotaxonomicBlackListRepository(IXmlBiotaxonomicBlackListContextProvider contextProvider, IConfig config)
            : base(contextProvider)
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
