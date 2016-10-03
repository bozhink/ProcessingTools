namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary.Floats;
    using ProcessingTools.Contracts;
    using ProcessingTools.MediaType.Services.Data.Contracts;

    [Description("Resolve media-types.")]
    public class ResolveMediaTypesController : TaggerControllerFactory, IResolveMediaTypesController
    {
        private readonly IMediaTypeDataService service;
        private readonly ILogger logger;

        public ResolveMediaTypesController(
            IDocumentFactory documentFactory,
            IMediaTypeDataService service,
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
            var parser = new MediaTypesResolver(document.Xml, this.service, this.logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
