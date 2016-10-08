namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Contracts.Transformers;

    public interface IInitialFormatTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
