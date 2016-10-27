namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Resolve media-types.")]
    public class ResolveMediaTypesController : GenericXmlContextParserController<IMediaTypesResolver>, IResolveMediaTypesController
    {
        public ResolveMediaTypesController(IMediaTypesResolver parser)
            : base(parser)
        {
        }
    }
}
