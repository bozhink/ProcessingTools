namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Extensions;
    using Factories;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Geo.Data.Miners.Contracts;

    public class TagGeoNamesController : TaggerControllerFactory, ITagGeoNamesController
    {
        private IGeoNamesDataMiner miner;

        public TagGeoNamesController(IGeoNamesDataMiner miner)
        {
            this.miner = miner;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            XmlElement tagModel = document.CreateElement("named-content");
            tagModel.SetAttribute("content-type", "geo name");

            var xpathProvider = new XPathProvider(settings.Config);

            var textContent = document.GetTextContent(settings.Config.TextContentXslTransform);
            var data = await this.miner.Mine(textContent);

            var tagger = new StringTagger(document.OuterXml, data, tagModel, xpathProvider.SelectContentNodesXPathTemplate, namespaceManager, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);
        }
    }
}
