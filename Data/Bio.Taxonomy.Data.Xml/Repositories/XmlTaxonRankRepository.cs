namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Configurator;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlTaxonRankRepository : FileGenericRepository<ITaxaContext, ITaxonRankEntity>, IXmlTaxonRankRepository
    {
        public XmlTaxonRankRepository(ITaxaContextProvider contextProvider, Config config)
            : base(contextProvider)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            this.Config = config;
            this.Context.LoadFromFile(this.Config.RankListXmlFilePath).Wait();
        }

        protected Config Config { get; private set; }

        public override Task<long> SaveChanges() => this.Context.WriteToFile(this.Config.RankListXmlFilePath);
    }
}
