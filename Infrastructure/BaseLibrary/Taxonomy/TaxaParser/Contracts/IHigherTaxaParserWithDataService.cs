namespace ProcessingTools.BaseLibrary.Taxonomy.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public interface IHigherTaxaParserWithDataService<TService, T> : IGenericXmlContextParser<long>
        where TService : ITaxonRankResolverDataService
        where T : ITaxonRank
    {
    }
}
