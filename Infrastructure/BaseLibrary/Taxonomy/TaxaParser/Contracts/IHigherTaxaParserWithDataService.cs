namespace ProcessingTools.BaseLibrary.Taxonomy.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public interface IHigherTaxaParserWithDataService<TTaxaRankDataService> : IGenericXmlContextParser<long>
        where TTaxaRankDataService : ITaxonRankResolverDataService
    {
    }
}
