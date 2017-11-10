namespace ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
