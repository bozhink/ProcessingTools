namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class StringTagger : Base, ITagger
    {
        private string contentNodesXPathTemplate;
        private XmlElement tagModel;
        private IQueryable<string> data;
        private ILogger logger;

        public StringTagger(string xml, IQueryable<string> data, XmlElement tagModel, string contentNodesXPathTemplate, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.tagModel = tagModel;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.logger = logger;
        }

        public StringTagger(IBase baseObject, IQueryable<string> data, XmlElement tagModel, string contentNodesXPathTemplate, ILogger logger)
            : base(baseObject)
        {
            this.data = data;
            this.tagModel = tagModel;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.logger = logger;
        }

        public void Tag()
        {
            this.data.ToList()
                .OrderByDescending(i => i.Length)
                .TagContentInDocument(
                    this.tagModel,
                    this.contentNodesXPathTemplate,
                    this.XmlDocument,
                    false,
                    true,
                    this.logger);
        }
    }
}
