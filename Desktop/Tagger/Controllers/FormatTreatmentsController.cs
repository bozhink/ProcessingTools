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
        public FormatTreatmentsController(IDocumentFactory documentFactory)
            : base(documentFactory)
        {
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var formatter = new TreatmentFormatter(document.Xml, logger);

            await formatter.Format();

            document.Xml = formatter.Xml;
        }
    }
}