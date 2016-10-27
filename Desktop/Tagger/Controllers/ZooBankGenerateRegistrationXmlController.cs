namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Transformers;

    using Providers;

    // TODO: DI
    [Description("Generate xml document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlController : IZooBankGenerateRegistrationXmlController
    {
        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var transformer = new XslTransformer(new ZoobankNlmXslTransformProvider(new XslTransformCache()));
            var text = await transformer.Transform(document.Xml);
            document.Xml = text;

            return true;
        }
    }
}
