namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Coordinates;

    [System.ComponentModel.Description("Parse coordinates.")]
    public class ParseCoordinatesCommand : XmlContextParserCommand<ICoordinatesParser>, IParseCoordinatesCommand
    {
        public ParseCoordinatesCommand(ICoordinatesParser parser)
            : base(parser)
        {
        }
    }
}
