namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class StringTagger : Base, ITagger
    {
        private string contentNodesXPathTemplate;
        private XmlElement tagModel;
        private XmlDocumentFragment bufferXml;
        private IQueryable<string> data;
        private ILogger logger;

        public StringTagger(string xml, IQueryable<string> data, XmlElement tagModel, string contentNodesXPathTemplate, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.tagModel = tagModel;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.logger = logger;

            this.bufferXml = this.XmlDocument.CreateDocumentFragment();
        }

        public void Tag()
        {
            this.data.ToList()
                .Select(this.XmlEncode)
                .OrderByDescending(i => i.Length)
                .TagContentInDocument(
                    this.tagModel,
                    this.contentNodesXPathTemplate,
                    this.XmlDocument,
                    false,
                    true,
                    this.logger);
        }

        private string XmlEncode(string text)
        {
            this.bufferXml.InnerText = text;
            return this.bufferXml.InnerXml;
        }
    }
}
