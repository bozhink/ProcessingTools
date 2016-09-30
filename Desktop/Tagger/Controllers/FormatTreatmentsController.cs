namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    [Description("Format treatments.")]
    public class FormatTreatmentsController : TaggerControllerFactory, IFormatTreatmentsController
    {
        protected override async Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var formatter = new TreatmentFormatter(document.Xml, logger);

            await formatter.Format();

            document.Xml = formatter.Xml;
        }
    }
}