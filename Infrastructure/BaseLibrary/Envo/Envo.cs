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
            XmlDocument envoTermsTagSet = this.GenerateEnvoTagSet();

            XmlNodeList nodeList = this.XmlDocument.SelectNodes("/*", this.NamespaceManager);
            envoTermsTagSet
                .DocumentElement
                .ChildNodes
                .TagContentInDocument(nodeList, false, true, this.logger);
        }

        private XmlDocument GenerateEnvoTagSet()
        {
            XmlDocument envoTermsTagSet = new XmlDocument();
            envoTermsTagSet.LoadXml("<items />");

            this.harvester.Harvest(this.TextContent);
            this.harvester.Data
                .ToList()
                .ForEach(t =>
                {
                    XmlElement node = envoTermsTagSet.CreateElement(EnvoTagName);
                    for (int i = 0, len = t.Types.Length; i < len; ++i)
                    {
                        node.SetAttribute($"type{i + 1}", t.Types[i].ToString());
                        node.SetAttribute($"identifier{i + 1}", t.Identifiers[i]);
                    }

                    node.InnerText = t.Content;
                    envoTermsTagSet.DocumentElement.AppendChild(node);
                });

            return envoTermsTagSet;
        }
    }
}