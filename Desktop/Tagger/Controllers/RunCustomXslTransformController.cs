namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.Controllers;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [Description("Custom XSL transform.")]
    public class RunCustomXslTransformController : IRunCustomXslTransformController
    {
        private readonly IDocumentXslProcessor processor;

        public RunCustomXslTransformController(IDocumentXslProcessor processor)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.processor = processor;
        }

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

            int numberOfFileNames = settings.FileNames.Count();
            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The name of the XSLT file should be set.");
            }

            this.processor.XslFilePath = settings.FileNames.ElementAt(2);

            await this.processor.Process(document);

            return true;
        }
    }
}
