namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts;

    [Description("Tag abbreviations.")]
    public class TagAbbreviationsController : TaggerControllerFactory, ITagAbbreviationsController
    {
        private readonly IAbbreviationsTagger abbreviationsTagger;

        public TagAbbreviationsController(
            IDocumentFactory documentFactory,
            IAbbreviationsTagger abbreviationsTagger)
            : base(documentFactory)
        {
            if (abbreviationsTagger == null)
            {
                throw new ArgumentNullException(nameof(abbreviationsTagger));
            }

            this.abbreviationsTagger = abbreviationsTagger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.abbreviationsTagger.Tag(document.XmlDocument);
        }
    }
}
