namespace ProcessingTools.BaseLibrary
{
    using System.Linq;

    using Configurator;
    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;

    public class StringHarvestTagger : TaggerBase, IBaseTagger
    {
        private ITagContent tag;

        private IHarvester<string> harvester;
        private IXPathProvider xpathProvider;
        private ILogger logger;

        public StringHarvestTagger(Config config, string xml, IHarvester<string> harvester, ITagContent tag, IXPathProvider xpathProvider, ILogger logger)
            : base(config, xml)
        {
            this.harvester = harvester;
            this.tag = tag;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public StringHarvestTagger(IBase baseObject, IHarvester<string> harvester, ITagContent tag, IXPathProvider xpathProvider, ILogger logger)
            : base(baseObject)
        {
            this.harvester = harvester;
            this.tag = tag;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public void Tag()
        {
            this.harvester.Harvest(this.TextContent);

            var nodeList = this.XmlDocument
                .SelectNodes(this.xpathProvider.SelectContentNodesXPathTemplate, this.NamespaceManager);

            this.harvester
                .Data
                .ToList()
                .TagContentInDocument(this.tag, nodeList, false, true, this.logger);
        }
    }
}
