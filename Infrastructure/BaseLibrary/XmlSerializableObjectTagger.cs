namespace ProcessingTools.BaseLibrary
{
    using System.Linq;

    using Factories;
    using ProcessingTools.Contracts.Log;

    public class XmlSerializableObjectTagger<T> : XmlSerializableObjectTaggerFactory<T>
    {
        private string contentNodesXPathTemplate;
        private IQueryable<T> data;
        private ILogger logger;

        public XmlSerializableObjectTagger(string xml, IQueryable<T> data, string contentNodesXPathTemplate, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.contentNodesXPathTemplate = contentNodesXPathTemplate;
            this.logger = logger;
        }

        public override void Tag()
        {
            this.data.ToList()
                .Select(this.SerializeObject)
                .OrderByDescending(i => i.InnerText.Length)
                .TagContentInDocument(
                    this.contentNodesXPathTemplate,
                    this.XmlDocument,
                    false,
                    true,
                    this.logger);
        }
    }
}