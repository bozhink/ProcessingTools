namespace ProcessingTools.Layout.Processors.Contracts
{
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Contracts;

    public interface IInitialFormatTransformerFactory
    {
        IXslTransformer Create(SchemaType schemaType);
    }
}
