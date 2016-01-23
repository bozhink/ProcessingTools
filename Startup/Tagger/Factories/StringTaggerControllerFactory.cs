namespace ProcessingTools.MainProgram.Factories
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Extensions;

    public abstract class StringTaggerControllerFactory : ITaggerController
    {
        protected abstract IStringDataMiner Miner { get; }

        protected abstract XmlElement TagModel { get; }

        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            document.LoadXml(Resources.ContextWrapper);
            document.DocumentElement.InnerXml = context.InnerXml;

            var textContent = document.GetTextContent(settings.Config.TextContentXslTransform);
            var data = await this.Miner.Mine(textContent);

            var xpathProvider = new XPathProvider(settings.Config);

            var tagger = new StringTagger(document.OuterXml, data, this.TagModel, xpathProvider.SelectContentNodesXPathTemplate, namespaceManager, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml);

            context.InnerXml = document.DocumentElement.InnerXml;
        }
    }
}
