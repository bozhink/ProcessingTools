namespace ProcessingTools.Tagger.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaBySuffixController : IParseHigherTaxaWithDataServiceGenericController<ISuffixHigherTaxaRankDataService>
    {
    }
}
