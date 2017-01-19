namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Coordinates;

    [Description("Tag coordinates.")]
    public class TagCoordinatesController : GenericDocumentTaggerController<ICoordinatesTagger>, ITagCoordinatesController
    {
        public TagCoordinatesController(ICoordinatesTagger tagger)
            : base(tagger)
        {
        }
    }
}
