namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Floats;

    [System.ComponentModel.Description("Resolve media-types.")]
    public class ResolveMediaTypesCommand : XmlContextParserCommand<IMediatypesParser>, IResolveMediaTypesCommand
    {
        public ResolveMediaTypesCommand(IMediatypesParser parser)
            : base(parser)
        {
        }
    }
}
