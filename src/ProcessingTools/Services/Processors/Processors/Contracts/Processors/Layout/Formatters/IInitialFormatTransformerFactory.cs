namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts.Processors;
    using ProcessingTools.Enumerations;

    public interface IInitialFormatTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
