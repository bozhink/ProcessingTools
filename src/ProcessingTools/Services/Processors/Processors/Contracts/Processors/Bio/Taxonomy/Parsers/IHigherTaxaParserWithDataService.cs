namespace ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    public interface IHigherTaxaParserWithDataService<TService, T> : IXmlContextParser<long>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
    }
}
