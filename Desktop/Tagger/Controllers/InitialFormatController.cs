namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Format;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Processors;

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

            var transformer = new XslTransformer();

            string xml = await transformer.Transform(document, xslTransformProvider);
            var formatter = new NlmInitialFormatter(xml);
            await formatter.Format();
            document.LoadXml(formatter.Xml);
        }
    }
}
