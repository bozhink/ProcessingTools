namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public interface ITransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
