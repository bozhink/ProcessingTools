namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;

    public interface IStringTagger
    {
        Task<object> Tag(IDocument document, IEnumerable<string> data, XmlElement tagModel, string contentNodesXPathTemplate);
    }
}
