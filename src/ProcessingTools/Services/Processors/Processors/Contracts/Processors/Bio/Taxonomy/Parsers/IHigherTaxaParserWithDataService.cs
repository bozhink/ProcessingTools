namespace ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public interface IHigherTaxaParserWithDataService<TService, T> : IXmlContextParser<long>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
    }
}
