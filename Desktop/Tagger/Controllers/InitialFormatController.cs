namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.DocumentProvider.Extensions;
    using ProcessingTools.Layout.Processors.Contracts;

    [Description("Initial format.")]
    public class InitialFormatController : TaggerControllerFactory, IInitialFormatController
    {
        private readonly IDocumentInitialFormatter formatter;

        public InitialFormatController(IDocumentInitialFormatter formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            // TODO: TaggerController.Run should use IDocument
            var taxpubDocument = document.ToTaxPubDocument();
            taxpubDocument.SchemaType = settings.ArticleSchemaType;

            await this.formatter.Format(taxpubDocument);
            document.LoadXml(taxpubDocument.Xml);
        }
    }
}
