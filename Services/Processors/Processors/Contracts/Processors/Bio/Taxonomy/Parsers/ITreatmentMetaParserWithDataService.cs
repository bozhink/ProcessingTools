namespace ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
