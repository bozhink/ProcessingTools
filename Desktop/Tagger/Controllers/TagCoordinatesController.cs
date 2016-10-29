namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
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
