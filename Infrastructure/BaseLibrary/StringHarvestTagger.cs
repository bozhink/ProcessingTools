namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Configurator;
    using Contracts;

    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Log;
    using ProcessingTools.Harvesters.Common.Contracts;

    public class StringHarvestTagger : HarvestableDocument, IBaseTagger
    {
        private XmlElement tagModel;

        private IStringHarvester harvester;
        private IXPathProvider xpathProvider;
        private ILogger logger;

        public StringHarvestTagger(Config config, string xml, IStringHarvester harvester, XmlElement tagModel, IXPathProvider xpathProvider, ILogger logger)
            : base(config, xml)
        {
            this.harvester = harvester;
            this.tagModel = tagModel;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public StringHarvestTagger(IBase baseObject, IStringHarvester harvester, XmlElement tagModel, IXPathProvider xpathProvider, ILogger logger)
            : base(baseObject)
        {
            this.harvester = harvester;
            this.tagModel = tagModel;
            this.xpathProvider = xpathProvider;
            this.logger = logger;
        }

        public void Tag()
        {
            this.harvester.Harvest(this.TextContent);

            this.harvester.Data
                .ToList()
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
