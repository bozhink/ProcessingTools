namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
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
