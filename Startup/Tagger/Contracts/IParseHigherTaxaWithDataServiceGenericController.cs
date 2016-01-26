namespace ProcessingTools.MainProgram.Contracts
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public interface IParseHigherTaxaWithDataServiceGenericController<TService> : ITaggerController
        where TService : ITaxaDataService<ITaxonClassification>
    {
    }
}
