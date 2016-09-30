namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [Description("Custom XSL transform.")]
    public class RunCustomXslTransformController : TaggerControllerFactory, IRunCustomXslTransformController
    {
        private readonly IDocumentXslProcessor processor;

        public RunCustomXslTransformController(IDocumentFactory documentFactory, IDocumentXslProcessor processor)
            : base(documentFactory)
        {
            if (processor == null)
            {
                throw new ArgumentNullException(nameof(processor));
            }

            this.processor = processor;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            int numberOfFileNames = settings.FileNames.Count();

            if (numberOfFileNames < 3)
            {
                throw new ApplicationException("The name of the XSLT file should be set.");
            }

            this.processor.XslFilePath = settings.FileNames.ElementAt(2);

            await this.processor.Process(document);
        }
    }
}
