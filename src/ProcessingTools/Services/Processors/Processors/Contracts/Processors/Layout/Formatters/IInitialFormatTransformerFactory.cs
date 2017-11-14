namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts;

    public interface IInitialFormatTransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
