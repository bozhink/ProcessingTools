namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Coordinates;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesCommand : GenericXmlContextParserCommand<ICoordinatesParser>, IParseCoordinatesCommand
    {
        public ParseCoordinatesCommand(ICoordinatesParser parser)
            : base(parser)
        {
        }
    }
}
