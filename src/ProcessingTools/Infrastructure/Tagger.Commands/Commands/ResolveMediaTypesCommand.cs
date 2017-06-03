namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Floats;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Resolve media-types.")]
    public class ResolveMediaTypesCommand : GenericXmlContextParserCommand<IMediatypesParser>, IResolveMediaTypesCommand
    {
        public ResolveMediaTypesCommand(IMediatypesParser parser)
            : base(parser)
        {
        }
    }
}
