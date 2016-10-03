namespace ProcessingTools.Tagger.Controllers
{
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Contracts;

    [Description("Expand lower taxa.")]
    public class ExpandLowerTaxaController : TaggerControllerFactory, IExpandLowerTaxaController
    {
        private readonly ILogger logger;

        public ExpandLowerTaxaController(IDocumentFactory documentFactory, ILogger logger)
            : base(documentFactory)
        {
            this.logger = logger;
        }

        protected override Task Run(IDocument document, ProgramSettings settings)
        {
            return Task.Run(() =>
            {
                var expander = new Expander(document.XmlDocument.OuterXml, this.logger);

                expander.StableExpand();

                expander.ForceExactSpeciesMatchExpand();

                document.Xml = expander.Xml;
            });
        }
    }
}
