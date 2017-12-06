namespace ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers
{
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public interface ITreatmentMetaParserWithDataService<TService> : ITreatmentMetaParser
        where TService : ITaxaClassificationResolver
    {
    }
}
