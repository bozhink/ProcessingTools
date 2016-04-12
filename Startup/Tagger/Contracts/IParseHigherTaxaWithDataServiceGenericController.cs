namespace ProcessingTools.Tagger.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaWithDataServiceGenericController<TService> : ITaggerController
        where TService : ITaxaInformationResolverDataService<ITaxonClassification>
    {
    }
}
