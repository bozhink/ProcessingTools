namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.DocumentProvider.Factories;
    using ProcessingTools.Layout.Processors.Taggers;
    using ProcessingTools.Xml.Extensions;

    public abstract class StringTaggerControllerFactory : ITaggerController
    {
        protected abstract IStringDataMiner Miner { get; }

        protected abstract XmlElement TagModel { get; }

        public async Task Run(XmlNode context, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (namespaceManager == null)
            {
                throw new ArgumentNullException(nameof(namespaceManager));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            // TODO: DI
            var documentFactory = new TaxPubDocumentFactory();

            try
            {
                var document = documentFactory.Create(Resources.ContextWrapper);
                document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
                document.SchemaType = settings.ArticleSchemaType;

                var textContent = document.XmlDocument.GetTextContent();
                var data = await this.Miner.Mine(textContent);

                var tagger = new StringTagger(logger);

                await tagger.Tag(document, data, this.TagModel, XPathConstants.SelectContentNodesXPathTemplate);

                context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
            }
            catch (Exception e)
            {
                logger?.Log(e, string.Empty);
            }
        }
    }
}
