namespace ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers
{
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
