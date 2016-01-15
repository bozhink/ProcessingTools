namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class StringTagger : Base, ITagger
    {
        private XmlElement tagModel;
        private IQueryable<string> data;

        private IXPathProvider xpathProvider;
        private ILogger logger;

        public StringTagger(string xml, IQueryable<string> data, XmlElement tagModel, IXPathProvider xpathProvider, ILogger logger)
            : base(xml)
        {
            this.data = data;
            this.tagModel = tagModel;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public StringTagger(IBase baseObject, IQueryable<string> data, XmlElement tagModel, IXPathProvider xpathProvider, ILogger logger)
            : base(baseObject)
        {
            this.data = data;
            this.tagModel = tagModel;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public void Tag()
        {
            this.data.ToList()
                .TagContentInDocument(
                this.tagModel,
                this.xpathProvider.SelectContentNodesXPathTemplate,
                this.XmlDocument,
                false,
                true,
                this.logger);
        }
    }
}
