namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.EnvironmentTerms;
    using ProcessingTools.Contracts;

    [Description("Tag envo terms using EXTRACT.")]
    public class TagEnvironmentTermsWithExtractController : ITagEnvironmentTermsWithExtractController
    {
        private readonly IEnvironmentTermsWithExtractTagger tagger;

        public TagEnvironmentTermsWithExtractController(IEnvironmentTermsWithExtractTagger tagger)
        {
            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.tagger = tagger;
        }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return await this.tagger.Tag(document);
        }
    }
}
