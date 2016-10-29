namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts.Controllers;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.ZooBank;

    [Description("Generate xml document for registration in ZooBank.")]
    public class ZooBankGenerateRegistrationXmlController : IZooBankGenerateRegistrationXmlController
    {
        private readonly IZooBankRegistrationXmlGenerator generator;

        public ZooBankGenerateRegistrationXmlController(IZooBankRegistrationXmlGenerator generator)
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            this.generator = generator;
        }

        public Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.generator.Generate(document);
        }
    }
}
