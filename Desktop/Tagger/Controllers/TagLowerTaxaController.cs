namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaController : TaggerControllerFactory, ITagLowerTaxaController
    {
        private readonly IBiotaxonomicBlackListIterableDataService service;
        private readonly ILogger logger;

        public TagLowerTaxaController(
            IDocumentFactory documentFactory,
            IBiotaxonomicBlackListIterableDataService service,
            ILogger logger)
            : base(documentFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new LowerTaxaTagger(document.Xml, this.service, this.logger);

            await tagger.Tag();

            document.Xml = tagger.Xml.NormalizeXmlToSystemXml();
        }
    }
}
