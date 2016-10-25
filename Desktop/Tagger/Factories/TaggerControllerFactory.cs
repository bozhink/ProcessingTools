namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Contracts;

    public abstract class TaggerControllerFactory : ITaggerController
    {
        private readonly IDocumentFactory documentFactory;

        public TaggerControllerFactory(IDocumentFactory documentFactory)
        {
            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            this.documentFactory = documentFactory;
        }

        protected IDocumentFactory DocumentFactory => this.documentFactory;

        public async Task Run(XmlNode context, IProgramSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var document = this.documentFactory.Create(Resources.ContextWrapper);
            document.XmlDocument.DocumentElement.InnerXml = context.InnerXml;
            document.SchemaType = settings.ArticleSchemaType;

            await this.Run(document, settings);

            context.InnerXml = document.XmlDocument.DocumentElement.InnerXml;
        }

        protected abstract Task Run(IDocument document, IProgramSettings settings);
    }
}
