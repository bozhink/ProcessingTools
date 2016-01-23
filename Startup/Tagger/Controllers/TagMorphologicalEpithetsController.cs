namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;

    public class TagMorphologicalEpithetsController : TaggerControllerFactory, ITagMorphologicalEpithetsController
    {
        private IMorphologicalEpithetDataMiner miner;

        public TagMorphologicalEpithetsController(IMorphologicalEpithetDataMiner miner)
        {
            this.miner = miner;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            XmlElement tagModel = document.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "morphological epithet");

            var xpathProvider = new XPathProvider(settings.Config);

            var harvestableDocument = new HarvestableDocument(settings.Config, document.OuterXml);
            var data = await this.miner.Mine(harvestableDocument.TextContent);

            var tagger = new StringTagger(document.OuterXml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, namespaceManager, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
