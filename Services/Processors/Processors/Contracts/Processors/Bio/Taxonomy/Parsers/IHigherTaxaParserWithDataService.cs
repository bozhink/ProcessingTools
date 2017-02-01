namespace ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface IHigherTaxaParserWithDataService<TService, T> : IGenericXmlContextParser<long>
        where TService : ITaxaRankResolver
        where T : ITaxonRank
    {
    }
}
