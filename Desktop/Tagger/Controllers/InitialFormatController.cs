namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts;

    [Description("Initial format.")]
    public class InitialFormatController : TaggerControllerFactory, IInitialFormatController
    {
        private readonly IDocumentInitialFormatter formatter;

        public InitialFormatController(IDocumentFactory documentFactory, IDocumentInitialFormatter formatter)
            : base(documentFactory)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.formatter = formatter;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.formatter.Format(document);
        }
    }
}
