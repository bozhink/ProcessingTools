namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Tag floats.")]
    public class TagFloatsController : TaggerControllerFactory, ITagFloatsController
    {
        private readonly IFloatsTagger tagger;

        public TagFloatsController(IDocumentFactory documentFactory, IFloatsTagger tagger)
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
            await this.tagger.Tag(document.XmlDocument.DocumentElement);
        }
    }
}