namespace ProcessingTools.Xml.Contracts.Wrappers
{
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public interface IDocumentWrapper
    {
        IDocument Create();

        IDocument Create(XmlNode context, SchemaType schemaType);
    }
}
