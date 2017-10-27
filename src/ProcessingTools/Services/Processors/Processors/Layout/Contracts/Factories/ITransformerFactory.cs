namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Enumerations;
    using ProcessingTools.Processors.Contracts;

    public interface ITransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
