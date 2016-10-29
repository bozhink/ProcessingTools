namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Coordinates;

    [Description("Parse coordinates.")]
    public class ParseCoordinatesController : GenericXmlContextParserController<ICoordinatesParser>, IParseCoordinatesController
    {
        public ParseCoordinatesController(ICoordinatesParser parser)
            : base(parser)
        {
        }
    }
}
