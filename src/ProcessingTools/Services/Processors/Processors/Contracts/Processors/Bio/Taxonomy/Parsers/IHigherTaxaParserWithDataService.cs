namespace ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface IHigherTaxaParserWithDataService<TService, T> : IXmlContextParser<long>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
    }
}
