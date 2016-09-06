namespace ProcessingTools.Tagger.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaWithAphiaController : IParseHigherTaxaWithDataServiceGenericController<IAphiaTaxaRankResolverDataService>
    {
    }
}
