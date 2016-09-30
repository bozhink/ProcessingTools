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

        public TagLowerTaxaController(IDocumentFactory documentFactory, IBiotaxonomicBlackListIterableDataService service)
            : base(documentFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings, ILogger logger)
        {
            var tagger = new LowerTaxaTagger(document.Xml, this.service, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml.NormalizeXmlToSystemXml();
        }
    }
}
