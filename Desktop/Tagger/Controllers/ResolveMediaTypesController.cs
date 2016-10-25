namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Resolve media-types.")]
    public class ResolveMediaTypesController : TaggerControllerFactory, IResolveMediaTypesController
    {
        private readonly IMediaTypesResolver parser;

        public ResolveMediaTypesController(IDocumentFactory documentFactory, IMediaTypesResolver parser)
            : base(documentFactory)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(IDocument document, IProgramSettings settings)
        {
            await this.parser.Parse(document.XmlDocument.DocumentElement);
        }
    }
}
