namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

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

        public TagLowerTaxaController(IBiotaxonomicBlackListIterableDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override async Task Run(XmlDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new LowerTaxaTagger(document.OuterXml, this.service, logger);

            await tagger.Tag();

            document.LoadXml(tagger.Xml.NormalizeXmlToSystemXml());
        }
    }
}
