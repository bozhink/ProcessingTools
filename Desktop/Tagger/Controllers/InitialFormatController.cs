namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.DocumentProvider.Extensions;
    using ProcessingTools.Layout.Processors.Formatters;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Transformers;

    using Providers;

    // TODO: DI
    [Description("Initial format.")]
    public class InitialFormatController : TaggerControllerFactory, IInitialFormatController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            // TODO: Factory
            IXslTransformProvider xslTransformProvider = null;
            switch (settings.ArticleSchemaType)
            {
                case SchemaType.Nlm:
                    xslTransformProvider = new NlmInitialFormatXslTransformProvider(new XslTransformCache());
                    break;

                default:
                    xslTransformProvider = new SystemInitialFormatXslTransformProvider(new XslTransformCache());
                    break;
            }

            // TODO: TaggerController.Run should use IDocument
            var taxpubDocument = document.ToTaxPubDocument();

            var transformer = new XslTransformer(xslTransformProvider);
            taxpubDocument.Xml = await transformer.Transform(taxpubDocument.Xml);

            // TODO: DI
            var formatter = new DocumentInitialFormatter();
            await formatter.Format(taxpubDocument);
            document.LoadXml(taxpubDocument.Xml);
        }
    }
}
