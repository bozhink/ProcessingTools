namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts.Xml;
    using ProcessingTools.Enumerations;

    public interface IInitialFormatTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
