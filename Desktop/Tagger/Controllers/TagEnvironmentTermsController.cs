namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.EnvironmentTerms;
    using ProcessingTools.Contracts;

    [Description("Tag envo terms using local database.")]
    public class TagEnvironmentTermsController : TaggerControllerFactory, ITagEnvironmentTermsController
    {
        private readonly IEnvironmentTermsTagger tagger;

        public TagEnvironmentTermsController(
            IDocumentFactory documentFactory,
            IEnvironmentTermsTagger tagger)
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
