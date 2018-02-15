namespace ProcessingTools.Processors.Contracts.Bio.Taxonomy
{
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
