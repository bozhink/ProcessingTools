namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Transformers;

    using Providers;

    // TODO: DI
    [Description("Generate xml document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlController : TaggerControllerFactory, IZooBankGenerateRegistrationXmlController
    {
        public ZooBankGenerateRegistrationXmlController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override async Task Run(IDocument document, ProgramSettings settingsogger)
        {
            var transformer = new XslTransformer(new ZoobankNlmXslTransformProvider(new XslTransformCache()));
            var text = await transformer.Transform(document.Xml);
            document.Xml = text;
        }
    }
}
