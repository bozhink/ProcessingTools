namespace ProcessingTools.Bio.Taxonomy.Data.Xml.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Bio.Taxonomy.Data.Xml.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Common.File.Repositories;

    public class XmlTaxonRankRepository : FileGenericRepository<IXmlTaxaContext, ITaxonRankEntity>, IXmlTaxonRankRepository
    {
        private readonly string dataFileName;

        public XmlTaxonRankRepository(string dataFileName, IFactory<IXmlTaxaContext> contextFactory)
            : base(contextFactory)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            this.dataFileName = dataFileName;

            this.Context.LoadFromFile(this.dataFileName).Wait();
        }

        public override Task<long> SaveChanges() => this.Context.WriteToFile(this.dataFileName);
    }
}
