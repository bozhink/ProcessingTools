namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Factories;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaController : TaggerControllerFactory, IExpandLowerTaxaController
    {
        protected override Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            return Task.Run(() =>
            {
                var expander = new Expander(settings.Config, document.OuterXml, logger);

                expander.StableExpand();

                expander.ForceExactSpeciesMatchExpand();

                document.LoadXml(expander.Xml);
            });
        }
    }
}
