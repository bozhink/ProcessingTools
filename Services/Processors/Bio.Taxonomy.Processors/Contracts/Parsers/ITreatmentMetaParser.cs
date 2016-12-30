namespace ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    public interface ITreatmentMetaParser<TService> : IDocumentParser
        where TService : ITaxaClassificationResolver
    {
    }
}
