namespace ProcessingTools.Layout.Processors.Contracts.Taggers
{
    using ProcessingTools.Contracts;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

    public interface IStringTagger
    {
        Task<object> Tag(IDocument document, IEnumerable<string> data, XmlElement tagModel, string contentNodesXPathTemplate);
    }
}
