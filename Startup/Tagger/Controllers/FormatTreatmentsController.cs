namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    public class FormatTreatmentsController : TaggerControllerFactory, IFormatTreatmentsController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var formatter = new TreatmentFormatter(settings.Config, document.OuterXml, logger);

            await formatter.Format();

            document.LoadXml(formatter.Xml);
        }
    }
}