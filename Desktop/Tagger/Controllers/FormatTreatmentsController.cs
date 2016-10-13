namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    [Description("Format treatments.")]
    public class FormatTreatmentsController : TaggerControllerFactory, IFormatTreatmentsController
    {
        private readonly ILogger logger;

        public FormatTreatmentsController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var formatter = new TreatmentFormatter(this.logger);

            await formatter.Format(document);
        }
    }
}