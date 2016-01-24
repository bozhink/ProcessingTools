namespace ProcessingTools.MainProgram.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    public class ValidateTaxaController : TaggerControllerFactory, IValidateTaxaController
    {
        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var validator = new TaxonomicNamesValidator(settings.Config, document.OuterXml, logger);

            await validator.Validate();
        }
    }
}