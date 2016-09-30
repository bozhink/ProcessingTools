namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider;

    public abstract class TaggerControllerFactory : ITaggerController
    {
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

            try
            {
                var document = new TaxPubDocument(Resources.ContextWrapper);
                document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
                document.SchemaType = settings.ArticleSchemaType;

                await this.Run(document, settings, logger);

                context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
            }
            catch (Exception e)
            {
                logger?.Log(e, string.Empty);
            }
        }

        protected abstract Task Run(IDocument document, ProgramSettings settings, ILogger logger);
    }
}
