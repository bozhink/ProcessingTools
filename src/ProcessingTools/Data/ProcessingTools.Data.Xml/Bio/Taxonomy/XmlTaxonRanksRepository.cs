namespace ProcessingTools.Data.Xml.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Xml.Abstractions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class XmlTaxonRanksRepository : FileRepository<IXmlTaxaContext, ITaxonRankItem>, IXmlTaxonRankRepository
    {
        private readonly string dataFileName;

        public XmlTaxonRanksRepository(string dataFileName, IFactory<IXmlTaxaContext> contextFactory)
            : base(contextFactory)
        {
            if (string.IsNullOrWhiteSpace(dataFileName))
            {
                throw new ArgumentNullException(nameof(dataFileName));
            }

            this.dataFileName = dataFileName;

            this.Context.LoadFromFileAsync(this.dataFileName).Wait();
        }

        public override async Task<object> SaveChangesAsync() => await this.Context.WriteToFileAsync(this.dataFileName).ConfigureAwait(false);
    }
}
