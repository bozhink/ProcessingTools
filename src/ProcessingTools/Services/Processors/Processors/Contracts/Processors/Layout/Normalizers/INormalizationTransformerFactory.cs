namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts;

    public interface INormalizationTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
