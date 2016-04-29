namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Xml.Extensions;

    public abstract class StringTaggerControllerFactory : ITaggerController
    {
        protected abstract IStringDataMiner Miner { get; }

        protected abstract XmlElement TagModel { get; }

        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException("namespaceManager");
            }

            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            XmlDocument document = new XmlDocument
            {
                PreserveWhitespace = true
            };

            try
            {
                document.LoadXml(Resources.ContextWrapper);
                document.DocumentElement.InnerXml = context.InnerXml;

                var textContent = document.GetTextContent();
                var data = await this.Miner.Mine(textContent);

                var tagger = new StringTagger(document.OuterXml, data, this.TagModel, XPathConstants.SelectContentNodesXPathTemplate, namespaceManager, logger);

                await tagger.Tag();

                document.LoadXml(tagger.Xml);

                context.InnerXml = document.DocumentElement.InnerXml;
            }
            catch (Exception e)
            {
                logger?.Log(e, string.Empty);
            }
        }
    }
}
