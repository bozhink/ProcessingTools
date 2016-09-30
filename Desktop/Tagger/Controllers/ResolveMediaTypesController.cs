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

        public ResolveMediaTypesController(IDocumentFactory documentFactory, IMediaTypeDataService service)
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
            var parser = new MediaTypesResolver(document.Xml, this.service, logger);

            await parser.Parse();

            document.Xml = parser.Xml;
        }
    }
}
