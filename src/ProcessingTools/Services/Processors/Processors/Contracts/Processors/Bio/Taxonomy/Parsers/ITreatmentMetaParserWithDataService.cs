namespace ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
