namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Enumerations;

    public interface INormalizationTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
