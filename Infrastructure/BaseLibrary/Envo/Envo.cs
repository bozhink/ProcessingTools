namespace ProcessingTools.BaseLibrary
{
    using System.Linq;
    using System.Xml;

    using Bio.Harvesters.Contracts;
    using Configurator;
    using Contracts;
    using ProcessingTools.Contracts.Log;

    public class Envo : HarvestableDocument, IBaseTagger
    {
        private const string EnvoTagName = "envo";

        private IExtractHcmrHarvester harvester;
        private ILogger logger;

        public Envo(Config config, string xml, IExtractHcmrHarvester harvester, ILogger logger)
            : base(config, xml)
        {
            this.harvester = harvester;
            this.logger = logger;
        }

        public Envo(IBase baseObject, IExtractHcmrHarvester harvester, ILogger logger)
            : base(baseObject)
        {
            this.harvester = harvester;
            this.logger = logger;
        }

        public void Tag()
        {
            const string XPath = "/*";
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(XPath, this.NamespaceManager);

            var data = this.harvester.Harvest(this.TextContent).Result;

            data.ToList()
                .ForEach(t =>
                {
                    XmlElement element = this.XmlDocument.CreateElement(EnvoTagName);
                    element.InnerText = t.Content;
                    for (int i = 0, len = t.Types.Length; i < len; ++i)
                    {
                        element.SetAttribute($"type{i + 1}", t.Types[i].ToString());
                        element.SetAttribute($"identifier{i + 1}", t.Identifiers[i]);
                    }

                    element.TagContentInDocument(nodeList, false, true, this.logger);
                });
        }
    }
}