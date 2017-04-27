namespace ProcessingTools.Special.Processors.Contracts.Processors
{
    using System.Xml;
    using ProcessingTools.Contracts;

    public interface IFlora
    {
        void ParseInfra(IDocument document);

        void ParseTn(IDocument document, XmlDocument template);

        void PerformReplace(IDocument document, XmlDocument template);
    }
}
