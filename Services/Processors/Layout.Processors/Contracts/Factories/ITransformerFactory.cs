namespace ProcessingTools.Layout.Processors.Contracts.Factories
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;

    public interface ITransformerFactory
    {
        IXmlTransformer Create(SchemaType schemaType);
    }
}
