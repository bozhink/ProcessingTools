namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.ExternalLinks;

    [Description("Tag web links and DOI.")]
    public class TagWebLinksController : TaggerControllerFactory, ITagWebLinksController
    {
        private readonly IExternalLinksTagger tagger;

        public TagWebLinksController(
            IDocumentFactory documentFactory,
            IExternalLinksTagger tagger)
            : base(documentFactory)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        protected override async Task Run(IDocument document, IProgramSettings settings)
        {
            await this.tagger.Tag(document);
        }
    }
}
