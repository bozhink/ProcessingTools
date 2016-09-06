namespace ProcessingTools.Tagger.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaWithCatalogueOfLifeController : IParseHigherTaxaWithDataServiceGenericController<ICatalogueOfLifeTaxaRankResolverDataService>
    {
    }
}
