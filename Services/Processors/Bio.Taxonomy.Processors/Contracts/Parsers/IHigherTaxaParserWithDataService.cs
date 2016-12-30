namespace ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface IHigherTaxaParserWithDataService<TService, T> : IGenericXmlContextParser<long>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
    }
}
