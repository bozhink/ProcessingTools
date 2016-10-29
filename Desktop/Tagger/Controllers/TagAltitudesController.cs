namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Geo;

    [Description("Tag altitudes.")]
    public class TagAltitudesController : GenericDocumentTaggerController<IAltitudesTagger>, ITagAltitudesController
    {
        public TagAltitudesController(IAltitudesTagger tagger)
            : base(tagger)
        {
        }
    }
}
