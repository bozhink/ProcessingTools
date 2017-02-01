namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Processors.Coordinates;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesCommand : GenericXmlContextParserCommand<ICoordinatesParser>, IParseCoordinatesCommand
    {
        public ParseCoordinatesCommand(ICoordinatesParser parser)
            : base(parser)
        {
        }
    }
}
