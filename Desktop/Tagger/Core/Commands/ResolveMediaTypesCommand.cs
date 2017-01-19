namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Floats;

    [Description("Resolve media-types.")]
    public class ResolveMediaTypesController : GenericXmlContextParserController<IMediatypesParser>, IResolveMediaTypesController
    {
        public ResolveMediaTypesController(IMediatypesParser parser)
            : base(parser)
        {
        }
    }
}
