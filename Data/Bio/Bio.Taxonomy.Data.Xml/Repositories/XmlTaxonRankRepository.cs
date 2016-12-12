namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlTaxonRankRepository : FileGenericRepository<IXmlTaxaContext, ITaxonRankEntity>, IXmlTaxonRankRepository, IXmlTaxonRankSearchableRepository
    {
        public XmlTaxonRankRepository(IXmlTaxaContextProvider contextProvider, IConfig config)
            : base(contextProvider)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.Context.LoadFromFile(this.Config.RankListXmlFilePath).Wait();
        }

        protected IConfig Config { get; private set; }

        public override Task<long> SaveChanges() => this.Context.WriteToFile(this.Config.RankListXmlFilePath);
    }
}
